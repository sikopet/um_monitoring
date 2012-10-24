using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

namespace CellInfo
{
    public class ComPortClass
    {
        public SerialPort sp = new SerialPort ( );

        //���캯������ʼ����������ʵ��
        public ComPortClass ( string PortName , int PortNum )
        {
            sp = new SerialPort ( PortName , PortNum , Parity.None , 8 );
            sp.ReceivedBytesThreshold = 16;
            sp.Handshake = Handshake.RequestToSend;
            sp.Parity = Parity.None;
            sp.ReadTimeout = 800;
            sp.WriteTimeout = 800;
            //sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived); 
            //sp.ErrorReceived += new SerialErrorReceivedEventHandler(sp_ErrorReceived); 
            sp.Open ( );
        }

        //------------------------���ڲ������ֿ�ʼ------------------------- 

        //��ǰС����ϢAT����
        public string AT_Serving()
        {
            try
            {
                sp.Write("AT^MONI\r");
                Thread.Sleep(100);
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
                Thread.Sleep(100);
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
                Thread.Sleep(100);
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
                Thread.Sleep(100);
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
            sp.Dispose ( );
            sp.Close ( );
        }

        //------------------------���ڲ������ֽ���------------------------- 

        //--------------------------�¼����ֿ�ʼ-------------------- 

        //�����¼�
        private void sp_DataReceived ( Object sender , SerialDataReceivedEventArgs e ) 
        {
            System.Windows.Forms.MessageBox.Show ( sp.ReadExisting ( ) );
        }

        //�����¼� 
        private void sp_ErrorReceived ( Object sender , SerialErrorReceivedEventArgs e )
        {
        }
        //--------------------------�¼����ֽ���-------------------- 

    }
}