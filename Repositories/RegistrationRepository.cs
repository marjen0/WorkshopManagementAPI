using Microsoft.EntityFrameworkCore;
using ServiceManagement.Database;
using ServiceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Repositories
{
    public class RegistrationRepository : GenericRepository<Registration>, IRegistrationRepository
    {
        private readonly WorkshopContext _context;
        public RegistrationRepository(WorkshopContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Registration>> GetRegistrations()
        {
            return await _context.Registrations
                .AsNoTracking()
                .Include(r => r.Vehicle)
                .Include(r => r.Workshop)
                .ToListAsync();
        }
    }
}
