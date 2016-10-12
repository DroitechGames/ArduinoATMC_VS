using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoACP
{
    public partial class ArduinoACP : Form
    {
        public ArduinoACP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           using (Process processNew = new Process())
            {
                string LocalDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
                string LocalUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                //////
                string urldir = "\\Documents\\Visual Studio 2015\\Projects\\ArduinoATMC\\ArduinoATMC\\bin\\Release";
                string atmc = "ArduinoATMC";
                string ex = ".exe";
                string FN = atmc + ex;
                string WD = LocalUser + urldir;

                listBox1.Items.Add(WD);
                listBox1.Items.Add(FN);

                processNew.StartInfo.WorkingDirectory = WD;
                processNew.StartInfo.FileName = FN;

                processNew.Start();
                processNew.WaitForExit();

                if (processNew.HasExited == true)
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("Arduino ATMC has been closed.");
                }
            }
        }

        private void ArduinoACP_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.SysIcon;
        }
    }
}
