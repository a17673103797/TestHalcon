using HalconDotNet;

public partial class HDevelopExport
{
  public void disp_message (HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem, 
      HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
  {
    HTuple hv_GenParamName = new HTuple(), hv_GenParamValue = new HTuple();
    HTuple   hv_Color_COPY_INP_TMP = new HTuple(hv_Color);
    HTuple   hv_Column_COPY_INP_TMP = new HTuple(hv_Column);
    HTuple   hv_CoordSystem_COPY_INP_TMP = new HTuple(hv_CoordSystem);
    HTuple   hv_Row_COPY_INP_TMP = new HTuple(hv_Row);
    if ((int)((new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
        new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(new HTuple())))) != 0)
    {

      hv_Color_COPY_INP_TMP.Dispose();
      hv_Column_COPY_INP_TMP.Dispose();
      hv_CoordSystem_COPY_INP_TMP.Dispose();
      hv_Row_COPY_INP_TMP.Dispose();
      hv_GenParamName.Dispose();
      hv_GenParamValue.Dispose();

      return;
    }
    if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
    {
      hv_Row_COPY_INP_TMP.Dispose();
      hv_Row_COPY_INP_TMP = 12;
    }
    if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
    {
      hv_Column_COPY_INP_TMP.Dispose();
      hv_Column_COPY_INP_TMP = 12;
    }
    //
    //Convert the parameter Box to generic parameters.
    hv_GenParamName.Dispose();
    hv_GenParamName = new HTuple();
    hv_GenParamValue.Dispose();
    hv_GenParamValue = new HTuple();
    if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(0))) != 0)
    {
      if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleEqual("false"))) != 0)
      {
        //Display no box
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        {
        HTuple 
          ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
            "box");
        hv_GenParamName.Dispose();
        hv_GenParamName = ExpTmpLocalVar_GenParamName;
        }
        }
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        {
        HTuple 
          ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
            "false");
        hv_GenParamValue.Dispose();
        hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
        }
        }
      }
      else if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleNotEqual("true"))) != 0)
      {
        //Set a color other than the default.
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        {
        HTuple 
          ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
            "box_color");
        hv_GenParamName.Dispose();
        hv_GenParamName = ExpTmpLocalVar_GenParamName;
        }
        }
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        {
        HTuple 
          ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
            hv_Box.TupleSelect(0));
        hv_GenParamValue.Dispose();
        hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
        }
        }
      }
    }
    if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(1))) != 0)
    {
      if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleEqual("false"))) != 0)
      {
        //Display no shadow.
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        {
        HTuple 
          ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
            "shadow");
        hv_GenParamName.Dispose();
        hv_GenParamName = ExpTmpLocalVar_GenParamName;
        }
        }
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        {
        HTuple 
          ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
            "false");
        hv_GenParamValue.Dispose();
        hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
        }
        }
      }
      else if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleNotEqual("true"))) != 0)
      {
        //Set a shadow color other than the default.
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        {
        HTuple 
          ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
            "shadow_color");
        hv_GenParamName.Dispose();
        hv_GenParamName = ExpTmpLocalVar_GenParamName;
        }
        }
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        {
        HTuple 
          ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
            hv_Box.TupleSelect(1));
        hv_GenParamValue.Dispose();
        hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
        }
        }
      }
    }
    //Restore default CoordSystem behavior.
    if ((int)(new HTuple(hv_CoordSystem_COPY_INP_TMP.TupleNotEqual("window"))) != 0)
    {
      hv_CoordSystem_COPY_INP_TMP.Dispose();
      hv_CoordSystem_COPY_INP_TMP = "image";
    }
    //
    if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(""))) != 0)
    {
      //disp_text does not accept an empty string for Color.
      hv_Color_COPY_INP_TMP.Dispose();
      hv_Color_COPY_INP_TMP = new HTuple();
    }
    //
    HOperatorSet.DispText(hv_WindowHandle, hv_String, hv_CoordSystem_COPY_INP_TMP, 
        hv_Row_COPY_INP_TMP, hv_Column_COPY_INP_TMP, hv_Color_COPY_INP_TMP, hv_GenParamName, 
        hv_GenParamValue);

    hv_Color_COPY_INP_TMP.Dispose();
    hv_Column_COPY_INP_TMP.Dispose();
    hv_CoordSystem_COPY_INP_TMP.Dispose();
    hv_Row_COPY_INP_TMP.Dispose();
    hv_GenParamName.Dispose();
    hv_GenParamValue.Dispose();

    return;
  }
  private void action()
  {


    // Local iconic variables 

    HObject ho_Image=null, ho_GrayImage=null, ho_SymbolXLDs=null;

    // Local control variables 

    HTuple hv_AcqHandle = new HTuple(), hv_WindowHandle = new HTuple();
    HTuple hv_DataCodeHandle = new HTuple(), hv_ResultHandles = new HTuple();
    HTuple hv_DecodedDataStrings = new HTuple();
    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_Image);
    HOperatorSet.GenEmptyObj(out ho_GrayImage);
    HOperatorSet.GenEmptyObj(out ho_SymbolXLDs);
    //Image Acquisition 01: Code generated by Image Acquisition 01
    hv_AcqHandle.Dispose();
    HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8, "rgb", 
        -1, "false", "default", "[0] ", 0, -1, out hv_AcqHandle);
    HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
    //设置系统参数，filename_encoding，字符编码设置为utf8
    HOperatorSet.SetSystem("filename_encoding", "utf8");
    if (HDevWindowStack.IsOpen())
    {
      hv_WindowHandle = HDevWindowStack.GetActive();
    }
    while ((int)(1) != 0)
    {
      //从采集图像的设备中异步抓取图像
      ho_Image.Dispose();
      HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
      ho_GrayImage.Dispose();
      HOperatorSet.Rgb1ToGray(ho_Image, out ho_GrayImage);
      //创建一个二维码数据class模型
      hv_DataCodeHandle.Dispose();
      HOperatorSet.CreateDataCode2dModel("QR Code", "default_parameters", "maximum_recognition", 
          out hv_DataCodeHandle);
      //-检测和读取图像中的二维数据代码符号或训练二维数据代码模型。
      ho_SymbolXLDs.Dispose();hv_ResultHandles.Dispose();hv_DecodedDataStrings.Dispose();
      HOperatorSet.FindDataCode2d(ho_GrayImage, out ho_SymbolXLDs, hv_DataCodeHandle, 
          "train", "all", out hv_ResultHandles, out hv_DecodedDataStrings);
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      disp_message(hv_WindowHandle, "识别结果："+hv_DecodedDataStrings, "window", 
          12, 12, "black", "true");
      }
      //删除2D数据代码模型并释放分配的内存。
      HOperatorSet.ClearDataCode2dModel(hv_DataCodeHandle);
    }
    //关闭指定的图像采集设备。
    HOperatorSet.CloseFramegrabber(hv_AcqHandle);
    ho_Image.Dispose();
    ho_GrayImage.Dispose();
    ho_SymbolXLDs.Dispose();

    hv_AcqHandle.Dispose();
    hv_WindowHandle.Dispose();
    hv_DataCodeHandle.Dispose();
    hv_ResultHandles.Dispose();
    hv_DecodedDataStrings.Dispose();

  }
}


