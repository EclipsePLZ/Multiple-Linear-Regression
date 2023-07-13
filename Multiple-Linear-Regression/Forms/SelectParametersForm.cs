using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Multiple_Linear_Regression {
    public partial class SelectParametersForm : Form {
        public List<string> SelectedFactors { get; private set; } = new List<string>();

        // Background worker for resize elements
        private BackgroundWorker resizeWorker = new BackgroundWorker();
        private OperationsWithControls operationsWithControls = new OperationsWithControls();

        private bool isResizeNeeded = false;

        public SelectParametersForm(string formName, List<string> availableFactors, List<string> selectedFactors) {
            InitializeComponent();
            this.Text = formName;
            this.CenterToParent();
            SelectedFactors = selectedFactors;
            listAvailabelFactors.Items.AddRange(availableFactors.ToArray());
            listSelectedFactors.Items.AddRange(selectedFactors.ToArray());

            // Run background worker for resizing components on form
            resizeWorker.DoWork += new DoWorkEventHandler(DoResizeComponents);
            resizeWorker.WorkerSupportsCancellation = true;
            resizeWorker.RunWorkerAsync();
        }

        private void listSelectedFactors_DoubleClick(object sender, EventArgs e) {
            MoveItemBetweenLists(listSelectedFactors, listAvailabelFactors);
        }

        private void listAvailabelFactors_DoubleClick(object sender, EventArgs e) {
            MoveItemBetweenLists(listAvailabelFactors, listSelectedFactors);
        }

        private void toSelectList_Click(object sender, EventArgs e) {
            MoveItemBetweenLists(listAvailabelFactors, listSelectedFactors);
        }

        private void toAvailableList_Click(object sender, EventArgs e) {
            MoveItemBetweenLists(listSelectedFactors, listAvailabelFactors);
        }

        /// <summary>
        /// Move selected item from one list to another
        /// </summary>
        /// <param name="fromList">The list from which we move the item</param>
        /// <param name="toList">The list to which we move the item</param>
        private void MoveItemBetweenLists(ListBox fromList, ListBox toList) {
            operationsWithControls.MoveModelBetweenLists(fromList, toList);
            CheckRulesForAcceptParamters();
        }

        private void allToSelectList_Click(object sender, EventArgs e) {
            MoveAllItemsBetweenLists(listAvailabelFactors, listSelectedFactors);
        }

        private void allToAvailableList_Click(object sender, EventArgs e) {
            MoveAllItemsBetweenLists(listSelectedFactors, listAvailabelFactors);
        }

        /// <summary>
        /// Move all items from one list to another
        /// </summary>
        /// <param name="fromList">The list from which we move the items</param>
        /// <param name="toList">The list to which we move the items</param>
        private void MoveAllItemsBetweenLists(ListBox fromList, ListBox toList) {
            if (fromList.Items.Count > 0) {
                operationsWithControls.MoveAllItemsBetweenLists(fromList, toList);
                CheckRulesForAcceptParamters();
            }
        }

        /// <summary>
        /// Checking rules for enable accept button
        /// </summary>
        private void CheckRulesForAcceptParamters() {
            if (listSelectedFactors.Items.Count > 0) {
                acceptSelectedFactorsButton.Enabled = true;
            }
            else {
                acceptSelectedFactorsButton.Enabled = false;
            }
        }

        private void acceptSelectedFactorsButton_Click(object sender, EventArgs e) {
            SelectedFactors = listSelectedFactors.Items.OfType<string>().ToList();
            this.Close();
        }

        private void SelectParametersForm_Resize(object sender, EventArgs e) {
            isResizeNeeded = true;
        }

        private void SelectParametersForm_FormClosing(object sender, FormClosingEventArgs e) {
            // Stop worker
            resizeWorker.CancelAsync();
        }

        /// <summary>
        /// Resize all components on SelectParametersForm
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
                        int widthMainForm = this.Width;
                        int heightMainForm = this.Height;

                        // The height of one element in the list is 13, so for a smooth drawing of the lists
                        // will change their height to a multiple of 13
                        int listsHeight = ((heightMainForm - 107 - 17) / 13) * 13 + 17;

                        toSelectList.Invoke(new Action<Point>((loc) => toSelectList.Location = loc),
                            new Point((widthMainForm / 2) - toSelectList.Width, toSelectList.Location.Y));

                        toAvailableList.Invoke(new Action<Point>((loc) => toAvailableList.Location = loc),
                            new Point((widthMainForm / 2) - toAvailableList.Width, toAvailableList.Location.Y));

                        allToSelectList.Invoke(new Action<Point>((loc) => allToSelectList.Location = loc),
                            new Point((widthMainForm / 2) - allToSelectList.Width, allToSelectList.Location.Y));

                        allToAvailableList.Invoke(new Action<Point>((loc) => allToAvailableList.Location = loc),
                            new Point((widthMainForm / 2) - allToAvailableList.Width, allToAvailableList.Location.Y));

                        listSelectedFactors.Invoke(new Action<Size>((size) => listSelectedFactors.Size = size),
                            new Size(toSelectList.Location.X - 77, listsHeight));

                        listAvailabelFactors.Invoke(new Action<Point>((loc) => listAvailabelFactors.Location = loc),
                            new Point(toSelectList.Location.X + 74, listAvailabelFactors.Location.Y));

                        listAvailabelFactors.Invoke(new Action<Size>((size) => listAvailabelFactors.Size = size),
                            new Size(listSelectedFactors.Width, listsHeight));

                        labelAvailableFactors.Invoke(new Action<Point>((loc) => labelAvailableFactors.Location = loc),
                            new Point(listAvailabelFactors.Location.X - 3, labelAvailableFactors.Location.Y));

                        acceptSelectedFactorsButton.Invoke(new Action<Point>((loc) => acceptSelectedFactorsButton.Location = loc),
                            new Point(toSelectList.Location.X - 33, acceptSelectedFactorsButton.Location.Y));

                        isResizeNeeded = false;
                    }
                }
            }
        }
    }
}
