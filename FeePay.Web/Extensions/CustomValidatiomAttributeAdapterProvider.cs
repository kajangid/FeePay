using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using FeePay.Core.Application.Behaviors;

namespace FeePay.Web.Extensions
{
    public class CustomValidatiomAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        readonly IValidationAttributeAdapterProvider baseProvider = new ValidationAttributeAdapterProvider();
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is FileTypeExtensionsAttribute)
                return new FileTypeExtensionsAttributeAdapter(attribute as FileTypeExtensionsAttribute, stringLocalizer);
            if (attribute is MaxFileSizeAttribute)
                return new MaxFileSizeAttributeAdapter(attribute as MaxFileSizeAttribute, stringLocalizer); 
            else return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}