using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.iOS.Controls
{
    public class UITextFieldPadding : UITextField
    {
        private Thickness padding = new Thickness(0);

        public Thickness Padding
        {
            get => this.padding;
            set
            {
                if (this.padding != value)
                {
                    this.padding = value;
                    //this.InvalidateIntrinsicContentSize();
                }
            }
        }

        public UITextFieldPadding()
        {
        }

        public UITextFieldPadding(NSCoder coder) : base(coder)
        {
        }

        public UITextFieldPadding(CGRect rect) : base(rect)
        {
        }

        public override CGRect TextRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets((float)this.Padding.Top, (float)this.Padding.Left, (float)this.Padding.Bottom, (float)this.Padding.Right);
            return insets.InsetRect(forBounds);
        }

        public override CGRect PlaceholderRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets((float)this.Padding.Top, (float)this.Padding.Left, (float)this.Padding.Bottom, (float)this.Padding.Right);
            return insets.InsetRect(forBounds);
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets((float)this.Padding.Top, (float)this.Padding.Left, (float)this.Padding.Bottom, (float)this.Padding.Right);
            return insets.InsetRect(forBounds);
        }
    }
}