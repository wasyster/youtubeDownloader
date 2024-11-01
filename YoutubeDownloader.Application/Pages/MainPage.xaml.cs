namespace MauiApplication;

public partial class MainPage : ContentPage
{
    public static string Name => nameof(MainPage);

    public MainPageViewModel ViewModel => this.BindingContext as MainPageViewModel;

    public MainPage(MainPageViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}
