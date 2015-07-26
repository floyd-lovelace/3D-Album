using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += new System.EventHandler(this.btnOpen_Click);
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;  //让窗体不遮住任务栏
            this.button6.Click += new EventHandler(this.button6_Click);
            this.button5.Click += new EventHandler(this.button5_Click);
            //this.button1.MouseEnter += new EventHandler(this.button1_MouseEnter);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {


            Form2 form2 = new Form2();
            form2.Show();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        //private void button1_MouseEnter(object sender, EventArgs e)
        //{
        //    this.button1.BackgroundImage = Image.FromFile("form4按钮.png");
        //}

    }
}
