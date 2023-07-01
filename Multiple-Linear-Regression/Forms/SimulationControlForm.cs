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
using static OfficeOpenXml.ExcelErrorValue;

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

                        regressorsSetDataGrid.Rows.Add(new string[] {regressor.Key, 
                            Math.Round(regressor.Value.Last(), 2).ToString(),
                            Math.Round(RegressorsDefinitionArea[regressor.Key].Item1, 2).ToString(), 
                            Math.Round(RegressorsDefinitionArea[regressor.Key].Item2, 2).ToString()});
                    }
                }
                // Calculate the regressants values
                regressantsResultDataGrid.Rows.Add(new string[] {model.RegressantName, 
                    Math.Round(CalcModelValue(model), 2).ToString(),
                    model.Equation});
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
        /// <param name="regressors">Dictionary of regressors with values</param>
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
            NumberGroupOfCorrelatedRegressors = (int)Math.Log(2,AllRegressors.Count);

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

            // Find threshold correlation coefficients
            List<double> corrIntervals = GetCorrIntervals();

            // For each regressor, find the impact on the other regressors
            foreach (var mainRegressorName in SelectedStartRegressors.Keys) {
                Dictionary<string, Dictionary<string, double>> nextSecsRegressorsForMain =
                    new Dictionary<string, Dictionary<string, double>>();
                List<string> unUsedRegressors = new List<string>(SelectedStartRegressors.Keys);
                List<List<string>> groupsRegressors = new List<List<string>>();

                unUsedRegressors.Remove(mainRegressorName);

                // Fill second regressors for main regressor
                foreach (var corrLevel in corrIntervals) {

                    // Find regressors for next correlation level
                    List<string> nextGroupRegressors = RegressorsForImpact(unUsedRegressors, mainRegressorName, corrLevel);
                    
                    if (nextGroupRegressors.Count > 0) {
                        groupsRegressors.Add(nextGroupRegressors);
                        FillNextGroupRegressors(ref nextSecsRegressorsForMain, groupsRegressors, mainRegressorName);
                        unUsedRegressors = unUsedRegressors.Except(nextGroupRegressors).ToList();
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
        /// Get threshold values for find correlation group of regressors
        /// </summary>
        /// <returns>List of threshold values</returns>
        private List<double> GetCorrIntervals() {
            List<double> thresholdValues = new List<double>();
            double oneStep = 1.0 / NumberGroupOfCorrelatedRegressors;

            // Find next threshold value
            for (int i = 0; i < NumberGroupOfCorrelatedRegressors; i++) {
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
        /// Fill dictionary impact for group of regressors
        /// </summary>
        /// <param name="regressorsForMain">Dictionary impact for main regressor</param>
        /// <param name="groupsOfRegressors"></param>
        /// <param name="mainRegressor"></param>
        private void FillNextGroupRegressors(ref Dictionary<string, Dictionary<string, double>> regressorsForMain,
            List<List<string>> groupsOfRegressors, string mainRegressor) {

            foreach (var secRegressor in groupsOfRegressors.Last()) {
                regressorsForMain[secRegressor] = ImpactCoefficientsGroup(secRegressor, mainRegressor,
                    groupsOfRegressors.GetRange(0, groupsOfRegressors.Count() - 1));
            }
        }

        /// <summary>
        /// Find coefficients of impact between correlated regressors
        /// </summary>
        /// <param name="yRegressor">Y-regressor</param>
        /// <param name="xRegressor">X-regressor</param>
        /// <param name="prevGroupsRegressors">Groups of regressors with a high correlation coefficient</param>
        /// <returns>Coefficients of impact between regressors</returns>
        private Dictionary<string, double> ImpactCoefficientsGroup(string yRegressor, string xRegressor, 
            List<List<string>> prevGroupsRegressors) {

            double rValue = 0;

            if (prevGroupsRegressors.Count > 0) {
                List<int> indexesOfRegressors = Enumerable.Repeat(0, prevGroupsRegressors.Count).ToList();

                // Find rValue
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
                        nextCoeff *= RegressorsCorrelation[leftRegressor][rightRegressor];
                        leftRegressor = rightRegressor;
                    }
                    nextCoeff *= RegressorsCorrelation[leftRegressor][yRegressor];

                    rValue += nextCoeff;
                    CalcIndexesOfNextRegressors(ref indexesOfRegressors, prevGroupsRegressors,
                        indexesOfRegressors.Count - 1);
                }
            }
            else {
                rValue = RegressorsCorrelation[yRegressor][xRegressor];
            }

            // Checking Exceptional Situations
            rValue = rValue < -1 ? -1 : rValue;
            rValue = rValue > 1 ? 1 : rValue;

            return GetCoefficientsForImpactEquation(rValue, yRegressor, xRegressor);
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

                    List<List<string>> allRows = new List<List<string>>();
                    try {
                        allRows = fileService.Open(dialogService.FilePath);
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                    }
                    finally {
                        RegressorsFromFile(allRows);
                    }
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
                    allRegressorsFromFile[regressorName].Add(Math.Round(Convert.ToDouble(allRows[row][col]), 2));
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

                    regressorsRowValues.Add(regressorName, Math.Round(value, 2));
                }

                // Add regressors values to next Row
                foreach(var regressorValue in regressorsRowValues.Values) {
                    nextRow.Add(regressorValue.ToString());
                }

                // For each model add regressant value to next Row
                foreach(var model in Models) {
                    nextRow.Add(Math.Round(CalcModelValue(model, regressorsRowValues), 2).ToString());
                }

                predictedRegressants.Add(nextRow);
            }

            // Show predicted regressants with regressors values
            FileRegressors fileRegressorsForm = new FileRegressors(predictedRegressants, "Имитация управления",
                StepsInfo.ImitationRegressorsFromFile);
            fileRegressorsForm.ShowDialog();
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

                            regressorsSetDataGrid[e.ColumnIndex, e.RowIndex].Value = Math.Round(Convert.ToDouble(regressorsSetDataGrid[e.ColumnIndex, e.RowIndex].Value), 2);
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
                regressantsResultDataGrid[1, Models.IndexOf(model)].Value = Math.Round(CalcModelValue(model), 2);
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
            regressorsSetDataGrid[MODIFY_COLUMN, regressorsNames.IndexOf(pariwiseCombinationRegressorname)].Value = Math.Round(newValue, 2);

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
                    Math.Round(AllRegressors[regressorName], 2);
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
