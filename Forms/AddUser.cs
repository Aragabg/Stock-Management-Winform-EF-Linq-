using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StockManagement
{
    public partial class AddUser : Form
    {

        Model2 Ent = new Model2();
        public AddUser()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            
            
        }


        //useless
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm form = new MainForm();
            form.BackFromAny();
            form.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = new User();
            if (textBox1.Text != "" && textBox2.Text != "" && IsNumeric(textBox2.Text))
            {
                var ExsistingOrNot = Ent.Users.Where(a => a.UserName == (textBox1.Text) && a.Password == textBox2.Text).FirstOrDefault();
                if (ExsistingOrNot==null)
                {
                    user.UserName = textBox1.Text;
                    user.Password = textBox2.Text;
                    Ent.Users.Add(user);
                    Ent.SaveChanges();
                    textBox1.Text = textBox2.Text = "";
                    MessageBox.Show("تمت إضافة المستخدم بنجاح");
                }
                else
                {
                    MessageBox.Show("اسم المستخدم موجود بالفعل");

                }
            }
            else
            {
                MessageBox.Show($"أدخل بيانات المستخدم كاملة وصحيحة من فضلك\n" +
                                       "يرجى عدم ترك حقلاً فارغاً\n" +
                                "وتأكد من أن كلمة مرورك مكونة من أرقام فقط");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                var ExsistingOrNot = Ent.Users.Where(a => a.UserName == (textBox1.Text) && a.Password == textBox2.Text).FirstOrDefault();
                if (ExsistingOrNot != null)
                {
                    var beforeUser = textBox1.Text;
                    var beforePass= textBox2.Text;
                    var check = ExsistingOrNot;
                    Ent.Users.Remove(ExsistingOrNot);
                    Ent.SaveChanges();
                    MessageBox.Show("تم الحذف بنجاح");

                    var after = Ent.Users.Where(a => a.UserName == (beforeUser) && a.Password == beforePass ).FirstOrDefault();

                    if (after == null)
                    {
                        this.Hide();
                        login LoginAgain = new login();
                        LoginAgain.ShowDialog();
                        this.Close();
                    }
                    textBox1.Text = textBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("لا توجد بيانات مسجلة");

                }
            }
            else
            {
                MessageBox.Show("أدخل بيانات المستخدم كاملة من فضلك");
            }
        }
        private bool IsNumeric(string text)
        {
            return text.All(char.IsDigit);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
