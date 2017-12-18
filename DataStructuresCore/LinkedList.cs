using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5 {

  public class Node {

    public Node Next {
      get; set;
    }

    public readonly object Value;

    public Node(object value) : this(value, null) {
    }

    public Node(object value, Node next) {
      Next = next;
      Value = value;
    }
  }

  public class LinkedList : ICollection {

    private class LinkedListEnumerator : IEnumerator {

      private readonly Node firstNode;
      private Node currentNode;

      public LinkedListEnumerator(Node node) {

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

    public Node First {
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

    public IEnumerator GetEnumerator() {

      return new LinkedListEnumerator(First);
    }

    public virtual void Clear() {

      First = null;
      Count = 0;
    }

    protected virtual Node CreateNode(object value, Node prev, Node next) {

      return new Node(value, next);
    }

    protected virtual Node AddInternal(object value, Node prev, Node next) {

      var newNode = CreateNode(value, prev, next);
      if (prev != null)
        prev.Next = newNode;
      Count++;
      return newNode;
    }

    public Node Insert(Node node, object value) {

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

    public void RemoveFirst() {

      RemoveNodeInternal(First, null);
    }

    public void RemoveLast() {

      Node last = First;
      if (last != null) {
        Node lastPrevios = null;
        while (last.Next != null) {
          lastPrevios = last;
          last = last.Next;
        }

        RemoveNodeInternal(last, lastPrevios);
      }
    }

    public bool Remove(object value) {

      return Remove(node => node.Value.Equals(value));
    }

    public void Remove(Node node) {

      Remove(currentNode => currentNode == node);
    }

    private bool Remove(Predicate<Node> predicate) {

      if (First == null)
        return false;

      if (predicate(First)){
        RemoveFirst();
        return true;
      }
      else {
        var current = First.Next;
        Node prev = First;
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

    protected virtual void RemoveNodeInternal(Node nodeToRemove, Node previosNode) {

      if (nodeToRemove == null)
        throw new ArgumentNullException(nameof(nodeToRemove));

      if (previosNode == null)
        First = nodeToRemove.Next;
      else
        previosNode.Next = nodeToRemove.Next;

      Count--;
    }

    public Node Find(object value) {

      if (First != null) {
        Node current = First;
        do {
          if (current.Value.Equals(value))
            return current;
        } while ((current = current.Next) != null);
      }
      return null;
    }

    public bool Contains(object value) {

      return Find(value) != null;
    }

    public object[] ToArray() {

      var result = new object[Count];
      Node current = First;
      for (int i = 0; i < Count; i++, current = current.Next)
        result[i] = current.Value;
      return result;
    }
  }
}
