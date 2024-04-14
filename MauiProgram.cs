using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Storage;
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
            builder.Services.AddSingleton<IDataService>(new FirebaseDataService());
            builder.Services.AddSingleton<IStorageService>(new FirebaseStorageService());
            
            builder.Services.AddTransient<RegistroTareaPage>();
            builder.Services.AddTransient<RegistroTareaViewModel>();
            builder.Services.AddTransient<ToDoPage>();
            builder.Services.AddTransient<TodoviewModel>();

            builder.Services.AddTransient<RegistroEncuestaPage>();
            builder.Services.AddTransient<RegistroEncuestaViewModel>();

            builder.Services.AddTransient<FormEncuestaPage>();
            builder.Services.AddTransient<FormEncuestaViewModel>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<RegistroPage>();
            builder.Services.AddTransient<RegistroViewModel>();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIcons");
                });

            #if DEBUG
    		builder.Logging.AddDebug();
            #endif

            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyDmyQt9Z-gJI6O9bU6cfaJRyNt-yZejTCw",
                AuthDomain = "todolist-1e486.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            }));

            return builder.Build();
        }
    }
}
