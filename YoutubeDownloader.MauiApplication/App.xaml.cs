namespace YoutubeDownloader.MauiApplication;

public partial class App : Application
{
	private const int DefaultWidth = 500;
	private const int DefaultHeight = 1000;

    private readonly IDbContextService<SettingsModel> dbSettingsContext;

    public App(IDbContextService<SettingsModel> dbSettingsContext)
    {
        this.dbSettingsContext = dbSettingsContext;

        InitializeComponent();
        MainPage = new AppShell();
    }

    protected override async void OnStart()
    {
        base.OnStart();

        var getSettingsTask = CreateIfNotExistsSttingsAsync();

        await getSettingsTask.ContinueWith((task) =>
        {
            MainThread.BeginInvokeOnMainThread(() => MainPage = new AppShell());
        });


    }

    protected override Window CreateWindow(IActivationState activationState)
	{
		Window window = base.CreateWindow(activationState);
		window.Activated += WindowActivatedAsync;
		return window;
	}

    private async Task CreateIfNotExistsSttingsAsync()
    {
        var settings = await dbSettingsContext.GetAsync(DatabeseKeys.Settings);

        if (settings is not null)
        {
            return;
        }

        FolderPickerResult result = null;

        do
        {
            result = await FolderPicker.Default.PickAsync();
            if (!result.IsSuccessful)
            {
                await Toast.Make($"The folder was not picked").Show();
                await Task.Delay(1000);
            }
            else
            {
                await dbSettingsContext.CreateOrUpdateIfExistsAsync(new SettingsModel(result));
            }
        }
        while (!result.IsSuccessful);
    }

    private async void WindowActivatedAsync(object sender, EventArgs e)
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