using UIKit;

namespace CrossPlatformLibrary.iOS
{
    public static class Appearance
    {
        public static void Configure(UIColor tintColor, UIColor textColor)
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
            UIPickerView.Appearance.TintColor = tintColor;

            //UIToolbar.Appearance.TintColor = tintColor;
            //UIBarButtonItem.Appearance.TintColor = tintColor;

            UIView.AppearanceWhenContainedIn(typeof(UIAlertView)).TintColor = tintColor;
            UIView.AppearanceWhenContainedIn(typeof(UIAlertController)).TintColor = tintColor;

            //UISegmentedControl.Appearance.TintColor = tintColor;
        }
    }
}