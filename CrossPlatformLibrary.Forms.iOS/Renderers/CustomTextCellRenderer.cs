using System;
using CrossPlatformLibrary.Forms.iOS.Extensions;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TextCell), typeof(CustomTextCellRenderer))]
namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    // http://motzcod.es/post/126046400067/adding-a-disclosure-indicator-accessory-to
    public class CustomTextCellRenderer : TextCellRenderer
    {
        public static void Init()
        {
            var test = DateTime.UtcNow;
        }

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SetDisclosure(item.StyleId);
            return cell;
        }
    }
}

