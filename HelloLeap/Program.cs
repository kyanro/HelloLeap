using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using System.Diagnostics;

namespace HelloLeapConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Controller controller = new Controller())
            {
                //controller.FrameReady += newFrameHandler;
                SampleListener listener = new SampleListener();
                controller.Connect += listener.OnServiceConnect;
                controller.Device += listener.OnConnect;
                //controller.FrameReady += listener.OnFrame;
                controller.FrameReady += newFrameHandler;
                // Keep this process running until Enter is pressed
                Console.WriteLine("Press Enter to quit...");
                Console.ReadLine();
            }
        }

        static void newFrameHandler(object sender, FrameEventArgs eventArgs)
        {
            Frame frame = eventArgs.frame;
            Console.WriteLine("ID:" + frame.Id);
            Console.WriteLine("Timestamp:" + frame.Timestamp);
            Console.WriteLine("FPS:" + frame.CurrentFramesPerSecond);
            Console.WriteLine("HandCount:" + frame.Hands.Count);
        }
    }

    class SampleListener
    {
        public void OnServiceConnect(object sender, ConnectionEventArgs args)
        {
            Console.WriteLine("Service Connected");
        }

        public void OnConnect(object sender, DeviceEventArgs args)
        {
            Console.WriteLine("Connected");
        }

        public void OnFrame(object sender, FrameEventArgs args)
        {
            Console.WriteLine("Frame Available.");
        }
    }
}
