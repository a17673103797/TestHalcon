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
    public partial class Form1 : Form
    {
        getHotJian gethot;
        ConnectToMySql contomysql;
        ProgressForm pro;
        WatchMemory wat;
        ShowChart showchrat;
        MoneyConversion money;
        ZhongWenPinYin zhong;

        string name;
        string pwd;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button19_Click(object sender, EventArgs e)
        {
            gethot = new getHotJian();
            gethot.ShowDialog();
            gethot.Close();
            gethot.Dispose();
            GC.Collect();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            contomysql = new ConnectToMySql();
            contomysql.ShowDialog();
            contomysql.Close();
            contomysql.Dispose();
            GC.Collect();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            pro = new ProgressForm();
            pro.ShowDialog();
            pro.Close();
            pro.Dispose();
            GC.Collect();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            wat = new WatchMemory();
            wat.ShowDialog();
            wat.Close();
            wat.Dispose();
            GC.Collect();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            showchrat = new ShowChart();
            showchrat.ShowDialog();
            showchrat.Close();
            showchrat.Dispose();
            GC.Collect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            money = new MoneyConversion();
            money.ShowDialog();
            money.Close();
            money.Dispose();
            GC.Collect();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            zhong = new ZhongWenPinYin();
            zhong.ShowDialog();
            zhong.Close();
            zhong.Dispose();
            GC.Collect();
        }
        
    }
}
