using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using 冷库管理系统.Models;

namespace 冷库管理系统
{
    public class AppContext:DbContext
    {
        public AppContext() : base("AppContext")
        {

        }

        public DbSet<GuoNong> GuoNongs { get; set; }
        public DbSet<GuiGe> GuiGes { get; set; }
        public DbSet<JiBie> JiBies { get; set; }
        public DbSet<InStore> InStores { get; set; }
        public DbSet<OutStore> OutStores { get; set; }
        public DbSet<GlobalSet> GlobalSets { get; set; }
        public DbSet<ZhiQian> ZhiQians { get; set; }

    }
}
