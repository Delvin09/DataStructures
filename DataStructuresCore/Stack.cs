using DataStructures.Interfaces;
using System;

namespace DataStructures {

  // IEnumerable -> ICollection    -> IDictionary    -> DictionaryBase
  //                                                 -> Hashtable
  //                                                 -> SortedList
  //                               -> IList          -> ArrayList
  //             -> IEnumerable<T> -> ICollection<T> -> IDictionary<TKey, TValue> -> Dictionary<TKey, TValue> (SortedList, SortedDictionary)
  //                                                 -> IList<T>                  -> List<T>
  //                                                 -> ISet<T>                   -> HashSet<T>
  //              

  public class Stack : ICollection, System.Collections.ICollection, IPeekedCollection {

    private readonly object syncObject = new object();
    private LinkedList innerList = new LinkedList();

    public int Count => innerList.Count;

    public object SyncRoot => syncObject;

    public bool IsSynchronized => false;

    public void CopyTo(Array array, int index) => innerList.CopyTo(array, index);

    public System.Collections.IEnumerator GetEnumerator() => innerList.GetEnumerator();

    public void Clear() => innerList.Clear();

    public bool Contains(object value) => innerList.Contains(value);

    public object[] ToArray() {

      int index = 0;
      object[] result = new object[innerList.Count];
      foreach (var item in innerList)
        result[index++] = item;
      return result;
    }

    public object Peek() {

      if (Count <= 0)
        throw new InvalidOperationException();

      return innerList.First.Value;
    }

    public bool TryPeek(out object result) {

      result = null;
      if (Count > 0) {
        result = Peek();
        return true;
      }
      return false;
    }

    public void Push(object value) => innerList.AddFirst(value);

    public object Pop() {

      if (Count <= 0)
        throw new InvalidOperationException();

      var result = innerList.First.Value;
      innerList.RemoveFirst();
      return result;
    }
  }
}
