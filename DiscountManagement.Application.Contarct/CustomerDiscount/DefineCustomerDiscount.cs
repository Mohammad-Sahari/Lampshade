﻿
using _01_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contarct.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]

        public long ProductId { get;  set; }
        [Range(1, 99, ErrorMessage = ValidationMessages.IsRequired)]

        public int DiscountRate { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartDate { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndDate { get;  set; }
        public string EventName { get;  set; }
        public List<ProductViewModel> Products { get;  set; }
    }

}
