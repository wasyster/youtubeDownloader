using Android.App;
using Android.Runtime;

namespace YoutubeDownloader.Application
{
    [Application]
    public class MainApplication : YoutubeDownloader.MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
