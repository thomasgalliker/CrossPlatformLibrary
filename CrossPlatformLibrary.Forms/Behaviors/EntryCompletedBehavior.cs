using System;
using CrossPlatformLibrary.Extensions;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    /// <summary>
    /// Apply this behavior to an <seealso cref="Entry"/> in order to focus the next <seealso cref="VisualElement"/>
    /// if the Entry's Completed event is raised. The next element can either be <see cref="TargetElement"/> (supports binding) or <see cref="TargetElementName"/> (static string).
    /// </summary>
    /// 
    /// <example>
    /// <Entry Placeholder="Entry 1">
    ///     <Entry.Behaviors>
    ///         <behaviors:EntryCompletedBehavior TargetElement="{x:Reference Entry2}" />
    /// </Entry.Behaviors >
    /// </Entry >
    /// 
    /// <Entry Placeholder="Entry 2">
    ///     <Entry.Behaviors>
    ///         <behaviors:EntryCompletedBehavior TargetElementName="Entry2" />
    /// </Entry.Behaviors >
    /// </Entry >
    /// </example>
    public class EntryCompletedBehavior : BehaviorBase<VisualElement>
    {
        public static readonly BindableProperty TargetElementProperty =
            BindableProperty.Create(
                nameof(TargetElement),
                typeof(VisualElement),
                typeof(EntryCompletedBehavior));

        public VisualElement TargetElement
        {
            get => (VisualElement)this.GetValue(TargetElementProperty);
            set => this.SetValue(TargetElementProperty, value);
        }

        public string TargetElementName { get; set; }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);

            var entry = bindable.AsEntry();
            if (entry == null)
            {
                throw new InvalidOperationException("bindable must be of type Entry or ValidatableEntry");
            }

            entry.Completed += this.OnEntryCompleted;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            var entry = bindable.AsEntry();
            entry.Completed -= this.OnEntryCompleted;

            base.OnDetachingFrom(bindable);
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TargetElementName))
            {
                var entry = this.TargetElement.AsVisualElement();
                if (entry == null)
                {
                    throw new InvalidOperationException("TargetElementName must be of type VisualElement or ValidatableEntry");
                }

                entry.Focus();
            }
            else
            {
                var parent = ((Element)sender).Parent;
                while (parent != null)
                {
                    var targetElement = parent.FindByName<VisualElement>(this.TargetElementName);
                    if (targetElement != null)
                    {
                        targetElement.Focus();
                        break;
                    }
                    else
                    {
                        parent = parent.Parent;
                    }
                }
            }
        }
    }
}
