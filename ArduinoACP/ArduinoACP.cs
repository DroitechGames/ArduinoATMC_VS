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
using System.Management;
using System.Windows.Forms;
// DEVELOPED BY MASTER SHAUN CASSIDY.
namespace ArduinoACP
{
    public partial class ArduinoACP : Form
    {
        public ArduinoACP()
        {
            InitializeComponent();
            richTextBox1.Text   = "Log: ";
            ComInfoTB.Text      = "";
            BaudInfoTB.Text     = "";
        }

        static string USB_COM_PORT;
        static string USB_COM_NAME;

        //static string USB_COM_GUID; // COM DEVICE GUID.
        private string installDir = Environment.CurrentDirectory;
        /// <summary>
        /// Serial Device is the Serial Device.
        /// Info String is the information string before the device serial data is printed to screen.
        /// </summary>
        static string SerialDevice;
        static string InfoString = "Serial Port : ";
        static string DeviceNP = "{0}";
        static string SerialUSB = null;
        static object SerialUSB_P = null;
        private static string returnString = null;
        private static string xScope = "root\\CIMV2";
        private static string xPnp_Q = "SELECT * FROM Win32_PnPEntity";

        string LocalDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
        string LocalUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string AppDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //////
        //string urldir = "\\Documents\\Visual Studio 2015\\Projects\\ArduinoATMC\\ArduinoATMC\\bin\\Release";
        string atmc = "ArduinoATMC";
        string ex = ".exe";

        private static ManagementObjectSearcher xSearch;// = new ManagementObjectSearcher(xScope, xPnp_Q);

