using ServiceManagement.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceManagement.DTO
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
