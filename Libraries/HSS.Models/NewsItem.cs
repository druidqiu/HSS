//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HSS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Short { get; set; }
        public string FullText { get; set; }
        public bool Published { get; set; }
        public bool AllowComments { get; set; }
        public int CommentCount { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
