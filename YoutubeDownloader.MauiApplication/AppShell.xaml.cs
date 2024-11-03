namespace YoutubeDownloader.MauiApplication
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(Views.MainPage.Name, typeof(Views.MainPage));
        }
    }
}
