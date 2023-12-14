namespace StockManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductsStore")]
    public partial class ProductsStore
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int StoreId { get; set; }

        public int SupplierId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ProductionDate { get; set; }

        [Column("[Expiry(Per Month)")]
        public int? C_Expiry_Per_Month_ { get; set; }

        public int? SupplyOrderId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Store Store { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual SupplyOrder SupplyOrder { get; set; }
    }
}
