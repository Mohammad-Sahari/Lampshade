﻿using _02_LampshadeQuery.Contract.Article;
using _02_LampshadeQuery.Contract.ArticleCategory;
using _02_LampshadeQuery.Query;
using BlogManagement.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infrastructure.Configuration
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            // Define ArticleCategory services in DI container
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();

            // Define Article services in DI container
            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleReposiotry, ArticleRepository>();

            // Define Article Query services in DI container
            services.AddTransient<IArticleQuery, ArticleQuery>();
            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();

            // Define EFCore services in DI container
            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
