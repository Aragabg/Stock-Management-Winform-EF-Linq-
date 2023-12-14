namespace StockManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            ProductsStores = new HashSet<ProductsStore>();
            SupplyOrders = new HashSet<SupplyOrder>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierId { get; set; }

        [StringLength(50)]
        public string SupplierName { get; set; }

        public int? SupplierPhone { get; set; }

        public int? SupplierFAX { get; set; }

        public int? SupplierMobile { get; set; }

        [StringLength(50)]
        public string SupplierMail { get; set; }

        [StringLength(50)]
        public string SupplierWebsite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductsStore> ProductsStores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplyOrder> SupplyOrders { get; set; }
    }
}
