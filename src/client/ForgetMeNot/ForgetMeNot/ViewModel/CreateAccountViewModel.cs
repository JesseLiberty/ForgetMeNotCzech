using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using ForgetMeNot.Api.Dto;
using ForgetMeNot.Services;

namespace ForgetMeNot.ViewModel
{
    [ObservableObject]
    public partial class CreateAccountViewModel
    {
        private AccountService accountService;

        [ObservableProperty] private string name;
        [ObservableProperty] private string email;
        [ObservableProperty] private string password;
        [ObservableProperty] private string nameErrorMessage;
        [ObservableProperty] private string emailErrorMessage;
        [ObservableProperty] private string passwordErrorMessage;
        [ObservableProperty] private bool showNameErrorMessage;
        [ObservableProperty] private bool showEmailErrorMessage;
        [ObservableProperty] private bool showPasswordErrorMessage;
        [ObservableProperty] private bool enableButton;
        [ObservableProperty] private bool isValidName;
        [ObservableProperty] private bool isValidEmail;
        [ObservableProperty] private bool isValidPassword;

        public CreateAccountViewModel(AccountService accountService)
        {
            this.accountService = accountService;
        }


        [RelayCommand]
        async Task SignUp()
        {
            if (EnableButton)
            {
                AccountCreateRequest accountCreateRequest = new()
                {
                    Email = this.Email,
                    FullName = Name,
                    PlainPassword = Password
                };

                try
                {
                    await accountService.CreateAccount(accountCreateRequest);
                    await ShowSnackBar();
                    await Shell.Current.GoToAsync("..");
                }
                catch (Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert("Sign up failed",
                        "We were not able to create an account with that user name", "Ok");
                }
            }

        }

        private async Task ShowSnackBar()
        {
          CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
          var message = "Your account was created";
          var dismissalText = "Click Here to Close the SnackBar";
          TimeSpan duration = TimeSpan.FromSeconds(10);


          Action action = async () =>
            await Application.Current.MainPage.DisplayAlert("Sign up completed",
              "Your user has been created successfully", "Ok");


          var snackbarOptions = new SnackbarOptions
          {
            BackgroundColor = Colors.Red,
            TextColor = Colors.Yellow,
            ActionButtonTextColor = Colors.Black,
            CornerRadius = new CornerRadius(20),
            Font = Microsoft.Maui.Font.SystemFontOfSize(14),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14)
          };

          var snackbar = Snackbar.Make(
            message,
            action,
            dismissalText,
            duration,
            snackbarOptions);

          await snackbar.Show(cancellationTokenSource.Token);

        }


    [RelayCommand]
        public Task ValidateName()
        {
            if (!string.IsNullOrEmpty(Name) && Name.Length >= 2)
            {
                IsValidName = true;
                ShowNameErrorMessage = false;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;
            }
            else
            {
                NameErrorMessage = "*Please enter a name with at least two characters";
                IsValidName = false;
                ShowNameErrorMessage = true;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }


        string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        [RelayCommand]
        public async Task ValidateEmail()
        {
            if (!string.IsNullOrEmpty(Email) && Regex.IsMatch(Email, emailPattern))
            {
                IsValidEmail = true;
                ShowEmailErrorMessage = false;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;

            }
            else
            {
                EmailErrorMessage = "*Invalid email";
                IsValidEmail = false;
                ShowEmailErrorMessage = true;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;

            }

        }

        [RelayCommand]
        public Task ValidatePassword()
        {
            if (!string.IsNullOrEmpty(Password) && Password.Length >= 6)
            {
                IsValidPassword = true;
                ShowPasswordErrorMessage = false;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;

            }
            else
            {
                PasswordErrorMessage = "*Invalid password. Must be at least 6 characters";
                IsValidPassword = false;
                ShowPasswordErrorMessage = true;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;

            }

            return Task.CompletedTask;
        }


    }
}
