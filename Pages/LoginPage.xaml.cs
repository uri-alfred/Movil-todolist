using TodoList.ViewModels;

namespace TodoList.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ValidateToken();
    }

    private async void ValidateToken()
    {
        var token = Preferences.Get("AuthToken", "");
        if (!string.IsNullOrEmpty(token))
        {
            _ = Shell.Current.GoToAsync(nameof(ToDoPage));
        }
    }
}