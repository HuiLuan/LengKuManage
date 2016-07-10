using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using 冷库管理系统.Models;
using static System.Char;

namespace 冷库管理系统
{
    public partial class OutStoreEdit : Form
    {
        public OutStoreEdit()
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
            if (cbxGuoNong.SelectedIndex <=0)
            {
                MessageBox.Show("果农不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                MessageBox.Show("重量不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("单价不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                MessageBox.Show("数量不能为空");
                return;
            }

            var db = SigleAppContext.Instance();
            {
                OutStore outstore;
                if (mId > 0)
                {
                    outstore = db.OutStores.Find(mId);
                }
                else
                {
                    outstore = new OutStore();
                }
                outstore.GuoNongId = Convert.ToInt64(cbxGuoNong.SelectedValue);
                outstore.BillDate = Convert.ToDateTime(dateTimePicker1.Text);
                outstore.GuiGeId = Convert.ToInt64(cbxGuiGe.SelectedValue);
                outstore.JiBieId = Convert.ToInt64(cbxJiBie.SelectedValue);

                outstore.Weight = Convert.ToDecimal(txtWeight.Text);
                if (!string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    outstore.Price = Convert.ToDecimal(txtPrice.Text);
                }
                if (!string.IsNullOrWhiteSpace(txtLengCangFei.Text))
                {
                    outstore.LengCangFei = Convert.ToDecimal(txtLengCangFei.Text);
                }
                if (!string.IsNullOrWhiteSpace(txtShiJiMoney.Text))
                {
                    outstore.ShiJiMoney = Convert.ToDecimal(txtShiJiMoney.Text);
                }

                outstore.Number = Convert.ToInt32(txtNumber.Text);
                outstore.Money = Convert.ToDecimal(txtMoney.Text);


                if (mId <= 0)
                {
                    db.OutStores.Add(outstore);
                }
                db.SaveChanges();
            }
            DialogResult = DialogResult.OK;
            MessageBox.Show("保存成功");
            Close();
        }
        private void SetMoney()
        {
            if (!string.IsNullOrWhiteSpace(txtWeight.Text) && !string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                decimal weight = decimal.Parse(txtWeight.Text.Trim());
                decimal price = decimal.Parse(txtPrice.Text.Trim());
                var money = weight * price;
                txtMoney.Text = Math.Round(money, 2).ToString();
            }
            else
            {
                txtMoney.Text = "0";
            }

            decimal lengcang = 0;
            if (decimal.TryParse(txtLengCangFei.Text, out lengcang))
            {

            }
            txtShiJiMoney.Text = (Convert.ToDecimal(txtMoney.Text) - lengcang).ToString();
        }
       
        private void OutStoreEdit_Load(object sender, EventArgs e)
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
                var outstore =
                    db.OutStores.Include(x => x.GuiGe)
                        .Include(x => x.GuoNong)
                        .Include(x => x.JiBie)
                        .FirstOrDefault(x => x.Id == mId);
                if (outstore != null)
                {
                    cbxGuoNong.Text = outstore.GuoNong.Name;
                    cbxGuoNong.SelectedValue = outstore.GuoNongId;

                    dateTimePicker1.Text = outstore.BillDate.ToString();

                    cbxGuiGe.Text = outstore.GuiGe.Name;
                    cbxGuiGe.SelectedValue = outstore.GuiGeId;

                    cbxJiBie.Text = outstore.JiBie.Name;
                    cbxJiBie.SelectedValue = outstore.JiBieId;

                    txtWeight.Text = outstore.Weight.ToString();
                    txtPrice.Text = outstore.Price.ToString();
                    txtNumber.Text = outstore.Number.ToString();
                    txtMoney.Text = outstore.Money.ToString();

                }
            }
        }
        private void InitDataSource()
        {
            DataSourceUtil.GuoNong(cbxGuoNong, true);
            DataSourceUtil.GuiGe(cbxGuiGe, true);
            DataSourceUtil.JiBie(cbxJiBie, true);
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            SetMoney();
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            SetMoney();
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
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

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            var fei =
                SigleAppContext.Instance().GlobalSets.FirstOrDefault(x => x.GlobalSetType == GlobalSetType.LengCangFei);
            if (fei != null)
            {
                txtLengCangFei.Text = (fei.DecimalValue*Convert.ToInt32(txtNumber.Text)).ToString();
            }
            else
            {
                txtLengCangFei.Text = "0";
            }
            SetMoney();
        }
    }
}
