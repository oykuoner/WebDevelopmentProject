using LSDCS.Entity.DTOs.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Abstractions
{
    public interface IClientService
    {

        public Task<List<ClientDto>> GetAllClientsNoneDelete();
    }
}
