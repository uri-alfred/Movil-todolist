using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using TodoList.Models;

namespace TodoList.Services
{
    public class FirebaseDataService : IDataService
    {
        public List<Tarea> Tasks { get; set; } = new();
        FirebaseClient firebaseClient;

        public FirebaseDataService()
        {
            firebaseClient = new FirebaseClient("https://todolist-1e486-default-rtdb.firebaseio.com/");
            firebaseClient
                .Child("Todo")
                .AsObservable<Tarea>()
                .Subscribe( item => Tasks.Add(item.Object));
        }

        public async Task AddTask(Tarea tarea)
        {
            var firebaseObject = await firebaseClient.Child("Todo").PostAsync(tarea);
            tarea.Id = firebaseObject.Key;
            // Actualizar el documento en Firebase con el ID generado
            await firebaseClient.Child("Todo").Child(tarea.Id).PutAsync(tarea);
        }

        public List<Tarea> GetTasks()
        {
            return Tasks;
        }

        public async Task<bool> DeleteTaskAsync(Tarea tarea)
        {
            try
            {
                await firebaseClient.Child("Todo").Child(tarea.Id).DeleteAsync();
                return true; // La eliminación fue exitosa
            }
            catch (Exception)
            {
                return false; // La eliminación falló
            }
        }
    }
}
