using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ExternalTool
{
    public partial class Form1 : Form
    {
        const string PATH = "..\\..\\..\\GamePrototype\\bin\\DesktopGL\\x86\\Debug\\Settings"; 
        //"C:\\Users\\Caleb\\My Documents\\Visual Studio 2015\\Projects\\InteractionAttempt\\InteractionAttempt\\bin\\DesktopGL\\x86\\Debug\\Settings";
        //"C:\\Users\\Caleb\\Source\\Repos\\Game-Squad\\test\\GamePrototype\\bin\\DesktopGL\\x86\\Debug\\SaveFile"
        // writes values the user enters
        BinaryWriter writer;
        // writes default values if the user does not enter any values
        BinaryWriter defaultWriter;
        public Form1()
        {
            defaultWriter = new BinaryWriter(File.Open(PATH, FileMode.OpenOrCreate));
            defaultWriter.Write(false);
            defaultWriter.Write(false);
            defaultWriter.Close();
            InitializeComponent();
        }
        // IGNORE THIS
        private void bobRossCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Close();
        }

        private void runGame_Click(object sender, EventArgs e)
        {
            writer = new BinaryWriter(File.Open(PATH, FileMode.OpenOrCreate));
            if (timerBox.Checked)
            {
                writer.Write(true);
            }
            else
            {
                writer.Write(false);
            }
            if (bobRossCheckBox.Checked)
            {
                writer.Write(true);
            }
            else
            {
                writer.Write(false);
            }
            // Tom - Getting resolution options
            if(fullScreenButton.Checked) // fullscreen selected
            {
                writer.Write(true);
            }
            else // windowed mode
            {
                writer.Write(false);
                if(resButton1.Checked) // 800x600
                {
                    writer.Write(800);
                    writer.Write(600);
                }
                else if(resButton2.Checked) // 1024x768
                {
                    writer.Write(1024);
                    writer.Write(768);
                }
                else if(resButton3.Checked) // 1732x972
                {
                    writer.Write(1728);
                    writer.Write(972);
                }
                else if(resButtonCustom.Checked) // Custom resolution
                {
                    int width = int.Parse(resBoxWidth.Text);
                    int height = int.Parse(resBoxHeight.Text);
                    writer.Write(width);
                    writer.Write(height);
                }
            }
            writer.Close();
            Close();
        }

        private void timerBox_CheckedChanged(object sender, EventArgs e)
        {
            timeLimit.Enabled = timerBox.Checked;
        }

        private void bobRossCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void windowedButton_CheckedChanged(object sender, EventArgs e)
        {
            resPanelW.Enabled = windowedButton.Checked;
        }
    }
}
