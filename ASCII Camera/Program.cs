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
            //Set Colors for better view
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Choose camera");

            //Collect all cameras
            FilterInfoCollection cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //Set default camera to the first one
            int camNum = 0;

            //Write out all cameras to choose from
            if (cameras.Count > 1)
            {
                for (int i = 0; i < cameras.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". - " + cameras[i].Name);
                }
                camNum = Convert.ToInt32(Console.ReadLine()) - 1;
            }

            //Turn off console
            Console.CursorVisible = false;
            //Set camera by the choosen index
            VideoCaptureDevice finalVideo = new VideoCaptureDevice(new FilterInfoCollection(FilterCategory.VideoInputDevice)[camNum].MonikerString);
            
            //Call event everytime a new frame appears
            finalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
            //Start stream
            finalVideo.Start();
        }

        //Event that calls at every new frame 
        static void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap video = (Bitmap)eventArgs.Frame.Clone();
            //Convert frame to grayscale
            video = ConvertToGrayScale(video);
            //Set size to actual console size
            video = new Bitmap(video, new Size(Console.BufferWidth-5, Console.WindowHeight-5 ));
            //Call function to convert to ascii
            WriteBitmapToConsoleInASCII(video);
            //System.Threading.Thread.Sleep(50);
            
        }
        //Converts image to grayscale
        public static Bitmap ConvertToGrayScale(Bitmap bm)
        {
            var filterGreyScale = new Grayscale(0.2125, 0.7154, 0.0721);

            bm = filterGreyScale.Apply(bm);
            return bm;
        }

        //Write out image in ascii
        public static void WriteBitmapToConsoleInASCII(Bitmap bm)
        {
            //Set cursor to start of console
            Console.SetCursorPosition(0,0);
            /*
            Use a stringbuilder because it is smoother.
            Because it is grayscale every pixel red, green and blue pixel value are equal.
            So it tests for the red one, and it is set a specific character for every range.
            */
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
            //Then write out the whole image
            Console.Write(sb);
        }
    }
}