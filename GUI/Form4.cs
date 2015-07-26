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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button3.Click += new EventHandler(this.button3_Click);
            this.button4.Click += new EventHandler(this.button4_Click);
            this.button5.Click += new EventHandler(this.button5_Click);
            this.button6.Click += new EventHandler(this.button6_Click);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button5_Click(object sender, EventArgs e)//关闭
        {
            this.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)//导入要合成的单张图片
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        public void button2_Click(object sender, EventArgs e)//单张合成！！！！！！！！！！！！！！！
        {
            IplImage src=null;
            IplImage dstl=null;
            IplImage dstr = null;
            

                src = Cv.LoadImage(openFileDialog1.FileName);

                int x = src.Width * 89;
                int y = src.Height * 89;
                //Cv.SetImageROI(src, new CvRect(src.Width / 60, src.Height / 60, 4089, 3132));   //裁剪第一张
                //dstl = Cv.CreateImage(new CvSize(4089, 3132), src.Depth, src.NChannels);
                Cv.SetImageROI(src, new CvRect(src.Width / 90, src.Height / 90, x/90, y/90));   //裁剪第一张
                dstl = Cv.CreateImage(new CvSize(x / 90, y / 90), src.Depth, src.NChannels);
                Cv.Copy(src, dstl, null);
                Cv.ResetImageROI(src);

                //dstr = Cv.CreateImage(new CvSize(4089, 3132), src.Depth, src.NChannels);        //裁剪第二张
                //Cv.SetImageROI(src, new CvRect(0, 0, 4089, 3132));
                dstr = Cv.CreateImage(new CvSize(x / 90, y / 90), src.Depth, src.NChannels);        //裁剪第二张
                Cv.SetImageROI(src, new CvRect(0, 0, x / 90, y / 90));
                Cv.Copy(src, dstr, null);
                Cv.ResetImageROI(src);  

            //再次调用分离合成…………………………

                //IplImage pImg;   模拟左眼……
                IplImage dstl1, dstl2, dstl3;
                IplImage rdstl, bdstl, gdstl;
                IplImage pTem;
                CvScalar value = 0;

                pTem = Cv.CreateImage(new CvSize(dstl.Width, dstl.Height), dstl.Depth, 1);
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

                rdstl = Cv.CreateImage(Cv.Size(dstl.Width, dstl.Height), dstl.Depth, 1);
                bdstl = Cv.CreateImage(Cv.Size(dstl.Width, dstl.Height), dstl.Depth, 1);
                gdstl = Cv.CreateImage(Cv.Size(dstl.Width, dstl.Height), dstl.Depth, 1);

                dstl1 = Cv.CreateImage(Cv.Size(dstl.Width, dstl.Height), dstl.Depth, 3);//
                dstl2 = Cv.CreateImage(Cv.Size(dstl.Width, dstl.Height), dstl.Depth, 3);//
                dstl3 = Cv.CreateImage(Cv.Size(dstl.Width, dstl.Height), dstl.Depth, 3);//

                Cv.Merge(bdstl, pTem, pTem, null, dstl1);
                Cv.Split(dstl, bdstl, gdstl, rdstl, null);

                Cv.Merge(pTem, gdstl, pTem, null, dstl2);
                Cv.Merge(pTem, pTem, rdstl, null, dstl3);

                Cv.Merge(bdstl, gdstl, null, null, dstl1);//dstl1显示蓝绿色通道

            //模拟右眼………………………………………………………………

                IplImage dstr1, dstr2, dstr3;
                IplImage rdstr, bdstr, gdstr;
                IplImage pTemr;
                CvScalar valuer = 0;

                pTemr = Cv.CreateImage(new CvSize(dstr.Width, dstr.Height), dstr.Depth, 1);
                for (int i = 0; i < pTemr.NChannels; i++)
                {
                    value.Val0 = 0x1;
                }

                for (int i = 0; i < pTemr.Height; i++)
                {
                    for (int j = 0; j < pTemr.Width; j++)
                    {
                        Cv.Set2D(pTemr, i, j, valuer);
                    }
                }

                rdstr = Cv.CreateImage(Cv.Size(dstr.Width, dstr.Height), dstr.Depth, 1);
                bdstr = Cv.CreateImage(Cv.Size(dstr.Width, dstr.Height), dstr.Depth, 1);
                gdstr = Cv.CreateImage(Cv.Size(dstr.Width, dstr.Height), dstr.Depth, 1);

                dstr1 = Cv.CreateImage(Cv.Size(dstr.Width, dstr.Height), dstr.Depth, 3);//
                dstr2 = Cv.CreateImage(Cv.Size(dstr.Width, dstr.Height), dstr.Depth, 3);//
                dstr3 = Cv.CreateImage(Cv.Size(dstr.Width, dstr.Height), dstr.Depth, 3);//

                Cv.Merge(bdstr, pTemr, pTemr, null, dstr1);
                Cv.Split(dstr, bdstr, gdstr, rdstr, null);

                Cv.Merge(pTemr, gdstr, pTemr, null, dstr2);
                Cv.Merge(pTemr, pTemr, rdstr, null, dstr3);
                Cv.Merge(null, null, rdstr, null, dstr3);//pimg1显示红色通道

                Cv.AddWeighted(dstl1, 1, dstr3, 1, 1, dstl1);//合成…………

                Cv.SaveImage("NewFolder2.jpg", dstl1);
                pictureBox2.Load("NewFolder2.jpg");

                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                Cv.ReleaseImage(dstl1);

        }

        private void button6_Click(object sender, EventArgs e)      //保存立体图
        {
            saveFileDialog1.Filter = "*.jpg|*.jpg|*.bmp|*.bmp|*.jpeg|*.jpeg|*.gif|*.gif|*.png|*.png|*.ico|*.ico|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;          //默认保存格式，第一个
            saveFileDialog1.Title = "保存图像";
            saveFileDialog1.InitialDirectory = "F:\\";      //默认保存位置
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
            
            //拷贝复制图片，不然不能保存
            Image image = new Bitmap("NewFolder2.jpg");
            Image newImage = new Bitmap(image.Width,image.Height);
            Graphics draw = Graphics.FromImage(newImage);
            draw.DrawImage(image, 0, 0);
            image.Dispose();

            if (saveFileDialog1.FileName != "")
            {
                {
                    newImage.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);//这句有问题……
                }
            }
        }


    }
}
