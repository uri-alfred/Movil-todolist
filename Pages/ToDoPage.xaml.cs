using TodoList.ViewModels;

namespace TodoList.Pages;

public partial class ToDoPage : ContentPage
{
	public ToDoPage(TodoviewModel mv)
	{
		InitializeComponent();
		BindingContext = mv;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		TodoviewModel mviewModel = ((TodoviewModel)BindingContext);
		if (mviewModel.AgregarTareasCommand.CanExecute(null))
		{
			mviewModel.AgregarTareasCommand.Execute(null);
		}
    }
}