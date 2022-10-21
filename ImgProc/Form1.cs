using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImgProcEngine;

namespace ImgProc
{
    public partial class Form1 : Form
    {
        readonly string[] fileNames = new string[]
        {
            //"test",
            "scrubber_control_disabled_holo",
            "scrubber_control_focused_holo",
            "scrubber_control_normal_holo",
            "scrubber_control_pressed_holo",
            "scrubber_primary_holo.9",
            "scrubber_secondary_holo.9",
            "scrubber_track_holo_light.9"
        };

        readonly string directoryAddr = @"D:\Programs\AirGB\app\src\main\res\drawable\";

        //readonly Color BlueReal = Color.FromArgb(255, 0, 0, 255);

        public Form1()
        {
            InitializeComponent();
            //Bitmap b = new Bitmap(@"D:\Programs\AirGB\app\src\main\res\drawable\" + fileNames[2] + @".png");

            //byte[] bByte = null; Conversion.BmpToData(b, ref bByte);
            ////ByteArrayOperations.SetPixel4BppBgra(ref bByte, b.Width, b.Height, 7, 0, Color.Green);
            //Bitmap bConverted = Conversion.LoadBitmapData(false, ref b, bByte, b.Width, b.Height, 1d);

            //pbOriginal.Image = bConverted;

            //foreach (string fname in fileNames)
            //{
            //    Bitmap bmp = new Bitmap(@"D:\Programs\AirGB\app\src\main\res\drawable\" + fname + @".png");
            //    Bitmap converted = ToBlue(bmp);
            //}
        }



        public static Bitmap ToRGB(Bitmap img, int RGBIdx)
        {
            byte[] imgByte = null;
            Conversion.BmpToData(img, ref imgByte);

            for(int col = 0; col < img.Width; col++)
            {
                for(int row = 0; row < img.Height; row++)
                {
                    Color pixel = ByteArrayOperations.GetPixel4BppBgra(imgByte, img.Width, img.Height, col, row);
                    float hue = pixel.GetHue();
                    if(hue > 195 && hue < 199)
                    {
                        byte[] newPix = ComputePixel(pixel, RGBIdx);

                        Color cNew = Color.FromArgb(newPix[ByteArrayOperations.ALPHA_INDEX_BGRA],
                                                    newPix[ByteArrayOperations.RED_INDEX_BGRA],
                                                    newPix[ByteArrayOperations.GREEN_INDEX_BGRA],
                                                    newPix[ByteArrayOperations.BLUE_INDEX_BGRA]);

                        ByteArrayOperations.SetPixel4BppBgra(ref imgByte, img.Width, img.Height, col, row, cNew);
                    }
                }
            }

            ScaleImageColorUp(ref imgByte, img.Width, img.Height, RGBIdx);
            Bitmap converted = img.Clone() as Bitmap;
            Conversion.LoadBitmapData(false, ref converted, imgByte, img.Width, img.Height, 1d);
            return converted;
        }

