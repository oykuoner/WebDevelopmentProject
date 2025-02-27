using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Abstractions
{
    public interface IDocumentService
    {

        Task AddMailLogDocument(int mailLogId, List<int> documentsId);
        Task<List<int>> DocumentAdd(List<IFormFile> documentFiles);

        Task DocumentDelete(int documentId);


    }
}
