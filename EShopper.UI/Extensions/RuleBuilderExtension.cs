using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EShopper.UI.Extensions
{
    public static class RuleBuilderExtension
    {
        public static IRuleBuilder<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage("Please enter your Email Address!")
                .Must(IsEmailValid).WithMessage("Please enter valid Email Address!");
            return options;
        }

        public static IRuleBuilder<T, string> Username<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage("Please enter your Username!");
            return options;
        }

        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage("Please enter your Password!")
                .MinimumLength(6).WithMessage("Your Password must be 6 characters!")
                .Matches("[A-Z]").WithMessage("Your Password must contain at least one Uppercased letter!")
                .Matches("[a-z]").WithMessage("Your Password must contain at least one Lowercased letter!")
                .Matches("[0-9]").WithMessage("Your Password must contain at least one Number!");
            return options;
        }

        public static IRuleBuilder<T, string> Fullname<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .MaximumLength(100).WithMessage("Your Name must be string with length of 100!")
                .Must(IsValidName).WithMessage("Please enter valid Name!");
            return options;
        }

        #region Private Functions

        private static bool IsValidName(string name)
        {
            var allowedNameCharacters = new Regex(@"^[\p{L} \.'\-]+$");

            if (name == null || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return true;
            }

            if (allowedNameCharacters.IsMatch(name))
            {
                return true;
            }

            return false;
        }

        private static bool IsEmailValid(string email)
        {
            var allowedEmailCharacters = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            if (allowedEmailCharacters.IsMatch(email))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
