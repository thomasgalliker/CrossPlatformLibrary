using System;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms.Platform.Android;

namespace CrossPlatformLibrary.Forms.Android.Renderers.ShapeView
{
    /// <summary>
    /// This is our class responsible for drawing our shapes
    /// </summary>
    public class Shape : View
    {
        private readonly float QuarterTurnCounterClockwise = -90;

        public Forms.Controls.ShapeView ShapeView { get; set; }

        // Pixel density
        private readonly float density;

        // We need to make sure we account for the padding changes
        public new int Width
        {
            get { return base.Width - (int)(this.Resize(this.ShapeView.Padding.HorizontalThickness)); }
        }

        public new int Height
        {
            get { return base.Height - (int)(this.Resize(this.ShapeView.Padding.VerticalThickness)); }
        }

        public Shape(float density, Context context) : base(context)
        {
            this.density = density;
        }

        public Shape(float density, Context context, IAttributeSet attributes) : base(context, attributes)
        {
            this.density = density;
        }

        public Shape(float density, Context context, IAttributeSet attributes, int defStyle) : base(context, attributes, defStyle)
        {
            this.density = density;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            this.HandleShapeDraw(canvas);
        }

        protected virtual void HandleShapeDraw(Canvas canvas)
        {
            // We need to account for offsetting the coordinates based on the padding
            var x = this.GetX() + this.Resize(this.ShapeView.Padding.Left);
            var y = this.GetY() + this.Resize(this.ShapeView.Padding.Top);

            switch (this.ShapeView.ShapeType)
            {
                case ShapeType.Box:
                    this.HandleStandardDraw(canvas, p =>
                    {
                        var rect = new RectF(x, y, x + this.Width, y + this.Height);
                        if (this.ShapeView.CornerRadius > 0)
                        {
                            var cr = this.Resize(this.ShapeView.CornerRadius);
                            canvas.DrawRoundRect(rect, cr, cr, p);
                        }
                        else
                        {
                            canvas.DrawRect(rect, p);
                        }
                    });
                    break;
                case ShapeType.Circle:
                    this.HandleStandardDraw(canvas, p => canvas.DrawCircle(x + this.Width / 2, y + this.Height / 2, (this.Width - 10) / 2, p));
                    break;
                case ShapeType.CircleIndicator:
                    this.HandleStandardDraw(canvas, p => canvas.DrawCircle(x + this.Width / 2, y + this.Height / 2, (this.Width - 10) / 2, p), drawFill: false);
                    this.HandleStandardDraw(canvas, p => canvas.DrawArc(new RectF(x, y, x + this.Width, y + this.Height), this.QuarterTurnCounterClockwise, 360 * (this.ShapeView.IndicatorPercentage / 100), false, p), this.ShapeView.StrokeWidth + 3, false);
                    break;
            }
        }

        /// <summary>
        /// A simple method that handles drawing our shape with the various colours we need
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        /// <param name="drawShape">Draw shape.</param>
        /// <param name="lineWidth">Line width.</param>
        /// <param name="drawFill">If set to <c>true</c> draw fill.</param>
        protected virtual void HandleStandardDraw(Canvas canvas, Action<Paint> drawShape, float? lineWidth = null, bool drawFill = true)
        {
            var strokePaint = new Paint(PaintFlags.AntiAlias);
            strokePaint.SetStyle(Paint.Style.Stroke);
            strokePaint.StrokeWidth = this.Resize(lineWidth ?? this.ShapeView.StrokeWidth);
            strokePaint.StrokeCap = Paint.Cap.Round;
            strokePaint.Color = this.ShapeView.StrokeColor.ToAndroid();
            var fillPaint = new Paint();
            fillPaint.SetStyle(Paint.Style.Fill);
            fillPaint.Color = this.ShapeView.Color.ToAndroid();

            if (drawFill)
                drawShape(fillPaint);
            drawShape(strokePaint);
        }

        // Helper functions for dealing with pizel density
        private float Resize(float input)
        {
            return input * this.density;
        }

        private float Resize(double input)
        {
            return this.Resize((float)input);
        }
    }
}
