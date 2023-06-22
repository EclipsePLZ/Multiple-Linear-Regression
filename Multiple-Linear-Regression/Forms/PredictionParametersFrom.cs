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
    public partial class PredictionParametersFrom : Form {

        /// <summary>
        /// Number of observations in one time interval
        /// </summary>
        public int NumberObserInOneTimeInterval { get; private set; }

        /// <summary>
        /// Value of lag for prediciton task
        /// </summary>
        public int LagValue { get; private set; }

        private int TotalValuesCount { get; }

        public PredictionParametersFrom(int valuesCount) {
            InitializeComponent();
            this.CenterToParent();

            FixFormSize();

            TotalValuesCount = valuesCount;
            totalValuesCount.Text = valuesCount.ToString();

            numberValuesInTimeInterval.Maximum = valuesCount;
            lagValue.Maximum = Decimal.MaxValue;
            toolTipLagValue.SetToolTip(lagValue, StepsInfo.LagValueInfo);
        }

        private void acceptPredictionParametersButton_Click(object sender, EventArgs e) {
            LagValue = (int)lagValue.Value;
            NumberObserInOneTimeInterval = (int)numberValuesInTimeInterval.Value;

            if (LagValue * NumberObserInOneTimeInterval < TotalValuesCount) {
                this.Close();
            }
            else {
                MessageBox.Show($"Величина временного сдвига (Временной лаг={LagValue} * Кол-во наблюдений в одном " +
                    $"интервале={NumberObserInOneTimeInterval}) превышает общее количество наблюдений = {TotalValuesCount}");
            }
        }


        /// <summary>
        /// Fixed form size
        /// </summary>
        private void FixFormSize() {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
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
    }
}
