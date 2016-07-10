using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 冷库管理系统
{
    public partial class ZhiQianList : Form
    {
        public ZhiQianList()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
        }

        private void ZhiQianList_Load(object sender, EventArgs e)
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
                var list = db.ZhiQians.Include(x => x.GuoNong);
                var gnid = Convert.ToInt32(cbxGuoNong.SelectedValue);
                var mindate = Convert.ToDateTime(dateTimePicker1.Text);
                var maxdate = Convert.ToDateTime(dateTimePicker2.Text);
                if (cbxGuoNong.SelectedIndex > 0)
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
                var dataList = list.ToList();
                dataGridView1.DataSource = dataList;

                lblSumMoney.Text = dataList.Sum(x => x.Money ).ToString();
            }

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            var f = new ZhiQianEdit();
            f.Init(Convert.ToInt64(id));
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

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new ZhiQianEdit();
            if (f.ShowDialog() == DialogResult.OK)
            {
                BindGridView();
            }
        }

        private void deleteSelectItem_Click(object sender, EventArgs e)
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
                    var dmo = db.ZhiQians.Find(id);
                    db.ZhiQians.Remove(dmo);
                    db.SaveChanges();
                }
                BindGridView();
            }
        }
    }
}
