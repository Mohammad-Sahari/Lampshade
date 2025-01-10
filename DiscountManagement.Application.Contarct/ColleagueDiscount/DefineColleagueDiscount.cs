﻿using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contarct.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Range(1, 1000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get;  set; }
        [Range(1, 99, ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get;  set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
