using Multiple_Linear_Regression.Work_WIth_Files;
using Multiple_Linear_Regression.Work_WIth_Files.Interfaces;
using Multiple_Linear_Regression.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Multiple_Linear_Regression.Work_With_Files;

namespace Multiple_Linear_Regression {
    public partial class MainForm : Form {
        private IFileService fileService;
        private IDialogService dialogService = new DefaultDialogService();

        private BackgroundWorker resizeWorker = new BackgroundWorker();
        private bool isResizeNeeded = false;

        LoadDataStep loadDataStep = new LoadDataStep();
        RegressorsGrouping regressorsGrouping = new RegressorsGrouping();
        FilterRegressors filterRegressors = new FilterRegressors();

        private Dictionary<string, int> RegressantsHeaders { get; set; } = new Dictionary<string, int>();
        private Dictionary<string, int> RegressorsHeaders { get; set; } = new Dictionary<string, int>();
        private Dictionary<string, int> Headers { get; set; } = new Dictionary<string, int>();
        private Dictionary<string, string> RegressorsShortName { get; set; } = new Dictionary<string, string>();
        private Dictionary<string, List<double>> BaseRegressors { get; set; } = new Dictionary<string, List<double>>();
        private Dictionary<string, List<double>> BaseRegressants { get; set; } = new Dictionary<string, List<double>>();
        private List<string> AllNotCombinedRegressorsNamesFromModels { get; set; } = new List<string>();
        private List<string> AllRegressorsNamesFromModels { get; set; } = new List<string>();
        private List<List<double>> AllValues { get; set; } = new List<List<double>>();
        private Dictionary<string, List<Model>> ModelsForRegressants { get; set; }
        private bool IsPredictionTask { get; set; }
        private bool IsControlTask { get; set; }
        private List<Model> BestModels { get; set; }
        private List<Model> Models { get; set; } = new List<Model>();


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

