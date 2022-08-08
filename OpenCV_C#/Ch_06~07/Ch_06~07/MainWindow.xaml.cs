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

using OpenCvSharp;      //opencv namespace 사용 (외부 dll파일도 사용할 수 있다.)
namespace Ch_06_07
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // CH06
            /*
            Mat src1 = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_06~07\lenna256.bmp", ImreadModes.Grayscale);
            Mat src2 = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_06~07\square.bmp", ImreadModes.Grayscale);

            Cv2.ImShow("src1", src1);
            Cv2.ImShow("src2", src2);

            Mat dst1, dst2, dst3, dst4;

            dst1=new Mat();
            dst2=new Mat();
            dst3=new Mat();
            dst4=new Mat();

            Cv2.Add(src1, src2, dst1);
            Cv2.AddWeighted(src1, 0.5, src2, 0.5, 0, dst2);
            Cv2.Subtract(src1, src2, dst3);
            Cv2.Absdiff(src1, src2, dst4);  //사각형만 반전 절댓값 씌웠기 때문

            //Cv2.BitwiseAnd(src1, src2, dst1);
            //Cv2.BitwiseOr(src1, src2, dst2);  //사각형이 255니까 or연산시 사각형 모드 픽셀 비트 1
            //Cv2.BitwiseXor(src1, src2, dst3); // 흰색 사각형이 255니까 레나에서 1인부분은 0으로 바뀌고 0인 부분은 1로 바뀌니까 사각형만 반전
            //Cv2.BitwiseNot(src1, dst4);       //영상반전

            Cv2.ImShow("dst1", dst1);
            Cv2.ImShow("dst2", dst2);
            Cv2.ImShow("dst3", dst3);
            Cv2.ImShow("dst4", dst4);

            Cv2.WaitKey();
            */


            //CH07 Embossing
            /*
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_06~07\rose.bmp", ImreadModes.Grayscale);

            float[] data = { -1, -1, 0, -1, 0, 1, 0, 1, 1 };
            Mat emboss = new Mat(3, 3, MatType.CV_32FC1, data);

            Mat dst = new Mat();
            Cv2.Filter2D(src, dst, -1, emboss, new OpenCvSharp.Point(-1, -1), 128);

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
            */


            //평균값 필터
            /*
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_06~07\rose.bmp", ImreadModes.Grayscale);
            Cv2.ImShow("src", src);

            Mat dst = new Mat();

            for (int ksize = 3; ksize<=7; ksize+=2) 
            {
                Cv2.Blur(src, dst, new OpenCvSharp.Size(ksize, ksize));

                String desc = String.Format("Mean: {0}x{1}", ksize, ksize);

                Cv2.PutText(dst, desc, new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 1.0, new Scalar(255), 1, LineTypes.AntiAlias);

                Cv2.ImShow("dst", dst);

                Cv2.WaitKey();
            }

            Cv2.DestroyAllWindows();
            */

            

            //가우시안 필터
            /*
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_06~07\rose.bmp", ImreadModes.Grayscale);
            Cv2.ImShow("src", src);

            Mat dst = new Mat();

            for (int sigma = 1; sigma<=5; sigma++)
            {
                Cv2.GaussianBlur(src, dst, new OpenCvSharp.Size(),Convert.ToDouble(sigma));

                String text = String.Format("sigma = {0}", sigma);

                Cv2.PutText(dst, text, new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 1.0, new Scalar(255), 1, LineTypes.AntiAlias);

                Cv2.ImShow("dst", dst);

                Cv2.WaitKey();
            }

            Cv2.DestroyAllWindows();
            */


            //언샤프 마스크 필터
            /*
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_06~07\rose.bmp", ImreadModes.Grayscale);
            Cv2.ImShow("src", src);

            for(int sigma = 1; sigma <=8; sigma++)
            {
                Mat blurred = new Mat();
                Cv2.GaussianBlur(src, blurred, new OpenCvSharp.Size(), sigma);

                float alpha = 1.0f;
                Mat dst = (1 + alpha) * src - alpha * blurred;

                String desc = String.Format("sigma = {0}", sigma);
                Cv2.PutText(dst, desc, new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 1.0, new Scalar(255), 1, LineTypes.AntiAlias);

                Cv2.ImShow("dst", dst);
                Cv2.WaitKey();

            }
            Cv2.DestroyAllWindows();
            */



            //잡음 제거 필터링
            /*
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_06~07\lenna.bmp", ImreadModes.Grayscale);
            Cv2.ImShow("src", src);

            for(int stddev=10; stddev<= 30; stddev+=10)
            {
                Mat noise = new Mat(src.Size(), MatType.CV_32SC1);
                Cv2.Randn(noise, 0, stddev);

                Mat dst = new Mat();
                Cv2.Add(src, noise, dst, new Mat(), MatType.CV_8U);

                String desc = String.Format("stddev = {0}", stddev);
                Cv2.PutText(dst, desc, new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 1.0, new Scalar(255), 1, LineTypes.AntiAlias);

                Cv2.ImShow("dst", dst);
                Cv2.WaitKey();

            }

            Cv2.DestroyAllWindows();
            */


            //양방향필터
            /*
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_06~07\lenna.bmp", ImreadModes.Grayscale);

            Mat noise = new Mat(src.Size(), MatType.CV_32SC1);
            Cv2.Randn(noise, 0, 5);
            Cv2.Add(src, noise, src, new Mat(), MatType.CV_8U);

            Mat dst1 = new Mat();
            Cv2.GaussianBlur(src, dst1, new OpenCvSharp.Size(),5);

            Mat dst2 = new Mat();
            Cv2.BilateralFilter(src, dst2, -1, 10, 5);


            Cv2.ImShow("src", src);
            Cv2.ImShow("dst1", dst1);
            Cv2.ImShow("dst2", dst2);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
            */


            //미디언 필터
            /*
            Mat src = Cv2.ImRead(@"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\Ch_06~07\lenna.bmp", ImreadModes.Grayscale);

            int num = Convert.ToInt32(src.Total() * 0.1);
            Random rand = new Random();
            

            for (int i=0; i < num; i++)
            {
                int x = rand.Next() % src.Cols;
                int y = rand.Next() % src.Rows;
                src.At<Byte>(y, x)= Convert.ToByte((i % 2) * 255);
            }
            Mat dst1 = new Mat();
            Cv2.GaussianBlur(src, dst1, new OpenCvSharp.Size(), 1);

            Mat dst2 = new Mat();
            Cv2.MedianBlur(src, dst2, 3);

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst1", dst1);
            Cv2.ImShow("dst2", dst2);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
            */
        }
    }
}
