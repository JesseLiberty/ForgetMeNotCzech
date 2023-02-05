using CommunityToolkit.Maui.Core.Views;

namespace ForgetMeNot.View;

public partial class Login : ContentPage
{
    public Login(LoginViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}