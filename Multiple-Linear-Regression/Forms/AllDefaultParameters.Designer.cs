namespace Multiple_Linear_Regression.Forms {
    partial class AllDefaultParameters {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllDefaultParameters));
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelDefaultParametersButton = new System.Windows.Forms.Button();
            this.acceptDefaultParametersButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(59, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выполнить быстрое построение моделей?\r\n\r\nДля построения будут использоваться \r\nпа" +
    "раметры по умолчанию.";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(378, 224);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // cancelDefaultParametersButton
            // 
            this.cancelDefaultParametersButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cancelDefaultParametersButton.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelDefaultParametersButton.Location = new System.Drawing.Point(227, 3);
            this.cancelDefaultParametersButton.Name = "cancelDefaultParametersButton";
            this.cancelDefaultParametersButton.Size = new System.Drawing.Size(104, 35);
            this.cancelDefaultParametersButton.TabIndex = 1;
            this.cancelDefaultParametersButton.Text = "Отмена";
            this.cancelDefaultParametersButton.UseVisualStyleBackColor = true;
            this.cancelDefaultParametersButton.Click += new System.EventHandler(this.cancelDefaultParametersButton_Click);
            // 
            // acceptDefaultParametersButton
            // 
            this.acceptDefaultParametersButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.acceptDefaultParametersButton.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptDefaultParametersButton.Location = new System.Drawing.Point(41, 3);
            this.acceptDefaultParametersButton.Name = "acceptDefaultParametersButton";
            this.acceptDefaultParametersButton.Size = new System.Drawing.Size(104, 35);
            this.acceptDefaultParametersButton.TabIndex = 0;
            this.acceptDefaultParametersButton.Text = "Подтвердить";
            this.acceptDefaultParametersButton.UseVisualStyleBackColor = true;
            this.acceptDefaultParametersButton.Click += new System.EventHandler(this.acceptDefaultParametersButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.acceptDefaultParametersButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cancelDefaultParametersButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 142);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(372, 79);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // AllDefaultParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 224);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(394, 263);
            this.MinimumSize = new System.Drawing.Size(394, 263);
            this.Name = "AllDefaultParameters";
            this.Text = "Автоматический расчет";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button acceptDefaultParametersButton;
        private System.Windows.Forms.Button cancelDefaultParametersButton;
    }
}