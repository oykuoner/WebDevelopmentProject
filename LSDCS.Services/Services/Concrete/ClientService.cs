using AutoMapper;
using LSDCS.DataAccess.UnitOfWorks;
using LSDCS.Entity.DTOs.Clients;
using LSDCS.Entity.Entities;
using LSDCS.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Concrete
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<ClientDto>> GetAllClientsNoneDelete()
        {
            
            var clients = await _unitOfWork.GetRepository<Clients>().GetAllAsync();
            var mapper =  _mapper.Map<List<ClientDto>>(clients);

            return mapper;
        }
    }
}
