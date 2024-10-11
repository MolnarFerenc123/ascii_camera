using System;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Math.Metrics;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;
using System.Text;

namespace ASCIICamera
{
    class ASCIICamera
    {
        static void Main(string[] args)
        {
            int camNum = Convert.ToInt32(Console.ReadLine());

            Console.CursorVisible = false;
            VideoCaptureDevice finalVideo = new VideoCaptureDevice(new FilterInfoCollection(FilterCategory.VideoInputDevice)[camNum].MonikerString);
            
            finalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
            finalVideo.Start();
        }
        static void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap video = (Bitmap)eventArgs.Frame.Clone();
            video = ConvertToGrayScale(video);
            video = new Bitmap(video, new Size(Console.BufferWidth-5, Console.WindowHeight-5 ));
            //video.Save("C:\\proba.jpeg", ImageFormat.Jpeg);
            WriteBitmapToConsoleInASCII(video);
            //System.Threading.Thread.Sleep(50);
            
        }
        public static Bitmap ConvertToGrayScale(Bitmap bm)
        {
            var filterGreyScale = new Grayscale(0.2125, 0.7154, 0.0721);

            bm = filterGreyScale.Apply(bm);
            return bm;
        }

        public static void WriteBitmapToConsoleInASCII(Bitmap bm)
        {
            Console.SetCursorPosition(0,0);
            var sb = new StringBuilder();
            for (int i = 0; i < bm.Height; i++)
            {
                for (int j = 0; j < bm.Width; j++)
                {
                    int pixelInfo = bm.GetPixel(j, i).R;
                    if (pixelInfo <= 33)
                    {
                        sb.Append("@");
                    }
                    else
                    {
                        if (pixelInfo <= 58)
                        {
                            sb.Append('%');
                        }
                        else
                        {
                            if (pixelInfo <= 96)
                            {
                                sb.Append('*');
                            }
                            else
                            {
                                if (pixelInfo <= 119)
                                {
                                    sb.Append("+");
                                }
                                else
                                {
                                    if (pixelInfo <= 144)
                                    {
                                        sb.Append('=');
                                    }
                                    else
                                    {
                                        if (pixelInfo <= 154)
                                        {
                                            sb.Append("-");
                                        }
                                        else
                                        {
                                            if (pixelInfo <= 160)
                                            {
                                                sb.Append(':');
                                            }
                                            else
                                            {
                                                sb.Append(' ');
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                sb.Append('\n');
            }
            Console.Write(sb);
        }
    }
}