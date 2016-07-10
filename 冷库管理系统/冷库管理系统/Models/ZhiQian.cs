using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 冷库管理系统.Models
{
    public class ZhiQian:DbBase
    {
        
        public DateTime BillDate { get; set; }

        public long GuoNongId { get; set; }

        public virtual GuoNong GuoNong { get; set; }

        public decimal Money { get; set; }

        public string Remark { get; set; }
    }
}
