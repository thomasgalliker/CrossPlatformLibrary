using System;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers.ShapeView;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ShapeView), typeof(ShapeViewRenderer))]
namespace CrossPlatformLibrary.Forms.iOS.Renderers.ShapeView
{
    public class ShapeViewRenderer : VisualElementRenderer<Controls.ShapeView>
    {
        private readonly float QuarterTurnCounterClockwise = (float)(-1f * (Math.PI * 0.5f));

        public ShapeViewRenderer()
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == BoxView.ColorProperty.PropertyName || e.PropertyName == Controls.ShapeView.StrokeColorProperty.PropertyName)
            {
                this.SetNeedsDisplay();
            }
        }

        public override void Draw(CGRect rect)
        {
            var currentContext = UIGraphics.GetCurrentContext();
            var properRect = this.AdjustForThickness(rect);
            this.HandleShapeDraw(currentContext, properRect);
        }

        protected RectangleF AdjustForThickness(CGRect rect)
        {
            var x = rect.X + this.Element.Padding.Left;
            var y = rect.Y + this.Element.Padding.Top;
            var width = rect.Width - this.Element.Padding.HorizontalThickness;
            var height = rect.Height - this.Element.Padding.VerticalThickness;
            return new RectangleF((float)x, (float)y, (float)width, (float)height);
        }

        protected virtual void HandleShapeDraw(CGContext currentContext, RectangleF rect)
        {
            // Only used for circles
            var centerX = rect.X + (rect.Width / 2);
            var centerY = rect.Y + (rect.Height / 2);
            var radius = rect.Width / 2;
            var startAngle = 0;
            var endAngle = (float)(Math.PI * 2);

            switch (this.Element.ShapeType)
            {
                case ShapeType.Box:
                    this.HandleStandardDraw(currentContext, rect, () =>
                    {
                        if (this.Element.CornerRadius > 0)
                        {
                            var path = UIBezierPath.FromRoundedRect(rect, this.Element.CornerRadius);
                            currentContext.AddPath(path.CGPath);
                        }
                        else
                        {
                            currentContext.AddRect(rect);
                        }
                    });
                    break;
                case ShapeType.Circle:
                    this.HandleStandardDraw(currentContext, rect, () => currentContext.AddArc(centerX, centerY, radius, startAngle, endAngle, true));
                    break;
                case ShapeType.CircleIndicator:
                    this.HandleStandardDraw(currentContext, rect, () => currentContext.AddArc(centerX, centerY, radius, startAngle, endAngle, true));
                    this.HandleStandardDraw(currentContext, rect, () => currentContext.AddArc(centerX, centerY, radius, this.QuarterTurnCounterClockwise, (float)(Math.PI * 2 * (this.Element.IndicatorPercentage / 100)) + this.QuarterTurnCounterClockwise, false), this.Element.StrokeWidth + 3);
                    break;
            }
        }

        /// <summary>
        /// A simple method for handling our drawing of the shape. This method is called differently for each type of shape
        /// </summary>
        /// <param name="currentContext">Current context.</param>
        /// <param name="rect">Rect.</param>
        /// <param name="createPathForShape">Create path for shape.</param>
        /// <param name="lineWidth">Line width.</param>
        protected virtual void HandleStandardDraw(CGContext currentContext, RectangleF rect, Action createPathForShape, float? lineWidth = null)
        {
            currentContext.SetLineWidth(lineWidth ?? this.Element.StrokeWidth);
            currentContext.SetFillColor(base.Element.Color.ToCGColor());
            currentContext.SetStrokeColor(this.Element.StrokeColor.ToCGColor());

            createPathForShape();

            currentContext.DrawPath(CGPathDrawingMode.FillStroke);
        }
    }
}
