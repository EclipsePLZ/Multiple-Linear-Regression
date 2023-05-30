using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiple_Linear_Regression.Forms {
    public partial class SimulationControlForm : Form {
        private BackgroundWorker resizeWorker = new BackgroundWorker();

        private bool isResizeNeeded = false;

        private List<Model> Models { get; set; }

        private Dictionary<string, double> AllRegressors { get; set; } = new Dictionary<string, double>();

        public SimulationControlForm(List<Model> models) {
            Models = models;

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

                }
            }

            helpImitationContorl.ToolTipText = StepsInfo.ImitationOfControlForm;
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
