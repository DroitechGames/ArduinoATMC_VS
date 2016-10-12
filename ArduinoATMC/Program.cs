using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO.Ports;

namespace ArduinoATMC
{
    public class Program
    {
        static private SerialPort ArduinoPort;
        
        /// <summary>
        ///  USB_COM_PORT is the port that is used to communicate over serial connection with the Arduino UNO ATMEGA 380 Chip.
        ///  USB_COM_NAME is the port name that is used to communicate over serial connection with the Arduino UNO ATMEGA 380 Chip.
        ///  USB_COM_GUID is the device GUID that is obtained from the Driver Registry. // This is also developed with a custom ticking
        ///  system that flashes leds on the device.
        /// </summary>
        static string USB_COM_PORT;
        static string USB_COM_NAME;
        static string USB_COM_GUID; // COM DEVICE GUID.
        /// <summary>
        /// Serial Device is the Serial Device.
        /// Info String is the information string before the device is printed to screen.
        /// </summary>
        static string SerialDevice; 
        static string InfoString = "Serial Port : ";
        static string DeviceNP = "{0}";
        static string SerialUSB = null; 
        static object SerialUSB_P = null;
        /// <summary>
        /// GetArduinoSerialPort_VK() is the function to call to AUTODETECT the COM Serial Port on Windows and the Arduino UNO ATMEGA380 Chip.
        /// </summary>
        private static void GetArduinoSerialPort_VK()
        {
            // Arduino Driver has to be installed.. but this works, not with the official drivers.. go figure right.. 
            try
            {
                ///
                string xScope = "root\\CIMV2";
                string xPnp_Q = "SELECT * FROM Win32_PnPEntity";
                ///
                using (ManagementObjectSearcher xSearch = new ManagementObjectSearcher(xScope, xPnp_Q))
                {
                    foreach (ManagementObject queryObj in xSearch.Get())
                    {
                        if (queryObj["Caption"].ToString().Contains("(COM") && queryObj["Caption"].ToString().Contains("CH340"))
                        {
                            SerialUSB = InfoString + DeviceNP;
                            SerialUSB_P = queryObj["Caption"];
                            SerialDevice = Convert.ToString(SerialUSB_P);
                            if (SerialDevice.Contains("CH340")) // ATMEGA380
                            {
                                var CP = SerialDevice.Substring(SerialDevice.Length - 6);
                                var DV = SerialDevice.Remove(17);
                                USB_COM_PORT = CP.Substring(CP.Length - 5).Remove(4); // Remove excess parenthesis e.g. ( and ).
                                USB_COM_NAME = DV;
                                USB_COM_GUID = null; // Set to value of the GUID of Device. to do...
                                Console.WriteLine("COM Port Found: " + USB_COM_PORT + Environment.NewLine + "Device: " + USB_COM_NAME);
                            }
                        }
                    }
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            ArduinoPort = new SerialPort();
            var portNames = SerialPort.GetPortNames();
            // Arduino Chip Name
            string ArduinoUNO_CHIP = "CH340";
            string ArduinoUNO_BOARD = "ARDUINO UNO: ";
            string ArduinoUNO_RV = " R3";
            // Console text.
            string CText1 = "Welcome to the ATMEGA380 (ARDUINO UNO) Port Detector " + Environment.NewLine ;
            string CText2 = "Baud Rate: " ;
            string CText3 = "You need to attach the " + ArduinoUNO_BOARD + ArduinoUNO_CHIP + ArduinoUNO_RV;
            //Console.WindowWidth = 70;
            //Console.
            //Console.SetWindowSize(170, 300);
            Console.Title = "Arduino UNO Console Development Interface - ATMEGA380";
            Console.WriteLine(CText1);
            GetArduinoSerialPort_VK();
            try
            {
                ArduinoPort.PortName = USB_COM_PORT;
                ArduinoPort.BaudRate = 9600;
                Console.WriteLine(CText2 + ArduinoPort.BaudRate + "@" + ArduinoPort.PortName + Environment.NewLine);

                try
                {
                    ArduinoPort.Open();
                    while (true)
                    {
                        string DeviceData_RX = ArduinoPort.ReadLine();
                        Console.WriteLine("IN: " + ArduinoPort.BaudRate + "@" + USB_COM_PORT + "-RX: " + DeviceData_RX);
                    }

                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                    Console.WriteLine("Device has been removed...");
                    Console.WriteLine("Press Enter to Return.");
                    Console.ReadLine();
                }
            }
            catch (ArgumentNullException NEX)
            {
                Console.WriteLine("No Device Detected in COM Port");
                Console.WriteLine(CText3);
                Console.WriteLine("Press Enter to Return.");
                Console.ReadLine();
            }
            Console.WriteLine("Press Enter to Exit.");
            Console.ReadLine();
        }
    }
}
