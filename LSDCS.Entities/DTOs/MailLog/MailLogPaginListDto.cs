using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LSDCS.Entity.DTOs.MailLog
{
    public class MailLogPaginListDto
    {


        public IList<MailLogListDto>? MailLogs { get; set; }
        public int? ClientId { get; set; }
        //public List<string> ALICI_TO { get; set; } = new List<string>();
        //public List<string> ALICI_CC { get; set; } = new List<string>();
        public virtual int CurrentPage { get; set; } = 1;
        public virtual int PageSize { get; set; } = 3;
        public virtual int TotalCount { get; set; }
        public virtual int TotalPages =>  (int)Math.Ceiling(decimal.Divide(TotalCount,PageSize));

        public virtual bool ShowPrevious => CurrentPage > 1;

        public virtual bool ShowNext => CurrentPage < TotalPages;

        public virtual bool IsAscending { get; set; } = false;
    }
}
