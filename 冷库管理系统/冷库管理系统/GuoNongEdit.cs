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

            var db = SigleAppContext.Instance();
            {
                if (mId > 0)
                {
                    var guoNong = db.GuoNongs.Find(mId);
                    //db.GuoNongs.Attach(guoNong);
                    guoNong.Name = textBox1.Text.Trim();
                    guoNong.Spell = HHPinYin.GetInitials(guoNong.Name);
                    guoNong.Remark = textBox2.Text.Trim();

                }
                else
                {
                    var guoNong = new GuoNong();
                    guoNong.Name = textBox1.Text.Trim();
                    guoNong.Spell = HHPinYin.GetInitials(guoNong.Name);
                    guoNong.CreateTime = DateTime.Now;
                    guoNong.Remark = textBox2.Text.Trim();
                    db.GuoNongs.Add(guoNong);
                }
                db.SaveChanges();
            }
            DialogResult=DialogResult.OK;
            Close();
        }

        private void GuoNongEdit_Load(object sender, EventArgs e)
        {
            if (mId > 0)
            {
                var db = SigleAppContext.Instance();
                {
                    var gn = db.GuoNongs.Find(mId);
                    textBox1.Text = gn.Name;
                    textBox2.Text = gn.Remark;
                }
            }
        }
    }
}
