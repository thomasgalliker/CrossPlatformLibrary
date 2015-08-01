using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

using Microsoft.Phone.Controls;

namespace System.Windows.Controls
{
    /// <summary>
    ///     The delay text box.
    /// </summary>
    public class PhoneTextBoxEx : PhoneTextBox
    {
        #region Fields

        private const int DefaultDelay = 500;
        private readonly DispatcherTimer delayTimer;

        #endregion

        // Delay property

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PhoneTextBoxEx" /> class.
        /// </summary>
        public PhoneTextBoxEx()
        {
            this.Delay = DefaultDelay;

            this.delayTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(this.Delay) };
            this.delayTimer.Tick += this.DelayTimerTick;
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     Occurs when [text changed delayed].
        /// </summary>
        public event EventHandler<string> TextChangedDelayed;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the delay (in milliseconds) after which the TextChangedDelayed event is raised
        ///     when no further input is made.
        /// </summary>
        /// <value>The delay (in milliseconds).</value>
        public int Delay { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Called when <see cref="E:System.Windows.UIElement.KeyDown" /> event occurs.
        /// </summary>
        /// <param name="e">The data for the event.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.Delay == 0)
            {
                this.RaiseTextChangedEvent();
            }
            else
            {
                if (!this.delayTimer.IsEnabled)
                {
                    this.delayTimer.Start();
                }
                else
                {
                    this.delayTimer.Stop();
                    this.delayTimer.Start();
                }
            }

            base.OnKeyDown(e);
        }

        private void DelayTimerTick(object sender, EventArgs e)
        {
            this.delayTimer.Stop();

            this.RaiseTextChangedEvent();
        }

        private void RaiseTextChangedEvent()
        {
            this.Dispatcher.BeginInvoke(
                () =>
                    {
                        // Refresh binding (if used in mvvm ui)
                        BindingExpression be = this.GetBindingExpression(TextProperty);
                        be.UpdateSource();

                        // Raise event (if used in code-behind scenarios)
                        if (this.TextChangedDelayed != null)
                        {
                            this.TextChangedDelayed(this, this.Text);
                        }
                    });
        }

        #endregion
    }
}