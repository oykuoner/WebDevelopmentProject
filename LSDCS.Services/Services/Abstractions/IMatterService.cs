using LSDCS.Entity.DTOs.Clients;
using LSDCS.Entity.DTOs.Matter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Abstractions
{
    public interface IMatterService
    {

        public Task<List<MatterDto>> GetAllMattersNoneDelete();
    }
}
