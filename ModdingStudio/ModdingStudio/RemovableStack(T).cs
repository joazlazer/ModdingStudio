using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Utilities
{
    public class RemovableStack<T> : ObservableCollection<T>
    {
        public RemovableStack()
        {
            this.CollectionChanged += OnElementsChanged;
        }

        private void OnElementsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnElementsChanged(e);
        }

        new public void Add(T item) { throw new NotSupportedException(); }
        new public void AddRange(IEnumerable<T> collection) { throw new NotSupportedException(); }
        new public void Insert(int index, T item) { throw new NotSupportedException(); }
        new public void InsertRange(int index, IEnumerable<T> collection) { throw new NotSupportedException(); }
        new public void Reverse() { throw new NotSupportedException(); }
        new public void Reverse(int index, int count) { throw new NotSupportedException(); }
        new public void Sort() { throw new NotSupportedException(); }
        new public void Sort(Comparison<T> comparison) { throw new NotSupportedException(); }
        new public void Sort(IComparer<T> comparer) { throw new NotSupportedException(); }
        new public void Sort(int index, int count, IComparer<T> comparer) { throw new NotSupportedException(); }

        public event EventHandler ElementsChanged;

        protected virtual void OnElementsChanged(EventArgs e)
        {
            if (ElementsChanged != null)
                ElementsChanged(this, e);
        }

        public void Push(T item)
        {
            base.Add(item);
            OnElementsChanged(new EventArgs());
        }

        public T Pop()
        {
            var t = base[base.Count - 1];
            base.RemoveAt(base.Count - 1);
            OnElementsChanged(new EventArgs());
            return t;
        }

        public T Peek()
        {
            if (Count != 0)
            return base[base.Count - 1];
            return default(T);
        }
    }
}
