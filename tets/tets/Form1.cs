using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ToolS;

namespace tets
{
    public partial class Form1 : Form
    {
        int i,b;
        String a;
        bool t = true;
        String data;
        ToolS.RandomNmbTool rd = new ToolS.RandomNmbTool();
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = null;
            comboBox1.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = null;
            if (comboBox1.Text!=""&&textBox1.Text.Length!=0)
            {
                switch (comboBox1.Text)
                {
                    case "汉字":
                        if (checkBox1.Checked)
                        {
                            MessageBox.Show("不好意思！暂时没有这个功能！");
                            i = int.Parse(textBox1.Text);
                            a = GetRandomNumber(i, t);
                            richTextBox1.Text = a;
                            i = 0;
                            b = 0;
                            a = null;
                        }
                        i = int.Parse(textBox1.Text);
                        
                        a = GetRandomChinese2(i);
                        richTextBox1.Text = a;
                        i = 0;
                        b = 0;
                        a = null;
                        break;
                    case "字母":
                        if (checkBox1.Checked)
                        {
                            MessageBox.Show("不好意思！暂时没有这个功能！");
                            i = int.Parse(textBox1.Text);
                            a = GetRandomNumber(i, t);
                            richTextBox1.Text = a;
                            i = 0;
                            b = 0;
                            a = null;
                        }
                        i = int.Parse(textBox1.Text);
                        a = GetRandomPureChar(i,t);
                        richTextBox1.Text = a;
                        i = 0;
                        b = 0;
                        a = null;
                        break;
                    case "数字":
                        if (checkBox1.Checked)
                        {
                            MessageBox.Show("不好意思！暂时没有这个功能！");
                            i = int.Parse(textBox1.Text);
                            a = GetRandomNumber(i, t);
                            richTextBox1.Text = a;
                            i = 0;
                            b = 0;
                            a = null;
                        }
                        i = int.Parse(textBox1.Text);
                        a = GetRandomNumber(i,t);
                        richTextBox1.Text = a;
                        i = 0;
                        b = 0;
                        a = null;
                        break;
                }
                
                
            }
            else
            {
                MessageBox.Show("请选择要生成的类型和要生成的长度", "重新填入", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
             
                return;
            }
            //i = int.Parse(textBox1.Text);
            //b = GetWeekAmount(i);
            //label1.Text = i + "年一共有" + b + "周！";

            
        }

        //定义一个委托
        private delegate void DelegateFunction(int ipos);

        //设置进度条的Value
        private void SetPos(int ipos)
        {
            if (this.progressBar1.InvokeRequired)
            {
                DelegateFunction df = new DelegateFunction(StartMultiWork);
                this.Invoke(df, new object[] { ipos });
            }
            else
            {
                progressBar1.Value = Int32.Parse(ipos);
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = null;
            comboBox1.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data= GetProductOrderNum().ToString();
            textBox2.Text = data;
            data = null;
        }


        /// <summary>
        /// 获取某一年有多少周
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>该年周数</returns>
        public static int GetWeekAmount(int year)
        {
            var end = new DateTime(year, 12, 31); //该年最后一天
            var gc = new GregorianCalendar();
            return gc.GetWeekOfYear(end, CalendarWeekRule.FirstDay, DayOfWeek.Monday); //该年星期数
        }

        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string GetRandomPureChar(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }

        /// <summary>
        /// 此函数为生成指定数目的汉字
        /// </summary>
        /// <param name="charLen">汉字数目</param>
        /// <returns>所有汉字</returns>
        public static string GetRandomChinese2(int strlength)
        {
            int area, code;//汉字由区位和码位组成(都为0-94,其中区位16-55为一级汉字区,56-87为二级汉字区,1-9为特殊字符区)
            string[] charArrary = new string[strlength];
            Random rand = new Random();
            for (int i = 0; i < strlength; i++)
            {
                area = rand.Next(16, 88);
                if (area == 55)//第55区只有89个字符
                {
                    code = rand.Next(1, 90);
                }
                else
                {
                    code = rand.Next(1, 94);
                }
                charArrary[i] = Encoding.GetEncoding("GB2312").GetString(new byte[] { Convert.ToByte(area + 160), Convert.ToByte(code + 160) });
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charArrary.Length; i++)
            {
                sb.Append(charArrary[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成编号，全站统一格式(年月日时分秒+4位随机数)
        /// </summary>
        /// <returns></returns>
        public static string GetProductOrderNum()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + GetRandomNumberString(4, true, Guid.NewGuid().ToString());
        }

        #region 生成随机数字字符串
        /// <summary>
        /// 生成随机数字字符串
        /// </summary>
        /// <param name="int_NumberLength">数字长度</param>
        /// <returns></returns>
        public static string GetRandomNumberString(int int_NumberLength, bool onlyNumber, string _randTag)
        {
            string randTag = string.IsNullOrEmpty(_randTag) ? Guid.NewGuid().ToString() : _randTag;
            Random random = new Random();
            return GetRandomNumberString(int_NumberLength, onlyNumber, random, randTag);
        }

        /// <summary>
        /// 生成随机数字字符串
        /// </summary>
        /// <param name="int_NumberLength">数字长度</param>
        /// <returns></returns>
        public static string GetRandomNumberString(int int_NumberLength, bool onlyNumber, Random random, string randTag)
        {
            string strings = "123456789";
            if (!onlyNumber) strings += "abcdefghjkmnpqrstuvwxyz";
            char[] chars = strings.ToCharArray();
            string returnCode = string.Empty;
            for (int i = 0; i < int_NumberLength; i++)
                returnCode += chars[random.Next(0, chars.Length)].ToString();
            return returnCode;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Class1 mytest = new Class1();
            mytest.runthread(this, getstr, add);
        }


        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string GetRandomNumber(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
    }
}
#endregion