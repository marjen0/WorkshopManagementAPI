﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.Models
{
    public class Registration
    {
        public int ID { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime DateOfRepair { get; set; }
        public Workshop Workshop { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
