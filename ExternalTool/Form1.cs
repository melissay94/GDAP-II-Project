﻿using System;
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
            }
            catch
            {
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
                health = saveReader.ReadInt32();
                power = saveReader.ReadInt32();
            }
            catch
            {
                Console.WriteLine("File read error.");
                Console.WriteLine("Press any key to close this window.");
                Console.ReadKey();
            }
            finally
            {
                saveReader.Close();
            }
        }
    }
}
