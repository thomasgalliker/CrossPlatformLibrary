using System;
using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Text;
using Android.Views.InputMethods;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using TextChangedEventArgs = Android.Text.TextChangedEventArgs;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        private string textBeforeChange;

        public CustomEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                if (this.Control != null)
                {
                    this.Control.BeforeTextChanged -= this.OnBeforeTextChanged;
                    this.Control.AfterTextChanged -= this.OnAfterTextChanged;
                }
            }

            if (e.NewElement != null)
            {
                if (this.Control != null)
                {
                    this.Control.BeforeTextChanged += this.OnBeforeTextChanged;
                    this.Control.AfterTextChanged += this.OnAfterTextChanged;
                }
            }

            var editorEditText = this.Control;
            if (editorEditText != null)
            {
                editorEditText.SetBackgroundColor(Color.Transparent);
                //editorEditText.InputType = InputTypes.ClassText | InputTypes.TextFlagMultiLine | InputTypes.TextFlagNoSuggestions;

                if (this.Element is CustomEditor customEditor)
                {
                    if (customEditor.HideKeyboard)
                    {
                        editorEditText.ShowSoftInputOnFocus = false;
                    }

                    this.UpdateLines(customEditor);
                }
            }
        }

        private void OnBeforeTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.Element is CustomEditor customEditor && customEditor.MaxLines != CustomEditor.MaxLinesDefault)
            {
                if (this.Control.LineCount <= customEditor.MaxLines)
                {
                    this.textBeforeChange = customEditor.Text;
                }
            }
        }

        private void OnAfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            if (this.Element is CustomEditor customEditor && customEditor.MaxLines != CustomEditor.MaxLinesDefault)
            {
                if (this.Control.LineCount > customEditor.MaxLines)
                {
                    customEditor.Text = this.textBeforeChange;
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
                        // In case if the focus was moved from another Entry
                        // Forcefully dismiss the Keyboard 
                        var inputMethodManager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                        inputMethodManager.HideSoftInputFromWindow(this.Control.WindowToken, 0);
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
                this.Control.SetSingleLine(false);
                this.Control.SetMaxLines(customEditor.MaxLines);
                this.Control.VerticalScrollBarEnabled = false;
            }
        }
    }
}