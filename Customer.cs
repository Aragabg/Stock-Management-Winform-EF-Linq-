namespace StockManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerId { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        public int? CustomerPhone { get; set; }

        public int? CustomerFAX { get; set; }

        public int? CustomerMobile { get; set; }

        [StringLength(50)]
        public string CustomerMail { get; set; }

        [StringLength(50)]
        public string CustomerWebsite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
