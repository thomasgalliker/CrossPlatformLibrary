using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
        private View cellCore;
        private Drawable unselectedBackground;
        private bool isCellPropertyChanging;

        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            this.cellCore = base.GetCellCore(item, convertView, parent, context);

            this.unselectedBackground = this.cellCore.Background;

            if (item is ExtendedViewCell extendedViewCell)
            {
                this.UpdateBackgroundColor(extendedViewCell, extendedViewCell.IsSelected);
            }

            return this.cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);

            if (e.PropertyName == "IsSelected" && this.isCellPropertyChanging == false && sender is ExtendedViewCell extendedViewCell)
            {
                try
                {
                    this.isCellPropertyChanging = true;
                    var toggled = !extendedViewCell.IsSelected;
                    this.UpdateBackgroundColor(extendedViewCell, toggled);
                    extendedViewCell.IsSelected = toggled;
                }
                finally
                {
                    this.isCellPropertyChanging = false;
                }
            }
        }

        /// <summary>
        ///     Set background color according to IsSelected state.
        /// </summary>
        private void UpdateBackgroundColor(ExtendedViewCell extendedViewCell, bool isSelected)
        {
            if (isSelected)
            {
                this.cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
            }
            else
            {
                this.cellCore.SetBackground(this.unselectedBackground);
            }
        }
    }
}