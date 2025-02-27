using AutoMapper;
using LSDCS.Entity.DTOs.MailRecipient;
using LSDCS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.AutoMapper.Recipients
{
    public class MailRecipientProfile : Profile
    {
        public MailRecipientProfile()
        {
            CreateMap<MailRecipients, MailRecipientDto>()
            .ForMember(dest => dest.RecipientEmail, opt => opt.MapFrom(src => src.Recipients.ALICI_MAIL));


        }

    }
}
