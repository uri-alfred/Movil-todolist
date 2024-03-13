using TodoList.ViewModels;

namespace TodoList.Pages;

public partial class RegistroPage : ContentPage
{
	public RegistroPage(RegistroViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}