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
        readonly ObservableCollection<T> _children = new ObservableCollection<T>();

        /// <summary>
        ///     Constructs a Section without an empty header.
        /// </summary>
        protected TableSectionBase()
        {
            this._children.CollectionChanged += this.OnChildrenChanged;
        }

        /// <summary>
        ///     Constructs a Section with the specified header.
        /// </summary>
        protected TableSectionBase(string title) : base(title)
        {
            this._children.CollectionChanged += this.OnChildrenChanged;
        }

        public void Add(T item)
        {
            this._children.Add(item);
        }

        public void Clear()
        {
            this._children.Clear();
        }

        public bool Contains(T item)
        {
            return this._children.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this._children.CopyTo(array, arrayIndex);
        }

        public int Count => this._children.Count;

        bool ICollection<T>.IsReadOnly => false;

        public bool Remove(T item)
        {
            return this._children.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._children.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return this._children.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this._children.Insert(index, item);
        }

        public T this[int index]
        {
            get => this._children[index];
            set => this._children[index] = value;
        }

        public void RemoveAt(int index)
        {
            this._children.RemoveAt(index);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => this._children.CollectionChanged += value;
            remove => this._children.CollectionChanged -= value;
        }

        public void Add(IEnumerable<T> items)
        {
            items.ForEach(this._children.Add);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            object bc = this.BindingContext;
            foreach (T child in this._children)
                SetInheritedBindingContext(child, bc);
        }

        void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            // We need to hook up the binding context for new items.
            if (notifyCollectionChangedEventArgs.NewItems == null)
                return;
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