            try {
                if (dialogService.OpenFileDialog() == true) {
                    fileService = Files.GetFileService(dialogService.FilePath);

                    RunBackgroundWorkerLoadFile();
                }
            }
            catch (Exception ex) {
                dialogService.ShowMessage(ex.Message);
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
                List<List<string>> allRows = new List<List<string>>();
                try {
                    allRows = fileService.Open(dialogService.FilePath);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                finally {
                    if (allRows.Count > 0) {

                        // Clear StepLoadData
                        Action clear = () => ClearControlsLoadData();
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
                        factorsData.Invoke(new Action<List<string>>((s) =>
                            OperationsWithControls.SetDataGVColumnHeaders(s, factorsData, false)), allRows[0]);

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
                                // Add row to AllValues
                                AllValues.Add(allRows[i].Select(str => double.Parse(str)).ToList());

                                // Add row to dataGridView
                                factorsData.Invoke(new Action<List<string>>((s) => factorsData.Rows.Add(s.ToArray())),
                                    allRows[i].Select(elem => Math.Round(double.Parse(elem), 2).ToString()).ToList());
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
            AddRegressorsToRegressorsList(RegressorsHeaders.Keys.ToList());
        }

        /// <summary>
        /// Add regressors names and short version of regressors names
        /// </summary>
        private void AddRegressorsToRegressorsList(List<string> regressorsNames) {
            regressorsList.Items.Clear();
            RegressorsShortName.Clear();

            for (int i = 0; i < regressorsNames.Count(); i++) {
                RegressorsShortName[regressorsNames[i]] = $"X{i + 1}";
                regressorsList.Items.Add($"X{i + 1} - {regressorsNames[i]}");
            }
        }

        private void clearSelectedFactorsButton_Click(object sender, EventArgs e) {
            RegressorsShortName.Clear();
            regressorsList.Items.Clear();
            regressantsList.Items.Clear();
            RegressantsHeaders.Clear();
            RegressorsHeaders.Clear();
            labelResultDataLoad.Visible = false;
        }

        private void acceptFactorsButton_Click(object sender, EventArgs e) {
            if (RegressorsHeaders.Count > 0 && RegressantsHeaders.Count > 0 && (radioPredictionTask.Checked || radioControlTask.Checked)) {
                labelResultDataLoad.Visible = false;

                LoadValuesForFactors();

                IsControlTask = radioControlTask.Checked;
                IsPredictionTask = radioPredictionTask.Checked;

                // Create pairwise combinations of factors if it's needed
                if (checkPairwiseCombinations.Checked) {
                    loadDataStep.CreatePairwiseCombinationsOfFactors(BaseRegressors, RegressorsShortName);
                }

                ClearTabsAfterSelectedFactors();

                // Change value factor if prediction task was choosen
                if (radioPredictionTask.Checked) {
                    int valuesCount = BaseRegressants[BaseRegressants.Keys.First()].Count;

                    // Show form for set paramters for prediction task
                    PredictionParametersForm form = new PredictionParametersForm(valuesCount);
                    form.ShowDialog();

                    int numberOfValuesForDelete = form.LagValue * form.NumberObserInOneTimeInterval;
                    int newNumberOfValues = valuesCount - numberOfValuesForDelete;

                    loadDataStep.ShiftFactorValues(BaseRegressors, 0, newNumberOfValues);
                    loadDataStep.ShiftFactorValues(BaseRegressants, numberOfValuesForDelete, newNumberOfValues);

                }

                OperationsWithModels.SetFactorForModels(Models, BaseRegressants, BaseRegressors);

                // Will the automatic calculation be used
                AllDefaultParameters autoCalcForm = new AllDefaultParameters();
                autoCalcForm.ShowDialog();

                if (autoCalcForm.UsingDefaultParameters) {
                    // Background worker for function preprocessing
                    BackgroundWorker bgWorkerFunc = new BackgroundWorker();
                    bgWorkerFunc.DoWork += new DoWorkEventHandler((senderNew, eNew) => RunFullyAutoCalculation(senderNew, eNew, bgWorkerFunc));
                    bgWorkerFunc.WorkerSupportsCancellation = true;
                    bgWorkerFunc.RunWorkerAsync();

                    // Backgound worker for loading label
                    BackgroundWorker bgWorkerLabel = new BackgroundWorker();
                    bgWorkerLabel.DoWork += new DoWorkEventHandler((senderNew, eNew) =>
                        OperationsWithControls.ShowLoadingLogo(senderNew, eNew, bgWorkerLabel,
                                                               bgWorkerFunc, labelFindingBestModel, labelFindingBestModelEnd));
                    bgWorkerLabel.WorkerSupportsCancellation = true;
                    bgWorkerLabel.RunWorkerAsync();
                }
                else {
                    labelFindingBestModelEnd.Visible = false;
                    labelResultDataLoad.Visible = true;
                    processingStatDataTabGusev.Enabled = true;
                    processingStatDataTabOkunev.Enabled = true;
                    removeUnimportantFactorsTab.Enabled = false;
                    buildRegrEquationsTab.Enabled = false;
                    doFunctionalProcessGusevButton.Enabled = true;
                    doFunctionalProcessOkunevButton.Enabled = true;
                    formationOfControlFactorSetsTab.Enabled = true;
                    groupBoxFilterRegressors.Enabled = true;
                    groupBoxGroupedRegressors.Enabled = true;
                }
            }
            else {
                ShowErrorSelectedFactors();
            }
        }

        /// <summary>
        /// Clear all tabs after selected factors
        /// </summary>
        private void ClearTabsAfterSelectedFactors() {
            ClearControlsGroupingFactors();
            ClearControlsProcessDataGusev();
            ClearControlsProcessDataOkunev();
            ClearControlsFilterFactors();
            ClearControlsBuildEquations();
            ClearControlsImitationParameters();
            ClearPredictionTab();
        }

        /// <summary>
        /// Fully automatic calculations for finding best models
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void RunFullyAutoCalculation(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Check if bgworker has been stopped
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                allTabs.Invoke(new Action<bool>((b) => allTabs.Enabled = b), false);

                // Automatic calculation with default parameters
                // Gusev functional preprocess
                Action gusevDG = () => OperationsWithControls.SetDataGVColumnHeaders(new List<string>() { 
                        "Регрессант", "Регрессор", "Функции предобработки", "Модуль коэффициента корреляции" 
                        },
                        functionsForProcessingGusevDataGrid, true, new List<int>() { 3 });

                functionsForProcessingGusevDataGrid.Invoke(gusevDG);
                GusevProcessingAllModels();

                // Header for grouped data grid
                Action groupedDG = () => SetHeaderGroupedDataGrid();
                groupedRegressorsDataGrid.Invoke(groupedDG);

                // Header for important factors data grid
                Action importantFactorsDG = () => SetHeaderImportantFactorsDataGrid();
                onlyImportantFactorsDataGrid.Invoke(importantFactorsDG);

                // Get groups of non-correlated regressors
                GetGroupsRegressors();

                // Filtering of insignificant regressors by empirical way
                filterRegressors.EmpiricalWayToFilterRegressors(Convert.ToDouble(valueEmpWayCorr.Value),
                                                                        ModelsForRegressants.Values.ToList());

                // Print grouped regressors for each regressant
                PrintGroupedRegressors(onlyImportantFactorsDataGrid);

                // Header for data grid with equations
                Action equationsDG = () => SetHeadersForEquationsDataGrid();
                equationsDataGrid.Invoke(equationsDG);

                // Find best model for each regressant
                FindAllBestModels();

                allTabs.Invoke(new Action<bool>((b) => allTabs.Enabled = b), true);
                predictionTab.Invoke(new Action<bool>((b) => predictionTab.Enabled = b), IsPredictionTask);
                controlSimulationTab.Invoke(new Action<bool>((b) => controlSimulationTab.Enabled = b), IsControlTask);

                // Selecting a tab based on the solving task
                if (IsPredictionTask) {
                    allTabs.Invoke(new Action<TabPage>((page) => allTabs.SelectTab(page)), predictionTab);
                }
                else if (IsControlTask) {
                    allTabs.Invoke(new Action<TabPage>((page) => allTabs.SelectTab(page)), controlSimulationTab);
                }

                // Enable used tabs
                processingStatDataTabGusev.Invoke(new Action<bool>((b) => processingStatDataTabGusev.Enabled = b), true);

                formationOfControlFactorSetsTab.Invoke(new Action<bool>((b) => formationOfControlFactorSetsTab.Enabled = b), true);
                groupBoxGroupedRegressors.Invoke(new Action<bool>((b) => groupBoxGroupedRegressors.Enabled = b), false);

                removeUnimportantFactorsTab.Invoke(new Action<bool>((b) => removeUnimportantFactorsTab.Enabled = b), true);
                groupBoxFilterRegressors.Invoke(new Action<bool>((b) => groupBoxFilterRegressors.Enabled = b), false);

                buildRegrEquationsTab.Invoke(new Action<bool>((b) => buildRegrEquationsTab.Enabled = b), true);
                buildEquationsButton.Invoke(new Action<bool>((b) => buildEquationsButton.Enabled = b), false);

                bgWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Load values for selected factors from data grid view
        /// </summary>
        private void LoadValuesForFactors() {
            BaseRegressants.Clear();
            BaseRegressors.Clear();

            // Load regressants
            BaseRegressants = new Dictionary<string, List<double>>(
                loadDataStep.GetFactorsWithValues(RegressantsHeaders, AllValues)
                );

            // Load regressors
            BaseRegressors = new Dictionary<string, List<double>>(
                loadDataStep.GetFactorsWithValues(RegressorsHeaders, AllValues)
                );
        }

        /// <summary>
        /// Show type of error in selected factors step
        /// </summary>
        private void ShowErrorSelectedFactors() {
            if (RegressantsHeaders.Count == 0 && RegressorsHeaders.Count == 0) {
                MessageBox.Show("Вы не выбрали показатели для исследования");
            }
            else if (RegressantsHeaders.Count == 0) {
                MessageBox.Show("Вы не выбрали управляемые факторы для исследования");
            }
            else if (RegressorsHeaders.Count == 0) {
                MessageBox.Show("Вы не выбрали управляющие факторы для исследования");
            }
            else if (!radioControlTask.Checked && !radioPredictionTask.Checked) {
                MessageBox.Show("Вы не выбрали тип решаемой задачи");
            }
        }

        private void groupedRegressorsButton_Click(object sender, EventArgs e) {
            groupedRegressorsButton.Enabled = false;

            ClearTabsForGroupingFactors();

            RunBackgroundGroupingRegressors();

            EnableControlsAfterGroupingFactors();
        }

        /// <summary>
        /// Clear tabs for grouping factors
        /// </summary>
        private void ClearTabsForGroupingFactors() {
            ClearControlsFilterFactors();
            ClearControlsBuildEquations();
        }

        /// <summary>
        /// Enable controls after grouping factors
        /// </summary>
        private void EnableControlsAfterGroupingFactors() {
            removeUnimportantFactorsTab.Enabled = true;
            buildRegrEquationsTab.Enabled = true;
            buildEquationsButton.Enabled = true;
        }

        private void RunBackgroundGroupingRegressors() {
            PrepareGroupImportantDataGrids();

            // Background worker for grouping regressors
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler((sender, e) => GroupingRegressors(sender, e, bgWorker));
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerAsync();

            // Background worker for loading label
            BackgroundWorker bgWorkerLoad = new BackgroundWorker();
            bgWorkerLoad.DoWork += new DoWorkEventHandler((sender, e) =>
                OperationsWithControls.ShowLoadingLogo(sender, e, bgWorkerLoad, bgWorker, labelGroupingRegressors,
                labelGroupingRegressorsEnd));
            bgWorkerLoad.WorkerSupportsCancellation = true;
            bgWorkerLoad.RunWorkerAsync();
        }

        /// <summary>
        /// Prepare headers for grouping and important factors data grid
        /// </summary>
        private void PrepareGroupImportantDataGrids() {
            SetHeaderGroupedDataGrid();
            SetHeaderImportantFactorsDataGrid();
        }

        /// <summary>
        /// Set header for grouped data grid
        /// </summary>
        private void SetHeaderGroupedDataGrid() {
            ClearDataGV(groupedRegressorsDataGrid);

            // Fill grouped regressors table headers
            OperationsWithControls.SetDataGVColumnHeaders(new List<string>() { "Регрессант", "Регрессоры" }, 
                                                    groupedRegressorsDataGrid, true);
        }

        /// <summary>
        /// Set header for important factors data grid
        /// </summary>
        private void SetHeaderImportantFactorsDataGrid() {
            // Fill filtered table headers
            OperationsWithControls.SetDataGVColumnHeaders(new List<string>() { "Регрессант", "Регрессоры" }, 
                                                    onlyImportantFactorsDataGrid, true);
        }

        /// <summary>
        /// Grouping non-correlation regressors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void GroupingRegressors(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Check if bgworker has been stopped
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                GetGroupsRegressors();
                
                bgWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Get all groups of non-correlated regressors for each regressant
        /// </summary>
        private void GetGroupsRegressors() {
            double thresholdCorrCoef = Convert.ToDouble(maxCorrelBtwRegressors.Value);

            // Get all possible combinations
            List<List<string>> combinationsOfRegressors = new List<List<string>>();
            regressorsGrouping.GetRegressorsCombinations(regressorsGrouping.GetCorrelatedRegressors(thresholdCorrCoef, BaseRegressors),
                                                         0, new List<string>(), combinationsOfRegressors);

            if (checkPairwiseCombinations.Checked) {
                regressorsGrouping.AddPairwiseCombinationsOfFactors(combinationsOfRegressors);
            }

            // Fill all possible combinations for each model
            ModelsForRegressants = regressorsGrouping.CreateGroupsOfModels(combinationsOfRegressors, Models);

            // Print regressors from all models for each regressant
            PrintGroupedRegressors(groupedRegressorsDataGrid);
            PrintGroupedRegressors(onlyImportantFactorsDataGrid);

            // Enable accept button for grouping of regressors
            groupedRegressorsButton.Invoke(new Action<bool>((b) => groupedRegressorsButton.Enabled = b), true);
        }

        /// <summary>
        /// For each regressant print all models as regressors in short-form
        /// </summary>
        /// <param name="dataGrid">DataGrid for printing grouped regressors</param>
        private void PrintGroupedRegressors(DataGridView dataGrid) {
            Action action = () => ClearDataGV(dataGrid, true);
            dataGrid.Invoke(action);

            foreach(var item in ModelsForRegressants) {
                foreach(var model in item.Value) {
                    dataGrid.Invoke(new Action<List<string>>((row) => dataGrid.Rows.Add(row.ToArray())),
                        new List<string>() { item.Key, 
                                             String.Join(", ", OperationsWithModels.GetRegressorsShortNamesFromModel(model, RegressorsShortName).ToArray()) });
                }
            }
        }

        private void doFunctionalProcessGusevButton_Click(object sender, EventArgs e) {
            // Show warinig form
            UserWarningForm warningForm = new UserWarningForm(StepsInfo.UserWarningFuncPreprocessing);
            warningForm.ShowDialog();
            if (warningForm.AcceptAction) {
                doFunctionalProcessOkunevButton.Enabled = false;
                RunBackgroundFunctionalProcessGusevData();

                doFunctionalProcessGusevButton.Enabled = false;
            }
        }

        /// <summary>
        /// Run background worker for functional process data by Gusev method
        /// </summary>
        private void RunBackgroundFunctionalProcessGusevData() {
            OperationsWithControls.SetDataGVColumnHeaders(new List<string>() { "Регрессант", "Регрессор", "Функции предобработки", 
                                                                         "Модуль коэффициента корреляции" },
                                                    functionsForProcessingGusevDataGrid, true, new List<int>() { 3 });

            // Background worker for function preprocessing
            BackgroundWorker bgWorkerFunc = new BackgroundWorker();
            bgWorkerFunc.DoWork += new DoWorkEventHandler((sender, e) => FunctionProcessingGusev(sender, e, bgWorkerFunc));
            bgWorkerFunc.WorkerSupportsCancellation = true;
            bgWorkerFunc.RunWorkerAsync();

            // Backgound worker for loading label
            BackgroundWorker bgWorkerLabel = new BackgroundWorker();
            bgWorkerLabel.DoWork += new DoWorkEventHandler((sender, e) =>
                                    OperationsWithControls.ShowLoadingLogo(sender, e, bgWorkerLabel, bgWorkerFunc, 
                                                                           labelFuncPreprocessGusev, labelPreprocessingGusevFinish));
            bgWorkerLabel.WorkerSupportsCancellation = true;
            bgWorkerLabel.RunWorkerAsync();
        }

        /// <summary>
        /// Find the functions that maximize the Pearson coefficient between regressors and regressants factors by Gusev method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void FunctionProcessingGusev(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Check if mainBgWorker has been stopped
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                GusevProcessingAllModels();

                bgWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Perform Gusev functional processing for each model
        /// </summary>
        private void GusevProcessingAllModels() {
            // Find best functions for each regressors for each model
            foreach (var model in Models) {
                model.StartGusevFunctionalPreprocessing();

                // Add functions and correlation coefficients to data grid view
                foreach (var regressor in model.ProcessFunctions) {
                    string regressorName = $"{RegressorsShortName[regressor.Key]} - {regressor.Key}";
                    // Add row of preprocess functions to data grid
                    functionsForProcessingGusevDataGrid.Invoke(new Action<List<string>>((row) => 
                        functionsForProcessingGusevDataGrid.Rows.Add(row.ToArray())),
                        new List<string>() { model.RegressantName, regressorName,
                                String.Join(", ", regressor.Value.ToArray()),
                                Math.Round(Math.Abs(model.CorrelationCoefficient[regressor.Key]), 2).ToString() });
                }
            }
        }

        private void doFunctionalProcessOkunevButton_Click(object sender, EventArgs e) {
            // Show warinig form
            UserWarningForm warningForm = new UserWarningForm(StepsInfo.UserWarningFuncPreprocessing);
            warningForm.ShowDialog();
            if (warningForm.AcceptAction) {
                doFunctionalProcessGusevButton.Enabled = false;
                RunBackgroundFunctionalProcessOkunevData();

                doFunctionalProcessOkunevButton.Enabled = false;
            }
        }

        /// <summary>
        /// Run background worker for functional process data by Okunev method
        /// </summary>
        private void RunBackgroundFunctionalProcessOkunevData() {
            OperationsWithControls.SetDataGVColumnHeaders(new List<string>() { "Регрессант", "Регрессор", "Функции предобработки", 
                                                                        "Модуль коэффициента корреляции" },
                                                    functionsForProcessingOkunevDataGrid, true, new List<int>() { 3 });

            // Background worker for function preprocessing
            BackgroundWorker bgWorkerFunc = new BackgroundWorker();
            bgWorkerFunc.DoWork += new DoWorkEventHandler((sender, e) => FunctionProcessingOkunev(sender, e, bgWorkerFunc));
            bgWorkerFunc.WorkerSupportsCancellation = true;
            bgWorkerFunc.RunWorkerAsync();

            // Backgound worker for loading label
            BackgroundWorker bgWorkerLabel = new BackgroundWorker();
            bgWorkerLabel.DoWork += new DoWorkEventHandler((sender, e) =>
                                        OperationsWithControls.ShowLoadingLogo(sender, e, bgWorkerLabel, bgWorkerFunc, 
                                                                               labelFuncPreprocessOkunev, labelPreprocessingOkunevFinish));
            bgWorkerLabel.WorkerSupportsCancellation = true;
            bgWorkerLabel.RunWorkerAsync();
        }

        /// <summary>
        /// Find the functions that maximize the Pearson coefficient between regressors and regressants factors by Okunev method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void FunctionProcessingOkunev(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Check if mainBgWorker has been stopped
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                // Find best functions for each regressors for each model
                foreach (var model in Models) {
                    model.StartOkunevFunctionalPreprocessing();

                    // Add functions and correlation coefficients to data grid view
                    foreach (var regressor in model.ProcessFunctions) {
                        string regressorName = $"{RegressorsShortName[regressor.Key]} - {regressor.Key}";
                        // Add row of preprocess functions to data grid
                        functionsForProcessingOkunevDataGrid.Invoke(new Action<List<string>>((row) => functionsForProcessingOkunevDataGrid.Rows.Add(row.ToArray())),
                            new List<string>() { model.RegressantName, regressorName,
                                String.Join(", ", regressor.Value.ToArray()),
                                Math.Round(Math.Abs(model.CorrelationCoefficient[regressor.Key]), 2).ToString() });
                    }
                }

                bgWorker.CancelAsync();
            }
        }

        private void acceptFilterFactorsButton_Click(object sender, EventArgs e) {
            ClearDataGV(onlyImportantFactorsDataGrid);

            RunBackgroundFilterRegressors();

            ClearControlsBuildEquations();
            buildEquationsButton.Enabled = true;
        }

        /// <summary>
        /// Filtering regressors for each model for each regressant
        /// </summary>
        private void RunBackgroundFilterRegressors() {
            // Background worker for filtering regressors
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler((sender, e) => FilterRegressors(sender, e, bgWorker));
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerAsync();

            // Background worker for loading label
            BackgroundWorker bgWorkerLoad = new BackgroundWorker();
            bgWorkerLoad.DoWork += new DoWorkEventHandler((sender, e) =>
                OperationsWithControls.ShowLoadingLogo(sender, e, bgWorkerLoad, bgWorker, labelFilterLoad, labelFilterFinish));
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
                        filterRegressors.ClassicWayToFilterRegressors(ModelsForRegressants.Values.ToList());
                    }
                    else if (empWayRadio.Checked) {
                        filterRegressors.EmpiricalWayToFilterRegressors(Convert.ToDouble(valueEmpWayCorr.Value),
                                                                        ModelsForRegressants.Values.ToList());
                    }

                    // Print grouped regressors for each regressant
                    PrintGroupedRegressors(onlyImportantFactorsDataGrid);

                    // Enable cancel filtering button
                    cancelFilterFactorsButton.Invoke(new Action<bool>((b) => cancelFilterFactorsButton.Enabled = b), true);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                bgWorker.CancelAsync();
            }
        }

