using AutoMapper;
using LSDCS.Entity.DTOs.MailRelation;
using LSDCS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.AutoMapper.MailReletion
{
    public class MailReletionProfile : Profile
    {

        public MailReletionProfile()
        {
            CreateMap<MailRelation,MailRelationDto>().ReverseMap();
        }
    }
}
