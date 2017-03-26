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
            writer.Close();
            Close();
        }

        private void timerBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bobRossCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
