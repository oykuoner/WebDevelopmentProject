using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Abstractions
{
    public interface IMailRelationsService
    {
        Task AddMailRelationAsync(int parentMailId, int childMailId);

    }
}
