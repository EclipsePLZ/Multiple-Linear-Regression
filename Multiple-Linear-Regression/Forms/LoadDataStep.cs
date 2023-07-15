using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiple_Linear_Regression.Forms {
    public class LoadDataStep {

        /// <summary>
        /// Get factors with it's values
        /// </summary>
        /// <param name="factors">Dictionary with name of factor and it's index</param>
        /// <param name="values">Matrix with values of factors</param>
        /// <returns>Dictionary with values for each factor</returns>
        public Dictionary<string, List<double>> GetFactorsWithValues(Dictionary<string, int> factors, List<List<double>> values) {
            Dictionary<string, List<double>> factorValues = new Dictionary<string, List<double>>();

            // Add values for each factor
            foreach (var factor in factors) {
                List<double> valuesForFactor = new List<double>();

                // Collect all values for current factor
                for (int row = 0; row < values.Count; row++) {
                    valuesForFactor.Add(values[row][factor.Value]);
                }
                factorValues[factor.Key] = valuesForFactor;
            }

            return factorValues;
        }

        /// <summary>
        /// Create pairwise combinations of factors as new factors and add them to dictionary with regressors
        /// </summary>
        /// <param name="fullRegressors">Dictionary with regressors names and values</param>
        /// <param name="regressorsShortNames">Dictionary with regressors fullnames and shortnames</param>
        public void CreatePairwiseCombinationsOfFactors(Dictionary<string, List<double>> fullRegressors,
                                                         Dictionary<string, string> regressorsShortNames) {

            List<string> regressorsKeys = fullRegressors.Keys.ToList();

            // Create new factor as pairwise combination of factors
            for (int i = 0; i < regressorsKeys.Count - 1; i++) {
                for (int j = i + 1; j < regressorsKeys.Count; j++) {
                    List<double> newRegressorFactorValues = new List<double>();

                    // The value of the new factor is obtained by multiplying the values of the two factors
                    for (int elemNum = 0; elemNum < fullRegressors[regressorsKeys[i]].Count; elemNum++) {
                        newRegressorFactorValues.Add(fullRegressors[regressorsKeys[i]][elemNum] 
                                                     * fullRegressors[regressorsKeys[j]][elemNum]);
                    }
                    string combinedName = regressorsKeys[i] + " & " + regressorsKeys[j];
                    regressorsShortNames[combinedName] = $"{regressorsShortNames[regressorsKeys[i]]}*{regressorsShortNames[regressorsKeys[j]]}";
                    fullRegressors[combinedName] = newRegressorFactorValues;
                }
            }
        }

        /// <summary>
        /// A method for performing a shift in factor values from a dictionary.
        /// </summary>
        /// <param name="factors">Dictionary of factors</param>
        /// <param name="startPosition">Start position for shifting</param>
        /// <param name="numberOfValues">Number of values for shift</param>
        public void ShiftFactorValues(Dictionary<string, List<double>> factors, int startPosition, int numberOfValues) {
            List<string> factorsNames = new List<string>(factors.Keys);

            foreach (var factorName in factorsNames) {
                factors[factorName] = factors[factorName].GetRange(startPosition, numberOfValues);
            }
        }
    }
}
