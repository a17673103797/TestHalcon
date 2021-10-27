using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace TestHalcon
{
    public partial class Form1 : Form
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public Form1()
        {
            InitializeComponent();
        }
        GrabImage grabimg = new GrabImage();//实例化GrabImage类

        private void button1_Click(object sender, EventArgs e)
        {
            grabimg.grabimage(hWindowControl1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                //timer1.Start();
                timer2.Start();
                checkBox1.Enabled = false;
                this.button2.Enabled = false;
                this.button4.Enabled = true;
                label2.Text = "已开启连续保存!";
            }
            else {
                timer1.Start();
                //timer2.Start();
                checkBox1.Enabled = false;
                this.button2.Enabled = false;
                this.button4.Enabled = true;
                label2.Text = "未开启连续保存!";
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = false;   //停止定时器
            //grabimg.baocunimg();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            grabimg.grabimage(hWindowControl1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            this.button2.Enabled = true;
            this.button4.Enabled = false;
            checkBox1.Enabled = true;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grabimg.open();//调用open方法
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            grabimg.close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            grabimg.baocunimg(hWindowControl1);
        }

        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
