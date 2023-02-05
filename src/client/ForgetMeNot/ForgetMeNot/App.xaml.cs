namespace ForgetMeNot;

public partial class App : Application
{

  public App(LoginViewModel loginViewModel)
  {
    InitializeComponent();

    //MainPage = new AppShell();

    MainPage = new Login(loginViewModel);

  }




}

