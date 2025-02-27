using AutoMapper;
using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Entity.DTOs.MailRecipient;
using LSDCS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.AutoMapper.MailLogMapper
{
    public class MailLogProfile : Profile
    {
        public MailLogProfile()
        {


            //CreateMap<MailLog, MailLogListDto>()

            //        .ForMember(dest => dest.Matter, opt => opt.MapFrom(src => src.Matter))
            //        .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Clients))
            //        .ReverseMap()
            //        .ForMember(dest => dest.MailRecipients, opt => opt.Ignore());

            CreateMap<MailLog, MailLogListDto>()
          .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Clients))
          .ForMember(dest => dest.Matter, opt => opt.MapFrom(src => src.Matter))
          .ForMember(dest => dest.MailRecipients, opt => opt.MapFrom(src => src.MailRecipients.Where(x => !x.Deleted)));

            CreateMap<MailLogAddDto, MailLog>().ReverseMap();


            CreateMap<MailLogUpdateDto, MailLog>().ReverseMap();
            CreateMap<MailLogUpdateDto, MailLogListDto>().ReverseMap();

            CreateMap<MailDetailsDto, MailLogListDto>().ReverseMap();
            CreateMap<MailDetailsDto, MailLog>().ReverseMap();
          
        }

    }
}
