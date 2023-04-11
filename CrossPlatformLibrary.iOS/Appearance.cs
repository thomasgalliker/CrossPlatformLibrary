using UIKit;

namespace CrossPlatformLibrary.iOS
{
    public static class Appearance
    {
        public static void SetTintColor(UIColor color)
        {
            //UINavigationBar.Appearance.BarTintColor = tintColor;
            //UINavigationBar.Appearance.TintColor = textColor;
            //UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
            //{
            //    ForegroundColor = textColor,
            //};

            //UIProgressView.Appearance.ProgressTintColor = tintColor;

            //UISlider.Appearance.MinimumTrackTintColor = tintColor;
            //UISlider.Appearance.MaximumTrackTintColor = tintColor;
            //UISlider.Appearance.ThumbTintColor = tintColor;

            //UISwitch.Appearance.OnTintColor = tintColor;

            //UITableViewHeaderFooterView.Appearance.TintColor = tintColor;

            //UITableView.Appearance.SectionIndexBackgroundColor = tintColor;
            //UITableView.Appearance.SeparatorColor = tintColor;

            //UITextField.Appearance.TintColor = tintColor;

            //UIButton.Appearance.TintColor = tintColor;
            //UIButton.Appearance.SetTitleColor(tintColor, UIControlState.Normal);

            //UIPickerView.Appearance.BackgroundColor = UIColor.Blue;
            UIPickerView.Appearance.TintColor = color;
           
            //UIToolbar.Appearance.BarTintColor = UIColor.Blue;
            UIToolbar.Appearance.TintColor = color;

            UIBarButtonItem.AppearanceWhenContainedIn(typeof(UIToolbar)).TintColor = color;

            UIView.AppearanceWhenContainedIn(typeof(UIAlertView)).TintColor = color;
            UIView.AppearanceWhenContainedIn(typeof(UIAlertController)).TintColor = color;

            //UISegmentedControl.Appearance.TintColor = tintColor;
        }
    }
}