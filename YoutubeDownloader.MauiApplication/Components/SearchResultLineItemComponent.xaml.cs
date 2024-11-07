namespace YoutubeDownloader.MauiApplication.Components;

public partial class SearchResultLineItemComponent : ContentView
{
    public static readonly BindableProperty SearchResultProperty = BindableProperty.Create(
        propertyName: nameof(SearchResult),
        returnType: typeof(SearchResult),
        declaringType: typeof(SearchResultLineItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

	public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create(
	   propertyName: nameof(SelectedCommand),
	   returnType: typeof(IRelayCommand),
	   declaringType: typeof(SearchResultLineItemComponent),
	   defaultValue: null,
	   defaultBindingMode: BindingMode.TwoWay);

	public SearchResult SearchResult
    {
        get => (SearchResult)GetValue(SearchResultProperty);
        set => SetValue(SearchResultProperty, value);
    }

	public IRelayCommand SelectedCommand
	{
		get => (IRelayCommand)GetValue(SelectedCommandProperty);
		set => SetValue(SelectedCommandProperty, value);
	}

	public SearchResultLineItemComponent()
	{
		InitializeComponent();
	}
}