using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMexample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "You are in the process of withdrawal";
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = true;
            listBox1.Items.Add("123456789");
            listBox1.Items.Add("111111111");
            listBox1.Items.Add("555555555");
            listBox1.Items.Add("999999999");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel2.Visible = false;
            label1.Text = "Welcome to ZZZ Bank ATM Machine";
        }
    }
}
