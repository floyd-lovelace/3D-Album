using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;
using System.IO;
using System.Threading;

namespace GUI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;  //让窗体不遮住任务栏
            this.button1.Click += new System.EventHandler(this.btnOpen_Click);
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button3.Click += new EventHandler(this.button3_Click);
            this.button4.Click += new EventHandler(this.button4_Click);
            this.button5.Click += new EventHandler(this.button5_Click);
            this.button6.Click += new EventHandler(this.button6_Click);
            this.button7.Click += new EventHandler(this.button7_Click);

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
            //System.Environment.Exit(0);
        }

        private void btnOpen_Click(object sender, EventArgs e)//导入第一张图片
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void button2_Click(object sender, EventArgs e)//导入第二张图片
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Load(openFileDialog2.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            progressBar1.Show();
            Form3 Form3 = new Form3();
            //Form3.Show();
        }

        public void button3_Click(object sender, EventArgs e)//制作立体图
        {
            //分离左眼图片
            IplImage pImg;
            IplImage pImg1, pImg2, pImg3;
            IplImage rImg, bImg, gImg;
            IplImage pTem;
            CvScalar value = 0;

            pImg = Cv.LoadImage(openFileDialog1.FileName);//导入要操作的图片
            pTem = Cv.CreateImage(new CvSize(pImg.Width, pImg.Height), pImg.Depth, 1);

            for (int i = 0; i < pTem.NChannels; i++)
            {
                value.Val0 = 0x1;
            }

            for (int i = 0; i < pTem.Height; i++)
            {
                for (int j = 0; j < pTem.Width; j++)
                {
                    Cv.Set2D(pTem, i, j, value);
                }
            }

            rImg = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 1);
            bImg = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 1);
            gImg = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 1);

            pImg1 = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 3);//
            pImg2 = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 3);//
            pImg3 = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 3);//

            Cv.Merge(bImg, pTem, pTem, null, pImg1);
            Cv.Split(pImg, bImg, gImg, rImg, null);

            Cv.Merge(pTem, gImg, pTem, null, pImg2);
            Cv.Merge(pTem, pTem, rImg, null, pImg3);

            Cv.Merge(bImg, gImg, null, null, pImg1);//pimg1显示蓝色通道

            //Cv.Split(pImg, bImg, gImg, rImg, null);


            //分离右眼图片
            IplImage pImgr;
            IplImage pImg1r, pImg2r, pImg3r;
            IplImage rImgr, bImgr, gImgr;
            IplImage pTemr;
            CvScalar valuer = 0;

            pImgr = Cv.LoadImage(openFileDialog2.FileName);//导入要操作的图片
            pTemr= Cv.CreateImage(new CvSize(pImg.Width, pImg.Height), pImg.Depth, 1);

            for (int i = 0; i < pTemr.NChannels; i++)
            {
                value.Val0 = 0x1;
            }

            for (int i = 0; i < pTemr.Height; i++)
            {
                for (int j = 0; j < pTemr.Width; j++)
                {
                    Cv.Set2D(pTemr, i, j, value);
                }
            }

            rImgr = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 1);
            bImgr = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 1);
            gImgr = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 1);

            pImg1r = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 3);//
            pImg2r = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 3);//
            pImg3r = Cv.CreateImage(Cv.Size(pImg.Width, pImg.Height), pImg.Depth, 3);//

            Cv.Merge(bImgr, pTemr, pTemr, null, pImg1r);
            Cv.Split(pImgr, bImgr, gImgr, rImgr, null);
            
            Cv.Merge(pTemr, gImgr, pTemr, null, pImg2r);
            Cv.Merge(pTemr, pTemr, rImgr, null, pImg3r);

            Cv.Merge(null, null, rImgr, null, pImg3r);//pimg1显示红色通道


            //Cv.Split(pImgr, bImgr, gImgr, rImgr, null);

            Cv.AddWeighted(pImg1, 1, pImg3r, 1, 1, pImg1);

            Cv.SaveImage("NewFolder1.jpg", pImg1);
            pictureBox3.ImageLocation = "NewFolder1.jpg";//目前感觉只有这样加载图片才能保存，直接load总是得不到值^^^^^*****相对路径终于解决了………………
            
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        }


        private void button4_Click(object sender, EventArgs e)      //保存立体图
        {
            saveFileDialog1.Filter = "*.jpg|*.jpg|*.bmp|*.bmp|*.jpeg|*.jpeg|*.gif|*.gif|*.png|*.png|*.ico|*.ico|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;          //默认保存格式，第一个
            saveFileDialog1.Title = "保存图像";
            saveFileDialog1.InitialDirectory = "F:\\";      //默认保存位置
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                {
                    pictureBox3.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
            }
        }

        private void pictureBox3_LoadProgressChanged(object sender, ProgressChangedEventArgs e)//进度窗体
        {
            Form3 Form3 = new Form3();
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage.ToString() + "%";
            Form3.Close();

            //    int maximum=100, value=0;
            //    this.progressBar1.Maximum = maximum;//设置进度条最大值
            //    this.progressBar1.Value = value;//设置进度条当前值

            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;
            for (int i = 0; i <= 100; i++)
            {
                progressBar1.Value = i;
            }
            progressBar1.Visible = false;
            //Form3.Close();//关闭窗体


        }


    }
}


