using DeepParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_Linear_Regression {
    public class Model {
        /// <summary>
        /// Name of the regressant
        /// </summary>
        public string RegressantName { get; }

        /// <summary>
        /// Values of regressant
        /// </summary>
        public List<double> RegressantValues { get; private set; }

        /// <summary>
        /// Dictionary of regressors
        /// </summary>
        public Dictionary<string, List<double>> Regressors { get; private set; }

        /// <summary>
        /// Dictionary of non-filter regressors
        /// </summary>
        private Dictionary<string, List<double>> NonFilterRegressors { get; set; }

        /// <summary>
        /// Dictionary of functions that were used in preprocessing data
        /// </summary>
        public Dictionary<string, List<string>> ProcessFunctions { get; private set; }

        /// <summary>
        /// Dictionary that contains correlation coefficient for each regressor
        /// </summary>
        public Dictionary<string, double> CorrelationCoefficient { get; private set; }

        public Model(string regerssantName, List<double> regressantValues, Dictionary<string, List<double>> regressors = null) {
            RegressantName = regerssantName;
            RegressantValues = regressantValues;            
            ProcessFunctions = new Dictionary<string, List<string>>();
            CorrelationCoefficient = new Dictionary<string, double>();

            if (regressors is null) {
                Regressors = null;
                NonFilterRegressors = null;
            }
            else {
                SetNewRegressors(regressors);
            }
        }

        /// <summary>
        /// Set new regressord dictionary
        /// </summary>
        /// <param name="regressors">Dictionary of regressors</param>
        public void SetNewRegressors(Dictionary<string, List<double>> regressors) {
            Regressors = new Dictionary<string, List<double>>(regressors);
            CalcNewCorrelationCoefficients();
        }

        /// <summary>
        /// Remove regressor by name
        /// </summary>
        /// <param name="regressorName">Regressor's name</param>
        private void RemoveRegressor(string regressorName) {
            Regressors.Remove(regressorName);
        }

        /// <summary>
        /// Calculation of correlation coefficients between regressant and regressors
        /// </summary>
        private void CalcNewCorrelationCoefficients() {
            CorrelationCoefficient.Clear();
            foreach(var regressor in Regressors) {
                CorrelationCoefficient.Add(regressor.Key, Statistics.PearsonCorrelationCoefficient(RegressantValues, regressor.Value));
            }
        }

        /// <summary>
        /// Functional data preprocessing
        /// </summary>
        public void StartFunctionalPreprocessing() {
            List<string> regressorsNames = new List<string>(Regressors.Keys);
            foreach(var regressor in regressorsNames) {
                List<string> functions = new List<string>();
                double startCorrCoef = Statistics.PearsonCorrelationCoefficient(RegressantValues,
                    Statistics.ConvertValuesToInterval(2.0, 102.0, Regressors[regressor]));
                double maxFuncCorrCoeff = 0;
                string maxFuncName = "";

                while (true) {
                    // Convert values to interval [2, 102]
                    Regressors[regressor] = Statistics.ConvertValuesToInterval(2.0, 102.0, Regressors[regressor]);

                    // For each function we find the correlation coefficient with the regressant
                    foreach (var func in Statistics.PreprocessingFunctions) {
                        double funcCorr = Statistics.PearsonCorrelationCoefficient(RegressantValues, func.Value(Regressors[regressor]));

                        // Find the function with the maximum correlation coefficient
                        if (Math.Abs(funcCorr) > Math.Abs(maxFuncCorrCoeff)) {
                            maxFuncCorrCoeff = funcCorr;
                            maxFuncName = func.Key;
                        }
                    }
                    // Check if the application of this function makes sense
                    if (Math.Abs(maxFuncCorrCoeff) > Math.Abs(startCorrCoef) + 0.01) {
                        functions.Add(maxFuncName);
                        startCorrCoef = maxFuncCorrCoeff;
                        Regressors[regressor] = Statistics.PreprocessingFunctions[maxFuncName](Regressors[regressor]);
                    }
                    else {
                        ProcessFunctions.Add(regressor, functions);
                        CorrelationCoefficient[regressor] = startCorrCoef;
                        break;
                    }
                }
            }
            NonFilterRegressors = new Dictionary<string, List<double>>(Regressors);
        }

        /// <summary>
        /// Filtering of regressors by comparison with the threshold value of the correlation coefficient
        /// </summary>
        /// <param name="thresholdCorrCoeff">Threshold value of the correlation coefficient</param>
        public void EmpiricalWayFilterRegressors(double thresholdCorrCoeff) {
            CheckNonFilterRegressors();

            foreach (var corrCoeff in CorrelationCoefficient) {
                if (Math.Abs(corrCoeff.Value) < thresholdCorrCoeff) {
                    RemoveRegressor(corrCoeff.Key);
                }
            }
        }

        /// <summary>
        /// Check for significance at the significance level a = 0.05
        /// </summary>
        public void ClassicWayFilterRegressors() {
            CheckNonFilterRegressors();
            int k = RegressantValues.Count;
            double alpha = 0.05;

            // For each regressor we find the probability of the t-statistic falling into the critical region
            foreach (var corrCoeff in CorrelationCoefficient) {
                // Find the critical area
                double p = 2.0 * (1.0 - alglib.studenttdistribution(k - 2, Statistics.T_Statistics(k - 2, corrCoeff.Value)));

                if (p >= alpha) {
                    RemoveRegressor(corrCoeff.Key);
                }
            }
        }

        /// <summary>
        /// Check if a non-filter regressor was created
        /// </summary>
        private void CheckNonFilterRegressors() {
            if (NonFilterRegressors is null) {
                NonFilterRegressors = new Dictionary<string, List<double>>(Regressors);
            }
        }

        /// <summary>
        /// Restoring the regressors to their pre-filter state
        /// </summary>
        public void RestoreNonFilterRegressors() {
            SetNewRegressors(NonFilterRegressors);
        }
    }
}
