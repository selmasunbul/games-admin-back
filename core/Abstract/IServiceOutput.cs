using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Abstract
{
    public interface IServiceOutput
    {
        public int Code { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; }
        public int? RowCount { get; set; }
        public int? TotalCount { get; set; }
        public int? PageCount { get; }
        public object? Data { get; set; }
    }
}
