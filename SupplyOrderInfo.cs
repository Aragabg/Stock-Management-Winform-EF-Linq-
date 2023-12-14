namespace StockManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplyOrderInfo")]
    public partial class SupplyOrderInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplyOrderInfoId { get; set; }

        public int SupplyOrderId { get; set; }

        public int ProductId { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime ProductionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime Expiry { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpiryDate { get; set; }

        public virtual Product Product { get; set; }
    }
}
