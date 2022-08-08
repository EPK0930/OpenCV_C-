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
namespace Ch_9
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // git git

            //Sobel();
            //Canny();
            //Hough_lines();
            //Hough_line_segments();
            //Hough_circles();
        }

        public void Sobel()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_9\lenna.bmp", ImreadModes.Grayscale);

            Mat dx = new Mat(), dy = new Mat();
            Cv2.Sobel(src, dx, MatType.CV_32FC1, 1, 0);
            Cv2.Sobel(src, dy, MatType.CV_32FC1, 0, 1);

            Mat fmag = new Mat(), mag = new Mat();
            Cv2.Magnitude(dx, dy, fmag);
            fmag.ConvertTo(mag, MatType.CV_8UC1);



            Mat edge = mag.GreaterThan(150);


            Cv2.ImShow("src", src);
            Cv2.ImShow("mag", mag);
            Cv2.ImShow("edge", edge);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        public void Canny()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_9\lenna.bmp", ImreadModes.Grayscale);

            Mat dst1 = new Mat(), dst2 = new Mat();

            Cv2.Canny(src, dst1, 50, 100);
            Cv2.Canny(src, dst2, 50, 150);

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst1", dst1);
            Cv2.ImShow("dst2", dst2);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();

        }

        public void Hough_lines()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_9\building.jpg", ImreadModes.Grayscale);

            Mat edge = new Mat();
            Cv2.Canny(src, edge, 50, 150);



            LineSegmentPolar[] lines;
            lines = Cv2.HoughLines(edge, 1, Math.PI / 180, 250);

            Mat dst = new Mat();

            Cv2.CvtColor(edge, dst, ColorConversionCodes.GRAY2BGR);

            for (int i = 0; i < lines.Length; i++)
            {
                float rho = lines[i].Rho, theta = lines[i].Theta;
                double cos_t = Math.Cos(theta), sin_t = Math.Sin(theta);
                double x0 = cos_t * rho;
                double y0 = sin_t * rho;
                double alpha = 1000;

                OpenCvSharp.Point pt1 = new OpenCvSharp.Point { X=(int)Math.Round(x0 + alpha * (-sin_t)), Y=(int)Math.Round(y0 + alpha * cos_t) };
                OpenCvSharp.Point pt2 = new OpenCvSharp.Point { X=(int)Math.Round(x0 - alpha * (-sin_t)), Y=(int)Math.Round(y0 - alpha * cos_t) };
                Cv2.Line(dst, pt1, pt2, Scalar.Red, 2, LineTypes.AntiAlias);
            }
            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        public void Hough_line_segments()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_9\building.jpg", ImreadModes.Grayscale);

            Mat edge = new Mat();
            Cv2.Canny(src, edge, 50, 150);

            LineSegmentPoint[] lines;
            lines = Cv2.HoughLinesP(edge, 1, Math.PI / 180, 160, 50, 5);

            Mat dst = new Mat();
            Cv2.CvtColor(edge, dst, ColorConversionCodes.GRAY2BGR);

            foreach (LineSegmentPoint l in lines)
            {
                Cv2.Line(dst, l.P1, l.P2, Scalar.Red, 2, LineTypes.AntiAlias);
            }

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();

            
        }

        public void Hough_circles()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_9\coins.png", ImreadModes.Grayscale);

            Mat blurred = new Mat();
            Cv2.Blur(src, blurred, new OpenCvSharp.Size(3, 3));

            CircleSegment[] circles;
            circles = Cv2.HoughCircles(blurred, HoughModes.Gradient, 1, 50, 150, 30);

            Mat dst = new Mat();
            Cv2.CvtColor(src, dst, ColorConversionCodes.GRAY2BGR);

            foreach(CircleSegment c in circles)
            {

                Cv2.Circle(dst, (int)c.Center.X, (int)c.Center.Y, (int)c.Radius, Scalar.Red, 2, LineTypes.AntiAlias);
                
            }
            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
