﻿using ServiceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Repositories
{
    public interface IWorkshopRepository: IGenericRepository<Workshop>
    {
        public Task<IEnumerable<Workshop>> GetWorkshopById(int id);
    }
}
