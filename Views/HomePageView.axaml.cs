using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Threading;
//the below was suggested as a solution - todo - is there a way to just change the string reference of the file path?
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace GroupProject.Views
{
    public partial class HomePageView : UserControl
    {

        bool defaultKeyframe = true;//start on "default monkey"
      
        public HomePageView()
        {
            InitializeComponent();
            AnimatedMonke();
         
        }

        private async void AnimatedMonke()
        {   //add random
            //values for animation times
            var timeOnBlink = TimeSpan.FromSeconds(0.35);
            var timeOnKeyframe = TimeSpan.FromSeconds(3.5); 

            //timer 
            var periodicTimer = new PeriodicTimer(timeOnBlink);
            while (await periodicTimer.WaitForNextTickAsync())
            {

                //if we're already on the "default monkey", change to the next one at the file path and change the flag, and change the timer interval
                if (defaultKeyframe)
                {
                    //This line below was slightly different, and I accepted a change from Visual Studio - it included the underscore "discard" - need to look into it incase it causes future issues
                    _ = Dispatcher.UIThread.InvokeAsync(async () =>
                    {
                        LogoImage.Source = new Bitmap("C:\\Users\\user\\Documents\\GitHub\\COMP208-GroupProject\\Assets\\homepage-logo-02.png");
                        defaultKeyframe = false;
                        periodicTimer = new PeriodicTimer(timeOnKeyframe); 
                    });
                }

                //if we're already on the "blinking monkey", change to the next one at the file path and change the flag, and timer interval
                else
                {
                    _ = Dispatcher.UIThread.InvokeAsync(async () =>
                    {
                        LogoImage.Source = new Bitmap("C:\\Users\\user\\Documents\\GitHub\\COMP208-GroupProject\\Assets\\homepage-logo.png");
                        defaultKeyframe = true;
                        periodicTimer = new PeriodicTimer(timeOnBlink); 
                    });
                }
            }
        }
    }
}