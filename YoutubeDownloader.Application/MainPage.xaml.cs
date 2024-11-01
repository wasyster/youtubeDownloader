namespace MauiApplication;
public partial class MainPage : ContentPage
{
    public static string Name => nameof(MainPage);

    public LoginViewModel ViewModel => this.BindingContext as LoginViewModel;

    public MainPage(LoginViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}
