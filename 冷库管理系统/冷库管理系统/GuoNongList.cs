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
    }
}
