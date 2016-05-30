using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 冷库管理系统
{
    public class SigleAppContext
    {
        private static AppContext instance;
        private static object _lock = new object();

        private SigleAppContext()
        {

        }
        public static AppContext Instance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new AppContext();
                    }
                }
            }
            return instance;
        }
    }
}
