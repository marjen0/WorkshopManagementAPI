using Microsoft.EntityFrameworkCore;
using ServiceManagement.Database;
using ServiceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Repositories
{
    public class WorkshopRepository: GenericRepository<Workshop>, IWorkshopRepository
    {
        WorkshopContext _context;
        public WorkshopRepository(WorkshopContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Workshop>> GetWorkshopById(int id)
        {
            return await _context.Workshops.AsNoTracking().Where(w => w.ID == id).Include(w => w.Registrations).ToListAsync();
        }
    }
}
