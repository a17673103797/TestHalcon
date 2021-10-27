using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Caijixiangji
{
    public partial class Form1 : Form
    {
        HObject ho_Image, ho_Regions, ho_image;
        HTuple hv_window;
        HTuple hv_AcqHandle = null, hv_Width = new HTuple();
        HTuple hv_Height = new HTuple();

        string imagepath =null;

        private bool Acqthread_stop = false;

        private Thread thread_Acquire = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            thread_Acquire = new Thread(new ThreadStart(Acq_Function));
            anniuY();
        }
        private void Acq_Function()
        {
            
        }

        private void anniuY() {
            if (hv_window==null)
            {
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
            }
            else
            {
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JEPG文件|*.jpg*|BMP文件|*.bmp*|PNG文件|*.png*";
            openFileDialog1.RestoreDirectory = true;

            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Regions);

            ho_Image.Dispose();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imagepath = openFileDialog1.FileName;
                HOperatorSet.ReadImage(out ho_Image, imagepath);
            }
            

        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, hWindowControl1.Width, hWindowControl1.Height, hWindowControl1.HalconWindow, "", "", out hv_window);
            HDevWindowStack.Push(hv_window);

            //HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            HOperatorSet.SetPart(hv_window, 0, 0, hWindowControl1.Height, hWindowControl1.Width);
            HOperatorSet.DispObj(ho_Image, hv_window);

            ho_Regions.Dispose();
            HOperatorSet.Threshold(ho_Image, out ho_Regions, 81, 141);
            HOperatorSet.SetColor(hv_window, "red");
            HOperatorSet.DispObj(ho_Regions, hv_window);
            ho_Image.Dispose();
            ho_Regions.Dispose();
            ho_Image = null;
            //hv_window = null;
            anniuY();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ho_image.Dispose();
            HOperatorSet.GrabImageAsync(out ho_image, hv_AcqHandle, -1);
            HOperatorSet.GetImageSize(ho_image, out hv_Width, out hv_Height);
            HOperatorSet.SetPart(hv_window, 0, 0, hv_Height, hv_Width);

            HOperatorSet.DispObj(ho_image, hv_window);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void hWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (hv_window != null)
            {
                HOperatorSet.SetColor(hv_window, "red");
                hWindowControl1.Focus();
                HTuple r, c, radius;
                HObject circle;
                HOperatorSet.DrawCircle(hv_window, out r, out c, out radius);
                HOperatorSet.GenCircle(out circle, r, c, radius);
                HOperatorSet.DispObj(circle, hv_window);
            }
            return;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (hv_window != null)
            {
                HOperatorSet.SetColor(hv_window, "green");
                hWindowControl1.Focus();
                HTuple r, c, phi, len1, len2;
                HObject rectangle;
                hWindowControl1.Focus();
                HOperatorSet.DrawRectangle2(hv_window, out r, out c, out phi, out len1, out len2);
                HOperatorSet.GenRectangle2(out rectangle, r, c, phi, len1, len2);
                HOperatorSet.DispObj(rectangle, hv_window);
            }
            return;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (hv_window != null)
            {
                HOperatorSet.SetColor(hv_window, "yellow");
                hWindowControl1.Focus();
                HTuple r, c, phi, rad1, rad2;
                HObject ellipse;
                HOperatorSet.DrawEllipse(hv_window, out r, out c, out phi, out rad1, out rad2);
                HOperatorSet.GenEllipse(out ellipse, r, c, phi, rad1, rad2);
                HOperatorSet.DispObj(ellipse, hv_window);
            }
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.button1.Text == "打开相机")
            {
                this.button1.Text = "关闭相机";
                HOperatorSet.GenEmptyObj(out ho_image);
                //Image Acquisition 01: Code generated by Image Acquisition 01
                HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8, "rgb",
                -1, "false", "default", "[0] ", 0, -1, out hv_AcqHandle);
                HOperatorSet.GrabImageStart(hv_AcqHandle, -1);

                UpdateLogMessage("打开相机\r\n");
            }
            else if (this.button1.Text == "关闭相机")
            {
                this.button1.Text = "打开相机";
                HOperatorSet.CloseFramegrabber(hv_AcqHandle);
                ho_image.Dispose();
                UpdateLogMessage("关闭相机\r\n");
            }
        }

        private void UpdateLogMessage(string Content)
        {
            string TotalContent = DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.button3.Text == "开始采集" && this.button1.Text == "关闭相机")
            {
                timer1.Enabled = true;
                this.button3.Text = "停止采集";

                HOperatorSet.OpenWindow(0, 0, hWindowControl1.Width, hWindowControl1.Height, hWindowControl1.HalconWindow, "", "", out hv_window);
                HDevWindowStack.Push(hv_window);                 //使用定时器的情况下
                UpdateLogMessage("开始采集\r\n");
            }
            else if (this.button3.Text == "停止采集")
            {
                timer1.Enabled = false;
                this.button3.Text = "开始采集";
                HOperatorSet.CloseWindow(hv_window);
                UpdateLogMessage("停止采集\r\n");
            }
        }
    }
}
