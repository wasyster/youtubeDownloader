namespace MauiApplication.Models;

public class SearchModel : ObservableObject
{
    public ValidatableObject<string> VideoURL { get; private set; }

    public SearchModel()
    {
        this.VideoURL = new ValidatableObject<string>();

        AddValidations();
    }

    protected bool IsModelValid() => VideoURL.Validate();

    private void AddValidations()
    {
        VideoURL.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Required field."
        });
    }
}
