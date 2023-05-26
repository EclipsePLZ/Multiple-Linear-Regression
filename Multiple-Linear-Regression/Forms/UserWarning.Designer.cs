namespace Multiple_Linear_Regression.Forms {
    partial class UserWarningForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserWarningForm));
            this.AcceptActionButton = new System.Windows.Forms.Button();
            this.CancelActionButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AcceptActionButton
            // 
            this.AcceptActionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptActionButton.Location = new System.Drawing.Point(52, 120);
            this.AcceptActionButton.Margin = new System.Windows.Forms.Padding(2);
            this.AcceptActionButton.Name = "AcceptActionButton";
            this.AcceptActionButton.Size = new System.Drawing.Size(99, 33);
            this.AcceptActionButton.TabIndex = 4;
            this.AcceptActionButton.Text = "Подтвердить";
            this.AcceptActionButton.UseVisualStyleBackColor = true;
            this.AcceptActionButton.Click += new System.EventHandler(this.AcceptActionButton_Click);
            // 
            // CancelActionButton
            // 
            this.CancelActionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelActionButton.Location = new System.Drawing.Point(208, 120);
            this.CancelActionButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelActionButton.Name = "CancelActionButton";
            this.CancelActionButton.Size = new System.Drawing.Size(99, 33);
            this.CancelActionButton.TabIndex = 5;
            this.CancelActionButton.Text = "Отмена";
            this.CancelActionButton.UseVisualStyleBackColor = true;
            this.CancelActionButton.Click += new System.EventHandler(this.CancelActionButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(34, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Вы уверены, что хотите продолжить?";
            // 
            // labelWarning
            // 
            this.labelWarning.AutoSize = true;
            this.labelWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWarning.Location = new System.Drawing.Point(35, 22);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(0, 15);
            this.labelWarning.TabIndex = 7;
            // 
            // UserWarningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 175);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelActionButton);
            this.Controls.Add(this.AcceptActionButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(372, 214);
            this.MinimumSize = new System.Drawing.Size(372, 214);
            this.Name = "UserWarningForm";
            this.Text = "Подтверждение";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AcceptActionButton;
        private System.Windows.Forms.Button CancelActionButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelWarning;
    }
}