using SharpPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MingDeAdj
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            boardControl1.SetBoardID(0);
            boardControl2.SetBoardID(1);
            boardControl3.SetBoardID(2);
            boardControl4.SetBoardID(3);


            CaptureDeviceList allDevices = SharpPcap.CaptureDeviceList.Instance;

            foreach (ICaptureDevice dev in allDevices)
            {
                Device cdev = new Device();
                cdev.DisplayName = dev.Description;
                cdev.CaptureDevice = dev;
                comboBox1.Items.Add(cdev);
            }


            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sys.Dev = ((Device)comboBox1.SelectedItem).CaptureDevice;
            Sys.Dev.Open();

            Sys.ProtocolType = BitConverter.GetBytes(Convert.ToUInt16(0x9220));
            Sys.Dev.Filter = "ether proto 0x2092";

            Sys.SenderMAC = Sys.Dev.MacAddress.GetAddressBytes();  
            Sys.ReceiverMAC = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

            Sys.Dev.OnPacketArrival += Dev_OnPacketArrival;
            Sys.Dev.StartCapture();
            comboBox1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = true;
            boardControl1.Enabled = true;
            boardControl2.Enabled = true;
            boardControl3.Enabled = true;
            boardControl4.Enabled = true;


            toolStripStatusLabel2.Text = comboBox1.SelectedItem.ToString();
        }
       // delegate void PkgProcessDele(byte[])
        void Dev_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            this.Invoke(new Action<byte[]>(p => 
            { this.boardControl1.PacketReceived(p); 
                this.boardControl2.PacketReceived(p); 
                this.boardControl3.PacketReceived(p); 
                this.boardControl4.PacketReceived(p); }), new object[] { e.Packet.Data });
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sys.Dev.StopCapture();
            Sys.Dev.OnPacketArrival -= Dev_OnPacketArrival;

            Sys.Dev.Close();
            Sys.Dev = null;
            
            comboBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = false;


            boardControl1.Enabled = false;
            boardControl2.Enabled = false;
            boardControl3.Enabled = false;
            boardControl4.Enabled = false;

            toolStripStatusLabel2.Text = "<Null>";

        }

        
    }
}
