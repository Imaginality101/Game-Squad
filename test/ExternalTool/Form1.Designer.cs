namespace ExternalTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bobRossCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // bobRossCheckBox
            // 
            this.bobRossCheckBox.AutoSize = true;
            this.bobRossCheckBox.Location = new System.Drawing.Point(85, 84);
            this.bobRossCheckBox.Name = "bobRossCheckBox";
            this.bobRossCheckBox.Size = new System.Drawing.Size(102, 17);
            this.bobRossCheckBox.TabIndex = 0;
            this.bobRossCheckBox.Text = "Bob Ross Mode";
            this.bobRossCheckBox.UseVisualStyleBackColor = true;
            this.bobRossCheckBox.CheckedChanged += new System.EventHandler(this.bobRossCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.bobRossCheckBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox bobRossCheckBox;
    }
}

