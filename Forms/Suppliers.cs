using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagement.Forms
{
    public partial class Suppliers : Form
    {
        Model2 Ent = new Model2(); 
        public Suppliers()
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

        private void Suppliers_Load(object sender, EventArgs e)
        {
            var ex = from i in Ent.Suppliers select i.SupplierId;
            foreach (var item in ex)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            emptyTextbox();
            var SupNum = int.Parse(comboBox1.Text);
            var ex = Ent.Suppliers.Where(a => a.SupplierId == SupNum).FirstOrDefault();
            textBox1.Text = ex.SupplierId.ToString();
            textBox2.Text = ex.SupplierName;
            textBox3.Text = ex.SupplierPhone.ToString();
            textBox4.Text = ex.SupplierFAX.ToString();
            textBox5.Text = ex.SupplierMobile.ToString();
            textBox6.Text = ex.SupplierMail;
            textBox7.Text = ex.SupplierWebsite;
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

                var SupNum = int.Parse(textBox1.Text);
                var searchinIfExsist = from i in Ent.Suppliers where i.SupplierId == SupNum select i;

                if (searchinIfExsist.FirstOrDefault() == null)
                {
                    if (int.TryParse(textBox3.Text, out int phone) &&
                        int.TryParse(textBox4.Text, out int fax) &&
                        int.TryParse(textBox5.Text, out int mobile))
                    {
                        Supplier newSup = new Supplier();
                        newSup.SupplierId = SupNum;
                        newSup.SupplierName = textBox2.Text;
                        newSup.SupplierPhone = int.Parse(textBox3.Text);
                        newSup.SupplierFAX = int.Parse(textBox4.Text);
                        newSup.SupplierMobile = int.Parse(textBox5.Text);
                        newSup.SupplierMail = textBox6.Text;
                        newSup.SupplierWebsite = textBox7.Text;
                        Ent.Suppliers.Add(newSup);
                        Ent.SaveChanges();
                        comboBox1.Items.Add(SupNum);
                        emptyTextbox();
                        MessageBox.Show("تمت إضافة المورد بنجاح");

                    }
                    else
                    {
                        MessageBox.Show("من فضلك أدخل بيانات الهاتف أوالمحمول أوالفاكس بالشكل الصحيح");

                    }
                }
                else
                {
                    MessageBox.Show("رقم المورد موجود بالفعل");
                }
            }
            else
            {
                MessageBox.Show("من فضلك ادخل جميع بيانات المورد كاملة");
            }
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

                    var SupNum = int.Parse(textBox1.Text);

                    var searchinIfExsist = Ent.Suppliers.Find(SupNum);

                    if (searchinIfExsist != null)
                    {
                        searchinIfExsist.SupplierId = SupNum;
                        searchinIfExsist.SupplierName = textBox2.Text;
                        searchinIfExsist.SupplierPhone = int.Parse(textBox3.Text);
                        searchinIfExsist.SupplierFAX = int.Parse(textBox4.Text);
                        searchinIfExsist.SupplierMobile = int.Parse(textBox5.Text);
                        searchinIfExsist.SupplierMail = textBox6.Text;
                        searchinIfExsist.SupplierWebsite = textBox7.Text;
                        Ent.SaveChanges();
                        MessageBox.Show("تمت تعديل بيانات المورد بنجاح");
                        emptyTextbox();
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد مورد بهذا الرقم");
                    }
                }
                else
                {
                    MessageBox.Show("من فضلك أدخل بيانات الهاتف أوالمحمول أوالفاكس بالشكل الصحيح");
                }
            }
            else
            {
                MessageBox.Show("من فضلك أدخل بيانات المورد المراد تعديله كاملة");
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
