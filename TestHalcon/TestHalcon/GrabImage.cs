using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace TestHalcon
{
    class GrabImage
    {
        HTuple hv_AcqHandle; //打开相机窗口的引用句柄
        HTuple imageWidth, imageHeight;//定义宽高

        private static HObject ho_Image;
        private static HObject ho_ROI_0;
        private static HObject ho_ImageReduced;
        private static HObject ho_ImagePart;
        private static HObject ho_SymbolXLDs;

        private static HTuple hv_DataCodeHandle;
        private static HTuple hv_ResultHandles;
        private static HTuple hv_DecodedDataStrings;

        public void open()
        {
            try
            {

                HOperatorSet.GenEmptyObj(out ho_Image);// 初始化本地图像空间的变量
                //打开本地相机
                HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8, "rgb",
             -1, "false", "default", "[0] ", 0, -1, out hv_AcqHandle);
                //开始采集图像
                HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("打开相机失败！");//报错
            }

        }
        //构造单次采集图像方法
        public void grabimage(HWindowControl HW)
        {
            //清空窗体halconwindows
            HW.HalconWindow.ClearWindow();
            //采集图像
            HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            //转成黑白图像
            HOperatorSet.Rgb1ToGray(ho_Image, out ho_Image);
            //图片自适应窗口
            HOperatorSet.GetImageSize(ho_Image, out imageWidth, out imageHeight);
            HOperatorSet.SetPart(HW.HalconWindow, 0, 0, imageHeight - 1, imageWidth - 1);
            //显示图像
            HOperatorSet.DispObj(ho_Image, HW.HalconWindow);
            //以当前日期保存图像到D盘下
            //HOperatorSet.WriteImage(ho_Image, "bmp", 0, "D:\\图片保存\\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
            //baocun_Image = ho_Image;
            ho_Image.Dispose();

        }

        public void baocunimg(HWindowControl HW)
        {
            //清空窗体halconwindows
            HW.HalconWindow.ClearWindow();
            //采集图像
            HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            //转成黑白图像
            HOperatorSet.Rgb1ToGray(ho_Image, out ho_Image);
            //图片自适应窗口
            HOperatorSet.GetImageSize(ho_Image, out imageWidth, out imageHeight);
            HOperatorSet.SetPart(HW.HalconWindow, 0, 0, imageHeight - 1, imageWidth - 1);
            //显示图像
            HOperatorSet.DispObj(ho_Image, HW.HalconWindow);
            //以当前日期保存图像到D盘下
            HOperatorSet.WriteImage(ho_Image, "bmp", 0, "D:\\图片保存\\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
            //baocun_Image = ho_Image;
            ho_Image.Dispose();
        }

        //关闭相机，释放内存
        public void close()
        {
            ho_Image.Dispose();
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);


        }
    }
}
