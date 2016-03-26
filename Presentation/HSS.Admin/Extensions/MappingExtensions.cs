using AutoMapper;
using HSS.Admin.Models.Localization;
using HSS.Admin.Models.Logging;
using HSS.Admin.Models.News;
using HSS.Models;
using HSS.Models.CommonEnum;

namespace ZhengHe.Web.Extensions
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        #region Category

        //public static CategoryModel ToModel(this Category entity)
        //{
        //    return entity.MapTo<Category, CategoryModel>();
        //}

        //public static Category ToEntity(this CategoryModel model)
        //{
        //    return model.MapTo<CategoryModel, Category>();
        //}

        //public static Category ToEntity(this CategoryModel model, Category destination)
        //{
        //    return model.MapTo(destination);
        //}

        #endregion
        
        #region Customer attributes

        //attributes
        //public static CustomerAttributeModel ToModel(this CustomerAttribute entity)
        //{
        //    return entity.MapTo<CustomerAttribute, CustomerAttributeModel>();
        //}

        //public static CustomerAttribute ToEntity(this CustomerAttributeModel model)
        //{
        //    return model.MapTo<CustomerAttributeModel, CustomerAttribute>();
        //}

        //public static CustomerAttribute ToEntity(this CustomerAttributeModel model, CustomerAttribute destination)
        //{
        //    return model.MapTo(destination);
        //}

        #endregion        

        #region Languages

        public static LanguageModel ToModel(this Language entity)
        {
            return entity.MapTo<Language, LanguageModel>();
        }

        public static Language ToEntity(this LanguageModel model)
        {
            return model.MapTo<LanguageModel, Language>();
        }

        public static Language ToEntity(this LanguageModel model, Language destination)
        {
            return model.MapTo(destination);
        }

        #endregion
                
        #region Log

        public static LogModel ToModel(this Log entity)
        {
            return entity.MapTo<Log, LogModel>();
        }

        public static Log ToEntity(this LogModel model)
        {
            return model.MapTo<LogModel, Log>();
        }

        public static Log ToEntity(this LogModel model, Log destination)
        {
            return model.MapTo(destination);
        }

        public static ActivityLogTypeModel ToModel(this ActivityLogType entity)
        {
            return entity.MapTo<ActivityLogType, ActivityLogTypeModel>();
        }

        public static ActivityLogModel ToModel(this ActivityLog entity)
        {
            return entity.MapTo<ActivityLog, ActivityLogModel>();
        }

        #endregion
                
        #region News

        //news items
        public static NewsItemModel ToModel(this NewsItem entity)
        {
            return entity.MapTo<NewsItem, NewsItemModel>();
        }

        public static NewsItem ToEntity(this NewsItemModel model)
        {
            return model.MapTo<NewsItemModel, NewsItem>();
        }

        public static NewsItem ToEntity(this NewsItemModel model, NewsItem destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Customer roles

        //customer roles
        //public static CustomerRoleModel ToModel(this CustomerRole entity)
        //{
        //    return entity.MapTo<CustomerRole, CustomerRoleModel>();
        //}

        //public static CustomerRole ToEntity(this CustomerRoleModel model)
        //{
        //    return model.MapTo<CustomerRoleModel, CustomerRole>();
        //}

        //public static CustomerRole ToEntity(this CustomerRoleModel model, CustomerRole destination)
        //{
        //    return model.MapTo(destination);
        //}

        #endregion
        

        //public static NewsSettingsModel ToModel(this NewsSettings entity)
        //{
        //    return entity.MapTo<NewsSettings, NewsSettingsModel>();
        //}
        //public static NewsSettings ToEntity(this NewsSettingsModel model, NewsSettings destination)
        //{
        //    return model.MapTo(destination);
        //}


        //public static CatalogSettingsModel ToModel(this CatalogSettings entity)
        //{
        //    return entity.MapTo<CatalogSettings, CatalogSettingsModel>();
        //}
        //public static CatalogSettings ToEntity(this CatalogSettingsModel model, CatalogSettings destination)
        //{
        //    return model.MapTo(destination);
        //}


        //public static RewardPointsSettingsModel ToModel(this RewardPointsSettings entity)
        //{
        //    return entity.MapTo<RewardPointsSettings, RewardPointsSettingsModel>();
        //}
        //public static RewardPointsSettings ToEntity(this RewardPointsSettingsModel model, RewardPointsSettings destination)
        //{
        //    return model.MapTo(destination);
        //}


        //public static MediaSettingsModel ToModel(this MediaSettings entity)
        //{
        //    return entity.MapTo<MediaSettings, MediaSettingsModel>();
        //}
        //public static MediaSettings ToEntity(this MediaSettingsModel model, MediaSettings destination)
        //{
        //    return model.MapTo(destination);
        //}

        //customer/user settings
        //public static CustomerUserSettingsModel.CustomerSettingsModel ToModel(this CustomerSettings entity)
        //{
        //    return entity.MapTo<CustomerSettings, CustomerUserSettingsModel.CustomerSettingsModel>();
        //}
        //public static CustomerSettings ToEntity(this CustomerUserSettingsModel.CustomerSettingsModel model, CustomerSettings destination)
        //{
        //    return model.MapTo(destination);
        //}

        //#endregion
        

        #region Templates

        //public static CategoryTemplateModel ToModel(this CategoryTemplate entity)
        //{
        //    return entity.MapTo<CategoryTemplate, CategoryTemplateModel>();
        //}

        //public static CategoryTemplate ToEntity(this CategoryTemplateModel model)
        //{
        //    return model.MapTo<CategoryTemplateModel, CategoryTemplate>();
        //}

        //public static CategoryTemplate ToEntity(this CategoryTemplateModel model, CategoryTemplate destination)
        //{
        //    return model.MapTo(destination);
        //}


        #endregion


    }
}