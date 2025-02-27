using AutoMapper;
using LSDCS.DataAccess.UnitOfWorks;
using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Entity.DTOs.MailRelation;
using LSDCS.Entity.Entities;
using LSDCS.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Concrete
{
    public class MailRelationsService : IMailRelationsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MailRelationsService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task AddMailRelationAsync(int parentMailId, int childMailId)
        {


            var mailRelationDto = new MailRelationDto
            {
                ParentMailLogID = parentMailId,
                ChildMailLogID = childMailId
            };


            var mailRelation = _mapper.Map<MailRelation>(mailRelationDto);

            await _uow.GetRepository<MailRelation>().AddAsync(mailRelation);
            await _uow.SaveAsync();
        }
    }
}
