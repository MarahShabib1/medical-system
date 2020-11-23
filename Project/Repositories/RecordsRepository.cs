using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories
{


    public class RecordsRepository : IRecordsRepository
    {
        private readonly DataContext _context;


        public RecordsRepository(DataContext context)
        {
            _context = context;
        }

     public Records Create_Record(Records model)
        {
            _context.records.Add(model);
            return model;
        }

        public async Task<Records> Get_Record(int Recordid)
        {
            var record = await _context.records.Where(o => o.id == Recordid).Include(o=>o.prescription).FirstOrDefaultAsync();
            return record;
        }



    }
}
