using DeepParameters;
using DeepParameters.Work_WIth_Files;
using DeepParameters.Work_WIth_Files.Interfaces;
using Multiple_Linear_Regression.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiple_Linear_Regression {
    public partial class MainForm : Form {
        private IFileService fileService;
        private IDialogService dialogService = new DefaultDialogService();

        private BackgroundWorker resizeWorker = new BackgroundWorker();
        private bool isResizeNeeded = false;

        //private List<string> Headers { get; set; } = new List<string>();
        //private List<string> RegressantsHeaders { get; set; } = new List<string>();
        //private List<string> RegressorsHeaders { get; set; } = new List<string>();

        private Dictionary<string, int> RegressantsHeaders { get; set; } = new Dictionary<string, int>();
        private Dictionary<string, int> RegressorsHeaders { get; set; } = new Dictionary<string, int>();
        private Dictionary<string, int> Headers { get; set; } = new Dictionary<string, int>();
        private Dictionary<string, List<double>> BaseRegressors { get; set; } = new Dictionary<string, List<double>>();

        private List<Model> Models { get; set; } = new List<Model>();

        private double[,] X { get; set; }
        private double[] Y { get; set; }


        public MainForm() {
            InitializeComponent();

            // Centered Main From on the screen
            this.CenterToScreen();

            // Locks all tabs except the first one
            LocksAllTabs();
            loadDataTab.Enabled = true;

            helpAllStepsMenu.ToolTipText = StepsInfo.Step1;

            // Run background worker for resizing components on form
            resizeWorker.DoWork += new DoWorkEventHandler(DoResizeComponents);
            resizeWorker.WorkerSupportsCancellation = true;
            resizeWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Locks all tabs in tab collection
        /// </summary>
        private void LocksAllTabs() {
            foreach (TabPage tab in allTabs.TabPages) {
                tab.Enabled = false;
            }
        }

        /// <summary>
        /// Open file (csv, excel) with data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileMenu_Click(object sender, EventArgs e) {
            allTabs.SelectTab(loadDataTab);

            if (dialogService.OpenFileDialog() == true) {
                fileService = GetFileService(dialogService.FilePath);

                RunBackgroundWorkerLoadFile();
            }
        }

        /// <summary>
        /// Run background worker for load dataset
        /// </summary>
        private void RunBackgroundWorkerLoadFile() {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.ProgressChanged += new ProgressChangedEventHandler((sender, e) => ProgressBarChanged(sender, e, progressBarDataLoad));
            bgWorker.DoWork += new DoWorkEventHandler((sender, e) => LoadData(sender, e, bgWorker));
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            factorsData.Size = new Size(factorsData.Width, factorsData.Height - 19);
            progressBarDataLoad.Value = 0;
            progressBarDataLoad.Visible = true;
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Load dataset from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void LoadData(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Check if bgworker has been stopped
            if (bgWorker.CancellationPending == true) {
                e.Cancel = true;
            }
            else {
                List<List<string>> allRows = fileService.Open(dialogService.FilePath);
                if (allRows.Count > 0) {

                    // Clear Step1
                    Action clear = () => ClearControlsStep1();
                    factorsData.Invoke(clear);

                    // Add headers to properties
                    Headers.Clear();
                    int headerCounter = 0;
                    foreach (var header in allRows[0]) {
                        Headers[header] = headerCounter;
                        headerCounter++;
                    }

                    RegressantsHeaders.Clear();
                    RegressorsHeaders.Clear();

                    // Add headers to data grid view
                    factorsData.Invoke(new Action<List<string>>((s) => SetDataGVColumnHeaders(s, factorsData, false)), allRows[0]);

                    // Create start parameters for progress bar
                    int progress = 0;
                    int step = (allRows.Count - 1) / 100;
                    int oneBarInProgress = 1;
                    if ((allRows.Count - 1) < 100) {
                        step = 1;
                        oneBarInProgress = (100 / (allRows.Count - 1)) + 1;
                    }

                    for (int i = 1; i < allRows.Count; i++) {
                        // Find progress
                        if ((i - 1) % step == 0) {
                            progress += oneBarInProgress;
                            bgWorker.ReportProgress(progress);
                        }

                        // Check empty elements in row
                        if (allRows[i].All(elem => elem.ToString() != "")) {
                            // Add row to dataGridView
                            factorsData.Invoke(new Action<List<string>>((s) => factorsData.Rows.Add(s.ToArray())), allRows[i]);
                        }
                    }

                    // Enable button to select regressors and regressants
                    selectRegressorsButton.Invoke(new Action<bool>((b) => selectRegressorsButton.Enabled = b), true);
                    selectRegressantsButton.Invoke(new Action<bool>((b) => selectRegressantsButton.Enabled = b), true);

                    // Enable accept selected factors button
                    acceptFactorsButton.Invoke(new Action<bool>((b) => acceptFactorsButton.Enabled = b), true);

                    // Enable clear selected factors button
                    clearSelectedFactorsButton.Invoke(new Action<bool>((b) => clearSelectedFactorsButton.Enabled = b), true);

                    // Hide progress bar
                    progressBarDataLoad.Invoke(new Action<bool>((b) => progressBarDataLoad.Visible = b), false);

                    // Resize dataGrid
                    factorsData.Invoke(new Action<Size>((size) => factorsData.Size = size),
                        new Size(factorsData.Width, factorsData.Height + 19));

                    bgWorker.CancelAsync();
                }
            }
        }

        private void selectRegressantsButton_Click(object sender, EventArgs e) {
            SelectParametersForm form = new SelectParametersForm("Выбор управляемых факторов", 
                Headers.Keys.Except(RegressorsHeaders.Keys).Except(RegressantsHeaders.Keys).ToList(), RegressantsHeaders.Keys.ToList());
            form.ShowDialog();

            // Get selected factors
            RegressantsHeaders.Clear();
            foreach (var header in form.SelectedFactors) {
                RegressantsHeaders[header] = Headers[header];
            }

            // Print factors to listbox
            regressantsList.Items.Clear();
            regressantsList.Items.AddRange(RegressantsHeaders.Keys.ToArray());
        }

        private void selectRegressorsButton_Click(object sender, EventArgs e) {
            SelectParametersForm form = new SelectParametersForm("Выбор управляющих факторов",
                Headers.Keys.Except(RegressantsHeaders.Keys).Except(RegressorsHeaders.Keys).ToList(), RegressorsHeaders.Keys.ToList());
            form.ShowDialog();

            // Get selected factors
            RegressorsHeaders.Clear();
            foreach (var header in form.SelectedFactors) {
                RegressorsHeaders[header] = Headers[header];
            }

            // Print factors to listbox
            regressorsList.Items.Clear();
            regressorsList.Items.AddRange(RegressorsHeaders.Keys.ToArray());
        }

        private void clearSelectedFactorsButton_Click(object sender, EventArgs e) {
            regressorsList.Items.Clear();
            regressantsList.Items.Clear();
            RegressantsHeaders.Clear();
            RegressorsHeaders.Clear();
            labelResultDataLoad.Visible = false;
        }

        private void acceptFactorsButton_Click(object sender, EventArgs e) {
            if (RegressorsHeaders.Count > 0 && RegressantsHeaders.Count > 0) {
                LoadValuesForFactors();

                if (checkPairwiseCombinations.Checked) {
                    CreatePairwiseCombinationsOfFactors();
                }

                FillRegressorsForModels();

                labelResultDataLoad.Visible = true;
                ClearControlsStep2();
                processingStatDataTab.Enabled = true;
                doFunctionalProcessButton.Enabled = true;
            }
            else {
                if (RegressantsHeaders.Count == 0 && RegressorsHeaders.Count == 0) {
                    MessageBox.Show("Вы не выбрали показатели для исследования");
                }
                else if (RegressantsHeaders.Count == 0) {
                    MessageBox.Show("Вы не выбрали управляемые факторы для исследования");
                }
                else {
                    MessageBox.Show("Вы не выбрали управляющие факторы для исследования");
                }
            }
        }

        /// <summary>
        /// Load values for selected factors from data grid view
        /// </summary>
        private void LoadValuesForFactors() {
            Models.Clear();
            
            // Load regressants
            foreach (var factor in RegressantsHeaders) {
                List<double> regressantValues = new List<double>();

                for (int row = 0; row < factorsData.Rows.Count; row++) {
                    regressantValues.Add(Convert.ToDouble(factorsData[factor.Value, row].Value));
                }
                Models.Add(new Model(factor.Key, regressantValues));
            }

            // Load regressors
            foreach (var factor in RegressorsHeaders) {
                List<double> regressorsValues = new List<double>();
                
                for (int row = 0; row < factorsData.Rows.Count; row++) {
                    regressorsValues.Add(Convert.ToDouble(factorsData[factor.Value, row].Value));
                }
                BaseRegressors[factor.Key] = regressorsValues;
            }
        }

        /// <summary>
        /// Create pairwise combinations of factors as new factors
        /// </summary>
        private void CreatePairwiseCombinationsOfFactors() {
            List<string> RegressorsKeys = BaseRegressors.Keys.ToList();

            // Create new factor as pairwise combination of factors
            for (int i = 0; i < RegressorsKeys.Count - 1; i++) {
                for (int j = i + 1; j < RegressorsKeys.Count; j++) {
                    List<double> newRegressorFactorValues = new List<double>();

                    // The value of the new factor is obtained by multiplying the values of the two factors
                    for (int elemNum = 0; elemNum < BaseRegressors[RegressorsKeys[i]].Count; elemNum++) {
                        newRegressorFactorValues.Add(BaseRegressors[RegressorsKeys[i]][elemNum] * BaseRegressors[RegressorsKeys[j]][elemNum]);
                    }
                    BaseRegressors[RegressorsKeys[i] + " & " + RegressorsKeys[j]] = newRegressorFactorValues;
                }
            }
        }

        /// <summary>
        /// Set regressors and regressors names for each model
        /// </summary>
        private void FillRegressorsForModels() {
            Models.ForEach(model => model.SetNewRegressors(BaseRegressors));
            Models.ForEach(model => model.SetRegressorsNames(BaseRegressors.Keys.ToList()));
        }

        private void doFunctionalProcessButton_Click(object sender, EventArgs e) {
            if (BaseRegressors.Count > 0) {
                UserWarningForm warningForm = new UserWarningForm(StepsInfo.UserWarningFuncPreprocessing);
                warningForm.ShowDialog();
                if (warningForm.AcceptAction) {
                    RunBackgroundFunctionalProcessData();

                    doFunctionalProcessButton.Enabled = false;
                }
            }
            else {
                MessageBox.Show("Вы не выбрали ни одного управляющего фактора");
            }
        }

        /// <summary>
        /// Run background worker for functional process data
        /// </summary>
        private void RunBackgroundFunctionalProcessData() {
            SetDataGVColumnHeaders(new List<string>() { "Регрессант", "Регрессор", "Функции предобработки", "Модуль коэффициента корреляции" },
                functionsForProcessingDataGrid, true, new List<int>() { 3 });

            // Background worker for function preprocessing
            BackgroundWorker bgWorkerFunc = new BackgroundWorker();
            bgWorkerFunc.DoWork += new DoWorkEventHandler((sender, e) => FunctionProcessing(sender, e, bgWorkerFunc));
            bgWorkerFunc.WorkerSupportsCancellation = true;
            bgWorkerFunc.RunWorkerAsync();

            // Backgound worker for loading label
            BackgroundWorker bgWorkerLabel = new BackgroundWorker();
            bgWorkerLabel.DoWork += new DoWorkEventHandler((sender, e) =>
                ShowLoadingFunctionPreprocessing(sender, e, bgWorkerLabel, bgWorkerFunc));
            bgWorkerLabel.WorkerSupportsCancellation = true;
            bgWorkerLabel.RunWorkerAsync();
        }

        /// <summary>
        /// Find the functions that maximize the Pearson coefficient between regressors and regressants factors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void FunctionProcessing(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Check if bgWorkerFunc has been stopped
            if (bgWorker.CancellationPending == true) {
                e.Cancel = true;
            }
            else {
                // Find best functions for each regressors for each model
                foreach(var model in Models) {
                    model.StartFunctionalPreprocessing();

                    // Add functions and correlation coefficients to data grid view
                    foreach (var regressor in model.ProcessFunctions) {
                        // Add row of preprocess functions to data grid
                        functionsForProcessingDataGrid.Invoke(new Action<List<string>>((row) => functionsForProcessingDataGrid.Rows.Add(row.ToArray())),
                            new List<string>() { model.RegressantName, regressor.Key,
                                String.Join(", ", regressor.Value.ToArray()),
                                Math.Abs(model.CorrelationCoefficient[regressor.Key]).ToString() });
                    }
                }

                bgWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Function for showing logo of the loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void ShowLoadingFunctionPreprocessing(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker, BackgroundWorker bgWorkerFunc) {
            labelFuncPreprocess.Invoke(new Action<bool>((vis) => labelFuncPreprocess.Visible = vis), true);

            // While bgWorkerFunc is busy, we will update the load indicator
            while (bgWorkerFunc.IsBusy == true) {
                if (labelFuncPreprocess.Text.Count(symb => symb == '.') < 3) {
                    labelFuncPreprocess.Invoke(new Action<string>((load) => labelFuncPreprocess.Text = load),
                        labelFuncPreprocess.Text + ".");
                }
                else {
                    labelFuncPreprocess.Invoke(new Action<string>((load) => labelFuncPreprocess.Text = load),
                        "Загрузка");
                }
                System.Threading.Thread.Sleep(500);
            }

            labelFuncPreprocess.Invoke(new Action<bool>((vis) => labelFuncPreprocess.Visible = vis), false);
            labelPreprocessingFinish.Invoke(new Action<bool>((vis) => labelPreprocessingFinish.Visible = vis), true);
            bgWorker.CancelAsync();
        }

        /// <summary>
        /// Function for clear controls start with step1
        /// </summary>
        private void ClearControlsStep1() {
            ClearDataGV(factorsData);
            selectRegressantsButton.Enabled = false;
            selectRegressorsButton.Enabled = false;
            acceptFactorsButton.Enabled = false;
            clearSelectedFactorsButton.Enabled = false;
            regressantsList.Items.Clear();
            regressorsList.Items.Clear();
            labelResultDataLoad.Visible = false;

            ClearControlsStep2();
        }

        /// <summary>
        /// Function for clear controls start with step2
        /// </summary>
        private void ClearControlsStep2() {
            ClearDataGV(functionsForProcessingDataGrid);
            doFunctionalProcessButton.Enabled = false;
            labelFuncPreprocess.Visible = false;
        }

        /// <summary>
        /// Clear headers and data from dataGridView
        /// </summary>
        /// <param name="data">dataGridView</param>
        /// <param name="columnHeadersVisible">Show headers of columns</param>
        private void ClearDataGV(DataGridView data, bool columnHeadersVisible = false) {
            data.Rows.Clear();
            data.ColumnHeadersVisible = columnHeadersVisible;
            data.Refresh();
        }

        /// <summary>
        /// Set column headers and column settings to dataGV
        /// </summary>
        /// <param name="headers">List of column headers</param>
        /// <param name="dataGV">DataGridView</param>
        /// <param name="autoSize">AutoSize column width</param>
        private void SetDataGVColumnHeaders(List<string> headers, DataGridView dataGV, bool autoSize, List<int> indexOfSortableColumns = null) {
            dataGV.ColumnCount = headers.Count;
            for (int i = 0; i < dataGV.Columns.Count; i++) {
                dataGV.Columns[i].HeaderText = headers[i];
                dataGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (autoSize) {
                    dataGV.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            if (indexOfSortableColumns != null) {
                foreach (var index in indexOfSortableColumns) {
                    dataGV.Columns[index].SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }
            dataGV.ColumnHeadersVisible = true;
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

        /// <summary>
        /// Change progress bar value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="progressBar">Progress bar that will be changed</param>
        private void ProgressBarChanged(object sender, ProgressChangedEventArgs e, ProgressBar progressBar) {
            if (e.ProgressPercentage > 100) {
                progressBar.Value = 100;
            }
            else {
                progressBar.Value = e.ProgressPercentage;
            }
        }

        /// <summary>
        /// Exit application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitAppMenu_Click(object sender, EventArgs e) {
            var exitForm = new ExitForm();
            exitForm.Show();
        }

        /// <summary>
        /// Change information text about step
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allTabs_Selected(object sender, TabControlEventArgs e) {
            switch (allTabs.SelectedIndex) {
                case 0:
                    helpAllStepsMenu.ToolTipText = StepsInfo.Step1;
                    break;
                case 1:
                    helpAllStepsMenu.ToolTipText = StepsInfo.Step2;
                    break;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e) {
            isResizeNeeded = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            resizeWorker.CancelAsync();
        }

        /// <summary>
        /// Resize all main from components
        /// </summary>
        private void DoResizeComponents(object sender, DoWorkEventArgs e) {
            // Check if resizeWorker has been stopped
            if (resizeWorker.CancellationPending == true) {
                e.Cancel = true;
            }
            else {
                while (true) {
                    if (isResizeNeeded) {
                        int newWidth = this.Width - 196;
                        int newHeight = this.Height - 67;
                        int widthDiff = newWidth - allTabs.Width;
                        int heightDiff = newHeight - allTabs.Height;
                        allTabs.Invoke(new Action<Size>((size) => allTabs.Size = size), new Size(newWidth, newHeight));


                        // Tab 1
                        selectRegressantsButton.Invoke(new Action<Point>((loc) => selectRegressantsButton.Location = loc),
                            new Point(selectRegressantsButton.Location.X + widthDiff, selectRegressantsButton.Location.Y));

                        selectRegressorsButton.Invoke(new Action<Point>((loc) => selectRegressorsButton.Location = loc),
                            new Point(selectRegressorsButton.Location.X + widthDiff, selectRegressorsButton.Location.Y));

                        clearSelectedFactorsButton.Invoke(new Action<Point>((loc) => clearSelectedFactorsButton.Location = loc),
                            new Point(clearSelectedFactorsButton.Location.X + widthDiff, clearSelectedFactorsButton.Location.Y));

                        acceptFactorsButton.Invoke(new Action<Point>((loc) => acceptFactorsButton.Location = loc),
                            new Point(acceptFactorsButton.Location.X + widthDiff, acceptFactorsButton.Location.Y));

                        factorsData.Invoke(new Action<Size>((size) => factorsData.Size = size),
                            new Size(factorsData.Width + widthDiff, factorsData.Height + heightDiff));

                        progressBarDataLoad.Invoke(new Action<Point>((loc) => progressBarDataLoad.Location = loc),
                            new Point(progressBarDataLoad.Location.X, progressBarDataLoad.Location.Y + heightDiff));

                        progressBarDataLoad.Invoke(new Action<Size>((size) => progressBarDataLoad.Size = size),
                            new Size(progressBarDataLoad.Width + widthDiff, progressBarDataLoad.Height));

                        checkPairwiseCombinations.Invoke(new Action<Point>((loc) => checkPairwiseCombinations.Location = loc),
                            new Point(checkPairwiseCombinations.Location.X + widthDiff, checkPairwiseCombinations.Location.Y));

                        labelResultDataLoad.Invoke(new Action<Point>((loc) => labelResultDataLoad.Location = loc),
                            new Point(labelResultDataLoad.Location.X + widthDiff, labelResultDataLoad.Location.Y));


                        isResizeNeeded = false;
                    }
                }
            }
        }
    }
}
