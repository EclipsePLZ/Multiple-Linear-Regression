using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiple_Linear_Regression {
    public class OperationsWithControls {
        /// <summary>
        /// Set column headers and column settings to dataGV
        /// </summary>
        /// <param name="headers">List of column headers</param>
        /// <param name="dataGV">DataGridView</param>
        /// <param name="autoSize">AutoSize column width</param>
        /// <param name="indexOfSortableColumns">List of indexes of sortable columns</param>
        /// <param name="indexOfModifiableColumns">List of indexes of modifiable columns</param>
        public void SetDataGVColumnHeaders(List<string> headers, DataGridView dataGV, bool autoSize,
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
                foreach (var index in indexOfModifiableColumns) {
                    dataGV.Columns[index].ReadOnly = false;
                }
            }
            dataGV.ColumnHeadersVisible = true;
        }

        /// <summary>
        /// Function for showing logo of the loading
        /// </summary>
        /// <param name="bgWorker">Background worker</param>
        /// <param name="mainBgWorker">Main background worker</param>
        /// <param name="loadLabel">Loading label</param>
        /// <param name="finishLabel">Label for finish</param>
        public void ShowLoadingLogo(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker,
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

        /// <summary>
        /// Move selected item from one list to another
        /// </summary>
        /// <param name="fromList">The list from which we move the item</param>
        /// <param name="toList">The list to which we move the item</param>
        public void MoveModelBetweenLists(ListBox fromList, ListBox toList) {
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
            }
        }

        /// <summary>
        /// Move all items from one list to another
        /// </summary>
        /// <param name="fromList">The list from which we move the items</param>
        /// <param name="toList">The list to which we move the items</param>
        public void MoveAllItemsBetweenLists(ListBox fromList, ListBox toList) {
            if (fromList.Items.Count > 0) {
                toList.Items.AddRange(fromList.Items);
                fromList.Items.Clear();
            }
        }
    }
}
