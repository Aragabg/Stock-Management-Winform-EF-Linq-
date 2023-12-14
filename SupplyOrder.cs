namespace StockManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplyOrder")]
    public partial class SupplyOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplyOrder()
        {
            ProductsStores = new HashSet<ProductsStore>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplyOrderId { get; set; }

        public int StoreId { get; set; }

        public int ProductId { get; set; }

        public int SupplierId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SupplyOrderDate { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ProductionDate { get; set; }

        public int? Expiry { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductsStore> ProductsStores { get; set; }

        public virtual Store Store { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
