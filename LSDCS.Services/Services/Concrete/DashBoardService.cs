using LSDCS.DataAccess.UnitOfWorks;
using LSDCS.Entity.Entities;
using LSDCS.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Concrete
{
    public class DashBoardService : IDashBoardService
    {

        private readonly IUnitOfWork _unitOfWork;

        public DashBoardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GetTotalMailLogCount()
        {

            var mailLogCount = await _unitOfWork.GetRepository<MailLog>().CountAsync();  

            return mailLogCount;
        }
    }
}
