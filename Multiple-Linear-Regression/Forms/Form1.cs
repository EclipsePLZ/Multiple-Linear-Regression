﻿using Multiple_Linear_Regression.Work_WIth_Files;
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
        private Dictionary<string, string> RegressorsShortName { get; set; } = new Dictionary<string, string>();
        private Dictionary<string, List<double>> BaseRegressors { get; set; } = new Dictionary<string, List<double>>();
        private Dictionary<string, List<Model>> ModelsForRegressants { get; set; }

        private List<Model> Models { get; set; } = new List<Model>();

        private double[,] X { get; set; }
        private double[] Y { get; set; }


        public MainForm() {
            InitializeComponent();

            // Centered Main From on the screen
            this.CenterToScreen();

            SetStartParameters();

            Chart c1 = new Chart();
            Console.WriteLine(c1.DataManipulator.Statistics.InverseFDistribution(.05, 2, 17));

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
                    fileService = GetFileService(dialogService.FilePath);

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
                List<List<string>> allRows = fileService.Open(dialogService.FilePath);
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
            if (RegressorsHeaders.Count > 0 && RegressantsHeaders.Count > 0) {
                LoadValuesForFactors();

                // Create pairwise combinations of factors if it's needed
                if (checkPairwiseCombinations.Checked) {
                    CreatePairwiseCombinationsOfFactors();
                }

                ClearControlsGroupingFactors();
                ClearControlsProcessData();
                ClearControlsFilterFactors();
                ClearControlsBuildEquations();

                FillRegressorsForModels();

                labelResultDataLoad.Visible = true;
                processingStatDataTab.Enabled = true;
                formationOfControlFactorSetsTab.Enabled = false;                
                removeUnimportantFactorsTab.Enabled = false;
                buildRegrEquationsTab.Enabled = false;
                doFunctionalProcessButton.Enabled = true;
                formationOfControlFactorSetsTab.Enabled = true;
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
                    string combinedName = RegressorsKeys[i] + " & " + RegressorsKeys[j];
                    RegressorsShortName[combinedName] = $"{RegressorsShortName[RegressorsKeys[i]]}*{RegressorsShortName[RegressorsKeys[j]]}";
                    BaseRegressors[combinedName] = newRegressorFactorValues;
                }
            }
        }

        /// <summary>
        /// Set regressors and regressors names for each model
        /// </summary>
        private void FillRegressorsForModels() {
            Models.ForEach(model => model.SetNewRegressors(BaseRegressors));
        }

        private void groupedRegressorsButton_Click(object sender, EventArgs e) {
            groupedRegressorsButton.Enabled = false;
            ClearControlsGroupingFactors();
            RunBackgroundGroupingRegressors();

            removeUnimportantFactorsTab.Enabled = true;
            buildRegrEquationsTab.Enabled = true;

            buildEquationsButton.Enabled = true;
        }

        ///// <summary>
        ///// Cancel grouping of regressors
        ///// </summary>
        //private void CancelGroupingRegressors() {
        //    ClearControlsGroupingFactors();

        //    List<Model> startModels = new List<Model>();

        //    foreach (var model in Models) {
        //        startModels.Add(new Model(model.RegressantName, model.RegressantValues, BaseRegressors));
        //    }

        //    Models = new List<Model>(startModels);
        //}

        private void RunBackgroundGroupingRegressors() {
            // Fill grouped regressors table headers
            SetDataGVColumnHeaders(new List<string>() { "Регрессант", "Регрессоры" }, groupedRegressorsDataGrid, true);

            // Fill filtered table headers
            SetDataGVColumnHeaders(new List<string>() { "Регрессант", "Регрессоры" }, onlyImportantFactorsDataGrid, true);

            // Background worker for grouping regressors
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler((sender, e) => GroupingRegressors(sender, e, bgWorker));
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerAsync();

            // Background worker for loading label
            BackgroundWorker bgWorkerLoad = new BackgroundWorker();
            bgWorkerLoad.DoWork += new DoWorkEventHandler((sender, e) =>
                ShowLoadingFunctionPreprocessing(sender, e, bgWorkerLoad, bgWorker, labelGroupingRegressors,
                labelGroupingRegressorsEnd));
            bgWorkerLoad.WorkerSupportsCancellation = true;
            bgWorkerLoad.RunWorkerAsync();
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
                double thresholdCorrCoef = Convert.ToDouble(maxCorrelBtwRegressors.Value);

                // Get all possible combinations
                List<List<string>> combinationsOfRegressors = new List<List<string>>();
                GetCombinations(GetCorrelatedRegressors(thresholdCorrCoef), 0, new List<string>(), combinationsOfRegressors);
                if (checkPairwiseCombinations.Checked) {
                    AddPairwiseCombinationsOfFactors(combinationsOfRegressors);
                }

                //// Get all combinations of regressors with values
                //List<Dictionary<string, List<double>>> combinationOfRegressorsValues = GetCombinationRegressorsWithValues(
                //    combinationsOfRegressors);

                // Fill all possible combinations for each model
                ModelsForRegressants = CreateGroupsOfModels(combinationsOfRegressors);

                // Print regressors from all models for each regressant
                PrintGroupedRegressors(groupedRegressorsDataGrid);
                PrintGroupedRegressors(onlyImportantFactorsDataGrid);

                // Enable accept button for grouping of regressors
                groupedRegressorsButton.Invoke(new Action<bool>((b) => groupedRegressorsButton.Enabled = b), true);

                bgWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Get all combinations of regressors
        /// </summary>
        /// <param name="groupedRegressors">List of group of regressors</param>
        /// <param name="index">Index of group</param>
        /// <param name="state">List for keeping combination</param>
        /// <param name="result">Result that contains all possible combinations</param>
        private void GetCombinations(List<List<string>> groupedRegressors, int index, List<string> state,
            List<List<string>> result) {

            if (index >= groupedRegressors.Count) {
                result.Add(new List<string>(state));
                return;
            }
            foreach(var item in groupedRegressors[index]) {
                state.Add(item);
                GetCombinations(groupedRegressors, index + 1, state, result);
                state.RemoveAt(state.Count - 1);
            }
        }

        /// <summary>
        /// Find correlated regressors
        /// </summary>
        /// <param name="thresholdCorr">Threshold value for check correlation</param>
        /// <returns></returns>
        private List<List<string>> GetCorrelatedRegressors(double thresholdCorr) {
            List<List<string>> corrRegressors = new List<List<string>>();
            List<string> nonCombinedRegressors = GetNotCombinedRegressors(BaseRegressors);
            List<string> usedRegressors = new List<string>();

            // Find groups of correlated regressors
            for (int i = 0; i < nonCombinedRegressors.Count - 1; i++) {
                if (!usedRegressors.Contains(nonCombinedRegressors[i])) {
                    List<string> corrRegressorsWithMain = new List<string>();
                    corrRegressorsWithMain.Add(nonCombinedRegressors[i]);
                    usedRegressors.Add(nonCombinedRegressors[i]);

                    // Find regressors that correlate with main regressor
                    for (int j = i + 1; j < nonCombinedRegressors.Count; j++) {
                        if (Math.Abs(Statistics.PearsonCorrelationCoefficient(BaseRegressors[nonCombinedRegressors[i]],
                            BaseRegressors[nonCombinedRegressors[j]])) > thresholdCorr) {

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
        /// <param name="factorCombinations">Formed combinations</param>
        private void AddPairwiseCombinationsOfFactors(List<List<string>> factorCombinations) {
            // Get number of factors without pairwise combinations
            int startFactorsNumber = factorCombinations[0].Count;

            for (int combNum = 0; combNum < factorCombinations.Count; combNum++){
                for (int i = 0; i < startFactorsNumber - 1; i++) {
                    for (int j = i + 1; j < startFactorsNumber; j++) {
                        factorCombinations[combNum].Add(factorCombinations[combNum][i] + " & "
                            + factorCombinations[combNum][j]);
                    }
                }
            }
        }

        ///// <summary>
        ///// Add values of regressors to combinations of regressors
        ///// </summary>
        ///// <param name="combinedGroupOfRegressors">Combined group of regressors</param>
        ///// <returns>Combined group of regressors with values</returns>
        //private List<Dictionary<string, List<double>>> GetCombinationRegressorsWithValues(
        //    List<List<string>> combinedGroupOfRegressors) {

        //    List<Dictionary<string, List<double>>> groupOfRegressorsWithValues = new List<Dictionary<string, List<double>>>();

        //    foreach (var combination in combinedGroupOfRegressors) {
        //        Dictionary<string, List<double>> combinedRegressorsWithValues = new Dictionary<string, List<double>>();
        //        foreach(var regressor in combination) {

        //            // If combined regressor doesn't contain regressor then try to switch regressors
        //            // in pairwise combination
        //            if (!BaseRegressors.Keys.Contains(regressor) && regressor.Contains(" & ")) {
        //                string[] regressors = regressor.Split(new string[] { " & " }, StringSplitOptions.None);
        //                string switchRegressor = regressors[1] + " & " + regressors[0];

        //                combinedRegressorsWithValues[switchRegressor] = new List<double>(BaseRegressors[switchRegressor]);
        //            }
        //            else {
        //                combinedRegressorsWithValues[regressor] = new List<double>(BaseRegressors[regressor]);
        //            }
        //        }

        //        groupOfRegressorsWithValues.Add(combinedRegressorsWithValues);
        //    }

        //    return groupOfRegressorsWithValues;
        //}

        /// <summary>
        /// Create all possible models for each regressant
        /// </summary>
        /// <param name="combinationsOfRegressors">Combinations of regressors with values</param>
        /// <returns>All possible models for each regressant</returns>
        private Dictionary<string, List<Model>> CreateGroupsOfModels(List<List<string>> combinationsOfRegressors) {
            Dictionary<string, List<Model>> groupsOfModels = new Dictionary<string, List<Model>>();

            foreach (var model in Models) {
                List<Model> variationOfRegressorsForRegressant = new List<Model>();

                foreach (var combination in combinationsOfRegressors) {
                    variationOfRegressorsForRegressant.Add(new Model(model.RegressantName, model.RegressantValues,
                        GetRegressorsWithValues(model, combination)));
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
                    string[] combinedRegressors = regressor.Split(new string[] { " & " }, StringSplitOptions.None);
                    string regressorName = combinedRegressors[1] + " & " + combinedRegressors[0];
                    regressorsWithValues[regressorName] = new List<double>(model.Regressors[regressorName]);
                }
            }

            return regressorsWithValues;
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
                        new List<string>() { item.Key, String.Join(", ", GetRegressorsShortNamesFromModel(model).ToArray()) });
                }
            }
        }

        /// <summary>
        /// Get list of regressors in short form from model
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>List of short-form regressors</returns>
        private List<string> GetRegressorsShortNamesFromModel(Model model) {
            List<string> shortRegressors = new List<string>();

            // For each regressor in model we get its short form
            foreach (var regressor in model.RegressorsNames) {
                string nextRegressor = "";
                if (RegressorsShortName.ContainsKey(regressor)) {
                    nextRegressor = RegressorsShortName[regressor];
                }
                else if (regressor.Contains(" & ")) {
                    string[] combinedRegressors = regressor.Split(new string[] { " & " }, StringSplitOptions.None);
                    nextRegressor = RegressorsShortName[combinedRegressors[1] + " & " + combinedRegressors[0]];
                }
                shortRegressors.Add(nextRegressor);
            }

            return shortRegressors;
        }

        /// <summary>
        /// Find best model for each regressant
        /// </summary>
        /// <param name="groupsOfModels">List of models for each regressant</param>
        /// <param name="bgWorker">Background worker</param>
        private void FindBestModels(Dictionary<string, List<Model>> groupsOfModels, BackgroundWorker bgWorker) {

            List<Model> bestModels = new List<Model>();

            // Find total number of models
            int totalNumberOfModels = groupsOfModels.Count * groupsOfModels[groupsOfModels.Keys.ToList()[0]].Count;

            // For each regressant find the best model
            foreach (var regressantModels in groupsOfModels.Values) {
                double maxDetermCoeffForRegressant = 0;
                Model bestModelForRegressant = regressantModels[0];

                // Calc coefficient of determination for each model to select the best one
                foreach (var model in regressantModels) {

                    model.BuildEquation();
                    if (model.AdjDetermCoeff > maxDetermCoeffForRegressant) {
                        maxDetermCoeffForRegressant = model.AdjDetermCoeff;
                        bestModelForRegressant = model;
                    }
                }

                // Add best model regressors for regressant to data grid view
                foreach (var regressor in bestModelForRegressant.RegressorsNames) {
                    groupedRegressorsDataGrid.Invoke(new Action<List<string>>((row) => groupedRegressorsDataGrid.Rows.Add(row.ToArray())),
                        new List<string> { bestModelForRegressant.RegressantName, regressor });
                }
                bestModels.Add(bestModelForRegressant);
            }

            // Set best models
            Models = new List<Model>(bestModels);

            // Fill filter data grid
            

            // Resize dataGrid
            groupedRegressorsDataGrid.Invoke(new Action<Size>((size) => groupedRegressorsDataGrid.Size = size),
                new Size(groupedRegressorsDataGrid.Width, groupedRegressorsDataGrid.Height + 19));

            // Enable numeric up down for setting maximum correlation between regressors
            maxCorrelBtwRegressors.Invoke(new Action<bool>((b) => maxCorrelBtwRegressors.Enabled = b), true);

            // Enable accept button for grouping of regressors
            groupedRegressorsButton.Invoke(new Action<bool>((b) => groupedRegressorsButton.Enabled = b), true);

            // Enable empirical way radio
            empWayRadio.Invoke(new Action<bool>((b) => empWayRadio.Enabled = b), true);

            // Enable classic way radio
            classicWayRadio.Invoke(new Action<bool>((b) => classicWayRadio.Enabled = b), true);

            bgWorker.CancelAsync();
        }

        /// <summary>
        /// Get list of headers of non-combined regressors
        /// </summary>
        /// <param name="regressors">Regressors</param>
        /// <returns>List of headers</returns>
        private List<string> GetNotCombinedRegressors(Dictionary<string, List<double>> regressors) {
            List<string> nonCombinedRegressors = new List<string>();
            foreach (var regressorName in regressors.Keys) {
                if (!regressorName.Contains(" & ")) {
                    nonCombinedRegressors.Add(regressorName);
                }
            }
            return nonCombinedRegressors;
        }

        private void doFunctionalProcessButton_Click(object sender, EventArgs e) {
            // Show warinig form
            UserWarningForm warningForm = new UserWarningForm(StepsInfo.UserWarningFuncPreprocessing);
            warningForm.ShowDialog();
            if (warningForm.AcceptAction) {
                RunBackgroundFunctionalProcessData();

                doFunctionalProcessButton.Enabled = false;
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
                        string regressorName = $"{RegressorsShortName[regressor.Key]} - {regressor.Key}";
                        // Add row of preprocess functions to data grid
                        functionsForProcessingDataGrid.Invoke(new Action<List<string>>((row) => functionsForProcessingDataGrid.Rows.Add(row.ToArray())),
                            new List<string>() { model.RegressantName, regressorName,
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

        /// <summary>
        /// Filter unimportant regressors by classic way
        /// </summary>
        private void ClassicWayToFilterRegressors() {
            foreach (var listModels in ModelsForRegressants.Values) {
                foreach (var model in listModels) {
                    model.ClassicWayFilterRegressors();
                }
            }
        }

        /// <summary>
        /// Filter unimportant regressors by empirical way
        /// </summary>
        private void EmpiricalWayToFilterRegressors() {
            double thresholdValueCorr = Convert.ToDouble(valueEmpWayCorr.Value);

            foreach (var listModels in ModelsForRegressants.Values) {
                foreach (var model in listModels) {
                    model.EmpiricalWayFilterRegressors(thresholdValueCorr);
                }
            }
        }

        private void cancelFilterFactorsButton_Click(object sender, EventArgs e) {
            // Restore non-filter regressors for each model for each regressant
            foreach (var listModels in ModelsForRegressants.Values) {
                listModels.ForEach(model => model.RestoreNonFilterRegressors());
            }
            cancelFilterFactorsButton.Enabled = false;

            // Print grouped regressors for each regressant
            PrintGroupedRegressors(onlyImportantFactorsDataGrid);

            ClearControlsBuildEquations();
            buildEquationsButton.Enabled = true;
        }

        private void buildEquationsButton_Click(object sender, EventArgs e) {
            RunBackgroundFindEquations();

            ClearControlsImitationParameters();

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
            // Check if bgworker has been stopped
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {

                // Build equation for each model if it's need
                foreach (var model in Models) {
                    model.BuildEquation();
                    equationsDataGrid.Invoke(new Action<List<string>>((row) => equationsDataGrid.Rows.Add(row.ToArray())),
                            new List<string>() { model.RegressantName, model.AdjDetermCoeff.ToString(), model.Equation });
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
            helpAllStepsMenu.ToolTipText = StepsInfo.StepLoadData;
            percentAreaExpansion.Maximum = Decimal.MaxValue;
            symbiosisAreaRadio.Checked = true;
            percentAreaExpansion.Value = 10;
            autoProportionRadio.Checked = true;
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

            ClearControlsGroupingFactors();
            ClearControlsProcessData();
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
        private void ClearControlsProcessData() {
            ClearDataGV(functionsForProcessingDataGrid);
            doFunctionalProcessButton.Enabled = false;
            labelFuncPreprocess.Visible = false;
            labelPreprocessingFinish.Visible = false;
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
            valueEmpWayCorr.Value = Convert.ToDecimal(0.15);
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
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepLoadData;
                    break;
                case 1:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepFormationRegressorsGroup;
                    break;
                case 2:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepProcessFactors;
                    break;
                case 3:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepFilterFactors;
                    break;
                case 4:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepBuildEquations;
                    break;
                case 5:
                    helpAllStepsMenu.ToolTipText = StepsInfo.StepSetImitationParameters;
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
                    System.Threading.Thread.Sleep(20);
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


                        // Tab formation group of regressors
                        groupedRegressorsDataGrid.Invoke(new Action<Size>((size) => groupedRegressorsDataGrid.Size = size),
                           new Size(groupedRegressorsDataGrid.Width + widthDiff, groupedRegressorsDataGrid.Height + heightDiff));

                        labelMaxCorrelBtwRegressors.Invoke(new Action<Point>((loc) => labelMaxCorrelBtwRegressors.Location = loc),
                            new Point(labelMaxCorrelBtwRegressors.Location.X + widthDiff, labelMaxCorrelBtwRegressors.Location.Y));

                        groupBoxGroupedRegressors.Invoke(new Action<Point>((loc) => groupBoxGroupedRegressors.Location = loc),
                            new Point(groupBoxGroupedRegressors.Location.X + widthDiff, groupBoxGroupedRegressors.Location.Y));

                        labelGroupingRegressors.Invoke(new Action<Point>((loc) => labelGroupingRegressors.Location = loc),
                            new Point(labelGroupingRegressors.Location.X + widthDiff, labelGroupingRegressors.Location.Y + heightDiff));

                        labelGroupingRegressorsEnd.Invoke(new Action<Point>((loc) => labelGroupingRegressorsEnd.Location = loc),
                            new Point(labelGroupingRegressorsEnd.Location.X + widthDiff, labelGroupingRegressorsEnd.Location.Y + heightDiff));


                        // Tab process data
                        functionsForProcessingDataGrid.Invoke(new Action<Size>((size) => functionsForProcessingDataGrid.Size = size),
                           new Size(functionsForProcessingDataGrid.Width + widthDiff, functionsForProcessingDataGrid.Height + heightDiff));

                        doFunctionalProcessButton.Invoke(new Action<Point>((loc) => doFunctionalProcessButton.Location = loc),
                            new Point(doFunctionalProcessButton.Location.X + widthDiff, doFunctionalProcessButton.Location.Y));

                        labelPreprocessingFinish.Invoke(new Action<Point>((loc) => labelPreprocessingFinish.Location = loc),
                            new Point(labelPreprocessingFinish.Location.X + widthDiff, labelPreprocessingFinish.Location.Y + heightDiff));

                        labelFuncPreprocess.Invoke(new Action<Point>((loc) => labelFuncPreprocess.Location = loc),
                            new Point(labelFuncPreprocess.Location.X + widthDiff, labelFuncPreprocess.Location.Y + heightDiff));


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
