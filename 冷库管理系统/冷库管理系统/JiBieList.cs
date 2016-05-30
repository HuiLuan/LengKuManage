using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 冷库管理系统
{
    public partial class JiBieList : Form
    {
        public JiBieList()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            var f = new JiBieEdit();
            f.Init(Convert.ToInt64(id));
            if (f.ShowDialog() == DialogResult.OK)
            {
                BindGridView();
            }
        }
        private void BindGridView()
        {
            var db = SigleAppContext.Instance();
            {
                dataGridView1.DataSource = db.JiBies.OrderByDescending(x => x.Name).ToList();
            }
        }
        private void JiBieList_Load(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new JiBieEdit();
            if (f.ShowDialog() == DialogResult.OK)
            {
                BindGridView();
            }
        }

        private void 删除选中项ToolStripMenuItem_Click(object sender, EventArgs e)
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
                    var dmo = db.JiBies.Find(id);
                    db.JiBies.Remove(dmo);
                    db.SaveChanges();
                }
                BindGridView();
            }
        }
    }
}
