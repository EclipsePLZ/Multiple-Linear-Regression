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
            this.acceptDefaultParametersButton = new System.Windows.Forms.Button();
            this.cancelDefaultParametersButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(48, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 64);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выполнить быстрое построение моделей?\r\n\r\nДля построения будут использоваться \r\nпа" +
    "раметры по умолчанию.";
            // 
            // acceptDefaultParametersButton
            // 
            this.acceptDefaultParametersButton.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptDefaultParametersButton.Location = new System.Drawing.Point(51, 152);
            this.acceptDefaultParametersButton.Name = "acceptDefaultParametersButton";
            this.acceptDefaultParametersButton.Size = new System.Drawing.Size(104, 35);
            this.acceptDefaultParametersButton.TabIndex = 0;
            this.acceptDefaultParametersButton.Text = "Подтвердить";
            this.acceptDefaultParametersButton.UseVisualStyleBackColor = true;
            this.acceptDefaultParametersButton.Click += new System.EventHandler(this.acceptDefaultParametersButton_Click);
            // 
            // cancelDefaultParametersButton
            // 
            this.cancelDefaultParametersButton.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelDefaultParametersButton.Location = new System.Drawing.Point(219, 152);
            this.cancelDefaultParametersButton.Name = "cancelDefaultParametersButton";
            this.cancelDefaultParametersButton.Size = new System.Drawing.Size(104, 35);
            this.cancelDefaultParametersButton.TabIndex = 1;
            this.cancelDefaultParametersButton.Text = "Отмена";
            this.cancelDefaultParametersButton.UseVisualStyleBackColor = true;
            this.cancelDefaultParametersButton.Click += new System.EventHandler(this.cancelDefaultParametersButton_Click);
            // 
            // AllDefaultParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 224);
            this.Controls.Add(this.cancelDefaultParametersButton);
            this.Controls.Add(this.acceptDefaultParametersButton);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(394, 263);
            this.MinimumSize = new System.Drawing.Size(394, 263);
            this.Name = "AllDefaultParameters";
            this.Text = "Автоматический расчет";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button acceptDefaultParametersButton;
        private System.Windows.Forms.Button cancelDefaultParametersButton;
    }
}