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
    public partial class SelectParametersForm : Form {
        public List<string> SelectedFactors { get; private set; } = new List<string>();

        // Background worker for resize elements
        private BackgroundWorker resizeWorker = new BackgroundWorker();

        public SelectParametersForm(string formName, List<string> availableFactors, List<string> selectedFactors) {
            this.Text = formName;
            InitializeComponent();
            SelectedFactors = selectedFactors;
            listAvailabelFactors.Items.AddRange(availableFactors.ToArray());
            listSelectedFactors.Items.AddRange(selectedFactors.ToArray());

            // Run background worker for resizing components on form
            resizeWorker.DoWork += new DoWorkEventHandler(DoResizeComponents);
            resizeWorker.WorkerSupportsCancellation = true;
            resizeWorker.RunWorkerAsync();
            // Добавить перерисовку элементов и сделать остановку воркера при закрытии формы
        }

        private void listSelectedFactors_DoubleClick(object sender, EventArgs e) {
            AddItemToAllList();
        }

        private void listAvailabelFactors_DoubleClick(object sender, EventArgs e) {
            AddItemToSelectedList();
        }

        private void toSelectList_Click(object sender, EventArgs e) {
            AddItemToSelectedList();
        }

        private void toAvailableList_Click(object sender, EventArgs e) {
            AddItemToAllList();
        }

        /// <summary>
        /// Move selected item from all list to selected list
        /// </summary>
        private void AddItemToSelectedList() {
            if (listAvailabelFactors.SelectedItems.Count == 1) {
                int selectedIndex = listAvailabelFactors.SelectedIndex;
                listSelectedFactors.Items.Add(listAvailabelFactors.SelectedItem);
                listAvailabelFactors.Items.Remove(listAvailabelFactors.SelectedItem);
                if (listAvailabelFactors.Items.Count > 0) {
                    if (selectedIndex < listAvailabelFactors.Items.Count) {
                        listAvailabelFactors.SelectedIndex = selectedIndex;
                    }
                    else {
                        listAvailabelFactors.SelectedIndex = selectedIndex - 1;
                    }
                }
                CheckRulesForAcceptParamters();
            }
        }

        /// <summary>
        /// Move selected item from selected list to all list
        /// </summary>
        private void AddItemToAllList() {
            if (listSelectedFactors.SelectedItems.Count == 1) {
                int selectedIndex = listSelectedFactors.SelectedIndex;
                listAvailabelFactors.Items.Add(listSelectedFactors.SelectedItem);
                listSelectedFactors.Items.Remove(listSelectedFactors.SelectedItem);
                if (listSelectedFactors.Items.Count > 0) {
                    if (selectedIndex < listSelectedFactors.Items.Count) {
                        listSelectedFactors.SelectedIndex = selectedIndex;
                    }
                    else {
                        listSelectedFactors.SelectedIndex = selectedIndex - 1;
                    }
                }
                CheckRulesForAcceptParamters();
            }
        }

        private void allToSelectList_Click(object sender, EventArgs e) {
            if (listAvailabelFactors.Items.Count > 0) {
                listSelectedFactors.Items.AddRange(listAvailabelFactors.Items);
                listAvailabelFactors.Items.Clear();
                CheckRulesForAcceptParamters();
            }
        }

        private void allToAvailableList_Click(object sender, EventArgs e) {
            if (listSelectedFactors.Items.Count > 0) {
                listAvailabelFactors.Items.AddRange(listSelectedFactors.Items);
                listSelectedFactors.Items.Clear();
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

        /// <summary>
        /// Resize all components on SelectParametersForm
        /// </summary>
        private void DoResizeComponents(object sender, DoWorkEventArgs e) {

        }
    }
}
