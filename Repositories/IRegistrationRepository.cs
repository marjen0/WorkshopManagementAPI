using ServiceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Repositories
{
    public interface IRegistrationRepository: IGenericRepository<Registration>
    {
        public Task<IEnumerable<Registration>> GetRegistrations();
    }
}
