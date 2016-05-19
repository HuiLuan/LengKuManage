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
    public partial class JiBieEdit : Form
    {
        public JiBieEdit()
        {
            InitializeComponent();
        }

        private long mId;

        public void Init(long id)
        {
            mId = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new AppContext())
            {
                if (mId > 0)
                {
                    var dmo = db.JiBies.Find(mId);

                    dmo.Name = textBox1.Text.Trim();
                    dmo.Spell = HHPinYin.GetInitials(dmo.Name);
                    dmo.Remark = textBox2.Text.Trim();

                }
                else
                {
                    var dmo = new JiBie();
                    dmo.Name = textBox1.Text.Trim();
                    dmo.Spell = HHPinYin.GetInitials(dmo.Name);
                    dmo.CreateTime = DateTime.Now;
                    dmo.Remark = textBox2.Text.Trim();
                    db.JiBies.Add(dmo);
                }
                db.SaveChanges();
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void JiBieEdit_Load(object sender, EventArgs e)
        {
            if (mId > 0)
            {
                using (var db = new AppContext())
                {
                    var gn = db.JiBies.Find(mId);
                    textBox1.Text = gn.Name;
                    textBox2.Text = gn.Remark;
                }
            }
        }
    }
}
