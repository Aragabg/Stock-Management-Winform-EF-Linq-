namespace StockManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesOrder")]
    public partial class SalesOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SalesOrderId { get; set; }

        public int ProductId { get; set; }

        public int StoreId { get; set; }

        public int CustomerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SalesOrderDate { get; set; }

        public int? Quantity { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }

        public virtual Store Store { get; set; }
    }
}
