namespace YoutubeDownloader.MauiApplication.Views;

public partial class MainPage : ContentPage
{
    public MainPageViewModel ViewModel => this.BindingContext as MainPageViewModel;

    public MainPage(MainPageViewModel viewModel)
	{
        this.BindingContext = viewModel;

        InitializeComponent();
	}
}