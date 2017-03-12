using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Windows.Forms;

namespace 冷库管理系统
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
        }

        private void 果农管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f=new GuoNongList();
            f.Show();
        }

        private void 规格管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new GuiGeList();
            f.Show();
        }

        private void 级别管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new JiBieList();
            f.Show();
        }

        private void 新增入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f=new InStoreEdit();
            f.ShowDialog();
        }

        private void 入库查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new InStoreList();
            f.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSourceUtil.GuoNong(cbxGuoNong, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindGridView();
        }
        private void BindGridView()
        {
            var db = SigleAppContext.Instance();
            {
                var list = db.OutStores.Include(x => x.GuoNong).Include(x => x.JiBie).Include(x => x.GuiGe);
                var gnid = Convert.ToInt32(cbxGuoNong.SelectedValue);
                var mindate = Convert.ToDateTime(dateTimePicker1.Text);
                var maxdate = Convert.ToDateTime(dateTimePicker2.Text);
                if (cbxGuoNong.SelectedIndex >0)
                {
                    list = list.Where(x => x.GuoNongId == gnid);
                }
                if (dateTimePicker1.Checked)
                {
                    list = list.Where(x => x.BillDate >= mindate);
                }
                if (dateTimePicker2.Checked)
                {
                    list = list.Where(x => x.BillDate <= maxdate);
                }
                if (!string.IsNullOrWhiteSpace(txtGuoNong.Text))
                {
                    list = list.Where(x => x.GuoNong.Name.Contains(txtGuoNong.Text));
                }
                var dataList = list.ToList();
                dataGridView1.DataSource = dataList;


                lblSumWeight.Text = dataList.Sum(x => x.Weight ?? 0).ToString();
                lblSumNumber.Text = dataList.Sum(x => x.Number ?? 0).ToString();
                lblSumMoney.Text = dataList.Sum(x => x.Money ?? 0).ToString();
                lblLengCangFei.Text = dataList.Sum(x => x.LengCangFei ?? 0).ToString();

                decimal shijiMoney = dataList.Sum(x => x.ShiJiMoney ?? 0);
                lblSumShiJiMoney.Text = shijiMoney.ToString();

                var zhiqians = db.ZhiQians.AsQueryable();
                if (cbxGuoNong.SelectedIndex > 0)
                {
                    zhiqians = zhiqians.Where(x => x.GuoNongId == gnid);
                }
                if (dateTimePicker1.Checked)
                {
                    zhiqians = zhiqians.Where(x => x.BillDate >= mindate);
                }
                if (dateTimePicker2.Checked)
                {
                    zhiqians = zhiqians.Where(x => x.BillDate <= maxdate);
                }
                var zhiQianList = zhiqians.ToList();

                var zhiQianMoney = zhiQianList.Sum(x => x.Money);
                lblZhiQuMoney.Text = zhiQianMoney.ToString();

                var lastPayMoeny = shijiMoney - zhiQianMoney;
                lblLastPayMoney.Text = lastPayMoeny.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new OutStoreEdit();
            if (f.ShowDialog() == DialogResult.OK)
            {
                BindGridView();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || !(sender is DataGridView))
                return;
            DataGridView view = (DataGridView)sender;
            if (view.Columns[e.ColumnIndex].DataPropertyName == "BillDate")
            {
                object originalValue = e.Value;
                e.Value = Convert.ToDateTime(originalValue).ToString("yyyy年MM月dd日");
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            var f = new OutStoreEdit();
            f.Init(Convert.ToInt64(id));
            if (f.ShowDialog() == DialogResult.OK)
            {
                BindGridView();
            }
        }

        private void 删除选中出库信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除选中行吗？", "删除", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (dataGridView1.SelectedRows.Count < 1)
                {
                    return;
                }
                var id = dataGridView1.SelectedRows[0].Cells[0].Value;
                var db = SigleAppContext.Instance();
                {
                    var dmo = db.OutStores.Find(id);
                    db.OutStores.Remove(dmo);
                    db.SaveChanges();
                }
                BindGridView();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SigleAppContext.Instance().Dispose();
        }

        private void 冷藏费ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f=new SetLengCanfFeiForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void 支钱管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new ZhiQianList();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("没有数据可打印");
                return;
            }
            string footstr = String.Format("总重量:{0} 总数量:{1} 总金额:{2} 冷藏费:{3} 实际金额:{4} 支取金额:{5} 支付金额:{6}",
                lblSumWeight.Text, lblSumNumber.Text, lblSumMoney.Text, lblLengCangFei.Text, lblSumShiJiMoney.Text,
                lblZhiQuMoney.Text, lblLastPayMoney.Text); 
            PrintDGV.InitFootStr(footstr);
            PrintDGV.Print_DataGridView(this.dataGridView1);
        }
    }
}
