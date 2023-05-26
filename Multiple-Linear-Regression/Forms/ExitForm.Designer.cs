namespace Multiple_Linear_Regression {
    partial class ExitForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExitForm));
            this.label1 = new System.Windows.Forms.Label();
            this.AcceptExitButton = new System.Windows.Forms.Button();
            this.CancelExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(11, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Вы уверены, что хотите выйти?";
            // 
            // AcceptExitButton
            // 
            this.AcceptExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptExitButton.Location = new System.Drawing.Point(53, 83);
            this.AcceptExitButton.Margin = new System.Windows.Forms.Padding(2);
            this.AcceptExitButton.Name = "AcceptExitButton";
            this.AcceptExitButton.Size = new System.Drawing.Size(93, 33);
            this.AcceptExitButton.TabIndex = 3;
            this.AcceptExitButton.Text = "Выйти";
            this.AcceptExitButton.UseVisualStyleBackColor = true;
            this.AcceptExitButton.Click += new System.EventHandler(this.AcceptExitButton_Click);
            // 
            // CancelExitButton
            // 
            this.CancelExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelExitButton.Location = new System.Drawing.Point(209, 83);
            this.CancelExitButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelExitButton.Name = "CancelExitButton";
            this.CancelExitButton.Size = new System.Drawing.Size(93, 33);
            this.CancelExitButton.TabIndex = 4;
            this.CancelExitButton.Text = "Отмена";
            this.CancelExitButton.UseVisualStyleBackColor = true;
            this.CancelExitButton.Click += new System.EventHandler(this.CancelExitButton_Click);
            // 
            // ExitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 153);
            this.Controls.Add(this.CancelExitButton);
            this.Controls.Add(this.AcceptExitButton);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(372, 192);
            this.MinimumSize = new System.Drawing.Size(372, 192);
            this.Name = "ExitForm";
            this.Text = "Exit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AcceptExitButton;
        private System.Windows.Forms.Button CancelExitButton;
    }
}