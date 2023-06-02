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
        private BackgroundWorker resizeWorker = new BackgroundWorker();

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

            // Fill last value for each regressor as default value
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
                regressantsResultDataGrid.Rows.Add(new string[] {model.RegressantName, model.RegressantValues.Last().ToString(),
                    model.Equation});
            }

            // Perform recalculation of regressants
            for (int i = 0; i < Models.Count; i++) {
                regressantsResultDataGrid[1, i].Value = CalcModelValue(Models[i]);
            }
            //CalcRegressantsValue();

            helpImitationContorl.ToolTipText = StepsInfo.ImitationOfControlForm;
        }

        private double CalcModelValue(Model model) {
            // Fill new X values for model
            double[] xValues = new double[model.Regressors.Count];
            int position = 0;
            foreach(var regressor in model.Regressors) {
                xValues[position] = AllRegressors[regressor.Key];
                position++;
            }

            // Get predicted value for regressant
            return model.Predict(xValues);
        }

        private void CalcRegressantsValue() {
            for(int i = 0; i < Models.Count; i++) {
                // Fill new X values for each model
                int position = 0;
                double[] xValues = new double[Models[i].Regressors.Count];
                foreach (var regressor in Models[i].Regressors) {
                    xValues[position] = AllRegressors[regressor.Key];
                    position++;
                }

                // Get predicted value for regressant
                double newRegressantValue = Models[i].Predict(xValues);

                regressantsResultDataGrid[1, i].Value = newRegressantValue;
            }
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