        internal void GetArduinoSerialPort_VK()
        {
            // Arduino Driver has to be installed.. but this works, not with the official drivers.. go figure right.. 
            try
            {
                    using (xSearch = new ManagementObjectSearcher(xScope, xPnp_Q))
                    {
                        foreach (ManagementObject queryObj in xSearch.Get())
                        {
                            #region // Serial Device First Check
                            if (queryObj["Caption"].ToString().Contains("(COM") && queryObj["Caption"].ToString().Contains("CH340")) // check once.. for the ATMEGA380
                            {
                                SerialUSB = InfoString + DeviceNP;
                                SerialUSB_P = queryObj["Caption"];
                                SerialDevice = Convert.ToString(SerialUSB_P);
                                #region // Serial Device Second Check
                                if (SerialDevice.Contains("CH340")) // check twice.. for the ATMEGA380
                                {
                                    var CP = SerialDevice.Substring(SerialDevice.Length - 6);
                                    var DV = SerialDevice.Remove(17);
                                    USB_COM_PORT = CP.Substring(CP.Length - 5).Remove(4); // Remove excess parenthesis e.g. ( and ).
                                    USB_COM_NAME = DV;
                                    // USB_COM_GUID = null; // Set to value of the GUID of Device. still to do... as of 26/10/2016
                                    returnString = "COM Port Found: " + USB_COM_PORT + Environment.NewLine + "Device: " + USB_COM_NAME;
                                //deviceInformationLB.Items.Add(USB_COM_PORT);
                                ComInfoTB.Text = USB_COM_PORT;
                                BaudInfoTB.Text = "9600";
                                deviceInformationLB.Items.Add(USB_COM_NAME + "@ " + USB_COM_PORT);
                                    //richTextBox1.Text = returnString;
                                    return;
                                
                                }
                                #endregion
                            }
                            else
                            {
                                
                            }
                            #endregion
                        }
                    }

                return; //returnString;
            }
            catch (ManagementException e)
            {
                ArduinoCodeEditor.Text = e.Message;
                return; // e.Message;
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenArduinoATMC_CMD();
           
        }

        private void OpenArduinoATMC_CMD()
        {
            using (Process processNew = new Process())
            {
                string FN = atmc + ex;
                //string WD = LocalUser + urldir;
                //MessageBox.Show(AppDirectory + FN);
                //richTextBox1.AppendText(WD);
                richTextBox1.AppendText(Environment.NewLine + Environment.NewLine);
                richTextBox1.AppendText(AppDirectory + FN);
                try
                {
                    processNew.StartInfo.WorkingDirectory = AppDirectory;
                    processNew.StartInfo.FileName = FN;
                    processNew.Start();
                    processNew.WaitForExit();
                    if (processNew.HasExited == true)
                    {
                        //deviceInformationLB.Items.Clear();
                        richTextBox1.AppendText( Environment.NewLine + "Arduino ATMC has been closed.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ArduinoACP_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.SysIcon;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UploadToArduino();
        }
        static string _AVR_DIRECTORY;
        static string _AVR_DUDE;
        static string _AVR_CYGWIN;
        static string _AVR_LIBUSB;
        static string _AVR_IMG;
        private void UploadToArduino()
        {
            _AVR_DIRECTORY  = AppDirectory + "AVRAccess\\" ;
            _AVR_DUDE       = "avrdude";
            _AVR_CYGWIN     = "cygwin1";
            _AVR_LIBUSB     = "libusb0";
            _AVR_IMG        = "AVRImage";
            // todo. not complete. not finished... still to do..
            // These files must be part of the installation.
            // They come from the Arduino installation directory arduino/hardware/tools/avr/bin
            // Arduino files now in the app directory AVR alongside Executable, no longer requiring the IDE for this tool.

            if (!File.Exists( _AVR_DIRECTORY + _AVR_DUDE + ".exe"))
            {
                MessageBox.Show("Unable to find AVRDUDE Tool!", "AVRUpdate error");
                return;
            }
            if (!File.Exists(_AVR_DIRECTORY + _AVR_DUDE + ".conf"))
            {
                MessageBox.Show("Unable to find AVRDUDE Configuration File!", "AVRUpdate error");
                return;
            }
            if (!File.Exists(_AVR_DIRECTORY + _AVR_CYGWIN + ".dll"))
            {
                MessageBox.Show("Unable to find AVRDUDE Cygwin 64 Bit DLL!", "AVRUpdate error");
                return;
            }
            if (!File.Exists(_AVR_DIRECTORY + _AVR_LIBUSB + ".dll"))
            {
                MessageBox.Show("Unable to find AVRDUDE USB DLL!", "AVRUpdate error");
                return;
            }

            // THis file is the new image to be uploaded to the Arduino board...
            if (!File.Exists(_AVR_DIRECTORY + _AVR_IMG + ".hex"))
            {
                MessageBox.Show("Unable to find AVR IMAGE!", "AVRUpdate error");
                return;
            }

            statusBox.Text += "(DO NOT RESET OR TURN OFF TILL THIS COMPLETES)\r\n";
            MessageBox.Show("Click OK to Start", "AVR Update");


                string _AVR_PORT = USB_COM_PORT;
                string dir = installDir;
                dir.Replace("\\", "/");

                Process             _AVR_ACCESS_PROCESS     = new Process()             ;
                StreamReader        _AVR_STANDARD_OUTPUT,   _AVR_STANDARD_ERROR         ;
                StreamWriter        _AVR_STANDARD_INPUT                                 ;
                ProcessStartInfo    _CMD_PROCESS_STARTINFO = new ProcessStartInfo("cmd");
            try
            {
                _CMD_PROCESS_STARTINFO.UseShellExecute              = false             ;
                _CMD_PROCESS_STARTINFO.RedirectStandardInput        = true              ;
                _CMD_PROCESS_STARTINFO.RedirectStandardOutput       = true              ;
                _CMD_PROCESS_STARTINFO.RedirectStandardError        = true              ;
                _CMD_PROCESS_STARTINFO.CreateNoWindow               = true              ;

                try
                {
                    _AVR_ACCESS_PROCESS.StartInfo = _CMD_PROCESS_STARTINFO;
                    _AVR_ACCESS_PROCESS.Start();
                }

                catch (Exception K1)
                {
                    MessageBox.Show(K1.Message);
                }

                _AVR_STANDARD_INPUT             = _AVR_ACCESS_PROCESS.StandardInput;
                _AVR_STANDARD_OUTPUT            = _AVR_ACCESS_PROCESS.StandardOutput;
                _AVR_STANDARD_ERROR             = _AVR_ACCESS_PROCESS.StandardError;
                _AVR_STANDARD_INPUT.AutoFlush   = true;
                //avrstdin.WriteLine(installDir + "\\avr\\avrdude.exe -Cavr/avrdude.conf -patmega328p -cstk500v1 -P" + avrport + " -b57600 -D -Uflash:w:" + dir + "/avr/AVRImage.hex:i");
                _AVR_STANDARD_INPUT.WriteLine( _AVR_DIRECTORY + "avrdude.exe -Cavr/avrdude.conf -patmega328p -cstk500v1 -P" + _AVR_PORT + " -b57600 -D -Uflash:w:" + _AVR_DIRECTORY + "AVRImage.hex:i");
                _AVR_STANDARD_INPUT.Close();

                richTextBox1.Text = _AVR_STANDARD_OUTPUT.ReadToEnd();
                richTextBox1.Text += _AVR_STANDARD_ERROR.ReadToEnd();
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }

        private void DetectArduino_Click(object sender, EventArgs e)
        {
            try
            {
                GetArduinoSerialPort_VK();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
    }
}
