using System;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                if (this.Control != null)
                {
                    this.Control.TextChanged -= this.UiSearchBarTextChanged;
                    this.Control.CancelButtonClicked -= this.UiSearchCancelButtonClicked;
                }
            }

            if (e.NewElement != null)
            {
                if (this.Control != null)
                {
                    this.Control.TextChanged += this.UiSearchBarTextChanged;
                    this.Control.CancelButtonClicked += this.UiSearchCancelButtonClicked;
                }
            }
        }

        private void UiSearchCancelButtonClicked(object sender, EventArgs e)
        {
            this.ExecuteSearchCommand(searchText: null);
        }

        private void UiSearchBarTextChanged(object sender, UISearchBarTextChangedEventArgs e)
        {
            this.ExecuteSearchCommand(e.SearchText);
        }

        private void ExecuteSearchCommand(string searchText)
        {
            if (string.IsNullOrEmpty(searchText) && this.Element.SearchCommand != null && this.Element.SearchCommand.CanExecute(searchText))
            {
                this.Element.SearchCommand.Execute(searchText);
            }
        }
    }
}