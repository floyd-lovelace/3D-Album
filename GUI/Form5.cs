using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.button5.Click += new EventHandler(this.button5_Click);
            this.button6.Click += new EventHandler(this.button6_Click);
            this.button7.Click += new EventHandler(this.button7_Click);
            this.MouseWheel += new MouseEventHandler(this.panel2_MouseWheel);
        }

        private void button5_Click(object sender, EventArgs e)//最小化
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e)//最大化，还原
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button7_Click(object sender, EventArgs e)//关闭
        {
            this.Close();
        }

        
        private void panel2_MouseWheel(object sender, MouseEventArgs e)   //鼠标滚动事件……
        {
            Point mousePoint = new Point(e.X, e.Y);
            mousePoint.Offset(this.Location.X, this.Location.Y);
            if (panel2.RectangleToScreen(panel2.DisplayRectangle).Contains(mousePoint))
            {
                panel2.AutoScrollPosition = new Point(0, panel2.VerticalScroll.Value - e.Delta);//滚动
            }
        }


    }
}
