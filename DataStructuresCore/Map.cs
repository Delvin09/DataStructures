using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.Interfaces;

namespace DataStructures {

  public class Map : IMap {

    public struct KeyValue {

      public readonly object Key;
      public readonly object Value;

      public KeyValue(object key, object value) {
        this.Key = key;
        this.Value = value;
      }
    }

    private class MapKeyDefaultComparer : IMapKeyComparer {

      public int Compare(object key1, object key2) {

        if (key1 == null)
          throw new ArgumentNullException(nameof(key1));
        if (key2 == null)
          throw new ArgumentNullException(nameof(key2));
        if (key1.GetType() != key2.GetType())
          throw new ArgumentException($"Type of key1:{key1.GetType()} and key2:{key2.GetType()} isn't equals");

        if (object.ReferenceEquals(key1, key2))
          return 0;
        if (key1.Equals(key2))
          return 0;

        if (key1 is IComparable comp1)
          return comp1.CompareTo(key2);

        return key1.GetHashCode() - key2.GetHashCode();
      }

      public int GetHash(object key) => key.GetHashCode();
    }

    private const int InitCapacity = 10;
    public static readonly IMapKeyComparer DefaultComparer = new MapKeyDefaultComparer();

    private KeyValue[] bank;
    private readonly IMapKeyComparer comparer;

    public Map() : this(DefaultComparer) {
    }

    public Map(IMapKeyComparer comparer) {

      this.comparer = comparer;
      bank = new KeyValue[InitCapacity];
      Count = 0;
    }

    public object this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public int Count {
      get;
      private set;
    }

    public void Add(object key, object value) {
      throw new NotImplementedException();
    }

    public void Clear() {
      Count = 0;
      bank = new KeyValue[InitCapacity];
    }

    public bool ContainsKey(object key) {
      throw new NotImplementedException();
    }

    public bool Remove(object key) {
      throw new NotImplementedException();
    }

    public bool TryGetValue(object key, out object value) {
      throw new NotImplementedException();
    }

    public System.Collections.IEnumerator GetEnumerator() {
      throw new NotImplementedException();
    }

    bool ICollection.Contains(object value) {
      throw new NotImplementedException();
    }

    object[] ICollection.ToArray() {
      throw new NotImplementedException();
    }
  }
}
