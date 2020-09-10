using ServiceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceManagement.Database;

namespace ServiceManagement.Repositories
{
    public class OfferedServiceRepository : GenericRepository<OfferedService>, IOfferedServiceRepository
    {
        private readonly WorkshopContext _context;
        public OfferedServiceRepository(WorkshopContext context): base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
