using DataStructures.Interfaces;
using System;

namespace DataStructures {

  public class Node : ILinkedNode {

    public ILinkedNode Next {
      get; set;
    }

    public object Value {
      get; private set;
    }

    public Node(object value) : this(value, null) {
    }

    public Node(object value, ILinkedNode next) {
      Next = next;
      Value = value;
    }
  }

  public class LinkedList : System.Collections.ICollection, ILinkedList {

    private class LinkedListEnumerator : System.Collections.IEnumerator {

      private readonly ILinkedNode firstNode;
      private ILinkedNode currentNode;

      public LinkedListEnumerator(ILinkedNode node) {

        firstNode = node;
      }

      public object Current => currentNode.Value;

      public bool MoveNext() {

        currentNode = currentNode == null ? firstNode : currentNode.Next;
        return currentNode != null;
      }

      public void Reset() {
        currentNode = firstNode;
      }
    }

    private readonly object syncRoot = new object();

    public ILinkedNode First {
      get; protected set;
    }

    public int Count {
      get; protected set;
    }

    public object SyncRoot => syncRoot;

    public bool IsSynchronized => false;

    public void CopyTo(Array array, int index) {

      var current = First;
      for (int i = 0; i < Count; i++, current = current.Next, index++)
        array.SetValue(current.Value, index);
    }

    public System.Collections.IEnumerator GetEnumerator() => new LinkedListEnumerator(First);

    public virtual void Clear() {

      First = null;
      Count = 0;
    }

    protected virtual ILinkedNode CreateNode(object value, ILinkedNode prev, ILinkedNode next) => new Node(value, next);

    protected virtual ILinkedNode AddInternal(object value, ILinkedNode prev, ILinkedNode next) {

      var newNode = CreateNode(value, prev, next);
      if (prev != null)
        prev.Next = newNode;
      Count++;
      return newNode;
    }

    public ILinkedNode Insert(ILinkedNode node, object value) {

      return AddInternal(value, prev: node, next: node.Next);
    }

    public void AddFirst(object value) {

      First = AddInternal(value, prev: null, next: First);
    }

    public virtual void AddLast(object value) {

      if (First == null)
        AddFirst(value);
      else {
        var last = First;
        while (last.Next != null)
          last = last.Next;
        last.Next = AddInternal(value, prev: last, next: null);
      }
    }

    public void RemoveFirst() => RemoveNodeInternal(First, null);

    public void RemoveLast() {

      var last = First;
      if (last != null) {
        ILinkedNode lastPrevios = null;
        while (last.Next != null) {
          lastPrevios = last;
          last = last.Next;
        }
        RemoveNodeInternal(last, lastPrevios);
      }
    }

    public bool Remove(object value) => Remove(node => node.Value.Equals(value));

    public void Remove(ILinkedNode node) => Remove(currentNode => currentNode == node);

    private bool Remove(Predicate<ILinkedNode> predicate) {

      if (First == null)
        return false;

      if (predicate(First)) {
        RemoveFirst();
        return true;
      }
      else {
        var current = First.Next;
        var prev = First;
        do {
          if (predicate(current)) {
            RemoveNodeInternal(current, prev);
            return true;
          }
          prev = current;
        }
        while ((current = current.Next) != null);
      }
      return false;
    }

    protected virtual void RemoveNodeInternal(ILinkedNode nodeToRemove, ILinkedNode previosNode) {

      if (nodeToRemove == null)
        throw new ArgumentNullException(nameof(nodeToRemove));

      if (previosNode == null)
        First = nodeToRemove.Next;
      else
        previosNode.Next = nodeToRemove.Next;

      Count--;
    }

    public ILinkedNode Find(object value) {

      if (First != null) {
        var current = First;
        do {
          if (current.Value.Equals(value))
            return current;
        } while ((current = current.Next) != null);
      }
      return null;
    }

    public bool Contains(object value) => Find(value) != null;

    public object[] ToArray() {

      var result = new object[Count];
      var current = First;
      for (int i = 0; i < Count; i++, current = current.Next)
        result[i] = current.Value;
      return result;
    }
  }
}
