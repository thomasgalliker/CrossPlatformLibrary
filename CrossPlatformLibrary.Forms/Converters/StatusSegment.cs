using CrossPlatformLibrary.Mvvm;

namespace CrossPlatformLibrary.Forms.Converters
{
    internal class StatusSegment : BindableBase
    {
        private bool isFirstElement;
        private bool isLastElement;
        private bool isStartElement;
        private bool isEndElement;
        private bool isMiddleElement;
        public object Payload { get; }

        public StatusSegment(object payload)
        {
            this.Payload = payload;
        }

        public bool IsFirstElement
        {
            get => this.isFirstElement;
            set => this.SetProperty(ref this.isFirstElement, value, "");
        }

        public bool IsLastElement
        {
            get => this.isLastElement;
            set => this.SetProperty(ref this.isLastElement, value, "");
        }

        public bool IsStartElement
        {
            get => this.isStartElement;
            set => this.SetProperty(ref this.isStartElement, value, "");
        }

        public bool IsMiddleElement
        {
            get => this.isMiddleElement;
            set => this.SetProperty(ref this.isMiddleElement, value, "");
        }

        public bool IsEndElement
        {
            get => this.isEndElement;
            set => this.SetProperty(ref this.isEndElement, value, "");
        }
    }
}