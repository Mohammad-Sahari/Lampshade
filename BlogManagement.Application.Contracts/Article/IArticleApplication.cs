﻿using _01_Framework.Application;

namespace BlogManagement.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        OperationResult Edit(EditArticle command);

        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        EditArticle GetDetails(long id);
    }
}
