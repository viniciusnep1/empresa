using System;
using System.Collections.Generic;
using System.Text;

namespace core.utils
{
    public class PaginateViewModel
    {
        public int Page { get; set; }
        public string Parametro{ get; set; }
        public int PageSize { get; set; }
        public Guid FazendaId { get; set; }
    }
}
