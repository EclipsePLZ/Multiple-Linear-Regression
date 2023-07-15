using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_Linear_Regression.Forms {
    public class RegressorsGrouping {

        /// <summary>
        /// Get all combinations of regressors
        /// </summary>
        /// <param name="correlatedRegressors">List of group of correlated regressors</param>
        /// <param name="index">Index of group</param>
        /// <param name="state">List for keeping combination</param>
        /// <param name="result">Result that contains all possible combinations</param>
        public void GetRegressorsCombinations(List<List<string>> correlatedRegressors, int index, 
                                              List<string> state, List<List<string>> result) {

            if (index >= correlatedRegressors.Count) {
                result.Add(new List<string>(state));
                return;
            }
            foreach (var item in correlatedRegressors[index]) {
                state.Add(item);
                GetRegressorsCombinations(correlatedRegressors, index + 1, state, result);
                state.RemoveAt(state.Count - 1);
            }
        }

        /// <summary>
        /// Find correlated regressors
        /// </summary>
        /// <param name="thresholdCorr">Threshold value for check correlation</param>
        /// <param name="regressors">Dcitionary with regressors</param>
        /// <returns>List with groups of correlated regressors</returns>
        public List<List<string>> GetCorrelatedRegressors(double thresholdCorr, Dictionary<string, List<double>> regressors) {
            List<List<string>> corrRegressors = new List<List<string>>();
            List<string> nonCombinedRegressors = OperationsWithModels.GetNonCombinedRegressors(regressors.Keys.ToList());
            List<string> usedRegressors = new List<string>();

            // Find groups of correlated regressors
            for (int i = 0; i < nonCombinedRegressors.Count; i++) {
                if (!usedRegressors.Contains(nonCombinedRegressors[i])) {
                    List<string> corrRegressorsWithMain = new List<string>();
                    corrRegressorsWithMain.Add(nonCombinedRegressors[i]);
                    usedRegressors.Add(nonCombinedRegressors[i]);

                    // Find regressors that correlate with the main regressor
                    for (int j = i + 1; j < nonCombinedRegressors.Count; j++) {
                        if (Math.Abs(Statistics.PearsonCorrelationCoefficient(regressors[nonCombinedRegressors[i]],
                            regressors[nonCombinedRegressors[j]])) > thresholdCorr) {

                            usedRegressors.Add(nonCombinedRegressors[j]);
                            corrRegressorsWithMain.Add(nonCombinedRegressors[j]);
                        }
                    }
                    corrRegressors.Add(corrRegressorsWithMain);
                }
            }
            return corrRegressors;
        }

        /// <summary>
        /// Add pairwise combinations to every formed combination
        /// </summary>
        /// <param name="factorsCombinations">Formed combinations</param>
        public void AddPairwiseCombinationsOfFactors(List<List<string>> factorsCombinations) {
            // Get number of factors without pairwise combinations
            int startFactorsNumber = factorsCombinations[0].Count;

            for (int combNum = 0; combNum < factorsCombinations.Count; combNum++) {
                for (int i = 0; i < startFactorsNumber - 1; i++) {
                    for (int j = i + 1; j < startFactorsNumber; j++) {
                        factorsCombinations[combNum].Add(factorsCombinations[combNum][i] + " & "
                            + factorsCombinations[combNum][j]);
                    }
                }
            }
        }

        /// <summary>
        /// Create all possible models for each regressant
        /// </summary>
        /// <param name="combinationsOfRegressors">Combinations of regressors with values</param>
        /// <param name="models">List of base models</param>
        /// <returns>All possible models for each regressant</returns>
        public Dictionary<string, List<Model>> CreateGroupsOfModels(List<List<string>> combinationsOfRegressors,
                                                                     List<Model> models) {

            Dictionary<string, List<Model>> groupsOfModels = new Dictionary<string, List<Model>>();

            // For each model create variations with model with each combination of regressors
            foreach (var model in models) {
                List<Model> variationOfRegressorsForRegressant = new List<Model>();

                foreach (var combination in combinationsOfRegressors) {
                    Model nextModel = new Model(model);
                    nextModel.SetNewRegressors(GetRegressorsWithValues(model, combination));
                    variationOfRegressorsForRegressant.Add(nextModel);
                }
                groupsOfModels[model.RegressantName] = variationOfRegressorsForRegressant;
            }

            return groupsOfModels;
        }

        /// <summary>
        /// Get list of regressors from model with values
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="regressors">List of regressors</param>
        /// <returns>Regressors with values</returns>
        private Dictionary<string, List<double>> GetRegressorsWithValues(Model model, List<string> regressors) {
            Dictionary<string, List<double>> regressorsWithValues = new Dictionary<string, List<double>>();

            foreach (var regressor in regressors) {

                // Check if regressor name in models regressors else we switch parts of combine regressor
                if (model.Regressors.ContainsKey(regressor)) {
                    regressorsWithValues[regressor] = new List<double>(model.Regressors[regressor]);
                }
                else if (regressor.Contains(" & ")) {
                    string regressorName = OperationsWithModels.SwapPartsOfCombinedFactor(regressor);
                    regressorsWithValues[regressorName] = new List<double>(model.Regressors[regressorName]);
                }
            }

            return regressorsWithValues;
        }
    }
}
