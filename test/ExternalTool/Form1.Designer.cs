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
            this.runGame = new System.Windows.Forms.Button();
            this.timerBox = new System.Windows.Forms.CheckBox();
            this.bobRossCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // runGame
            // 
            this.runGame.Location = new System.Drawing.Point(67, 197);
            this.runGame.Name = "runGame";
            this.runGame.Size = new System.Drawing.Size(120, 53);
            this.runGame.TabIndex = 1;
            this.runGame.Text = "Run Game!";
            this.runGame.UseVisualStyleBackColor = true;
            this.runGame.Click += new System.EventHandler(this.runGame_Click);
            // 
            // timerBox
            // 
            this.timerBox.AutoSize = true;
            this.timerBox.Location = new System.Drawing.Point(67, 48);
            this.timerBox.Name = "timerBox";
            this.timerBox.Size = new System.Drawing.Size(52, 17);
            this.timerBox.TabIndex = 2;
            this.timerBox.Text = "Timer";
            this.timerBox.UseVisualStyleBackColor = true;
            this.timerBox.CheckedChanged += new System.EventHandler(this.timerBox_CheckedChanged);
            // 
            // bobRossCheckBox
            // 
            this.bobRossCheckBox.AutoSize = true;
            this.bobRossCheckBox.Location = new System.Drawing.Point(67, 97);
            this.bobRossCheckBox.Name = "bobRossCheckBox";
            this.bobRossCheckBox.Size = new System.Drawing.Size(102, 17);
            this.bobRossCheckBox.TabIndex = 3;
            this.bobRossCheckBox.Text = "Bob Ross Mode";
            this.bobRossCheckBox.UseVisualStyleBackColor = true;
            this.bobRossCheckBox.CheckedChanged += new System.EventHandler(this.bobRossCheckBox_CheckedChanged_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.bobRossCheckBox);
            this.Controls.Add(this.timerBox);
            this.Controls.Add(this.runGame);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button runGame;
        private System.Windows.Forms.CheckBox timerBox;
        private System.Windows.Forms.CheckBox bobRossCheckBox;
    }
}

