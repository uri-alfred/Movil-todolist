using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Services
{
    public interface IStorageService
    {
        Task<string> UploadFile(Stream fileStream, string fileName);
        Task DeleteFile(string fileName);
    }
}
