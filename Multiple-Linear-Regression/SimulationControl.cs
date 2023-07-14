using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiple_Linear_Regression {
    public class SimulationControl {

        /// <summary>
        /// Check whether the regressorKey value is within the boundaries of the definition area
        /// </summary>
        /// <param name="regressorName">Name of adjustable regressorKey</param>
        /// <param name="regressorValue">Value of adjustable regressorKey</param>
        /// <param name="definitionArea">Definition area (min, max) for regressor</param>
        public void CheckDefAreaForRegressor(string regressorName, double regressorValue, (double, double) definitionArea) {
            if (regressorValue < definitionArea.Item1) {
                MessageBox.Show($"Значение регрессора ({regressorName}) не попадает в область определения (Минимальное значение - " +
                    $"{definitionArea.Item1}). Модель может работать некорректно.");
            }
            else if (regressorValue > definitionArea.Item2) {
                MessageBox.Show($"Значение регрессора ({regressorName}) не попадает в область определения (Максимальное значение - " +
                   $"{definitionArea.Item2}). Модель может работать некорректно.");
            }
        }

        /// <summary>
        /// Calculation number group of correlated regressors
        /// </summary>
        /// <param name="numberOfRegressors">Number of regressors</param>
        /// <returns>Number of groups of correlated regressors</returns>
        public int CalcNumberGroupOfCorrelatedRegressors(int numberOfRegressors) {
            int numberGroupOfCorrelatedRegressors = (int)Math.Log(numberOfRegressors, 2);

            return numberGroupOfCorrelatedRegressors > 3 ? numberGroupOfCorrelatedRegressors : 3;
        }

        /// <summary>
        /// Update regressors with regressors impact
        /// </summary>
        /// <param name="regressors">Dictionary with regressors and regressors values</param>
        /// <param name="changableRegressorName">Cnangable regressor name</param>
        /// <param name="needUpdateRegressors">Not-updated regressors</param>
        /// <param name="regressorsImpact">Dictionary with coefficients of regressors impact</param>
        public void UpdateRegressors(Dictionary<string, double> regressors, 
                                      string changableRegressorName,
                                      List<string> needUpdateRegressors,
                                      Dictionary<string, Dictionary<string, Dictionary<string, double>>> regressorsImpact) {

            needUpdateRegressors.Remove(changableRegressorName);
            if (needUpdateRegressors.Count != 0) {
                foreach (var regressor in needUpdateRegressors) {
                    regressors[regressor] = regressorsImpact[changableRegressorName][regressor]["a"] +
                       regressorsImpact[changableRegressorName][regressor]["b"] * regressors[changableRegressorName];
                }
                UpdateRegressors(regressors, needUpdateRegressors[0], needUpdateRegressors, regressorsImpact);
            }
        }

        /// <summary>
        /// Get coefficients of regressors mutual impact
        /// </summary>
        /// <param name="regressorsCorrelation">Correlation coefficients between regressors</param>
        /// <param name="numberGroupOfCorrelatedRegressors">Number of group of correlated regressors</param>
        /// <param name="regressorsNames">List with names of regressors</param>
        /// <param name="nonCombinedRegressors">Dictionary with non-combined regressors</param>
        /// <returns>Dictionary with coefficients of regressors mutual impact</returns>
        public Dictionary<string, Dictionary<string, Dictionary<string, double>>> GetRegressorsMutualImpact(
                       Dictionary<string, Dictionary<string, double>> regressorsCorrelation,
                       int numberGroupOfCorrelatedRegressors,
                       List<string> regressorsNames,
                       Dictionary<string, List<double>> nonCombinedRegressors) {

            Dictionary<string, Dictionary<string, Dictionary<string, double>>> regressorsImpact =
                new Dictionary<string, Dictionary<string, Dictionary<string, double>>>();

            // Find threshold correlation coefficients
            List<double> corrIntervals = GetCorrIntervals(numberGroupOfCorrelatedRegressors);

            // For each regressor, find the impact on the other regressors
            foreach (var mainRegressorName in regressorsNames) {
                Dictionary<string, Dictionary<string, double>> nextRegressorForMain =
                    new Dictionary<string, Dictionary<string, double>>();
                List<string> unUsedRegressors = new List<string>(regressorsNames);
                List<List<string>> groupsRegressors = new List<List<string>>();

                unUsedRegressors.Remove(mainRegressorName);

                // Fill second regressors for the main one
                foreach (var corrLevel in corrIntervals) {

                    // Find regressors for the next correlation level
                    List<string> nextGroupRegressors = RegressorsForImpact(unUsedRegressors, mainRegressorName,
                                                                           regressorsCorrelation, corrLevel);

                    if (nextGroupRegressors.Count > 0) {
                        groupsRegressors.Add(nextGroupRegressors);
                        FillNextGroupRegressors(ref nextRegressorForMain, groupsRegressors, mainRegressorName,
                                                regressorsCorrelation, nonCombinedRegressors);
                        unUsedRegressors = unUsedRegressors.Except(nextGroupRegressors).ToList();
                    }
                }

                regressorsImpact[mainRegressorName] = nextRegressorForMain;
            }

            return regressorsImpact;
        } 

        /// <summary>
        /// Get threshold values for find correlation group of regressors
        /// </summary>
        /// <param name="numberGroupOfCorrelatedRegressors">Number group of correlated regressors</param>
        /// <returns>List of threshold values</returns>
        private List<double> GetCorrIntervals(int numberGroupOfCorrelatedRegressors) {
            List<double> thresholdValues = new List<double>();
            double oneStep = 1.0 / numberGroupOfCorrelatedRegressors;

            // Find next threshold value
            for (int i = 0; i < numberGroupOfCorrelatedRegressors; i++) {
                thresholdValues.Add(oneStep * i);
            }

            thresholdValues.Reverse();

            return thresholdValues;
        }

        /// <summary>
        /// Get regressors wich coefficient of correlation more than threshold value
        /// </summary>
        /// <param name="secRegressors">List of regressors to choose from</param>
        /// <param name="mainRegressor">Main regressor</param>
        /// <param name="regressorsCorrelation">Dictionary with coefficint of correlation between regressors</param>
        /// <param name="thresholdCorrValue">Threshold value of correlation coefficient</param>
        /// <returns>List of regressors</returns>
        private List<string> RegressorsForImpact(List<string> secRegressors, string mainRegressor,
           Dictionary<string, Dictionary<string, double>> regressorsCorrelation, double thresholdCorrValue = 0) {

            List<string> selectedRegressors = new List<string>();

            foreach (var regressor in secRegressors) {
                if (Math.Abs(regressorsCorrelation[mainRegressor][regressor]) > thresholdCorrValue) {
                    selectedRegressors.Add(regressor);
                }
            }

            return selectedRegressors;
        }

        /// <summary>
        /// Fill dictionary impact for group of regressors
        /// </summary>
        /// <param name="regressorsForMain">Dictionary impact for main regressor</param>
        /// <param name="groupsOfRegressors">Grouped regressors by corr value</param>
        /// <param name="mainRegressor">Main regressor name</param>
        private void FillNextGroupRegressors(ref Dictionary<string, Dictionary<string, double>> regressorsForMain,
                                             List<List<string>> groupsOfRegressors, 
                                             string mainRegressor,
                                             Dictionary<string, Dictionary<string, double>> regressorsCorrelation,
                                             Dictionary<string, List<double>> nonCombinedRegressors) {

            foreach (var secRegressor in groupsOfRegressors.Last()) {
                regressorsForMain[secRegressor] = ImpactCoefficientsGroup(secRegressor, mainRegressor,
                                                                          nonCombinedRegressors[secRegressor],
                                                                          nonCombinedRegressors[mainRegressor],
                                                                          groupsOfRegressors.GetRange(0, groupsOfRegressors.Count() - 1), 
                                                                          regressorsCorrelation);
            }
        }

        /// <summary>
        /// Find coefficients of impact between correlated regressors
        /// </summary>
        /// <param name="yRegressor">Y-regressor</param>
        /// <param name="xRegressor">X-regressor</param>
        /// <param name="prevGroupsRegressors">Groups of regressors with a high correlation coefficient</param>
        /// <returns>Coefficients of impact between regressors</returns>
        private Dictionary<string, double> ImpactCoefficientsGroup(string yRegressor, 
                                                                   string xRegressor, 
                                                                   List<double> yRegressorValues, 
                                                                   List<double> xRegressorValues,
                                                                   List<List<string>> prevGroupsRegressors,
                                                                   Dictionary<string, Dictionary<string, double>> regressorsCorrelation) {

            double rValue = 0;

            if (prevGroupsRegressors.Count > 0) {
                List<int> indexesOfRegressors = Enumerable.Repeat(0, prevGroupsRegressors.Count).ToList();

                // Find r-value
                while (true) {

                    // Check if we have used all regressors
                    if (indexesOfRegressors[0] >= prevGroupsRegressors[0].Count) {
                        break;
                    }

                    double nextCoeff = 1;
                    string leftRegressor = xRegressor;
                    string rightRegressor = "";

                    // Find next multiple of correlation coefficients for r-value
                    for (int i = 0; i < indexesOfRegressors.Count; i++) {
                        rightRegressor = prevGroupsRegressors[i][indexesOfRegressors[i]];
                        nextCoeff *= regressorsCorrelation[leftRegressor][rightRegressor];
                        leftRegressor = rightRegressor;
                    }
                    nextCoeff *= regressorsCorrelation[leftRegressor][yRegressor];

                    rValue += nextCoeff;
                    CalcIndexesOfNextRegressors(ref indexesOfRegressors, prevGroupsRegressors, 
                                                indexesOfRegressors.Count - 1);
                }
            }
            else {
                rValue = regressorsCorrelation[yRegressor][xRegressor];
            }

            // Checking exceptional situations
            rValue = rValue < -1 ? -1 : rValue;
            rValue = rValue > 1 ? 1 : rValue;

            return GetCoefficientsForImpactEquation(rValue, yRegressorValues, xRegressorValues);
        }

        /// <summary>
        /// Finding regressor indexes to calculate the next term in the r-value
        /// </summary>
        /// <param name="indexesOfRegressors">List with indexes for next coeff in r-value</param>
        /// <param name="prevGroupsRegressors">Groups of more correlated regressors</param>
        /// <param name="position">Variable regressor position</param>
        private void CalcIndexesOfNextRegressors(ref List<int> indexesOfRegressors,
            List<List<string>> prevGroupsRegressors, int position) {

            if (position >= 0 && indexesOfRegressors[position] == prevGroupsRegressors[position].Count - 1) {
                indexesOfRegressors[position] = 0;
                CalcIndexesOfNextRegressors(ref indexesOfRegressors, prevGroupsRegressors, position - 1);
            }
            else if (position < 0) {
                indexesOfRegressors[0] = prevGroupsRegressors[0].Count;
            }
            else {
                indexesOfRegressors[position]++;
            }
        }

        /// <summary>
        /// Get coefficients for regressors impact equation
        /// </summary>
        /// <param name="r">Correlation coefficient</param>
        /// <param name="yRegressor">Y-regressor</param>
        /// <param name="xRegressor">X-regressor</param>
        /// <returns>Coefficients of impact equation</returns>
        private Dictionary<string, double> GetCoefficientsForImpactEquation(double r, List<double> yRegressor, 
                                                                            List<double> xRegressor) {
            Dictionary<string, double> coeffs = new Dictionary<string, double>();
            coeffs["b"] = r * (Statistics.StandardDeviation(yRegressor) /
                Statistics.StandardDeviation(xRegressor));
            coeffs["a"] = yRegressor.Average() - coeffs["b"] * xRegressor.Average();

            return coeffs;
        }

        /// <summary>
        /// Calculate correlation coefficients between regressors
        /// </summary>
        /// <param name="regressors">Dictionary with regressors</param>
        /// <returns>Dictionary with coefficients of correlation between all regressors</returns>
        public Dictionary<string, Dictionary<string, double>> CalcCorrelationCoefficients(Dictionary<string, List<double>> regressors) {

            Dictionary<string, Dictionary<string, double>> regressorsCorrelation = 
                new Dictionary<string, Dictionary<string, double>>();

            // Find correlation coefficients between all regressors
            foreach (var mainRegressor in regressors.Keys) {
                Dictionary<string, double> secondsRegressorsForMain = new Dictionary<string, double>();

                // For each regressor (except the main one) find correlation coefficient with main regressor
                foreach(var secRegressor in regressors.Keys) {
                    if (secRegressor != mainRegressor) {

                        // If we have already found the correlation coefficient for a given pair of factors,
                        // we take the value from the dictionary
                        if (regressorsCorrelation.ContainsKey(secRegressor)) {
                            secondsRegressorsForMain[secRegressor] = regressorsCorrelation[secRegressor][mainRegressor];
                        }
                        else {
                            secondsRegressorsForMain[secRegressor] = Statistics.PearsonCorrelationCoefficient(
                                regressors[mainRegressor], regressors[secRegressor]);
                        }
                    }
                }
                regressorsCorrelation[mainRegressor] = secondsRegressorsForMain;
            }

            return regressorsCorrelation;
        }
    }
}   
