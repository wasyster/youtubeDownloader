namespace YoutubeDownloader.MauiApplication.Components;

public partial class SearchResultLineItemComponent : ContentView
{
    public static readonly BindableProperty DownloadProperty = BindableProperty.Create(
        propertyName: nameof(Download),
        returnType: typeof(bool),
        declaringType: typeof(SearchResultLineItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

	public static readonly BindableProperty TitleProperty = BindableProperty.Create(
		propertyName: nameof(Title),
		returnType: typeof(string),
		declaringType: typeof(SearchResultLineItemComponent),
		defaultValue: null,
		defaultBindingMode: BindingMode.TwoWay);

	public static readonly BindableProperty ThumbnailProperty = BindableProperty.Create(
		propertyName: nameof(Thumbnail),
		returnType: typeof(string),
		declaringType: typeof(SearchResultLineItemComponent),
		defaultValue: null,
		defaultBindingMode: BindingMode.TwoWay);

	public static readonly BindableProperty OnlyAudioProperty = BindableProperty.Create(
		propertyName: nameof(OnlyAudio),
		returnType: typeof(bool),
		declaringType: typeof(SearchResultLineItemComponent),
		defaultValue: null,
		defaultBindingMode: BindingMode.TwoWay);

	public bool Download
    {
        get => (bool)GetValue(DownloadProperty);
        set => SetValue(DownloadProperty, value);
    }

    public string Title
    {
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}

	public string Thumbnail
	{
		get => (string)GetValue(ThumbnailProperty);
		set => SetValue(ThumbnailProperty, value);
	}

	public bool OnlyAudio
	{
		get => (bool)GetValue(OnlyAudioProperty);
		set => SetValue(OnlyAudioProperty, value);
	}


	public SearchResultLineItemComponent()
	{
		InitializeComponent();
	}
}