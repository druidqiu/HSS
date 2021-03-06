﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HSS.Web.Framework.Mvc
{
    public class MetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            var additionalValues = attributes.OfType<IModelAttribute>().ToList();
            foreach (var additionalValue in additionalValues)
            {
                if (metadata.AdditionalValues.ContainsKey(additionalValue.Name))
                    throw new Exception("已经存在的命名属性\"" + additionalValue.Name +
                                           "\" 在这个对象中");
                metadata.AdditionalValues.Add(additionalValue.Name, additionalValue);
            }
            return metadata;
        }
    }
}