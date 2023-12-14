using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StockManagement.Forms
{
    public partial class Reports : Form
    {
        public int storenums { get; set; }
        public int storenumss { get; set; }
        Model2 Ent = new Model2();
        public Reports()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void Reports_Load(object sender, EventArgs e)
        {
            var ex = (from i in Ent.Products select i.ProductId).ToList();
            foreach (var item in ex)
            {
                comboBox1.Items.Add(item);
                comboBox2.Items.Add(item);
                comboBox3.Items.Add(item);

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            var proNum = int.Parse(comboBox1.Text);
            var ex = (from i in Ent.ProductsStores
                      where i.ProductId == proNum
                      select i.StoreId).ToList();
            foreach (var item in ex.Distinct())
            {
                checkedListBox1.Items.Add(item);
            }
            storenums = ex.Count();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && checkedListBox1.CheckedItems.Count > 0 && textBox1.Text == "" && textBox2.Text == "")
            {
                var proNum = int.Parse(comboBox1.Text);
                var selectedStoreIds = int.Parse(checkedListBox1.CheckedItems[0].ToString());
                var ex = (from i in Ent.SupplyOrders
                          where i.ProductId == proNum && i.StoreId == selectedStoreIds
                          select new
                          {
                              i.ProductId,
                              i.StoreId,
                              i.Store.StoreName,
                              i.SupplyOrderDate,
                              i.SupplierId,
                              i.Supplier.SupplierName,
                              i.Quantity,
                              i.ProductionDate,
                              i.Expiry
                          }).ToList();
                dataGridView1.DataSource = ex;
            }
            else if (comboBox1.Text != "" && checkedListBox1.CheckedItems.Count <= storenums && checkedListBox1.CheckedItems.Count > 0 && textBox1.Text == "" && textBox2.Text == "")
            {
                var proNum = int.Parse(comboBox1.Text);
                List<int> selectedStoreIds = new List<int>();

                foreach (var item in checkedListBox1.CheckedItems)
                {
                    if (int.TryParse(item.ToString(), out int storeId))
                    {
                        selectedStoreIds.Add(storeId);
                    }
                }
                var ex = (from i in Ent.SupplyOrders
                          where i.ProductId == proNum && selectedStoreIds.Contains(i.StoreId)
                          select new
                          {
                              i.ProductId,
                              i.StoreId,
                              i.Store.StoreName,
                              i.SupplyOrderDate,
                              i.SupplierId,
                              i.Supplier.SupplierName,
                              i.Quantity,
                              i.ProductionDate,
                              i.Expiry
                          }).ToList();

                dataGridView1.DataSource = ex;

            }
            else if (comboBox1.Text != "" && checkedListBox1.CheckedItems.Count <= storenums && checkedListBox1.CheckedItems.Count > 0 && textBox1.Text != "" && textBox2.Text != "")
            {
                var date1 = DateTime.Parse(textBox1.Text);
                var date2 = DateTime.Parse(textBox2.Text);
                var proNum = int.Parse(comboBox1.Text);
                List<int> selectedStoreIds = new List<int>();

                foreach (var item in checkedListBox1.CheckedItems)
                {
                    if (int.TryParse(item.ToString(), out int storeId))
                    {
                        selectedStoreIds.Add(storeId);
                    }
                }
                var ex = (from i in Ent.SupplyOrders
                          where i.ProductId == proNum && selectedStoreIds.Contains(i.StoreId)
                          && i.SupplyOrderDate >= date1 && i.SupplyOrderDate <= date2
                          select new
                          {
                              i.ProductId,
                              i.StoreId,
                              i.Store.StoreName,
                              i.SupplyOrderDate,
                              i.SupplierId,
                              i.Supplier.SupplierName,
                              i.Quantity,
                              i.ProductionDate,
                              i.Expiry
                          }).ToList();

                dataGridView1.DataSource = ex;

            }
            else
            {
                MessageBox.Show("من فضلك اختر بيانات الصنف والمخزن");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm form = new MainForm();
            form.BackFromAny();
            form.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Products form = new Products();
            form.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "" && checkedListBox2.CheckedItems.Count > 0 && textBox3.Text == "" && textBox4.Text == "")
            {
                var proNum = int.Parse(comboBox2.Text);
                var selectedStoreIds = int.Parse(checkedListBox2.CheckedItems[0].ToString());
                var ex = (from i in Ent.Transferings
                          where i.ProductId == proNum && i.To == selectedStoreIds
                          select new
                          {
                              i.ProductId,
                              i.From,
                              i.To,
                              i.Date
                          }).ToList();
                dataGridView1.DataSource = ex;
            }
            else if (comboBox2.Text != "" && checkedListBox2.CheckedItems.Count <= storenumss && checkedListBox1.CheckedItems.Count > 0 && textBox3.Text == "" && textBox4.Text == "")
            {
                var proNum = int.Parse(comboBox2.Text);
                List<int> selectedStoreIdss = new List<int>();

                foreach (var item in checkedListBox2.CheckedItems)
                {
                    if (int.TryParse(item.ToString(), out int storeId))
                    {
                        selectedStoreIdss.Add(storeId);
                    }
                }
                var ex = (from i in Ent.Transferings
                          where i.ProductId == proNum && selectedStoreIdss.Contains(i.To)
                          select new
                          {
                              i.ProductId,
                              i.From,
                              i.To,
                              i.Date
                          }).ToList();

                dataGridView1.DataSource = ex;

            }
            else if (comboBox2.Text != "" && checkedListBox2.CheckedItems.Count <= storenumss && checkedListBox2.CheckedItems.Count > 0 && textBox3.Text != "" && textBox4.Text != "")
            {
                var date1 = DateTime.Parse(textBox4.Text);
                var date2 = DateTime.Parse(textBox3.Text);
                var proNum = int.Parse(comboBox2.Text);
                List<int> selectedStoreIdss = new List<int>();

                foreach (var item in checkedListBox2.CheckedItems)
                {
                    if (int.TryParse(item.ToString(), out int storeId))
                    {
                        selectedStoreIdss.Add(storeId);
                    }
                }
                var ex = (from i in Ent.Transferings
                          where i.ProductId == proNum && selectedStoreIdss.Contains(i.To)
                          && i.Date >= date1 && i.Date <= date2
                          select new
                          {
                              i.ProductId,
                              i.From,
                              i.To,
                              i.Date
                          }).ToList();

                dataGridView1.DataSource = ex;

            }
            else
            {
                MessageBox.Show("من فضلك اختر بيانات الصنف والمخزن");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox2.Items.Clear();
            var proNum = int.Parse(comboBox2.Text);
            var ex = (from i in Ent.Transferings
                      where i.ProductId == proNum
                      select i.To).ToList();
            foreach (var item in ex.Distinct())
            {
                checkedListBox2.Items.Add(item);
            }
            storenumss = ex.Count();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            var proNum = int.Parse(comboBox3.Text);
            var ex = (from i in Ent.ProductsStores
                      where i.ProductId == proNum
                      select i.StoreId).ToList();
            foreach (var item in ex.Distinct())
            {
                comboBox4.Items.Add(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text != "" && comboBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                if (DateTime.TryParse(textBox6.Text, out DateTime date1)
                    && DateTime.TryParse(textBox5.Text, out DateTime date2))
                {

                    var ex = (from i in Ent.SupplyOrders
                              join a in Ent.ProductsStores
                              on i.SupplyOrderId equals a.SupplyOrderId
                              where i.SupplyOrderDate >= date1
                                 && i.SupplyOrderDate <= date2
                              select new
                              {
                                  a.ProductId,
                                  a.Product.ProductName,
                                  a.StoreId,
                                  a.Store.StoreName,
                                  a.SupplierId,
                                  a.Supplier.SupplierName,
                                  a.Quantity,
                                  a.ProductionDate,
                                  a.C_Expiry_Per_Month_,
                                  i.SupplyOrderDate
                              }).ToList();
                    if (ex != null)
                    {
                        dataGridView1.DataSource = ex;
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد بيانات في هذه الفتره");
                    }
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل التاريخ بالشكل الصحيح");

                }
            }
            else
            {
                MessageBox.Show("من فضلك ادخل جميع البيانات");
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var supplyOrders = (from i in Ent.SupplyOrders
                                join a in Ent.ProductsStores
                                on i.SupplyOrderId equals a.SupplyOrderId
                                select new
                                {
                                    a.ProductId,
                                    a.Product.ProductName,
                                    a.StoreId,
                                    a.Store.StoreName,
                                    a.SupplierId,
                                    a.Supplier.SupplierName,
                                    a.Quantity,
                                    a.ProductionDate,
                                    a.C_Expiry_Per_Month_,
                                    i.SupplyOrderDate
                                }).ToList(); 
            var currentDate = DateTime.Now;

            var ex = supplyOrders
                .Where(a => a.ProductionDate.HasValue)
                .Select(a => new
                {
                    a.ProductId,
                    a.ProductName,
                    a.StoreId,
                    a.StoreName,
                    a.SupplierId,
                    a.SupplierName,
                    a.Quantity,
                    ProductionDate = a.ProductionDate.Value, 
                    a.C_Expiry_Per_Month_,
                    ExpiryDate = a.ProductionDate.Value.AddMonths((int)a.C_Expiry_Per_Month_),
                    a.SupplyOrderDate
                })
                .Where(a => a.ExpiryDate >= currentDate)
                .ToList();

            if (ex != null && ex.Count > 0)
            {
                dataGridView1.DataSource = ex;
            }
            else
            {
                MessageBox.Show("لا يوجد منتجات تقترب من الانتهاء");
            }
        }
    }
}