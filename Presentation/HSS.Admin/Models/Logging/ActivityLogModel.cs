using System;
using HSS.Web.Framework;
using HSS.Web.Framework.Mvc;

namespace HSS.Admin.Models.Logging
{
    public partial class ActivityLogModel : BaseEntityModel
    {
        [ResourceDisplayName("Admin.Configuration.ActivityLog.ActivityLog.Fields.ActivityLogType")]
        public string ActivityLogTypeName { get; set; }
        [ResourceDisplayName("Admin.Configuration.ActivityLog.ActivityLog.Fields.Customer")]
        public int CustomerId { get; set; }
        [ResourceDisplayName("Admin.Configuration.ActivityLog.ActivityLog.Fields.Customer")]
        public string CustomerEmail { get; set; }
        [ResourceDisplayName("Admin.Configuration.ActivityLog.ActivityLog.Fields.Comment")]
        public string Comment { get; set; }
        [ResourceDisplayName("Admin.Configuration.ActivityLog.ActivityLog.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}
