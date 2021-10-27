using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test22
{
    public partial class WatchMemory : Form
    {
        public WatchMemory()
        {
            InitializeComponent();
        }

        private void WatchMemory_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Computer myComputer = new Computer();
            //获取系统的物理内存总量
            textBox1.Text = Convert.ToString(myComputer.Info.TotalPhysicalMemory / 1024 / 1024);
            //获取系统的可用物理内存
            textBox2.Text = Convert.ToString(myComputer.Info.AvailablePhysicalMemory / 1024 / 1024);
        }

        private void WatchMemory_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }


    }
}
