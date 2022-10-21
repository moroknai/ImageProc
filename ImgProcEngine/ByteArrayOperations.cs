using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgProcEngine
{
    public class ByteArrayOperations
    {
        public const int BLUE_INDEX_BGRA = 0;
        public const int GREEN_INDEX_BGRA = 1;
        public const int RED_INDEX_BGRA = 2;
        public const int ALPHA_INDEX_BGRA = 3;


        /// <summary>
        /// Returns the intensity of a 2D grayscale image represented by a 1D byte-array
        /// </summary>
        /// <param name="img">byte-array with the image information</param>
        /// <param name="img_w">width of 2D image</param>
        /// <param name="img_h">height of 2D image</param>
        /// <param name="x">x coordinate of the wanted pixel</param>
        /// <param name="y">y coordinate of the wanted pixel</param>
        /// <returns></returns>
        public static byte GetPixel(ref byte[] img, int img_w, int img_h, int x, int y)
        {
            return img[(y * img_w) + x];
        }

        public static Color GetPixel4BppBgra(byte[] img, int img_w, int img_h, int x, int y)
        {
            byte b = img[(4 * y * img_w) + (4 * x) + BLUE_INDEX_BGRA];
            byte g = img[(4 * y * img_w) + (4 * x) + GREEN_INDEX_BGRA];
            byte r = img[(4 * y * img_w) + (4 * x) + RED_INDEX_BGRA];
            byte a = img[(4 * y * img_w) + (4 * x) + ALPHA_INDEX_BGRA];
            return Color.FromArgb(a, r, g, b);
        }

        public static void SetPixel4BppBgra(ref byte[] img, int img_w, int img_h, int x, int y, Color c)
        {
            img[(4 * y * img_w) + (4 * x) + BLUE_INDEX_BGRA] = c.B;
            img[(4 * y * img_w) + (4 * x) + GREEN_INDEX_BGRA] = c.G;
            img[(4 * y * img_w) + (4 * x) + RED_INDEX_BGRA] = c.R;
            img[(4 * y * img_w) + (4 * x) + ALPHA_INDEX_BGRA] = c.A;
        }


        public static void SetPixel(ref byte[] img, int img_w, int img_h, int x, int y, byte new_pixel)
        {
            img[(y * img_w) + x] = new_pixel;
        }

        public static bool IsPointInside(int im_w, int im_h, int x0, int y0)
        {
            if (x0 >= im_w || x0 < 0) return false;
            if (y0 >= im_h || y0 < 0) return false;
            return true;
        }

        public static bool IsRectangleInside(int im_w, int im_h, Rectangle rect)
        {
            if (rect.X < 0) return false;
            if (rect.Y < 0) return false;
            if ((rect.X + rect.Width) >= im_w) return false;
            if ((rect.Y + rect.Height) >= im_h) return false;
            return true;
        }
    }
}
