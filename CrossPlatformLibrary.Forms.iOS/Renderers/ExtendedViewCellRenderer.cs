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
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;

            if (item is ExtendedViewCell extendedViewCell)
            {
                cell.SelectedBackgroundView = new UIView
                {
                    BackgroundColor = extendedViewCell.SelectedBackgroundColor.ToUIColor()
                };
            }

            return cell;
        }
    }
}