        int cntr = 0;
        private void btnNext_Click(object sender, EventArgs e)
        {
            Text = fileNames[cntr];
            Bitmap img = new Bitmap(directoryAddr + fileNames[cntr] + @".png");
            pbOriginal.Image = img;

            Bitmap imgBlue = ToRGB(img, ByteArrayOperations.BLUE_INDEX_BGRA);
            Bitmap imgGreen = ToRGB(img, ByteArrayOperations.GREEN_INDEX_BGRA);
            Bitmap imgRed = ToRGB(img, ByteArrayOperations.RED_INDEX_BGRA);

            if(fileNames[cntr].Contains("primary"))
            {
                ScaleUpPrimaryImage(imgBlue, ByteArrayOperations.BLUE_INDEX_BGRA);
                ScaleUpPrimaryImage(imgGreen, ByteArrayOperations.GREEN_INDEX_BGRA);
                ScaleUpPrimaryImage(imgRed, ByteArrayOperations.RED_INDEX_BGRA);
            }
            if (fileNames[cntr].Contains("secondary"))
            {
                ScaleUpSecondaryImage(imgBlue, ByteArrayOperations.BLUE_INDEX_BGRA);
                ScaleUpSecondaryImage(imgGreen, ByteArrayOperations.GREEN_INDEX_BGRA);
                ScaleUpSecondaryImage(imgRed, ByteArrayOperations.RED_INDEX_BGRA);
            }

            imgBlue.Save(directoryAddr + "blue_" + fileNames[cntr] + @".png", ImageFormat.Png);
            imgGreen.Save(directoryAddr + "green_" + fileNames[cntr] + @".png", ImageFormat.Png);
            imgRed.Save(directoryAddr + "red_" + fileNames[cntr] + @".png", ImageFormat.Png);

            pbEdited.Image = imgBlue;
            cntr++;
            if (cntr == fileNames.Length)
            {
                cntr = 0;
            }
        }

        private void ScaleUpSecondaryImage(Bitmap img, int RGBIdx)
        {
            byte[] imgByte = null;
            Conversion.BmpToData(img, ref imgByte);
            int w = img.Width, h = img.Height;


            for (int col = 0; col < w; col++)
            {
                for (int row = 0; row < h; row++)
                {
                    Color c = ByteArrayOperations.GetPixel4BppBgra(imgByte, w, h, col, row);
                    byte[] cByte = Color32bppBgra2ByteArr(c);
                    if(cByte[RGBIdx] != 0)
                    {
                        cByte[RGBIdx] = (cByte[RGBIdx] + 10) > 255 ? (byte)255 : (byte)(cByte[RGBIdx] + 10);
                    }
                    //float scaled = (float)cByte[RGBIdx] * scaleFactor;
                    float scaled = cByte[ByteArrayOperations.ALPHA_INDEX_BGRA] == 0 ? 0f : (float)cByte[ByteArrayOperations.ALPHA_INDEX_BGRA] + 50;
                    if (scaled > 255)
                    {
                        cByte[ByteArrayOperations.ALPHA_INDEX_BGRA] = 255;
                        ByteArrayOperations.SetPixel4BppBgra(ref imgByte, w, h, col, row, Color.FromArgb(cByte[ByteArrayOperations.ALPHA_INDEX_BGRA],
                                                                                                         cByte[ByteArrayOperations.RED_INDEX_BGRA],
                                                                                                         cByte[ByteArrayOperations.GREEN_INDEX_BGRA],
                                                                                                         cByte[ByteArrayOperations.BLUE_INDEX_BGRA]));
                    }
                    else
                    {
                        cByte[ByteArrayOperations.ALPHA_INDEX_BGRA] = (byte)scaled;
                        ByteArrayOperations.SetPixel4BppBgra(ref imgByte, w, h, col, row, Color.FromArgb(cByte[ByteArrayOperations.ALPHA_INDEX_BGRA],
                                                                                                         cByte[ByteArrayOperations.RED_INDEX_BGRA],
                                                                                                         cByte[ByteArrayOperations.GREEN_INDEX_BGRA],
                                                                                                         cByte[ByteArrayOperations.BLUE_INDEX_BGRA]));
                    }
                }
            }

            Conversion.LoadBitmapData(false, ref img, imgByte, img.Width, img.Height, 1d);
        }

