using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Multiple_Linear_Regression {
    public partial class SelectParametersForm : Form {
        public List<string> SelectedFactors { get; private set; } = new List<string>();

        public SelectParametersForm(string formName, List<string> availableFactors, List<string> selectedFactors) {
            InitializeComponent();
            this.Text = formName;
            this.CenterToParent();
            SelectedFactors = selectedFactors;
            listAvailabelFactors.Items.AddRange(availableFactors.ToArray());
            listSelectedFactors.Items.AddRange(selectedFactors.ToArray());

            listAvailabelFactors.SelectedIndex = 0;
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
            OperationsWithControls.MoveModelBetweenLists(fromList, toList);
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
                OperationsWithControls.MoveAllItemsBetweenLists(fromList, toList);
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
    }
}
