using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace HSS.Core.Infrastructure
{
    /// <summary>
    /// 提供当前网络应用程序中类型的信息
    /// 加载的所有程序集
    /// </summary>
    public class WebAppTypeFinder : AppDomainTypeFinder
    {
        #region Fields
        
        private bool _binFolderAssembliesLoaded;

        #endregion
        

        #region Methods

        /// <summary>
        /// 获取本地物理路径
        /// </summary>
        /// <returns>物理路径 E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HttpRuntime.BinDirectory;
            }

            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (!_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                string binPath = GetBinDirectory();
                //binPath = _webHelper.MapPath("~/bin");
                LoadMatchingAssemblies(binPath);
            }

            return base.GetAssemblies();
        }

        #endregion
    }
}
