using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class FrmSever : Form
    {
        public FrmSever()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
           /* Form1 frm = new Form1();
            Ten.tensever = textBox1.Text;
            frm.Show();
            this.Hide();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                Form1 frm = new Form1();
                Ten.tensever = textBox1.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                errorProvider1.SetError(textBox1, "Ban chua nhap ten Sever!");
            }
        }

        private void FrmSever_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
