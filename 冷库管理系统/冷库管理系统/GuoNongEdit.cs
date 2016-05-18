using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 冷库管理系统.Models;
using 冷库管理系统.Utils.PinYinUtil;

namespace 冷库管理系统
{
    public partial class GuoNongEdit : Form
    {
        private long mId;

        public void Init(long id)
        {
            mId = id;
        }

        public GuoNongEdit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (var db = new AppContext())
            {
                if (mId > 0)
                {
                    var guoNong = db.GuoNongs.Find(mId);
                    //db.GuoNongs.Attach(guoNong);
                    guoNong.Name = textBox1.Text.Trim();
                    guoNong.Spell = HHPinYin.GetPinyin(guoNong.Name);

                }
                else
                {
                    var guoNong = new GuoNong();
                    guoNong.Name = textBox1.Text.Trim();
                    guoNong.Spell = HHPinYin.GetPinyin(guoNong.Name);
                    guoNong.CreateTime = DateTime.Now;
                    db.GuoNongs.Add(guoNong);
                }
                db.SaveChanges();
            }
        }

        private void GuoNongEdit_Load(object sender, EventArgs e)
        {
            if (mId > 0)
            {
                using (var db = new AppContext())
                {
                    var name = db.GuoNongs.Find(mId).Name;
                    textBox1.Text = name;
                }
            }
        }
    }
}
