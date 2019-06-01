﻿using System;
using System.ComponentModel;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using CrossPlatformLibrary.Forms.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using PickerLineColorEffect = CrossPlatformLibrary.Forms.iOS.Effects.PickerLineColorEffect;

[assembly: ExportEffect(typeof(PickerLineColorEffect), nameof(PickerLineColorEffect))]
namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class PickerLineColorEffect : PlatformEffect // TODO : Adjust to EntryLineColorEffect
    {
        UITextField control;

        protected override void OnAttached()
        {
            try
            {
                this.control = this.Control as UITextField;
                this.Initialize();
                this.UpdateLineColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            this.control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == LineColorEffect.LineColorProperty.PropertyName ||
                args.PropertyName == "Height")
            {
                this.Initialize();
                this.UpdateLineColor();
            }
        }

        private void Initialize()
        {
            var picker = this.Element as Picker;

            if (picker != null)
            {
                this.Control.Bounds = new CGRect(0, 0, picker.Width, picker.Height);
            }
        }

        private void UpdateLineColor()
        {
            if (this.control == null)
            {
                return;
            }

            var lineLayer = this.control.Layer.Sublayers
                       .OfType<BorderLineLayer>()
                       .SingleOrDefault();

            if (lineLayer == null)
            {
                lineLayer = new BorderLineLayer();
                lineLayer.MasksToBounds = true;
                lineLayer.BorderWidth = 1.0f;
                this.control.Layer.AddSublayer(lineLayer);
                this.control.BorderStyle = UITextBorderStyle.None;
            }

            lineLayer.Frame = new CGRect(0f, this.Control.Frame.Height - 13f, this.Control.Bounds.Width, 1f);
            lineLayer.BorderColor = LineColorEffect.GetLineColor(this.Element).ToCGColor();
        }

        private class BorderLineLayer : CALayer
        {
        }
    }
}