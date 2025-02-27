using AutoMapper;
using LSDCS.Entity.DTOs.Matter;
using LSDCS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.AutoMapper.Matters
{
    public class MatterProfile:Profile
    {
        public MatterProfile()
        {
            CreateMap<MatterDto,Matter>().ReverseMap();
        }

    }
}
