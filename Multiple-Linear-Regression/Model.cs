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
        /// Names of regressors
        /// </summary>
        public List<string> RegressorsNames { get; private set; }

        /// <summary>
        /// Values of regressant
        /// </summary>
        public List<double> RegressantValues { get; private set; }

        /// <summary>
        /// Dictionary of regressors
        /// </summary>
        public Dictionary<string, List<double>> Regressors { get; private set; }

        /// <summary>
        /// Dictionary of functions that were used in preprocessing data
        /// </summary>
        public Dictionary<string, List<string>> ProcessFunctions { get; private set; }

        /// <summary>
        /// Dictionary that contains correlation coefficient for each regressor
        /// </summary>
        public Dictionary<string, double> CorrelationCoefficient { get; private set; }

        public Model(string regerssantName, List<double> regressantValues, 
            List<string> regressorsNames = null, Dictionary<string, List<double>> regressors = null) {
            RegressantName = regerssantName;
            RegressantValues = regressantValues;
            RegressorsNames = regressorsNames;
            Regressors = regressors;
            ProcessFunctions = new Dictionary<string, List<string>>();
            CorrelationCoefficient = new Dictionary<string, double>();
        }

        /// <summary>
        /// Set new regressord dictionary
        /// </summary>
        /// <param name="regressors">Dictionary of regressors</param>
        public void SetNewRegressors(Dictionary<string, List<double>> regressors) {
            Regressors = new Dictionary<string, List<double>>(regressors);
        }


        /// <summary>
        /// Set new regressors names
        /// </summary>
        /// <param name="regressorsNames">List of regressors names</param>
        public void SetRegressorsNames(List<string> regressorsNames) {
            RegressorsNames = new List<string>(regressorsNames);
        }

        /// <summary>
        /// Functional data preprocessing
        /// </summary>
        public void StartFunctionalPreprocessing() {
            foreach(var regressor in RegressorsNames) {
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
                        CorrelationCoefficient.Add(regressor, startCorrCoef);
                        break;
                    }
                }
            }
        }
    }
}
