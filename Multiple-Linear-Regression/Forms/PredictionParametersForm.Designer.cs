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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numberValuesInTimeInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lagValue)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelValuesCount
            // 
            this.labelValuesCount.AutoSize = true;
            this.labelValuesCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelValuesCount.Location = new System.Drawing.Point(27, 5);
            this.labelValuesCount.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelValuesCount.Name = "labelValuesCount";
            this.labelValuesCount.Size = new System.Drawing.Size(171, 56);
            this.labelValuesCount.TabIndex = 0;
            this.labelValuesCount.Text = "Общее количество наблюдений:";
            // 
            // totalValuesCount
            // 
            this.totalValuesCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.totalValuesCount.Enabled = false;
            this.totalValuesCount.Location = new System.Drawing.Point(204, 3);
            this.totalValuesCount.Name = "totalValuesCount";
            this.totalValuesCount.Size = new System.Drawing.Size(108, 20);
            this.totalValuesCount.TabIndex = 1;
            // 
            // labelObserCountInTimeInterval
            // 
            this.labelObserCountInTimeInterval.AutoSize = true;
            this.labelObserCountInTimeInterval.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelObserCountInTimeInterval.Location = new System.Drawing.Point(39, 61);
            this.labelObserCountInTimeInterval.Name = "labelObserCountInTimeInterval";
            this.labelObserCountInTimeInterval.Size = new System.Drawing.Size(159, 61);
            this.labelObserCountInTimeInterval.TabIndex = 2;
            this.labelObserCountInTimeInterval.Text = "Количество наблюдений в \r\nодном временном интервале:";
            // 
            // acceptPredictionParametersButton
            // 
            this.acceptPredictionParametersButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.acceptPredictionParametersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptPredictionParametersButton.Location = new System.Drawing.Point(151, 193);
            this.acceptPredictionParametersButton.Name = "acceptPredictionParametersButton";
            this.acceptPredictionParametersButton.Size = new System.Drawing.Size(106, 41);
            this.acceptPredictionParametersButton.TabIndex = 3;
            this.acceptPredictionParametersButton.Text = "Подтвердить";
            this.acceptPredictionParametersButton.UseVisualStyleBackColor = true;
            this.acceptPredictionParametersButton.Click += new System.EventHandler(this.acceptPredictionParametersButton_Click);
            // 
            // numberValuesInTimeInterval
            // 
            this.numberValuesInTimeInterval.Location = new System.Drawing.Point(204, 72);
            this.numberValuesInTimeInterval.Margin = new System.Windows.Forms.Padding(3, 11, 3, 3);
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
            this.labelLagValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelLagValue.Location = new System.Drawing.Point(90, 122);
            this.labelLagValue.Name = "labelLagValue";
            this.labelLagValue.Size = new System.Drawing.Size(108, 62);
            this.labelLagValue.TabIndex = 5;
            this.labelLagValue.Text = "Временной лаг для прогнозирования:";
            // 
            // lagValue
            // 
            this.lagValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lagValue.Location = new System.Drawing.Point(204, 133);
            this.lagValue.Margin = new System.Windows.Forms.Padding(3, 11, 3, 3);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labelValuesCount, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lagValue, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.totalValuesCount, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelLagValue, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelObserCountInTimeInterval, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numberValuesInTimeInterval, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(403, 184);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.acceptPredictionParametersButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(409, 251);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // PredictionParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 251);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(425, 290);
            this.MinimumSize = new System.Drawing.Size(425, 290);
            this.Name = "PredictionParametersForm";
            this.Text = "Параметры прогнозирования";
            ((System.ComponentModel.ISupportInitialize)(this.numberValuesInTimeInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lagValue)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}