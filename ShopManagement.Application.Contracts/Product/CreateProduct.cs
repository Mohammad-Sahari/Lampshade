﻿using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get;  set; }
        [Range(1,100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long CategoryId { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]

        public string Code { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]

        public string ShortDescription { get;  set; }
        public string Description { get;  set; }

        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]

        public string Keywords { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]

        public string MetaDescription { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]

        public string Slug { get;  set; }
        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}
