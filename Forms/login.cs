using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagement
{
    public partial class login : Form
    {
        Model2 Ent = new Model2();
        public login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            

        }
        //useless
        private void label1_Click(object sender, EventArgs e)
        {

        }
        //useless
        private void label2_Click(object sender, EventArgs e)
        {

        }
        //Enter Button Imp
        private void button1_Click(object sender, EventArgs e)
        {

            var user = Ent.Users.Where(a=>a.UserName == (textBox1.Text) && a.Password == textBox2.Text).FirstOrDefault();

                if (textBox2.Text !="" && textBox1.Text !="")
                {
                    if (IsNumeric(textBox2.Text))
                    {
                        if (user != null)
                        {

                            this.Hide();
                            MainForm form = new MainForm();
                            form.TransferUsername(textBox1.Text);
                            form.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("اسم المستخدم او كلمة السر خاطئة");
                        }
                    }
                    else
                    {
                    MessageBox.Show("كلمة السر خاطئة");
                    }
            }
                else
                {
                    MessageBox.Show("ادخل اسم المستخدم وكلمة السر من فضلك");
                }

        }
        //useless
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        //Delete button for deleting the textboxes
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = "";
        }
        //making textbox typing like passwords
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '●';
        }

        private bool IsNumeric(string text)
        {
            return text.All(char.IsDigit);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
