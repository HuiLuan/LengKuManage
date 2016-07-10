using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 冷库管理系统.Models;

namespace 冷库管理系统
{
    public partial class SetLengCanfFeiForm : Form
    {
        public SetLengCanfFeiForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fei =
                   SigleAppContext.Instance().GlobalSets.FirstOrDefault(x => x.GlobalSetType == GlobalSetType.LengCangFei);
            if (fei == null)
            {
                fei=new GlobalSet();
                fei.GlobalSetType=GlobalSetType.LengCangFei;
                fei.DecimalValue = Convert.ToDecimal(textBox1.Text);
                SigleAppContext.Instance().GlobalSets.Add(fei);
            }
            else
            {
                SigleAppContext.Instance().GlobalSets.Attach(fei);
                fei.DecimalValue = Convert.ToDecimal(textBox1.Text);
            }
            SigleAppContext.Instance().SaveChanges();
            MessageBox.Show("保存成功");
            DialogResult=DialogResult.OK;
            Close();
        }

        private void SetLengCanfFeiForm_Load(object sender, EventArgs e)
        {
            var fei =
                SigleAppContext.Instance().GlobalSets.FirstOrDefault(x => x.GlobalSetType == GlobalSetType.LengCangFei);
            if (fei != null)
            {
                textBox1.Text = fei.DecimalValue.ToString();
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }
    }
}
