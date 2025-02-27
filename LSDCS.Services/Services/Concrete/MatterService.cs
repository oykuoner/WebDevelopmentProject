    using AutoMapper;
using LSDCS.DataAccess.UnitOfWorks;
using LSDCS.Entity.DTOs.Clients;
using LSDCS.Entity.DTOs.Matter;
using LSDCS.Entity.Entities;
using LSDCS.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Concrete
{
    public class MatterService : IMatterService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MatterService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MatterDto>> GetAllMattersNoneDelete()
        {
            var clients = await _unitOfWork.GetRepository<Matter>().GetAllAsync();
            var mapper = _mapper.Map<List<MatterDto>>(clients);

            return mapper;

        }

       



    }
}
