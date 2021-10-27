using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace test22
{
    public partial class ShowChart : Form
    {

        private Queue<double> dataQueue = new Queue<double>(200);

        private int curValue = 0;

        private int num = 5;//每次删除增加几个点

        public ShowChart()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            InitChart();
        }

        /// <summary>
        /// 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //this.timer1.Start();
            timer1.Enabled = true;
            //MessageBox.Show("123456");
        }

        /// <summary>
        /// 停止事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // this.timer1.Stop();
            timer1.Enabled = false;
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //ThreadStart th = new ThreadStart(UpdateQueueValue);
            //Thread childThread = new Thread(th);
            //childThread.Start();

            //Thread thread1 = new Thread(new ThreadStart(UpdateQueueValue));
            //thread1.Start();
            UpdateQueueValue();

            this.chart1.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue.Count; i++)
            {
                this.chart1.Series[0].Points.AddXY((i + 1), dataQueue.ElementAt(i));
            }
        }

        /// <summary>
        /// 初始化图表
        /// </summary>
        private void InitChart()
        {
            //定义图表区域
            this.chart1.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("C1");
            this.chart1.ChartAreas.Add(chartArea1);
            //定义存储和显示点的容器
            this.chart1.Series.Clear();
            Series series1 = new Series("S1");
            series1.ChartArea = "C1";
            this.chart1.Series.Add(series1);
            //设置图表显示样式
            this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            this.chart1.ChartAreas[0].AxisY.Maximum = 200;
            this.chart1.ChartAreas[0].AxisX.Interval = 5;
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //设置标题
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("S01");
            this.chart1.Titles[0].Text = "XXX显示";
            this.chart1.Titles[0].ForeColor = Color.RoyalBlue;
            this.chart1.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            //设置图表显示样式
            this.chart1.Series[0].Color = Color.Red;
            if (radioButton1.Checked)
            {
                this.chart1.Titles[0].Text = string.Format("XXX {0} 显示", radioButton1.Text);
                this.chart1.Series[0].ChartType = SeriesChartType.Line;
            }
            if (radioButton2.Checked)
            {
                this.chart1.Titles[0].Text = string.Format("XXX {0} 显示", radioButton2.Text);
                this.chart1.Series[0].ChartType = SeriesChartType.Spline;
            }
            this.chart1.Series[0].Points.Clear();
        }

        //更新队列中的值
        private void UpdateQueueValue()
        {

            if (dataQueue.Count > 100)
            {
                //先出列
                for (int i = 0; i < num; i++)
                {
                    dataQueue.Dequeue();
                }
            }
            if (radioButton1.Checked)
            {
                Random r = new Random();
                for (int i = 0; i < num; i++)
                {
                    //int num1 = r.Next(0,100);
                    dataQueue.Enqueue(r.Next(0, 200));
                    //System.Threading.Thread.Sleep(num1);
                }
            }
            if (radioButton2.Checked)
            {
                for (int i = 0; i < num; i++)
                {
                    //对curValue只取[0,360]之间的值
                    curValue = curValue % 360;
                    //对得到的正玄值，放大50倍，并上移50
                    dataQueue.Enqueue((50 * Math.Sin(curValue * Math.PI / 180)) + 50);
                    curValue = curValue + 10;
                }
            }
        }



    }
}
