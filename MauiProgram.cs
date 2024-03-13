using Microsoft.Extensions.Logging;
using TodoList.Pages;
using TodoList.Services;
using TodoList.ViewModels;

namespace TodoList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            // inyeccion de dependencias // IDataService, 
            #if DEBUG
            builder.Services.AddSingleton<IDataService, FakeTaskService>();
            builder.Services.AddSingleton<IDataService, FirebaseDataService>();
            #endif
            builder.Services.AddTransient<RegistroTareaPage>();
            builder.Services.AddTransient<RegistroTareaViewModel>();
            builder.Services.AddTransient<ToDoPage>();
            builder.Services.AddTransient<TodoviewModel>();

            builder.Services.AddTransient<RegistroEncuestaPage>();
            builder.Services.AddTransient<RegistroEncuestaViewModel>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<RegistroPage>();
            builder.Services.AddTransient<RegistroViewModel>();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIcons");
                });

            #if DEBUG
    		builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}
