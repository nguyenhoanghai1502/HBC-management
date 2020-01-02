using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApp5
{

    public partial class Form1 : Form
    {
        public string tendatabase = "ThongTinSinh_Vien";
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source="+Ten.tensever+";Initial Catalog=QLSV;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from "+tendatabase;
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dtgvThongTin.DataSource = table;
        }
        void LoadData(string str)
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from "+str;
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dtgvThongTin.DataSource = table;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            pnlTrai.Enabled = pnlPhai.Enabled= true;
            txtHoten.Focus();            
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            button4.Enabled = true;
            button4.Visible = true;
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            pnlTrai.Enabled = pnlPhai.Enabled = true;
            txtHoten.Focus();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuuSua.Enabled = true;
            btnLuuSua.Visible = true;
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn xóa sinh viên này?","confirm", MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                //neu nguoi dung an co
                command = connection.CreateCommand();
                command.CommandText = "delete from "+tendatabase+" where convert(varchar(MAX),MSSV)='" + txtMSSV.Text + "'";
                command.ExecuteNonQuery();
                LoadData();
            }
        }

        private void TxtHoten_Leave(object sender, EventArgs e)
        {
               txtHoten.Text=VietHoa(txtHoten.Text);
        }
        public static string VietHoa(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            string result = "";

            string[] words = s.Split(' ');

            foreach (string word in words)
            { 
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    else
                        result += word.ToUpper() + " ";
                }

            }
            return result.Trim();
        }
        private bool KiemTraNgay(DateTime a)
        {
            if (System.DateTime.Today.Year - a.Year< 17||System.DateTime.Today.Year-a.Year>35)
                return true;
            else
                return false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadData();
        }

        private void DtgvThongTin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dtgvThongTin.CurrentRow.Index;
            txtHoten.Text = dtgvThongTin.Rows[i].Cells[0].Value.ToString();
            txtMSSV.Text = dtgvThongTin.Rows[i].Cells[1].Value.ToString();
            txtLop.Text = dtgvThongTin.Rows[i].Cells[2].Value.ToString();
            dtNS.Text = dtgvThongTin.Rows[i].Cells[3].Value.ToString();
            cbbMavung.Text = dtgvThongTin.Rows[i].Cells[5].Value.ToString();
            txtDT.Text = dtgvThongTin.Rows[i].Cells[6].Value.ToString();
            txtEmail.Text = dtgvThongTin.Rows[i].Cells[7].Value.ToString();
            if (radioButton1.Text == dtgvThongTin.Rows[i].Cells[4].Value.ToString())
            {
                radioButton1.Checked = true;
            }
            else if(radioButton2.Text == dtgvThongTin.Rows[i].Cells[4].Value.ToString())
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton3.Checked = true;
            }
        }

        private void BtnLuuThem_Click(object sender, EventArgs e)
        { 
           
        }

        private void BtnLuuSua_Click(object sender, EventArgs e)
        {
            string strthem;
            if (radioButton1.Checked)
                strthem = radioButton1.Text;
            else if (radioButton2.Checked)
                strthem = radioButton2.Text;
            else
                strthem = radioButton3.Text;
            command = connection.CreateCommand();
            command.CommandText = "set dateformat dmy update "+tendatabase+" set Hoten= N'" + txtHoten.Text + "',lop=N'" + txtLop.Text + "',Ngaysinh='" + dtNS.Value + "',gioitinh= N'" + strthem + "',mavung= '" + cbbMavung.Text + "',dienthoai= '" + txtDT.Text + "',email= '" + txtEmail.Text + "' where convert(varchar(MAX),MSSV)='" + txtMSSV.Text + "'";
            command.ExecuteNonQuery();
            LoadData();
            pnlTrai.Enabled = pnlPhai.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuuSua.Enabled = false;
            btnLuuSua.Visible = false;
        }
        private void TxtDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!Char.IsDigit(e.KeyChar)&&!Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn chưa lưu, bạn có muốn thoát?", "Hỏi thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                panel3.Visible = false;
                command = connection.CreateCommand();
                command.CommandText = "select * from " + tendatabase + " where MSSV=" + textBox1.Text;
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                dtgvThongTin.DataSource = table;
            }
            else
            {
                errorProvider2.SetError(textBox1, "Bạn chưa nhập tên bảng!");
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        { 
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from "+tendatabase;
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dtgvThongTin.DataSource = table;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            textBox2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                panel4.Visible = false;
                command = connection.CreateCommand();
                command.CommandText = "Create table " + textBox2.Text + " ( Hoten nchar(50), Mssv nchar(10), Lop nchar(20), Ngaysinh smalldatetime," +
                    "Gioitinh nchar(20), Mavung nchar(10), Dienthoai nchar(20), Email text)";
                command.ExecuteNonQuery();
                LoadData(textBox2.Text);
                tendatabase = textBox2.Text;
            }
            else
            {
                errorProvider1.SetError(textBox2, "Tên bảng không được để trống!");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            textBox3.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox3.Text))
            {
                panel5.Visible = false;
                LoadData(textBox3.Text);
                tendatabase = textBox3.Text;
            }
            else
            {
                errorProvider3.SetError(textBox3, "bạn chưa nhập tên bảng!");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void newToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
        }

        private void fileToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
        }

        private void searchToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoten.Text))
            {
                epHoten.Icon = Properties.Resources.iconfinder_sign_error_299045;
                epHoten.SetError(txtHoten, "Bạn chưa nhập họ tên!");

            }
            else
            {
                epHoten.Icon = Properties.Resources.iconfinder_sign_check_299110;
                epHoten.SetError(txtHoten, "Hợp lệ!");
                if (string.IsNullOrEmpty(txtMSSV.Text))
                {
                    epMSSV.Icon = Properties.Resources.iconfinder_sign_error_299045;
                    epMSSV.SetError(txtMSSV, "Bạn chưa nhập mã sinh viên!");
                }
                else
                {
                    epMSSV.Icon = Properties.Resources.iconfinder_sign_check_299110;
                    epMSSV.SetError(txtMSSV, "Hợp lệ!");
                    if (string.IsNullOrEmpty(txtLop.Text))
                    {
                        epLop.Icon = Properties.Resources.iconfinder_sign_error_299045;
                        epLop.SetError(txtLop, "Bạn chưa nhập lớp!");
                    }
                    else
                    {
                        epLop.Icon = Properties.Resources.iconfinder_sign_check_299110;
                        epLop.SetError(txtLop, "Hợp lệ!");
                        if (KiemTraNgay(dtNS.Value))
                        {
                            epNS.Icon = Properties.Resources.iconfinder_sign_error_299045;
                            epNS.SetError(dtNS, "Năm sinh không hợp lệ!");
                        }
                        else
                        {
                            epNS.Icon = Properties.Resources.iconfinder_sign_check_299110;
                            epNS.SetError(dtNS, "Hợp lệ!");
                            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
                            {
                                epGT.Icon = Properties.Resources.iconfinder_sign_error_299045;
                                epGT.SetError(radioButton3, "Bạn chưa chọn giới tính!");
                            }
                            else
                            {
                                epGT.Icon = Properties.Resources.iconfinder_sign_check_299110;
                                epGT.SetError(radioButton3, "Hợp lệ!");
                                if (string.IsNullOrEmpty(txtDT.Text) || string.IsNullOrEmpty(cbbMavung.Text))
                                {
                                    epSDT.Icon = Properties.Resources.iconfinder_sign_error_299045;
                                    epSDT.SetError(txtDT, "Số điện thoại không hợp lệ!");
                                }
                                else
                                {
                                    epSDT.Icon = Properties.Resources.iconfinder_sign_check_299110;
                                    epSDT.SetError(txtDT, "Hợp lệ!");
                                    if (string.IsNullOrEmpty(txtEmail.Text))
                                    {
                                        epEmail.Icon = Properties.Resources.iconfinder_sign_error_299045;
                                        epEmail.SetError(txtEmail, "Bạn chưa nhập email!");
                                    }
                                    else
                                    {
                                        epEmail.Icon = Properties.Resources.iconfinder_sign_check_299110;
                                        epEmail.SetError(txtEmail, "Hợp lệ!");
                                        string strthem;
                                        if (radioButton1.Checked)
                                            strthem = radioButton1.Text;
                                        else if (radioButton2.Checked)
                                            strthem = radioButton2.Text;
                                        else
                                            strthem = radioButton3.Text;
                                        command = connection.CreateCommand();
                                        command.CommandText = "set dateformat dmy insert into " + tendatabase + " values( N'" + txtHoten.Text + "', '" + txtMSSV.Text + "'," +
                                            "'" + txtLop.Text + "','" + dtNS.Value + "', N'" + strthem + "', '" + cbbMavung.Text + "', '" + txtDT.Text + "', '" + txtEmail.Text + "')";
                                        command.ExecuteNonQuery();
                                        LoadData();
                                        pnlTrai.Enabled = pnlPhai.Enabled = false;
                                        btnThem.Enabled = true;
                                        btnSua.Enabled = true;
                                        btnXoa.Enabled = true;
                                        button4.Enabled = false;
                                        button4.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }
    }
}
