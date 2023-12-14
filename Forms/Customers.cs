using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagement.Forms
{
    public partial class Customers : Form
    {
        Model2 Ent = new Model2();
        public Customers()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        public void emptyTextbox()
        {
            textBox1.Text = textBox2.Text
            = textBox3.Text = textBox4.Text 
            = textBox5.Text = textBox6.Text 
            = textBox7.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" 
                && textBox2.Text != "" 
                && textBox3.Text != "" 
                && textBox4.Text != ""
                && textBox5.Text != ""
                && textBox6.Text != ""
                && textBox7.Text != "")
            {

                var CusNum = int.Parse(textBox1.Text);
                var searchinIfExsist = from i in Ent.Customers where i.CustomerId == CusNum select i;

                if (searchinIfExsist.FirstOrDefault() == null)
                {
                    if (int.TryParse(textBox3.Text, out int phone) &&
                        int.TryParse(textBox4.Text, out int fax) &&
                        int.TryParse(textBox5.Text, out int mobile))
                    {
                            Customer newCus = new Customer();
                            newCus.CustomerId = CusNum;
                            newCus.CustomerName = textBox2.Text;
                            newCus.CustomerPhone = int.Parse(textBox3.Text);
                            newCus.CustomerFAX = int.Parse(textBox4.Text);
                            newCus.CustomerMobile = int.Parse(textBox5.Text);
                            newCus.CustomerMail = textBox6.Text;
                            newCus.CustomerWebsite = textBox7.Text;
                            Ent.Customers.Add(newCus);
                            Ent.SaveChanges();
                            comboBox1.Items.Add(CusNum);
                            emptyTextbox();
                            MessageBox.Show("تمت إضافة العميل بنجاح");
                    
                    }
                    else
                    {
                        MessageBox.Show("من فضلك أدخل بيانات الهاتف أوالمحمول أوالفاكس بالشكل الصحيح");

                    }
                }
                else
                {
                    MessageBox.Show("رقم العميل موجود بالفعل");
                }
            }
            else
            {
                MessageBox.Show("من فضلك ادخل جميع بيانات العميل كاملة");
            }
        }

        private void Customers_Load(object sender, EventArgs e)
        {
          //  comboBox1.Items.Clear();
            var ex = from i in Ent.Customers select i.CustomerId;
            foreach (var item in ex)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            emptyTextbox();
            var CusNum = int.Parse(comboBox1.Text);
            var ex = Ent.Customers.Where(a => a.CustomerId == CusNum).FirstOrDefault();
            textBox1.Text = ex.CustomerId.ToString();
            textBox2.Text = ex.CustomerName;
            textBox3.Text = ex.CustomerPhone.ToString();
            textBox4.Text = ex.CustomerFAX.ToString();
            textBox5.Text = ex.CustomerMobile.ToString();
            textBox6.Text = ex.CustomerMail;
            textBox7.Text = ex.CustomerWebsite; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""
                && textBox2.Text != ""
                && textBox3.Text != ""
                && textBox4.Text != ""
                && textBox5.Text != ""
                && textBox6.Text != ""
                && textBox7.Text != "")
            {
                if (int.TryParse(textBox3.Text, out int phone) &&
                      int.TryParse(textBox4.Text, out int fax) &&
                      int.TryParse(textBox5.Text, out int mobile))
                {

                    var CusNum = int.Parse(textBox1.Text);

                    var searchinIfExsist = Ent.Customers.Find(CusNum);

                    if (searchinIfExsist != null)
                    {
                        searchinIfExsist.CustomerId = CusNum;
                        searchinIfExsist.CustomerName = textBox2.Text;
                        searchinIfExsist.CustomerPhone = int.Parse(textBox3.Text);
                        searchinIfExsist.CustomerFAX = int.Parse(textBox4.Text);
                        searchinIfExsist.CustomerMobile = int.Parse(textBox5.Text);
                        searchinIfExsist.CustomerMail = textBox6.Text;
                        searchinIfExsist.CustomerWebsite = textBox7.Text;
                        Ent.SaveChanges();
                        MessageBox.Show("تمت تعديل بيانات العميل بنجاح");
                        emptyTextbox();
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد عميل بهذا الرقم");
                    }
                }
                else
                {
                    MessageBox.Show("من فضلك أدخل بيانات الهاتف أوالمحمول أوالفاكس بالشكل الصحيح");
                }
            }
            else
            {
                MessageBox.Show("من فضلك أدخل بيانات العميل المراد تعديله كاملة");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm form = new MainForm();
            form.BackFromAny();
            form.ShowDialog();
            this.Close();
        }
    }
}
