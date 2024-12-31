using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace AMBOWeb.Classes
{
    public static class InputTypes
    {
        public static string AlphaNumericWithSpace = "AlphaNumericWithSpace";
        public static string AlphaNumeric = "AlphaNumeric";
        public static string Numeric = "Numeric";
        public static string NumericWithSpace = "NumericWithSpace";
        public static string AlphaNumericSymbol1WithSpace = "AlphaNumericSymbol1WithSpace";
        public static string AlphaNumericSymbol1 = "AlphaNumericSymbol1";
        public static string Email = "Email";
        public static string Mobile = "Mobile";
    }

    public static class CustomHtmlHelpers
    {
        public static IHtmlString Custom2RadioButtonGroup(this HtmlHelper helper, string name, string label, string id1, string value1, string text1, string id2, string value2, string text2)
        {
            string output = $"<div class=\"form-group\"><label for=\"{name}\">{label}</label><br /><input type=\"radio\" id=\"{id1}\" value=\"{value1}\" name = \"{name}\"/> {text1}&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"radio\" id=\"{id2}\" value=\"{value2}\" name = \"{name}\"/> {text2}</div>";
            return new HtmlString(output);
        }

        public static IHtmlString CustomTextBox(this HtmlHelper helper, string id, string name, string label, string placeholder, string AllowedKeys)
        {
            string output = $"<div class=\"form-group\"><label for=\"{id}\">{label}</label><input type=\"text\" id=\"{id}\" class=\"form-control input-sm\" name=\"{name}\" placeholder=\"{placeholder}\" {AllowedKeys} /></div>";
            return new HtmlString(output);
        }

        public static IHtmlString CustomDropDown(this HtmlHelper helper, string id, string name, string label, string classes)
        {
            string output = $"<div class=\"form-group\"><label for=\"{id}\">{label}</label><span class=\"text-blue small clearDD\"> (<i class=\"fa fa-close\"></i>Clear)</span><select id=\"{id}\" class=\"form-control {classes}\" name=\"{name}\" style=\"width:100%\"></select></div>";
            return new HtmlString(output);
        }

        public static IHtmlString CustomDropDownMultiple(this HtmlHelper helper, string id, string name, string label, string classes)
        {
            string output = $"<div class=\"form-group\"><label for=\"{id}\">{label}</label>&nbsp;&nbsp;<span class=\"text-blue small\"><span class=\"selectAllDD\">Select All</span>/<span class=\"removeAllDD\">Remove All</span></span><select id=\"{id}\" class=\"form-control input-sm {classes}\" name=\"{name}\" style=\"width:100%\" multiple></select></div>";
            return new HtmlString(output);
        }

        public static IHtmlString CustomHiddenBox(this HtmlHelper helper, string id, string name)
        {
            string output = $"<input type=\"hidden\" id=\"{id}\" name=\"{name}\" />";
            return new HtmlString(output);
        }

        public static IHtmlString CustomModalHeader(this HtmlHelper helper, string icon, string text)
        {
            string output = $"<div class=\"modal-header custom-header\"><button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button><h4 class=\"modal-title\"><i class=\"fa {icon}\"></i> {text}</h4></div>";
            return new HtmlString(output);
        }

        public static MvcHtmlString AmboTextboxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string id, string labeltext, string placeholder, string classes, string allowedkeys)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string output = $"<div class=\"form-group\"><label for=\"{id}\">{labeltext}</label><input value=\"{metadata.Model as string}\" type=\"text\" id=\"{id}\" class=\"form-control input-sm\" name=\"{name}\" placeholder=\"{placeholder}\" {allowedkeys} /></div>";
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString Ambo2RadioButtonGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string labeltext, string id1, string value1, string text1, 
            string id2, string value2, string text2)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            string output = $"<div class=\"form-group\"><label for=\"{name}\">{labeltext}</label><br /><input type=\"radio\" id=\"{id1}\" value=\"{value1}\" name = \"{name}\"/> {text1}&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"radio\" id=\"{id2}\" value=\"{value2}\" name = \"{name}\"/> {text2}</div>";
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString AmboFileUploadFor(this HtmlHelper htmlHelper, string name, string id, string labeltext)
        {
            string output = $"<div class=\"form-group\"><label for=\"{id}\">{labeltext}</label><div class=\"input-group\"><div class=\"input-group-btn\"><button onclick=\"$('#{id}').click();\" type=\"button\" class=\"btn btn-primary\">Browse...</button></div><input id=\"{id}Value\" type=\"text\" class=\"form-control\" disabled><input name=\"{name}\" type=\"file\" class=\"hidden\" id=\"{id}\" onchange=\"$('#{id}Value').val(document.getElementById('{id}').files[0].name);\"/></div></div>";
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString AmboDropdownMultipleFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string id, string labeltext, string classes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            string output = $"<div class=\"form-group\"><label for=\"{id}\">{labeltext}</label>&nbsp;&nbsp;<span class=\"text-blue small\"><span class=\"selectAllDD\">Select All</span>/<span class=\"removeAllDD\">Remove All</span></span><select id=\"{id}\" class=\"form-control {classes}\" name=\"{name}\" style=\"width:100%\" multiple></select></div>";
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString AmboTextareaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string id, string placeholder, string classes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string output = $"<textarea id=\"{id}\" name=\"{name}\" class=\"textarea {classes}\" placeholder=\"{placeholder}\" style=\"width:100%; height:150px; font-size: 14px; line-height: 18px; padding: 10px;\">{metadata.Model as string}</textarea>";
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString AmboHiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string id)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string output = $"<input type=\"hidden\" id=\"{id}\" name=\"{name}\" />";
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString AmboDropdownFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string id, string labeltext, string classes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            string output = $"<div class=\"form-group\"><label for=\"{id}\">{labeltext}</label><span class=\"text-blue small clearDD\"> (<i class=\"fa fa-close\"></i>Clear)</span><select id=\"{id}\" class=\"form-control {classes}\" name=\"{name}\" style=\"width:100%\"></select></div>";
            return new MvcHtmlString(output);
        }
    }
}