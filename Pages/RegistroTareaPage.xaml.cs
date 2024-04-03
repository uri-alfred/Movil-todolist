using TodoList.ViewModels;

namespace TodoList.Pages;

public partial class RegistroTareaPage : ContentPage
{
	public RegistroTareaPage(RegistroTareaViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();

        // si sale de la pantalla
        if (!string.IsNullOrEmpty(((RegistroTareaViewModel)BindingContext).fileName))
        {
            // Llamar al método para eliminar el archivo
            await((RegistroTareaViewModel)BindingContext).EliminarArchivo();
        }

    }
}