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

        private Dictionary<string, double> AllRegressors { get; set; } = new Dictionary<string, double>();

        private Dictionary<string, (double, double)> RegressorsDefinitionArea { get; set; } = new Dictionary<string, (double, double)>();

        private Func<IEnumerable<double>, (double, double)> GetDefinitionArea { get; }

        public SimulationControlForm(List<Model> models, Func<IEnumerable<double>, (double, double)> getDefinitionAreaFunc) {
            Models = models;
            GetDefinitionArea = getDefinitionAreaFunc;

            InitializeComponent();

            // Centered Form on the screen
            this.CenterToScreen();

            SetStartParameters();
            loadDataFileMenu.ToolTipText = StepsInfo.ImitationRegressorControlOpenFile;

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

            // Fill last value for each regressorKey as default value
            foreach (var model in Models) {
                foreach(var regressor in model.StartRegressors) {
                    if (!AllRegressors.Keys.Contains(regressor.Key)) {
                        AllRegressors.Add(regressor.Key, regressor.Value.Last());
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
                List<double> regressorValues = new List<double>();
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
                        value = allRegressorsFromFile[pairwiseRegressors[0]][row] * allRegressorsFromFile[pairwiseRegressors[1]][row];
                    }
                    else {
                        value = allRegressorsFromFile[regressorName][row];
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


            //List<string> regressorsHeaders = new List<string>(allRows[0]);

            //predictedRegressants.Add(new List<string>(regressorsHeaders));

            //// Add pairwise combinations names
            //foreach(var regressorName in AllRegressors.Keys) {
            //    if (regressorName.Contains(" & ")) {
            //        predictedRegressants[0].Add(regressorName);
            //    }
            //}

            //// Add regressant name
            //foreach(var model in Models) {
            //    predictedRegressants[0].Add(model.RegressantName);
            //}

            //for (int row = 1; row < allRows.Count; row++) {
            //    Dictionary<string, double> regressorsRow = new Dictionary<string, double>();

            //    //// Add all regressors names
            //    //foreach(var key in AllRegressors.Keys) {
            //    //    regressorsRow.Add(key, 0);
            //    //}
                
            //    // Add values for each regressor
            //    for (int i = 0; i < allRows[row].Count; i++) {
            //        regressorsRow.Add(regressorsHeaders[i], Convert.ToDouble(allRows[row][i]));
            //        //regressorsRow[regressorsHeaders[i]] = Convert.ToDouble(allRows[row][i]);
            //    }

            //    // Add values of pairwise combinations regressors
            //    for (int i = 0; i < regressorsHeaders.Count - 1; i++) {
            //        for (int j = i + 1; j < regressorsHeaders.Count; j++) {
            //            regressorsRow.Add
            //        }
            //    }

            //    // Create next row of values
            //    List<string> allValues = new List<string>();

            //    // Write regressors values
            //    foreach(var value in regressorsRow.Values) {
            //        allValues.Add(value.ToString());
            //    }

            //    // Write predicted regressants values
            //    foreach (var model in Models) {
            //        double predictedValue = CalcModelValue(model, regressorsRow);
            //        allValues.Add(predictedValue.ToString());
            //    }

            //    predictedRegressants.Add(allValues);
            //}

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

        private void regressorsSetDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            if (regressorsSetDataGrid.CurrentCell.ColumnIndex == MODIFY_COLUMN) {
                regressorsSetDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
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

                            UpdatePairwiseCombinations(regressorName);

                            // Calc predicted value for modified model
                            foreach (var model in GetModifiedModels(regressorsSetDataGrid[0, e.RowIndex].Value.ToString())) {
                                regressantsResultDataGrid[1, Models.IndexOf(model)].Value = CalcModelValue(model);
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
        /// Update pairwise combinations that contains adjustable regressor
        /// </summary>
        /// <param name="regressorName">Name of adjustable regressor</param>
        private void UpdatePairwiseCombinations(string regressorName) {
            List<string> regressorsNames = new List<string>(AllRegressors.Keys.ToList());
            foreach(var regressorKey in regressorsNames) {

                // Check if it's combination of regressors
                if (regressorKey.Contains(regressorName) && regressorKey.Contains(" & ")) {

                    // Find the regressors that form the combination
                    string[] combinedRegressors = regressorKey.Split(new string[] {" & "}, StringSplitOptions.None);

                    // Update combination of regressors
                    double newValue = AllRegressors[combinedRegressors[0]] * AllRegressors[combinedRegressors[1]];
                    AllRegressors[regressorKey] = newValue;
                    regressorsSetDataGrid[MODIFY_COLUMN, regressorsNames.IndexOf(regressorKey)].Value = newValue;

                    // Check value of pairwise combination regressor
                    CheckRegressorDefArea(regressorKey, newValue);

                    // Update model that contains combined regressorKey
                    foreach (var model in GetModifiedModels(regressorKey)) {
                        regressantsResultDataGrid[1, Models.IndexOf(model)].Value = CalcModelValue(model);
                    }
                }
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


                        isResizeNeeded = false;
                    }
                }
            }
        }
    }
}
