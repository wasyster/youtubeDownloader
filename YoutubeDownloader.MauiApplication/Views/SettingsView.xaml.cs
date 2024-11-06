namespace YoutubeDownloader.MauiApplication.Views;

public partial class SettingsView : ContentPage
{
	public SettingsViewModel ViewModel => this.BindingContext as SettingsViewModel;

	public SettingsView(SettingsViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}