using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using OpenCvSharp;

namespace Ch_10
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Color_inverse();
            //Color_gray();
            //Color_split();

            //Equalizehist();
            //Inrange();
            Calcbackproject();
        }


        public void Color_inverse()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_10\butterfly.jpg", ImreadModes.Color);
            Mat dst = new Mat(src.Rows, src.Cols, src.Type());


            //Mat dst = new Scalar(255, 255, 255) - src;    // 또 다른 방법1


            /*                                              // 또 다른 방법2
            Mat dst = new Mat();
            Cv2.BitwiseNot(src,dst);
            */

            
            for (int j = 0; j<src.Rows; j++)
            {
                for (int i = 0; i<src.Cols; i++)
                {
                    Vec3b p1 = src.At<Vec3b>(j, i);
                    Vec3b p2 = new Vec3b(255, 255, 255);
                    dst.At<Vec3b>(j, i) =  p2 - src.At<Vec3b>(j, i);
                }
            }

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();

        }

        public void Color_gray()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_10\butterfly.jpg", ImreadModes.Color);
            Mat dst = new Mat();

            Cv2.CvtColor(src,dst, ColorConversionCodes.BGR2GRAY);

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }


        public void Color_split()   
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_10\candies.png", ImreadModes.Color);

            Mat dst = new Mat();
            Mat[] bgr_planes = new Mat[3];

            Cv2.Split(src, out bgr_planes);
            //bgr_planes = Cv2.Split(src); 이렇게도 가능
            Cv2.ImShow("src", src);
            Cv2.ImShow("B_plane", bgr_planes[0]);
            Cv2.ImShow("G_plane", bgr_planes[1]);
            Cv2.ImShow("R_plane", bgr_planes[2]);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        public void Equalizehist()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_10\pepper.bmp", ImreadModes.Color);

            Mat src_ycrcb = new Mat();
            Cv2.CvtColor(src,src_ycrcb, ColorConversionCodes.BGR2YCrCb);

            Mat[] ycrcb_planes = new Mat[3];
            Cv2.Split(src_ycrcb, out ycrcb_planes);

            Cv2.EqualizeHist(ycrcb_planes[0], ycrcb_planes[0]);

            Mat dst_ycrcb = new Mat();
            Cv2.Merge(ycrcb_planes, dst_ycrcb);


            Mat dst = new Mat();
            Cv2.CvtColor(dst_ycrcb, dst, ColorConversionCodes.YCrCb2BGR);

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        public static class variable
        {
            public static int lower_hue = 40, upper_hue = 80;
            public static Mat src = new Mat(), src_hsv = new Mat(), mask = new Mat();
        }

        public void Inrange()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_10\candies.png", ImreadModes.Color);

            Cv2.CvtColor(src, variable.src_hsv, ColorConversionCodes.BGR2HSV);

            Cv2.ImShow("src", src);

            Cv2.NamedWindow("mask");
            Cv2.CreateTrackbar("Lower Hue", "mask", ref variable.lower_hue, 179, On_hue_changed);
            Cv2.CreateTrackbar("Upper Hue", "mask", ref variable.upper_hue, 179, On_hue_changed);
            On_hue_changed(0, src.CvPtr);

            Cv2.WaitKey();


        }

        public void On_hue_changed(int pos, IntPtr userdata)
        {
            Scalar lowerb = new Scalar(variable.lower_hue, 100, 0);
            Scalar upperb = new Scalar(variable.upper_hue, 255, 255);
            Cv2.InRange(variable.src_hsv, lowerb, upperb, variable.mask);

            Cv2.ImShow("mask", variable.mask);
        }


        public void Calcbackproject()
        {
            Mat refer = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_10\ref.png", ImreadModes.Color);
            Mat mask = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_10\mask.bmp", ImreadModes.Grayscale);
            Mat ref_ycrcb = new Mat();
            Cv2.CvtColor(refer, ref_ycrcb, ColorConversionCodes.BGR2YCrCb);
            

            Mat hist = new Mat();
            int[] channels = { 1, 2 };
            int cr_bins = 128; int cb_bins = 128;
            int[] histsize = { cr_bins, cb_bins };
            Rangef[] ranges = { new Rangef(0, 256), };

            Cv2.CalcHist(new Mat[] { ref_ycrcb }, channels, mask, hist, 1, histsize, ranges);



            Mat src = new Mat(), src_ycrcb = new Mat();
            src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_10\kids.png", ImreadModes.Color);
            Cv2.CvtColor(src, src_ycrcb, ColorConversionCodes.BGR2YCrCb);

            Mat backproj = new Mat();
            Cv2.CalcBackProject(new Mat[] { src_ycrcb }, channels, hist, backproj, ranges, true);



            Cv2.ImShow("src", src);
            Cv2.ImShow("backproj", backproj);

            Cv2.WaitKey();


        }
    }
}
