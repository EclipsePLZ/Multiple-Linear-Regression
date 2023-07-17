using Multiple_Linear_Regression.Work_WIth_Files.Interfaces;
using Multiple_Linear_Regression.Work_WIth_Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Multiple_Linear_Regression.Work_With_Files;

namespace Multiple_Linear_Regression.Forms {
    public partial class SimulationControlForm : Form {
        private IFileService fileService;
        private IDialogService dialogService = new DefaultDialogService();

        SimulationControl simulationControl = new SimulationControl();

        private const int MODIFY_COLUMN = 1;

        private List<Model> Models { get; set; }

        private Dictionary<string, double> AllRegressors { get; set; }

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
        }

        /// <summary>
        /// Setting start parameters for form
        /// </summary>
        private void SetStartParameters() {
            OperationsWithControls.SetDataGVColumnHeaders(new List<string>() { "Название", "Значение", "Минимум", "Максимум" }, 
                                                    regressorsSetDataGrid, true, null, new List<int>() { 1 });

            OperationsWithControls.SetDataGVColumnHeaders(new List<string>() { "Название", "Значение", "Уравнение" },
                                                    regressantsResultDataGrid, true);

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
                    Math.Round(OperationsWithModels.CalcModelValue(model, AllRegressors), 2).ToString(),
                    model.Equation});
            }

            helpImitationContorl.ToolTipText = StepsInfo.ImitationOfControlForm;
            loadDataFileMenu.ToolTipText = StepsInfo.ImitationRegressorControlOpenFile;

            if (NumberGroupOfCorrelatedRegressors == 0) {
                NumberGroupOfCorrelatedRegressors = simulationControl.CalcNumberGroupOfCorrelatedRegressors(AllRegressors.Count);
            }

            // Calculate correlation between regressors
            RegressorsCorrelation = simulationControl.CalcCorrelationCoefficients(SelectedStartRegressors);

            // Find coefficients of regressors mutual impact
            RegressorsImpact = simulationControl.GetRegressorsMutualImpact(RegressorsCorrelation, NumberGroupOfCorrelatedRegressors,
                                                                           SelectedStartRegressors.Keys.ToList(), SelectedStartRegressors);
        }

        /// <summary>
        /// Open file (csv, excel) with data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadDataFileMenu_Click(object sender, EventArgs e) {
            try {
                if (dialogService.OpenFileDialog() == true) {
                    fileService = Files.GetFileService(dialogService.FilePath);

                    List<List<string>> allRows = new List<List<string>>();
                    try {
                        allRows = fileService.Open(dialogService.FilePath);
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                    }
                    finally {
                        //RegressorsFromFile(allRows);
                        // Show predicted regressants with regressors values
                        FileRegressors fileRegressorsForm = new FileRegressors(AllRegressors.Keys.ToList(), Models, allRows,
                            "Имитация управления", StepsInfo.ImitationRegressorsFromFile);
                        fileRegressorsForm.ShowDialog();
                    }
                }
            }
            catch (Exception ex) {
                dialogService.ShowMessage(ex.Message);
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
                            simulationControl.CheckDefAreaForRegressor(regressorName, regressorValue, 
                                                                       RegressorsDefinitionArea[regressorName]);
                            AllRegressors[regressorName] = regressorValue;

                            // Update models, that contains regressor
                            UpdateModels(OperationsWithModels.GetModelsByRegressors(regressorName, Models));

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
        /// Update models predicted values
        /// </summary>
        /// <param name="models">List of models for update</param>
        private void UpdateModels(List<Model> models) {
            foreach (var model in models) {
                regressantsResultDataGrid[1, Models.IndexOf(model)].Value = 
                    Math.Round(OperationsWithModels.CalcModelValue(model, AllRegressors), 2);
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
        /// <param name="pariwiseCombinationRegressorName">Name of pairwise combination factor</param>
        private void UpdatePairwiseCombination(string pariwiseCombinationRegressorName) {
            List<string> regressorsNames = new List<string>(AllRegressors.Keys);

            // Find the regressors that form the combination
            string[] combinedRegressors = pariwiseCombinationRegressorName.Split(new string[] { " & " }, StringSplitOptions.None);

            // Update combination of regressors
            double newValue = AllRegressors[combinedRegressors[0]] * AllRegressors[combinedRegressors[1]];
            AllRegressors[pariwiseCombinationRegressorName] = newValue;
            regressorsSetDataGrid[MODIFY_COLUMN, regressorsNames.IndexOf(pariwiseCombinationRegressorName)].Value = Math.Round(newValue, 2);

            // Check value of pairwise combination regressor
            simulationControl.CheckDefAreaForRegressor(pariwiseCombinationRegressorName, newValue,
                                                       RegressorsDefinitionArea[pariwiseCombinationRegressorName]);

            // Update models that contains combined regressorKey
            UpdateModels(OperationsWithModels.GetModelsByRegressors(pariwiseCombinationRegressorName, Models));
        }

        /// <summary>
        /// Update regressors affected by changable regressor
        /// </summary>
        /// <param name="changableRegressorName">Changable regressor name</param>
        private void UpdateImpactRegressors(string changableRegressorName) {
            List<string> needUpdatedRegressors = new List<string>(SelectedStartRegressors.Keys);
            simulationControl.UpdateRegressors(AllRegressors, changableRegressorName, needUpdatedRegressors, RegressorsImpact);

            List<string> regressorsNames = new List<string>(AllRegressors.Keys);

            // Update regressors values in dataGridView
            foreach(var regressorName in SelectedStartRegressors.Keys) {
                regressorsSetDataGrid[MODIFY_COLUMN, regressorsNames.IndexOf(regressorName)].Value =
                    Math.Round(AllRegressors[regressorName], 2);
            }

            // Update all models
            UpdateModels(Models);
        }

        private void exitFormMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
