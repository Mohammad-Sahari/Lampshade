﻿using _01_Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        
        public string Name { get; private set; }
        public long CategoryId { get; private set; }
        public string Code { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public ProductCategory Category { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public List<ProductPicture> ProductPictures { get; private set; }
        

        public Product(string name, long categoryId, string code, string shortDescription,
            string description, string picture, string pictureAlt,
            string pictureTitle, string keywords, string metaDescription, string slug)
        {
            Name = name;
            CategoryId = categoryId;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string name, long categoryId, string code, string shortDescription,
            string description, string picture, string pictureAlt,
            string pictureTitle, string keywords, string metaDescription, string slug)
        {
            Name = name;
            CategoryId = categoryId;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
