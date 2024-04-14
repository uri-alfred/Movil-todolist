using TodoList.ViewModels;

namespace TodoList.Pages;

public partial class FormEncuestaPage : ContentPage
{
	public FormEncuestaPage(FormEncuestaViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}