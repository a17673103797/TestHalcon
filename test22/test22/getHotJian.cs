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
    public partial class getHotJian : Form
    {
        public getHotJian()
        {
            InitializeComponent();
        }


        private void getHotJian_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)//判断是否按下了F1功能键
            {
                MessageBox.Show("您按下了F1键！", "提示", MessageBoxButtons.OK);//消息提示框。用于提示信息；
            }
            else if (e.KeyCode == Keys.F2)
            {
                MessageBox.Show("您按下了F2键！", "提示", MessageBoxButtons.OK);
            }
            else if (e.KeyCode == Keys.F3)
            {
                MessageBox.Show("您按下了F3键！", "提示", MessageBoxButtons.OK);
            }
            else if (e.KeyCode == Keys.F4)
            {
                MessageBox.Show("您按下了F4键！", "提示", MessageBoxButtons.OK);
            }
            else if (e.KeyCode == Keys.F5)
            {
                MessageBox.Show("您按下了F5键！", "提示", MessageBoxButtons.OK);
            }
            else if (e.KeyCode == Keys.F6)
            {
                MessageBox.Show("您按下了F6键！", "提示", MessageBoxButtons.OK);
            }
            else if (e.KeyCode == Keys.F7)
            {
                MessageBox.Show("您按下了F7键！", "提示", MessageBoxButtons.OK);
            }
            else if (e.KeyCode == Keys.F8)
            {
                MessageBox.Show("您按下了F8键！", "提示", MessageBoxButtons.OK);
            }
            else if (e.KeyCode == Keys.F9)
            {
                MessageBox.Show("您按下了F9键！", "提示", MessageBoxButtons.OK);
            }
            else if (e.KeyCode == Keys.F10)
            {
                MessageBox.Show("您按下了F10键！", "提示", MessageBoxButtons.OK);
            }
            else if (e.KeyCode == Keys.F11)
            {
                MessageBox.Show("您按下了F11键！", "提示", MessageBoxButtons.OK);
            }
            else if (e.KeyCode == Keys.F12)
            {
                MessageBox.Show("您按下了F12键！", "提示", MessageBoxButtons.OK);
            }
        }
    }
}
