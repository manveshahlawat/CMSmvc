using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCMS.Models
{
    public class Orders
    {
        [Key]
        [DisplayName("ORDER ID")]
        public int OrdId { get; set; }

        [Required(ErrorMessage = "ENTER QUANTITY")]
        public int Quantity { get; set; }

        [DisplayName("TOTAL BILL")]
        public Double TotalBill { get; set; }


        public string Status { get; set; }

        public int? MenuId { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu menu { get; set; }

        public int? CustId { get; set; }

        [ForeignKey("CustId")]
        public virtual Customer cust { get; set; }

        public int? VendId { get; set; }

        [ForeignKey("VendId")]
        public virtual Vendor vend { get; set; }
    }
}