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
// INSPIRED CODE BORROWED FROM ARDUINO FORUMS.
// CONTAINS MODIFICATIONS OF CODE. 
// SEE ARDUINO THREAD TITLED 'ARDUINO ACP'.
namespace ArduinoACP
{
    public partial class ArduinoACP : Form
    {
        public ArduinoACP()
        {
            InitializeComponent();
            ApplicationLogText.Text   = "Log: ";
            ComInfoTB.Text      = "";
            BaudInfoTB.Text     = "";
        }
        // 32 and 64 Bit Directories.
        static string _APR64   = Application.StartupPath + "\\x64\\AVRAccess"; // 64 BIT APR
        static string _APR32   = Application.StartupPath + "\\x86\\AVRAccess"; // 32 BIT APR

        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// Information About Arduino Device.
        /// ///////////////////////////////////////////////////////////////////////////////////////
        
        static string _AVR_USB_COM_PORT;            // COM DEVICE PORT.
        static string _AVR_USB_COM_NAME;            // COM DEVICE NAME.   
        static string _AVR_USB_COM_GUID;            // COM DEVICE GUID.
        static string _AVR_SERIAL_USB_DEVICE;       // SERIAL DEVICE

        static string _AVR_INFO_DEVICE_STRING    = "Serial Port : ";    // INFO PORT STRING
        static string _AVR_INFO_DEVICE_NP        = "{0}";               // RETURNED PORT STRING

        static string _AVR_SERIAL_DEVICE_USB     = null;                // serial device

        static object _AVR_SERIAL_DEVICE_USB_CAPTION  = null;                // serial device 'caption'

        private static string _AVR_returnString  = null;                // final return string.
        private static string _AVR_xScope        = "root\\CIMV2";                   // THINK YOU CAN GUESS THIS ONE?
        private static string _AVR_xPnp_Q        = "SELECT * FROM Win32_PnPEntity"; // AMD THIS!

        private string _AVR_INSTALL_DIR           = Environment.CurrentDirectory;
        private string _AVR_LOCAL_DRIVE           = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
        private string _AVR_LOCAL_USER            = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        string _AVR_ATMC_PROCESS = "ArduinoATMC"; string _AVR_ATMC_EXTENTION = ".exe";
        string _AVR_ACP_PROCESS = "ArduinoACP"; string _AVR_ACP_EXTENTION = ".exe";

