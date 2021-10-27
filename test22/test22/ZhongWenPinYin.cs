using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test22
{
    public partial class ZhongWenPinYin : Form
    {
        Tool tool = new Tool();
        public ZhongWenPinYin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.textBox2.Text= tool.convertCh(this.textBox1.Text.ToString());
        }
    }
}
