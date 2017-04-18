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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resPanelW = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.resBoxHeight = new System.Windows.Forms.MaskedTextBox();
            this.resBoxWidth = new System.Windows.Forms.MaskedTextBox();
            this.resButtonCustom = new System.Windows.Forms.RadioButton();
            this.resButton3 = new System.Windows.Forms.RadioButton();
            this.resButton2 = new System.Windows.Forms.RadioButton();
            this.resButton1 = new System.Windows.Forms.RadioButton();
            this.windowedButton = new System.Windows.Forms.RadioButton();
            this.fullScreenButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.resPanelW.SuspendLayout();
            this.SuspendLayout();
            // 
            // runGame
            // 
            this.runGame.Location = new System.Drawing.Point(230, 522);
            this.runGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.runGame.Name = "runGame";
            this.runGame.Size = new System.Drawing.Size(180, 82);
            this.runGame.TabIndex = 1;
            this.runGame.Text = "Run Game!";
            this.runGame.UseVisualStyleBackColor = true;
            this.runGame.Click += new System.EventHandler(this.runGame_Click);
            // 
            // timerBox
            // 
            this.timerBox.AutoSize = true;
            this.timerBox.Location = new System.Drawing.Point(100, 74);
            this.timerBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timerBox.Name = "timerBox";
            this.timerBox.Size = new System.Drawing.Size(74, 24);
            this.timerBox.TabIndex = 2;
            this.timerBox.Text = "Timer";
            this.timerBox.UseVisualStyleBackColor = true;
            this.timerBox.CheckedChanged += new System.EventHandler(this.timerBox_CheckedChanged);
            // 
            // bobRossCheckBox
            // 
            this.bobRossCheckBox.AutoSize = true;
            this.bobRossCheckBox.Location = new System.Drawing.Point(100, 149);
            this.bobRossCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bobRossCheckBox.Name = "bobRossCheckBox";
            this.bobRossCheckBox.Size = new System.Drawing.Size(149, 24);
            this.bobRossCheckBox.TabIndex = 3;
            this.bobRossCheckBox.Text = "Bob Ross Mode";
            this.bobRossCheckBox.UseVisualStyleBackColor = true;
            this.bobRossCheckBox.CheckedChanged += new System.EventHandler(this.bobRossCheckBox_CheckedChanged_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resPanelW);
            this.groupBox1.Controls.Add(this.windowedButton);
            this.groupBox1.Controls.Add(this.fullScreenButton);
            this.groupBox1.Location = new System.Drawing.Point(313, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 293);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Video Settings";
            // 
            // resPanelW
            // 
            this.resPanelW.Controls.Add(this.label1);
            this.resPanelW.Controls.Add(this.resBoxHeight);
            this.resPanelW.Controls.Add(this.resBoxWidth);
            this.resPanelW.Controls.Add(this.resButtonCustom);
            this.resPanelW.Controls.Add(this.resButton3);
            this.resPanelW.Controls.Add(this.resButton2);
            this.resPanelW.Controls.Add(this.resButton1);
            this.resPanelW.Enabled = false;
            this.resPanelW.Location = new System.Drawing.Point(28, 85);
            this.resPanelW.Name = "resPanelW";
            this.resPanelW.Size = new System.Drawing.Size(229, 184);
            this.resPanelW.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "x";
            // 
            // resBoxHeight
            // 
            this.resBoxHeight.Location = new System.Drawing.Point(115, 142);
            this.resBoxHeight.Mask = "0000";
            this.resBoxHeight.Name = "resBoxHeight";
            this.resBoxHeight.Size = new System.Drawing.Size(79, 26);
            this.resBoxHeight.TabIndex = 4;
            // 
            // resBoxWidth
            // 
            this.resBoxWidth.Location = new System.Drawing.Point(8, 142);
            this.resBoxWidth.Mask = "0000";
            this.resBoxWidth.Name = "resBoxWidth";
            this.resBoxWidth.Size = new System.Drawing.Size(79, 26);
            this.resBoxWidth.TabIndex = 4;
            // 
            // resButtonCustom
            // 
            this.resButtonCustom.AutoSize = true;
            this.resButtonCustom.Location = new System.Drawing.Point(4, 96);
            this.resButtonCustom.Name = "resButtonCustom";
            this.resButtonCustom.Size = new System.Drawing.Size(89, 24);
            this.resButtonCustom.TabIndex = 3;
            this.resButtonCustom.TabStop = true;
            this.resButtonCustom.Text = "Custom";
            this.resButtonCustom.UseVisualStyleBackColor = true;
            // 
            // resButton3
            // 
            this.resButton3.AutoSize = true;
            this.resButton3.Location = new System.Drawing.Point(4, 65);
            this.resButton3.Name = "resButton3";
            this.resButton3.Size = new System.Drawing.Size(104, 24);
            this.resButton3.TabIndex = 2;
            this.resButton3.TabStop = true;
            this.resButton3.Text = "1728x972";
            this.resButton3.UseVisualStyleBackColor = true;
            // 
            // resButton2
            // 
            this.resButton2.AutoSize = true;
            this.resButton2.Location = new System.Drawing.Point(4, 34);
            this.resButton2.Name = "resButton2";
            this.resButton2.Size = new System.Drawing.Size(104, 24);
            this.resButton2.TabIndex = 1;
            this.resButton2.TabStop = true;
            this.resButton2.Text = "1024x768";
            this.resButton2.UseVisualStyleBackColor = true;
            // 
            // resButton1
            // 
            this.resButton1.AutoSize = true;
            this.resButton1.Location = new System.Drawing.Point(3, 3);
            this.resButton1.Name = "resButton1";
            this.resButton1.Size = new System.Drawing.Size(95, 24);
            this.resButton1.TabIndex = 0;
            this.resButton1.TabStop = true;
            this.resButton1.Text = "800x600";
            this.resButton1.UseVisualStyleBackColor = true;
            // 
            // windowedButton
            // 
            this.windowedButton.AutoSize = true;
            this.windowedButton.Location = new System.Drawing.Point(7, 55);
            this.windowedButton.Name = "windowedButton";
            this.windowedButton.Size = new System.Drawing.Size(108, 24);
            this.windowedButton.TabIndex = 1;
            this.windowedButton.Text = "Windowed";
            this.windowedButton.UseVisualStyleBackColor = true;
            this.windowedButton.CheckedChanged += new System.EventHandler(this.windowedButton_CheckedChanged);
            // 
            // fullScreenButton
            // 
            this.fullScreenButton.AutoSize = true;
            this.fullScreenButton.Checked = true;
            this.fullScreenButton.Location = new System.Drawing.Point(7, 25);
            this.fullScreenButton.Name = "fullScreenButton";
            this.fullScreenButton.Size = new System.Drawing.Size(107, 24);
            this.fullScreenButton.TabIndex = 0;
            this.fullScreenButton.TabStop = true;
            this.fullScreenButton.Text = "Fullscreen";
            this.fullScreenButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 719);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bobRossCheckBox);
            this.Controls.Add(this.timerBox);
            this.Controls.Add(this.runGame);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.resPanelW.ResumeLayout(false);
            this.resPanelW.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button runGame;
        private System.Windows.Forms.CheckBox timerBox;
        private System.Windows.Forms.CheckBox bobRossCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel resPanelW;
        private System.Windows.Forms.RadioButton resButtonCustom;
        private System.Windows.Forms.RadioButton resButton3;
        private System.Windows.Forms.RadioButton resButton2;
        private System.Windows.Forms.RadioButton resButton1;
        private System.Windows.Forms.RadioButton windowedButton;
        private System.Windows.Forms.RadioButton fullScreenButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox resBoxHeight;
        private System.Windows.Forms.MaskedTextBox resBoxWidth;
    }
}

