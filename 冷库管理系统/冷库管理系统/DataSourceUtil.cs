using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 冷库管理系统.Models;

namespace 冷库管理系统
{
    public class DataSourceUtil
    {
        public static void GuoNong(ComboBox box, bool hasEmptyTop)
        {
            using (var db = new AppContext())
            {
                var list = new List<GuoNong>();
                if (hasEmptyTop)
                {
                    list.Add(new GuoNong() { Id = 0, Name = "" });
                }
                list.AddRange(db.GuoNongs.ToList());
                box.DataSource = list;
                box.ValueMember = "Id";
                box.DisplayMember = "Name";
            }
        }
        public static void GuiGe(ComboBox box, bool hasEmptyTop)
        {
            using (var db = new AppContext())
            {
                var list = new List<GuiGe>();
                if (hasEmptyTop)
                {
                    list.Add(new GuiGe() { Id = 0, Name = "" });
                }
                list.AddRange(db.GuiGes.ToList());
                box.DataSource = list;
                box.ValueMember = "Id";
                box.DisplayMember = "Name";
            }
        }
        public static void JiBie(ComboBox box, bool hasEmptyTop)
        {
            using (var db = new AppContext())
            {
                var list = new List<JiBie>();
                if (hasEmptyTop)
                {
                    list.Add(new JiBie() { Id = 0, Name = "" });
                }
                list.AddRange(db.JiBies.ToList());
                box.DataSource = list;
                box.ValueMember = "Id";
                box.DisplayMember = "Name";
            }
        }

    }
}
