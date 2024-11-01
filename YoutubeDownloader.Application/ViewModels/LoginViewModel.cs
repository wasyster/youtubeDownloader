namespace MauiApplication.ViewModels;

public class LoginViewModel
{
    public ValidatableObject<string> Email { get; private set; }
    public ValidatableObject<string> Password { get; private set; }


    public IAsyncRelayCommand LoginAsyncCommand => new AsyncRelayCommand(LoginAsync);
    public IRelayCommand ValidateEmailCommand => new RelayCommand(() => Email.Validate());
    public IRelayCommand ValidatePasswordCommand => new RelayCommand(() => Password.Validate());


    public LoginViewModel()
    {
        this.Email = new ValidatableObject<string>();
        this.Password = new ValidatableObject<string>();
        AddValidations();
    }
    
    private async Task LoginAsync()
    {
        var isFormValid = Validate();

        if(!isFormValid)
        {
            return;
        }
    }

    private void AddValidations()
    {
        Email.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Email is required field."
        });

        Password.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Password is required field."
        });
    }

    private bool Validate()
    {
        bool isValidUser = ValidateEmail();
        bool isValidPassword = ValidatePassword();
        return isValidUser && isValidPassword;
    }

    private bool ValidateEmail()
    {
        return Email.Validate();
    }

    private bool ValidatePassword()
    {
        return Password.Validate();
    }
}
