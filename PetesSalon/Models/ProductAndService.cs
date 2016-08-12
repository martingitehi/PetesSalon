using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetesSalon.DomainClasses
{
    public class ProductAndService
    {
        [Key]
        [Display(Name = "Product #")]
        public int ProductId { get; set; }

        [Display(Name = "Product/Service Name")]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal ProductPricing { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Image")]
        public byte[] Image { get; set; }
    }
}