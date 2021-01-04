using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FeePay.Web.Extensions
{
    public class CustomVelidationExtension
    {

    }

    // Will not work with js have to create custom js method


    //[HasNumberValidation(ErrorMessage = "Password must have at least one digit.")]
    //[HasUpperCharValidation(ErrorMessage = "Password must have at least one upper case letter.")]
    //[HasLowerCharValidation(ErrorMessage = "Password must have at least one lower case letter.")]
    //[HasMiniMaxCharsValidation(Min: 8, Max: 16, ErrorMessage = "Password must have Minimum eight and maximum sixteen in length")]
    //[HasSymbolsValidation(ErrorMessage = "Password must have at least one special character(#?!@$%^&*-).")]
    public class EmailDomainValidation : ValidationAttribute
    {
        private readonly string allowDomain;
        public EmailDomainValidation(string AllowDomain)
        {
            allowDomain = AllowDomain;
        }
        public override bool IsValid(object value)
        {
            string[] doaminstring = value.ToString().Split('@');
            return doaminstring[1].ToUpper() == allowDomain.ToUpper();
        }
    }
    public class HasNumberValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Regex regex1 = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);
            return regex1.IsMatch(value.ToString());
        }
        public override string FormatErrorMessage(string name) => "This field should contain at least one lower case letter.";
    }
    public class HasUpperCharValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Regex regex1 = new Regex("^[A-Z]+$", RegexOptions.IgnoreCase);
            return regex1.IsMatch(value.ToString());
        }
        public override string FormatErrorMessage(string name) => "This field should contain at least one upper case letter.";
    }
    public class HasLowerCharValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Regex regex1 = new Regex("^[a-z]+$", RegexOptions.IgnoreCase);
            return regex1.IsMatch(value.ToString());
        }
        public override string FormatErrorMessage(string name) => "This field should contain at least one numeric value.";
    }
    public class HasMiniMaxCharsValidation : ValidationAttribute
    {
        private readonly int min;
        private readonly int max;
        public HasMiniMaxCharsValidation(int Min = 8, int Max = 15)
        {
            min = Min;
            max = Max;
        }
        public override bool IsValid(object value)
        {
            StringBuilder str = new StringBuilder();
            str.Append("^.{").Append(min).Append(",").Append(max).Append("}$");
            Regex regex1 = new Regex(str.ToString(), RegexOptions.IgnoreCase);
            return regex1.IsMatch(value.ToString());
        }
        public override string FormatErrorMessage(string name) => $"This field should not be lesser than {min} or greater than {max} characters.";
    }
    public class HasSymbolsValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Regex regex1 = new Regex("^[!@#$%^&*()_+=\\[{\\]};:<>|./?,-]$", RegexOptions.IgnoreCase);
            return regex1.IsMatch(value.ToString());
        }
        public override string FormatErrorMessage(string name) => "This field should contain at least one special case character.";
    }
}
