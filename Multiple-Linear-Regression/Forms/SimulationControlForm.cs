using Multiple_Linear_Regression.Work_WIth_Files.Interfaces;
using Multiple_Linear_Regression.Work_WIth_Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiple_Linear_Regression.Forms {
    public partial class SimulationControlForm : Form {
        private IFileService fileService;
        private IDialogService dialogService = new DefaultDialogService();

        private BackgroundWorker resizeWorker = new BackgroundWorker();

        private const int MODIFY_COLUMN = 1;

        private bool isResizeNeeded = false;

        private List<Model> Models { get; set; }

        private Dictionary<string, double> AllRegressors { get; set; }

        private Dictionary<string, double> StartRegressors { get; set; }

        private Dictionary<string, List<double>> SelectedStartRegressors { get; set; }

        private Dictionary<string, Dictionary<string, Dictionary<string, double>>> RegressorsImpact { get; set; }

        private Dictionary<string, Dictionary<string, double>> RegressorsCorrelation { get; set; }

        private Dictionary<string, (double, double)> RegressorsDefinitionArea { get; set; } = new Dictionary<string, (double, double)>();

        private Func<IEnumerable<double>, (double, double)> GetDefinitionArea { get; }

        private int NumberGroupOfCorrelatedRegressors { get; set; }

        public SimulationControlForm(List<Model> models, Func<IEnumerable<double>, (double, double)> getDefinitionAreaFunc,
            int numberGroupOfCorrRegressors) {
            Models = models;
            GetDefinitionArea = getDefinitionAreaFunc;
            NumberGroupOfCorrelatedRegressors = numberGroupOfCorrRegressors;

            InitializeComponent();

            // Centered Form on the screen
            this.CenterToScreen();

            SetStartParameters();

            // Run background worker for resizing components on form
            resizeWorker.DoWork += new DoWorkEventHandler(DoResizeComponents);
            resizeWorker.WorkerSupportsCancellation = true;
            resizeWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Setting start parameters for form
        /// </summary>
        private void SetStartParameters() {
            SetDataGVColumnHeaders(new List<string>() { "Название", "Значение", "Минимум", "Максимум" }, regressorsSetDataGrid,
                true, null, new List<int>() { 1 });

            SetDataGVColumnHeaders(new List<string>() { "Название", "Значение", "Уравнение" }, regressantsResultDataGrid,
                true);

            SelectedStartRegressors = new Dictionary<string, List<double>>();
            AllRegressors = new Dictionary<string, double>();

            // Fill last value for each regressorKey as default value
            foreach (var model in Models) {
                foreach(var regressor in model.StartRegressors) {
                    if (!AllRegressors.Keys.Contains(regressor.Key)) {
                        AllRegressors.Add(regressor.Key, regressor.Value.Last());

                        if (!regressor.Key.Contains(" & ")) {
                            SelectedStartRegressors.Add(regressor.Key, regressor.Value);
                        }

                        RegressorsDefinitionArea.Add(regressor.Key, GetDefinitionArea(regressor.Value));

                        regressorsSetDataGrid.Rows.Add(new string[] {regressor.Key, regressor.Value.Last().ToString(),
                            RegressorsDefinitionArea[regressor.Key].Item1.ToString(), 
                            RegressorsDefinitionArea[regressor.Key].Item2.ToString()});
                    }
                }
                // Calculate the regressants values
                regressantsResultDataGrid.Rows.Add(new string[] {model.RegressantName, CalcModelValue(model).ToString(), model.Equation});
            }

            helpImitationContorl.ToolTipText = StepsInfo.ImitationOfControlForm;
            loadDataFileMenu.ToolTipText = StepsInfo.ImitationRegressorControlOpenFile;

            StartRegressors = new Dictionary<string, double>(AllRegressors);

            if (NumberGroupOfCorrelatedRegressors == 0) {
                CalcNumberGroupOfCorrelatedRegressors();
            }

            GetRegressorsMutualImpact();
        }

        /// <summary>
        /// Calculate the predicted value for regressant of the model
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Predicted value</returns>
        private double CalcModelValue(Model model, Dictionary<string, double> regressors = null) {
            if (regressors is null) {
                regressors = new Dictionary<string, double>(AllRegressors);
            }
            // Fill new X values for model
            double[] xValues = new double[model.Regressors.Count];
            int position = 0;
            foreach(var regressor in model.Regressors) {
                xValues[position] = regressors[regressor.Key];
                position++;
            }

            // Get predicted value for regressant
            return model.Predict(xValues);
        }

        /// <summary>
        /// Automatically find the number of groups for correlated regressors 
        /// </summary>
        private void CalcNumberGroupOfCorrelatedRegressors() {
            NumberGroupOfCorrelatedRegressors = (int)(AllRegressors.Count / 5);

            if (NumberGroupOfCorrelatedRegressors < 3) {
                NumberGroupOfCorrelatedRegressors = 3;
            }
        }

        /// <summary>
        /// Get a coefficient for equation that represents regressors mutual impact
        /// </summary>
        private void GetRegressorsMutualImpact() {
            // Get correlation coefficients between regressors
            CalcCorrelationCoefficients();

            RegressorsImpact = new Dictionary<string, Dictionary<string, Dictionary<string, double>>>();

            // For each regressor, find the impact on the other regressors
            foreach (var mainRegressorName in SelectedStartRegressors.Keys) {
                Dictionary<string, Dictionary<string, double>> nextSecsRegressorsForMain =
                    new Dictionary<string, Dictionary<string, double>>();
                List<string> unUsedRegressors = new List<string>(SelectedStartRegressors.Keys);
                List<string> firstGroupRegressors = new List<string>();
                List<string> secondGroupRegressors = new List<string>();
                unUsedRegressors.Remove(mainRegressorName);

                // Find impact coefficient for regressos in first group
                firstGroupRegressors = RegressorsForImpact(unUsedRegressors, mainRegressorName, 0.7);
                if (firstGroupRegressors.Count > 0) {
                    FillFirstGroupRegressors(ref nextSecsRegressorsForMain, firstGroupRegressors, mainRegressorName);
                    unUsedRegressors = unUsedRegressors.Except(firstGroupRegressors).ToList();
                }


                // Find impact coefficient for regressors in second group
                secondGroupRegressors = RegressorsForImpact(unUsedRegressors, mainRegressorName, 0.3);

                if (secondGroupRegressors.Count > 0) {
                    if (firstGroupRegressors.Count == 0) {
                        FillFirstGroupRegressors(ref nextSecsRegressorsForMain, secondGroupRegressors, mainRegressorName);
                    }
                    else if (firstGroupRegressors.Count > 0) {
                        FillSecondGroupRegressors(ref nextSecsRegressorsForMain, secondGroupRegressors, firstGroupRegressors,
                            mainRegressorName);
                    }
                    unUsedRegressors = unUsedRegressors.Except(secondGroupRegressors).ToList();
                }



                // Find impact coefficient for regressors in third group
                if (unUsedRegressors.Count > 0) {
                    if (firstGroupRegressors.Count == 0 && secondGroupRegressors.Count == 0) {
                        FillFirstGroupRegressors(ref nextSecsRegressorsForMain, unUsedRegressors, mainRegressorName);
                    }
                    else if (firstGroupRegressors.Count == 0 && secondGroupRegressors.Count != 0) {
                        FillSecondGroupRegressors(ref nextSecsRegressorsForMain, unUsedRegressors, secondGroupRegressors,
                            mainRegressorName);
                    }
                    else if (firstGroupRegressors.Count != 0 && secondGroupRegressors.Count == 0) {
                        FillSecondGroupRegressors(ref nextSecsRegressorsForMain, unUsedRegressors, firstGroupRegressors,
                            mainRegressorName);
                    }
                    else {
                        FillThirdGroupRegressors(ref nextSecsRegressorsForMain, unUsedRegressors, firstGroupRegressors,
                            secondGroupRegressors, mainRegressorName);
                    }
                }

                RegressorsImpact[mainRegressorName] = nextSecsRegressorsForMain;
            }
        }

        /// <summary>
        /// Calculate correlation coefficients between all regressors
        /// </summary>
        private void CalcCorrelationCoefficients() {
            RegressorsCorrelation = new Dictionary<string, Dictionary<string, double>>();

            // Find correlation coefficients between all non-combine regressors
            foreach(var mainRegressor in SelectedStartRegressors.Keys) {
                Dictionary<string, double> secondsRegressorForMain = new Dictionary<string, double>();
                foreach(var secRegressor in SelectedStartRegressors.Keys) {
                    if (secRegressor != mainRegressor) {
                        if (RegressorsCorrelation.ContainsKey(secRegressor)) {
                            secondsRegressorForMain[secRegressor] = RegressorsCorrelation[secRegressor][mainRegressor];
                        }
                        else {
                            secondsRegressorForMain[secRegressor] = Statistics.PearsonCorrelationCoefficient(
                                SelectedStartRegressors[mainRegressor], SelectedStartRegressors[secRegressor]);
                        }
                    }
                }
                RegressorsCorrelation[mainRegressor] = secondsRegressorForMain;
            }
        }

        /// <summary>
        /// Get regressors wich coefficient of correlation more than threshold value
        /// </summary>
        /// <param name="secRegressors">List of regressors to choose from</param>
        /// <param name="mainRegressor">Main regressor</param>
        /// <param name="thresholdCorrValue">Threshold value of correlation coefficient</param>
        /// <returns>List of regressors</returns>
        private List<string> RegressorsForImpact(List<string> secRegressors, string mainRegressor,
            double thresholdCorrValue = 0) {
            List<string> selectedRegressors = new List<string>();

            foreach (var regressor in secRegressors) {
                if (Math.Abs(RegressorsCorrelation[mainRegressor][regressor]) > thresholdCorrValue) {
                    selectedRegressors.Add(regressor);
                }
            }

            return selectedRegressors;
        }

        /// <summary>
        /// Fill dictionary imact for first group of regressors
        /// </summary>
        /// <param name="regressorsForMain">Dictionary impact for main regressor</param>
        /// <param name="secRegressors">List of seconds regressors</param>
        /// <param name="mainRegressor">Main regressor</param>
        private void FillFirstGroupRegressors(ref Dictionary<string, Dictionary<string, double>> regressorsForMain,
            List<string> secRegressors, string mainRegressor) {

            foreach (var secRegressor in secRegressors) {
                regressorsForMain[secRegressor] = ImpactCoeffsFirstGroup(secRegressor, mainRegressor);
            }
        }

        /// <summary>
        /// Fill dictionary imact for first group of regressors
        /// </summary>
        /// <param name="regressorsForMain">Dictionary impact for main regressor</param>
        /// <param name="secRegressors">List of seconds regressors</param>
        /// <param name="firstGroupRegressors">List of regressors from first impact group</param>
        /// <param name="mainRegressor">Main regressor</param>
        private void FillSecondGroupRegressors(ref Dictionary<string, Dictionary<string, double>> regressorsForMain,
            List<string> secRegressors, List<string> firstGroupRegressors, string mainRegressor) {

            foreach (var secRegressor in secRegressors) {
                regressorsForMain[secRegressor] = ImpactCoeffsSecondGroup(firstGroupRegressors, secRegressor,
                    mainRegressor);
            }
        }

        /// <summary>
        /// Fill dictionary imact for first group of regressors
        /// </summary>
        /// <param name="regressorsForMain">Dictionary impact for main regressor</param>
        /// <param name="secRegressors">List of seconds regressors</param>
        /// <param name="firstGroupRegressors">List of regressors from first impact group</param>
        /// <param name="SecondGroupRegressors">List of regressors from second impact group</param>
        /// <param name="mainRegressor">Main regressor</param>
        private void FillThirdGroupRegressors(ref Dictionary<string, Dictionary<string, double>> regressorsForMain,
            List<string> secRegressors, List<string> firstGroupRegressors, List<string> SecondGroupRegressors,
            string mainRegressor) {

            foreach (var secRegressor in secRegressors) {
                regressorsForMain[secRegressor] = ImpactCoeffsThirdGroup(firstGroupRegressors, SecondGroupRegressors,
                    secRegressor, mainRegressor);
            }
        }

        /// <summary>
        /// Find coefficients of impact between high correlated regressors (0.7; 1]
        /// </summary>
        /// <param name="yRegressor">Y-regressor</param>
        /// <param name="xRegressor">X-regressor</param>
        /// <returns>Coefficients of impact between regressors</returns>
        private Dictionary<string, double> ImpactCoeffsFirstGroup(string yRegressor, string xRegressor) {
            return GetCoefficientsForImpactEquation(Statistics.PearsonCorrelationCoefficient(SelectedStartRegressors[yRegressor],
                SelectedStartRegressors[xRegressor]), yRegressor, xRegressor);
        }


        /// <summary>
        /// Find coefficients of impact between medium correlated regressors (0.3; 0.7]
        /// </summary>
        /// <param name="firstGroupRegressors">List of regressor from first group</param>
        /// <param name="yRegressor">Y-regressor</param>
        /// <param name="xRegressor">X-regressor</param>
        /// <returns>Coefficients of impact between regressors</returns>
        private Dictionary<string, double> ImpactCoeffsSecondGroup(List<string> firstGroupRegressors,
            string yRegressor, string xRegressor) {
            double rValue = 0;

            // Find r-value from higher group regressors
            foreach (var firstGroupRegressor in firstGroupRegressors) {
                rValue += RegressorsCorrelation[xRegressor][firstGroupRegressor] *
                    RegressorsCorrelation[firstGroupRegressor][yRegressor];
            }

            return GetCoefficientsForImpactEquation(rValue, yRegressor, xRegressor);
        }

        /// <summary>
        /// Find coefficients of impact between low correlated regressors (0.0; 0.3]
        /// </summary>
        /// <param name="firstGroupRegressors">List of regressor from first group</param>
        /// <param name="secondGroupRegressors">List of regressors from second group</param>
        /// <param name="yRegressor">Y-regressor</param>
        /// <param name="xRegressor">X-regressor</param>
        /// <returns>Coefficients of impact between regressors</returns>
        private Dictionary<string, double> ImpactCoeffsThirdGroup(List<string> firstGroupRegressors,
            List<string> secondGroupRegressors, string yRegressor, string xRegressor) {
            double rValue = 0;

            // Find r-value from higher group regressors
            foreach (var firstGroupRegressor in firstGroupRegressors) {
                foreach (var secondGroupRegressor in secondGroupRegressors) {
                    rValue += RegressorsCorrelation[xRegressor][firstGroupRegressor] *
                        RegressorsCorrelation[firstGroupRegressor][secondGroupRegressor] *
                        RegressorsCorrelation[secondGroupRegressor][yRegressor];
                }
            }

            return GetCoefficientsForImpactEquation(rValue, yRegressor, xRegressor);
        }

        /// <summary>
        /// Get coefficients for regressors impact equation
        /// </summary>
        /// <param name="r">Correlation coefficient</param>
        /// <param name="yRegressor">Y-regressor</param>
        /// <param name="xRegressor">X-regressor</param>
        /// <returns>Coefficients of impact equation</returns>
        private Dictionary<string, double> GetCoefficientsForImpactEquation(double r, string yRegressor, string xRegressor) {
            Dictionary<string, double> coeffs = new Dictionary<string, double>();
            coeffs["b"] = r * (Statistics.StandardDeviation(SelectedStartRegressors[yRegressor]) /
                Statistics.StandardDeviation(SelectedStartRegressors[xRegressor]));
            coeffs["a"] = SelectedStartRegressors[yRegressor].Average() - coeffs["b"] * SelectedStartRegressors[xRegressor].Average();

            return coeffs;
        }

        /// <summary>
        /// Open file (csv, excel) with data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadDataFileMenu_Click(object sender, EventArgs e) {
            try {
                if (dialogService.OpenFileDialog() == true) {
                    fileService = GetFileService(dialogService.FilePath);
                    RegressorsFromFile(fileService.Open(dialogService.FilePath));
                }
            }
            catch (Exception ex) {
                dialogService.ShowMessage(ex.Message);
            }
        }

        /// <summary>
        /// Get predicted regressants values for regressors from file
        /// </summary>
        /// <param name="allRows">Rows form file</param>
        private void RegressorsFromFile(List<List<string>> allRows) {
            List<List<string>> predictedRegressants = new List<List<string>>();

            // Create headers row
            List<string> headers = new List<string>(AllRegressors.Keys);
            foreach(var model in Models) {
                headers.Add(model.RegressantName);
            }
            predictedRegressants.Add(headers);

            Dictionary<string, List<double>> allRegressorsFromFile = new Dictionary<string, List<double>>();

            // Fill all regressors values from file data
            for (int col = 0; col < allRows[0].Count; col++) {
                string regressorName = allRows[0][col];
                allRegressorsFromFile.Add(regressorName, new List<double>());

                for (int row = 1; row < allRows.Count; row++) {
                    allRegressorsFromFile[regressorName].Add(Convert.ToDouble(allRows[row][col]));
                }
            }

            // Fill rows for file regressor form
            for (int row = 0; row < allRows.Count - 1; row++) {
                List<string> nextRow = new List<string>();
                Dictionary<string, double> regressorsRowValues = new Dictionary<string, double>();

                foreach (var regressorName in AllRegressors.Keys) {
                    double value = 0;

                    // If it's pairwise factor then multiply the factors
                    if (regressorName.Contains(" & ")) {
                        string[] pairwiseRegressors = regressorName.Split(new string[] { " & " }, StringSplitOptions.None);
                        double factor1 = allRegressorsFromFile.ContainsKey(pairwiseRegressors[0]) ? allRegressorsFromFile[pairwiseRegressors[0]][row]
                            : StartRegressors[pairwiseRegressors[0]];
                        double factor2 = allRegressorsFromFile.ContainsKey(pairwiseRegressors[1]) ? allRegressorsFromFile[pairwiseRegressors[1]][row]
                            : StartRegressors[pairwiseRegressors[1]];
                        value = factor1 * factor2;
                    }
                    else {
                        value = allRegressorsFromFile.ContainsKey(regressorName) ? allRegressorsFromFile[regressorName][row]
                            : StartRegressors[regressorName];
                    }

                    regressorsRowValues.Add(regressorName, value);
                }

                // Add regressors values to next Row
                foreach(var regressorValue in regressorsRowValues.Values) {
                    nextRow.Add(regressorValue.ToString());
                }

                // For each model add regressant value to next Row
                foreach(var model in Models) {
                    nextRow.Add(CalcModelValue(model, regressorsRowValues).ToString());
                }

                predictedRegressants.Add(nextRow);
            }

            // Show predicted regressants with regressors values
            FileRegressors fileRegressorsForm = new FileRegressors(predictedRegressants);
            fileRegressorsForm.Show();
        }

        /// <summary>
        /// Get right file service for reading file
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <returns>File Service</returns>
        IFileService GetFileService(string filename) {
            switch (filename.Split('.').Last()) {
                case "xls":
                    return new ExcelFileService();

                case "xlsx":
                    return new ExcelFileService();

                default:
                    return new ExcelFileService();
            }
        }

        private void regressorsSetDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == MODIFY_COLUMN && e.RowIndex >= 0) {
                string regressorName = AllRegressors.Keys.ToList()[e.RowIndex];

                // Check whether the regressorKey is not a combination of factors
                if (!regressorName.Contains(" & ")) {
                    try {
                        if ((regressorsSetDataGrid[e.ColumnIndex, e.RowIndex].Value != null && 
                            regressorsSetDataGrid[e.ColumnIndex, e.RowIndex].Value.ToString() != "-") ||
                            regressorsSetDataGrid[e.ColumnIndex, e.RowIndex].Value == null) {
                            double regressorValue = Convert.ToDouble(regressorsSetDataGrid[e.ColumnIndex, e.RowIndex].Value);

                            // Check if the value falls within the definition area
                            CheckRegressorDefArea(regressorName, regressorValue);
                            AllRegressors[regressorName] = regressorValue;

                            // Update models, that contains regressor
                            UpdateModels(GetModifiedModels(regressorName));

                            // Update regressors affected by changable regressor
                            if (checkMutualImpactFactors.Checked) {
                                UpdateImpactRegressors(regressorName);

                                // Update all pairwise combinations values
                                UpdatePairwiseCombinations();
                            }
                            else {
                                // Update pairwise combinations values
                                UpdatePairwiseCombinations(regressorName);
                            }
                        }
                    }
                    catch {
                        regressorsSetDataGrid[e.ColumnIndex, e.RowIndex].Value = AllRegressors[regressorName];
                    }
                }
                else {
                    regressorsSetDataGrid[e.ColumnIndex, e.RowIndex].Value = AllRegressors[regressorName];
                }
            }
        }

        /// <summary>
        /// Check whether the regressorKey value is within the boundaries of the definition area
        /// </summary>
        /// <param name="regressorName">Name of adjustable regressorKey</param>
        /// <param name="regressorValue">Value of adjustable regressorKey</param>
        private void CheckRegressorDefArea(string regressorName, double regressorValue) {
            if (regressorValue < RegressorsDefinitionArea[regressorName].Item1) {
                MessageBox.Show($"Значение регрессора ({regressorName}) не попадает в область определения (Минимальное значение - " +
                    $"{RegressorsDefinitionArea[regressorName].Item1}). Модель может работать некорректно.");
            }
            else if (regressorValue > RegressorsDefinitionArea[regressorName].Item2) {
                MessageBox.Show($"Значение регрессора ({regressorName}) не попадает в область определения (Максимальное значение - " +
                   $"{RegressorsDefinitionArea[regressorName].Item2}). Модель может работать некорректно.");
            }
        }

        /// <summary>
        /// Update models predicted values
        /// </summary>
        /// <param name="models">List of models for update</param>
        private void UpdateModels(List<Model> models) {
            foreach (var model in models) {
                regressantsResultDataGrid[1, Models.IndexOf(model)].Value = CalcModelValue(model);
            }
        }

        /// <summary>
        /// Update pairwise combinations that contains adjustable regressor
        /// </summary>
        /// <param name="regressorName">Name of adjustable regressor</param>
        private void UpdatePairwiseCombinations(string regressorName) {
            List<string> regressorsNames = new List<string>(AllRegressors.Keys);
            foreach(var regressorKey in regressorsNames) {

                // Check if it's combination of regressors
                if (regressorKey.Contains(regressorName) && regressorKey.Contains(" & ")) {
                    UpdatePairwiseCombination(regressorKey);
                }
            }
        }

        /// <summary>
        /// Update all pairwise combinations that contains adjustable regressor
        /// </summary>
        private void UpdatePairwiseCombinations() {
            List<string> regressorsNames = new List<string>(AllRegressors.Keys);
            foreach (var regressorKey in regressorsNames) {

                // Check if it's combination of regressors
                if (regressorKey.Contains(" & ")) {
                    UpdatePairwiseCombination(regressorKey);
                }
            }
        }

        /// <summary>
        /// Update single pairwise combination
        /// </summary>
        /// <param name="pariwiseCombinationRegressorname">Name of pairwise combination factor</param>
        private void UpdatePairwiseCombination(string pariwiseCombinationRegressorname) {
            List<string> regressorsNames = new List<string>(AllRegressors.Keys);

            // Find the regressors that form the combination
            string[] combinedRegressors = pariwiseCombinationRegressorname.Split(new string[] { " & " }, StringSplitOptions.None);

            // Update combination of regressors
            double newValue = AllRegressors[combinedRegressors[0]] * AllRegressors[combinedRegressors[1]];
            AllRegressors[pariwiseCombinationRegressorname] = newValue;
            regressorsSetDataGrid[MODIFY_COLUMN, regressorsNames.IndexOf(pariwiseCombinationRegressorname)].Value = newValue;

            // Check value of pairwise combination regressor
            CheckRegressorDefArea(pariwiseCombinationRegressorname, newValue);

            // Update models that contains combined regressorKey
            UpdateModels(GetModifiedModels(pariwiseCombinationRegressorname));
        }

        /// <summary>
        /// Update regressors affected by changable regressor
        /// </summary>
        /// <param name="changableRegressorName">Changable regressor name</param>
        private void UpdateImpactRegressors(string changableRegressorName) {
            List<string> needUpdatedRegressors = new List<string>(SelectedStartRegressors.Keys);
            UpdateAllRegressors(changableRegressorName, needUpdatedRegressors);

            List<string> regressorsNames = new List<string>(AllRegressors.Keys);

            // Update regressors values in dataGridView
            foreach(var regressorName in SelectedStartRegressors.Keys) {
                regressorsSetDataGrid[MODIFY_COLUMN, regressorsNames.IndexOf(regressorName)].Value =
                    AllRegressors[regressorName];
            }

            // Update all models
            UpdateModels(Models);
        }

        /// <summary>
        /// Update all regressors
        /// </summary>
        /// <param name="changableRegressorName">Cnangable regressor name</param>
        /// <param name="needUpdateRegressors">Not-updated regressors</param>
        private void UpdateAllRegressors(string changableRegressorName, List<string> needUpdateRegressors) {
            needUpdateRegressors.Remove(changableRegressorName);
            if (needUpdateRegressors.Count != 0) {
                foreach (var regressor in needUpdateRegressors) {
                    AllRegressors[regressor] = RegressorsImpact[changableRegressorName][regressor]["a"] +
                       RegressorsImpact[changableRegressorName][regressor]["b"] * AllRegressors[changableRegressorName];
                }
                UpdateAllRegressors(needUpdateRegressors[0], needUpdateRegressors);
            }         
        }

        /// <summary>
        /// Get a list of models that contain a modified regressorKey
        /// </summary>
        /// <param name="regressorName">Name of modified regressorKey</param>
        /// <returns></returns>
        private List<Model> GetModifiedModels(string regressorName) {
            List<Model> modifiedModels = new List<Model>();

            foreach(var model in Models) {
                if (model.Regressors.Keys.Contains(regressorName)) {
                    modifiedModels.Add(model);
                }
            }

            return modifiedModels;
        }

        /// <summary>
        /// Set column headers and column settings to dataGV
        /// </summary>
        /// <param name="headers">List of column headers</param>
        /// <param name="dataGV">DataGridView</param>
        /// <param name="autoSize">AutoSize column width</param>
        /// <param name="indexOfSortableColumns">List of indexes of sortable columns</param>
        /// <param name="indexOfModifiableColumns">List of indexes of modifiable columns</param>
        private void SetDataGVColumnHeaders(List<string> headers, DataGridView dataGV, bool autoSize, 
            List<int> indexOfSortableColumns = null, List<int> indexOfModifiableColumns = null) {
            dataGV.ColumnCount = headers.Count;
            for (int i = 0; i < dataGV.Columns.Count; i++) {
                dataGV.Columns[i].HeaderText = headers[i];
                dataGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGV.Columns[i].ReadOnly = true;
                if (autoSize) {
                    dataGV.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            if (indexOfSortableColumns != null) {
                foreach (var index in indexOfSortableColumns) {
                    dataGV.Columns[index].SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }
            if (indexOfModifiableColumns != null) {
                foreach(var index in indexOfModifiableColumns) {
                    dataGV.Columns[index].ReadOnly = false;
                }
            }
            dataGV.ColumnHeadersVisible = true;
        }

        private void SimulationControlForm_FormClosing(object sender, FormClosingEventArgs e) {
            resizeWorker.CancelAsync();
        }

        private void SimulationControlForm_Resize(object sender, EventArgs e) {
            isResizeNeeded = true;
        }

        private void SimulationControlForm_ResizeEnd(object sender, EventArgs e) {
            isResizeNeeded = true;
        }

        private void exitFormMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Resize all form components
        /// </summary>
        private void DoResizeComponents(object sender, DoWorkEventArgs e) {
            // Check if resizeWorker has been stopped
            if (resizeWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                while (true) {
                    System.Threading.Thread.Sleep(50);
                    if (isResizeNeeded) {
                        int formWidth = this.Width;
                        int formHeight = this.Height;

                        regressantsResultDataGrid.Invoke(new Action<Point>((loc) => regressantsResultDataGrid.Location = loc),
                            new Point(formWidth / 2 + 58, regressantsResultDataGrid.Location.Y));

                        labelRegressants.Invoke(new Action<Point>((loc) => labelRegressants.Location = loc),
                            new Point(regressantsResultDataGrid.Location.X - 3, labelRegressants.Location.Y));

                        regressantsResultDataGrid.Invoke(new Action<Size>((size) => regressantsResultDataGrid.Size = size),
                            new Size(formWidth - regressantsResultDataGrid.Location.X - 28, formHeight - 97));

                        regressorsSetDataGrid.Invoke(new Action<Size>((size) => regressorsSetDataGrid.Size = size),
                            new Size(regressantsResultDataGrid.Location.X - 51 - regressorsSetDataGrid.Location.X, formHeight - 97));

                        checkMutualImpactFactors.Invoke(new Action<Point>((loc) => checkMutualImpactFactors.Location = loc),
                            new Point(regressorsSetDataGrid.Size.Width - 149, checkMutualImpactFactors.Location.Y));


                        isResizeNeeded = false;
                    }
                }
            }
        }
    }
}
