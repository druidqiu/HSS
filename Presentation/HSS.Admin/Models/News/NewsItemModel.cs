using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using HSS.Web.Framework.Mvc;
using HSS.Web.Framework;
using HSS.Admin.Validators.News;

namespace HSS.Admin.Models.News
{
    [Validator(typeof(NewsItemValidator))]
    public partial class NewsItemModel : BaseEntityModel
    {
        

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.Short")]
        [AllowHtml]
        public string Short { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.AllowComments")]
        public bool AllowComments { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.StartDate")]
        [UIHint("DateTimeNullable")]
        public DateTime? StartDate { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.EndDate")]
        [UIHint("DateTimeNullable")]
        public DateTime? EndDate { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.Published")]
        public bool Published { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.Comments")]
        public int Comments { get; set; }

        [ResourceDisplayName("Admin.ContentManagement.News.NewsItems.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}