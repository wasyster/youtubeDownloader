namespace YoutubeDownloader.MauiApplication.Views;

public partial class FileDownloadView : ContentPage
{
    public static string Name => nameof(FileDownloadView);

	public FileDownloadViewModel ViewModel => this.BindingContext as FileDownloadViewModel;

    public FileDownloadView(FileDownloadViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}