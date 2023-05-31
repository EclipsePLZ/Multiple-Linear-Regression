using Multiple_Linear_Regression.Work_WIth_Files;
using Multiple_Linear_Regression.Work_WIth_Files.Interfaces;
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

            SetStartParameters();

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
            // Background worker for factors data load
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
            if (bgWorker.CancellationPending) {
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

                // Create pairwise combinations of factors if it's needed
                if (checkPairwiseCombinations.Checked) {
                    CreatePairwiseCombinationsOfFactors();
                }

                ClearControlsStep2();
                ClearControlsStep3();
                ClearControlsStep4();
                FillRegressorsForModels();

                labelResultDataLoad.Visible = true;
                processingStatDataTab.Enabled = true;
                removeUnimportantFactorsTab.Enabled = true;
                buildRegrEquationsTab.Enabled = true;
                buildEquationsButton.Enabled = true;
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
            BaseRegressors.Clear();
            
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

            // Fill filtered table headers
            SetDataGVColumnHeaders(new List<string>() { "Регрессант", "Регрессор", "Функции предобработки", "Модуль коэффициента корреляции" },
                onlyImportantFactorsDataGrid, true, new List<int>() { 3 });

            // Fill filtered dataGrid view
            RunBackgroundFillFilteredFactors();
        }

        private void doFunctionalProcessButton_Click(object sender, EventArgs e) {
            if (BaseRegressors.Count > 0) {
                // Show warinig form
                UserWarningForm warningForm = new UserWarningForm(StepsInfo.UserWarningFuncPreprocessing);
                warningForm.ShowDialog();
                if (warningForm.AcceptAction) {
                    RunBackgroundFunctionalProcessData();

                    ClearControlsStep4();
                    buildEquationsButton.Enabled = true;
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
                ShowLoadingFunctionPreprocessing(sender, e, bgWorkerLabel, bgWorkerFunc, labelFuncPreprocess, labelPreprocessingFinish));
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
            // Check if mainBgWorker has been stopped
            if (bgWorker.CancellationPending) {
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
                // Fill filtered data grid view
                Action action = () => RunBackgroundFillFilteredFactors();
                onlyImportantFactorsDataGrid.Invoke(action);

                bgWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Function for showing logo of the loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void ShowLoadingFunctionPreprocessing(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker, 
            BackgroundWorker mainBgWorker, Label loadLabel, Label finishLabel) {
            // Check if bgworker has been stopped
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                loadLabel.Invoke(new Action<bool>((vis) => loadLabel.Visible = vis), true);

                // While mainBgWorker is busy, we will update the load indicator
                while (mainBgWorker.IsBusy == true) {
                    if (loadLabel.Text.Count(symb => symb == '.') < 3) {
                        loadLabel.Invoke(new Action<string>((load) => loadLabel.Text = load),
                            loadLabel.Text + ".");
                    }
                    else {
                        loadLabel.Invoke(new Action<string>((load) => loadLabel.Text = load),
                            loadLabel.Text.Replace(".", ""));
                    }
                    System.Threading.Thread.Sleep(500);
                }

                // Hide loadLabel
                loadLabel.Invoke(new Action<bool>((vis) => loadLabel.Visible = vis), false);

                // Showing finish label
                finishLabel.Invoke(new Action<bool>((vis) => finishLabel.Visible = vis), true);
                bgWorker.CancelAsync();
            }
        }

        private void acceptFilterFactorsButton_Click(object sender, EventArgs e) {
            RunBackgroundFilterRegressors();

            ClearControlsStep4();
            buildEquationsButton.Enabled = true;
        }

        private void RunBackgroundFilterRegressors() {
            // Background worker for filtering regressors
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler((sender, e) => FilterRegressors(sender, e, bgWorker));
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerAsync();

            // Background worker for loading label
            BackgroundWorker bgWorkerLoad = new BackgroundWorker();
            bgWorkerLoad.DoWork += new DoWorkEventHandler((sender, e) => 
                ShowLoadingFunctionPreprocessing(sender, e, bgWorkerLoad, bgWorker, labelFilterLoad, labelFilterFinish));
            bgWorkerLoad.WorkerSupportsCancellation = true;
            bgWorkerLoad.RunWorkerAsync();
        }

        private void FilterRegressors(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Check if bgworker has been stopped
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                try {
                    if (classicWayRadio.Checked) {
                        ClassicWayToFilterRegressors();
                    }
                    else if (empWayRadio.Checked) {
                        EmpiricalWayToFilterRegressors();
                    }

                    // Update data grid view with filtered factors
                    Action action = () => RunBackgroundFillFilteredFactors();
                    onlyImportantFactorsDataGrid.Invoke(action);

                    // Enable cancel filtering button
                    cancelFilterFactorsButton.Invoke(new Action<bool>((b) => cancelFilterFactorsButton.Enabled = b), true);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                bgWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Filter unimportant regressors by classic way
        /// </summary>
        private void ClassicWayToFilterRegressors() {
            foreach (var model in Models) {
                model.ClassicWayFilterRegressors();
            }
        }

        /// <summary>
        /// Filter unimportant regressors by empirical way
        /// </summary>
        private void EmpiricalWayToFilterRegressors() {
            double thresholdValueCorr = Convert.ToDouble(valueEmpWayCorr.Value);

            foreach (var model in Models) {
                model.EmpiricalWayFilterRegressors(thresholdValueCorr);
            }
        }

        private void cancelFilterFactorsButton_Click(object sender, EventArgs e) {
            // Restore non-filter regressors for each model
            Models.ForEach(model => model.RestoreNonFilterRegressors());
            cancelFilterFactorsButton.Enabled = false;

            // Fill filtered data grid
            RunBackgroundFillFilteredFactors();

            ClearControlsStep4();
            buildEquationsButton.Enabled = true;
        }

        /// <summary>
        /// Run background worker for fill filtered factors data grid
        /// </summary>
        private void RunBackgroundFillFilteredFactors() {
            ClearDataGV(onlyImportantFactorsDataGrid, true);

            // Backgound worker for fill filtered factors in data grid view
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.ProgressChanged += new ProgressChangedEventHandler((sender, e) => ProgressBarChanged(sender, e, progressBarFillFilteredData));
            bgWorker.DoWork += new DoWorkEventHandler((sender, e) => FillFilteredFactors(sender, e, bgWorker));
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            onlyImportantFactorsDataGrid.Size = new Size(onlyImportantFactorsDataGrid.Width, onlyImportantFactorsDataGrid.Height - 19);
            progressBarFillFilteredData.Value = 0;
            progressBarFillFilteredData.Visible = true;
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Fill filtered factors to dataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void FillFilteredFactors(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Check if bgworker has been stopped
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                // Find total number of regressors
                int totalRegressors = 0;
                foreach (var model in Models) {
                    totalRegressors += model.Regressors.Count;
                }

                // Create start parameters for progress bar
                int progress = 0;
                int step = totalRegressors / 100;
                int oneBarInProgress = 1;
                if (totalRegressors < 100) {
                    step = 1;
                    oneBarInProgress = (100 / totalRegressors) + 1;
                }
                int counter = 0;

                foreach (var model in Models) {
                    foreach (var regressor in model.Regressors) {
                        // Find progress
                        if (counter % step == 0) {
                            progress += oneBarInProgress;
                            bgWorker.ReportProgress(progress);
                        }
                        counter++;

                        // Add information about regressor in data grid row
                        string regressant = model.RegressantName;
                        string regressorName = regressor.Key;
                        string functions = "";
                        if (model.ProcessFunctions.Keys.Contains(regressor.Key)) {
                            functions = String.Join(",", model.ProcessFunctions[regressor.Key]);
                        }
                        string corrCoef = Math.Abs(model.CorrelationCoefficient[regressor.Key]).ToString();

                        onlyImportantFactorsDataGrid.Invoke(new Action<List<string>>((row) => onlyImportantFactorsDataGrid.Rows.Add(row.ToArray())),
                            new List<string>() { regressant, regressorName, functions, corrCoef });
                    }
                }

                // Hide progress bar
                progressBarFillFilteredData.Invoke(new Action<bool>((b) => progressBarFillFilteredData.Visible = b), false);

                // Resize dataGrid
                onlyImportantFactorsDataGrid.Invoke(new Action<Size>((size) => onlyImportantFactorsDataGrid.Size = size),
                    new Size(onlyImportantFactorsDataGrid.Width, onlyImportantFactorsDataGrid.Height + 19));

                bgWorker.CancelAsync();
            }
        }

        private void buildEquationsButton_Click(object sender, EventArgs e) {
            RunBackgroundFindEquations();

            ClearControlsStep5();

            // Set models for next step
            AddModelsToList();

            controlSimulationTab.Enabled = true;
        }

        private void RunBackgroundFindEquations() {
            SetDataGVColumnHeaders(new List<string>() { "Регрессант", "Скорректрованный коэффициент детерминации", "Уравнение" },
                equationsDataGrid, true, new List<int>() { 1 });
            buildEquationsButton.Enabled = false;

            // Background worker for building equations
            BackgroundWorker bgWorkerEq = new BackgroundWorker();
            bgWorkerEq.DoWork += new DoWorkEventHandler((sender, e) => BuildEquations(sender, e, bgWorkerEq));
            bgWorkerEq.WorkerSupportsCancellation = true;
            bgWorkerEq.RunWorkerAsync();

            // Background worker for loading label
            BackgroundWorker bgWorkerLoad = new BackgroundWorker();
            bgWorkerLoad.DoWork += new DoWorkEventHandler((sender, e) =>
                ShowLoadingFunctionPreprocessing(sender, e, bgWorkerLoad, bgWorkerEq, labelBuildingLoad, labelBuildingFinish));
            bgWorkerLoad.WorkerSupportsCancellation = true;
            bgWorkerLoad.RunWorkerAsync();
        }

        /// <summary>
        /// Build equation for each model and fill data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void BuildEquations(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                // Build equation for each model
                foreach(var model in Models) {
                    model.BuildEquation();

                    equationsDataGrid.Invoke(new Action<List<string>>((row) => equationsDataGrid.Rows.Add(row.ToArray())),
                        new List<string>() { model.RegressantName, model.DetermCoeff.ToString(), model.Equation});
                }

                bgWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Add all models names (regressants names) to list
        /// </summary>
        private void AddModelsToList() {
            foreach (var model in Models) {
                listAvailabelModels.Items.Add(model.RegressantName);
            }
        }

        private void acceptControlsParametersButton_Click(object sender, EventArgs e) {
            //Func<IEnumerable<double>, (double, double)> defAreafunc = new Func<IEnumerable<double>, (double, double)>
            //    ((values) => Statistics.AutoEmpiricalDefinitionArea(values, 10));
            //SimulationControlForm simulationControlForm = new SimulationControlForm(Models, defAreafunc);

            // Fill selected models
            List<string> selectedModelsNames = listSelectedModels.Items.Cast<String>().ToList();
            List<Model> selectedModels = new List<Model>();
            foreach (var model in Models) {
                if (selectedModelsNames.Contains(model.RegressantName)) {
                    selectedModels.Add(model);
                }
            }

            SimulationControlForm simulationForm = new SimulationControlForm(selectedModels, DetermFuncAreaDefinition());
            simulationForm.Show();
        }

        /// <summary>
        /// Determining a function to find the area of definition
        /// </summary>
        /// <returns>Function for find area definition</returns>
        private Func<IEnumerable<double>, (double, double)> DetermFuncAreaDefinition() {
            double percentDefArea = Convert.ToDouble(percentAreaExpansion.Value);

            if (theoreticalAreaRadio.Checked) {
                return Statistics.TheoreticalDefinitionArea;
            }
            if (equallyBothWaysRadio.Checked) {
                if (theoreticalAreaRadio.Checked) {
                    return (values) => Statistics.EqualEmpiricalDefinitionArea(values, percentDefArea);
                }
                if (symbiosisAreaRadio.Checked) {
                    return (values) => Statistics.EqualSymbiosisDefinitionArea(values, percentDefArea);
                }
            }
            if (autoProportionRadio.Checked) {
                if (theoreticalAreaRadio.Checked) {
                    return (values) => Statistics.AutoEmpiricalDefinitionArea(values, percentDefArea);
                }
                if (symbiosisAreaRadio.Checked) {
                    return (values) => Statistics.AutoSymbiosisDefinitionArea(values, percentDefArea);
                }
            }

            // Default function
            return (values) => Statistics.AutoSymbiosisDefinitionArea(values, 10);
        }

        private void empWayRadio_CheckedChanged(object sender, EventArgs e) {
            if (empWayRadio.Checked) {
                classicWayRadio.Checked = false;
                valueEmpWayCorr.Enabled = true;
            }
            CheckAcceptFilterButtonRule();
        }

        private void classicWayRadio_CheckedChanged(object sender, EventArgs e) {
            if (classicWayRadio.Checked) {
                empWayRadio.Checked = false;
                valueEmpWayCorr.Enabled = false;
            }
            CheckAcceptFilterButtonRule();
        }

        /// <summary>
        /// Check rule for enable accept filter button
        /// </summary>
        private void CheckAcceptFilterButtonRule() {
            acceptFilterFactorsButton.Enabled = classicWayRadio.Checked || empWayRadio.Checked;
        }

        private void toSelectModelsList_Click(object sender, EventArgs e) {
            MoveModelBetweenLists(listAvailabelModels, listSelectedModels);
        }

        private void toAvailableModelsList_Click(object sender, EventArgs e) {
            MoveModelBetweenLists(listSelectedModels, listAvailabelModels);
        }

        private void listSelectedModels_DoubleClick(object sender, EventArgs e) {
            MoveModelBetweenLists(listSelectedModels, listAvailabelModels);
        }

        private void listAvailabelModels_DoubleClick(object sender, EventArgs e) {
            MoveModelBetweenLists(listAvailabelModels, listSelectedModels);
        }

        /// <summary>
        /// Move selected model from one list to another
        /// </summary>
        /// <param name="fromList">The list from which we move the model</param>
        /// <param name="toList">The list to which we move the model</param>
        private void MoveModelBetweenLists(ListBox fromList, ListBox toList) {
            if (fromList.SelectedItems.Count == 1) {
                int selectedIndex = fromList.SelectedIndex;
                toList.Items.Add(fromList.SelectedItem);
                fromList.Items.Remove(fromList.SelectedItem);
                if (fromList.Items.Count > 0) {
                    if (selectedIndex < fromList.Items.Count) {
                        fromList.SelectedIndex = selectedIndex;
                    }
                    else {
                        fromList.SelectedIndex = selectedIndex - 1;
                    }
                }
                CheckAcceptControlParameterButton();
            }
        }

        private void allToSelectModelsList_Click(object sender, EventArgs e) {
            MoveAllItemsBetweenLists(listAvailabelModels, listSelectedModels);
        }

        private void allToAvailableModelsList_Click(object sender, EventArgs e) {
            MoveAllItemsBetweenLists(listSelectedModels, listAvailabelModels);
        }

        /// <summary>
        /// Move all models from one list to another
        /// </summary>
        /// <param name="fromList">The list from which we move the models</param>
        /// <param name="toList">The list to which we move the models</param>
        private void MoveAllItemsBetweenLists(ListBox fromList, ListBox toList) {
            if (fromList.Items.Count > 0) {
                toList.Items.AddRange(fromList.Items);
                fromList.Items.Clear();
                CheckAcceptControlParameterButton();
            }
        }

        private void empDefAreaRadio_CheckedChanged(object sender, EventArgs e) {
            if (empDefAreaRadio.Checked) {
                theoreticalAreaRadio.Checked = false;
                symbiosisAreaRadio.Checked = false;
                percentAreaExpansion.Enabled = true;
                CheckAcceptControlParameterButton();
            }
        }

        private void theoreticalAreaRadio_CheckedChanged(object sender, EventArgs e) {
            if (theoreticalAreaRadio.Checked) {
                empDefAreaRadio.Checked = false;
                symbiosisAreaRadio.Checked = false;
                percentAreaExpansion.Enabled = false;
                equallyBothWaysRadio.Checked = true;
                autoProportionRadio.Checked = false;
                CheckAcceptControlParameterButton();
            }
        }

        private void symbiosisAreaRadio_CheckedChanged(object sender, EventArgs e) {
            if (symbiosisAreaRadio.Checked) {
                empDefAreaRadio.Checked = false;
                theoreticalAreaRadio.Checked = false;
                percentAreaExpansion.Enabled = true;
                CheckAcceptControlParameterButton();
            }
        }

        private void equallyBothWaysRadio_CheckedChanged(object sender, EventArgs e) {
            if (equallyBothWaysRadio.Checked) {
                autoProportionRadio.Checked = false;
                CheckAcceptControlParameterButton();
            }
        }

        private void autoProportionRadio_CheckedChanged(object sender, EventArgs e) {
            if (autoProportionRadio.Checked) {
                equallyBothWaysRadio.Checked = false;
                CheckAcceptControlParameterButton();
            }
        }

        /// <summary>
        /// Check rule for enable accept control parameters button
        /// </summary>
        private void CheckAcceptControlParameterButton() {
            acceptControlsParametersButton.Enabled = (empDefAreaRadio.Checked || theoreticalAreaRadio.Checked ||
                symbiosisAreaRadio.Checked) && (equallyBothWaysRadio.Checked || autoProportionRadio.Checked) &&
                (listSelectedModels.Items.Count > 0);
        }

        private void ValidateKeyPressedOnlyNums(object sender, KeyPressEventArgs e) {
            e.Handled = CheckNumericIntValue(e);
        }

        /// <summary>
        /// Check if predded numeric of backspace
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CheckNumericIntValue(KeyPressEventArgs e) {
            return (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8;
        }

        /// <summary>
        /// Set start parameters for application controls
        /// </summary>
        private void SetStartParameters() {
            LocksAllTabs();
            loadDataTab.Enabled = true;
            helpAllStepsMenu.ToolTipText = StepsInfo.Step1;
            percentAreaExpansion.Maximum = Decimal.MaxValue;
            symbiosisAreaRadio.Checked = true;
            percentAreaExpansion.Value = 10;
            autoProportionRadio.Checked = true;
            toolTipSymbiosis.SetToolTip(symbiosisAreaRadio, StepsInfo.SymbiosisInfo);
            toolTipAutoProportion.SetToolTip(autoProportionRadio, StepsInfo.AutoProportionInfo);
            toolTipPercentAreaExpansion.SetToolTip(percentAreaExpansion, StepsInfo.PercentAreaExpansion);
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
            ClearControlsStep3();
            ClearControlsStep4();
        }

        /// <summary>
        /// Function for clear controls on step 2
        /// </summary>
        private void ClearControlsStep2() {
            ClearDataGV(functionsForProcessingDataGrid);
            doFunctionalProcessButton.Enabled = false;
            labelFuncPreprocess.Visible = false;
            labelPreprocessingFinish.Visible = false;
        }

        /// <summary>
        /// Function for clear controls on step 3
        /// </summary>
        private void ClearControlsStep3() {
            ClearDataGV(onlyImportantFactorsDataGrid, true);
            empWayRadio.Checked = false;
            classicWayRadio.Checked = false;
            valueEmpWayCorr.Value = Convert.ToDecimal(0.15);
            valueEmpWayCorr.Enabled = false;
            acceptFilterFactorsButton.Enabled = false;
            cancelFilterFactorsButton.Enabled = false;
            labelFilterLoad.Visible = false;
            labelFilterFinish.Visible = false;
        }

        /// <summary>
        /// Function for clear controls on step 4
        /// </summary>
        private void ClearControlsStep4() {
            ClearDataGV(equationsDataGrid);
            labelBuildingLoad.Visible = false;
            labelBuildingFinish.Visible = false;
            ClearControlsStep5();
        }

        /// <summary>
        /// Function for clear controls on step 4
        /// </summary>
        private void ClearControlsStep5() {
            listSelectedModels.Items.Clear();
            listAvailabelModels.Items.Clear();
            empDefAreaRadio.Checked = false;
            theoreticalAreaRadio.Checked = false;
            symbiosisAreaRadio.Checked = true;
            percentAreaExpansion.Value = 10;
            percentAreaExpansion.Enabled = true;
            autoProportionRadio.Checked = true;
            acceptControlsParametersButton.Enabled = false;
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
        /// <param name="indexOfSortableColumns">List of indexes of sortable columns</param>
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
        /// Change help information text about step
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
                case 2:
                    helpAllStepsMenu.ToolTipText = StepsInfo.Step3;
                    break;
                case 3:
                    helpAllStepsMenu.ToolTipText = StepsInfo.Step4;
                    break;
                case 4:
                    helpAllStepsMenu.ToolTipText = StepsInfo.Step5;
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
        /// Resize all main form components
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
                        int newWidth = this.Width - 196;
                        int newHeight = this.Height - 67;
                        int widthDiff = newWidth - allTabs.Width;
                        int heightDiff = newHeight - allTabs.Height;
                        allTabs.Invoke(new Action<Size>((size) => allTabs.Size = size), new Size(newWidth, newHeight));

                        // Left Menu
                        regressorsList.Invoke(new Action<Point>((loc) => regressorsList.Location = loc),
                            new Point(regressorsList.Location.X, this.Height / 2 - 33));

                        regressorsList.Invoke(new Action<Size>((size) => regressorsList.Size = size),
                            new Size(regressorsList.Width, this.Height - regressorsList.Location.Y - 50));

                        labelRegressorsList.Invoke(new Action<Point>((loc) => labelRegressorsList.Location = loc),
                            new Point(labelRegressorsList.Location.X, regressorsList.Location.Y - 16));

                        regressantsList.Invoke(new Action<Size>((size) => regressantsList.Size = size),
                            new Size(regressantsList.Width, labelRegressorsList.Location.Y - regressantsList.Location.Y - 22));


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
                            new Point(labelResultDataLoad.Location.X + widthDiff, labelResultDataLoad.Location.Y + heightDiff));


                        // Tab 2
                        functionsForProcessingDataGrid.Invoke(new Action<Size>((size) => functionsForProcessingDataGrid.Size = size),
                           new Size(functionsForProcessingDataGrid.Width + widthDiff, functionsForProcessingDataGrid.Height + heightDiff));

                        doFunctionalProcessButton.Invoke(new Action<Point>((loc) => doFunctionalProcessButton.Location = loc),
                            new Point(doFunctionalProcessButton.Location.X + widthDiff, doFunctionalProcessButton.Location.Y));

                        labelPreprocessingFinish.Invoke(new Action<Point>((loc) => labelPreprocessingFinish.Location = loc),
                            new Point(labelPreprocessingFinish.Location.X + widthDiff, labelPreprocessingFinish.Location.Y + heightDiff));

                        labelFuncPreprocess.Invoke(new Action<Point>((loc) => labelFuncPreprocess.Location = loc),
                            new Point(labelFuncPreprocess.Location.X + widthDiff, labelFuncPreprocess.Location.Y + heightDiff));


                        // Tab 3
                        onlyImportantFactorsDataGrid.Invoke(new Action<Size>((size) => onlyImportantFactorsDataGrid.Size = size),
                           new Size(onlyImportantFactorsDataGrid.Width + widthDiff, onlyImportantFactorsDataGrid.Height + heightDiff));

                        progressBarFillFilteredData.Invoke(new Action<Point>((loc) => progressBarFillFilteredData.Location = loc),
                            new Point(progressBarFillFilteredData.Location.X, progressBarFillFilteredData.Location.Y + heightDiff));

                        progressBarFillFilteredData.Invoke(new Action<Size>((size) => progressBarFillFilteredData.Size = size),
                           new Size(progressBarFillFilteredData.Width + widthDiff, progressBarFillFilteredData.Height));

                        empWayRadio.Invoke(new Action<Point>((loc) => empWayRadio.Location = loc),
                            new Point(empWayRadio.Location.X + widthDiff, empWayRadio.Location.Y));

                        valueEmpWayCorr.Invoke(new Action<Point>((loc) => valueEmpWayCorr.Location = loc),
                            new Point(valueEmpWayCorr.Location.X + widthDiff, valueEmpWayCorr.Location.Y));

                        classicWayRadio.Invoke(new Action<Point>((loc) => classicWayRadio.Location = loc),
                            new Point(classicWayRadio.Location.X + widthDiff, classicWayRadio.Location.Y));

                        acceptFilterFactorsButton.Invoke(new Action<Point>((loc) => acceptFilterFactorsButton.Location = loc),
                            new Point(acceptFilterFactorsButton.Location.X + widthDiff, acceptFilterFactorsButton.Location.Y));

                        cancelFilterFactorsButton.Invoke(new Action<Point>((loc) => cancelFilterFactorsButton.Location = loc),
                            new Point(cancelFilterFactorsButton.Location.X + widthDiff, cancelFilterFactorsButton.Location.Y));

                        labelFilterLoad.Invoke(new Action<Point>((loc) => labelFilterLoad.Location = loc),
                            new Point(labelFilterLoad.Location.X + widthDiff, labelFilterLoad.Location.Y + heightDiff));

                        labelFilterFinish.Invoke(new Action<Point>((loc) => labelFilterFinish.Location = loc),
                            new Point(labelFilterFinish.Location.X + widthDiff, labelFilterFinish.Location.Y + heightDiff));


                        // Tab 4
                        equationsDataGrid.Invoke(new Action<Size>((size) => equationsDataGrid.Size = size),
                           new Size(equationsDataGrid.Width + widthDiff, equationsDataGrid.Height + heightDiff));

                        buildEquationsButton.Invoke(new Action<Point>((loc) => buildEquationsButton.Location = loc),
                            new Point(buildEquationsButton.Location.X + widthDiff, buildEquationsButton.Location.Y));

                        labelBuildingLoad.Invoke(new Action<Point>((loc) => labelBuildingLoad.Location = loc),
                            new Point(labelBuildingLoad.Location.X + widthDiff, labelBuildingLoad.Location.Y + heightDiff));

                        labelBuildingFinish.Invoke(new Action<Point>((loc) => labelBuildingFinish.Location = loc),
                            new Point(labelBuildingFinish.Location.X + widthDiff, labelBuildingFinish.Location.Y + heightDiff));


                        // Tab 5
                        // The height of one element in the list is 13, so for a smooth drawing of the lists
                        // will change their height to a multiple of 13
                        int heightMainTab = controlSimulationTab.Height;
                        int listsHeight = ((heightMainTab - 152 - 17) / 13) * 13 + 17;

                        labelSelectDefAreaParams.Invoke(new Action<Point>((loc) => labelSelectDefAreaParams.Location = loc),
                            new Point(controlSimulationTab.Width / 8 * 5, labelSelectDefAreaParams.Location.Y));

                        groupDefinitionAreaType.Invoke(new Action<Point>((loc) => groupDefinitionAreaType.Location = loc),
                            new Point(labelSelectDefAreaParams.Location.X, groupDefinitionAreaType.Location.Y));

                        groupPercentAreaExpansion.Invoke(new Action<Point>((loc) => groupPercentAreaExpansion.Location = loc),
                            new Point(labelSelectDefAreaParams.Location.X, groupPercentAreaExpansion.Location.Y));

                        groupProportionOfAreaExpansion.Invoke(new Action<Point>((loc) => groupProportionOfAreaExpansion.Location = loc),
                            new Point(labelSelectDefAreaParams.Location.X, groupProportionOfAreaExpansion.Location.Y));

                        acceptControlsParametersButton.Invoke(new Action<Point>((loc) => acceptControlsParametersButton.Location = loc),
                            new Point(labelSelectDefAreaParams.Location.X + 43, acceptControlsParametersButton.Location.Y));

                        toSelectModelsList.Invoke(new Action<Point>((loc) => toSelectModelsList.Location = loc),
                           new Point(controlSimulationTab.Width / 4, toSelectModelsList.Location.Y));

                        toAvailableModelsList.Invoke(new Action<Point>((loc) => toAvailableModelsList.Location = loc),
                           new Point(controlSimulationTab.Width / 4, toAvailableModelsList.Location.Y));

                        allToAvailableModelsList.Invoke(new Action<Point>((loc) => allToAvailableModelsList.Location = loc),
                           new Point(controlSimulationTab.Width / 4, allToAvailableModelsList.Location.Y));

                        allToSelectModelsList.Invoke(new Action<Point>((loc) => allToSelectModelsList.Location = loc),
                           new Point(controlSimulationTab.Width / 4, allToSelectModelsList.Location.Y));

                        listSelectedModels.Invoke(new Action<Size>((size) => listSelectedModels.Size = size),
                            new Size(toSelectModelsList.Location.X - 44, listsHeight));

                        listAvailabelModels.Invoke(new Action<Point>((loc) => listAvailabelModels.Location = loc),
                            new Point(toSelectModelsList.Location.X + 49, listAvailabelModels.Location.Y));

                        labelAvailableModels.Invoke(new Action<Point>((loc) => labelAvailableModels.Location = loc),
                            new Point(listAvailabelModels.Location.X - 3, labelAvailableModels.Location.Y));

                        listAvailabelModels.Invoke(new Action<Size>((size) => listAvailabelModels.Size = size),
                            new Size(listSelectedModels.Width, listsHeight));


                        isResizeNeeded = false;
                    }
                }
            }
        }

        
    }
}
