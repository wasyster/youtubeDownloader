namespace YoutubeDownloader.MauiApplication;

public partial class App : Application
{
	const int DefaultWidth = 500;
	const int DefaultHeight = 1000;

	public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

	protected override Window CreateWindow(IActivationState activationState)
	{
		Window window = base.CreateWindow(activationState);
		window.Activated += WindowActivated;
		return window;
	}

	private async void WindowActivated(object sender, EventArgs e)
	{
#if WINDOWS
        var window = sender as Window;

        // change window size.
        window.Width = DefaultWidth;
        window.Height = DefaultHeight;

        window.MinimumHeight = DefaultHeight;
        window.MinimumWidth = DefaultWidth;

        window.MaximumHeight = DefaultHeight;
        window.MaximumWidth = DefaultWidth;

        // give it some time to complete window resizing task.
        await window.Dispatcher.DispatchAsync(() => { });

        var disp = DeviceDisplay.Current.MainDisplayInfo;

        // move to screen center
        window.X = (disp.Width / disp.Density - window.Width) / 2;
        window.Y = (disp.Height / disp.Density - window.Height) / 2;
#endif
	}
}