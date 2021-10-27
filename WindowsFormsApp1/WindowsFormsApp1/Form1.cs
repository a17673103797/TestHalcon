﻿using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            // Local iconic variables 

            HObject ho_Image = null;

            // Local control variables 

            HTuple hv_AcqHandle = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            //Image Acquisition 01: Code generated by Image Acquisition 01
            hv_AcqHandle.Dispose();
            HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8, "rgb",
                -1, "false", "default", "[0] ", 0, -1, out hv_AcqHandle);
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
            while ((int)(1) != 0)
            {
                ho_Image.Dispose();
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                //Image Acquisition 01: Do something
            }
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            ho_Image.Dispose();

            hv_AcqHandle.Dispose();
        }
    }
}