using System;
using System.Reflection;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    internal static class BindingHelper
    {
        internal static string GetDisplayMemberString(object item, string displayMemberPath)
        {
            return GetDisplayMember(item, displayMemberPath)?.ToString();
        }

        internal static object GetDisplayMember(object item, string displayMemberPath)
        {
            var type = item.GetType();
            var prop = type.GetRuntimeProperty(displayMemberPath);
            if (prop == null)
            {
                throw new InvalidOperationException($"DisplayMemberPath={displayMemberPath} does not exist on item of type {type.Name}.");
            }
            var propValue = prop.GetValue(item);
            return propValue;
        }

        /// <summary>
        ///     Create UI content from a <see cref="DataTemplate" /> (or optionally a <see cref="DataTemplateSelector" />).
        /// </summary>
        /// <param name="template">The <see cref="DataTemplate" />.</param>
        /// <param name="item">The view model object.</param>
        /// <param name="container">The <see cref="BindableObject" /> that will be the parent to the content.</param>
        /// <returns>The content created by the template.</returns>
        public static View CreateContent(DataTemplate template, object item, BindableObject container)
        {
            if (template is DataTemplateSelector selector)
            {
                template = selector.SelectTemplate(item, container);
            }

            var content = template.CreateContent();

            View view;
            if (content is ViewCell viewCell)
            {
                view = viewCell.View;
            }
            else
            {
                view = content as View;
                if (view == null)
                {
                    throw new Exception("ItemTemplate must either be a View or a ViewCell");
                }
            }

            return view;
        }
    }
}