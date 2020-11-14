using System;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    /// <summary>
    /// Apply this behavior to an <seealso cref="Entry"/> in order to focus the next <seealso cref="VisualElement"/>
    /// if the Entry's Completed event is raised. The next element can either be <see cref="TargetElement"/> (supports binding) or <see cref="TargetElementName"/> (static string).
    /// </summary>
    /// <example>
    /// <Entry Placeholder="Field 1">
    ///     <Entry.Behaviors>
    ///         <behaviors:EntryFocusBehavior TargetElement="{x:Reference Entry2}" />
    /// </Entry.Behaviors >
    /// </Entry >
    /// </example>
    public class EntryFocusBehavior : BehaviorBase<Entry>
    {
        public static readonly BindableProperty TargetElementProperty =
            BindableProperty.Create(
                nameof(TargetElement),
                typeof(VisualElement),
                typeof(EntryFocusBehavior));

        public VisualElement TargetElement
        {
            get => (VisualElement)this.GetValue(TargetElementProperty);
            set => this.SetValue(TargetElementProperty, value);
        }

        public string TargetElementName { get; set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            this.AssociatedObject.Completed += this.OnEntryCompleted;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            this.AssociatedObject.Completed -= this.OnEntryCompleted;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TargetElementName))
            {
                this.TargetElement?.Focus();
            }
            else
            {
                var parent = ((VisualElement)sender).Parent;
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
