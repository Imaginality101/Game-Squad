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
        const string D_PATH = "Settings"; // If opened by the game
        Boolean standalone;
        //"C:\\Users\\Caleb\\My Documents\\Visual Studio 2015\\Projects\\InteractionAttempt\\InteractionAttempt\\bin\\DesktopGL\\x86\\Debug\\Settings";
        //"C:\\Users\\Caleb\\Source\\Repos\\Game-Squad\\test\\GamePrototype\\bin\\DesktopGL\\x86\\Debug\\SaveFile"
        // writes values the user enters
        BinaryWriter writer;
        // writes default values if the user does not enter any values
        BinaryWriter defaultWriter;
        public Form1(Boolean stnd)
        {
            standalone = stnd;
            if (standalone)
            {
                defaultWriter = new BinaryWriter(File.Open(PATH, FileMode.OpenOrCreate));
            }
            else
            {
                defaultWriter = new BinaryWriter(File.Open(D_PATH, FileMode.OpenOrCreate));
            }
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
            if (standalone)
            {
                writer = new BinaryWriter(File.Open(PATH, FileMode.OpenOrCreate));
            }
            else
            {
                Console.WriteLine("Saved dependently");
                writer = new BinaryWriter(File.Open(D_PATH, FileMode.OpenOrCreate));
            }
            if (timerBox.Checked)
            {
                int minutes = 0;
                // check the input of timeLimit box
                bool parseSuccessful = int.TryParse(timeLimit.Text, out minutes);
                if (parseSuccessful)
                {
                    // if a valid value was entered
                    if (minutes > 0)
                    {
                        writer.Write(true);
                        writer.Write(minutes);
                    }
                    else
                    {
                        MessageBox.Show("Bad timer value. Timer is turned off");
                        writer.Write(false);
                        writer.Write(-1);
                    }
                }
                else
                {
                    // if parse was unsuccessful, pretend that the timer checkbox was not checked
                    writer.Write(false);
                    // write -1 for the amount of minutes
                    writer.Write(-1);
                }
            }
            else
            {
                writer.Write(false);
                // write -1 for the amount of minutes
                writer.Write(-1);
            }
            if (easyBox.Checked)
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
                    // if resolution values are valid
                    if (width > 0 || height > 0)
                    {
                        writer.Write(width);
                        writer.Write(height);
                    }
                    else
                    {
                        MessageBox.Show("Bad resolution values. Default resolution is 800 by 600");
                        // have 800 x 600 as default value
                        writer.Write(800);
                        writer.Write(600);
                    }
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

        private void easyBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
