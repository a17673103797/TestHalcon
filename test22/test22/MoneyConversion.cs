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
    public partial class MoneyConversion : Form
    {
        public MoneyConversion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal num=decimal.Parse(this.textBox1.Text.ToString());
            this.label1.Text = "转换前金额：" + this.textBox1.Text+"元";
            this.label2.Text = "转换后金额：" + Common.Utilities.DecimalUtility.ConvertNumToZHUpperCase(num);
        }


    }
}
