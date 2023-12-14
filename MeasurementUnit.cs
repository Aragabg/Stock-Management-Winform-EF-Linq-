namespace StockManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MeasurementUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MeasurementId { get; set; }

        [StringLength(50)]
        public string MeasurementUnitName { get; set; }

        public int Price { get; set; }

        public int ProductId { get; set; }

        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }
    }
}
