using AutoMapper;
using HSS.Admin.Models.Localization;
using HSS.Admin.Models.Logging;
using HSS.Admin.Models.News;
using HSS.Core.Infrastructure;
using HSS.Models;

namespace HSS.Admin.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            
            //language
            Mapper.CreateMap<Language, LanguageModel>()
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            Mapper.CreateMap<LanguageModel, Language>();
           
            //topcis
            //Mapper.CreateMap<Topic, TopicModel>()
            //    .ForMember(dest => dest.AvailableTopicTemplates, mo => mo.Ignore())
            //    .ForMember(dest => dest.Url, mo => mo.Ignore())
            //    .ForMember(dest => dest.SeName, mo => mo.MapFrom(src => src.GetSeName(0, true, false)))
            //    .ForMember(dest => dest.Locales, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableStores, mo => mo.Ignore())
            //    .ForMember(dest => dest.SelectedStoreIds, mo => mo.Ignore())
            //    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            //Mapper.CreateMap<TopicModel, Topic>();

            //category
            //Mapper.CreateMap<Category, CategoryModel>()
            //    .ForMember(dest => dest.AvailableCategoryTemplates, mo => mo.Ignore())
            //    .ForMember(dest => dest.Locales, mo => mo.Ignore())
            //    .ForMember(dest => dest.Breadcrumb, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableCategories, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableDiscounts, mo => mo.Ignore())
            //    .ForMember(dest => dest.SelectedDiscountIds, mo => mo.Ignore())
            //    .ForMember(dest => dest.SeName, mo => mo.MapFrom(src => src.GetSeName(0, true, false)))
            //    .ForMember(dest => dest.AvailableCustomerRoles, mo => mo.Ignore())
            //    .ForMember(dest => dest.SelectedCustomerRoleIds, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableStores, mo => mo.Ignore())
            //    .ForMember(dest => dest.SelectedStoreIds, mo => mo.Ignore())
            //    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            //Mapper.CreateMap<CategoryModel, Category>()
            //    .ForMember(dest => dest.HasDiscountsApplied, mo => mo.Ignore())
            //    .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
            //    .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
            //    .ForMember(dest => dest.Deleted, mo => mo.Ignore())
            //    .ForMember(dest => dest.AppliedDiscounts, mo => mo.Ignore());
           

            //products
            //Mapper.CreateMap<Product, ProductModel>()
            //    .ForMember(dest => dest.ProductTypeName, mo => mo.Ignore())
            //    .ForMember(dest => dest.AssociatedToProductId, mo => mo.Ignore())
            //    .ForMember(dest => dest.AssociatedToProductName, mo => mo.Ignore())
            //    .ForMember(dest => dest.StockQuantityStr, mo => mo.Ignore())
            //    .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
            //    .ForMember(dest => dest.UpdatedOn, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductTags, mo => mo.Ignore())
            //    .ForMember(dest => dest.PictureThumbnailUrl, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableVendors, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableProductTemplates, mo => mo.Ignore())
            //    .ForMember(dest => dest.Locales, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableCategories, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableManufacturers, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableProductAttributes, mo => mo.Ignore())
            //    .ForMember(dest => dest.AddPictureModel, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductPictureModels, mo => mo.Ignore())
            //    .ForMember(dest => dest.AddSpecificationAttributeModel, mo => mo.Ignore())
            //    .ForMember(dest => dest.CopyProductModel, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductWarehouseInventoryModels, mo => mo.Ignore())
            //    .ForMember(dest => dest.IsLoggedInAsVendor, mo => mo.Ignore())
            //    .ForMember(dest => dest.SeName, mo => mo.MapFrom(src => src.GetSeName(0, true, false)))
            //    .ForMember(dest => dest.AvailableCustomerRoles, mo => mo.Ignore())
            //    .ForMember(dest => dest.SelectedCustomerRoleIds, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableStores, mo => mo.Ignore())
            //    .ForMember(dest => dest.SelectedStoreIds, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableTaxCategories, mo => mo.Ignore())
            //    .ForMember(dest => dest.PrimaryStoreCurrencyCode, mo => mo.Ignore())
            //    .ForMember(dest => dest.BaseDimensionIn, mo => mo.Ignore())
            //    .ForMember(dest => dest.BaseWeightIn, mo => mo.Ignore())
            //    .ForMember(dest => dest.Locales, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableDiscounts, mo => mo.Ignore())
            //    .ForMember(dest => dest.SelectedDiscountIds, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableDeliveryDates, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableWarehouses, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableBasepriceUnits, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableBasepriceBaseUnits, mo => mo.Ignore())
            //    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            //Mapper.CreateMap<ProductModel, Product>()
            //    .ForMember(dest => dest.ProductTags, mo => mo.Ignore())
            //    .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
            //    .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
            //    .ForMember(dest => dest.ParentGroupedProductId, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductType, mo => mo.Ignore())
            //    .ForMember(dest => dest.Deleted, mo => mo.Ignore())
            //    .ForMember(dest => dest.ApprovedRatingSum, mo => mo.Ignore())
            //    .ForMember(dest => dest.NotApprovedRatingSum, mo => mo.Ignore())
            //    .ForMember(dest => dest.ApprovedTotalReviews, mo => mo.Ignore())
            //    .ForMember(dest => dest.NotApprovedTotalReviews, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductCategories, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductManufacturers, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductPictures, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductReviews, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductSpecificationAttributes, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductWarehouseInventory, mo => mo.Ignore())
            //    .ForMember(dest => dest.HasTierPrices, mo => mo.Ignore())
            //    .ForMember(dest => dest.HasDiscountsApplied, mo => mo.Ignore())
            //    .ForMember(dest => dest.BackorderMode, mo => mo.Ignore())
            //    .ForMember(dest => dest.DownloadActivationType, mo => mo.Ignore())
            //    .ForMember(dest => dest.GiftCardType, mo => mo.Ignore())
            //    .ForMember(dest => dest.LowStockActivity, mo => mo.Ignore())
            //    .ForMember(dest => dest.ManageInventoryMethod, mo => mo.Ignore())
            //    .ForMember(dest => dest.RecurringCyclePeriod, mo => mo.Ignore())
            //    .ForMember(dest => dest.RentalPricePeriod, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductAttributeMappings, mo => mo.Ignore())
            //    .ForMember(dest => dest.ProductAttributeCombinations, mo => mo.Ignore())
            //    .ForMember(dest => dest.TierPrices, mo => mo.Ignore())
            //    .ForMember(dest => dest.AppliedDiscounts, mo => mo.Ignore());
            //logs
            Mapper.CreateMap<Log, LogModel>()
                .ForMember(dest => dest.CustomerEmail, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            Mapper.CreateMap<LogModel, Log>()
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
                .ForMember(dest => dest.LogLevelId, mo => mo.Ignore());
            //ActivityLogType
            //Mapper.CreateMap<ActivityLogTypeModel, ActivityLogType>()
            //    .ForMember(dest => dest.SystemKeyword, mo => mo.Ignore());
            //Mapper.CreateMap<ActivityLogType, ActivityLogTypeModel>()
            //    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            //Mapper.CreateMap<ActivityLog, ActivityLogModel>()
            //    .ForMember(dest => dest.ActivityLogTypeName, mo => mo.MapFrom(src => src.ActivityLogType.Name))
            //    .ForMember(dest => dest.CustomerEmail, mo => mo.MapFrom(src => src.Customer.Email))
            //    .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
            //    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
           
            //news
            Mapper.CreateMap<NewsItem, NewsItemModel>()
                .ForMember(dest => dest.Comments, mo => mo.Ignore())
                .ForMember(dest => dest.StartDate, mo => mo.Ignore())
                .ForMember(dest => dest.EndDate, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            Mapper.CreateMap<NewsItemModel, NewsItem>()
                .ForMember(dest => dest.CommentCount, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore());
            //customer roles
            //Mapper.CreateMap<CustomerRole, CustomerRoleModel>()
            //    .ForMember(dest => dest.PurchasedWithProductName, mo => mo.Ignore())
            //    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            //Mapper.CreateMap<CustomerRoleModel, CustomerRole>()
            //    .ForMember(dest => dest.PermissionRecords, mo => mo.Ignore());

            //product attributes
            //Mapper.CreateMap<ProductAttribute, ProductAttributeModel>()
            //    .ForMember(dest => dest.Locales, mo => mo.Ignore())
            //    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            //Mapper.CreateMap<ProductAttributeModel, ProductAttribute>();
           
        }
        
        public int Order
        {
            get { return 0; }
        }
    }
}