using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class FileRepository : IFileRepository
    {

        private readonly DataContext _context;
       

        public FileRepository(DataContext context)
        {
          
            _context = context;
        }
        public async void CreateFile(IFormFile file)
        {
            var filesPath = Directory.GetCurrentDirectory();
            //  var fileName = Path.GetFileName(file.FileName);
            filesPath = filesPath + "/" + file.FileName;
            //    var filePath = Path.Combine(filesPath, fileName);
            if (!System.IO.File.Exists(filesPath))
            {
                using (var stream = new FileStream(filesPath, FileMode.CreateNew))
                {
                    await file.CopyToAsync(stream);
                }

            }
        }

        public List<Names> read(string name)
        {

            var list = File.ReadAllLines(name).Skip(1).Where(l => l.Length > 1).Select(parsefromcsv).ToList();

            return list;

        }

        public Names parsefromcsv(string line)
        {
            var col = line.Split(',');
            return new Names
            {

                companyName = (col[1]),
                medicineName = (col[0]),

            };

        }

       public async Task<int> Add(List<Names> list)
        {
            foreach (var icon in list)
            {
                var find_company = await _context.company.Where(o => o.Name == icon.companyName).FirstOrDefaultAsync();

                if (find_company == null)
                {
                    find_company = new Company { Name = icon.companyName };
                    _context.company.Add(find_company);
                }

                var find_medicine = await _context.medicine.Where(o => o.Name == icon.medicineName).FirstOrDefaultAsync();
                if (find_medicine == null)
                {

                    var medicine = new Medicine { Name = icon.medicineName, company = find_company };
                    _context.medicine.Add(medicine);
                }
            }
            return 0;
        }



    }
}
