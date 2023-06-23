namespace Multiple_Linear_Regression {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.WorkFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAllSteps = new System.Windows.Forms.ToolStripMenuItem();
            this.allTabs = new System.Windows.Forms.TabControl();
            this.loadDataTab = new System.Windows.Forms.TabPage();
            this.labelResultDataLoad = new System.Windows.Forms.Label();
            this.checkPairwiseCombinations = new System.Windows.Forms.CheckBox();
            this.clearSelectedFactorsButton = new System.Windows.Forms.Button();
            this.acceptFactorsButton = new System.Windows.Forms.Button();
            this.selectRegressorsButton = new System.Windows.Forms.Button();
            this.selectRegressantsButton = new System.Windows.Forms.Button();
            this.progressBarDataLoad = new System.Windows.Forms.ProgressBar();
            this.factorsData = new System.Windows.Forms.DataGridView();
            this.processingStatDataTabGusev = new System.Windows.Forms.TabPage();
            this.labelPreprocessingGusevFinish = new System.Windows.Forms.Label();
            this.labelFuncPreprocessGusev = new System.Windows.Forms.Label();
            this.doFunctionalProcessGusevButton = new System.Windows.Forms.Button();
            this.functionsForProcessingGusevDataGrid = new System.Windows.Forms.DataGridView();
            this.processingStatDataTabOkunev = new System.Windows.Forms.TabPage();
            this.labelPreprocessingOkunevFinish = new System.Windows.Forms.Label();
            this.labelFuncPreprocessOkunev = new System.Windows.Forms.Label();
            this.doFunctionalProcessOkunevButton = new System.Windows.Forms.Button();
            this.functionsForProcessingOkunevDataGrid = new System.Windows.Forms.DataGridView();
            this.formationOfControlFactorSetsTab = new System.Windows.Forms.TabPage();
            this.groupBoxGroupedRegressors = new System.Windows.Forms.GroupBox();
            this.maxCorrelBtwRegressors = new System.Windows.Forms.NumericUpDown();
            this.groupedRegressorsButton = new System.Windows.Forms.Button();
            this.labelMaxCorrelBtwRegressors = new System.Windows.Forms.Label();
            this.labelGroupingRegressorsEnd = new System.Windows.Forms.Label();
            this.labelGroupingRegressors = new System.Windows.Forms.Label();
            this.groupedRegressorsDataGrid = new System.Windows.Forms.DataGridView();
            this.removeUnimportantFactorsTab = new System.Windows.Forms.TabPage();
            this.groupBoxFilterRegressors = new System.Windows.Forms.GroupBox();
            this.cancelFilterFactorsButton = new System.Windows.Forms.Button();
            this.acceptFilterFactorsButton = new System.Windows.Forms.Button();
            this.valueEmpWayCorr = new System.Windows.Forms.NumericUpDown();
            this.classicWayRadio = new System.Windows.Forms.RadioButton();
            this.empWayRadio = new System.Windows.Forms.RadioButton();
            this.labelFilterFinish = new System.Windows.Forms.Label();
            this.labelFilterLoad = new System.Windows.Forms.Label();
            this.onlyImportantFactorsDataGrid = new System.Windows.Forms.DataGridView();
            this.buildRegrEquationsTab = new System.Windows.Forms.TabPage();
            this.labelBuildingFinish = new System.Windows.Forms.Label();
            this.buildEquationsButton = new System.Windows.Forms.Button();
            this.labelBuildingLoad = new System.Windows.Forms.Label();
            this.equationsDataGrid = new System.Windows.Forms.DataGridView();
            this.controlSimulationTab = new System.Windows.Forms.TabPage();
            this.groupNumberCorrelatedIntervals = new System.Windows.Forms.GroupBox();
            this.numberOfCorrIntervalsManual = new System.Windows.Forms.NumericUpDown();
            this.manualNumberCorrIntervalRadio = new System.Windows.Forms.RadioButton();
            this.autoNumberCorrIntervalsRadio = new System.Windows.Forms.RadioButton();
            this.labelAvailableModels = new System.Windows.Forms.Label();
            this.labelSelectedModels = new System.Windows.Forms.Label();
            this.acceptControlsParametersButton = new System.Windows.Forms.Button();
            this.groupProportionOfAreaExpansion = new System.Windows.Forms.GroupBox();
            this.autoProportionRadio = new System.Windows.Forms.RadioButton();
            this.equallyBothWaysRadio = new System.Windows.Forms.RadioButton();
            this.groupPercentAreaExpansion = new System.Windows.Forms.GroupBox();
            this.percentAreaExpansion = new System.Windows.Forms.NumericUpDown();
            this.groupDefinitionAreaType = new System.Windows.Forms.GroupBox();
            this.symbiosisAreaRadio = new System.Windows.Forms.RadioButton();
            this.theoreticalAreaRadio = new System.Windows.Forms.RadioButton();
            this.empDefAreaRadio = new System.Windows.Forms.RadioButton();
            this.labelSelectDefAreaParams = new System.Windows.Forms.Label();
            this.labelSelectModelsForControl = new System.Windows.Forms.Label();
            this.allToAvailableModelsList = new System.Windows.Forms.Button();
            this.allToSelectModelsList = new System.Windows.Forms.Button();
            this.toAvailableModelsList = new System.Windows.Forms.Button();
            this.toSelectModelsList = new System.Windows.Forms.Button();
            this.listAvailabelModels = new System.Windows.Forms.ListBox();
            this.listSelectedModels = new System.Windows.Forms.ListBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.WorkFileMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitAppMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAllStepsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.regressantsList = new System.Windows.Forms.ListBox();
            this.labelregressantsList = new System.Windows.Forms.Label();
            this.regressorsList = new System.Windows.Forms.ListBox();
            this.labelRegressorsList = new System.Windows.Forms.Label();
            this.toolTipSymbiosis = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipAutoProportion = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipPercentAreaExpansion = new System.Windows.Forms.ToolTip(this.components);
            this.groupTaskType = new System.Windows.Forms.GroupBox();
            this.radioControlTask = new System.Windows.Forms.RadioButton();
            this.radioPredictionTask = new System.Windows.Forms.RadioButton();
            this.predictionTab = new System.Windows.Forms.TabPage();
            this.realPredictValuesDataGrid = new System.Windows.Forms.DataGridView();
            this.predictionMetricsDataGrid = new System.Windows.Forms.DataGridView();
            this.loadDataForPredictButton = new System.Windows.Forms.Button();
            this.allTabs.SuspendLayout();
            this.loadDataTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.factorsData)).BeginInit();
            this.processingStatDataTabGusev.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.functionsForProcessingGusevDataGrid)).BeginInit();
            this.processingStatDataTabOkunev.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.functionsForProcessingOkunevDataGrid)).BeginInit();
            this.formationOfControlFactorSetsTab.SuspendLayout();
            this.groupBoxGroupedRegressors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxCorrelBtwRegressors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupedRegressorsDataGrid)).BeginInit();
            this.removeUnimportantFactorsTab.SuspendLayout();
            this.groupBoxFilterRegressors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valueEmpWayCorr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.onlyImportantFactorsDataGrid)).BeginInit();
            this.buildRegrEquationsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.equationsDataGrid)).BeginInit();
            this.controlSimulationTab.SuspendLayout();
            this.groupNumberCorrelatedIntervals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfCorrIntervalsManual)).BeginInit();
            this.groupProportionOfAreaExpansion.SuspendLayout();
            this.groupPercentAreaExpansion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percentAreaExpansion)).BeginInit();
            this.groupDefinitionAreaType.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.groupTaskType.SuspendLayout();
            this.predictionTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.realPredictValuesDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.predictionMetricsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // WorkFileMenuItem
            // 
            this.WorkFileMenuItem.Name = "WorkFileMenuItem";
            this.WorkFileMenuItem.Size = new System.Drawing.Size(48, 20);
            this.WorkFileMenuItem.Text = "Файл";
            // 
            // OpenFile
            // 
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(180, 22);
            this.OpenFile.Text = "Открыть";
            // 
            // ExitApp
            // 
            this.ExitApp.Name = "ExitApp";
            this.ExitApp.Size = new System.Drawing.Size(54, 20);
            this.ExitApp.Text = "Выход";
            // 
            // helpAllSteps
            // 
            this.helpAllSteps.Name = "helpAllSteps";
            this.helpAllSteps.Size = new System.Drawing.Size(68, 20);
            this.helpAllSteps.Text = "Помощь";
            // 
            // allTabs
            // 
            this.allTabs.Controls.Add(this.loadDataTab);
            this.allTabs.Controls.Add(this.processingStatDataTabGusev);
            this.allTabs.Controls.Add(this.processingStatDataTabOkunev);
            this.allTabs.Controls.Add(this.formationOfControlFactorSetsTab);
            this.allTabs.Controls.Add(this.removeUnimportantFactorsTab);
            this.allTabs.Controls.Add(this.buildRegrEquationsTab);
            this.allTabs.Controls.Add(this.controlSimulationTab);
            this.allTabs.Controls.Add(this.predictionTab);
            this.allTabs.Location = new System.Drawing.Point(177, 27);
            this.allTabs.Name = "allTabs";
            this.allTabs.SelectedIndex = 0;
            this.allTabs.Size = new System.Drawing.Size(824, 416);
            this.allTabs.TabIndex = 3;
            this.allTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.allTabs_Selected);
            // 
            // loadDataTab
            // 
            this.loadDataTab.Controls.Add(this.groupTaskType);
            this.loadDataTab.Controls.Add(this.labelResultDataLoad);
            this.loadDataTab.Controls.Add(this.checkPairwiseCombinations);
            this.loadDataTab.Controls.Add(this.clearSelectedFactorsButton);
            this.loadDataTab.Controls.Add(this.acceptFactorsButton);
            this.loadDataTab.Controls.Add(this.selectRegressorsButton);
            this.loadDataTab.Controls.Add(this.selectRegressantsButton);
            this.loadDataTab.Controls.Add(this.progressBarDataLoad);
            this.loadDataTab.Controls.Add(this.factorsData);
            this.loadDataTab.Location = new System.Drawing.Point(4, 22);
            this.loadDataTab.Name = "loadDataTab";
            this.loadDataTab.Padding = new System.Windows.Forms.Padding(3);
            this.loadDataTab.Size = new System.Drawing.Size(816, 390);
            this.loadDataTab.TabIndex = 0;
            this.loadDataTab.Text = "Загрузка данных";
            this.loadDataTab.UseVisualStyleBackColor = true;
            // 
            // labelResultDataLoad
            // 
            this.labelResultDataLoad.AutoSize = true;
            this.labelResultDataLoad.Location = new System.Drawing.Point(651, 370);
            this.labelResultDataLoad.Name = "labelResultDataLoad";
            this.labelResultDataLoad.Size = new System.Drawing.Size(150, 13);
            this.labelResultDataLoad.TabIndex = 16;
            this.labelResultDataLoad.Text = "Факторы успешно выбраны";
            this.labelResultDataLoad.Visible = false;
            // 
            // checkPairwiseCombinations
            // 
            this.checkPairwiseCombinations.AutoSize = true;
            this.checkPairwiseCombinations.Checked = true;
            this.checkPairwiseCombinations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPairwiseCombinations.Location = new System.Drawing.Point(654, 271);
            this.checkPairwiseCombinations.Name = "checkPairwiseCombinations";
            this.checkPairwiseCombinations.Size = new System.Drawing.Size(155, 30);
            this.checkPairwiseCombinations.TabIndex = 15;
            this.checkPairwiseCombinations.Text = "Использовать попарные \r\nсочетания факторов";
            this.checkPairwiseCombinations.UseVisualStyleBackColor = true;
            // 
            // clearSelectedFactorsButton
            // 
            this.clearSelectedFactorsButton.Enabled = false;
            this.clearSelectedFactorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearSelectedFactorsButton.Location = new System.Drawing.Point(663, 143);
            this.clearSelectedFactorsButton.Name = "clearSelectedFactorsButton";
            this.clearSelectedFactorsButton.Size = new System.Drawing.Size(138, 28);
            this.clearSelectedFactorsButton.TabIndex = 14;
            this.clearSelectedFactorsButton.Text = "Очистить факторы";
            this.clearSelectedFactorsButton.UseVisualStyleBackColor = true;
            this.clearSelectedFactorsButton.Click += new System.EventHandler(this.clearSelectedFactorsButton_Click);
            // 
            // acceptFactorsButton
            // 
            this.acceptFactorsButton.Enabled = false;
            this.acceptFactorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptFactorsButton.Location = new System.Drawing.Point(680, 307);
            this.acceptFactorsButton.Name = "acceptFactorsButton";
            this.acceptFactorsButton.Size = new System.Drawing.Size(110, 48);
            this.acceptFactorsButton.TabIndex = 13;
            this.acceptFactorsButton.Text = "Подтвердить";
            this.acceptFactorsButton.UseVisualStyleBackColor = true;
            this.acceptFactorsButton.Click += new System.EventHandler(this.acceptFactorsButton_Click);
            // 
            // selectRegressorsButton
            // 
            this.selectRegressorsButton.Enabled = false;
            this.selectRegressorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectRegressorsButton.Location = new System.Drawing.Point(663, 72);
            this.selectRegressorsButton.Name = "selectRegressorsButton";
            this.selectRegressorsButton.Size = new System.Drawing.Size(138, 49);
            this.selectRegressorsButton.TabIndex = 12;
            this.selectRegressorsButton.Text = "Выбрать управляющие факторы";
            this.selectRegressorsButton.UseVisualStyleBackColor = true;
            this.selectRegressorsButton.Click += new System.EventHandler(this.selectRegressorsButton_Click);
            // 
            // selectRegressantsButton
            // 
            this.selectRegressantsButton.Enabled = false;
            this.selectRegressantsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectRegressantsButton.Location = new System.Drawing.Point(663, 16);
            this.selectRegressantsButton.Name = "selectRegressantsButton";
            this.selectRegressantsButton.Size = new System.Drawing.Size(138, 49);
            this.selectRegressantsButton.TabIndex = 11;
            this.selectRegressantsButton.Text = "Выбрать управляемые факторы";
            this.selectRegressantsButton.UseVisualStyleBackColor = true;
            this.selectRegressantsButton.Click += new System.EventHandler(this.selectRegressantsButton_Click);
            // 
            // progressBarDataLoad
            // 
            this.progressBarDataLoad.Location = new System.Drawing.Point(3, 370);
            this.progressBarDataLoad.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarDataLoad.Name = "progressBarDataLoad";
            this.progressBarDataLoad.Size = new System.Drawing.Size(632, 15);
            this.progressBarDataLoad.TabIndex = 10;
            this.progressBarDataLoad.Visible = false;
            // 
            // factorsData
            // 
            this.factorsData.AllowUserToAddRows = false;
            this.factorsData.AllowUserToDeleteRows = false;
            this.factorsData.AllowUserToResizeRows = false;
            this.factorsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.factorsData.Location = new System.Drawing.Point(3, 3);
            this.factorsData.Name = "factorsData";
            this.factorsData.ReadOnly = true;
            this.factorsData.RowHeadersWidth = 51;
            this.factorsData.Size = new System.Drawing.Size(632, 381);
            this.factorsData.TabIndex = 9;
            // 
            // processingStatDataTabGusev
            // 
            this.processingStatDataTabGusev.Controls.Add(this.labelPreprocessingGusevFinish);
            this.processingStatDataTabGusev.Controls.Add(this.labelFuncPreprocessGusev);
            this.processingStatDataTabGusev.Controls.Add(this.doFunctionalProcessGusevButton);
            this.processingStatDataTabGusev.Controls.Add(this.functionsForProcessingGusevDataGrid);
            this.processingStatDataTabGusev.Location = new System.Drawing.Point(4, 22);
            this.processingStatDataTabGusev.Name = "processingStatDataTabGusev";
            this.processingStatDataTabGusev.Padding = new System.Windows.Forms.Padding(3);
            this.processingStatDataTabGusev.Size = new System.Drawing.Size(816, 390);
            this.processingStatDataTabGusev.TabIndex = 1;
            this.processingStatDataTabGusev.Text = "Функциональная предобработка (1 вариант)";
            this.processingStatDataTabGusev.UseVisualStyleBackColor = true;
            // 
            // labelPreprocessingGusevFinish
            // 
            this.labelPreprocessingGusevFinish.AutoSize = true;
            this.labelPreprocessingGusevFinish.Location = new System.Drawing.Point(665, 371);
            this.labelPreprocessingGusevFinish.Name = "labelPreprocessingGusevFinish";
            this.labelPreprocessingGusevFinish.Size = new System.Drawing.Size(145, 13);
            this.labelPreprocessingGusevFinish.TabIndex = 17;
            this.labelPreprocessingGusevFinish.Text = "Предобработка выполнена";
            this.labelPreprocessingGusevFinish.Visible = false;
            // 
            // labelFuncPreprocessGusev
            // 
            this.labelFuncPreprocessGusev.AutoSize = true;
            this.labelFuncPreprocessGusev.Location = new System.Drawing.Point(666, 371);
            this.labelFuncPreprocessGusev.Name = "labelFuncPreprocessGusev";
            this.labelFuncPreprocessGusev.Size = new System.Drawing.Size(62, 13);
            this.labelFuncPreprocessGusev.TabIndex = 15;
            this.labelFuncPreprocessGusev.Text = "Обработка";
            this.labelFuncPreprocessGusev.Visible = false;
            // 
            // doFunctionalProcessGusevButton
            // 
            this.doFunctionalProcessGusevButton.Enabled = false;
            this.doFunctionalProcessGusevButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.doFunctionalProcessGusevButton.Location = new System.Drawing.Point(669, 55);
            this.doFunctionalProcessGusevButton.Name = "doFunctionalProcessGusevButton";
            this.doFunctionalProcessGusevButton.Size = new System.Drawing.Size(126, 66);
            this.doFunctionalProcessGusevButton.TabIndex = 14;
            this.doFunctionalProcessGusevButton.Text = "Выполнить функциональную предобработку";
            this.doFunctionalProcessGusevButton.UseVisualStyleBackColor = true;
            this.doFunctionalProcessGusevButton.Click += new System.EventHandler(this.doFunctionalProcessGusevButton_Click);
            // 
            // functionsForProcessingGusevDataGrid
            // 
            this.functionsForProcessingGusevDataGrid.AllowUserToAddRows = false;
            this.functionsForProcessingGusevDataGrid.AllowUserToDeleteRows = false;
            this.functionsForProcessingGusevDataGrid.AllowUserToResizeRows = false;
            this.functionsForProcessingGusevDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.functionsForProcessingGusevDataGrid.Location = new System.Drawing.Point(3, 3);
            this.functionsForProcessingGusevDataGrid.Name = "functionsForProcessingGusevDataGrid";
            this.functionsForProcessingGusevDataGrid.ReadOnly = true;
            this.functionsForProcessingGusevDataGrid.RowHeadersWidth = 51;
            this.functionsForProcessingGusevDataGrid.Size = new System.Drawing.Size(632, 381);
            this.functionsForProcessingGusevDataGrid.TabIndex = 10;
            // 
            // processingStatDataTabOkunev
            // 
            this.processingStatDataTabOkunev.Controls.Add(this.labelPreprocessingOkunevFinish);
            this.processingStatDataTabOkunev.Controls.Add(this.labelFuncPreprocessOkunev);
            this.processingStatDataTabOkunev.Controls.Add(this.doFunctionalProcessOkunevButton);
            this.processingStatDataTabOkunev.Controls.Add(this.functionsForProcessingOkunevDataGrid);
            this.processingStatDataTabOkunev.Location = new System.Drawing.Point(4, 22);
            this.processingStatDataTabOkunev.Name = "processingStatDataTabOkunev";
            this.processingStatDataTabOkunev.Size = new System.Drawing.Size(816, 390);
            this.processingStatDataTabOkunev.TabIndex = 6;
            this.processingStatDataTabOkunev.Text = "Функциональная предобработка (2 вариант)";
            this.processingStatDataTabOkunev.UseVisualStyleBackColor = true;
            // 
            // labelPreprocessingOkunevFinish
            // 
            this.labelPreprocessingOkunevFinish.AutoSize = true;
            this.labelPreprocessingOkunevFinish.Location = new System.Drawing.Point(665, 371);
            this.labelPreprocessingOkunevFinish.Name = "labelPreprocessingOkunevFinish";
            this.labelPreprocessingOkunevFinish.Size = new System.Drawing.Size(145, 13);
            this.labelPreprocessingOkunevFinish.TabIndex = 19;
            this.labelPreprocessingOkunevFinish.Text = "Предобработка выполнена";
            this.labelPreprocessingOkunevFinish.Visible = false;
            // 
            // labelFuncPreprocessOkunev
            // 
            this.labelFuncPreprocessOkunev.AutoSize = true;
            this.labelFuncPreprocessOkunev.Location = new System.Drawing.Point(665, 371);
            this.labelFuncPreprocessOkunev.Name = "labelFuncPreprocessOkunev";
            this.labelFuncPreprocessOkunev.Size = new System.Drawing.Size(62, 13);
            this.labelFuncPreprocessOkunev.TabIndex = 18;
            this.labelFuncPreprocessOkunev.Text = "Обработка";
            this.labelFuncPreprocessOkunev.Visible = false;
            // 
            // doFunctionalProcessOkunevButton
            // 
            this.doFunctionalProcessOkunevButton.Enabled = false;
            this.doFunctionalProcessOkunevButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.doFunctionalProcessOkunevButton.Location = new System.Drawing.Point(669, 55);
            this.doFunctionalProcessOkunevButton.Name = "doFunctionalProcessOkunevButton";
            this.doFunctionalProcessOkunevButton.Size = new System.Drawing.Size(126, 66);
            this.doFunctionalProcessOkunevButton.TabIndex = 15;
            this.doFunctionalProcessOkunevButton.Text = "Выполнить функциональную предобработку";
            this.doFunctionalProcessOkunevButton.UseVisualStyleBackColor = true;
            this.doFunctionalProcessOkunevButton.Click += new System.EventHandler(this.doFunctionalProcessOkunevButton_Click);
            // 
            // functionsForProcessingOkunevDataGrid
            // 
            this.functionsForProcessingOkunevDataGrid.AllowUserToAddRows = false;
            this.functionsForProcessingOkunevDataGrid.AllowUserToDeleteRows = false;
            this.functionsForProcessingOkunevDataGrid.AllowUserToResizeRows = false;
            this.functionsForProcessingOkunevDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.functionsForProcessingOkunevDataGrid.Location = new System.Drawing.Point(3, 3);
            this.functionsForProcessingOkunevDataGrid.Name = "functionsForProcessingOkunevDataGrid";
            this.functionsForProcessingOkunevDataGrid.ReadOnly = true;
            this.functionsForProcessingOkunevDataGrid.RowHeadersWidth = 51;
            this.functionsForProcessingOkunevDataGrid.Size = new System.Drawing.Size(632, 381);
            this.functionsForProcessingOkunevDataGrid.TabIndex = 11;
            // 
            // formationOfControlFactorSetsTab
            // 
            this.formationOfControlFactorSetsTab.Controls.Add(this.groupBoxGroupedRegressors);
            this.formationOfControlFactorSetsTab.Controls.Add(this.labelGroupingRegressorsEnd);
            this.formationOfControlFactorSetsTab.Controls.Add(this.labelGroupingRegressors);
            this.formationOfControlFactorSetsTab.Controls.Add(this.groupedRegressorsDataGrid);
            this.formationOfControlFactorSetsTab.Location = new System.Drawing.Point(4, 22);
            this.formationOfControlFactorSetsTab.Margin = new System.Windows.Forms.Padding(2);
            this.formationOfControlFactorSetsTab.Name = "formationOfControlFactorSetsTab";
            this.formationOfControlFactorSetsTab.Size = new System.Drawing.Size(816, 390);
            this.formationOfControlFactorSetsTab.TabIndex = 5;
            this.formationOfControlFactorSetsTab.Text = "Формирование наборов управляющих факторов";
            this.formationOfControlFactorSetsTab.UseVisualStyleBackColor = true;
            // 
            // groupBoxGroupedRegressors
            // 
            this.groupBoxGroupedRegressors.Controls.Add(this.maxCorrelBtwRegressors);
            this.groupBoxGroupedRegressors.Controls.Add(this.groupedRegressorsButton);
            this.groupBoxGroupedRegressors.Controls.Add(this.labelMaxCorrelBtwRegressors);
            this.groupBoxGroupedRegressors.Location = new System.Drawing.Point(641, 13);
            this.groupBoxGroupedRegressors.Name = "groupBoxGroupedRegressors";
            this.groupBoxGroupedRegressors.Size = new System.Drawing.Size(170, 154);
            this.groupBoxGroupedRegressors.TabIndex = 44;
            this.groupBoxGroupedRegressors.TabStop = false;
            this.groupBoxGroupedRegressors.Text = "Разбиение факторов";
            // 
            // maxCorrelBtwRegressors
            // 
            this.maxCorrelBtwRegressors.DecimalPlaces = 2;
            this.maxCorrelBtwRegressors.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.maxCorrelBtwRegressors.Location = new System.Drawing.Point(33, 57);
            this.maxCorrelBtwRegressors.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxCorrelBtwRegressors.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.maxCorrelBtwRegressors.Name = "maxCorrelBtwRegressors";
            this.maxCorrelBtwRegressors.Size = new System.Drawing.Size(120, 20);
            this.maxCorrelBtwRegressors.TabIndex = 41;
            this.maxCorrelBtwRegressors.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // groupedRegressorsButton
            // 
            this.groupedRegressorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupedRegressorsButton.Location = new System.Drawing.Point(20, 100);
            this.groupedRegressorsButton.Name = "groupedRegressorsButton";
            this.groupedRegressorsButton.Size = new System.Drawing.Size(141, 43);
            this.groupedRegressorsButton.TabIndex = 43;
            this.groupedRegressorsButton.Text = "Сформировать наборы управляющих факторов";
            this.groupedRegressorsButton.UseVisualStyleBackColor = true;
            this.groupedRegressorsButton.Click += new System.EventHandler(this.groupedRegressorsButton_Click);
            // 
            // labelMaxCorrelBtwRegressors
            // 
            this.labelMaxCorrelBtwRegressors.AutoSize = true;
            this.labelMaxCorrelBtwRegressors.Location = new System.Drawing.Point(10, 27);
            this.labelMaxCorrelBtwRegressors.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMaxCorrelBtwRegressors.Name = "labelMaxCorrelBtwRegressors";
            this.labelMaxCorrelBtwRegressors.Size = new System.Drawing.Size(148, 26);
            this.labelMaxCorrelBtwRegressors.TabIndex = 42;
            this.labelMaxCorrelBtwRegressors.Text = "Пороговое значение\r\nкоэффициента корреляции:";
            // 
            // labelGroupingRegressorsEnd
            // 
            this.labelGroupingRegressorsEnd.AutoSize = true;
            this.labelGroupingRegressorsEnd.Location = new System.Drawing.Point(658, 370);
            this.labelGroupingRegressorsEnd.Name = "labelGroupingRegressorsEnd";
            this.labelGroupingRegressorsEnd.Size = new System.Drawing.Size(131, 13);
            this.labelGroupingRegressorsEnd.TabIndex = 40;
            this.labelGroupingRegressorsEnd.Text = "Группировка выполнена";
            this.labelGroupingRegressorsEnd.Visible = false;
            // 
            // labelGroupingRegressors
            // 
            this.labelGroupingRegressors.AutoSize = true;
            this.labelGroupingRegressors.Location = new System.Drawing.Point(658, 370);
            this.labelGroupingRegressors.Name = "labelGroupingRegressors";
            this.labelGroupingRegressors.Size = new System.Drawing.Size(72, 13);
            this.labelGroupingRegressors.TabIndex = 39;
            this.labelGroupingRegressors.Text = "Группировка";
            this.labelGroupingRegressors.Visible = false;
            // 
            // groupedRegressorsDataGrid
            // 
            this.groupedRegressorsDataGrid.AllowUserToAddRows = false;
            this.groupedRegressorsDataGrid.AllowUserToDeleteRows = false;
            this.groupedRegressorsDataGrid.AllowUserToResizeRows = false;
            this.groupedRegressorsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupedRegressorsDataGrid.Location = new System.Drawing.Point(3, 3);
            this.groupedRegressorsDataGrid.Name = "groupedRegressorsDataGrid";
            this.groupedRegressorsDataGrid.ReadOnly = true;
            this.groupedRegressorsDataGrid.RowHeadersWidth = 51;
            this.groupedRegressorsDataGrid.Size = new System.Drawing.Size(632, 381);
            this.groupedRegressorsDataGrid.TabIndex = 12;
            // 
            // removeUnimportantFactorsTab
            // 
            this.removeUnimportantFactorsTab.Controls.Add(this.groupBoxFilterRegressors);
            this.removeUnimportantFactorsTab.Controls.Add(this.labelFilterFinish);
            this.removeUnimportantFactorsTab.Controls.Add(this.labelFilterLoad);
            this.removeUnimportantFactorsTab.Controls.Add(this.onlyImportantFactorsDataGrid);
            this.removeUnimportantFactorsTab.Location = new System.Drawing.Point(4, 22);
            this.removeUnimportantFactorsTab.Name = "removeUnimportantFactorsTab";
            this.removeUnimportantFactorsTab.Size = new System.Drawing.Size(816, 390);
            this.removeUnimportantFactorsTab.TabIndex = 2;
            this.removeUnimportantFactorsTab.Text = "Фильтрация управляющих факторов";
            this.removeUnimportantFactorsTab.UseVisualStyleBackColor = true;
            // 
            // groupBoxFilterRegressors
            // 
            this.groupBoxFilterRegressors.Controls.Add(this.cancelFilterFactorsButton);
            this.groupBoxFilterRegressors.Controls.Add(this.acceptFilterFactorsButton);
            this.groupBoxFilterRegressors.Controls.Add(this.valueEmpWayCorr);
            this.groupBoxFilterRegressors.Controls.Add(this.classicWayRadio);
            this.groupBoxFilterRegressors.Controls.Add(this.empWayRadio);
            this.groupBoxFilterRegressors.Location = new System.Drawing.Point(641, 13);
            this.groupBoxFilterRegressors.Name = "groupBoxFilterRegressors";
            this.groupBoxFilterRegressors.Size = new System.Drawing.Size(170, 184);
            this.groupBoxFilterRegressors.TabIndex = 46;
            this.groupBoxFilterRegressors.TabStop = false;
            this.groupBoxFilterRegressors.Text = "Фильтрация факторов";
            // 
            // cancelFilterFactorsButton
            // 
            this.cancelFilterFactorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelFilterFactorsButton.Location = new System.Drawing.Point(42, 152);
            this.cancelFilterFactorsButton.Name = "cancelFilterFactorsButton";
            this.cancelFilterFactorsButton.Size = new System.Drawing.Size(85, 26);
            this.cancelFilterFactorsButton.TabIndex = 37;
            this.cancelFilterFactorsButton.Text = "Отменить";
            this.cancelFilterFactorsButton.UseVisualStyleBackColor = true;
            this.cancelFilterFactorsButton.Click += new System.EventHandler(this.cancelFilterFactorsButton_Click);
            // 
            // acceptFilterFactorsButton
            // 
            this.acceptFilterFactorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptFilterFactorsButton.Location = new System.Drawing.Point(42, 120);
            this.acceptFilterFactorsButton.Name = "acceptFilterFactorsButton";
            this.acceptFilterFactorsButton.Size = new System.Drawing.Size(85, 26);
            this.acceptFilterFactorsButton.TabIndex = 36;
            this.acceptFilterFactorsButton.Text = "Применить";
            this.acceptFilterFactorsButton.UseVisualStyleBackColor = true;
            this.acceptFilterFactorsButton.Click += new System.EventHandler(this.acceptFilterFactorsButton_Click);
            // 
            // valueEmpWayCorr
            // 
            this.valueEmpWayCorr.DecimalPlaces = 2;
            this.valueEmpWayCorr.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.valueEmpWayCorr.Location = new System.Drawing.Point(28, 48);
            this.valueEmpWayCorr.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.valueEmpWayCorr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.valueEmpWayCorr.Name = "valueEmpWayCorr";
            this.valueEmpWayCorr.Size = new System.Drawing.Size(120, 20);
            this.valueEmpWayCorr.TabIndex = 35;
            this.valueEmpWayCorr.Value = new decimal(new int[] {
            15,
            0,
            0,
            131072});
            // 
            // classicWayRadio
            // 
            this.classicWayRadio.AutoSize = true;
            this.classicWayRadio.Location = new System.Drawing.Point(13, 83);
            this.classicWayRadio.Name = "classicWayRadio";
            this.classicWayRadio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.classicWayRadio.Size = new System.Drawing.Size(135, 17);
            this.classicWayRadio.TabIndex = 34;
            this.classicWayRadio.TabStop = true;
            this.classicWayRadio.Text = "Классический подход";
            this.classicWayRadio.UseVisualStyleBackColor = true;
            this.classicWayRadio.CheckedChanged += new System.EventHandler(this.classicWayRadio_CheckedChanged);
            // 
            // empWayRadio
            // 
            this.empWayRadio.AutoSize = true;
            this.empWayRadio.Location = new System.Drawing.Point(11, 25);
            this.empWayRadio.Name = "empWayRadio";
            this.empWayRadio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.empWayRadio.Size = new System.Drawing.Size(137, 17);
            this.empWayRadio.TabIndex = 33;
            this.empWayRadio.TabStop = true;
            this.empWayRadio.Text = "Эмпирический подход";
            this.empWayRadio.UseVisualStyleBackColor = true;
            this.empWayRadio.CheckedChanged += new System.EventHandler(this.empWayRadio_CheckedChanged);
            // 
            // labelFilterFinish
            // 
            this.labelFilterFinish.AutoSize = true;
            this.labelFilterFinish.Location = new System.Drawing.Point(652, 371);
            this.labelFilterFinish.Name = "labelFilterFinish";
            this.labelFilterFinish.Size = new System.Drawing.Size(130, 13);
            this.labelFilterFinish.TabIndex = 36;
            this.labelFilterFinish.Text = "Фильтрация выполнена";
            this.labelFilterFinish.Visible = false;
            // 
            // labelFilterLoad
            // 
            this.labelFilterLoad.AutoSize = true;
            this.labelFilterLoad.Location = new System.Drawing.Point(652, 371);
            this.labelFilterLoad.Name = "labelFilterLoad";
            this.labelFilterLoad.Size = new System.Drawing.Size(71, 13);
            this.labelFilterLoad.TabIndex = 35;
            this.labelFilterLoad.Text = "Фильтрация";
            this.labelFilterLoad.Visible = false;
            // 
            // onlyImportantFactorsDataGrid
            // 
            this.onlyImportantFactorsDataGrid.AllowUserToAddRows = false;
            this.onlyImportantFactorsDataGrid.AllowUserToDeleteRows = false;
            this.onlyImportantFactorsDataGrid.AllowUserToResizeRows = false;
            this.onlyImportantFactorsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.onlyImportantFactorsDataGrid.Location = new System.Drawing.Point(3, 3);
            this.onlyImportantFactorsDataGrid.Name = "onlyImportantFactorsDataGrid";
            this.onlyImportantFactorsDataGrid.ReadOnly = true;
            this.onlyImportantFactorsDataGrid.RowHeadersWidth = 51;
            this.onlyImportantFactorsDataGrid.Size = new System.Drawing.Size(632, 381);
            this.onlyImportantFactorsDataGrid.TabIndex = 11;
            // 
            // buildRegrEquationsTab
            // 
            this.buildRegrEquationsTab.Controls.Add(this.labelBuildingFinish);
            this.buildRegrEquationsTab.Controls.Add(this.buildEquationsButton);
            this.buildRegrEquationsTab.Controls.Add(this.labelBuildingLoad);
            this.buildRegrEquationsTab.Controls.Add(this.equationsDataGrid);
            this.buildRegrEquationsTab.Location = new System.Drawing.Point(4, 22);
            this.buildRegrEquationsTab.Name = "buildRegrEquationsTab";
            this.buildRegrEquationsTab.Size = new System.Drawing.Size(816, 390);
            this.buildRegrEquationsTab.TabIndex = 3;
            this.buildRegrEquationsTab.Text = "Построение регрессионных уравнений";
            this.buildRegrEquationsTab.UseVisualStyleBackColor = true;
            // 
            // labelBuildingFinish
            // 
            this.labelBuildingFinish.AutoSize = true;
            this.labelBuildingFinish.Location = new System.Drawing.Point(652, 371);
            this.labelBuildingFinish.Name = "labelBuildingFinish";
            this.labelBuildingFinish.Size = new System.Drawing.Size(104, 13);
            this.labelBuildingFinish.TabIndex = 39;
            this.labelBuildingFinish.Text = "Модели построены";
            this.labelBuildingFinish.Visible = false;
            // 
            // buildEquationsButton
            // 
            this.buildEquationsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buildEquationsButton.Location = new System.Drawing.Point(669, 55);
            this.buildEquationsButton.Name = "buildEquationsButton";
            this.buildEquationsButton.Size = new System.Drawing.Size(126, 66);
            this.buildEquationsButton.TabIndex = 38;
            this.buildEquationsButton.Text = "Построить регрессионные модели";
            this.buildEquationsButton.UseVisualStyleBackColor = true;
            this.buildEquationsButton.Click += new System.EventHandler(this.buildEquationsButton_Click);
            // 
            // labelBuildingLoad
            // 
            this.labelBuildingLoad.AutoSize = true;
            this.labelBuildingLoad.Location = new System.Drawing.Point(652, 371);
            this.labelBuildingLoad.Name = "labelBuildingLoad";
            this.labelBuildingLoad.Size = new System.Drawing.Size(115, 13);
            this.labelBuildingLoad.TabIndex = 37;
            this.labelBuildingLoad.Text = "Построение моделей";
            this.labelBuildingLoad.Visible = false;
            // 
            // equationsDataGrid
            // 
            this.equationsDataGrid.AllowUserToAddRows = false;
            this.equationsDataGrid.AllowUserToDeleteRows = false;
            this.equationsDataGrid.AllowUserToResizeRows = false;
            this.equationsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.equationsDataGrid.Location = new System.Drawing.Point(3, 3);
            this.equationsDataGrid.Name = "equationsDataGrid";
            this.equationsDataGrid.ReadOnly = true;
            this.equationsDataGrid.RowHeadersWidth = 51;
            this.equationsDataGrid.Size = new System.Drawing.Size(632, 381);
            this.equationsDataGrid.TabIndex = 12;
            // 
            // controlSimulationTab
            // 
            this.controlSimulationTab.Controls.Add(this.groupNumberCorrelatedIntervals);
            this.controlSimulationTab.Controls.Add(this.labelAvailableModels);
            this.controlSimulationTab.Controls.Add(this.labelSelectedModels);
            this.controlSimulationTab.Controls.Add(this.acceptControlsParametersButton);
            this.controlSimulationTab.Controls.Add(this.groupProportionOfAreaExpansion);
            this.controlSimulationTab.Controls.Add(this.groupPercentAreaExpansion);
            this.controlSimulationTab.Controls.Add(this.groupDefinitionAreaType);
            this.controlSimulationTab.Controls.Add(this.labelSelectDefAreaParams);
            this.controlSimulationTab.Controls.Add(this.labelSelectModelsForControl);
            this.controlSimulationTab.Controls.Add(this.allToAvailableModelsList);
            this.controlSimulationTab.Controls.Add(this.allToSelectModelsList);
            this.controlSimulationTab.Controls.Add(this.toAvailableModelsList);
            this.controlSimulationTab.Controls.Add(this.toSelectModelsList);
            this.controlSimulationTab.Controls.Add(this.listAvailabelModels);
            this.controlSimulationTab.Controls.Add(this.listSelectedModels);
            this.controlSimulationTab.Location = new System.Drawing.Point(4, 22);
            this.controlSimulationTab.Name = "controlSimulationTab";
            this.controlSimulationTab.Size = new System.Drawing.Size(816, 390);
            this.controlSimulationTab.TabIndex = 4;
            this.controlSimulationTab.Text = "Имитация управления";
            this.controlSimulationTab.UseVisualStyleBackColor = true;
            // 
            // groupNumberCorrelatedIntervals
            // 
            this.groupNumberCorrelatedIntervals.Controls.Add(this.numberOfCorrIntervalsManual);
            this.groupNumberCorrelatedIntervals.Controls.Add(this.manualNumberCorrIntervalRadio);
            this.groupNumberCorrelatedIntervals.Controls.Add(this.autoNumberCorrIntervalsRadio);
            this.groupNumberCorrelatedIntervals.Location = new System.Drawing.Point(530, 286);
            this.groupNumberCorrelatedIntervals.Name = "groupNumberCorrelatedIntervals";
            this.groupNumberCorrelatedIntervals.Size = new System.Drawing.Size(254, 104);
            this.groupNumberCorrelatedIntervals.TabIndex = 24;
            this.groupNumberCorrelatedIntervals.TabStop = false;
            this.groupNumberCorrelatedIntervals.Text = "Интервалы коррелированности регрессоров";
            // 
            // numberOfCorrIntervalsManual
            // 
            this.numberOfCorrIntervalsManual.Location = new System.Drawing.Point(18, 78);
            this.numberOfCorrIntervalsManual.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfCorrIntervalsManual.Name = "numberOfCorrIntervalsManual";
            this.numberOfCorrIntervalsManual.Size = new System.Drawing.Size(120, 20);
            this.numberOfCorrIntervalsManual.TabIndex = 26;
            this.numberOfCorrIntervalsManual.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numberOfCorrIntervalsManual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateKeyPressedOnlyNums);
            // 
            // manualNumberCorrIntervalRadio
            // 
            this.manualNumberCorrIntervalRadio.AutoSize = true;
            this.manualNumberCorrIntervalRadio.Location = new System.Drawing.Point(18, 55);
            this.manualNumberCorrIntervalRadio.Name = "manualNumberCorrIntervalRadio";
            this.manualNumberCorrIntervalRadio.Size = new System.Drawing.Size(191, 17);
            this.manualNumberCorrIntervalRadio.TabIndex = 1;
            this.manualNumberCorrIntervalRadio.TabStop = true;
            this.manualNumberCorrIntervalRadio.Text = "Указать количество интервалов";
            this.manualNumberCorrIntervalRadio.UseVisualStyleBackColor = true;
            this.manualNumberCorrIntervalRadio.CheckedChanged += new System.EventHandler(this.manualNumberCorrIntervalRadio_CheckedChanged);
            // 
            // autoNumberCorrIntervalsRadio
            // 
            this.autoNumberCorrIntervalsRadio.AutoSize = true;
            this.autoNumberCorrIntervalsRadio.Location = new System.Drawing.Point(18, 28);
            this.autoNumberCorrIntervalsRadio.Name = "autoNumberCorrIntervalsRadio";
            this.autoNumberCorrIntervalsRadio.Size = new System.Drawing.Size(103, 17);
            this.autoNumberCorrIntervalsRadio.TabIndex = 0;
            this.autoNumberCorrIntervalsRadio.TabStop = true;
            this.autoNumberCorrIntervalsRadio.Text = "Автоматически";
            this.autoNumberCorrIntervalsRadio.UseVisualStyleBackColor = true;
            this.autoNumberCorrIntervalsRadio.CheckedChanged += new System.EventHandler(this.autoNumberCorrIntervalsRadio_CheckedChanged);
            // 
            // labelAvailableModels
            // 
            this.labelAvailableModels.AutoSize = true;
            this.labelAvailableModels.Location = new System.Drawing.Point(248, 46);
            this.labelAvailableModels.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAvailableModels.Name = "labelAvailableModels";
            this.labelAvailableModels.Size = new System.Drawing.Size(108, 13);
            this.labelAvailableModels.TabIndex = 23;
            this.labelAvailableModels.Text = "Доступные модели:";
            // 
            // labelSelectedModels
            // 
            this.labelSelectedModels.AutoSize = true;
            this.labelSelectedModels.Location = new System.Drawing.Point(22, 46);
            this.labelSelectedModels.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSelectedModels.Name = "labelSelectedModels";
            this.labelSelectedModels.Size = new System.Drawing.Size(110, 13);
            this.labelSelectedModels.TabIndex = 22;
            this.labelSelectedModels.Text = "Выбранные модели:";
            // 
            // acceptControlsParametersButton
            // 
            this.acceptControlsParametersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptControlsParametersButton.Location = new System.Drawing.Point(423, 232);
            this.acceptControlsParametersButton.Name = "acceptControlsParametersButton";
            this.acceptControlsParametersButton.Size = new System.Drawing.Size(95, 48);
            this.acceptControlsParametersButton.TabIndex = 21;
            this.acceptControlsParametersButton.Text = "Подтвердить";
            this.acceptControlsParametersButton.UseVisualStyleBackColor = true;
            this.acceptControlsParametersButton.Click += new System.EventHandler(this.acceptControlsParametersButton_Click);
            // 
            // groupProportionOfAreaExpansion
            // 
            this.groupProportionOfAreaExpansion.Controls.Add(this.autoProportionRadio);
            this.groupProportionOfAreaExpansion.Controls.Add(this.equallyBothWaysRadio);
            this.groupProportionOfAreaExpansion.Location = new System.Drawing.Point(530, 203);
            this.groupProportionOfAreaExpansion.Name = "groupProportionOfAreaExpansion";
            this.groupProportionOfAreaExpansion.Size = new System.Drawing.Size(254, 77);
            this.groupProportionOfAreaExpansion.TabIndex = 20;
            this.groupProportionOfAreaExpansion.TabStop = false;
            this.groupProportionOfAreaExpansion.Text = "Пропорция расширения области";
            // 
            // autoProportionRadio
            // 
            this.autoProportionRadio.AutoSize = true;
            this.autoProportionRadio.Location = new System.Drawing.Point(18, 51);
            this.autoProportionRadio.Name = "autoProportionRadio";
            this.autoProportionRadio.Size = new System.Drawing.Size(103, 17);
            this.autoProportionRadio.TabIndex = 1;
            this.autoProportionRadio.TabStop = true;
            this.autoProportionRadio.Text = "Автоматически";
            this.autoProportionRadio.UseVisualStyleBackColor = true;
            this.autoProportionRadio.CheckedChanged += new System.EventHandler(this.autoProportionRadio_CheckedChanged);
            // 
            // equallyBothWaysRadio
            // 
            this.equallyBothWaysRadio.AutoSize = true;
            this.equallyBothWaysRadio.Location = new System.Drawing.Point(18, 28);
            this.equallyBothWaysRadio.Name = "equallyBothWaysRadio";
            this.equallyBothWaysRadio.Size = new System.Drawing.Size(156, 17);
            this.equallyBothWaysRadio.TabIndex = 0;
            this.equallyBothWaysRadio.TabStop = true;
            this.equallyBothWaysRadio.Text = "В обе стороны одинаково";
            this.equallyBothWaysRadio.UseVisualStyleBackColor = true;
            this.equallyBothWaysRadio.CheckedChanged += new System.EventHandler(this.equallyBothWaysRadio_CheckedChanged);
            // 
            // groupPercentAreaExpansion
            // 
            this.groupPercentAreaExpansion.Controls.Add(this.percentAreaExpansion);
            this.groupPercentAreaExpansion.Location = new System.Drawing.Point(530, 140);
            this.groupPercentAreaExpansion.Name = "groupPercentAreaExpansion";
            this.groupPercentAreaExpansion.Size = new System.Drawing.Size(254, 57);
            this.groupPercentAreaExpansion.TabIndex = 19;
            this.groupPercentAreaExpansion.TabStop = false;
            this.groupPercentAreaExpansion.Text = "Процент расширения области";
            // 
            // percentAreaExpansion
            // 
            this.percentAreaExpansion.Location = new System.Drawing.Point(18, 30);
            this.percentAreaExpansion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.percentAreaExpansion.Name = "percentAreaExpansion";
            this.percentAreaExpansion.Size = new System.Drawing.Size(120, 20);
            this.percentAreaExpansion.TabIndex = 25;
            this.percentAreaExpansion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.percentAreaExpansion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateKeyPressedOnlyNums);
            // 
            // groupDefinitionAreaType
            // 
            this.groupDefinitionAreaType.Controls.Add(this.symbiosisAreaRadio);
            this.groupDefinitionAreaType.Controls.Add(this.theoreticalAreaRadio);
            this.groupDefinitionAreaType.Controls.Add(this.empDefAreaRadio);
            this.groupDefinitionAreaType.Location = new System.Drawing.Point(530, 37);
            this.groupDefinitionAreaType.Name = "groupDefinitionAreaType";
            this.groupDefinitionAreaType.Size = new System.Drawing.Size(254, 97);
            this.groupDefinitionAreaType.TabIndex = 18;
            this.groupDefinitionAreaType.TabStop = false;
            this.groupDefinitionAreaType.Text = "Вид области определения";
            // 
            // symbiosisAreaRadio
            // 
            this.symbiosisAreaRadio.AutoSize = true;
            this.symbiosisAreaRadio.Location = new System.Drawing.Point(18, 74);
            this.symbiosisAreaRadio.Name = "symbiosisAreaRadio";
            this.symbiosisAreaRadio.Size = new System.Drawing.Size(120, 17);
            this.symbiosisAreaRadio.TabIndex = 2;
            this.symbiosisAreaRadio.TabStop = true;
            this.symbiosisAreaRadio.Text = "Симбиоз областей";
            this.symbiosisAreaRadio.UseVisualStyleBackColor = true;
            this.symbiosisAreaRadio.CheckedChanged += new System.EventHandler(this.symbiosisAreaRadio_CheckedChanged);
            // 
            // theoreticalAreaRadio
            // 
            this.theoreticalAreaRadio.AutoSize = true;
            this.theoreticalAreaRadio.Location = new System.Drawing.Point(18, 51);
            this.theoreticalAreaRadio.Name = "theoreticalAreaRadio";
            this.theoreticalAreaRadio.Size = new System.Drawing.Size(146, 17);
            this.theoreticalAreaRadio.TabIndex = 1;
            this.theoreticalAreaRadio.TabStop = true;
            this.theoreticalAreaRadio.Text = "Теоретическая область";
            this.theoreticalAreaRadio.UseVisualStyleBackColor = true;
            this.theoreticalAreaRadio.CheckedChanged += new System.EventHandler(this.theoreticalAreaRadio_CheckedChanged);
            // 
            // empDefAreaRadio
            // 
            this.empDefAreaRadio.AutoSize = true;
            this.empDefAreaRadio.Location = new System.Drawing.Point(18, 28);
            this.empDefAreaRadio.Name = "empDefAreaRadio";
            this.empDefAreaRadio.Size = new System.Drawing.Size(143, 17);
            this.empDefAreaRadio.TabIndex = 0;
            this.empDefAreaRadio.TabStop = true;
            this.empDefAreaRadio.Text = "Эмпирическая область";
            this.empDefAreaRadio.UseVisualStyleBackColor = true;
            this.empDefAreaRadio.CheckedChanged += new System.EventHandler(this.empDefAreaRadio_CheckedChanged);
            // 
            // labelSelectDefAreaParams
            // 
            this.labelSelectDefAreaParams.AutoSize = true;
            this.labelSelectDefAreaParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSelectDefAreaParams.Location = new System.Drawing.Point(527, 13);
            this.labelSelectDefAreaParams.Name = "labelSelectDefAreaParams";
            this.labelSelectDefAreaParams.Size = new System.Drawing.Size(279, 15);
            this.labelSelectDefAreaParams.TabIndex = 17;
            this.labelSelectDefAreaParams.Text = "Настройка параметров области определения:";
            // 
            // labelSelectModelsForControl
            // 
            this.labelSelectModelsForControl.AutoSize = true;
            this.labelSelectModelsForControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSelectModelsForControl.Location = new System.Drawing.Point(20, 13);
            this.labelSelectModelsForControl.Name = "labelSelectModelsForControl";
            this.labelSelectModelsForControl.Size = new System.Drawing.Size(197, 15);
            this.labelSelectModelsForControl.TabIndex = 16;
            this.labelSelectModelsForControl.Text = "Выбор моделей для управления:";
            // 
            // allToAvailableModelsList
            // 
            this.allToAvailableModelsList.Location = new System.Drawing.Point(202, 194);
            this.allToAvailableModelsList.Name = "allToAvailableModelsList";
            this.allToAvailableModelsList.Size = new System.Drawing.Size(29, 24);
            this.allToAvailableModelsList.TabIndex = 15;
            this.allToAvailableModelsList.Text = ">>";
            this.allToAvailableModelsList.UseVisualStyleBackColor = true;
            this.allToAvailableModelsList.Click += new System.EventHandler(this.allToAvailableModelsList_Click);
            // 
            // allToSelectModelsList
            // 
            this.allToSelectModelsList.Location = new System.Drawing.Point(202, 164);
            this.allToSelectModelsList.Name = "allToSelectModelsList";
            this.allToSelectModelsList.Size = new System.Drawing.Size(29, 24);
            this.allToSelectModelsList.TabIndex = 14;
            this.allToSelectModelsList.Text = "<<";
            this.allToSelectModelsList.UseVisualStyleBackColor = true;
            this.allToSelectModelsList.Click += new System.EventHandler(this.allToSelectModelsList_Click);
            // 
            // toAvailableModelsList
            // 
            this.toAvailableModelsList.Location = new System.Drawing.Point(202, 110);
            this.toAvailableModelsList.Name = "toAvailableModelsList";
            this.toAvailableModelsList.Size = new System.Drawing.Size(29, 24);
            this.toAvailableModelsList.TabIndex = 13;
            this.toAvailableModelsList.Text = ">";
            this.toAvailableModelsList.UseVisualStyleBackColor = true;
            this.toAvailableModelsList.Click += new System.EventHandler(this.toAvailableModelsList_Click);
            // 
            // toSelectModelsList
            // 
            this.toSelectModelsList.Location = new System.Drawing.Point(202, 80);
            this.toSelectModelsList.Name = "toSelectModelsList";
            this.toSelectModelsList.Size = new System.Drawing.Size(29, 24);
            this.toSelectModelsList.TabIndex = 12;
            this.toSelectModelsList.Text = "<";
            this.toSelectModelsList.UseVisualStyleBackColor = true;
            this.toSelectModelsList.Click += new System.EventHandler(this.toSelectModelsList_Click);
            // 
            // listAvailabelModels
            // 
            this.listAvailabelModels.FormattingEnabled = true;
            this.listAvailabelModels.HorizontalScrollbar = true;
            this.listAvailabelModels.Location = new System.Drawing.Point(251, 61);
            this.listAvailabelModels.Margin = new System.Windows.Forms.Padding(2);
            this.listAvailabelModels.Name = "listAvailabelModels";
            this.listAvailabelModels.Size = new System.Drawing.Size(160, 316);
            this.listAvailabelModels.TabIndex = 3;
            this.listAvailabelModels.DoubleClick += new System.EventHandler(this.listAvailabelModels_DoubleClick);
            // 
            // listSelectedModels
            // 
            this.listSelectedModels.FormattingEnabled = true;
            this.listSelectedModels.HorizontalScrollbar = true;
            this.listSelectedModels.Location = new System.Drawing.Point(23, 61);
            this.listSelectedModels.Margin = new System.Windows.Forms.Padding(2);
            this.listSelectedModels.Name = "listSelectedModels";
            this.listSelectedModels.Size = new System.Drawing.Size(160, 316);
            this.listSelectedModels.TabIndex = 2;
            this.listSelectedModels.DoubleClick += new System.EventHandler(this.listSelectedModels_DoubleClick);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WorkFileMenuItem2,
            this.ExitAppMenu,
            this.helpAllStepsMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(1004, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // WorkFileMenuItem2
            // 
            this.WorkFileMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileMenu});
            this.WorkFileMenuItem2.Name = "WorkFileMenuItem2";
            this.WorkFileMenuItem2.Size = new System.Drawing.Size(48, 20);
            this.WorkFileMenuItem2.Text = "Файл";
            // 
            // OpenFileMenu
            // 
            this.OpenFileMenu.Name = "OpenFileMenu";
            this.OpenFileMenu.Size = new System.Drawing.Size(121, 22);
            this.OpenFileMenu.Text = "Открыть";
            this.OpenFileMenu.Click += new System.EventHandler(this.OpenFileMenu_Click);
            // 
            // ExitAppMenu
            // 
            this.ExitAppMenu.Name = "ExitAppMenu";
            this.ExitAppMenu.Size = new System.Drawing.Size(54, 20);
            this.ExitAppMenu.Text = "Выход";
            this.ExitAppMenu.Click += new System.EventHandler(this.ExitAppMenu_Click);
            // 
            // helpAllStepsMenu
            // 
            this.helpAllStepsMenu.Name = "helpAllStepsMenu";
            this.helpAllStepsMenu.Size = new System.Drawing.Size(68, 20);
            this.helpAllStepsMenu.Text = "Помощь";
            // 
            // regressantsList
            // 
            this.regressantsList.FormattingEnabled = true;
            this.regressantsList.HorizontalScrollbar = true;
            this.regressantsList.Location = new System.Drawing.Point(12, 62);
            this.regressantsList.Name = "regressantsList";
            this.regressantsList.Size = new System.Drawing.Size(159, 108);
            this.regressantsList.TabIndex = 5;
            // 
            // labelregressantsList
            // 
            this.labelregressantsList.AutoSize = true;
            this.labelregressantsList.Location = new System.Drawing.Point(12, 46);
            this.labelregressantsList.Name = "labelregressantsList";
            this.labelregressantsList.Size = new System.Drawing.Size(130, 13);
            this.labelregressantsList.TabIndex = 6;
            this.labelregressantsList.Text = "Управляемые факторы:";
            // 
            // regressorsList
            // 
            this.regressorsList.FormattingEnabled = true;
            this.regressorsList.HorizontalScrollbar = true;
            this.regressorsList.Location = new System.Drawing.Point(12, 208);
            this.regressorsList.Name = "regressorsList";
            this.regressorsList.Size = new System.Drawing.Size(159, 225);
            this.regressorsList.TabIndex = 7;
            // 
            // labelRegressorsList
            // 
            this.labelRegressorsList.AutoSize = true;
            this.labelRegressorsList.Location = new System.Drawing.Point(12, 192);
            this.labelRegressorsList.Name = "labelRegressorsList";
            this.labelRegressorsList.Size = new System.Drawing.Size(131, 13);
            this.labelRegressorsList.TabIndex = 8;
            this.labelRegressorsList.Text = "Управляющие факторы:";
            // 
            // groupTaskType
            // 
            this.groupTaskType.Controls.Add(this.radioPredictionTask);
            this.groupTaskType.Controls.Add(this.radioControlTask);
            this.groupTaskType.Location = new System.Drawing.Point(663, 196);
            this.groupTaskType.Name = "groupTaskType";
            this.groupTaskType.Size = new System.Drawing.Size(138, 69);
            this.groupTaskType.TabIndex = 17;
            this.groupTaskType.TabStop = false;
            this.groupTaskType.Text = "Тип задачи";
            // 
            // radioControlTask
            // 
            this.radioControlTask.AutoSize = true;
            this.radioControlTask.Location = new System.Drawing.Point(6, 23);
            this.radioControlTask.Name = "radioControlTask";
            this.radioControlTask.Size = new System.Drawing.Size(87, 17);
            this.radioControlTask.TabIndex = 0;
            this.radioControlTask.TabStop = true;
            this.radioControlTask.Text = "Управление";
            this.radioControlTask.UseVisualStyleBackColor = true;
            this.radioControlTask.CheckedChanged += new System.EventHandler(this.radioControlTask_CheckedChanged);
            // 
            // radioPredictionTask
            // 
            this.radioPredictionTask.AutoSize = true;
            this.radioPredictionTask.Location = new System.Drawing.Point(6, 46);
            this.radioPredictionTask.Name = "radioPredictionTask";
            this.radioPredictionTask.Size = new System.Drawing.Size(116, 17);
            this.radioPredictionTask.TabIndex = 1;
            this.radioPredictionTask.TabStop = true;
            this.radioPredictionTask.Text = "Прогнозирование";
            this.radioPredictionTask.UseVisualStyleBackColor = true;
            this.radioPredictionTask.CheckedChanged += new System.EventHandler(this.radioPredictionTask_CheckedChanged);
            // 
            // predictionTab
            // 
            this.predictionTab.Controls.Add(this.loadDataForPredictButton);
            this.predictionTab.Controls.Add(this.predictionMetricsDataGrid);
            this.predictionTab.Controls.Add(this.realPredictValuesDataGrid);
            this.predictionTab.Location = new System.Drawing.Point(4, 22);
            this.predictionTab.Name = "predictionTab";
            this.predictionTab.Size = new System.Drawing.Size(816, 390);
            this.predictionTab.TabIndex = 7;
            this.predictionTab.Text = "Прогнозирование";
            this.predictionTab.UseVisualStyleBackColor = true;
            // 
            // realPredictValuesDataGrid
            // 
            this.realPredictValuesDataGrid.AllowUserToAddRows = false;
            this.realPredictValuesDataGrid.AllowUserToDeleteRows = false;
            this.realPredictValuesDataGrid.AllowUserToResizeRows = false;
            this.realPredictValuesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.realPredictValuesDataGrid.Location = new System.Drawing.Point(3, 3);
            this.realPredictValuesDataGrid.Name = "realPredictValuesDataGrid";
            this.realPredictValuesDataGrid.ReadOnly = true;
            this.realPredictValuesDataGrid.RowHeadersWidth = 51;
            this.realPredictValuesDataGrid.Size = new System.Drawing.Size(632, 265);
            this.realPredictValuesDataGrid.TabIndex = 13;
            // 
            // predictionMetricsDataGrid
            // 
            this.predictionMetricsDataGrid.AllowUserToAddRows = false;
            this.predictionMetricsDataGrid.AllowUserToDeleteRows = false;
            this.predictionMetricsDataGrid.AllowUserToResizeRows = false;
            this.predictionMetricsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.predictionMetricsDataGrid.Location = new System.Drawing.Point(3, 274);
            this.predictionMetricsDataGrid.Name = "predictionMetricsDataGrid";
            this.predictionMetricsDataGrid.ReadOnly = true;
            this.predictionMetricsDataGrid.RowHeadersWidth = 51;
            this.predictionMetricsDataGrid.Size = new System.Drawing.Size(632, 113);
            this.predictionMetricsDataGrid.TabIndex = 14;
            // 
            // loadDataForPredictButton
            // 
            this.loadDataForPredictButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadDataForPredictButton.Location = new System.Drawing.Point(669, 55);
            this.loadDataForPredictButton.Name = "loadDataForPredictButton";
            this.loadDataForPredictButton.Size = new System.Drawing.Size(124, 66);
            this.loadDataForPredictButton.TabIndex = 15;
            this.loadDataForPredictButton.Text = "Загрузить данные для прогноза";
            this.loadDataForPredictButton.UseVisualStyleBackColor = true;
            this.loadDataForPredictButton.Click += new System.EventHandler(this.loadDataForPredictButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 444);
            this.Controls.Add(this.labelRegressorsList);
            this.Controls.Add(this.regressorsList);
            this.Controls.Add(this.labelregressantsList);
            this.Controls.Add(this.regressantsList);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.allTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1020, 482);
            this.Name = "MainForm";
            this.Text = "Многомерная Линейная Регрессия";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.allTabs.ResumeLayout(false);
            this.loadDataTab.ResumeLayout(false);
            this.loadDataTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.factorsData)).EndInit();
            this.processingStatDataTabGusev.ResumeLayout(false);
            this.processingStatDataTabGusev.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.functionsForProcessingGusevDataGrid)).EndInit();
            this.processingStatDataTabOkunev.ResumeLayout(false);
            this.processingStatDataTabOkunev.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.functionsForProcessingOkunevDataGrid)).EndInit();
            this.formationOfControlFactorSetsTab.ResumeLayout(false);
            this.formationOfControlFactorSetsTab.PerformLayout();
            this.groupBoxGroupedRegressors.ResumeLayout(false);
            this.groupBoxGroupedRegressors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxCorrelBtwRegressors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupedRegressorsDataGrid)).EndInit();
            this.removeUnimportantFactorsTab.ResumeLayout(false);
            this.removeUnimportantFactorsTab.PerformLayout();
            this.groupBoxFilterRegressors.ResumeLayout(false);
            this.groupBoxFilterRegressors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valueEmpWayCorr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.onlyImportantFactorsDataGrid)).EndInit();
            this.buildRegrEquationsTab.ResumeLayout(false);
            this.buildRegrEquationsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.equationsDataGrid)).EndInit();
            this.controlSimulationTab.ResumeLayout(false);
            this.controlSimulationTab.PerformLayout();
            this.groupNumberCorrelatedIntervals.ResumeLayout(false);
            this.groupNumberCorrelatedIntervals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfCorrIntervalsManual)).EndInit();
            this.groupProportionOfAreaExpansion.ResumeLayout(false);
            this.groupProportionOfAreaExpansion.PerformLayout();
            this.groupPercentAreaExpansion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.percentAreaExpansion)).EndInit();
            this.groupDefinitionAreaType.ResumeLayout(false);
            this.groupDefinitionAreaType.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupTaskType.ResumeLayout(false);
            this.groupTaskType.PerformLayout();
            this.predictionTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.realPredictValuesDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.predictionMetricsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem WorkFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFile;
        private System.Windows.Forms.ToolStripMenuItem ExitApp;
        private System.Windows.Forms.ToolStripMenuItem helpAllSteps;
        private System.Windows.Forms.TabControl allTabs;
        private System.Windows.Forms.TabPage loadDataTab;
        private System.Windows.Forms.TabPage processingStatDataTabGusev;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem WorkFileMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem OpenFileMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitAppMenu;
        private System.Windows.Forms.ToolStripMenuItem helpAllStepsMenu;
        private System.Windows.Forms.DataGridView factorsData;
        private System.Windows.Forms.ListBox regressantsList;
        private System.Windows.Forms.Label labelregressantsList;
        private System.Windows.Forms.ListBox regressorsList;
        private System.Windows.Forms.Label labelRegressorsList;
        private System.Windows.Forms.ProgressBar progressBarDataLoad;
        private System.Windows.Forms.Button acceptFactorsButton;
        private System.Windows.Forms.Button selectRegressorsButton;
        private System.Windows.Forms.Button selectRegressantsButton;
        private System.Windows.Forms.Button clearSelectedFactorsButton;
        private System.Windows.Forms.CheckBox checkPairwiseCombinations;
        private System.Windows.Forms.Button doFunctionalProcessGusevButton;
        private System.Windows.Forms.DataGridView functionsForProcessingGusevDataGrid;
        private System.Windows.Forms.Label labelFuncPreprocessGusev;
        private System.Windows.Forms.Label labelResultDataLoad;
        private System.Windows.Forms.Label labelPreprocessingGusevFinish;
        private System.Windows.Forms.TabPage removeUnimportantFactorsTab;
        private System.Windows.Forms.DataGridView onlyImportantFactorsDataGrid;
        private System.Windows.Forms.Label labelFilterLoad;
        private System.Windows.Forms.Label labelFilterFinish;
        private System.Windows.Forms.TabPage buildRegrEquationsTab;
        private System.Windows.Forms.Label labelBuildingFinish;
        private System.Windows.Forms.Button buildEquationsButton;
        private System.Windows.Forms.Label labelBuildingLoad;
        private System.Windows.Forms.DataGridView equationsDataGrid;
        private System.Windows.Forms.TabPage controlSimulationTab;
        private System.Windows.Forms.ListBox listSelectedModels;
        private System.Windows.Forms.ListBox listAvailabelModels;
        private System.Windows.Forms.Label labelSelectModelsForControl;
        private System.Windows.Forms.Button allToAvailableModelsList;
        private System.Windows.Forms.Button allToSelectModelsList;
        private System.Windows.Forms.Button toAvailableModelsList;
        private System.Windows.Forms.Button toSelectModelsList;
        private System.Windows.Forms.GroupBox groupPercentAreaExpansion;
        private System.Windows.Forms.GroupBox groupDefinitionAreaType;
        private System.Windows.Forms.RadioButton symbiosisAreaRadio;
        private System.Windows.Forms.RadioButton theoreticalAreaRadio;
        private System.Windows.Forms.RadioButton empDefAreaRadio;
        private System.Windows.Forms.Label labelSelectDefAreaParams;
        private System.Windows.Forms.NumericUpDown percentAreaExpansion;
        private System.Windows.Forms.GroupBox groupProportionOfAreaExpansion;
        private System.Windows.Forms.RadioButton autoProportionRadio;
        private System.Windows.Forms.RadioButton equallyBothWaysRadio;
        private System.Windows.Forms.Button acceptControlsParametersButton;
        private System.Windows.Forms.ToolTip toolTipSymbiosis;
        private System.Windows.Forms.ToolTip toolTipAutoProportion;
        private System.Windows.Forms.ToolTip toolTipPercentAreaExpansion;
        private System.Windows.Forms.Label labelSelectedModels;
        private System.Windows.Forms.Label labelAvailableModels;
        private System.Windows.Forms.TabPage formationOfControlFactorSetsTab;
        private System.Windows.Forms.Label labelMaxCorrelBtwRegressors;
        private System.Windows.Forms.NumericUpDown maxCorrelBtwRegressors;
        private System.Windows.Forms.Label labelGroupingRegressorsEnd;
        private System.Windows.Forms.Label labelGroupingRegressors;
        private System.Windows.Forms.DataGridView groupedRegressorsDataGrid;
        private System.Windows.Forms.Button groupedRegressorsButton;
        private System.Windows.Forms.GroupBox groupBoxGroupedRegressors;
        private System.Windows.Forms.GroupBox groupBoxFilterRegressors;
        private System.Windows.Forms.Button cancelFilterFactorsButton;
        private System.Windows.Forms.Button acceptFilterFactorsButton;
        private System.Windows.Forms.NumericUpDown valueEmpWayCorr;
        private System.Windows.Forms.RadioButton classicWayRadio;
        private System.Windows.Forms.RadioButton empWayRadio;
        private System.Windows.Forms.GroupBox groupNumberCorrelatedIntervals;
        private System.Windows.Forms.NumericUpDown numberOfCorrIntervalsManual;
        private System.Windows.Forms.RadioButton manualNumberCorrIntervalRadio;
        private System.Windows.Forms.RadioButton autoNumberCorrIntervalsRadio;
        private System.Windows.Forms.TabPage processingStatDataTabOkunev;
        private System.Windows.Forms.Label labelPreprocessingOkunevFinish;
        private System.Windows.Forms.Label labelFuncPreprocessOkunev;
        private System.Windows.Forms.Button doFunctionalProcessOkunevButton;
        private System.Windows.Forms.DataGridView functionsForProcessingOkunevDataGrid;
        private System.Windows.Forms.GroupBox groupTaskType;
        private System.Windows.Forms.RadioButton radioPredictionTask;
        private System.Windows.Forms.RadioButton radioControlTask;
        private System.Windows.Forms.TabPage predictionTab;
        private System.Windows.Forms.DataGridView predictionMetricsDataGrid;
        private System.Windows.Forms.DataGridView realPredictValuesDataGrid;
        private System.Windows.Forms.Button loadDataForPredictButton;
    }
}

