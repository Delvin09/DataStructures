using System;
using System.Collections;
using DataStructures.Interfaces;

namespace DataStructures {

  public class Set : ISet {

    private struct Slot {

      public readonly object Value;
      public readonly int Hash;

      public Slot(object value, int hash) {

        Value = value;
        Hash = hash;
      }
    }

    private class SlotComparer : IComparer {

      public int Compare(object x, object y) {

        return ((Slot)x).Hash.CompareTo((Slot)y);
      }
    }

    private class SetEnumerator : IEnumerator {

      private Slot[] slots;
      private int currentIndex;

      public SetEnumerator(Slot[] slots) {
        this.slots = slots;
        currentIndex = -1;
      }

      public object Current => slots[currentIndex].Value;

      public bool MoveNext() {

        return ++currentIndex < slots.Length;
      }

      public void Reset() {
        currentIndex = -1;
      }
    }

    private const int InitCapacity = 10;

    private static Set ToSet(IEnumerable other) {

      if (!(other is Set otherSet)) {
        otherSet = new Set();
        foreach (var item in other)
          otherSet.Add(item);
      }
      return otherSet;
    }

    private Slot[] slots;
    private readonly IComparer comparer;

    public Set() {

      slots = new Slot[InitCapacity];
      comparer = new SlotComparer();
      Count = 0;
    }

    public int Count {
      get;
      private set;
    }

    public bool Add(object item) {

      if (item == null)
        throw new ArgumentNullException(nameof(item));

      if (Contains(item))
        return false;
      else {
        int hash = item.GetHashCode();
        if (Count == slots.Length) {
          var newSlots = new Slot[slots.Length * 2];
          bool isNotInserted = true;
          for (int i = 0; i <= newSlots.Length; i++) {
            if (slots[i].Hash < hash)
              newSlots[i] = slots[i];
            else if (isNotInserted) {
              newSlots[i] = new Slot(item, hash);
              isNotInserted = false;
            }
            else
              newSlots[i] = slots[i - 1];
          }
          slots = newSlots;
        }
        else {
          bool isFirst = true;
          for (int i = 0; i <= Count; i++) {
            if (slots[i].Hash > hash && isFirst) {
              slots[i] = new Slot(item, hash);
              isFirst = true;
            }
            else
              slots[i] = slots[i - 1];
          }
        }
        Count++;
      }

      return true;
    }

    public void Clear() {

      slots = new Slot[InitCapacity];
      Count = 0;
    }

    public bool Contains(object value) => Array.BinarySearch(slots, 0, Count, value.GetHashCode(), comparer) >= 0;

    private void RemoveAt(int index) {

      if (index >= 0 && index < Count) {
        for (int i = index; i < Count - 1; i++)
          slots[i] = slots[i + 1];
        Count--;
      }
    }

    public void ExceptWith(IEnumerable other) => InternalExceptWith(other, false);

    public void SymmetricExceptWith(IEnumerable other) => InternalExceptWith(other, true);

    private void InternalExceptWith(IEnumerable other, bool symmetric) {

      foreach (var item in other) {
        var hash = item.GetHashCode();
        int index = Array.BinarySearch(slots, 0, Count, hash, comparer);
        if (index >= 0)
          RemoveAt(index);
        else if (symmetric)
          Add(item);
      }
    }

    public void IntersectWith(IEnumerable other) {

      var newSet = new Set();
      foreach (var item in other)
        if (Contains(item))
          newSet.Add(item);

      this.slots = newSet.slots;
      this.Count = newSet.Count;
    }

    public bool Overlaps(IEnumerable other) {

      foreach (var item in other)
        if (Contains(item))
          return true;
      return false;
    }

    public bool SetEquals(IEnumerable other) {

      foreach (var item in other)
        if (!Contains(item))
          return false;
      return true;
    }

    public void UnionWith(IEnumerable other) {

      foreach (var item in other)
        Add(item);
    }

    public bool IsProperSubsetOf(IEnumerable other) => InnerIsSubsetOf(other, true);

    public bool IsSubsetOf(IEnumerable other) => InnerIsSubsetOf(other, false);

    public bool IsProperSupersetOf(IEnumerable other) => InnerIsSupersetOf(other, true);

    public bool IsSupersetOf(IEnumerable other) => InnerIsSupersetOf(other, false);

    private bool InnerIsSupersetOf(IEnumerable other, bool isProper) {

      var otherSet = ToSet(other);
      if (otherSet.Count == 0)
        return true;

      if ((isProper && Count <= otherSet.Count) || Count < otherSet.Count)
        return false;

      foreach (var item in otherSet)
        if (!Contains(item))
          return false;
      return true;
    }

    private bool InnerIsSubsetOf(IEnumerable other, bool isProper) {

      if (Count == 0)
        return true;

      var otherSet = ToSet(other);
      if ((isProper && Count >= otherSet.Count) || Count > otherSet.Count)
        return false;

      foreach (var item in this)
        if (!otherSet.Contains(item))
          return false;
      return true;
    }

    public object[] ToArray() {

      var result = new object[slots.Length];
      for (int i = 0; i < slots.Length; i++)
        result[i] = slots[i].Value;
      return result;
    }

    public IEnumerator GetEnumerator() {
      return new SetEnumerator(slots);
    }
  }
}
