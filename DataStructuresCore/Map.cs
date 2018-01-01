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

    private class MapEnumerator : System.Collections.IEnumerator {

      private readonly KeyValue[] arr;
      private readonly int count;
      private int currentIndex = -1;

      public MapEnumerator(KeyValue[] arr, int count) {
        this.arr = arr;
        this.count = count;
      }

      public object Current => arr[currentIndex];

      public bool MoveNext() => ++currentIndex < count;

      public void Reset() => currentIndex = -1;
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

      if (key == null)
        throw new ArgumentNullException(nameof(key));

      if (ContainsKey(key))
        throw new ArgumentException($"Key with value {key} is exists", nameof(key));

      if (Count == bank.Length)
        GrowBankSize();
      InsertToBank(key, value);
      Count++;
    }

    private void GrowBankSize() {
      var newBank = new KeyValue[bank.Length * 2];
      for (int i = 0; i < bank.Length; i++)
        newBank[i] = bank[i];
      bank = newBank;
    }

    private void InsertToBank(object key, object value) {

      Search(key, out int index);
      var item = bank[index];
      bank[index++] = new KeyValue(key, value);
      for (; index <= Count; index++) {
        var item2 = bank[index];
        bank[index] = item;
        item = item2;
      }
    }

    private bool Search(object key, out int index) {

      index = -1;
      int begin = 0;
      int end = Count - 1;
      while (begin <= end && begin >= 0 && end < Count) {
        int middle = index = ((end - begin) / 2) + begin;
        if (comparer.Compare(bank[middle], key) > 0) // берем меньшую часть
          end = middle - 1;
        else if (comparer.Compare(bank[middle], key) < 0) // берем большую часть
          begin = middle + 1;
        else
          return true;
      }
      return false;
    }

    public void Clear() {
      Count = 0;
      bank = new KeyValue[InitCapacity];
    }

    public bool ContainsKey(object key) => key != null && Search(key, out int index);

    public bool Remove(object key) {

      if (key == null)
        throw new ArgumentNullException(nameof(key));

      if (!Search(key, out int index))
        return false;

      Count--;
      for (; index < Count; index++)
        bank[index] = bank[index + 1];
      return true;
    }

    public bool TryGetValue(object key, out object value) {

      value = null;
      if (key == null || !ContainsKey(key))
        return false;

      value = this[key];
      return true;
    }

    public System.Collections.IEnumerator GetEnumerator() => new MapEnumerator(bank, Count);

    bool ICollection.Contains(object value) {

      if (ContainsKey(value))
        return true;
      foreach (var item in bank) {
        if (item.Value == value || ReferenceEquals(item.Value, value) || item.Value.Equals(value))
          return true;
      }
      return false;
    }

    object[] ICollection.ToArray() {

      object[] result = new object[Count];
      Array.Copy(bank, result, Count);
      return result;
    }
  }
}
