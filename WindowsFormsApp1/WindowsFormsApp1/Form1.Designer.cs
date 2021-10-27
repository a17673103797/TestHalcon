using HalconDotNet;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.hWindowControl3 = new HalconDotNet.HWindowControl();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hWindowControl3
            // 
            this.hWindowControl3.BackColor = System.Drawing.Color.Black;
            this.hWindowControl3.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl3.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl3.Location = new System.Drawing.Point(12, 12);
            this.hWindowControl3.Name = "hWindowControl3";
            this.hWindowControl3.Size = new System.Drawing.Size(495, 490);
            this.hWindowControl3.TabIndex = 0;
            this.hWindowControl3.WindowSize = new System.Drawing.Size(495, 490);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(597, 206);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 78);
            this.button3.TabIndex = 1;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(782, 525);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.hWindowControl3);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

       
        private HWindowControl hWindowControl3;
        private System.Windows.Forms.Button button3;
    }
}

