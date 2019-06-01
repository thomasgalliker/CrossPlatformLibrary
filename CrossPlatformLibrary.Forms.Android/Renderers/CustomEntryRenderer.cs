﻿using Android.Content;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            if (this.Element is CustomEntry customEntry)
            {
                var paddingLeft = (int)customEntry.Padding.Left;
                var paddingTop = (int)customEntry.Padding.Top;
                var paddingRight = (int)customEntry.Padding.Right;
                var paddingBottom = (int)customEntry.Padding.Bottom;
                this.Control.SetPadding(paddingLeft, paddingTop, paddingRight, paddingBottom);
            }
        }
    }
}