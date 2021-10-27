using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace test2
{
    public partial class Form1 : Form
    {
        //定义halconq窗口的句柄
        HTuple WindowHandle = null;
        //定义宽高变量
        HTuple width = null, height = null;
        HObject ho_Image, ho_SymbolXLDs;
        HObject ho_Rectangle, ho_ImageReduced;
        HObject ho_RegionAffineTrans = null;

        // Local control variables 
        HTuple hv_Row1 = null, hv_Column1 = null, hv_Row2 = null;
        HTuple hv_Column2 = null, hv_Area = null, hv_Row = null;
        HTuple hv_Column = null, hv_Phi = null, hv_ModelID = null;
        HTuple hv_Row11 = new HTuple(), hv_Column11 = new HTuple();
        HTuple hv_Angle = new HTuple(), hv_Score = new HTuple();

       

        HTuple hv_HomMat2D = new HTuple();

        



        // halcon相机句柄
        HTuple hv_AcqHandle;

        


        //定义线程开关状态
        bool B = false;
        //定义一个线程
        Thread th;

        //主窗口加载
        private void Form1_Load_1(object sender, EventArgs e)
        {
            //允许跨线程访问
            Control.CheckForIllegalCrossThreadCalls = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
        }

        public Form1()
        {
            InitializeComponent();
        }
        //halcon导出文件的函数
        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
     HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_UseShadow = null;
            HTuple hv_ShadowColor = null, hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();
            
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //Transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //Display text box depending on text size
            hv_UseShadow = 1;
            hv_ShadowColor = "gray";
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
            {
                if (hv_Box_COPY_INP_TMP == null)
                    hv_Box_COPY_INP_TMP = new HTuple();
                hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                hv_ShadowColor = "#f28d26";
            }
            if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                {
                    //Use default ShadowColor set above
                }
                else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                    "false"))) != 0)
                {
                    hv_UseShadow = 0;
                }
                else
                {
                    hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            1));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                }
            }
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
            {
                //Valid color?
                try
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                    throw new HalconException(hv_Exception);
                }
                //Calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //Display rectangles
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                //Set shadow color
                HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                if ((int)(hv_UseShadow) != 0)
                {
                    HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
                }
                //Set box color
                HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //Reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //打开相机
            HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8, "rgb",-1, "false", "default", "[0] ", 0, -1, out hv_AcqHandle);
            //抓一帧图像
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
            //抓取图像
            HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            //获取图像的宽高
            HOperatorSet.GetImageSize(ho_Image, out width, out height);
            //将图像适应在窗口的大小
            //HOperatorSet.SetPart(WindowHandle, 0, 0, height, width);
            //HOperatorSet.SetDraw(WindowHandle, "margin");
            //显示图像在新建的halcon窗口
            HOperatorSet.DispObj(ho_Image, WindowHandle);
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void hWindowControl1_HMouseMove(object sender, HalconDotNet.HMouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            disp_message(WindowHandle, "绘制人脸，右键结束", "window", 12, 12, "black", "false");
            HOperatorSet.DrawRectangle1(WindowHandle, out hv_Row1, out hv_Column1, out hv_Row2, out hv_Column2);
            HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Row1, hv_Column1, hv_Row2, hv_Column2);
            HOperatorSet.AreaCenter(ho_Rectangle, out hv_Area, out hv_Row, out hv_Column);
            HOperatorSet.OrientationRegion(ho_Rectangle, out hv_Phi);
            HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
            HOperatorSet.CreateNccModel(ho_ImageReduced, "auto", -0.39, 0.79, "auto", "use_polarity", out hv_ModelID);
            button2.Enabled = false;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            B = true;
            th = new Thread(dectet_face);
            th.IsBackground = true;
            th.Start();
            button3.Enabled = false;
            button5.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (button5.Text == "暂停")
            {
                B = false;
                button5.Text = "开始";
            }
            else
            {
                B = true;
                button5.Text = "暂停";
                th = new Thread(dectet_face);
                th.IsBackground = true;
                th.Start();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (B == true)
            {
                th.Abort(); //关闭相机
            }
            //关闭相机
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            //释放图像变量
            if (ho_Image != null)
            {
                ho_Image.Dispose();
            }
            //释放轮廓变量
            if (ho_SymbolXLDs != null)
            {
                ho_SymbolXLDs.Dispose();
            }
            this.Close();
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        //检测的线程函数
        public void dectet_face()
        {
            while (B)
            {
                //抓取图像
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                HOperatorSet.ClearWindow(WindowHandle);
                HOperatorSet.DispObj(ho_Image, WindowHandle);
                //匹配人脸
                HOperatorSet.FindNccModel(ho_Image, hv_ModelID, -0.39, 0.79, 0.4, 1, 0.6, "true", 0, out hv_Row11, out hv_Column11, out hv_Angle, out hv_Score);
                if ((int)(new HTuple((new HTuple(hv_Row11.TupleLength())).TupleGreater(0))) != 0)
                {
                    //仿射变换

                    HOperatorSet.VectorAngleToRigid(hv_Row, hv_Column, hv_Phi, hv_Row11, hv_Column11, hv_Angle, out hv_HomMat2D);
                    HOperatorSet.AffineTransRegion(ho_Rectangle, out ho_RegionAffineTrans, hv_HomMat2D, "nearest_neighbor");
                    HOperatorSet.DispObj(ho_RegionAffineTrans, WindowHandle);
                }
            }
        }
    }
}
