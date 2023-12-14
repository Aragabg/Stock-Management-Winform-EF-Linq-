using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StockManagement.Forms
{
    public partial class SalesOrders : Form
    {
        public static bool status = false;
        Model2 Ent = new Model2();  
        public SalesOrders()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox7.Enabled = false;
            textBox1.Enabled = false;
            textBox6.Enabled = false;
        }
        public void emptyTextbox()
        {
            textBox2.Text = textBox3.Text = textBox4.Text =
            textBox1.Text = textBox6.Text = 
            textBox5.Text = textBox7.Text = "";
            comboBox2.Text = comboBox3.Text = comboBox4.Text =
                comboBox5.Text = "";
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            emptyTextbox();
            var id = int.Parse(comboBox1.Text);
            var gettinSalesOrderById = (from i in Ent.SalesOrders where i.SalesOrderId == id select new { i.SalesOrderId,i.Product.ProductName
                                                                                                        ,i.Store.StoreName, i.Customer.CustomerName
                                                                                                        , i.SalesOrderDate,i.Quantity}).ToList();
            if (gettinSalesOrderById.Count!=0)
            {
            dataGridView1.DataSource = gettinSalesOrderById;
            }
            var get = Ent.SalesOrders.Where(i => i.SalesOrderId == id).FirstOrDefault();
            textBox5.Text = get.SalesOrderId.ToString();
            textBox3.Text = get.SalesOrderDate.ToString();
            comboBox3.Text = get.ProductId.ToString();
            comboBox2.Text = get.StoreId.ToString();
            comboBox4.Text = get.CustomerId.ToString();
            comboBox5.Text = get.Quantity.ToString();
            textBox2.Text = get.Customer.CustomerName;
            textBox4.Text = get.Store.StoreName;



        }

        private void SalesOrder_Load(object sender, EventArgs e)
        {
            var ex = from i in Ent.SalesOrders select i.SalesOrderId;
            foreach (var item in ex)
            {
                comboBox1.Items.Add(item);
            }
            var ex2= from i in Ent.Products select i.ProductId;
            foreach (var item in ex2)
            {
                comboBox3.Items.Add(item);
            }
            var ex3 = from i in Ent.Customers select i.CustomerId;
            foreach (var item in ex3)
            {
                comboBox4.Items.Add(item);
            }
            var ex4 = (from i in Ent.MeasurementUnits select i.Quantity).Distinct();
            foreach (var item in ex4)
            {
                comboBox5.Items.Add(item);
            }
            textBox3.Text = DateTime.Now.ToString();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            textBox6.Text = "";
            var selectedProNum = int.Parse(comboBox3.Text);
            var ex = from i in Ent.ProductsStores
                      where i.ProductId == selectedProNum
                      select i.StoreId;
            foreach (var item in ex.Distinct())
            {
                comboBox2.Items.Add(item);
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customers form = new Customers();
            form.ShowDialog();
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox6.Text = "";
            var selectedProNum = int.Parse(comboBox3.Text);
            var selectedStoNum = int.Parse(comboBox2.Text);
            var ex = (from i in Ent.Stores 
                     where i.StoreId == selectedStoNum
                     select i.StoreName).FirstOrDefault();
            textBox4.Text = ex.ToString();
            var ex2 = from i in Ent.ProductsStores
                      where i.StoreId == selectedStoNum && i.ProductId== selectedProNum
                      select i.Quantity;
            textBox7.Text=ex2.Sum().ToString();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCusNum = int.Parse(comboBox4.Text);
            var ex = (from i in Ent.Customers
                      where i.CustomerId == selectedCusNum
                      select i.CustomerName).FirstOrDefault();
            textBox2.Text = ex.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != ""
               && textBox5.Text != ""
               && comboBox2.Text != ""
               && comboBox3.Text != ""
               && comboBox4.Text != ""
               && comboBox5.Text != ""
               )
            {
                var x = int.Parse(textBox5.Text); 
                var orderNum = int.Parse(textBox5.Text);
                var searchinIfExsist = from i in Ent.SalesOrders where i.SalesOrderId == orderNum select i;

                if (searchinIfExsist.FirstOrDefault() == null)
                {
                    
                    if (DateTime.TryParse(textBox3.Text, out DateTime date) &&
                        int.TryParse(textBox5.Text, out int orderId)
                        )
                    {
                        SalesOrder newOrd = new SalesOrder();
                        newOrd.SalesOrderId = orderId; 
                        newOrd.SalesOrderDate= date;
                        newOrd.Quantity = int.Parse(comboBox5.Text);
                        newOrd.ProductId = int.Parse(comboBox3.Text);
                        newOrd.StoreId = int.Parse(comboBox2.Text);
                        newOrd.CustomerId = int.Parse(comboBox4.Text);
                        Ent.SalesOrders.Add(newOrd);
                        Ent.SaveChanges();
                        comboBox1.Items.Add(orderId);
                        var transfer = (from i in Ent.SalesOrders
                                        where i.SalesOrderId == x
                                        select new
                                        {
                                            i.SalesOrderId,
                                            i.Product.ProductName,
                                            i.Store.StoreName,
                                            i.Customer.CustomerName,
                                            i.SalesOrderDate,
                                            i.Quantity
                                        }).ToList();
                        var combinedData = new List<object>();

                        foreach (var item in transfer)
                        {
                            combinedData.Add(new
                            {
                                item.SalesOrderId,
                                item.ProductName,
                                item.StoreName,
                                item.CustomerName,
                                item.SalesOrderDate,
                                item.Quantity,
                                Measurment = textBox1.Text,
                                Price = textBox6.Text
                            });
                        }
                        dataGridView2.DataSource = combinedData;
                         
                        var proNum = int.Parse(comboBox3.Text);
                        var stoNum = int.Parse(comboBox2.Text);
                        var val = int.Parse(comboBox5.Text);
                        var z = (from i in Ent.ProductsStores
                                            where i.ProductId == proNum && i.StoreId == stoNum
                                            select i).FirstOrDefault();

                        if (z!=null)
                        {
                            z.Quantity -= val;
                            Ent.SaveChanges();
                          
                            if (z.Quantity==0)
                            {
                                Ent.ProductsStores.Remove(z);
                                Ent.SaveChanges();
                            }
                        }

                        Ent.SaveChanges();
                        MessageBox.Show("تمت اضافة الاذن بنجاح");
                        emptyTextbox();



                    }
                    else
                    {
                        MessageBox.Show("من فضلك أدخل بيانات الاذن بالشكل الصحيح");

                    }
                }
                else
                {
                    MessageBox.Show("رقم الاذن موجود بالفعل");
                }
            }
            else
            {
                MessageBox.Show("من فضلك ادخل جميع بيانات الإذن كاملة");
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            var q = int.Parse(comboBox5.Text);
            var id = int.Parse(comboBox3.Text);
            var ex = (from i in Ent.MeasurementUnits
                     where i.Quantity == q && i.ProductId == id
                     select new
                     {
                         i.MeasurementUnitName,
                         i.Price
                     }).FirstOrDefault();
            textBox1.Text = ex.MeasurementUnitName;
            textBox6.Text = ex.Price.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != ""
               && textBox5.Text != ""
               && comboBox2.Text != ""
               && comboBox3.Text != ""
               && comboBox4.Text != ""
               && comboBox5.Text != ""
               )
            {
                var x = int.Parse(textBox5.Text);
                var orderNum = int.Parse(textBox5.Text);
                var searchinIfExsist = (from i in Ent.SalesOrders where i.SalesOrderId == orderNum select i).FirstOrDefault();

                if (searchinIfExsist!= null)
                {
                    var oldQ = searchinIfExsist.Quantity.Value;
                    if (DateTime.TryParse(textBox3.Text, out DateTime date) &&
                        int.TryParse(textBox5.Text, out int orderId))
                    {
                        searchinIfExsist.ProductId = int.Parse(comboBox3.Text);
                        searchinIfExsist.StoreId = int.Parse(comboBox2.Text);
                        searchinIfExsist.Quantity= int.Parse(comboBox5.Text);
                        searchinIfExsist.CustomerId = int.Parse(comboBox4.Text);
                        searchinIfExsist.SalesOrderDate = DateTime.Parse(textBox3.Text);
                        Ent.SaveChanges();
                        var pronumber = int.Parse(comboBox3.Text);
                        var stonumber = int.Parse(comboBox2.Text);
                        var newP = (from i in Ent.ProductsStores
                                    where i.ProductId == pronumber
                                     && i.StoreId == stonumber
                                    select i).FirstOrDefault();
                        if (oldQ > int.Parse(comboBox5.Text))
                        {
                            newP.Quantity += Math.Abs(oldQ - int.Parse(comboBox5.Text));
                            Ent.SaveChanges();
                        }
                        else
                        {
                            newP.Quantity -= Math.Abs(int.Parse(comboBox5.Text) - oldQ);
                            Ent.SaveChanges();
                        }
                        MessageBox.Show("تم تعديل الاذن بنجاح");
                        emptyTextbox();



                    }
                    else
                    {
                        MessageBox.Show("من فضلك أدخل بيانات الاذن بالشكل الصحيح");

                    }
                }
                else
                {
                    MessageBox.Show("لا يوجد اذن بهذا الرقم");
                }
            }
            else
            {
                MessageBox.Show("من فضلك ادخل جميع بيانات الإذن كاملة");
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
