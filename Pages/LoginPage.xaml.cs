using TodoList.ViewModels;

namespace TodoList.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}