        private void cancelFilterFactorsButton_Click(object sender, EventArgs e) {
            // Restore non-filter regressors for each model for each regressant
            filterRegressors.CancelFilteringRegressors(ModelsForRegressants.Values.ToList());
            cancelFilterFactorsButton.Enabled = false;

            // Print grouped regressors for each regressant
            PrintGroupedRegressors(onlyImportantFactorsDataGrid);

            ClearControlsBuildEquations();
            buildEquationsButton.Enabled = true;
        }

        private void buildEquationsButton_Click(object sender, EventArgs e) {
            RunBackgroundFindEquations();

            ClearControlsImitationParameters();
            ClearPredictionTab();

            predictionTab.Enabled = IsPredictionTask;
            controlSimulationTab.Enabled = IsControlTask;
        }

        /// <summary>
        /// Find best model for each regressant
        /// </summary>
        private void RunBackgroundFindEquations() {
            SetHeadersForEquationsDataGrid();
            buildEquationsButton.Enabled = false;

            // Background worker for building equations
            BackgroundWorker bgWorkerEq = new BackgroundWorker();
            bgWorkerEq.DoWork += new DoWorkEventHandler((sender, e) => BuildEquations(sender, e, bgWorkerEq));
            bgWorkerEq.WorkerSupportsCancellation = true;
            bgWorkerEq.RunWorkerAsync();

            // Background worker for loading label
            BackgroundWorker bgWorkerLoad = new BackgroundWorker();
            bgWorkerLoad.DoWork += new DoWorkEventHandler((sender, e) =>
                OperationsWithControls.ShowLoadingLogo(sender, e, bgWorkerLoad, bgWorkerEq, labelBuildingLoad, labelBuildingFinish));
            bgWorkerLoad.WorkerSupportsCancellation = true;
            bgWorkerLoad.RunWorkerAsync();
        }

