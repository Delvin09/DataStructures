using System;
using System.Collections;

namespace DataStructures {

  public class Queue : ICollection {

    private readonly object syncRoot = new object();
    private DuoLinkedList innerList = new DuoLinkedList();

    public int Count => innerList.Count;

    public object SyncRoot => syncRoot;

    public bool IsSynchronized => false;

    public void CopyTo(Array array, int index) {

      innerList.CopyTo(array, index);
    }

    public IEnumerator GetEnumerator() {
      return innerList.GetEnumerator();
    }

    public void Enqueue(object value) {

      innerList.AddLast(value);
    }

    public object Dequeue() {

      if (innerList.Count == 0)
        throw new InvalidOperationException();

      var result = innerList.First.Value;
      innerList.RemoveFirst();
      return result;
    }

    public object Peek() {

      if (innerList.Count == 0)
        throw new InvalidOperationException();

      return innerList.First.Value;
    }

    public bool TryPeek(out object result) {

      result = null;
      if (innerList.Count > 0) {
        result = innerList.First.Value;
        return true;
      }
      return false;
    }

    public bool Dequeue(out object result) {

      result = null;
      if (innerList.Count > 0) {
        result = innerList.First.Value;
        innerList.RemoveFirst();
        return true;
      }
      return false;
    }

    public void Clear() {

      innerList.Clear();
    }

    public bool Contains(object value) {

      return innerList.Contains(value);
    }

    public object[] ToArray() {

      return innerList.ToArray();
    }
  }
}