        private void ScaleUpPrimaryImage(Bitmap img, int RGBIdx)
        {

            byte[] imgByte = null;
            Conversion.BmpToData(img, ref imgByte);
            int w = img.Width, h = img.Height;

            float scaleFactor = 1.4f;
            // scale up
            for (int col = 0; col < w; col++)
            {
                for (int row = 0; row < h; row++)
                {
                    Color c = ByteArrayOperations.GetPixel4BppBgra(imgByte, w, h, col, row);
                    byte[] cByte = Color32bppBgra2ByteArr(c);
                    float scaled = (float)cByte[RGBIdx] * scaleFactor;
                    if (scaled > 230)
                    {
                        cByte[RGBIdx] = 230;
                        ByteArrayOperations.SetPixel4BppBgra(ref imgByte, w, h, col, row, Color.FromArgb(cByte[ByteArrayOperations.ALPHA_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.RED_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.GREEN_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.BLUE_INDEX_BGRA]));
                    }
                    else if (scaled > 0)
                    {
                        cByte[RGBIdx] = (byte)scaled;
                        ByteArrayOperations.SetPixel4BppBgra(ref imgByte, w, h, col, row, Color.FromArgb(cByte[ByteArrayOperations.ALPHA_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.RED_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.GREEN_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.BLUE_INDEX_BGRA]));
                    }
                }
            }

            Conversion.LoadBitmapData(false, ref img, imgByte, img.Width, img.Height, 1d);
        }

        static byte[] Color32bppBgra2ByteArr(Color c)
        {
            byte[] b = new byte[4];
            b[ByteArrayOperations.ALPHA_INDEX_BGRA] = c.A;
            b[ByteArrayOperations.RED_INDEX_BGRA]   = c.R;
            b[ByteArrayOperations.GREEN_INDEX_BGRA] = c.G;
            b[ByteArrayOperations.BLUE_INDEX_BGRA]  = c.B;
            return b;
        }



        static void ScaleImageColorUp(ref byte[] img, int w, int h, int RGBIdx)
        {
            byte maxVal = 0;

            // find max
            for (int col = 0; col < w; col++)
            {
                for(int row = 0; row < h; row++)
                {
                    Color c = ByteArrayOperations.GetPixel4BppBgra(img, w, h, col, row);
                    byte[] cByte = Color32bppBgra2ByteArr(c);
                    if (cByte[RGBIdx] > maxVal) maxVal = cByte[RGBIdx];
                }
            }

            float scaleFactor = 255f / maxVal;
            // scale up
            for (int col = 0; col < w; col++)
            {
                for (int row = 0; row < h; row++)
                {
                    Color c = ByteArrayOperations.GetPixel4BppBgra(img, w, h, col, row);
                    byte[] cByte = Color32bppBgra2ByteArr(c);
                    float scaled = (float)cByte[RGBIdx] * scaleFactor;
                    if (scaled > 230)
                    {
                        cByte[RGBIdx] = 230;
                        ByteArrayOperations.SetPixel4BppBgra(ref img, w, h, col, row, Color.FromArgb(cByte[ByteArrayOperations.ALPHA_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.RED_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.GREEN_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.BLUE_INDEX_BGRA]));
                    }
                    else if(scaled > 0)
                    {
                        cByte[RGBIdx] = (byte)scaled;
                        ByteArrayOperations.SetPixel4BppBgra(ref img, w, h, col, row, Color.FromArgb(cByte[ByteArrayOperations.ALPHA_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.RED_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.GREEN_INDEX_BGRA],
                                                    cByte[ByteArrayOperations.BLUE_INDEX_BGRA]));
                    }
                }
            }
        }

        static byte[] ComputePixel(Color c, int idx)
        {
            byte[] cByte = Color32bppBgra2ByteArr(c);
            byte[] bNew = new byte[4];
            bNew[ByteArrayOperations.ALPHA_INDEX_BGRA] = c.A - 20 > 0 ? (byte)(c.A - 20) : (byte)0;
            bNew[ByteArrayOperations.RED_INDEX_BGRA]   = 0;
            bNew[ByteArrayOperations.GREEN_INDEX_BGRA] = 0;
            bNew[ByteArrayOperations.BLUE_INDEX_BGRA]  = 0;

            float bright = c.GetBrightness();
            bNew[idx] = (byte)(c.GetBrightness() * 255);

            return bNew;
        }
    }
}
