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

namespace Ch_08
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //Affine();
            //Translation();
            //Shear();
            //Scale();
            //Rotation();
            //Flip();

            Perspective();
        }

        public void Affine()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_08\tekapo.bmp");

            Point2f[] srcPts = new Point2f[3] { new Point2f(0, 0), new Point2f(src.Cols - 1, 0), new Point2f(src.Cols - 1, src.Rows - 1) };
            Point2f[] dstPts = new Point2f[3] { new Point2f(50, 50), new Point2f(src.Cols - 100, 100), new Point2f(src.Cols - 50, src.Rows - 50) };


            Mat M = Cv2.GetAffineTransform(srcPts, dstPts);

            Mat dst = new Mat();
            Cv2.WarpAffine(src, dst, M, new OpenCvSharp.Size());

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        public void Translation()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_08\tekapo.bmp");

            double[] array = new double[] { 1, 0, 150, 0, 1, 100 };
            Mat M = new Mat<double>(2, 3, array);

            Mat dst = new Mat();
            Cv2.WarpAffine(src, dst, M, new OpenCvSharp.Size());

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        public void Shear()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_08\tekapo.bmp");

            double mx = 0.3;

            double[] array = new double[] { 1, mx, 0, 0, 1, 0 };
            Mat M = new Mat<double>(2, 3, array);

            Mat dst = new Mat();

            

            Cv2.WarpAffine(src, dst, M, new OpenCvSharp.Size(Math.Round(src.Cols + src.Rows * mx), src.Rows));

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        public void Scale()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_08\rose.bmp");

            Mat dst1 = new Mat(), dst2 = new Mat(), dst3 = new Mat(), dst4 = new Mat();

            Cv2.Resize(src, dst1, new OpenCvSharp.Size(), 4, 4, InterpolationFlags.Nearest);
            Cv2.Resize(src, dst2, new OpenCvSharp.Size(1920, 1280));
            Cv2.Resize(src, dst3, new OpenCvSharp.Size(1920, 1280),0,0,InterpolationFlags.Cubic);
            Cv2.Resize(src, dst4, new OpenCvSharp.Size(1920, 1280),0,0,InterpolationFlags.Lanczos4);

          
            dst1 = dst1.SubMat(new OpenCvSharp.Rect(400, 500, 400, 400));
            dst2 = dst2.SubMat(new OpenCvSharp.Rect(400, 500, 400, 400));
            dst3 = dst3.SubMat(new OpenCvSharp.Rect(400, 500, 400, 400));
            dst4 = dst4.SubMat(new OpenCvSharp.Rect(400, 500, 400, 400));

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst1", dst1);
            Cv2.ImShow("dst2", dst2);
            Cv2.ImShow("dst3", dst3);
            Cv2.ImShow("dst4", dst4);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        public void Rotation()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_08\tekapo.bmp");

            Point2f cp = new Point2f(src.Cols / 2.0f, src.Rows / 2.0f);
            Mat M = Cv2.GetRotationMatrix2D(cp, 20, 1);

            Mat dst = new Mat();
            Cv2.WarpAffine(src, dst, M, new OpenCvSharp.Size());

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        public void Flip()
        {
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_08\eastsea.bmp");
            Cv2.ImShow("src", src);

            Mat dst = new Mat();
            

            
            Cv2.Flip(src, dst, FlipMode.Y);
            String desc = String.Format("flipCode: {0}", 1);
            Cv2.PutText(dst, desc, new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 1.0, new Scalar(255, 0, 0), 1, LineTypes.AntiAlias);
            Cv2.ImShow("dst", dst);
            Cv2.WaitKey();

            Cv2.Flip(src, dst, FlipMode.X);
            desc = String.Format("flipCode: {0}", 0);
            Cv2.PutText(dst, desc, new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 1.0, new Scalar(255, 0, 0), 1, LineTypes.AntiAlias);
            Cv2.ImShow("dst", dst);
            Cv2.WaitKey();

            Cv2.Flip(src, dst, FlipMode.XY);
            desc = String.Format("flipCode: {0}", -1);
            Cv2.PutText(dst, desc, new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 1.0, new Scalar(255, 0, 0), 1, LineTypes.AntiAlias);
            Cv2.ImShow("dst", dst);
            Cv2.WaitKey();

            Cv2.DestroyAllWindows();
        }

        Mat src1 = new Mat();
        Point2f[] srcQuad = new Point2f[4], dstQuad = new Point2f[4];

        public int Perspective()
        {
            src1 = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_08\card.bmp");

            Cv2.NamedWindow("src1");
            Cv2.SetMouseCallback("src1", on_mouse);

            Cv2.ImShow("src1", src1);
            Cv2.WaitKey();


            return 0;
        }
        public static class CNT
        {
            public static int cnt = 0;
        }
        public void on_mouse(MouseEventTypes eventoccur, int x, int y, MouseEventFlags flags, IntPtr userData )
        {

            if (eventoccur == MouseEventTypes.LButtonDown)
            {
                if(CNT.cnt<4)
                {
                    srcQuad[CNT.cnt++] = new Point2f(x, y);

                    Cv2.Circle(src1, new OpenCvSharp.Point(x, y), 5, new Scalar(0, 0, 255), -1);
                    Cv2.ImShow("src1", src1);

                    if(CNT.cnt==4)
                    {
                        int w = 200, h = 300;

                        dstQuad[0] = new Point2f(0, 0);
                        dstQuad[1] = new Point2f(w - 1, 0);
                        dstQuad[2] = new Point2f(w - 1, h - 1);
                        dstQuad[3] = new Point2f(0, h - 1);

                        Mat pers = Cv2.GetPerspectiveTransform(srcQuad, dstQuad);

                        Mat dst = new Mat();
                        Cv2.WarpPerspective(src1, dst, pers, new OpenCvSharp.Size(w, h));

                        Cv2.ImShow("dst", dst);
                    }
                }
            }

        }

       
    }
    
        
}
