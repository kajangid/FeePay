using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FeePay.Core.Application.Behaviors;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace FeePay.Web.Extensions
{
    public class FileTypeExtensionsAttributeAdapter : AttributeAdapterBase<FileTypeExtensionsAttribute>
    {
        public FileTypeExtensionsAttributeAdapter(FileTypeExtensionsAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }
        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            string[] validFileTypeNames = Attribute.FileTypes.Select(ft => ft.ToString("G")).ToArray();
            string validFileTypeNamesString = string.Join(",", validFileTypeNames);

            AddAttribute(context.Attributes, "data-val", "true");
            AddAttribute(context.Attributes, "data-val-filetype", GetErrorMessage(context));
            AddAttribute(context.Attributes, "data-val-filetype-validtypes", validFileTypeNamesString);
        }
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null) throw new ArgumentNullException(nameof(validationContext));

            string propertyDisplayName = validationContext.ModelMetadata.GetDisplayName();
            string[] validFileTypeNames = Attribute.FileTypes.Select(ft => ft.ToString("G")).ToArray();
            string validFileTypeNamesString = string.Join(",", validFileTypeNames);
            return string.Format(CultureInfo.InvariantCulture, Attribute.ErrorMessage, propertyDisplayName, validFileTypeNamesString);            
            //return GetErrorMessage(validationContext.ModelMetadata, propertyDisplayName, validFileTypeNamesString);
        }
        private static void AddAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (!attributes.ContainsKey(key))
            {
                attributes.Add(key, value);
            }
        }
    }
}