        private static ManagementObjectSearcher xSearch;// = new ManagementObjectSearcher(xScope, xPnp_Q);       
        internal void GetArduinoSerialPort_VK()
        {
            // Arduino Driver has to be installed.. but this works, not with the official drivers.. go figure right.. 
            try
            {
                    using (xSearch = new ManagementObjectSearcher(_AVR_xScope, _AVR_xPnp_Q))
                    {
                        foreach (ManagementObject queryObj in xSearch.Get())
                        {
                            #region // Serial Device First Check
                            if (queryObj["Caption"].ToString().Contains("(COM") && queryObj["Caption"].ToString().Contains("CH340")) // check once.. for the ATMEGA380
                            {
                                _AVR_SERIAL_USB_DEVICE = _AVR_INFO_DEVICE_STRING + _AVR_INFO_DEVICE_NP; // set the usb device string and np variable.

                                _AVR_SERIAL_DEVICE_USB_CAPTION = queryObj["Caption"];
                                _AVR_SERIAL_DEVICE_USB = Convert.ToString(_AVR_SERIAL_DEVICE_USB_CAPTION);

                                #region // Serial Device Second Check
                                if (_AVR_SERIAL_DEVICE_USB.Contains("CH340")) // check twice.. for the ATMEGA380 CHIP.. for safe measure.
                                {
                                    // using math. we work out the placement of the strings positions relating to the CH340 in Windows thats gets detected.. if you have no driver,
                                    // the driver used in conjunction with this is provided for ease...
                                    // this development machine is windows 7 64 bit. the projected system is windows 7. we prefer windows 7 to 
                                    var CP = _AVR_SERIAL_DEVICE_USB.Substring(_AVR_SERIAL_DEVICE_USB.Length - 6);
                                    // Remove some strings from the front..
                                    var DV = _AVR_SERIAL_DEVICE_USB.Remove(17);
                                    // and back of the strings.. realtime text manipulation.. you know, reminds me of the matrix.. ;).
                                    _AVR_USB_COM_PORT = CP.Substring(CP.Length - 5).Remove(4); 
                                    // Remove excess parenthesis e.g. ( and ).
                                    _AVR_USB_COM_NAME = DV;
                                    // USB_COM_GUID = null; // Set to value of the GUID of Device. still to do... as of 26/10/2016
                                    _AVR_returnString = "COM Port Found: " + _AVR_USB_COM_PORT + Environment.NewLine + "Device: " + _AVR_USB_COM_NAME;
                                    //deviceInformationLB.Items.Add(USB_COM_PORT);
                                    ComInfoTB.Text = _AVR_USB_COM_PORT;
                                    BaudInfoTB.Text = "9600";
                                    BaudInfoTB.Enabled = true;
                                    deviceInformationLB.Items.Add(_AVR_USB_COM_NAME + "is located on: " + _AVR_USB_COM_PORT);
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
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            OpenArduinoATMC_CMD();
            statusBox.Text = "";
        }
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////////////////////////////////////////////////
        private void OpenArduinoATMC_CMD()
        {
            using (Process processNew = new Process())
            {
                string FN = _AVR_ATMC_PROCESS + _AVR_ATMC_EXTENTION;
                //string WD = LocalUser + urldir;
                //MessageBox.Show(AppDirectory + FN);
                //richTextBox1.AppendText(WD);
                ApplicationLogText.AppendText(Environment.NewLine + Environment.NewLine);
                ApplicationLogText.AppendText(Application.StartupPath + FN);
                try
                {
                    processNew.StartInfo.WorkingDirectory = Application.StartupPath;
                    processNew.StartInfo.FileName = FN;
                    processNew.Start();
                    processNew.WaitForExit();
                    if (processNew.HasExited == true)
                    {
                        //deviceInformationLB.Items.Clear();
                        ApplicationLogText.AppendText( Environment.NewLine + "Arduino ATMC has been closed.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////////////////////////////////////////////////
        private void ArduinoACP_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.SysIcon;
            statusBox.Text = "";
        }
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////////////////////////////////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            UploadToArduino();
        }
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////////////////////////////////////////////////
        static string _AVR_DIRECTORY;
        static string _AVR_DUDE;
        static string _AVR_CYGWIN;
        static string _AVR_LIBUSB;
        static string _AVR_IMG;
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////////////////////////////////////////////////       
        public void CheckFiles()
        {
            
            try
            {
                
                MessageBox.Show(_APR64);

                string[] AVR_DIR = Directory.GetFiles(_APR64, ".hex*");

                foreach (string D in AVR_DIR)
                {
                    ArduinoCodeEditor.Text = D;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Process Failed: {0}: " + ex.ToString());
            }

        }
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////////////////////////////////////////////////
        private void UploadToArduino()
        {
            _AVR_DIRECTORY  = _APR64;
            _AVR_DUDE       = "avrdude";
            _AVR_CYGWIN     = "cygwin1";
            _AVR_LIBUSB     = "libusb0";
            _AVR_IMG        = "AVRImage"; // avrimage name... 
            

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

            if (!File.Exists(_AVR_DIRECTORY + _AVR_IMG + ".hex"))
            {
                MessageBox.Show("Unable to find AVR IMAGE!", "AVRUpdate error");
                return;
            }

            statusBox.Text += "(DO NOT RESET OR TURN OFF TILL THIS COMPLETES)\r\n";
            MessageBox.Show("Click OK to Start", "AVR Update");


                string _AVR_PORT = _AVR_USB_COM_PORT;

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
                this.statusBox.Text = "Uploading to Device...";
                _AVR_STANDARD_INPUT.WriteLine( _AVR_DIRECTORY + "avrdude.exe -Cavr/avrdude.conf -patmega328p -cstk500v1 -P" + _AVR_PORT + " -b57600 -D -Uflash:w:" + _AVR_DIRECTORY + "AVRImage.hex:i");
                _AVR_STANDARD_INPUT.Close();

                ApplicationLogText.Text = _AVR_STANDARD_OUTPUT.ReadToEnd();
                ApplicationLogText.Text += _AVR_STANDARD_ERROR.ReadToEnd();
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
                this.statusBox.Text = "Upload to Device Failed..";
            }
        }
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////////////////////////////////////////////////
        private void DetectArduino_Click(object sender, EventArgs e)
        {
            try
            {
                this.statusBox.Text = "Getting Arduino COM PORT...";
                GetArduinoSerialPort_VK();
                this.statusBox.Text = "...";

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
                this.statusBox.Text = "Failed in Getting Arduino COM PORT...";
            }
        }
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////////////////////////////////////////////////
        private void CheckHexBtn_Click(object sender, EventArgs e)
        {
            CheckFiles();
        }
    }
}
