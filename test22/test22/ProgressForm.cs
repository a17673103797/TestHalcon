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
    public partial class ProgressForm : Form
    {

        Random r;
        public ProgressForm()
        {
            InitializeComponent();
            this.backgroundWorker1.WorkerReportsProgress = true;  //设置能报告进度更新
            this.backgroundWorker1.WorkerSupportsCancellation = true;  //设置支持异步取消
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.WorkerReportsProgress = true;
            r = new Random();
            BackgroundWorker worker = sender as BackgroundWorker;  
            for (int i = 0; i <= 100; i++)
            {
                int num = r.Next(5, 80);
                System.Threading.Thread.Sleep(num);
                //worker.ReportProgress(i);
                if (worker.CancellationPending) //获取程序是否已请求取消后台操作
                {
                    e.Cancel = true;
                    break;
                }
                //调用 ReportProgress 方法，会触发ProgressChanged事件
                backgroundWorker1.ReportProgress(i, String.Format("{0}%", i));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
            this.button1.Enabled = false;
            this.button2.Enabled = true;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("取消");
            }
            else
            {
                MessageBox.Show("完成");
                this.button2.Enabled = false;
                this.button1.Enabled = true;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;  //获取异步任务的进度百分比
            this.label1.Text = e.UserState.ToString();
            this.label1.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync(); //请求取消挂起的后台操作
            this.button2.Enabled = false;
            this.button1.Enabled = true;
            this.button1.Text = "重新下载";
        }
    }
}
