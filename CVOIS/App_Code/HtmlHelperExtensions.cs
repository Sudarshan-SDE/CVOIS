using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace CVOIS.App_Code
{
    public static class HtmlHelperExtensions
    {
        public static string LabelWithAsteriskFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            // Get property name from the expression
            string propertyName = GetPropertyName(expression);
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property not found");

            // Get the property info from the model
            var propertyInfo = typeof(TModel).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            // Check if the property has the Required attribute
            bool isRequired = propertyInfo?.GetCustomAttributes(typeof(RequiredAttribute), false).Any() ?? false;

            // Create the label tag
            var labelTag = new TagBuilder("label");
            labelTag.Attributes.Add("for", htmlHelper.IdFor(expression));

            // Get the display name
            string displayName = htmlHelper.DisplayNameFor(expression);

            // Build label text with spacing fix
            labelTag.InnerHtml.Append(displayName + " "); // Ensure a space before the asterisk

            // Add an asterisk if the field is required
            if (isRequired)
            {
                var asterisk = new TagBuilder("span");
                asterisk.Attributes.Add("style", "color: red; font-weight: bold;");
                asterisk.InnerHtml.Append("*"); // No extra space here, it's already added
                labelTag.InnerHtml.AppendHtml(asterisk);
            }

            // Render label as a string
            using var writer = new System.IO.StringWriter();
            labelTag.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            return writer.ToString();
        }

        // Helper method to extract property name from the expression
        private static string GetPropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            throw new ArgumentException("Invalid property expression");
        }
    }
}
