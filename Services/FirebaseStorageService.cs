using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;

namespace TodoList.Services
{
    public class FirebaseStorageService : IStorageService
    {
        FirebaseStorage storage;
        public FirebaseStorageService() 
        {
            storage = new FirebaseStorage("todolist-1e486.appspot.com", new FirebaseStorageOptions
            {
                ThrowOnCancel = true
            }) ;
        }

        public async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            // Sube el archivo a Firebase Storage.
            var task = storage.Child("tareas").Child(fileName).PutAsync(fileStream);

            // Espera hasta que se complete la carga del archivo.
            await task;

            // Obtiene la URL de descarga del archivo cargado.
            var downloadUrl = await task;
            return downloadUrl;
        }

        public async Task DeleteFile(string fileName)
        {
            // Elimina el archivo del almacenamiento de Firebase.
            await storage.Child("tareas").Child(fileName).DeleteAsync();
        }
    }
}
