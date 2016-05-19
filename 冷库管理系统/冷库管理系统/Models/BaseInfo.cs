using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 冷库管理系统.Models
{
    public class BaseInfo : DbBase
    {
        public string Name { get; set; }

        public string Spell { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remark { get; set; }

    }
}
