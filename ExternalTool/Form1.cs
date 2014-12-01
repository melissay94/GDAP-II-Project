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
        //Zoe McHenry
        public Form1()
        {
            InitializeComponent();
        }

        //Save
        private void button1_Click(object sender, EventArgs e)
        {
            BinaryWriter saveWriter = null;
            try
            {
                Stream saveFile = File.OpenWrite("../../../AlpacasWithBonnets/AlpacasWithBonnets/bin/x86/Debug/" + textBox1.Text + ".alpaca");
                saveWriter = new BinaryWriter(saveFile);
                saveWriter.Write(numericUpDown1.Value);
                saveWriter.Write(numericUpDown2.Value);
                MessageBox.Show("File saved.");
            }
            catch
            {
                MessageBox.Show("File save error.");
            }
            finally
            {
                saveWriter.Close();
            }
        }

        //Load
        private void button2_Click(object sender, EventArgs e)
        {
            BinaryReader saveReader = null;
            int health = 1;
            int power = 0;
            try
            {
                Stream saveFile = File.OpenRead("../../../AlpacasWithBonnets/AlpacasWithBonnets/bin/x86/Debug/" + textBox1.Text + ".alpaca");
                //NOTE: For the purpose of testing we NEED to save this file as "testfile.alpaca"
                //at least until I can implement the final version of character file selection ingame
                //-ZM

                saveReader = new BinaryReader(saveFile);
                numericUpDown1.Value = saveReader.ReadInt32(); //Health
                numericUpDown2.Value = saveReader.PeekChar(); //Power
                MessageBox.Show("File loaded.");
            }
            catch
            {
                MessageBox.Show("File load error.");
            }
            finally
            {
                saveReader.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
