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
            try
            {
                using (var writer = new BinaryWriter(File.OpenWrite("../../../AlpacasWithBonnets/AlpacasWithBonnets/bin/x86/Debug/" + textBox1.Text + ".alpaca")))
                {
                    writer.Write((int)numericUpDown1.Value);
                    writer.Write((int)numericUpDown2.Value);

                    //Saves color
                    Color color = panel1.BackColor;
                    writer.Write(color.R);
                    writer.Write(color.G);
                    writer.Write(color.B);
                    writer.Write(color.A);
                }
                MessageBox.Show("File saved.");
            }
            catch
            {
                MessageBox.Show("File save error.");
            }
        }

        //Load
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var reader = new BinaryReader(File.OpenRead("../../../AlpacasWithBonnets/AlpacasWithBonnets/bin/x86/Debug/" + textBox1.Text + ".alpaca")))
                {
                    numericUpDown1.Value = reader.ReadInt32(); //Health
                    numericUpDown2.Value = reader.ReadInt32(); //Power

                    //Loads color
                    byte r, g, b, a;
                    r = reader.ReadByte();
                    g = reader.ReadByte();
                    b = reader.ReadByte();
                    a = reader.ReadByte();
                    panel1.BackColor = Color.FromArgb(a, r, g, b);
                }
                
                MessageBox.Show("File loaded.");
            }
            catch
            {
                MessageBox.Show("File load error.");
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            ColorDialog colors = new ColorDialog();
            if (colors.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                panel1.BackColor = colors.Color;
            }
        }
    }
}
