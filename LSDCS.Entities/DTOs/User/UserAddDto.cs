using LSDCS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Entity.DTOs.User
{
    public class UserAddDto
    {


        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public  int RoleId { get; set; }
        public string Password { get; set; }
        public List<AppRole> Roles { get; set; }
    }
}
