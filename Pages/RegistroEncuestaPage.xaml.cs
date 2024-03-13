using TodoList.ViewModels;

namespace TodoList.Pages;

public partial class RegistroEncuestaPage : ContentPage
{
	public RegistroEncuestaPage(RegistroEncuestaViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}