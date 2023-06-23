using Multiple_Linear_Regression.Mathematic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;

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
        /// Dictionary for not processing regressors data
        /// </summary>
        public Dictionary<string, List<double>> StartRegressors { get; private set; }

        /// <summary>
        /// Dictionary for not processing and not filter regressors data
        /// </summary>
        public Dictionary<string, List<double>> NonFilterStartRegressors { get; private set; }

        /// <summary>
        /// List of regressors names
        /// </summary>
        public List<string> RegressorsNames { get; private set; }

        /// <summary>
        /// Dictionary of regressors coefficients
        /// </summary>
        private Dictionary<string, double> RegressorsCoeffs { get; set; }

        /// <summary>
        /// List of errors for model
        /// </summary>
        private List<double> Errors { get; set; }

        /// <summary>
        /// List fo predicted values of regressant
        /// </summary>
        public List<double> PredictedValues { get; private set; }

        /// <summary>
        /// Dictionary of non-filter regressors
        /// </summary>
        public Dictionary<string, List<double>> NonFilterRegressors { get; set; }

        /// <summary>
        /// Dictionary of functions that were used in preprocessing data
        /// </summary>
        public Dictionary<string, List<string>> ProcessFunctions { get; private set; }

        /// <summary>
        /// Dictionary of non-filter process functions
        /// </summary>
        private Dictionary<string, List<string>> NonFilterProcessFunctions { get; set; }

        /// <summary>
        /// Dictionary that contains correlation coefficient for each regressor
        /// </summary>
        public Dictionary<string, double> CorrelationCoefficient { get; private set; }

        /// <summary>
        /// Equation for model
        /// </summary>
        public string Equation { get; private set; }

        /// <summary>
        /// Adjusted coefficient of determination
        /// </summary>
        public double AdjDetermCoeff { get; private set; }

        /// <summary>
        /// Coefficient of determination
        /// </summary>
        public double DetermCoef { get; private set; }

        /// <summary>
        /// Is the model adequate
        /// </summary>
        public bool IsAdequate { get; private set; }

        /// <summary>
        /// Check Wilcoxon creterion rules
        /// </summary>
        public bool WilcoxonCreterion { get; private set; }

        /// <summary>
        /// Check rules for coefficients of assymetry and excess
        /// </summary>
        public bool AsymmetryAndExcess { get; private set; }

        /// <summary>
        /// Check rules for normal distribution interval
        /// </summary>
        public bool NormalDistrInterval { get; private set; }

        /// <summary>
        /// Distance to Adequacy
        /// </summary>
        public double DistanceToAdequate { get; private set; }

        /// <summary>
        /// Distance to Significant
        /// </summary>
        public double DistanceToSignificat { get; private set; }

        /// <summary>
        /// Is the model significant
        /// </summary>
        public bool IsSignificant { get; private set; }

        /// <summary>
        /// The Gusev method was used for functional preprocessing
        /// </summary>
        public bool IsGusevMethod { get; private set; }

        /// <summary>
        /// The Okunev method was used for functional preprocessing
        /// </summary>
        public bool IsOkunevMethod { get; private set; }

        
        public Model(string regerssantName, List<double> regressantValues, Dictionary<string, List<double>> regressors = null) {
            RegressantName = regerssantName;
            RegressantValues = new List<double>(regressantValues);            
            ProcessFunctions = new Dictionary<string, List<string>>();
            CorrelationCoefficient = new Dictionary<string, double>();
            StartRegressors = null;
            NonFilterStartRegressors = null;
            NonFilterProcessFunctions = new Dictionary<string, List<string>>();
            IsAdequate = false;
            IsSignificant = false;
            IsGusevMethod = false;
            IsOkunevMethod = false;
            Errors = null;
            PredictedValues = null;

            if (regressors is null) {
                Regressors = null;
                NonFilterRegressors = null;
                RegressorsNames = null;
            }
            else {
                SetNewRegressors(regressors);
            }
        }

        public Model(Model refModel) {
            RegressantName = refModel.RegressantName;
            RegressantValues = new List<double>(refModel.RegressantValues);
            ProcessFunctions = new Dictionary<string, List<string>>(refModel.ProcessFunctions);
            CorrelationCoefficient = new Dictionary<string, double>(refModel.CorrelationCoefficient);
            StartRegressors = new Dictionary<string, List<double>>(refModel.StartRegressors);
            NonFilterStartRegressors = new Dictionary<string, List<double>>(refModel.NonFilterStartRegressors);
            NonFilterProcessFunctions = new Dictionary<string, List<string>>(refModel.NonFilterProcessFunctions);
            Regressors = new Dictionary<string, List<double>>(refModel.Regressors);
            NonFilterRegressors = new Dictionary<string, List<double>>(refModel.NonFilterRegressors);
            RegressorsNames = new List<string>(refModel.RegressorsNames);
            IsAdequate = refModel.IsAdequate;
            IsSignificant = refModel.IsSignificant;
            DistanceToAdequate = refModel.DistanceToAdequate;
            DistanceToSignificat = refModel.DistanceToSignificat;
            Errors = refModel.Errors;
            PredictedValues = refModel.PredictedValues;
            IsGusevMethod = refModel.IsGusevMethod;
            IsOkunevMethod = refModel.IsOkunevMethod;
        }

        /// <summary>
        /// Set new regressord dictionary
        /// </summary>
        /// <param name="regressors">Dictionary of regressors</param>
        public void SetNewRegressors(Dictionary<string, List<double>> regressors) {
            if (StartRegressors is null) {
                StartRegressors = new Dictionary<string, List<double>>(regressors);
                NonFilterStartRegressors = new Dictionary<string, List<double>>(regressors);
            }
            if (NonFilterRegressors is null) {
                NonFilterRegressors = new Dictionary<string, List<double>>(regressors);
            }
            Regressors = new Dictionary<string, List<double>>(regressors);
            RegressorsNames = new List<string>(Regressors.Keys);
            CalcNewCorrelationCoefficients();

            List <string> removedKeys = new List<string>(StartRegressors.Keys.Except(Regressors.Keys));
            UpdateStartRegressors(removedKeys);
            UpdateNonFilterStartRegressors(removedKeys);
            UpdateNonFilterRegressors(removedKeys);
            UpdateProcessFunctions(removedKeys);
            UpdateNonFilterProcessFunctions(removedKeys);
        }

        /// <summary>
        /// Remove unnecessary regressors
        /// </summary>
        /// <param name="keys">List of unnecessary regressors names</param>
        private void UpdateStartRegressors(List<string> keys) {
            foreach (var key in keys) {
                StartRegressors.Remove(key);
            }
        }

        /// <summary>
        /// Remove unnecessary regressors
        /// </summary>
        /// <param name="keys">List of unnecessary regressors names</param>
        private void UpdateNonFilterStartRegressors(List<string> keys) {
            foreach (var key in keys) {
                NonFilterStartRegressors.Remove(key);
            }
        }

        /// <summary>
        /// Remove unnecessary regressors
        /// </summary>
        /// <param name="keys">List of unnecessary regressors names</param>
        private void UpdateNonFilterRegressors(List<string> keys) {
            foreach (var key in keys) {
                NonFilterRegressors.Remove(key);
            }
        }

        /// <summary>
        /// Remove unnecessary functions
        /// </summary>
        /// <param name="keys">List of unnecessary regressors names</param>
        private void UpdateProcessFunctions(List<string> keys) {
            foreach (var key in keys) {
                ProcessFunctions.Remove(key);
            }
        }

        /// <summary>
        /// Remove unnecessary functions
        /// </summary>
        /// <param name="keys">List of unnecessary regressors names</param>
        private void UpdateNonFilterProcessFunctions(List<string> keys) {
            foreach (var key in keys) {
                NonFilterProcessFunctions.Remove(key);
            }
        }

        /// <summary>
        /// Remove regressor by name
        /// </summary>
        /// <param name="regressorName">Regressor's name</param>
        private void RemoveRegressor(string regressorName) {
            StartRegressors.Remove(regressorName);
            ProcessFunctions.Remove(regressorName);
            Regressors.Remove(regressorName);
            RegressorsNames.Remove(regressorName);
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
        /// Functional data preprocessing by Gusev method
        /// </summary>
        public void StartGusevFunctionalPreprocessing() {
            IsGusevMethod = true;
            IsOkunevMethod = false;
            ProcessFunctions = new Dictionary<string, List<string>>();
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
                    foreach (var func in FunctionPreprocess.PreprocessingFunctionsGusev) {
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
                        Regressors[regressor] = FunctionPreprocess.PreprocessingFunctionsGusev[maxFuncName](Regressors[regressor]);
                    }
                    else {
                        ProcessFunctions.Add(regressor, functions);
                        CorrelationCoefficient[regressor] = startCorrCoef;
                        break;
                    }
                }
            }
            NonFilterProcessFunctions = new Dictionary<string, List<string>>(ProcessFunctions);
            NonFilterRegressors = new Dictionary<string, List<double>>(Regressors);
        }

        /// <summary>
        /// Functional data preprocessing by Okunev method
        /// </summary>
        public void StartOkunevFunctionalPreprocessing() {
            IsGusevMethod = false;
            IsOkunevMethod = true;
            ProcessFunctions = new Dictionary<string, List<string>>();
            foreach (var regressor in RegressorsNames) {
                List<string> functions = new List<string>();
                double startCorrCoef = Statistics.PearsonCorrelationCoefficient(RegressantValues,
                    Statistics.ConvertValuesToInterval(-1.0, 1.0, Regressors[regressor]));
                double maxFuncCorrCoeff = 0;
                string maxFuncName = "";

                while (true) {
                    // Convert values to interval [-1, 1]
                    Regressors[regressor] = Statistics.ConvertValuesToInterval(-1.0, 1.0, Regressors[regressor]);

                    // For each function we find the correlation coefficient with the regressant
                    foreach (var func in FunctionPreprocess.PreprocessingFunctionsOkunev) {
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
                        Regressors[regressor] = FunctionPreprocess.PreprocessingFunctionsOkunev[maxFuncName](Regressors[regressor]);
                    }
                    else {
                        ProcessFunctions.Add(regressor, functions);
                        CorrelationCoefficient[regressor] = startCorrCoef;
                        break;
                    }
                }
            }
            NonFilterProcessFunctions = new Dictionary<string, List<string>>(ProcessFunctions);
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
            if (NonFilterProcessFunctions is null && ProcessFunctions.Count > 0) {
                NonFilterProcessFunctions = new Dictionary<string, List<string>>(ProcessFunctions);
            }
        }

        /// <summary>
        /// Restoring the regressors to their pre-filter state
        /// </summary>
        public void RestoreNonFilterRegressors() {
            if (NonFilterStartRegressors != null){
                StartRegressors = new Dictionary<string, List<double>>(NonFilterStartRegressors);
            }
            if (NonFilterProcessFunctions != null) {
                ProcessFunctions = new Dictionary<string, List<string>>(NonFilterProcessFunctions);
            }
            SetNewRegressors(NonFilterRegressors);
        }

        /// <summary>
        /// Make an equation for expressing regressants through regressors
        /// </summary>
        /// <param name="shortNames">Dictionary with short names of regressors</param>
        public void BuildEquation(Dictionary<string, string> shortNames) {
            RegressorsCoeffs = new Dictionary<string, double>();
            int numberOfValues = RegressantValues.Count;
            int numberOfCoeffs = Regressors.Count + 1;

            // Fill arrays Z and Y
            double[,] Z = new double[numberOfValues, numberOfCoeffs];
            double[] Y = RegressantValues.ToArray();

            for (int i = 0; i < numberOfValues; i++) { 
                for (int j = 0; j < numberOfCoeffs; j++) {
                    if (j == 0) {
                        Z[i, j] = 1.0;
                    }
                    else {
                        Z[i, j] = Regressors[RegressorsNames[j - 1]][i];
                    }
                }
            }

            // Find vector of coeffs for regression equation
            double[,] transposedZ = Algebra.Transpose(Z);
            double[] coeffs = Algebra.Mult(Algebra.Mult(Algebra.Inverse(Algebra.Mult(transposedZ, Z)), transposedZ), Y);

            // Fill coeff for each regressor in dictionary
            RegressorsCoeffs.Add("", coeffs[0]);
            for (int i = 1; i < coeffs.Length; i++) {
                RegressorsCoeffs.Add(RegressorsNames[i - 1], coeffs[i]);
            }

            // Find adjusted coefficient of determination
            AdjDetermCoeff = Statistics.AdjustedDetermCoefficient(Y, Algebra.Mult(Z, coeffs), Regressors.Count);

            // Find predicted values
            PredictedValues = Algebra.Mult(Z, coeffs).ToList();

            // Find coefficient of determination
            DetermCoef = Statistics.DetermCoefficient(Y, PredictedValues);

            // Find a string representation of the equation
            GetEquation(shortNames);

            // Find errors for model
            Errors = Algebra.Substract(Y, PredictedValues.ToArray()).ToList();

            // A test of adequacy
            CheckAdequate();

            // A test of significance
            CheckSignificant();
        }

        /// <summary>
        /// Get a string representation of the equation
        /// </summary>
        /// <param name="shortNames">Dictionary with short names of regressors</param>
        private void GetEquation(Dictionary<string, string> shortNames) {
            Equation = "Y = " + Math.Round(RegressorsCoeffs[RegressorsCoeffs.Keys.ToList()[0]], 4).ToString();

            for (int i = 1; i < RegressorsCoeffs.Count; i++) {
                string regressorName = RegressorsNames[i - 1];
                string regressorShortName = "";

                if (shortNames.ContainsKey(regressorName)) {
                    regressorShortName = shortNames[regressorName];
                }
                else {
                    string[] combinedRegressors = regressorName.Split(new string[] { " & " }, StringSplitOptions.None);
                    regressorShortName = shortNames[combinedRegressors[1] + " & " + combinedRegressors[0]];
                }

                if (RegressorsCoeffs[RegressorsNames[i - 1]] < 0) {
                    Equation += " - " + Math.Abs(Math.Round(RegressorsCoeffs[RegressorsNames[i - 1]], 4)).ToString()
                        + $"*{regressorShortName}";
                    continue;
                }
                else {
                    Equation += " + " + Math.Round(RegressorsCoeffs[RegressorsNames[i - 1]], 4).ToString() + 
                        $"*{regressorShortName}";
                }
            }
        }

        /// <summary>
        /// Get model prediction
        /// </summary>
        /// <param name="x">Values of regressors</param>
        /// <returns>Predicted value</returns>
        public double Predict(double[] x) {
            double[] functionX;
            if (ProcessFunctions.Count > 0) {
                functionX = ProcessValues(x);
            }
            else {
                functionX = x;
            }
            double[] xWithFreeCoeff = new double[functionX.Length + 1];
            functionX.CopyTo(xWithFreeCoeff, 1);
            xWithFreeCoeff[0] = 1;

            return Algebra.Mult(xWithFreeCoeff, RegressorsCoeffs.Values.ToArray());
        }

        /// <summary>
        /// Apply the functions to the new values of the regressors
        /// </summary>
        /// <param name="x">New regressors values</param>
        /// <returns>Processed regressors values</returns>
        private double[] ProcessValues(double[] x) {
            if (IsGusevMethod) {
                return ProcessValuesByGusev(x);
            }

            if (IsOkunevMethod) {
                return ProcessValuesByOkunev(x);
            }

            // Exceptional situation
            return null;
        }

        /// <summary>
        /// Apply the functions by Gusev method to the new values of the regressors
        /// </summary>
        /// <param name="x">New regressors values</param>
        /// <returns>Processed regressors values</returns>
        private double[] ProcessValuesByGusev(double[] x) {
            double[] processX = new double[x.Length];

            for (int i = 0; i < ProcessFunctions.Count; i++) {
                List<double> nextValue = new List<double>(StartRegressors[RegressorsNames[i]]);
                nextValue.Add(x[i]);
                foreach (var funcName in ProcessFunctions[RegressorsNames[i]]) {
                    nextValue = Statistics.ConvertValuesToInterval(2, 102, nextValue);
                    nextValue = FunctionPreprocess.PreprocessingFunctionsGusev[funcName](nextValue);
                }
                nextValue = Statistics.ConvertValuesToInterval(2, 102, nextValue);
                processX[i] = nextValue.Last();
            }

            return processX;
        }

        /// <summary>
        /// Apply the functions by Okunev method to the new values of the regressors
        /// </summary>
        /// <param name="x">New regressors values</param>
        /// <returns>Processed regressors values</returns>
        private double[] ProcessValuesByOkunev(double[] x) {
            double[] processX = new double[x.Length];

            for (int i = 0; i < ProcessFunctions.Count; i++) {
                List<double> nextValue = new List<double>(StartRegressors[RegressorsNames[i]]);
                nextValue.Add(x[i]);
                foreach (var funcName in ProcessFunctions[RegressorsNames[i]]) {
                    nextValue = Statistics.ConvertValuesToInterval(-1.0, 1.0, nextValue);
                    nextValue = FunctionPreprocess.PreprocessingFunctionsOkunev[funcName](nextValue);
                }
                nextValue = Statistics.ConvertValuesToInterval(-1.0, 1.0, nextValue);
                processX[i] = nextValue.Last();
            }

            return processX;
        }

        /// <summary>
        /// Check whether the model is adequate
        /// </summary>
        private void CheckAdequate() {
            DistanceToAdequate = 0;
            WilcoxonCreterion = CheckWilcoxonCriterion();
            AsymmetryAndExcess = CheckAsymmetryAndExcess();
            NormalDistrInterval = CheckIntervalOfNormalDistribution();
            IsAdequate = WilcoxonCreterion && AsymmetryAndExcess && NormalDistrInterval ;
        }

        /// <summary>
        /// Check Wilcoxon creterion for model
        /// </summary>
        /// <returns>Result of Wilcoxon creterion</returns>
        private bool CheckWilcoxonCriterion() {
            double[] HnZ = new double[Errors.Count];
            double[] HnZmin = new double[Errors.Count];
            double sum = 0;
            double[] e = Errors.ToArray();
            Array.Sort(e);

            for (int i = 0; i < Errors.Count; i++) {
                int j = 0;
                while (e[j] <= e[i] && j < e.Length - 1) {
                    j++;
                }
                int kol = j;

                j = 0;
                while (e[j] <= -e[i] && j < e.Length - 1) {
                    j++;
                }
                int kolMin = j;

                HnZ[i] = Convert.ToDouble(kol) / Convert.ToDouble(Errors.Count);
                HnZmin[i] = Convert.ToDouble(kolMin) / Convert.ToDouble(Errors.Count);
            }

            for (int i = 0; i < Errors.Count; i++) {
                sum += Math.Pow((HnZ[i] + HnZmin[i] - 1), 2);
            }

            // If the Wilcoxon criterion is not fulfilled, then we find the distance to the fulfillment of the criterion.
            if (sum < 1.2 || sum > 2.8) {
                FindDistanceToWilcoxon(sum);
            }

            return sum <= 2.8 && sum >= 1.2;
        }

        /// <summary>
        /// Find distance to Wilcoxon creterion
        /// </summary>
        /// <param name="sum">Sum for Wilcoxon creterion</param>
        private void FindDistanceToWilcoxon(double sum) {
            if (sum < 1.2) {
                DistanceToAdequate += (1.2 - sum) / 1.2;
            }
            if (sum > 2.8) {
                DistanceToAdequate += (sum - 2.8) / 2.8;
            }
        }

        /// <summary>
        /// Check coefficients of asymmetry and excess
        /// </summary>
        /// <returns>Result of checking the asymmetry and excess requirements</returns>
        private bool CheckAsymmetryAndExcess() {
            double moduleAsymmetryCoeff = Math.Abs(Statistics.AsymmetryCoefficient(Errors));
            double moduleExcessCoeff = Math.Abs(Statistics.ExcessCoefficient(Errors));

            // If the coefficient conditions are not met, then find the distance to meet them
            if (moduleAsymmetryCoeff > 1) {
                DistanceToAdequate += moduleAsymmetryCoeff - 1;
            }
            if (moduleExcessCoeff > 1) {
                DistanceToAdequate += (moduleExcessCoeff - 1);
            }

            return moduleAsymmetryCoeff <= 1 && moduleExcessCoeff <= 1;
        }

        /// <summary>
        /// Check that 99.73% of the observations are in the range of mean plus/minus three standard deviations
        /// </summary>
        /// <returns>Check interval for normal distribution</returns>
        private bool CheckIntervalOfNormalDistribution() {
            double errorsAvg = Errors.Average();
            double errorsStd = Statistics.StandardDeviation(Errors);

            double min = errorsAvg - 3 * errorsStd;
            double max = errorsAvg + 3 * errorsStd;


            int maxOutsideOfIntervalStart = (int)Math.Round(Errors.Count * 0.0027);
            int maxOutsideOfInterval = maxOutsideOfIntervalStart;

            foreach(var error in Errors) {
                if (error <= min || error >= max) {
                    maxOutsideOfInterval--;
                }
            }

            // If the number of values outside the interval exceeds the normal distribution, we calculate the distance to adequacy
            if (maxOutsideOfInterval < 0) {
                DistanceToAdequate += (double)Math.Abs(maxOutsideOfInterval) / (double)maxOutsideOfIntervalStart;
            }

            return maxOutsideOfInterval >= 0;
        }

        /// <summary>
        /// Check whether the model is significant
        /// </summary>
        private void CheckSignificant() {
            double f1 = Regressors.Count;
            double f2 = RegressantValues.Count - f1 - 1;

            Chart chart1 = new Chart();
            double calcFStat = (DetermCoef / (1 - DetermCoef)) * (f2 / f1);
            double theorFStat = chart1.DataManipulator.Statistics.InverseFDistribution(0.05, (int)f2, (int)f1);

            // If the condition of model significance is not satisfied, we calculate the distance to significance
            if (calcFStat < theorFStat) {
                DistanceToSignificat = (theorFStat - calcFStat) / theorFStat;
            }

            IsSignificant = calcFStat > theorFStat;
        }
    }
}
