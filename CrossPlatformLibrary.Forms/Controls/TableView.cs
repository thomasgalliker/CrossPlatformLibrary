using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CrossPlatformLibrary.Forms.Controls
{
    public abstract class TableSectionBase<T> : TableSectionBase, IList<T>, INotifyCollectionChanged where T : BindableObject
    {
        private readonly ObservableCollection<T> children = new ObservableCollection<T>();

        /// <summary>
        ///     Constructs a Section without an empty header.
        /// </summary>
        protected TableSectionBase()
        {
            this.children.CollectionChanged += this.OnChildrenChanged;
        }

        /// <summary>
        ///     Constructs a Section with the specified header.
        /// </summary>
        protected TableSectionBase(string title) : base(title)
        {
            this.children.CollectionChanged += this.OnChildrenChanged;
        }

        public void Add(T item)
        {
            this.children.Add(item);
        }

        public void Clear()
        {
            this.children.Clear();
        }

        public bool Contains(T item)
        {
            return this.children.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.children.CopyTo(array, arrayIndex);
        }

        public int Count => this.children.Count;

        bool ICollection<T>.IsReadOnly => false;

        public bool Remove(T item)
        {
            return this.children.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.children.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return this.children.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.children.Insert(index, item);
        }

        public T this[int index]
        {
            get => this.children[index];
            set => this.children[index] = value;
        }

        public void RemoveAt(int index)
        {
            this.children.RemoveAt(index);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => this.children.CollectionChanged += value;
            remove => this.children.CollectionChanged -= value;
        }

        public void Add(IEnumerable<T> items)
        {
            items.ForEach(this.children.Add);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            object bc = this.BindingContext;
            foreach (var child in this.children)
            {
                SetInheritedBindingContext(child, bc);
            }
        }

        void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            // We need to hook up the binding context for new items.
            if (notifyCollectionChangedEventArgs.NewItems == null)
            {
                return;
            }

            object bc = this.BindingContext;
            foreach (BindableObject item in notifyCollectionChangedEventArgs.NewItems)
            {
                SetInheritedBindingContext(item, bc);
            }
        }
    }

    public sealed class TableView : TableSectionBase<Cell>
    {
        public TableView()
        {
        }

        public TableView(string title) : base(title)
        {
        }
    }
}