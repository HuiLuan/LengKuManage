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
    public partial class GuoNongList : Form
    {
        public GuoNongList()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void GuoNongList_Load(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {
            using (var db=new AppContext())
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    dataGridView1.DataSource =
                        db.GuoNongs.Where(
                            x => x.Name.Contains(textBox1.Text.Trim()) || x.Spell.Contains(textBox1.Text.Trim())).OrderBy(x=>x.Name).ToList();
                }
                else
                {
                    dataGridView1.DataSource = db.GuoNongs.OrderBy(x=>x.Name).ToList();
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f=new GuoNongEdit();
            if (f.ShowDialog() == DialogResult.OK)
            {
                BindGridView();
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            var f = new GuoNongEdit();
            f.Init(Convert.ToInt64(id));
            if (f.ShowDialog() == DialogResult.OK)
            {
                BindGridView();
            }
        }

        private void 删除选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除选中行吗？", "删除", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (dataGridView1.SelectedRows.Count < 1)
                {
                    return;
                }
                var id = dataGridView1.SelectedRows[0].Cells[0].Value;
                using (var db=new AppContext())
                {
                    var gn = db.GuoNongs.Find(id);
                    db.GuoNongs.Remove(gn);
                    db.SaveChanges();
                }
                BindGridView();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BindGridView();
            }
        }
    }
}