        /// <summary>
        /// Set headers for data grid with equations
        /// </summary>
        private void SetHeadersForEquationsDataGrid() {
            OperationsWithControls.SetDataGVColumnHeaders(new List<string>() { "Регрессант", 
                                                                         "Скорректрованный коэффициент детерминации", "Уравнение" },
                                                    equationsDataGrid, true, new List<int>() { 1 });
        }

        /// <summary>
        /// Build equation for each model and fill data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bgWorker">Background worker</param>
        private void BuildEquations(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker) {
            // Check if bgworker has been stopped
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                FindAllBestModels();

                bgWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Find best model (adequate and significant for each regressant)
        /// </summary>
        private void FindAllBestModels() {
            BestModels = new List<Model>();

            // Build the equation for each model and find the best model for each regressant
            foreach (var regressant in ModelsForRegressants.Keys) {
                ModelsForRegressants[regressant].ForEach(model => model.BuildEquation(RegressorsShortName));

                Model nextBestModel = OperationsWithModels.FindBestModel(ModelsForRegressants[regressant], regressant);

                BestModels.Add(nextBestModel);

                equationsDataGrid.Invoke(new Action<List<string>>((row) => equationsDataGrid.Rows.Add(row.ToArray())),
                    new List<string>() { regressant, Math.Round(nextBestModel.AdjDetermCoeff, 2).ToString(), nextBestModel.Equation });
            }

            if (IsControlTask) {
                // Add model for the imitation step
                listAvailabelModels.Invoke(new Action<List<string>>((regrName) => listAvailabelModels.Items.AddRange(regrName.ToArray())),
                    ModelsForRegressants.Keys.ToList());
            }

            if (IsPredictionTask) {
                // Fill prediction tab
                FillPredictionTab();
            }
        }

        /// <summary>
        /// Fill tab with predict values for prediction factors
        /// </summary>
        private void FillPredictionTab() {
            AllRegressorsNamesFromModels = OperationsWithModels.GetAllRegressorsFromModels(BestModels);
            AllNotCombinedRegressorsNamesFromModels = OperationsWithModels.GetNonCombinedRegressors(AllRegressorsNamesFromModels);

            // Fill all regressors from models with values 
            Dictionary<string, List<double>> allRegressors = new Dictionary<string, List<double>>();
            foreach (var regressorName in AllNotCombinedRegressorsNamesFromModels) {
                allRegressors[regressorName] = new List<double>(BaseRegressors[regressorName]);
            }

            // Set headers for data grid views in predict tab
            SetHeadersInPredictTab(AllNotCombinedRegressorsNamesFromModels);

            // Fill values for data grid view in predict tab
            FillValuesInPredictTab(allRegressors);

            loadDataForPredictButton.Invoke(new Action<bool>((enab) => loadDataForPredictButton.Enabled = enab), true);
        }

        /// <summary>
        /// Set headers for data grid views in predict tab
        /// </summary>
        /// <param name="regressorsNames">List of regressors names</param>
        private void SetHeadersInPredictTab(List<string> regressorsNames) {
            List<string> headers = new List<string>(regressorsNames);

            // Get all names of predicting factors
            foreach (var model in BestModels) {
                headers.Add($"{model.RegressantName} (Реальное)");
                headers.Add($"{model.RegressantName} (Предсказанное)");
                headers.Add("Прогнозная ошибка (%)");
                headers.Add("Прогнозная ошибка с размахом (%)");
                headers.Add("Абсолютная ошибка");
                headers.Add("Прогнозная ошибка с максимумом (%)");
                headers.Add("Прогнозная ошибка с минимумом (%)");
            }

            Action setHeadersRealPredict = () => OperationsWithControls.SetDataGVColumnHeaders(headers, realPredictValuesDataGrid, false);
            Action setHeadersPredictionMetrics = () => OperationsWithControls.SetDataGVColumnHeaders(new List<string>() { 
                "Управляющий показатель", 
                "Среднее значение прогнозной ошибки (%)",
                "Среднее значение прогнозной ошибки с размахом (%)",
                "Среднее значение абсолютной ошибки",
                "Среднее значение прогнозной ошибки с максимумом (%)",
                "Среднее значение прогнозной ошибки с минимумом (%)"
            }, predictionMetricsDataGrid, true);

            realPredictValuesDataGrid.Invoke(setHeadersRealPredict);
            predictionMetricsDataGrid.Invoke(setHeadersPredictionMetrics);
        }

        /// <summary>
        /// Calc predict values for each model and fill data grid view in predict tab
        /// </summary>
        /// <param name="regressorsValues">Dict with values for regressors</param>
        private void FillValuesInPredictTab(Dictionary<string, List<double>> regressorsValues) {
            Dictionary<string, Dictionary<string, List<double>>> modelsPredictionErrors = new Dictionary<string, Dictionary<string, List<double>>>();
            List<List<string>> allRows = OperationsWithModels.GetErrorsOfModelsForPrediction(BestModels, regressorsValues, modelsPredictionErrors);

            foreach (var nextRow in allRows) {
                realPredictValuesDataGrid.Invoke(new Action<List<string>>((row) => realPredictValuesDataGrid.Rows.Add(row.ToArray())),
                    nextRow);
            }

            // Fill prediction metric data grid view
            foreach (var regressantErrors in modelsPredictionErrors) {
                predictionMetricsDataGrid.Invoke(new Action<List<string>>((row) => predictionMetricsDataGrid.Rows.Add(row.ToArray())),
                    new List<string>() { 
                        regressantErrors.Key,
                        Math.Round(regressantErrors.Value["MAPE"].Average(), 2).ToString(),
                        Math.Round(regressantErrors.Value["MinMax"].Average(), 2).ToString(),
                        Math.Round(regressantErrors.Value["AbsError"].Average(), 2).ToString(),
                        Math.Round(regressantErrors.Value["MaxError"].Average(), 2).ToString(),
                        Math.Round(regressantErrors.Value["MinError"].Average(), 2).ToString()
                    });
            }
        }

        private void acceptControlsParametersButton_Click(object sender, EventArgs e) {
            // Fill selected models
            List<string> selectedModelsNames = listSelectedModels.Items.Cast<String>().ToList();
            List<Model> selectedModels = new List<Model>();
            foreach (var model in BestModels) {
                if (selectedModelsNames.Contains(model.RegressantName)) {
                    selectedModels.Add(model);
                }
            }

            // Get number group of correlated regressors
            int numberGroupRegressors = 0;
            if (manualNumberCorrIntervalRadio.Checked) {
                numberGroupRegressors = (int)numberOfCorrIntervalsManual.Value;
            }

            SimulationControlForm simulationForm = new SimulationControlForm(selectedModels, DetermFuncAreaDefinition(), 
                numberGroupRegressors);
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
                if (empDefAreaRadio.Checked) {
                    return (values) => Statistics.EqualEmpiricalDefinitionArea(values, percentDefArea);
                }
                if (symbiosisAreaRadio.Checked) {
                    return (values) => Statistics.EqualSymbiosisDefinitionArea(values, percentDefArea);
                }
            }
            if (autoProportionRadio.Checked) {
                if (empDefAreaRadio.Checked) {
                    return (values) => Statistics.AutoEmpiricalDefinitionArea(values, percentDefArea);
                }
                if (symbiosisAreaRadio.Checked) {
                    return (values) => Statistics.AutoSymbiosisDefinitionArea(values, percentDefArea);
                }
            }

            // Default function
            return (values) => Statistics.AutoSymbiosisDefinitionArea(values, 10);
        }

