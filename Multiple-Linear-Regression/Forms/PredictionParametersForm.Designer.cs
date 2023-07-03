namespace Multiple_Linear_Regression.Forms {
    partial class PredictionParametersForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PredictionParametersForm));
            this.labelValuesCount = new System.Windows.Forms.Label();
            this.totalValuesCount = new System.Windows.Forms.TextBox();
            this.labelObserCountInTimeInterval = new System.Windows.Forms.Label();
            this.acceptPredictionParametersButton = new System.Windows.Forms.Button();
            this.numberValuesInTimeInterval = new System.Windows.Forms.NumericUpDown();
            this.labelLagValue = new System.Windows.Forms.Label();
            this.lagValue = new System.Windows.Forms.NumericUpDown();
            this.toolTipLagValue = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numberValuesInTimeInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lagValue)).BeginInit();
            this.SuspendLayout();
            // 
            // labelValuesCount
            // 
            this.labelValuesCount.AutoSize = true;
            this.labelValuesCount.Location = new System.Drawing.Point(31, 33);
            this.labelValuesCount.Name = "labelValuesCount";
            this.labelValuesCount.Size = new System.Drawing.Size(171, 13);
            this.labelValuesCount.TabIndex = 0;
            this.labelValuesCount.Text = "Общее количество наблюдений:";
            // 
            // totalValuesCount
            // 
            this.totalValuesCount.Enabled = false;
            this.totalValuesCount.Location = new System.Drawing.Point(208, 30);
            this.totalValuesCount.Name = "totalValuesCount";
            this.totalValuesCount.Size = new System.Drawing.Size(108, 20);
            this.totalValuesCount.TabIndex = 1;
            // 
            // labelObserCountInTimeInterval
            // 
            this.labelObserCountInTimeInterval.AutoSize = true;
            this.labelObserCountInTimeInterval.Location = new System.Drawing.Point(31, 93);
            this.labelObserCountInTimeInterval.Name = "labelObserCountInTimeInterval";
            this.labelObserCountInTimeInterval.Size = new System.Drawing.Size(159, 26);
            this.labelObserCountInTimeInterval.TabIndex = 2;
            this.labelObserCountInTimeInterval.Text = "Количество наблюдений в \r\nодном временном интервале:";
            // 
            // acceptPredictionParametersButton
            // 
            this.acceptPredictionParametersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptPredictionParametersButton.Location = new System.Drawing.Point(146, 198);
            this.acceptPredictionParametersButton.Name = "acceptPredictionParametersButton";
            this.acceptPredictionParametersButton.Size = new System.Drawing.Size(106, 41);
            this.acceptPredictionParametersButton.TabIndex = 3;
            this.acceptPredictionParametersButton.Text = "Подтвердить";
            this.acceptPredictionParametersButton.UseVisualStyleBackColor = true;
            this.acceptPredictionParametersButton.Click += new System.EventHandler(this.acceptPredictionParametersButton_Click);
            // 
            // numberValuesInTimeInterval
            // 
            this.numberValuesInTimeInterval.Location = new System.Drawing.Point(237, 99);
            this.numberValuesInTimeInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberValuesInTimeInterval.Name = "numberValuesInTimeInterval";
            this.numberValuesInTimeInterval.Size = new System.Drawing.Size(120, 20);
            this.numberValuesInTimeInterval.TabIndex = 4;
            this.numberValuesInTimeInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberValuesInTimeInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateKeyPressedOnlyNums);
            // 
            // labelLagValue
            // 
            this.labelLagValue.AutoSize = true;
            this.labelLagValue.Location = new System.Drawing.Point(31, 147);
            this.labelLagValue.Name = "labelLagValue";
            this.labelLagValue.Size = new System.Drawing.Size(200, 13);
            this.labelLagValue.TabIndex = 5;
            this.labelLagValue.Text = "Временной лаг для прогнозирования:";
            // 
            // lagValue
            // 
            this.lagValue.Location = new System.Drawing.Point(237, 145);
            this.lagValue.Name = "lagValue";
            this.lagValue.Size = new System.Drawing.Size(120, 20);
            this.lagValue.TabIndex = 6;
            this.lagValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lagValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateKeyPressedOnlyNums);
            // 
            // PredictionParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 251);
            this.Controls.Add(this.lagValue);
            this.Controls.Add(this.labelLagValue);
            this.Controls.Add(this.numberValuesInTimeInterval);
            this.Controls.Add(this.acceptPredictionParametersButton);
            this.Controls.Add(this.labelObserCountInTimeInterval);
            this.Controls.Add(this.totalValuesCount);
            this.Controls.Add(this.labelValuesCount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PredictionParametersForm";
            this.Text = "Параметры прогнозирования";
            ((System.ComponentModel.ISupportInitialize)(this.numberValuesInTimeInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lagValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelValuesCount;
        private System.Windows.Forms.TextBox totalValuesCount;
        private System.Windows.Forms.Label labelObserCountInTimeInterval;
        private System.Windows.Forms.Button acceptPredictionParametersButton;
        private System.Windows.Forms.NumericUpDown numberValuesInTimeInterval;
        private System.Windows.Forms.Label labelLagValue;
        private System.Windows.Forms.NumericUpDown lagValue;
        private System.Windows.Forms.ToolTip toolTipLagValue;
    }
}