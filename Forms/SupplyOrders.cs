using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagement.Forms
{
    public partial class SupplyOrders : Form
    {
        Model2 Ent = new Model2();

        public SupplyOrders()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        public void emptyTextbox()
        {
            textBox2.Text = textBox3.Text = textBox4.Text =
            textBox1.Text = textBox6.Text =textBox5.Text =
            comboBox2.Text = comboBox3.Text = comboBox4.Text =
            comboBox5.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            emptyTextbox();
            var id = int.Parse(comboBox1.Text);
            var gettinSupOrderById = (from i in Ent.SupplyOrders
                                        where i.SupplyOrderId == id
                                        select new
                                        {
                                            i.SupplyOrderId ,
                                            i.Product.ProductName
                                                                                                        ,
                                            i.Store.StoreName,
                                            i.Supplier.SupplierName
                                                                                                        ,
                                            i.SupplyOrderDate,
                                            i.Quantity,
                                            i.ProductionDate,
                                            i.Expiry
                                        }).ToList();
            if (gettinSupOrderById.Count != 0)
            {
                dataGridView1.DataSource = gettinSupOrderById;
            }
            var get = Ent.SupplyOrders.Where(i => i.SupplyOrderId == id).FirstOrDefault();
            textBox5.Text = get.SupplyOrderId.ToString();
            textBox3.Text = get.SupplyOrderDate.ToString();
            comboBox3.Text = get.ProductId.ToString();
            comboBox2.Text = get.StoreId.ToString();
            comboBox4.Text = get.SupplierId.ToString();
            comboBox5.Text = get.Quantity.ToString();
            textBox2.Text = get.Supplier.SupplierName;
            textBox4.Text = get.Store.StoreName;
            textBox1.Text = get.ProductionDate.ToString();
            textBox6.Text = get.Expiry.ToString();
            

        }

        private void SupplyOrders_Load(object sender, EventArgs e)
        {
            var ex = from i in Ent.SupplyOrders select i.SupplyOrderId;
            foreach (var item in ex)
            {
                comboBox1.Items.Add(item);
            }
            var ex2 = from i in Ent.Products select i.ProductId;
            foreach (var item in ex2)
            {
                comboBox3.Items.Add(item);
            }
            var ex3 = from i in Ent.Suppliers select i.SupplierId;
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != ""
              && textBox5.Text != ""
              && textBox1.Text != ""
              && textBox6.Text != ""
              && textBox4.Text != ""
              && textBox2.Text != ""
              && comboBox5.Text != ""
              && comboBox2.Text != ""
              && comboBox3.Text != ""
              && comboBox4.Text != ""
              )
            {
                var x = int.Parse(textBox5.Text);
                var orderNum = int.Parse(textBox5.Text);
                var searchinIfExsist = from i in Ent.SupplyOrders where i.SupplyOrderId == orderNum select i;

                if (searchinIfExsist.FirstOrDefault() == null)
                {

                    if (DateTime.TryParse(textBox3.Text, out DateTime date) &&
                        int.TryParse(textBox5.Text, out int orderId)
                        )
                    {
                        SupplyOrder newOrd = new SupplyOrder();
                        newOrd.SupplyOrderId = orderId;
                        newOrd.SupplyOrderDate = date;
                        newOrd.Quantity = int.Parse(comboBox5.Text);
                        newOrd.ProductId = int.Parse(comboBox3.Text);
                        newOrd.StoreId = int.Parse(comboBox2.Text);
                        newOrd.SupplierId = int.Parse(comboBox4.Text);
                        newOrd.ProductionDate = DateTime.Parse(textBox1.Text);
                        newOrd.Expiry = int.Parse(textBox6.Text);
                        Ent.SupplyOrders.Add(newOrd);
                        Ent.SaveChanges();
                        comboBox1.Items.Add(orderId);
                       var pronumber  = int.Parse(comboBox3.Text);
                       var stonumber  = int.Parse(comboBox2.Text);
                       var supnumber  = int.Parse(comboBox4.Text);
                       var datenumber = DateTime.Parse(textBox1.Text);
                       var exnumber   = int.Parse(textBox6.Text);
                        var newP = (from i in Ent.ProductsStores
                                   where i.ProductId           == pronumber  
                                    &&   i.StoreId             == stonumber  
                                    &&   i.SupplierId          == supnumber  
                                    &&   i.ProductionDate      == datenumber 
                                    &&   i.C_Expiry_Per_Month_ == exnumber
                                    select i).FirstOrDefault();
                        if (newP ==null)
                        {
                            ProductsStore product = new ProductsStore();
                            product.ProductId = int.Parse(comboBox3.Text);
                            product.StoreId = int.Parse(comboBox2.Text);
                            product.SupplierId = int.Parse(comboBox4.Text);
                            product.ProductionDate = DateTime.Parse(textBox1.Text);
                            product.C_Expiry_Per_Month_ = int.Parse(textBox6.Text);
                            product.SupplyOrderId = orderId;
                            product.Quantity = int.Parse(comboBox5.Text);
                            Ent.ProductsStores.Add(product);
                            Ent.SaveChanges();
                        }
                        else
                        {
                            newP.Quantity+= int.Parse(comboBox5.Text);
                            Ent.SaveChanges();
                        }

                        var transfer = (from i in Ent.SupplyOrders
                                        where i.SupplyOrderId == x
                                        select new
                                        {
                                            i.SupplyOrderId,
                                            i.Product.ProductName,
                                            i.Store.StoreName,
                                            i.Supplier.SupplierName,
                                            i.SupplyOrderDate,
                                            i.Quantity,
                                            i.ProductionDate,
                                            i.Expiry,
                                        }).ToList();

                        dataGridView2.DataSource = transfer;
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm form = new MainForm();
            form.BackFromAny();
            form.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != ""
               && textBox5.Text != ""
               && textBox1.Text != ""
               && textBox6.Text != ""
               && comboBox2.Text != ""
               && comboBox3.Text != ""
               && comboBox4.Text != ""
               && comboBox5.Text != "")
            {
                var x = int.Parse(textBox5.Text);
                var orderNum = int.Parse(textBox5.Text);
                var searchinIfExsist = (from i in Ent.SupplyOrders where i.SupplyOrderId == orderNum select i).FirstOrDefault();
                
                if (searchinIfExsist != null)
                {
                    var oldQ = searchinIfExsist.Quantity.Value;
                    if (DateTime.TryParse(textBox3.Text, out DateTime date) &&
                        int.TryParse(textBox5.Text, out int orderId))
                    {
                        searchinIfExsist.ProductId = int.Parse(comboBox3.Text);
                        searchinIfExsist.StoreId = int.Parse(comboBox2.Text);
                        searchinIfExsist.Quantity = int.Parse(comboBox5.Text);
                        searchinIfExsist.SupplierId = int.Parse(comboBox4.Text);
                        searchinIfExsist.ProductionDate = DateTime.Parse(textBox1.Text);
                        searchinIfExsist.Expiry = int.Parse(textBox6.Text);
                        searchinIfExsist.SupplyOrderDate = DateTime.Parse(textBox3.Text);
                        Ent.SaveChanges();
                        var pronumber = int.Parse(comboBox3.Text);
                        var stonumber = int.Parse(comboBox2.Text);
                        var supnumber = int.Parse(comboBox4.Text);
                        var exnumber = int.Parse(textBox6.Text);
                        var newP = (from i in Ent.ProductsStores
                                    where i.ProductId == pronumber
                                     && i.StoreId == stonumber
                                     && i.SupplierId == supnumber
                                     && i.C_Expiry_Per_Month_ == exnumber
                                    select i).FirstOrDefault();
                            if (oldQ > int.Parse(comboBox5.Text))
                            {
                            newP.Quantity -= Math.Abs( oldQ - int.Parse(comboBox5.Text));
                                Ent.SaveChanges();
                            }
                            else
                            {
                                newP.Quantity += Math.Abs( int.Parse(comboBox5.Text)- oldQ);
                                Ent.SaveChanges();
                            }

                        Ent.SaveChanges();
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
    }
}
