using HSS.Core;
using HSS.Core.Infrastructure;
using HSS.Services.Localization;
using HSS.Web.Framework.Mvc;

namespace HSS.Web.Framework
{
    public class ResourceDisplayName : System.ComponentModel.DisplayNameAttribute, IModelAttribute
    {
        private string _resourceValue = string.Empty;
        //private bool _resourceValueRetrived;

        public ResourceDisplayName(string resourceKey)
            : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public string ResourceKey { get; set; }

        public override string DisplayName
        {
            get
            {
                var langId = EngineContext.Current.Resolve<IWorkContext>().WorkingLanguage.Id;
                _resourceValue = EngineContext.Current
                    .Resolve<ILocalizationService>()
                    .GetResource(ResourceKey, langId, true, ResourceKey);
                return _resourceValue;
            }
        }

        public string Name
        {
            get { return "ResourceDisplayName"; }
        }
    }
}
