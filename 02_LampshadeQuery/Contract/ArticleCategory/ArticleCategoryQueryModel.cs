﻿using _02_LampshadeQuery.Contract.Article;

namespace _02_LampshadeQuery.Contract.ArticleCategory
{
    public class ArticleCategoryQueryModel
    {
        public string Name { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string Description { get;  set; }
        public int DisplayOrder { get;  set; }
        public string Slug { get;  set; }
        public string Keywords { get;  set; }
        public List<string> KeywordList { get;  set; }
        public string MetaDescription { get;  set; }
        public string CanonicalAddress { get; set; }
        public int ArticleCount { get; set; }
        public List<ArticleQueryModel> Articles { get; set; }
    }
}
