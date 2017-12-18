using System;
using System.Collections;

namespace DataStructures {

  public class List : IList {

    private class ListEnumerator : IEnumerator {

      private readonly List list;
      private int currentIndex = -1;

      public ListEnumerator(List list) {
        this.list = list;
      }

      public object Current => list[currentIndex];

      public bool MoveNext() {

        return ++currentIndex < list.Count;
      }

      public void Reset() {

        currentIndex = -1;
      }
    }

    private object[] innerArray;
    private readonly object syncRoot = new object();
    private readonly int origCapacity;

    public List() : this(10) {
    }

    public List(int capacity) {
      origCapacity = capacity;
      innerArray = new object[capacity];
    }

    public object this[int index] { get => innerArray[index]; set => innerArray[index] = value; }

    public bool IsReadOnly => false;

    public bool IsFixedSize => false;

    public int Count {
      get; private set;
    }

    public int Capacity => innerArray.Length;

    public object SyncRoot => syncRoot;

    public bool IsSynchronized => false;

    public int Add(object value) {

      GrowingArray();

      var index = Count++;
      innerArray[index] = value;
      return index;
    }

    public void Clear() {

      Count = 0;
      innerArray = new object[origCapacity];
    }

    public bool Contains(object value) {

      return IndexOf(value) >= 0;
    }

    public void CopyTo(Array array, int index) {

      for (int innerIndex = 0; innerIndex < Count; innerIndex++, index++)
        array.SetValue(innerArray[innerIndex], index);
    }

    public IEnumerator GetEnumerator() {
      return new ListEnumerator(this);
    }

    public int IndexOf(object value) {

      for (int index = 0; index < Count; index++)
        if (innerArray[index].Equals(value))
          return index;
      return -1;
    }

    public void Insert(int index, object value) {

      if (index < 0 || index > Count)
        throw new IndexOutOfRangeException();

      GrowingArray();

      object stored = null;
      for (; index < Count + 1; index++) {
        stored = innerArray[index];
        innerArray[index] = value;
        value = stored;
      }

      Count++;
    }

    public void Remove(object value) {

      var index = IndexOf(value);
      if (index >= 0)
        RemoveAt(index);
    }

    public void RemoveAt(int index) {

      if (index < 0 || index >= Count)
        throw new IndexOutOfRangeException();

      for (int nextIndex = index + 1; index < Count && nextIndex < Count; index++, nextIndex++)
        innerArray[index] = innerArray[nextIndex];

      Count--;
    }

    public void RemoveAll(object value) {

      int index = -1;
      while ((index = IndexOf(value)) >= 0)
        RemoveAt(index);
    }

    public object[] ToArray() {

      var result = new object[Count];
      for (int index = 0; index < Count; index++)
        result[index] = innerArray[index];
      return result;
    }

    public void Reverse() {

      for (int index = 0, lastIndex = Count - 1; index < lastIndex; index++, lastIndex--) {
        object store = innerArray[index];
        innerArray[index] = innerArray[lastIndex];
        innerArray[lastIndex] = store;
      }
    }

    private void GrowingArray() {
      if (Capacity == Count) {
        var newArray = new object[(Capacity / 2) + Capacity];
        for (int i = 0; i < innerArray.Length; i++)
          newArray[i] = innerArray[i];
        innerArray = newArray;
      }
    }
  }
}
