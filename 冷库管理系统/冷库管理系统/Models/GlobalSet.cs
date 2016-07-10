using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 冷库管理系统.Models
{
    public class GlobalSet: DbBase
    {
        public GlobalSetType GlobalSetType { get; set; }

        public decimal DecimalValue { get; set; }
    }

    public enum GlobalSetType
    {
        LengCangFei=1,
    }
}