        private void loadDataForPredictButton_Click(object sender, EventArgs e) {
            try {
                if (dialogService.OpenFileDialog() == true) {
                    fileService = Files.GetFileService(dialogService.FilePath);
                    List<List<string>> allRows = fileService.Open(dialogService.FilePath);

                    // Show predicted regressants with regressors values
                    FileRegressors fileRegressorsForm = new FileRegressors(AllRegressorsNamesFromModels, BestModels,
                        allRows, "Прогнозирование", StepsInfo.PredictRegressorsFromFile);
                    fileRegressorsForm.ShowDialog();
                }
            }
            catch (Exception ex) {
                dialogService.ShowMessage(ex.Message);
            }
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
            MoveItemBetweenLists(listAvailabelModels, listSelectedModels);
        }

        private void toAvailableModelsList_Click(object sender, EventArgs e) {
            MoveItemBetweenLists(listSelectedModels, listAvailabelModels);
        }

        private void listSelectedModels_DoubleClick(object sender, EventArgs e) {
            MoveItemBetweenLists(listSelectedModels, listAvailabelModels);
        }

        private void listAvailabelModels_DoubleClick(object sender, EventArgs e) {
            MoveItemBetweenLists(listAvailabelModels, listSelectedModels);
        }

        /// <summary>
        /// Move selected model from one list to another
        /// </summary>
        /// <param name="fromList"></param>
        /// <param name="toList"></param>
        private void MoveItemBetweenLists(ListBox fromList, ListBox toList) {
            OperationsWithControls.MoveModelBetweenLists(fromList, toList);
            CheckAcceptControlParameterButton();
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
                OperationsWithControls.MoveAllItemsBetweenLists(fromList, toList);
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

        private void autoNumberCorrIntervalsRadio_CheckedChanged(object sender, EventArgs e) {
            if (autoNumberCorrIntervalsRadio.Checked) {
                manualNumberCorrIntervalRadio.Checked = false;
                numberOfCorrIntervalsManual.Enabled = false;
                CheckAcceptControlParameterButton();
            }
        }

        private void manualNumberCorrIntervalRadio_CheckedChanged(object sender, EventArgs e) {
            if (manualNumberCorrIntervalRadio.Checked) {
                autoNumberCorrIntervalsRadio.Checked = false;
                numberOfCorrIntervalsManual.Enabled = true;
                CheckAcceptControlParameterButton();
            }
        }

        /// <summary>
        /// Check rule for enable accept control parameters button
        /// </summary>
        private void CheckAcceptControlParameterButton() {
            acceptControlsParametersButton.Enabled = (empDefAreaRadio.Checked || theoreticalAreaRadio.Checked ||
                symbiosisAreaRadio.Checked) && (equallyBothWaysRadio.Checked || autoProportionRadio.Checked) &&
                (autoNumberCorrIntervalsRadio.Checked || manualNumberCorrIntervalRadio.Checked) && 
                (listSelectedModels.Items.Count > 0);
        }

        private void radioControlTask_CheckedChanged(object sender, EventArgs e) {
            if (radioControlTask.Checked) {
                radioPredictionTask.Checked = false;
            }
        }

        private void radioPredictionTask_CheckedChanged(object sender, EventArgs e) {
            if (radioPredictionTask.Checked) {
                radioControlTask.Checked = false;
            }
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
            helpAllStepsMenu.ToolTipText = StepsInfo.StepLoadData;
            percentAreaExpansion.Maximum = Decimal.MaxValue;
            symbiosisAreaRadio.Checked = true;
            percentAreaExpansion.Value = 10;
            autoProportionRadio.Checked = true;
            numberOfCorrIntervalsManual.Value = 3;
            autoNumberCorrIntervalsRadio.Checked = true;
            numberOfCorrIntervalsManual.Maximum = Decimal.MaxValue;
            toolTipSymbiosis.SetToolTip(symbiosisAreaRadio, StepsInfo.SymbiosisInfo);
            toolTipAutoProportion.SetToolTip(autoProportionRadio, StepsInfo.AutoProportionInfo);
            toolTipPercentAreaExpansion.SetToolTip(percentAreaExpansion, StepsInfo.PercentAreaExpansion);
        }

        /// <summary>
        /// Function for clear controls start with Load Data tab
        /// </summary>
        private void ClearControlsLoadData() {
            ClearDataGV(factorsData);
            selectRegressantsButton.Enabled = false;
            selectRegressorsButton.Enabled = false;
            acceptFactorsButton.Enabled = false;
            clearSelectedFactorsButton.Enabled = false;
            regressantsList.Items.Clear();
            regressorsList.Items.Clear();
            labelResultDataLoad.Visible = false;
            labelFindingBestModel.Visible = false;
            labelFindingBestModelEnd.Visible = false;
            radioControlTask.Checked = true;
            radioPredictionTask.Checked = false;

            ClearControlsGroupingFactors();
            ClearControlsProcessDataGusev();
            ClearControlsProcessDataOkunev();
            ClearControlsFilterFactors();
            ClearControlsBuildEquations();
        }

        /// <summary>
        /// Function for clear controls on grouping regressors tab
        /// </summary>
        private void ClearControlsGroupingFactors() {
            ClearDataGV(groupedRegressorsDataGrid);
            maxCorrelBtwRegressors.Value = Convert.ToDecimal(0.7);
            maxCorrelBtwRegressors.Enabled = true;
            labelGroupingRegressors.Visible = false;
            labelGroupingRegressorsEnd.Visible = false;
        }

        /// <summary>
        /// Function for clear controls on process data tab
        /// </summary>
        private void ClearControlsProcessDataGusev() {
            ClearDataGV(functionsForProcessingGusevDataGrid);
            doFunctionalProcessGusevButton.Enabled = false;
            labelFuncPreprocessGusev.Visible = false;
            labelPreprocessingGusevFinish.Visible = false;
        }

        /// <summary>
        /// Function for clear controls on process data tab
        /// </summary>
        private void ClearControlsProcessDataOkunev() {
            ClearDataGV(functionsForProcessingOkunevDataGrid);
            doFunctionalProcessOkunevButton.Enabled = false;
            labelFuncPreprocessOkunev.Visible = false;
            labelPreprocessingOkunevFinish.Visible = false;
        }

        /// <summary>
        /// Function for clear controls on filter data tab
        /// </summary>
        private void ClearControlsFilterFactors() {
            ClearDataGV(onlyImportantFactorsDataGrid, true);
            empWayRadio.Checked = false;
            classicWayRadio.Checked = false;
            empWayRadio.Enabled = true;
            classicWayRadio.Enabled = true;
            valueEmpWayCorr.Value = Convert.ToDecimal(0.1);
            valueEmpWayCorr.Enabled = false;
            labelFilterLoad.Visible = false;
            labelFilterFinish.Visible = false;
            acceptFilterFactorsButton.Enabled = false;
            cancelFilterFactorsButton.Enabled = false;
        }

        /// <summary>
        /// Function for clear controls on build equations tab
        /// </summary>
        private void ClearControlsBuildEquations() {
            ClearDataGV(equationsDataGrid);
            labelBuildingLoad.Visible = false;
            labelBuildingFinish.Visible = false;
            ClearControlsImitationParameters();
        }

        /// <summary>
        /// Function for clear controls on filter factors tab
        /// </summary>
        private void ClearControlsImitationParameters() {
            listSelectedModels.Items.Clear();
            listAvailabelModels.Items.Clear();
            empDefAreaRadio.Checked = false;
            theoreticalAreaRadio.Checked = false;
            symbiosisAreaRadio.Checked = true;
            percentAreaExpansion.Value = 10;
            percentAreaExpansion.Enabled = true;
            autoProportionRadio.Checked = true;
            autoNumberCorrIntervalsRadio.Checked = true;
            manualNumberCorrIntervalRadio.Checked = false;
            numberOfCorrIntervalsManual.Value = 3;
            acceptControlsParametersButton.Enabled = false;
        }

        /// <summary>
        /// Function for clear controls on prediction tab
        /// </summary>
        private void ClearPredictionTab() {
            ClearDataGV(realPredictValuesDataGrid);
            ClearDataGV(predictionMetricsDataGrid);
            loadDataForPredictButton.Enabled = false;
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
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepLoadData;
                    break;
                case 1:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepProcessFactorsGusev;
                    break;
                case 2:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepProcessFactorsOkunev;
                    break;
                case 3:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepFormationRegressorsGroup;
                    break;
                case 4:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepFilterFactors;
                    break;
                case 5:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepBuildEquations;
                    break;
                case 6:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepSetImitationParameters;
                    break;
                case 7:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepPredictValues;
                    break;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e) {
            isResizeNeeded = true;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e) {
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


                        // Tab load data
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

                        labelFindingBestModel.Invoke(new Action<Point>((loc) => labelFindingBestModel.Location = loc),
                            new Point(labelFindingBestModel.Location.X + widthDiff, labelFindingBestModel.Location.Y + heightDiff));

                        labelFindingBestModelEnd.Invoke(new Action<Point>((loc) => labelFindingBestModelEnd.Location = loc),
                            new Point(labelFindingBestModelEnd.Location.X + widthDiff, labelFindingBestModelEnd.Location.Y + heightDiff));

                        groupTaskType.Invoke(new Action<Point>((loc) => groupTaskType.Location = loc),
                            new Point(groupTaskType.Location.X + widthDiff, groupTaskType.Location.Y));


                        // Tab formation group of regressors
                        groupedRegressorsDataGrid.Invoke(new Action<Size>((size) => groupedRegressorsDataGrid.Size = size),
                           new Size(groupedRegressorsDataGrid.Width + widthDiff, groupedRegressorsDataGrid.Height + heightDiff));

                        groupBoxGroupedRegressors.Invoke(new Action<Point>((loc) => groupBoxGroupedRegressors.Location = loc),
                            new Point(groupBoxGroupedRegressors.Location.X + widthDiff, groupBoxGroupedRegressors.Location.Y));

                        labelGroupingRegressors.Invoke(new Action<Point>((loc) => labelGroupingRegressors.Location = loc),
                            new Point(labelGroupingRegressors.Location.X + widthDiff, labelGroupingRegressors.Location.Y + heightDiff));

                        labelGroupingRegressorsEnd.Invoke(new Action<Point>((loc) => labelGroupingRegressorsEnd.Location = loc),
                            new Point(labelGroupingRegressorsEnd.Location.X + widthDiff, labelGroupingRegressorsEnd.Location.Y + heightDiff));


                        // Tab process data Gusev
                        functionsForProcessingGusevDataGrid.Invoke(new Action<Size>((size) => functionsForProcessingGusevDataGrid.Size = size),
                           new Size(functionsForProcessingGusevDataGrid.Width + widthDiff, functionsForProcessingGusevDataGrid.Height + heightDiff));

                        doFunctionalProcessGusevButton.Invoke(new Action<Point>((loc) => doFunctionalProcessGusevButton.Location = loc),
                            new Point(doFunctionalProcessGusevButton.Location.X + widthDiff, doFunctionalProcessGusevButton.Location.Y));

                        labelPreprocessingGusevFinish.Invoke(new Action<Point>((loc) => labelPreprocessingGusevFinish.Location = loc),
                            new Point(labelPreprocessingGusevFinish.Location.X + widthDiff, labelPreprocessingGusevFinish.Location.Y + heightDiff));

                        labelFuncPreprocessGusev.Invoke(new Action<Point>((loc) => labelFuncPreprocessGusev.Location = loc),
                            new Point(labelFuncPreprocessGusev.Location.X + widthDiff, labelFuncPreprocessGusev.Location.Y + heightDiff));


                        // Tab process data Okunev
                        functionsForProcessingOkunevDataGrid.Invoke(new Action<Size>((size) => functionsForProcessingOkunevDataGrid.Size = size),
                           new Size(functionsForProcessingOkunevDataGrid.Width + widthDiff, functionsForProcessingOkunevDataGrid.Height + heightDiff));

                        doFunctionalProcessOkunevButton.Invoke(new Action<Point>((loc) => doFunctionalProcessOkunevButton.Location = loc),
                            new Point(doFunctionalProcessOkunevButton.Location.X + widthDiff, doFunctionalProcessOkunevButton.Location.Y));

                        labelPreprocessingOkunevFinish.Invoke(new Action<Point>((loc) => labelPreprocessingOkunevFinish.Location = loc),
                            new Point(labelPreprocessingOkunevFinish.Location.X + widthDiff, labelPreprocessingOkunevFinish.Location.Y + heightDiff));

                        labelFuncPreprocessOkunev.Invoke(new Action<Point>((loc) => labelFuncPreprocessOkunev.Location = loc),
                            new Point(labelFuncPreprocessOkunev.Location.X + widthDiff, labelFuncPreprocessOkunev.Location.Y + heightDiff));


                        // Tab filter data
                        onlyImportantFactorsDataGrid.Invoke(new Action<Size>((size) => onlyImportantFactorsDataGrid.Size = size),
                           new Size(onlyImportantFactorsDataGrid.Width + widthDiff, onlyImportantFactorsDataGrid.Height + heightDiff));

                        groupBoxFilterRegressors.Invoke(new Action<Point>((loc) => groupBoxFilterRegressors.Location = loc),
                            new Point(groupBoxFilterRegressors.Location.X + widthDiff, groupBoxFilterRegressors.Location.Y));

                        labelFilterLoad.Invoke(new Action<Point>((loc) => labelFilterLoad.Location = loc),
                            new Point(labelFilterLoad.Location.X + widthDiff, labelFilterLoad.Location.Y + heightDiff));

                        labelFilterFinish.Invoke(new Action<Point>((loc) => labelFilterFinish.Location = loc),
                            new Point(labelFilterFinish.Location.X + widthDiff, labelFilterFinish.Location.Y + heightDiff));


                        // Tab buid equations
                        equationsDataGrid.Invoke(new Action<Size>((size) => equationsDataGrid.Size = size),
                           new Size(equationsDataGrid.Width + widthDiff, equationsDataGrid.Height + heightDiff));

                        buildEquationsButton.Invoke(new Action<Point>((loc) => buildEquationsButton.Location = loc),
                            new Point(buildEquationsButton.Location.X + widthDiff, buildEquationsButton.Location.Y));

                        labelBuildingLoad.Invoke(new Action<Point>((loc) => labelBuildingLoad.Location = loc),
                            new Point(labelBuildingLoad.Location.X + widthDiff, labelBuildingLoad.Location.Y + heightDiff));

                        labelBuildingFinish.Invoke(new Action<Point>((loc) => labelBuildingFinish.Location = loc),
                            new Point(labelBuildingFinish.Location.X + widthDiff, labelBuildingFinish.Location.Y + heightDiff));


                        // Tab select parameters for imitation control
                        // The height of one element in the list is 13, so for a smooth drawing of the lists
                        // will change their height to a multiple of 13
                        int heightMainTab = allTabs.Height - 26;
                        int widthMainTab = allTabs.Width - 8;
                        int listsHeight = ((heightMainTab - 74 - 17) / 13) * 13 + 17;

                        labelSelectDefAreaParams.Invoke(new Action<Point>((loc) => labelSelectDefAreaParams.Location = loc),
                            new Point(widthMainTab / 8 * 5 + 20, labelSelectDefAreaParams.Location.Y));

                        groupDefinitionAreaType.Invoke(new Action<Point>((loc) => groupDefinitionAreaType.Location = loc),
                            new Point(labelSelectDefAreaParams.Location.X, groupDefinitionAreaType.Location.Y));

                        groupPercentAreaExpansion.Invoke(new Action<Point>((loc) => groupPercentAreaExpansion.Location = loc),
                            new Point(labelSelectDefAreaParams.Location.X, groupPercentAreaExpansion.Location.Y));

                        groupProportionOfAreaExpansion.Invoke(new Action<Point>((loc) => groupProportionOfAreaExpansion.Location = loc),
                            new Point(labelSelectDefAreaParams.Location.X, groupProportionOfAreaExpansion.Location.Y));

                        groupNumberCorrelatedIntervals.Invoke(new Action<Point>((loc) => groupNumberCorrelatedIntervals.Location = loc),
                            new Point(labelSelectDefAreaParams.Location.X, groupNumberCorrelatedIntervals.Location.Y));

                        toSelectModelsList.Invoke(new Action<Point>((loc) => toSelectModelsList.Location = loc),
                           new Point(widthMainTab / 4, toSelectModelsList.Location.Y));

                        toAvailableModelsList.Invoke(new Action<Point>((loc) => toAvailableModelsList.Location = loc),
                           new Point(widthMainTab / 4, toAvailableModelsList.Location.Y));

                        allToAvailableModelsList.Invoke(new Action<Point>((loc) => allToAvailableModelsList.Location = loc),
                           new Point(widthMainTab / 4, allToAvailableModelsList.Location.Y));

                        allToSelectModelsList.Invoke(new Action<Point>((loc) => allToSelectModelsList.Location = loc),
                           new Point(widthMainTab / 4, allToSelectModelsList.Location.Y));

                        listSelectedModels.Invoke(new Action<Size>((size) => listSelectedModels.Size = size),
                            new Size(toSelectModelsList.Location.X - 44, listsHeight));

                        listAvailabelModels.Invoke(new Action<Point>((loc) => listAvailabelModels.Location = loc),
                            new Point(toSelectModelsList.Location.X + 49, listAvailabelModels.Location.Y));

                        labelAvailableModels.Invoke(new Action<Point>((loc) => labelAvailableModels.Location = loc),
                            new Point(listAvailabelModels.Location.X - 3, labelAvailableModels.Location.Y));

                        listAvailabelModels.Invoke(new Action<Size>((size) => listAvailabelModels.Size = size),
                            new Size(listSelectedModels.Width, listsHeight));

                        int acceptButtonSpace = (groupProportionOfAreaExpansion.Location.X - (listAvailabelModels.Location.X
                            + listAvailabelModels.Width)) / 2 - acceptControlsParametersButton.Width / 2;

                        acceptControlsParametersButton.Invoke(new Action<Point>((loc) => acceptControlsParametersButton.Location = loc),
                            new Point(listAvailabelModels.Location.X + listAvailabelModels.Width + acceptButtonSpace,
                            acceptControlsParametersButton.Location.Y));


                        // Prediction tab
                        realPredictValuesDataGrid.Invoke(new Action<Size>((size) => realPredictValuesDataGrid.Size = size),
                           new Size(realPredictValuesDataGrid.Width + widthDiff, realPredictValuesDataGrid.Height + heightDiff));

                        predictionMetricsDataGrid.Invoke(new Action<Size>((size) => predictionMetricsDataGrid.Size = size),
                           new Size(predictionMetricsDataGrid.Width + widthDiff, predictionMetricsDataGrid.Height));

                        predictionMetricsDataGrid.Invoke(new Action<Point>((loc) => predictionMetricsDataGrid.Location = loc),
                            new Point(predictionMetricsDataGrid.Location.X, predictionMetricsDataGrid.Location.Y + heightDiff));

                        loadDataForPredictButton.Invoke(new Action<Point>((loc) => loadDataForPredictButton.Location = loc),
                            new Point(loadDataForPredictButton.Location.X + widthDiff, loadDataForPredictButton.Location.Y));


                        isResizeNeeded = false;
                    }
                }
            }
        }
    }
}
