using ServiceManagement.Database;
using ServiceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Repositories
{
    public class MechanicRepository: GenericRepository<Mechanic>, IMechanicRrepository
    {
        private readonly WorkshopContext _context;

        public MechanicRepository(WorkshopContext context): base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        } 
    }
}
