using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using Common.Utility;
using DotNet.Utilities.CSVHelper;
using System.IO;

namespace test22
{
    public partial class ConnectToMySql : Form
    {
        //定义一个全局的MySqlConnection对象
        public static MySqlConnection mysql;
        //定义数据库连接信息的存储属性server：连接的IP地址，port：端口号，user：账号，passwd：密码，database：数据库名称
        String server, port, user, passwd, database;






        public ConnectToMySql()
        {
            InitializeComponent();
        }

        //数据库连接
        //只需要登陆一次，将返回的对象传入下面的函数即可
        public MySqlConnection connetion(string server, string port, string user, string passwd, string database)
        {   /*
            String conStr = string.Format("server={0};port={1};user id={2};password={3};database={4}", server, port,user, passwd, database);
            MySqlConnection DBcon = new MySqlConnection(conStr);
            //DBcon.ConnectionString = conStr;
            DBcon.Open();
             * string M_str_sqlcon = "server=cdb-p64rh8th.gz.tencentcdb.com;port=10065;user id=root;password=Aa8812508;database=MyTest";
             * string server, string port,string user, string passwd, string database
            **/

            //构建数据库连接字符串
            string M_str_sqlcon = string.Format("server={0};port={1};user id={2};password={3};database={4}", server, port, user, passwd, database);
            //"server=" + server + ";port=" + port + ";user id=" + user + ";password=" + passwd + ";database=" + database; //根据自己的设置
            //创建数据库连接对象
            MySqlConnection mycon = new MySqlConnection();
            //将连接语句传给ConnectionString；
            mycon.ConnectionString = M_str_sqlcon;
            return mycon;
        }

        //数据库操作
        //不需要额外调用
        public MySqlCommand command(string sql, MySqlConnection DBconn)
        {
            MySqlCommand rt = new MySqlCommand(sql, DBconn);
            return rt;
        }

        //数据库增删改
        public int Exute(string sql, MySqlConnection DBconn)
        {
            //返回受影响的行数
            return this.command(sql, DBconn).ExecuteNonQuery();
        }

        //通过select语句进行查询
        public MySqlDataReader read(string sql, MySqlConnection DBconn)
        {
            //数据库查询
            return this.command(sql, DBconn).ExecuteReader();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox7.Text != "" && textBox8.Text != "")
            {
                server = textBox1.Text;
                port = textBox8.Text;
                user = textBox2.Text;
                passwd = textBox3.Text;
                database = textBox7.Text;
                try
                {
                    mysql = connetion(server, port, user, passwd, database);
                    if (mysql != null)
                    {
                        //打开数据库连接
                        mysql.Open();
                        label9.Text = "数据库已连接！";
                        label9.ForeColor = Color.Green;
                        richTextBox1.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "请输入正确的数据库信息！", MessageBoxButtons.OK);
                    Debug.Write(ex);
                    richTextBox1.Text = ex.ToString();
                    //消息提示框。用于提示信息；
                    //MessageBox.Show("请输入正确的数据库信息！", "提示", MessageBoxButtons.OK);
                }
            }
            else
            {
                //消息提示框。用于提示信息；
                MessageBox.Show("请输入数据库信息！", "提示", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //进来之前把表格显示清空
            this.dataGridView1.DataSource = null;
            //判断comboBox1是否为空，如果为空则说明查询的对象不对
            if (comboBox1.Text != "")
            {
                try
                {
                    //定义sql语句，查询表信息，表名称通过comboBox1选中的参数
                    string sql = "select * from " + comboBox1.Text;
                    //查询sql语句
                    MySqlDataAdapter mda = new MySqlDataAdapter(sql, mysql);
                    DataSet ds = new DataSet();
                    mda.Fill(ds, "table1");
                    //显示数据
                    this.dataGridView1.DataSource = ds.Tables["table1"];
                    // Test(dataGridView1);
                }
                catch (Exception ex)
                {
                    //将接收到异常信息以红色的字体输出到richTextBox1进行展示
                    Debug.Write(ex);
                    richTextBox1.AppendText(ex.ToString());
                    richTextBox1.ForeColor = Color.Red;
                    //消息提示框。用于提示信息；
                    MessageBox.Show("请输入正确的表名称信息！", "提示", MessageBoxButtons.OK);
                }

            }
            else
            {
                //消息提示框。用于提示信息；
                MessageBox.Show("请输入正确的表名称信息！", "提示", MessageBoxButtons.OK);
            }

        }

        private void ConnectToMySql_Load(object sender, EventArgs e)
        {
            //将数据库连接信息默认初始化，可以修改，这里为测试数据
            textBox1.Text = "cdb-p64rh8th.gz.tencentcdb.com";
            textBox2.Text = "root";
            textBox3.Text = "Aa8812508";
            textBox7.Text = "MyTest";
            textBox8.Text = "10065";

        }

        public static List<string> GetAllTableName()
        {
            //定义一个集合用于存储查询到的信息
            List<string> retNameList = new List<string>();
            // 这句是关键  Connectioin为连接 使用的是Mysql.Data.dll
            DataTable tbName = mysql.GetSchema("Tables");
            if (tbName.Columns.Contains("TABLE_NAME"))
            {
                foreach (DataRow dr in tbName.Rows)
                {
                    retNameList.Add((string)dr["TABLE_NAME"]);
                }
            }
            //返回查询到的集合
            return retNameList;
        }

        #region 将查到的数据库表名称添加到下拉列表
        /// <summary>
        /// 添加表名称到下拉栏
        /// </summary>

        List<string> getDataTableName;//定义一个集合接收数据库表名称数据；

        private void button4_Click(object sender, EventArgs e)
        {
            //接收数据；
            getDataTableName = GetAllTableName();
            //判断对象是否为空
            if (getDataTableName != null)
            {
                //遍历
                for (int i = 0; i < getDataTableName.Count; i++)
                {
                    //将表单名称添加到item
                    comboBox1.Items.Add(getDataTableName[i].ToString());
                }
            }
            //设置控件显示的item索引，通过SelectedIndex方法可以设置，“0”代表索引；
            comboBox1.SelectedIndex = 0;

        }
        #endregion

        /// <summary>
        /// 将文件进行保存，默认是csv文件；
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.csv|csv file"; // 设置文件类型为文本文件
            saveFileDialog1.DefaultExt = ".csv";// 默认文件的拓展名
            saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMddHHmm") + ".csv";//设置文件的默认名字为年-月-日-时-分.csv

            DialogResult dr = saveFileDialog1.ShowDialog();//打开保存文件对话框
            string filename = saveFileDialog1.FileName;//将设置保存的名字赋值给filename用于将数据保存到csv文件中

            if (dr == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(filename))
            {
                StreamWriter sw = new StreamWriter(filename, true, Encoding.UTF8);
                sw.Close();
                DataTable tbName = mysql.GetSchema("Tables");//获取所有table的所有数据然后赋值给DataTable
                DotNet.Utilities.CSVHelper.CSVHelper.DataTableToCSV(tbName, filename);//将DataTable中的数据写入到文件中

                if (Common.Utility.DirFile.IsExistFile(filename))//查询文件是否创建完成
                {
                    MessageBox.Show("保存成功！");//显示信息
                }
                else
                {
                    MessageBox.Show("保存失败！");//显示信息
                }
            }

        }
    }

}

