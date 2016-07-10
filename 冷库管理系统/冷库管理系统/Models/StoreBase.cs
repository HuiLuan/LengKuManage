using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 冷库管理系统.Models
{
    public class StoreBase:DbBase
    {
        public DateTime BillDate { get; set; }

        public long GuoNongId { get; set; }

        public virtual  GuoNong GuoNong { get; set; }

        public long? GuiGeId { get; set; }

        public GuiGe GuiGe { get; set; }

        public long? JiBieId { get; set; }

        public JiBie JiBie { get; set; }

        //重量（公斤）
        public decimal? Weight { get; set; }

        //价格（元/公斤）
        public decimal? Price { get; set; }

        //数量（桶）
        public int? Number { get; set; }

        //金额（元）  =重量*价格
        public decimal? Money { get; set; }
      
    }
}
