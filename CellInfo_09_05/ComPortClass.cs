using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace CellInfo
{
    public class ComPortClass
    {
        public static SerialPort sp = new SerialPort ( );
        
        //���캯������ʼ����������ʵ��
        public ComPortClass ()
        {          
            sp.ReceivedBytesThreshold = 16;

            //sp.ReadTimeout = 800;
            //sp.WriteTimeout = 800;
            //sp.DataReceived += new SerialDataReceivedEventHandler(DataReceived); 
            //sp.ErrorReceived += new SerialErrorReceivedEventHandler(ErrorReceived);             
        }

        public static void Port_Assignment(string Portname, int Baudrate, string handshake, string parity, int Databits, string Stopbits)
        {
            //SerialPort(String, Int32, Parity, Int32, StopBits) 
            //ʹ��ָ���Ķ˿����ơ������ʡ���żУ��λ������λ��ֹͣλ��ʼ�� SerialPort �����ʵ���� 
            sp.PortName = Portname;
            sp.BaudRate = Baudrate;

            switch (handshake)
            {
                case "None":
                    sp.Handshake = Handshake.None;
                    break;
                case "XonXoff":
                    sp.Handshake = Handshake.XOnXOff;
                    break;
                case "RequestToSend":
                    sp.Handshake = Handshake.RequestToSend;
                    break;
                case "RequestToSendXonXoff":
                    sp.Handshake = Handshake.RequestToSendXOnXOff;
                    break;
                default:
                    MessageBox.Show("Handshake��δ����", "��ʾ��Ϣ");
                    break;
            }

            switch (parity)
            {
                case "None":
                    sp.Parity = Parity.None;
                    break;
                case "Odd":
                    sp.Parity = Parity.Odd;
                    break;
                case "Even":
                    sp.Parity = Parity.Even;
                    break;
                case "Mark":
                    sp.Parity = Parity.Mark;
                    break;
                case "Space":
                    sp.Parity = Parity.Space;
                    break;
                default:
                    MessageBox.Show("Parity��δ����", "��ʾ��Ϣ");
                    break;
            }

            switch (Stopbits)
            {
                //case "None":
                //     sp.StopBits = StopBits.None;
                //    break;    
                //Setting this property to StopBits.None or StopBits.OnePointFive will throw an exception
                //ArgumentOutOfRangeException:The StopBits value is StopBits.None.
                // The default value for StopBits is One.
                //The StopBits.None value is not supported.

                case "One":
                    sp.StopBits = StopBits.One;
                    break;
                case "Two":
                    sp.StopBits = StopBits.Two;
                    break;
                case "OnePointFive":
                    sp.StopBits = StopBits.OnePointFive;
                    break;
                default:
                    MessageBox.Show("Stop Bits��δ����", "��ʾ��Ϣ");
                    break;
            }

            sp.DataBits = Databits;
        }


        //------------------------���ڲ������ֿ�ʼ------------------------- 

        private SerialDataReceivedEventHandler SerialDataReceivedEventHandler1;
        private SerialErrorReceivedEventHandler SerialErrorReceivedEventHandler1;

        /// <summary>
        /// If the COM port is open, close it.
        /// </summary>
        /// 
        /// <param name="portToClose"> the SerialPort object to close </param>  

        internal void CloseComPort(SerialPort portToClose)
        {            
                //if (null != UserInterfaceData) UserInterfaceData("DisplayStatus", "", Color.Black);

                object transTemp0 = portToClose;
                if (!(transTemp0 == null))
                {
                    if (portToClose.IsOpen)
                    {
                        portToClose.Close();
                        //if (null != UserInterfaceData) UserInterfaceData("DisplayCurrentSettings", "", Color.Black);
                    }
                }            
        }        

        /// <summary>
        /// Provide a central mechanism for displaying exception information.
        /// Display a message that describes the exception.
        /// </summary>
        /// 
        /// <param name="ex"> The exception </param> 
        /// <param name="moduleName" > the module where the exception was raised. </param>

        private void DisplayException(string moduleName, Exception ex)
        {
            string errorMessage = null;

            errorMessage = "Exception: " + ex.Message + " Module: " + moduleName + ". Method: " + ex.TargetSite.Name;

            //if (null != UserInterfaceData) UserInterfaceData("DisplayStatus", errorMessage, Color.Red);

            //  To display errors in a message box, uncomment this line:
            //  MessageBox.Show(errorMessage)
        }

        /// <summary>
        /// Respond to error events.
        /// </summary>

        private void ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            SerialError SerialErrorReceived1 = 0;

            SerialErrorReceived1 = e.EventType;

            switch (SerialErrorReceived1)
            {
                case SerialError.Frame:
                    Console.WriteLine("Framing error.");

                    break;
                case SerialError.Overrun:
                    Console.WriteLine("Character buffer overrun.");

                    break;
                case SerialError.RXOver:
                    Console.WriteLine("Input buffer overflow.");

                    break;
                case SerialError.RXParity:
                    Console.WriteLine("Parity error.");

                    break;
                case SerialError.TXFull:
                    Console.WriteLine("Output buffer full.");
                    break;
            }
        }       

        /// <summary>
        /// Open the SerialPort object selectedPort.
        /// If open, close the SerialPort object previousPort.
        /// </summary>

        internal bool OpenComPort()
        {
            bool success = false;
            SerialDataReceivedEventHandler1 = new SerialDataReceivedEventHandler(DataReceived);
            SerialErrorReceivedEventHandler1 = new SerialErrorReceivedEventHandler(ErrorReceived);

                           
            //  The system has at least one COM port.
            //  If the previously selected port is still open, close it.

        //    if (sp.IsOpen)
          //  {
            //    CloseComPort(sp);
           // }

             if ((!(sp.IsOpen)))
            {
                sp.Open();

                if (sp.IsOpen)
                {
                    //  The port is open. Set additional parameters.
                    //  Timeouts are in milliseconds.

                    sp.ReadTimeout = 5000;
                    sp.WriteTimeout = 5000;

                    //  Specify the routines that run when a DataReceived or ErrorReceived event occurs.

                    sp.DataReceived += SerialDataReceivedEventHandler1;
                    sp.ErrorReceived += SerialErrorReceivedEventHandler1;

                    //  Send data to other modules.

                    //if (null != UserInterfaceData) UserInterfaceData("DisplayCurrentSettings", "", Color.Black);
                    //if (null != UserInterfaceData) UserInterfaceData("DisplayStatus", "", Color.Black);

                    success = true;

                    //  The port is open with the current parameters.
                                                    
                }               
            } 
            return success;
        } 

        // ����
        public string Dial(long  phoneNum,bool ifTerminate)
        {
            string cmd = "ATD" + Convert.ToString(phoneNum) + ";\r";
            sp.Write(cmd);
            Thread.Sleep(360);
            byte[] buffer = new byte[sp.BytesToRead];
            sp.Read(buffer, 0, buffer.Length);
             if (ifTerminate) sp.Write("ATH\r");
            return System.Text.Encoding.ASCII.GetString(buffer);
           
        }

        //��ǰС����ϢAT����
        public string AT_Serving()
        {
            try
            {
                sp.Write("AT^MONI\r");
                Thread.Sleep(360);
                byte[] buffer = new byte[sp.BytesToRead];
                sp.Read(buffer, 0, buffer.Length);
                return System.Text.Encoding.ASCII.GetString(buffer);
            }
            catch
            {
                return "����ʧ�ܣ�";
            }
        }

        //�ھ�С����ϢAT����
        public string AT_Neighbor()
        {
            try
            {
                sp.Write("AT^MONP\r");
                Thread.Sleep(360);
                byte[] buffer = new byte[sp.BytesToRead];
                sp.Read(buffer, 0, buffer.Length);
                return System.Text.Encoding.ASCII.GetString(buffer);
            }
            catch
            {
                return "����ʧ�ܣ�";
            }
        }       

        //С�����_1AT����
        public string AT_CellMonitor_1()
        {
            try
            {
                sp.Write("AT^SMONC\r");
                Thread.Sleep(360);
                byte[] buffer = new byte[sp.BytesToRead];
                sp.Read(buffer, 0, buffer.Length);
                return System.Text.Encoding.ASCII.GetString(buffer);
            }
            catch
            {
                return "����ʧ�ܣ�";
            }
        }

        //С�����_2AT����
        public string AT_CellMonitor_2()
        {
            try
            {
                sp.Write("AT^SMNOD\r");
                Thread.Sleep(360);
                byte[] buffer = new byte[sp.BytesToRead];
                sp.Read(buffer, 0, buffer.Length);
                return System.Text.Encoding.ASCII.GetString(buffer);
            }
            catch
            {
                return "����ʧ�ܣ�";
            }
        }

        //�رմ��� 
        public void CloseCom ( )
        {
            if (sp.IsOpen)
            {
                sp.Dispose();
                sp.Close();
            }
        }

        //------------------------���ڲ������ֽ���------------------------- 

        //--------------------------�¼����ֿ�ʼ-------------------- 

        //�����¼�
        private void DataReceived ( Object sender , SerialDataReceivedEventArgs e ) 
        {
            System.Windows.Forms.MessageBox.Show ( sp.ReadExisting ( ) );
        }

        
        //--------------------------�¼����ֽ���-------------------- 

    }
}