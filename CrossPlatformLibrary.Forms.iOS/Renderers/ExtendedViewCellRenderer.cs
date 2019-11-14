using System.ComponentModel;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
        private UITableViewCell uiTableViewCell;
        private UIColor unselectedBackground;

        public override UITableViewCell GetCell(Cell cell, UITableViewCell reusableCell, UITableView tv)
        {
            this.uiTableViewCell = base.GetCell(cell, reusableCell, tv);

            this.unselectedBackground = this.uiTableViewCell.SelectedBackgroundView.BackgroundColor;

            if (cell is ExtendedViewCell extendedViewCell)
            {
                extendedViewCell.PropertyChanged -= this.ExtendedViewCell_PropertyChanged;
                extendedViewCell.PropertyChanged += this.ExtendedViewCell_PropertyChanged;

                this.UpdateBackgroundColor(extendedViewCell, extendedViewCell.IsSelected);
            }

            return this.uiTableViewCell;
        }

        private void ExtendedViewCell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is ExtendedViewCell extendedViewCell))
            {
                return;
            }

            if (e.PropertyName == ExtendedViewCell.IsSelectedProperty.PropertyName)
            {
                this.UpdateBackgroundColor(extendedViewCell, extendedViewCell.IsSelected);
            }
        }

        /// <summary>
        ///     Set background color according to IsSelected state.
        /// </summary>
        private void UpdateBackgroundColor(ExtendedViewCell extendedViewCell, bool isSelected)
        {
            if (isSelected)
            {
                this.uiTableViewCell.SelectionStyle = UITableViewCellSelectionStyle.Default;
                this.uiTableViewCell.SelectedBackgroundView = new UIView
                {
                    BackgroundColor = extendedViewCell.SelectedBackgroundColor.ToUIColor()
                };
            }
            else
            {
                this.uiTableViewCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                this.uiTableViewCell.SelectedBackgroundView = new UIView
                {
                    BackgroundColor = this.unselectedBackground
                };
            }
        }
    }
}