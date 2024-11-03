namespace YoutubeDownloader.MauiApplication.Components;

public partial class SearchResultLineItemComponent : ContentView
{
    public static readonly BindableProperty SearchResultProperty = BindableProperty.Create(
        propertyName: nameof(SearchResult),
        returnType: typeof(SearchResult),
        declaringType: typeof(SearchResultLineItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

    public SearchResult SearchResult
    {
        get => (SearchResult)GetValue(SearchResultProperty);
        set => SetValue(SearchResultProperty, value);
    }

    public SearchResultLineItemComponent()
	{
		InitializeComponent();
	}
}