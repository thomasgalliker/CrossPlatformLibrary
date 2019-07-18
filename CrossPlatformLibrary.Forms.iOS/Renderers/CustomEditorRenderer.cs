using System;
using System.ComponentModel;
using System.Linq;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                if (this.Control != null)
                {
                    this.Control.Started -= this.Control_EditingStarted;
                }
            }

            if (e.NewElement != null)
            {
                if (this.Control != null)
                {
                    this.Control.Started += this.Control_EditingStarted;
                }
            }

            var editorEditText = this.Control;
            if (editorEditText != null)
            {
                if (this.Element is CustomEditor customEditor)
                {
                    if (customEditor.HideKeyboard)
                    {

                    }

                    this.UpdateLines(customEditor);
                }
            }
        }

        private void Control_EditingStarted(object sender, EventArgs e)
        {
            if (this.Element is CustomEditor customEditor)
            {
                if (customEditor.HideKeyboard)
                {
                    this.Control.ResignFirstResponder();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);

            }
            catch (Exception exception)
            {
            }


            if (this.Element is CustomEditor customEditor)
            {
                if (e.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
                {
                    if (customEditor.HideKeyboard)
                    {
                        this.Control.ResignFirstResponder();
                    }
                }
                else if (e.PropertyName == CustomEditor.MaxLinesProperty.PropertyName)
                {
                    this.UpdateLines(customEditor);
                }
                else if (e.PropertyName == Editor.TextProperty.PropertyName)
                {
                    this.UpdateText(customEditor);
                }
            }
        }

        private void UpdateText(CustomEditor customEditor)
        {
            if (customEditor.MaxLines != CustomEditor.MaxLinesDefault)
            {
                if (customEditor.Text != null)
                {
                    var textLines = customEditor.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.None);
                    if (textLines.Length > customEditor.MaxLines)
                    {
                        customEditor.Text = string.Join(Environment.NewLine, textLines.Take(customEditor.MaxLines));
                    }
                }
            }
        }

        private void UpdateLines(CustomEditor customEditor)
        {
            if (customEditor.MaxLines != CustomEditor.MaxLinesDefault)
            {
                this.Control.TextContainer.MaximumNumberOfLines = new nuint((uint)customEditor.MaxLines);
                this.Control.AlwaysBounceVertical = false;
                this.Control.Bounces = false;
                this.Control.ShowsVerticalScrollIndicator = false;
                this.Control.ShowsHorizontalScrollIndicator = false;
            }
            else
            {
                this.Control.TextContainer.MaximumNumberOfLines = 0;
                this.Control.AlwaysBounceVertical = true;
                this.Control.Bounces = true;
                this.Control.ShowsVerticalScrollIndicator = true;
                this.Control.ShowsHorizontalScrollIndicator = true;
            }
        }
    }
}