using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static FeePay.Core.Application.Enums.FileTypeEnum;

namespace FeePay.Core.Application.Behaviors
{
    public class FileTypeExtensionsAttribute : ValidationAttribute
    {
        public FileTypeExtensionsAttribute(FileType fileType)
        {
            FileTypes = new FileType[] { fileType };
            ErrorMessage ??= "{0} should be in {1} format.";
        }
        public FileTypeExtensionsAttribute(FileType[] fileTypes)
        {
            FileTypes = fileTypes;
            ErrorMessage ??= "{0} should be in {1} format.";
        }
        public FileType[] FileTypes { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null) throw new ArgumentNullException(nameof(validationContext));


            PropertyInfo propertyInfo = validationContext.ObjectType.GetProperty(validationContext.MemberName);

            if (propertyInfo == null)
                throw new ArgumentException($"The object does not contain any property with name '{validationContext.MemberName}'");


            if (propertyInfo.PropertyType != typeof(IFormFile))
                throw new ArgumentException($"The FileAttribute is not valid on property type {propertyInfo.PropertyType}" +
                                            $"This Attribute is only valid on {typeof(IFormFile)}");


            if (value != null)
            {
                IFormFile inputFile = (IFormFile)value;

                if (inputFile.Length > 0)
                {
                    if (FileTypes != null && FileTypes.Length > 0)
                    {
                        string[] validFileTypes = FileTypes.Select(ft => ft.ToDescriptionString().ToUpperInvariant()).ToArray();
                        validFileTypes = validFileTypes.SelectMany(vft => vft.Split(',')).ToArray();
                        if (!validFileTypes.Contains(inputFile.ContentType.ToUpperInvariant()))
                        {
                            string[] validFileTypeNames = FileTypes.Select(ft => ft.ToString("G")).ToArray();
                            string validFileTypeNamesString = string.Join(",", validFileTypeNames);
                            string fileTypeErrorMessage = GetFileTypeErrorMessage(ErrorMessage, validationContext.DisplayName, validFileTypeNamesString);
                            return new ValidationResult(fileTypeErrorMessage);
                        }
                    }
                }
                else return new ValidationResult("Selected file is empty.");
            }
            return ValidationResult.Success;
        }
        private static string GetFileTypeErrorMessage(string errorMessageString, string propertyName, string fileTypeNamesString) =>
            string.Format(CultureInfo.InvariantCulture, errorMessageString, propertyName, fileTypeNamesString);
    }
}
