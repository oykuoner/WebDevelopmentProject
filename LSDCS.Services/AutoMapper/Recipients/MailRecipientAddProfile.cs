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
    public class MailRecipientAddProfile : Profile
    {

        public MailRecipientAddProfile()
        {
            CreateMap<MailRecipientDto,MailRecipients>().ReverseMap();
        }
    }
}
