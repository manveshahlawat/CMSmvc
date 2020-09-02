using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCMS.Models
{
    public class Menu
    {
        [Key]
        [DisplayName("FOOD ID")]
        public int FoodId { get; set; }

        [Required(ErrorMessage = "ENTER FOOD NAME")]
        [DisplayName("FOOD NAME")]
        public string FoodName { get; set; }

        [Required(ErrorMessage = "ENTER FOOD PRICE")]
        [DisplayName("FOOD PRICE")]
        public double FoodPrice { get; set; }
    }
}