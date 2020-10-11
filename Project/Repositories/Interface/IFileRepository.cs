using Microsoft.AspNetCore.Http;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
   public interface IFileRepository
    {
        void CreateFile(IFormFile file);
        List<Names> read(string name);
        Names parsefromcsv(string line);
        Task<int> Add(List<Names> list);
    }
}
