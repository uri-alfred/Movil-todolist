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
        }

        public async Task AddTask(Tarea tarea)
        {
            var firebaseObject = await firebaseClient.Child("Todo").PostAsync(tarea);
            tarea.Id = firebaseObject.Key;
        }

        public async Task<List<Tarea>> GetTasks()
        {
            return (await firebaseClient.Child("Todo").OnceAsync<Tarea>()).Select(item => new Tarea
            {
                Id = item.Key,
                Titulo = item.Object.Titulo,
                Descripcion = item.Object.Descripcion,
                FechaInicial = item.Object.FechaInicial,
                FechaFinal = item.Object.FechaFinal,
                TipoTarea = item.Object.TipoTarea,
                Prioridad = item.Object.Prioridad,
                Estado = item.Object.Estado,
                Encuesta = item.Object.Encuesta,
                URL = item.Object.URL,
            }).ToList();
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

        public async Task<bool> EditTaskAsync(Tarea tarea)
        {
            try
            {
                await firebaseClient.Child("Todo").Child(tarea.Id).PutAsync(tarea);
                return true; // La edición fue exitosa
            }
            catch (Exception)
            {
                return false; // La edición falló
            }
        }
    }
}
