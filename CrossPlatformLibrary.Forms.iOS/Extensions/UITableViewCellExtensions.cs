using UIKit;

namespace CrossPlatformLibrary.Forms.iOS.Extensions
{
    public static class UITableViewCellExtensions
    {
        public static void SetDisclosure(this UITableViewCell cell, string id)
        {
            if (id != "none")
            {
                if (id != "checkmark")
                {
                    if (id != "detail-button")
                    {
                        if (id != "detail-disclosure-button")
                        {
                            if (id == "disclosure" || id == "disclosure-indicator") cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                            else cell.Accessory = UITableViewCellAccessory.None;
                        }
                        else cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
                    }
                    else cell.Accessory = UITableViewCellAccessory.DetailButton;
                }
                else cell.Accessory = UITableViewCellAccessory.Checkmark;
            }
            else cell.Accessory = UITableViewCellAccessory.None;
        }
    }
}