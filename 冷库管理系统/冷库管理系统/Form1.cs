﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 冷库管理系统
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            f.Show();
        }
    }
}
