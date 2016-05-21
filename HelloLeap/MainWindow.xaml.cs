using Leap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace HelloLeap
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        //public MainWindow()
        //{
        //    InitializeComponent();
        //    Debug.WriteLine("log:" + button.Content);

        //    using (Controller controller = new Controller())
        //    {
        //        SampleListener listener = new SampleListener(this);
        //        controller.Connect += listener.OnServiceConnect;
        //        controller.Device += listener.OnConnect;
        //        controller.FrameReady += listener.OnFrame;

        //    }


        //}

        //class SampleListener
        //{
        //    private MainWindow mainWindow;

        //    public SampleListener(MainWindow mainWindow)
        //    {
        //        this.mainWindow = mainWindow;
        //    }

        //    public void OnServiceConnect(object sender, ConnectionEventArgs args)
        //    {
        //        Console.WriteLine("Service Connected");
        //    }

        //    public void OnConnect(object sender, DeviceEventArgs args)
        //    {
        //        Console.WriteLine("Connected");
        //    }

        //    public void OnFrame(object sender, FrameEventArgs args)
        //    {
        //        Leap.Frame frame = args.frame;
        //        mainWindow.id.Content = frame.Id.ToString();
        //        mainWindow.time_stomp.Content = frame.Timestamp.ToString();
        //        mainWindow.fps.Content = frame.CurrentFramesPerSecond.ToString();
        //        mainWindow.hands.Content = frame.Hands.Count.ToString();


        //    }
        //}

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("log:" + sender.GetType());

            button.Content = "clicked!";
        }

        private Controller controller = new Controller();

        public MainWindow()
        {
            InitializeComponent();
            using (Controller controller = new Controller())
            {
                controller.FrameReady += newFrameHandler;
            }
        }

        long animationStarted;
        Boolean isLastExtended;

        void newFrameHandler(object sender, FrameEventArgs eventArgs)
        {
            Leap.Frame frame = eventArgs.frame;
            this.id.Content = frame.Id.ToString();
            this.time_stomp.Content = frame.Timestamp.ToString();
            this.fps.Content = frame.CurrentFramesPerSecond.ToString();
            this.hands.Content = frame.Hands.Count.ToString();

            if (frame.Hands.Count > 0 && frame.Hands[0].Fingers.Count >= 1)
            {
                //this.fingers.Content = frame.Hands[0].PinchStrength;
                this.fingers.Content = frame.Hands[0].Fingers[1].IsExtended;
                if (isLastExtended != frame.Hands[0].Fingers[1].IsExtended)
                {
                    this.button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    animationStarted = frame.Timestamp;
                    isLastExtended = frame.Hands[0].Fingers[1].IsExtended;
                }
            }
        }
    }
}
