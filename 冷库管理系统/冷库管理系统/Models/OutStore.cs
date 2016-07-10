using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 冷库管理系统.Models
{
    public class OutStore : StoreBase
    {
        //冷藏费（元） 
        public decimal? LengCangFei { get; set; }

        //实际金额（元）
        public decimal? ShiJiMoney { get; set; }
    }
}
