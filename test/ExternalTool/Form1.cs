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
        const string PATH = "....\\GamePrototype\\bin\\DesktopGL\\x86\\Debug\\SaveFile"; //"C:\\Users\\Caleb\\My Documents\\Visual Studio 2015\\Projects\\InteractionAttempt\\InteractionAttempt\\bin\\DesktopGL\\x86\\Debug\\Settings";
        BinaryWriter writer;
        public Form1()
        {
            writer = new BinaryWriter(File.Open(PATH, FileMode.Create));
            InitializeComponent();
        }

        private void bobRossCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bobRossCheckBox.Checked)
            {
                writer.Write(true);
            }
            else
            {
                writer.Write(false);
            }
        }
    }
}
