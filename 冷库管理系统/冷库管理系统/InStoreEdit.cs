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
using static System.Char;

namespace 冷库管理系统
{
    public partial class InStoreEdit : Form
    {
        private long mId;

        public void Init(long id)
        {
            mId = id;
        }

        public InStoreEdit()
        {
            InitializeComponent();
        }

        private void InStoreEdit_Load(object sender, EventArgs e)
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
                var inStore =
                    db.InStores.Include(x => x.GuiGe)
                        .Include(x => x.GuoNong)
                        .Include(x => x.JiBie)
                        .FirstOrDefault(x => x.Id == mId);
                if (inStore != null)
                {
                    cbxGuoNong.Text = inStore.GuoNong.Name;
                    cbxGuoNong.SelectedValue = inStore.GuoNongId;

                    dateTimePicker1.Text = inStore.BillDate.ToString();

                    cbxGuiGe.Text = inStore.GuiGe.Name;
                    cbxGuiGe.SelectedValue = inStore.GuiGeId;

                    cbxJiBie.Text = inStore.JiBie.Name;
                    cbxJiBie.SelectedValue = inStore.JiBieId;

                    txtWeight.Text = inStore.Weight.ToString();
                    txtPrice.Text = inStore.Price.ToString();
                    txtNumber.Text = inStore.Number.ToString();
                    txtMoney.Text = inStore.Money.ToString();

                }
            }
        }

        private void InitDataSource()
        {
            DataSourceUtil.GuoNong(cbxGuoNong,true);
            DataSourceUtil.GuiGe(cbxGuiGe,true);
            DataSourceUtil.JiBie(cbxJiBie,true);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (cbxGuoNong.SelectedIndex <= 0)
            {
                MessageBox.Show("果农不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                MessageBox.Show("重量不能为空");
                return;
            }
         
            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                MessageBox.Show("数量不能为空");
                return;
            }


            var db = SigleAppContext.Instance();
            {
                InStore instore;
                if (mId > 0)
                {
                     instore = db.InStores.Find(mId);
                }
                else
                {
                    instore =new InStore();
                }
                instore.GuoNongId =Convert.ToInt64(cbxGuoNong.SelectedValue);
                instore.BillDate = Convert.ToDateTime(dateTimePicker1.Text);
                instore.GuiGeId= Convert.ToInt64(cbxGuiGe.SelectedValue);
                instore.JiBieId= Convert.ToInt64(cbxJiBie.SelectedValue);

                instore.Weight = Convert.ToDecimal(txtWeight.Text);
                if (!string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    instore.Price = Convert.ToDecimal(txtPrice.Text);
                }

                instore.Number = Convert.ToInt32(txtNumber.Text);

                if (!string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    instore.Money = Convert.ToDecimal(txtMoney.Text);
                }
                

                if (mId <= 0)
                {
                    db.InStores.Add(instore);
                }
                db.SaveChanges();
            }
            DialogResult=DialogResult.OK;
            MessageBox.Show("保存成功");
            Close();
        }

        private void SetMoney()
        {
            if (!string.IsNullOrWhiteSpace(txtWeight.Text) && !string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                decimal weight = decimal.Parse(txtWeight.Text.Trim());
                decimal price = decimal.Parse(txtPrice.Text.Trim());
                var money = weight*price;
                txtMoney.Text = Math.Round(money, 2).ToString();
            }
            else
            {
                txtMoney.Text = "0";
            }
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsNumber(e.KeyChar) && e.KeyChar != (char) 8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        private void txtMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            SetMoney();
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            SetMoney();
        }
    }
}
