using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace core.utils
{
    public class ViewModelBase
    {
        public IQueryable<dynamic> Items { get; set; }
        public int Page { get; set; }
        public int Total { get; set; }
    }
}
