using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Multiple_Linear_Regression {
    public static class OperationsWithModels {

        /// <summary>
        /// Set regressants and regressors for each model
        /// </summary>
        /// <param name="models">List of models</param>
        /// <param name="regressants">Dictionary with regressants</param>
        /// <param name="regressors">Dictionary with regressors</param>
        public static void SetFactorForModels(List<Model> models, Dictionary<string, List<double>> regressants,
                                               Dictionary<string, List<double>> regressors) {

            foreach (var regressant in regressants) {
                models.Add(new Model(regressant.Key, regressant.Value, regressors));
            }
        }

        /// <summary>
        /// Swap the factors that make up the combination of factors.
        /// </summary>
        /// <param name="factorName">Name of combination</param>
        /// <returns>New combination name</returns>
        public static string SwapPartsOfCombinedFactor(string factorName) {
            string[] combinedFactors = factorName.Split(new string[] { " & " }, StringSplitOptions.None);
            return combinedFactors[1] + " & " + combinedFactors[0];
        }

        /// <summary>
        /// Calculate the predicted value for regressant of the model
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="regressors">Dictionary of regressors with values</param>
        /// <returns>Predicted value</returns>
        public static double CalcModelValue(Model model, Dictionary<string, double> regressors) {
            // Fill new X values for model
            double[] xValues = new double[model.Regressors.Count];
            int position = 0;

            foreach (var regressor in model.Regressors) {
                xValues[position] = regressors[regressor.Key];
                position++;
            }

            // Get predicted value for regressant
            return model.Predict(xValues);
        }

        /// <summary>
        /// Get all regressors names from models
        /// </summary>
        /// <param name="models">List of models</param>
        /// <returns>List of names of all regressors</returns>
        public static List<string> GetAllRegressorsFromModels(List<Model> models) {
            List<string> allRegressorsNames = new List<string>();

            // Get all names of regressors
            foreach (var model in models) {
                allRegressorsNames = allRegressorsNames.Union(model.RegressorsNames).ToList();
            }

            return allRegressorsNames;
        }

        /// <summary>
        /// Get list of regressors in short form from model
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="regressorsShortNames">Dictionary with regressors short names</param>
        /// <returns>List of short-form regressors</returns>
        public static List<string> GetRegressorsShortNamesFromModel(Model model, 
                                                                    Dictionary<string, string> regressorsShortNames) {
            List<string> shortRegressors = new List<string>();

            // For each regressor in model we get its short form
            foreach (var regressor in model.RegressorsNames) {
                string nextRegressor = "";
                if (regressorsShortNames.ContainsKey(regressor)) {
                    nextRegressor = regressorsShortNames[regressor];
                }
                else if (regressor.Contains(" & ")) {
                    nextRegressor = OperationsWithModels.SwapPartsOfCombinedFactor(regressor);
                }
                shortRegressors.Add(nextRegressor);
            }

            return shortRegressors;
        }

        /// <summary>
        /// Get all start regressors names from models
        /// </summary>
        /// <param name="models">List of models</param>
        /// <returns>List of names of all start regressors</returns>
        public static List<string> GetStartRegressorsFromModels(List<Model> models) {
            List<string> startRegressorsFromModels = new List<string>();

            // Get all names of start regressors
            foreach (var model in models) {
                startRegressorsFromModels = startRegressorsFromModels.Union(model.StartRegressors.Keys).ToList();
            }

            return startRegressorsFromModels;
        }

        /// <summary>
        /// Get a list of models that contain a given regressorKey
        /// </summary>
        /// <param name="regressorName">Name of given regressorKey</param>
        /// <returns>List of models</returns>
        public static List<Model> GetModelsByRegressors(string regressorName, List<Model> models) {
            List<Model> modifiedModels = new List<Model>();

            foreach (var model in models) {
                if (model.Regressors.Keys.Contains(regressorName)) {
                    modifiedModels.Add(model);
                }
            }

            return modifiedModels;
        }

        /// <summary>
        /// Get list of headers of non-combined regressors
        /// </summary>
        /// <param name="regressors">Regressors</param>
        /// <returns>List of headers</returns>
        public static List<string> GetNonCombinedRegressors(List<string> regressors) {
            List<string> nonCombinedRegressors = new List<string>();
            foreach (var regressorName in regressors) {
                if (!regressorName.Contains(" & ")) {
                    nonCombinedRegressors.Add(regressorName);
                }
            }
            return nonCombinedRegressors;
        }

        /// <summary>
        /// Find the best model among the built models for regressant
        /// </summary>
        /// <param name="models">List of models</param>
        /// <param name="regressant">Regressant name</param>
        /// <returns>Best model for regressant</returns>
        public static Model FindBestModel(List<Model> models, string regressant) {
            // Check if there are significant models
            List<Model> significantModels = GetSignificantModels(models);
            if (significantModels.Count > 0) {
                return FindAdequateModelInSignificantModels(significantModels, regressant);
            }

            // If there are no significant models, we will look for adequate models that are closest to the significance
            return FindMostAdequateAndSignificantModel(models, regressant);
        }

        public static List<List<string>> GetErrorsOfModelsForPrediction(List<Model> models,
                                         Dictionary<string, List<double>> regressorsValues,
                                         Dictionary<string, Dictionary<string, List<double>>> modelsPredictionErrors) {

            List<List<string>> allRows = new List<List<string>>();
            int numOfValues = regressorsValues[regressorsValues.Keys.First()].Count;

            foreach (var model in models) {
                modelsPredictionErrors[model.RegressantName] = new Dictionary<string, List<double>>();
                modelsPredictionErrors[model.RegressantName]["MAPE"] = new List<double>();
                modelsPredictionErrors[model.RegressantName]["MinMax"] = new List<double>();
                modelsPredictionErrors[model.RegressantName]["AbsError"] = new List<double>();
                modelsPredictionErrors[model.RegressantName]["MaxError"] = new List<double>();
                modelsPredictionErrors[model.RegressantName]["MinError"] = new List<double>();
            }

            // Find min and max in all models
            Dictionary<string, (double, double)> minMaxModel = GetMinMaxValuesForRegressants(models);

            // Fill real/predict values for all observations
            for (int i = 0; i < numOfValues; i++) {
                List<string> nextRow = new List<string>();

                // Add regressors values to row
                foreach (var regressor in regressorsValues) {
                    nextRow.Add(Math.Round(regressor.Value[i], 2).ToString());
                }

                // Add regressant values to row
                foreach (var model in models) {
                    nextRow.Add(Math.Round(model.RegressantValues[i], 2).ToString());
                    nextRow.Add(Math.Round(model.PredictedValues[i], 2).ToString());

                    // Calc all errors for regressant
                    double mapeError = Statistics.PredictError(model.RegressantValues[i], model.PredictedValues[i]);
                    double minMaxError = Statistics.RangeError(model.RegressantValues[i], model.PredictedValues[i],
                        minMaxModel[model.RegressantName].Item1, minMaxModel[model.RegressantName].Item2);
                    double absoluteError = Statistics.AbsoluteError(model.RegressantValues[i], model.PredictedValues[i]);
                    double maxPercentError = Statistics.MaxPercentError(model.RegressantValues[i], model.PredictedValues[i],
                        minMaxModel[model.RegressantName].Item2);
                    double minPercentError = Statistics.MinPercentError(model.RegressantValues[i], model.PredictedValues[i],
                        minMaxModel[model.RegressantName].Item1);

                    // Add all error to next row
                    nextRow.Add(Math.Round(mapeError, 2).ToString());
                    nextRow.Add(Math.Round(minMaxError, 2).ToString());
                    nextRow.Add(Math.Round(absoluteError, 2).ToString());
                    nextRow.Add(Math.Round(maxPercentError, 2).ToString());
                    nextRow.Add(Math.Round(minPercentError, 2).ToString());

                    modelsPredictionErrors[model.RegressantName]["MAPE"].Add(mapeError);
                    modelsPredictionErrors[model.RegressantName]["MinMax"].Add(minMaxError);
                    modelsPredictionErrors[model.RegressantName]["AbsError"].Add(absoluteError);
                    modelsPredictionErrors[model.RegressantName]["MaxError"].Add(maxPercentError);
                    modelsPredictionErrors[model.RegressantName]["MinError"].Add(minPercentError);
                }

                allRows.Add(nextRow);
            }

            return allRows;
        }

        /// <summary>
        /// Get min and max values for each regressant in models
        /// </summary>
        /// <param name="models">List of models</param>
        /// <returns>Dicitonary with min and max values</returns>
        private static Dictionary<string, (double, double)> GetMinMaxValuesForRegressants(List<Model> models) {
            Dictionary<string, (double, double)> minMaxModel = new Dictionary<string, (double, double)>();

            // Find min and max values of regressant for each model
            foreach (var model in models) {
                minMaxModel[model.RegressantName] = (model.RegressantValues.Min(), model.RegressantValues.Max());
            }

            return minMaxModel;
        }

        /// <summary>
        /// Get only significant models from list of models
        /// </summary>
        /// <param name="listModels">List of models</param>
        /// <returns>List of significant models</returns>
        public static List<Model> GetSignificantModels(List<Model> listModels) {
            List<Model> significantsModels = new List<Model>();

            // Checking the significance of the models
            foreach (var model in listModels) {
                if (model.IsSignificant) {
                    significantsModels.Add(model);
                }
            }

            return significantsModels;
        }

        /// <summary>
        /// Find most adequacy model in significance models
        /// </summary>
        /// <param name="significantModels">List of significance models</param>
        /// <param name="regressant">Name of regressant</param>
        /// <returns>Most adequacy model</returns>
        public static Model FindAdequateModelInSignificantModels(List<Model> significantModels, string regressant) {
            List<Model> wilcoxonModels;
            List<Model> asymmExcessModels;
            List<Model> intervalModels;

            // If there are significant and adequate models, we derive the model with the highest coefficient of determination
            List<Model> fullyAdequateModels = GetFullyAdequateModels(significantModels);

            if (fullyAdequateModels.Count > 0) {
                MessageBox.Show($"Для показателя ({regressant}) найдена значимая и адекватная модель");
                return GetModelWithBestDetermCoeff(fullyAdequateModels);
            }

            string message = $"Для показателя ({regressant}) найдена значимая модель, но не найдена адекватная модель." +
                        $" Выведена самая близкая к адекватности модель";
            // If there are models for which the Wilcoxon criterion is satisfied
            wilcoxonModels = GetModelWithWilcoxonCriterion(significantModels);
            if (wilcoxonModels.Count > 0) {
                // If there are models for which the Wilcoxon criterion and asymmetry excess is satisfied
                asymmExcessModels = GetModelWithAsymmetryExcess(wilcoxonModels);

                if (asymmExcessModels.Count > 0) {
                    MessageBox.Show(message);
                    return GetModelWithBestDetermCoeff(asymmExcessModels);
                }


                // If there are models for which the Wilcoxon criterion normal interval is satisfied
                intervalModels = GetModelWithIntervalOfNormalDistribution(wilcoxonModels);

                if (intervalModels.Count > 0) {
                    MessageBox.Show(message);
                    return GetModelWithBestDetermCoeff(intervalModels);
                }

                // If there are no models for which the two adequacy conditions (first is Wilcoxon) are met,
                // we return the model with the highest correlation coefficient
                MessageBox.Show(message);
                return GetModelWithBestDetermCoeff(wilcoxonModels);
            }


            // If there are models for which the coefficients of asymmetry and excess is satisfied
            asymmExcessModels = GetModelWithAsymmetryExcess(significantModels);
            if (asymmExcessModels.Count > 0) {

                // If there are models for which the coefficients of asymmetry and excess and normal interval is satisfied
                intervalModels = GetModelWithIntervalOfNormalDistribution(asymmExcessModels);
                if (intervalModels.Count > 0) {
                    MessageBox.Show(message);
                    return GetModelWithBestDetermCoeff(intervalModels);
                }

                // If there are no models for which the two adequacy conditions (asymmetry and normal interval)
                // are met, we return the model with the highest correlation coefficient
                MessageBox.Show(message);
                return GetModelWithBestDetermCoeff(asymmExcessModels);
            }

            // If there are models for which only the normal distribution interval is satisfied
            intervalModels = GetModelWithIntervalOfNormalDistribution(significantModels);
            if (intervalModels.Count > 0) {
                MessageBox.Show(message);
                return GetModelWithBestDetermCoeff(intervalModels);
            }

            // If there are no models for which at least one condition for adequacy is met,
            // then we find the closest to adequacy model
            MessageBox.Show(message);
            return GetClosestModelToAdequacy(significantModels);
        }

        /// <summary>
        /// Find closest model to adequacy and significance
        /// </summary>
        /// <param name="listOfModels">List of models</param>
        /// <param name="regressant">Name of regressant</param>
        /// <returns>Closest model to significance and adequacy</returns>
        public static Model FindMostAdequateAndSignificantModel(List<Model> listOfModels, string regressant) {
            List<Model> fullyAdequateModels;
            List<Model> wilcoxonModels;
            List<Model> asymmExcessModels;
            List<Model> intervalModels;

            fullyAdequateModels = GetFullyAdequateModels(listOfModels);
            if (fullyAdequateModels.Count > 0) {
                MessageBox.Show($"Для показателя ({regressant}) найдена адекватная модель, но не найдена значимая модель." +
                            $" Выведена самая близкая к значимости модель");
                return GetClosestModelToSignificance(fullyAdequateModels);
            }

            string message = $"Для показателя ({regressant}) не найдено ни значимой ни адекватной модели." +
                            $" Выведена самая близкая к значимости и адекватности модель";

            // Try find a model with wilcoxon criterion adequacy conditions
            wilcoxonModels = GetModelWithWilcoxonCriterion(listOfModels);
            if (wilcoxonModels.Count > 0) {
                // If there are models for which the Wilcoxon criterion and asymmetry excess is satisfied
                asymmExcessModels = GetModelWithAsymmetryExcess(wilcoxonModels);

                if (asymmExcessModels.Count > 0) {
                    MessageBox.Show(message);
                    return GetClosestModelToSignificance(asymmExcessModels);
                }


                // If there are models for which the Wilcoxon criterion normal interval is satisfied
                intervalModels = GetModelWithIntervalOfNormalDistribution(wilcoxonModels);

                if (intervalModels.Count > 0) {
                    MessageBox.Show(message);
                    return GetClosestModelToSignificance(intervalModels);
                }

                // If there are no models for which the two adequacy conditions (first is Wilcoxon) are met,
                // we return the model with the highest correlation coefficient
                MessageBox.Show(message);
                return GetClosestModelToSignificance(wilcoxonModels);
            }


            // Try find a model with asymmetry and excess criterions condition is satisfied
            asymmExcessModels = GetModelWithAsymmetryExcess(listOfModels);
            if (asymmExcessModels.Count > 0) {

                // If there are models for which the asymmetry and excess criterions and normal interval is satisfied
                intervalModels = GetModelWithIntervalOfNormalDistribution(asymmExcessModels);
                if (intervalModels.Count > 0) {
                    MessageBox.Show(message);
                    return GetClosestModelToSignificance(intervalModels);
                }

                MessageBox.Show(message);
                return GetClosestModelToSignificance(asymmExcessModels);
            }


            // Try find a model with normal distribution interval condition is satisfied
            intervalModels = GetModelWithIntervalOfNormalDistribution(listOfModels);
            if (intervalModels.Count > 0) {
                MessageBox.Show(message);
                return GetClosestModelToSignificance(intervalModels);
            }


            // If there are no significance and adequacy models then return the closest one
            MessageBox.Show(message);
            return GetClosestModelToSignificanceAndAdequacy(listOfModels);
        }

        /// <summary>
        /// Get only fully adequate models from list of models
        /// </summary>
        /// <param name="listModels">List of models</param>
        /// <returns>List of fully-adequate models</returns>
        private static List<Model> GetFullyAdequateModels(List<Model> listModels) {
            List<Model> adequateModels = new List<Model>();

            // Checking the fully adequate rules
            foreach (var model in listModels) {
                if (model.IsAdequate) {
                    adequateModels.Add(model);
                }
            }

            return adequateModels;
        }

        /// <summary>
        /// Get models from list of models for which the Wilcoxon criterion is satisfied
        /// </summary>
        /// <param name="listModels">List of models</param>
        /// <returns>List of models for which the Wilcoxon criterion is satisfied</returns>
        private static List<Model> GetModelWithWilcoxonCriterion(List<Model> listModels) {
            List<Model> wilcoxonModels = new List<Model>();

            // Checking the models for which the Wilcoxon criterion is satisfied
            foreach (var model in listModels) {
                if (model.WilcoxonCreterion) {
                    wilcoxonModels.Add(model);
                }
            }

            return wilcoxonModels;
        }

        /// <summary>
        /// Get models from list of models for which the asymmetry and excess coefficients are less than unity
        /// </summary>
        /// <param name="listModels">List of models</param>
        /// <returns>List of models for which the asymmetry and excess coefficients are less than unity</returns>
        private static List<Model> GetModelWithAsymmetryExcess(List<Model> listModels) {
            List<Model> asymExcesModels = new List<Model>();

            // Checking the models for which the asymmetry and excess coefficients are less than unity
            foreach (var model in listModels) {
                if (model.AsymmetryAndExcess) {
                    asymExcesModels.Add(model);
                }
            }

            return asymExcesModels;
        }

        /// <summary>
        /// Get models from list of models for which 99.73% of observations fall within 
        /// the interval plus/minus three standard deviations
        /// </summary>
        /// <param name="listModels">List of models</param>
        /// <returns>List of models for which 99.73% of observations fall within
        /// the interval plus/minus three standard deviations</returns>
        private static List<Model> GetModelWithIntervalOfNormalDistribution(List<Model> listModels) {
            List<Model> normDistrModels = new List<Model>();

            // Checking the models for which 99.73% of observations fall within
            // the interval plus/minus three standard deviations
            foreach (var model in listModels) {
                if (model.NormalDistrInterval) {
                    normDistrModels.Add(model);
                }
            }

            return normDistrModels;
        }

        /// <summary>
        /// Get best model from list of models by adjusted coefficient of determination
        /// </summary>
        /// <param name="models">List of models</param>
        /// <returns>Best model by adjusted coefficient of determination</returns>
        private static Model GetModelWithBestDetermCoeff(List<Model> models) {
            double bestCoeff = models[0].AdjDetermCoeff;
            Model bestModel = models[0];

            // Find the model with the best coefficient of determination
            for (int i = 1; i < models.Count; i++) {
                if (models[i].AdjDetermCoeff > bestCoeff) {
                    bestCoeff = models[i].AdjDetermCoeff;
                    bestModel = models[i];
                }
            }

            return bestModel;
        }

        /// <summary>
        /// Get model with minimum distance to adequacy
        /// </summary>
        /// <param name="models">List of models</param>
        /// <returns>Closest to adequacy model</returns>
        private static Model GetClosestModelToAdequacy(List<Model> models) {
            double minDist = models[0].DistanceToAdequate;
            Model bestModel = models[0];

            // Find the model with min distance to adequacy
            for (int i = 1; i < models.Count; i++) {
                if (models[i].DistanceToAdequate < minDist) {
                    minDist = models[i].DistanceToAdequate;
                    bestModel = models[i];
                }

                if (models[i].DistanceToAdequate == minDist) {
                    if (models[i].AdjDetermCoeff > bestModel.AdjDetermCoeff) {
                        bestModel = models[i];
                    }
                }
            }

            return bestModel;
        }

        /// <summary>
        /// Get model with minimum distance to significance
        /// </summary>
        /// <param name="models">List of models</param>
        /// <returns>Closest to significance model</returns>
        private static Model GetClosestModelToSignificance(List<Model> models) {
            double minDist = models[0].DistanceToSignificat;
            Model bestModel = models[0];

            // Find the model with min distance to significance
            for (int i = 1; i < models.Count; i++) {
                if (models[i].DistanceToSignificat < minDist) {
                    minDist = models[i].DistanceToSignificat;
                    bestModel = models[i];
                }

                if (models[i].DistanceToSignificat == minDist) {
                    if (models[i].AdjDetermCoeff > bestModel.AdjDetermCoeff) {
                        bestModel = models[i];
                    }
                }
            }

            return bestModel;
        }

        /// <summary>
        /// Get closest model to adequacy and significance
        /// </summary>
        /// <param name="models">List of models</param>
        /// <returns>Closest model to adequacy and significance</returns>
        private static Model GetClosestModelToSignificanceAndAdequacy(List<Model> models) {
            double minDist = models[0].DistanceToAdequate + models[0].DistanceToSignificat;
            Model bestModel = models[0];

            // Find the closest model to significance and adequacy
            for (int i = 1; i < models.Count; i++) {
                double modelDist = models[i].DistanceToSignificat + models[i].DistanceToAdequate;

                if (modelDist < minDist) {
                    minDist = modelDist;
                    bestModel = models[i];
                }

                if (modelDist == minDist) {
                    if (models[i].AdjDetermCoeff > bestModel.AdjDetermCoeff) {
                        bestModel = models[i];
                    }
                }
            }

            return bestModel;
        }
    }
}
