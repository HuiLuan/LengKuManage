using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 冷库管理系统.Models;

namespace 冷库管理系统
{
    public partial class ZhiQianEdit : Form
    {
        private long mId;

        public void Init(long id)
        {
            mId = id;
        }

        public ZhiQianEdit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbxGuoNong.SelectedIndex <= 0)
            {
                MessageBox.Show("果农不能为空");
                return;
            }
          

            if (string.IsNullOrWhiteSpace(txtMoney.Text))
            {
                MessageBox.Show("金额不能为空");
                return;
            }


            var db = SigleAppContext.Instance();
            {
                ZhiQian zhiQian;
                if (mId > 0)
                {
                    zhiQian = db.ZhiQians.Find(mId);
                    db.ZhiQians.Attach(zhiQian);
                }
                else
                {
                    zhiQian = new ZhiQian();
                }
                zhiQian.GuoNongId = Convert.ToInt64(cbxGuoNong.SelectedValue);
                zhiQian.BillDate = Convert.ToDateTime(dateTimePicker1.Text);
                zhiQian.Remark = textBox1.Text;

                if (!string.IsNullOrWhiteSpace(txtMoney.Text))
                {
                    zhiQian.Money = Convert.ToDecimal(txtMoney.Text);
                }

                if (mId <= 0)
                {
                    db.ZhiQians.Add(zhiQian);
                }
                db.SaveChanges();
            }
            DialogResult = DialogResult.OK;
            MessageBox.Show("保存成功");
            Close();
        }

        private void ZhiQianEdit_Load(object sender, EventArgs e)
        {
            InitDataSource();
            if (mId > 0)
            {
                AppToUi();
            }
        }
        private void AppToUi()
        {
            var db = SigleAppContext.Instance();
            {
                var zhiQian = db.ZhiQians.Include(x => x.GuoNong)
                        .FirstOrDefault(x => x.Id == mId);
                if (zhiQian != null)
                {
                    cbxGuoNong.Text = zhiQian.GuoNong.Name;
                    cbxGuoNong.SelectedValue = zhiQian.GuoNongId;

                    dateTimePicker1.Text = zhiQian.BillDate.ToString();
                    textBox1.Text = zhiQian.Remark;
                    txtMoney.Text = zhiQian.Money.ToString();

                }
            }
        }

        private void InitDataSource()
        {
            DataSourceUtil.GuoNong(cbxGuoNong, true);
        }

        private void txtMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }
    }
}
