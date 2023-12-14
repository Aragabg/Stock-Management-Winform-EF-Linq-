namespace StockManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesOrderInfo")]
    public partial class SalesOrderInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SalesOrderInfoId { get; set; }

        public int SalesOrderId { get; set; }

        public int ProductId { get; set; }

        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }
    }
}
