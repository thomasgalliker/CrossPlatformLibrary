using System;
using CrossPlatformLibrary.Extensions;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    /// <summary>
    /// Apply this behavior to an <seealso cref="Entry"/> in order to trim Entry's Text property if the Unfocused event is raised.
    /// </summary>
    /// 
    /// <example>
    /// <Entry Placeholder="Entry 1">
    ///     <Entry.Behaviors>
    ///         <behaviors:EntryUnfocusedBehavior DecorationFlags="All" />
    /// </Entry.Behaviors >
    /// </Entry >
    /// 
    /// <Entry Placeholder="Entry 2">
    ///     <Entry.Behaviors>
    ///         <behaviors:EntryUnfocusedBehavior DecorationFlags="Trim" />
    /// </Entry.Behaviors >
    /// </Entry >
    /// </example>
    public class EntryUnfocusedBehavior : BehaviorBase<VisualElement>
    {
        public static readonly BindableProperty DecorationFlagsProperty =
            BindableProperty.Create(
                nameof(DecorationFlags),
                typeof(TextDecorationFlags),
                typeof(EntryUnfocusedBehavior));

        public TextDecorationFlags DecorationFlags
        {
            get => (TextDecorationFlags)this.GetValue(DecorationFlagsProperty);
            set => this.SetValue(DecorationFlagsProperty, value);
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);

            var entry = bindable.AsEntry();
            if (entry == null)
            {
                throw new InvalidOperationException("bindable must be of type Entry or ValidatableEntry");
            }

            entry.Unfocused += this.OnEntryUnfocused;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            var entry = bindable.AsEntry();
            entry.Unfocused -= this.OnEntryUnfocused;

            base.OnDetachingFrom(bindable);
        }

        private void OnEntryUnfocused(object sender, EventArgs e)
        {
            if (!(sender is Entry entry))
            {
                return;
            }

            if (entry.Text is string value && 
                this.DecorationFlags is TextDecorationFlags flags && flags != TextDecorationFlags.None)
            {
                if (flags.HasFlag(TextDecorationFlags.TrimWhitespaces))
                {
                    value = value.TrimWhitespaces();
                }

                if (flags.HasFlag(TextDecorationFlags.TrimStart))
                {
                    value = value.TrimStart();
                }

                if (flags.HasFlag(TextDecorationFlags.TrimEnd))
                {
                    value = value.TrimEnd();
                }
                entry.Text = value;
            }
        }
    }

    [Flags]
    public enum TextDecorationFlags
    {
        None = 0,
        TrimStart = 1,
        TrimEnd = 2,
        Trim = TrimStart | TrimEnd,
        TrimWhitespaces = 4,
        All = Trim | TrimWhitespaces,
    }
}
