using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagement.Forms
{
    public partial class Products : Form
    {   
        Model2 Ent =new Model2();
        public Products()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        public void emptyTextbox()
        {
            textBox1.Text = textBox2.Text
            = textBox3.Text = textBox4.Text
            = textBox5.Text = textBox6.Text
            = textBox7.Text = textBox8.Text 
            = textBox9.Text ="";
            
        }

        private void Products_Load(object sender, EventArgs e)
        {
            //filling the combo1 with products IDs
            var ex = from i in Ent.Products select i.ProductId;
            foreach (var item in ex)
            {
                comboBox1.Items.Add(item);
            }
            button3.Enabled = false;
            textBox9.Enabled = false;
            button7.Enabled = false;
           

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filling the textboxes from combo1
            comboBox2.Items.Clear();
            textBox1.Text = textBox2.Text
            = textBox3.Text = textBox4.Text
            = textBox5.Text = textBox6.Text
            = textBox7.Text = textBox8.Text
            = comboBox2.Text = "";
            var Combo = int.Parse(comboBox1.Text);
            var ex = (from i in Ent.Products
                      join k in Ent.ProductsStores
                      on i.ProductId equals k.ProductId
                      where k.ProductId == Combo
                      select new { i.ProductId,
                          i.ProductName,
                          i.ProductCode,
                          k.StoreId,
                          k.Quantity,
                          k.ProductionDate,
                          k.C_Expiry_Per_Month_,
                          k.SupplierId }).FirstOrDefault();
            textBox1.Text = ex.ProductId.ToString();
            textBox2.Text = ex.StoreId.ToString();
            textBox3.Text = ex.ProductName;
            textBox4.Text = ex.ProductCode.ToString();
            textBox5.Text = ex.Quantity.ToString();
            textBox8.Text = ex.ProductionDate.ToString();
            textBox7.Text = ex.SupplierId.ToString();
            textBox6.Text = ex.C_Expiry_Per_Month_.ToString();
            //pushing the stores that include the product into combo2
            var ex2 = (from i in Ent.Products
                       join k in Ent.ProductsStores
                       on i.ProductId equals k.ProductId
                       where k.ProductId == Combo
                       select k.StoreId).ToList();
            
            foreach (var item in ex2)
            {
                comboBox2.Items.Add(item);
            }


        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""
                && textBox2.Text != ""
                && textBox3.Text != ""
                && textBox4.Text != ""
                && textBox5.Text != ""
                && textBox6.Text != ""
                && textBox7.Text != ""
                && textBox8.Text != "")
            {
                if (int.TryParse(textBox1.Text, out int ProductNumber) &&
                        int.TryParse(textBox2.Text, out int StoreNumber) &&
                        int.TryParse(textBox4.Text, out int ProductCode) &&
                        int.TryParse(textBox5.Text, out int Quantity) &&
                        int.TryParse(textBox6.Text, out int Expiry) &&
                        int.TryParse(textBox7.Text, out int SupplierId) &&
                        int.TryParse(textBox9.Text, out int NEWStore) &&
                        DateTime.TryParse(textBox8.Text, out DateTime ProductionDate)
                        )
                {

                    var searchinIfExsist = (from i in Ent.ProductsStores
                                            where i.ProductId == ProductNumber && i.StoreId == StoreNumber
                                            select i).FirstOrDefault();
                    var searchinIfExsist3 = (from i in Ent.Stores
                                                where i.StoreId == NEWStore
                                                select i).FirstOrDefault();

                    if (searchinIfExsist != null && searchinIfExsist3 != null)
                    {
                        var newRec = new ProductsStore();
                        newRec.ProductId = ProductNumber;
                        newRec.StoreId = NEWStore;
                        newRec.SupplierId = SupplierId;
                        newRec.ProductionDate = ProductionDate;
                        newRec.C_Expiry_Per_Month_ = Expiry;
                        newRec.Quantity = Quantity;
                        Ent.ProductsStores.Remove(searchinIfExsist);
                        Ent.ProductsStores.Add(newRec);
                        Ent.SaveChanges();

                        Transfering rec = new Transfering();
                        rec.ProductId = ProductNumber;
                        rec.From=StoreNumber;
                        rec.To=NEWStore;
                        rec.Date = DateTime.Now;
                        Ent.Transferings.Add(rec);
                        Ent.SaveChanges();
                        MessageBox.Show("تمت نقل الصنف بنجاح");
                        emptyTextbox();
                    }
                    else
                    {
                        MessageBox.Show("خطأ في البيانات المدخلة , يرجى التأكده من ارقام المورد والمخزن ورقم الصنف ذاته");
                    }
                }
                else
                {
                    MessageBox.Show("من فضلك أدخل بيانات الأرقام والتواريخ بالشكل الصحيح");
                }
            }
            else
            {
                MessageBox.Show("من فضلك أدخل بيانات الصنف المراد نقله كاملة");
            }
        }
    

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filling textboxes with new selected store with the same product
              textBox5.Text = textBox6.Text
              = textBox7.Text = textBox8.Text = "";
              textBox2.Text = comboBox2.Text;
              var selecteStore = int.Parse(comboBox2.Text);
              var selecteProduct = int.Parse(textBox1.Text);

              var ex = (from i in Ent.ProductsStores
                        where i.ProductId == selecteProduct
                        && i.StoreId == selecteStore
                        select new
                        {
                            i.SupplierId,
                            i.ProductionDate,
                            i.Quantity,
                            i.C_Expiry_Per_Month_
                        }).FirstOrDefault();

              textBox5.Text = ex.Quantity.ToString();
              textBox6.Text = ex.C_Expiry_Per_Month_.ToString();
              textBox7.Text = ex.SupplierId.ToString();
              textBox8.Text = ex.ProductionDate.ToString();
            


            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm form = new MainForm();
            form.BackFromAny();
            form.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""
                && textBox2.Text != ""
                && textBox3.Text != ""
                && textBox4.Text != ""
                && textBox5.Text != ""
                && textBox6.Text != ""
                && textBox7.Text != ""
                && textBox8.Text != "")
            {
                var ProNum = int.Parse(textBox1.Text);
                var StoNum = int.Parse(textBox2.Text);
                var StoNumValid = Ent.Stores.Where(a=>a.StoreId==StoNum).FirstOrDefault();

                if (StoNumValid != null)
                {


                    var searchinIfExsist = (from i in Ent.ProductsStores
                                            where i.ProductId == ProNum && i.StoreId == StoNum
                                            select i).FirstOrDefault();
                    var searchinIfExsist2 = (from i in Ent.ProductsStores
                                             where i.ProductId == ProNum && i.StoreId != StoNum
                                             select i).FirstOrDefault();
                    if (searchinIfExsist == null || searchinIfExsist2 != null)
                    {
                        var product = new Product();
                        product.ProductId = ProNum;
                        product.ProductName = textBox3.Text;
                        product.ProductCode = int.Parse(textBox4.Text);
                        var productstore = new ProductsStore();
                        productstore.ProductId = ProNum;
                        productstore.Quantity = int.Parse(textBox5.Text);
                        productstore.C_Expiry_Per_Month_ = int.Parse(textBox6.Text);
                        productstore.SupplierId = int.Parse(textBox7.Text);
                        productstore.ProductionDate = DateTime.Parse(textBox8.Text);
                        productstore.StoreId= StoNum;
                        product.ProductsStores.Add(productstore);
  
                        Ent.Products.Add(product);
                        Ent.SaveChanges();
                        comboBox1.Items.Add(ProNum);
                        this.emptyTextbox();
                        MessageBox.Show("تمت إضافة الصنف بنجاح");



                    }
                    else
                    {
                        MessageBox.Show("رقم الصنف موجود بالفعل في هذا المخزن");
                    }
                }
                else
                {
                    MessageBox.Show("!!! مخزن غير موجود");
                }
            }
            else
            {
                MessageBox.Show("من فضلك ادخل جميع بيانات المنتج كاملة");
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
              && textBox7.Text != ""
              && textBox8.Text != "")
            {
                if (int.TryParse(textBox1.Text, out int ProductNumber) &&
                      int.TryParse(textBox2.Text, out int StoreNumber) &&
                      int.TryParse(textBox4.Text, out int ProductCode)&&
                      int.TryParse(textBox5.Text, out int Quantity)&&
                      int.TryParse(textBox6.Text, out int Expiry)&&
                      int.TryParse(textBox7.Text, out int SupplierId)&&
                      DateTime.TryParse(textBox8.Text, out DateTime ProductionDate)
                      )
                {
                    
                    var searchinIfExsist = (from i in Ent.ProductsStores
                                            where i.ProductId == ProductNumber && i.StoreId == StoreNumber
                                            select i).FirstOrDefault();
                    var searchoutIfExsist2 = (from i in Ent.Products 
                                              join j in Ent.ProductsStores
                                              on i.ProductId equals ProductNumber
                                              select i).FirstOrDefault();
                    var searchinIfExsist3 = (from i in Ent.Stores
                                             join j in Ent.ProductsStores
                                             on i.StoreId equals StoreNumber
                                             select i).FirstOrDefault();
                    var searchinIfExsist4 = (from i in Ent.Suppliers
                                             join j in Ent.ProductsStores
                                             on i.SupplierId equals SupplierId
                                             select i).FirstOrDefault();
                    if (searchinIfExsist != null&& searchoutIfExsist2 !=null && searchinIfExsist3!=null && searchinIfExsist4!=null)
                    {
                        
                        searchinIfExsist.ProductionDate = ProductionDate;
                        searchinIfExsist.C_Expiry_Per_Month_= Expiry;
                        searchinIfExsist.Quantity = Quantity;
                        searchinIfExsist.ProductId = ProductNumber;
                        searchinIfExsist.Product.ProductName = textBox3.Text;
                        searchinIfExsist.Product.ProductCode = ProductCode;
                        searchinIfExsist.SupplierId = SupplierId;
                        searchinIfExsist.StoreId = StoreNumber;

                        Ent.SaveChanges();
                        MessageBox.Show("تمت تعديل بيانات الصنف بنجاح");
                        emptyTextbox();
                    }
                    else
                    {
                        MessageBox.Show("خطأ في البيانات المدخلة , يرجى التأكده من ارقام المورد والمخزن ورقم الصنف ذاته");
                    }
                }
                else
                {
                    MessageBox.Show("من فضلك أدخل بيانات الأرقام والتواريخ بالشكل الصحيح");
                }
            }
            else
            {
                MessageBox.Show("من فضلك أدخل بيانات الصنف المراد تعديله كاملة");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            this.emptyTextbox();
            button3.Enabled = true;
            button7.Enabled = true;
            textBox9.Enabled = true;
            textBox2.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            //this.Products_Load(null, null);
            button3.Enabled = false;
            button7.Enabled = false;
            textBox9.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports form = new Reports();
            form.ShowDialog();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports form = new Reports();
            form.ShowDialog();
            this.Close();
        }
    }
}
