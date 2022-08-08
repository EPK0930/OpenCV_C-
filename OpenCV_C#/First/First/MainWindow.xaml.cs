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


// OpenCV 사용을 위한 using
using OpenCvSharp;


namespace First
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
         
        public MainWindow()
        {
            /******************************* 히스토그램 스트레칭과 평활화 비교 *************************************************/
            InitializeComponent();

            //Histogram_Stretching();
           // Histogram_equalization();


            //Cv2.WaitKey();       // 키 입력 대기 함수
            //Cv2.DestroyAllWindows();    // 열려있는 모든창을 닫는 함수




            
            // 변수 초기화 부분
            int a = 0;  // ref 키워드 변수 
            float alpha = 0.3f;

             
            //이미지 절대 경로 지정
            string file_Path = @"C:\Users\eastpillar0930\Desktop\OPENCV\OpenCV_C#\First\lenna.bmp";

           // Mat img1 = new Mat(file_Path, ImreadModes.Grayscale);     // Mat 클래스에 직접 이미지를 할당 가능
           
            
            Mat img1 = Cv2.ImRead(file_Path, ImreadModes.Grayscale);    //Mat 객체 생성(flag는 grayscale)
            Mat dst1 = img1 + (img1 - 128) * alpha; //명암비 조절

            Mat hist = CalcGrayHist(img1.CvPtr);    
            Mat hist_img1 = GetGrayHistImage(hist.CvPtr);

            Mat hist1 = CalcGrayHist(dst1.CvPtr);
            Mat hist_dst1 = GetGrayHistImage(hist1.CvPtr);

            //Cv2.NamedWindow("img1");
            //Cv2.ImShow("img1", img1);      // 이미지 출력 함수 (winname = lenna, InputArray mat)
            //Cv2.ImShow("img1Hist", hist_img1);


            //Cv2.ImShow("dst1", dst1);
            //Cv2.ImShow("dst1Hist", hist_dst1);


            Cv2.NamedWindow("dst");
            // ref a => ref키워드 사용시 스택영역 메모리 주소를 사용하며 참조 호출
            Cv2.CreateTrackbar("Brightness", "dst", ref a, 150, on_brightness, img1.CvPtr);
            on_brightness(0, img1.CvPtr);   // 호출하지 않으면 트랙바를 움직이기 전까지 이미지가 나타나지 않는다.
           // Histogram_Stretching();
           // Histogram_equalization();



            Cv2.WaitKey();       // 키 입력 대기 함수
            Cv2.DestroyAllWindows();    // 열려있는 모든창을 닫는 함수
            
            

        }

        void on_brightness(int pos, IntPtr userdata)    //callback 함수
        {
            Mat img1 = new Mat(userdata);
            Mat dst = img1 + pos;

            Cv2.ImShow("dst", dst);
        }

        Mat CalcGrayHist(IntPtr img)    //히스토그램 계산 함수
        {
            Mat img1 = new Mat(img);
            Mat hist = new Mat();
            int dims = 1;
            int[] histSize = { 256 };
            Rangef[] ranges = { new Rangef(0, 256), };

            Cv2.CalcHist( new Mat[] {img1}, new int[] { 0 } , null, hist, dims, histSize, ranges);  
            return hist;
        }

        Mat GetGrayHistImage(IntPtr hist)   //히스토그램 그리기 함수
        {
            Mat hist1 = new Mat(hist);
            
            double histMin=0, histMax;
            Cv2.MinMaxLoc(hist1, out histMin, out histMax);

            Mat imgHist = new Mat(100, 256, MatType.CV_8UC1, new Scalar(255));
            for (int i = 0; i < 256; i++) 
            {
                //Mat.Line method
                imgHist.Line(i,100,i, 100 - (int)Math.Round(hist1.At<float>(i,0)*100/histMax), new Scalar(0));  //float to int -> (int)Math.Round()
            }

            return imgHist;

        }
            
           
         void Histogram_Stretching()
        {
            //이미지 절대 경로 지정
            string file_Path = @"C:\Users\eastp\OneDrive\바탕 화면\C#\First\hawkes.bmp";
            Mat src = Cv2.ImRead(file_Path, ImreadModes.Grayscale);

            double gmin, gmax;
            Cv2.MinMaxLoc(src, out gmin, out gmax);

            Mat dst3 = (src - gmin) * 255 / (gmax - gmin);
            
            


            Mat hist = CalcGrayHist(src.CvPtr);
            Mat histsrc = GetGrayHistImage(hist.CvPtr);
            Mat hist3 = CalcGrayHist(dst3.CvPtr);
            Mat histdst3 = GetGrayHistImage(hist3.CvPtr);


            Cv2.ImShow("src", src);
            Cv2.ImShow("histsrc", histsrc);

            Cv2.ImShow("dst3", dst3);
            Cv2.ImShow("dst3Hist", histdst3);
        }
            
            
       void Histogram_equalization()
        {
            string file_Path = @"C:\Users\eastp\OneDrive\바탕 화면\C#\First\hawkes.bmp";
            Mat src = Cv2.ImRead(file_Path, ImreadModes.Grayscale);

            Mat dst_equal = src;
            Cv2.EqualizeHist(src, dst_equal);

            Mat hist_equal = CalcGrayHist(dst_equal.CvPtr);
            Mat histdst_equal = GetGrayHistImage(hist_equal.CvPtr);
            Cv2.ImShow("dst_equalization", dst_equal);
            Cv2.ImShow("dst_equalHist", histdst_equal);
        }
    }
}
