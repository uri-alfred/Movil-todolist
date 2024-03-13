using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Services
{
    public interface IDataService
    {
        public List<Tarea> Tasks { 
            get; set; 
        }
        public Task AddTask(Tarea tarea);
        public List<Tarea> GetTasks();
    }
}
