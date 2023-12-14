using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StockManagement
{
    public partial class Stores : Form
    {
        Model2 Ent = new Model2();

        public Stores()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        public void emptyTextbox()
        {
           textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            emptyTextbox();
            var storeNum = int.Parse(comboBox1.Text);
            var ex = Ent.Stores.Where(a => a.StoreId == storeNum).FirstOrDefault();
            textBox1.Text = ex.StoreId.ToString();
            textBox2.Text = ex.StoreName;
            textBox3.Text = ex.StoreAddress;
            textBox4.Text = ex.StoreSupervisor;


        }

        private void Stores_Load(object sender, EventArgs e)
        {
           // var ent = new Model1();
            comboBox1.Items.Clear();
            var ex = from i in Ent.Stores select i.StoreId;
            foreach (var item in ex)
            {
                comboBox1.Items.Add(item);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {

                var storeNum = int.Parse(comboBox1.Text);

                var searchinIfExsist = Ent.Stores.Find(storeNum);

                if (searchinIfExsist != null)
                {
                    searchinIfExsist.StoreId = storeNum;
                    searchinIfExsist.StoreName = textBox2.Text;
                    searchinIfExsist.StoreAddress = textBox3.Text;
                    searchinIfExsist.StoreSupervisor = textBox4.Text;
                    Ent.SaveChanges();
                    comboBox1.Text = "";
                    MessageBox.Show("تمت تعديل بيانات المخزن بنجاح");
                }
                else
                {
                    MessageBox.Show("لا يوجد مخزن بهذا الرقم");
                }
            }
            else
            {
                MessageBox.Show("من فضلك أدخل بيانات المخزن المراد تعديله كاملة");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            if (textBox1.Text !="" && textBox2.Text != "" && textBox3.Text !=""&& textBox4.Text != "")
            {
                
                var storeNum = int.Parse(textBox1.Text);
                var searchinIfExsist = from i in Ent.Stores where i.StoreId == storeNum select i;

                if (searchinIfExsist.FirstOrDefault() == null)
                {
                        Store newStore = new Store();
                        newStore.StoreId = storeNum;
                        newStore.StoreName = textBox2.Text;
                        newStore.StoreAddress=textBox3.Text;
                        newStore.StoreSupervisor=textBox4.Text;
                        Ent.Stores.Add(newStore);
                        Ent.SaveChanges();
                        comboBox1.Items.Add(storeNum);
                        textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
                        MessageBox.Show("تمت إضافة المخزن بنجاح");
                }
                else
                {
                    MessageBox.Show("رقم المخزن موجود بالفعل");
                }
            }
            else
            {
                MessageBox.Show("من فضلك ادخل بيانات المخزن كامله");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {

                var storeNum = int.Parse(textBox1.Text);
                var searchinIfExsist = (from i in Ent.Stores where i.StoreId == storeNum select new {i.StoreId,i.StoreName,i.StoreAddress,i.StoreSupervisor}).ToList();

                if (searchinIfExsist.Count != 0)
                {
                    dataGridView1.DataSource = searchinIfExsist;
                }
                else
                {
                    MessageBox.Show("ادخل رقم المخزن الصحيح");
                }
            }
            else
            {
                MessageBox.Show("من فضلك ادخل رقم المخزن");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
                var searchinIfExsist = (from i in Ent.Stores select new { i.StoreId, i.StoreName, i.StoreAddress, i.StoreSupervisor }).ToList();
                 dataGridView1.DataSource = searchinIfExsist;
              


        }
    }
}

