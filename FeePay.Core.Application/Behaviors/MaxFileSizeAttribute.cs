using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FeePay.Core.Application.Behaviors
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        public int MaxSize;
        public MaxFileSizeAttribute(int maxSize)
        {
            MaxSize = maxSize;
            ErrorMessage ??= "{0} should be not more than {1}.";
        }
        private string MaxSizeAndUnit => MaxSize >= 1024 ? Math.Round(MaxSize / 1024M, 2) + " MB" : MaxSize + " KB";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null) throw new ArgumentNullException(nameof(validationContext));

            PropertyInfo propertyInfo = validationContext.ObjectType.GetProperty(validationContext.MemberName);

            if (propertyInfo == null)
                throw new ArgumentException($"The object does not contain any property with name '{validationContext.MemberName}'");


            if (propertyInfo.PropertyType != typeof(IFormFile))
                throw new ArgumentException($@"The FileAttribute is not valid on property type {propertyInfo.PropertyType} 
                                                            This Attribute is only valid on {typeof(IFormFile)}");


            if (value != null)
            {
                IFormFile inputFile = (IFormFile)value;

                if (inputFile.Length > 0)
                {
                    long fileLengthInKByte = inputFile.Length / 1024;
                    if (MaxSize > 0 && fileLengthInKByte > MaxSize)
                    {
                        string formattedErrorMessage = string.Format(CultureInfo.InvariantCulture, ErrorMessage, validationContext.DisplayName, MaxSizeAndUnit);
                        return new ValidationResult(formattedErrorMessage);
                    }
                }
                else return new ValidationResult("Selected file is empty.");
            }
            return ValidationResult.Success;
        }
    }
}
