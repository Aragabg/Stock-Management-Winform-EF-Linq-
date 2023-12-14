namespace StockManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transfering")]
    public partial class Transfering
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int From { get; set; }

        public int To { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
    }
}
