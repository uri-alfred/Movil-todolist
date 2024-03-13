using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Services
{
    public class FakeTaskService : IDataService
    {
        public List<Tarea> Tasks { get; set; }
        public FakeTaskService()
        {
            Tasks = [];
            for (int i = 1; i <= 10; i++)
            {
                Tasks.Add(new()
                {
                    Titulo = $"Tarea {i}",
                    Descripcion = $"Tarea autogenerada {i}"
                });
            }
        }
        public async Task AddTask(Tarea tarea)
        {
            Tasks.Add(tarea);
        }
        public List<Tarea> GetTasks()
        {
            return Tasks;
        }
    }
}
