using AutoMapper;
using LSDCS.Entity.DTOs.Clients;
using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LSDCS.Service.AutoMapper.Client
{
    public class ClientsProfile : Profile
    {
        public ClientsProfile()
        {
            CreateMap<ClientDto, Clients>().ReverseMap();
            
        }
    }

}
