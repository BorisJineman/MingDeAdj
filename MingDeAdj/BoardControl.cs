using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MingDeAdj
{
    public partial class BoardControl : UserControl
    {
        public BoardControl()
        {
            InitializeComponent();
        }

        public void SetBoardID(int id)
        {
            currentID = (byte)(id&0xff);
            this.label2.Text = "Board " + currentID.ToString();
        }

        byte currentID;

        //Common Device All Status Data Read  MSG ID 0
        private void button3_Click(object sender, EventArgs e)
        {

            //  Sender + Receiver + Protocol
            // 0
            List<byte> sendDatas = new List<byte>();
            sendDatas.AddRange(Sys.ReceiverMAC);
            sendDatas.AddRange(Sys.SenderMAC);
            sendDatas.AddRange(Sys.ProtocolType);

            // Addr ID
            // 14
            sendDatas.Add(currentID);

            // Command ID 
            // 15
            sendDatas.Add(0x00);

            // x bytes for LOAD
            // 16 - Device Status
            sendDatas.Add(0x00);

            // 17 - DI Status
            sendDatas.AddRange(new byte[2]);

            // 18 - DO Status
            sendDatas.Add(0x00);

            // 20 - AI Status
            sendDatas.AddRange(new byte[228]);

            // 152 - AQ Status
            sendDatas.AddRange(new byte[16]);

            // about 168 Bytes need to be sent

            Sys.Dev.SendPacket(sendDatas.ToArray());
            label3.Text = "Read Cfg Data ...";
            label6.Text = "Unknown";
        }


        public void PacketReceived(byte[] p)
        {
           // MessageBox.Show(BitConverter.ToString(p));
            try {             
            if (p[15] % 2 == 1)
            {
                if (p[14] == currentID)
                {
                    label6.Text = "Online";
                    switch (p[15])
                    {
                        case 1:

                            label3.Text = "Read Cfg Data OK.";
                            for (int i = 0; i < 16; i++)
                            {
                                diCheckBoxs[i].Checked = Convert.ToBoolean((BitConverter.ToUInt16(p, 17) >> i) & 0x01);

                            }

                            for (int i = 0; i < 8; i++)
                            {
                                doCheckBoxs[i].Checked = Convert.ToBoolean((p[19] >> i) & 0x01);

                            }

                            for (int i = 0; i < 19;i++ )
                            {
                                aiCurrentTextBox[i].Text = BitConverter.ToInt32(p, 20 + i * 4).ToString();
                                aiFullTextBox[i].Text = BitConverter.ToInt32(p, 96 + i * 4).ToString();
                                aiEmptyTextBox[i].Text = BitConverter.ToInt32(p, 172 + i * 4).ToString();
                            }

                            for (int i = 0; i < 2; i++)
                            {
                                aoOutput[i].Text = BitConverter.ToInt32(p, 248 + i * 4).ToString();
                                aoActual[i].Text = BitConverter.ToInt32(p, 256 + i * 4).ToString();
                            }

                            break;

                        case 3:
                            label3.Text = "Write Cfg Data OK.";

                            break;

                        default:

                           
                            break;

                    }
                }
            }}
                catch(Exception ex)
            { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
        }


        List<CheckBox> diCheckBoxs = new List<CheckBox>();
        List<CheckBox> doCheckBoxs = new List<CheckBox>();
        List<TextBox> aiCurrentTextBox = new List<TextBox>();
        List<TextBox> aiFullTextBox = new List<TextBox>();
        List<TextBox> aiEmptyTextBox = new List<TextBox>();
        List<TextBox> aoOutput = new List<TextBox>();
        List<TextBox> aoActual = new List<TextBox>();

        private void BoardControl_Load(object sender, EventArgs e)
        {

            // di 8
            diCheckBoxs.Add(checkBox1);
            diCheckBoxs.Add(checkBox2);
            diCheckBoxs.Add(checkBox3);
            diCheckBoxs.Add(checkBox4);
            diCheckBoxs.Add(checkBox5);
            diCheckBoxs.Add(checkBox6);
            diCheckBoxs.Add(checkBox7);
            diCheckBoxs.Add(checkBox8);
            diCheckBoxs.Add(checkBox17);
            diCheckBoxs.Add(checkBox18);
            diCheckBoxs.Add(checkBox19);
            diCheckBoxs.Add(checkBox20);
            diCheckBoxs.Add(checkBox21);
            diCheckBoxs.Add(checkBox22);
            diCheckBoxs.Add(checkBox23);
            diCheckBoxs.Add(checkBox24);
                                     
            //do 16
            doCheckBoxs.Add(checkBox9);
            doCheckBoxs.Add(checkBox10);
            doCheckBoxs.Add(checkBox11);
            doCheckBoxs.Add(checkBox12);
            doCheckBoxs.Add(checkBox13);
            doCheckBoxs.Add(checkBox14);
            doCheckBoxs.Add(checkBox15);
            doCheckBoxs.Add(checkBox16);

            //ai current 11
            aiCurrentTextBox.Add(textBox4);
            aiCurrentTextBox.Add(textBox5);
            aiCurrentTextBox.Add(textBox6);
            aiCurrentTextBox.Add(textBox7);
            aiCurrentTextBox.Add(textBox8);
            aiCurrentTextBox.Add(textBox9);
            aiCurrentTextBox.Add(textBox10);
            aiCurrentTextBox.Add(textBox11);
            aiCurrentTextBox.Add(textBox28);
            aiCurrentTextBox.Add(textBox31);
            aiCurrentTextBox.Add(textBox34);
            aiCurrentTextBox.Add(textBox3);
            aiCurrentTextBox.Add(textBox41);
            aiCurrentTextBox.Add(textBox44);
            aiCurrentTextBox.Add(textBox47);
            aiCurrentTextBox.Add(textBox50);
            aiCurrentTextBox.Add(textBox53);
            aiCurrentTextBox.Add(textBox56);
            aiCurrentTextBox.Add(textBox59);

            //ai full 11
            aiFullTextBox.Add(textBox12);
            aiFullTextBox.Add(textBox13);
            aiFullTextBox.Add(textBox14);
            aiFullTextBox.Add(textBox15);
            aiFullTextBox.Add(textBox16);
            aiFullTextBox.Add(textBox17);
            aiFullTextBox.Add(textBox18);
            aiFullTextBox.Add(textBox19);
            aiFullTextBox.Add(textBox29);
            aiFullTextBox.Add(textBox32);
            aiFullTextBox.Add(textBox35);
            aiFullTextBox.Add(textBox37);
            aiFullTextBox.Add(textBox42);
            aiFullTextBox.Add(textBox45);
            aiFullTextBox.Add(textBox48);
            aiFullTextBox.Add(textBox51);
            aiFullTextBox.Add(textBox54);
            aiFullTextBox.Add(textBox57);
            aiFullTextBox.Add(textBox60);

            //ai empty 11
            aiEmptyTextBox.Add(textBox20);
            aiEmptyTextBox.Add(textBox21);
            aiEmptyTextBox.Add(textBox22);
            aiEmptyTextBox.Add(textBox23);
            aiEmptyTextBox.Add(textBox24);
            aiEmptyTextBox.Add(textBox25);
            aiEmptyTextBox.Add(textBox26);
            aiEmptyTextBox.Add(textBox27);
            aiEmptyTextBox.Add(textBox30);
            aiEmptyTextBox.Add(textBox33);
            aiEmptyTextBox.Add(textBox36);
            aiEmptyTextBox.Add(textBox39);
            aiEmptyTextBox.Add(textBox43);
            aiEmptyTextBox.Add(textBox46);
            aiEmptyTextBox.Add(textBox49);
            aiEmptyTextBox.Add(textBox52);
            aiEmptyTextBox.Add(textBox55);
            aiEmptyTextBox.Add(textBox58);
            aiEmptyTextBox.Add(textBox61);


            //ao output 2
            aoOutput.Add(textBox1);
            aoOutput.Add(textBox2);

            //ao actual 2
            aoActual.Add(textBox38);
            aoActual.Add(textBox40);
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                //  Sender + Receiver + Protocol
                // 0
                List<byte> sendDatas = new List<byte>();
                sendDatas.AddRange(Sys.ReceiverMAC);
                sendDatas.AddRange(Sys.SenderMAC);
                sendDatas.AddRange(Sys.ProtocolType);

                // Addr ID
                // 14
                sendDatas.Add(currentID);

                // Command ID 
                // 15
                sendDatas.Add(0x03);

                // x bytes for LOAD
                // 16 - Device Status
                sendDatas.Add(0x00);

                // 17 - di
                UInt16 di = 0;
                for (int i = 0; i < 16; i++)
                {
                    di |= (UInt16)((diCheckBoxs[i].Checked ? 1 : 0) << 8);
                    di = (UInt16)(di >> 1);
                }

                sendDatas.AddRange(BitConverter.GetBytes(di));

                // 18 - do
                byte dodata = 0;
                for (int i = 0; i < 8; i++)
                {
                    dodata |= (byte)((doCheckBoxs[i].Checked ? 1 : 0) << 16);
                    dodata = (byte)(dodata >> 1);

                }
                sendDatas.Add(dodata);

                // 20 - ai
                List<byte> aiCurrentTemp = new List<byte>();
                List<byte> aiFullTemp = new List<byte>();
                List<byte> aiEmptyTemp = new List<byte>();
                for (int i = 0; i < 19; i++)
                {
                    aiCurrentTemp.AddRange(BitConverter.GetBytes(Convert.ToInt32(aiCurrentTextBox[i].Text)));
                    aiFullTemp.AddRange(BitConverter.GetBytes(Convert.ToInt32(aiFullTextBox[i].Text)));
                    aiEmptyTemp.AddRange(BitConverter.GetBytes(Convert.ToInt32(aiEmptyTextBox[i].Text)));
                }
                sendDatas.AddRange(aiCurrentTemp);
                sendDatas.AddRange(aiFullTemp);
                sendDatas.AddRange(aiEmptyTemp);

                // 152 - ao
                List<byte> aoOutputTemp = new List<byte>();
                List<byte> aoActualTemp = new List<byte>();
                for (int i = 0; i < 2; i++)
                {
                    aoOutputTemp.AddRange(BitConverter.GetBytes(Convert.ToInt32(aoOutput[i].Text)));
                    aoActualTemp.AddRange(BitConverter.GetBytes(Convert.ToInt32(aoActual[i].Text)));
                }
                sendDatas.AddRange(aoOutputTemp);
                sendDatas.AddRange(aoActualTemp);

                // about 168 bytes need to be sent

                Sys.Dev.SendPacket(sendDatas.ToArray());

                label3.Text = "Write Cfg Data ...";
                label6.Text = "Unknown";

            }
            catch (Exception ex)
            {
                label3.Text = ex.Message;
            }
        }
    }
}
