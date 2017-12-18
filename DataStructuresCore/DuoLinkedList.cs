using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5 {

  public class DuoNode : Node {

    public Node Prev {
      get; set;
    }

    public DuoNode(object value)
      : base(value) {
    }

    public DuoNode(object value, Node prev, Node next) : base(value, next) {
      this.Prev = prev;
    }
  }

  public class DuoLinkedList : LinkedList {

    public Node Last {
      get;
      set;
    }

    public override void Clear() {

      Last = null;
      base.Clear();
    }

    protected override Node CreateNode(object value, Node prev = null, Node next = null) {

      return new DuoNode(value, prev, next);
    }

    protected override void RemoveNodeInternal(Node nodeToRemove, Node previosNode) {

      base.RemoveNodeInternal(nodeToRemove, previosNode);
      var nextNode = (DuoNode)nodeToRemove.Next;
      if (previosNode == null && nextNode != null)
        nextNode.Prev = null;

      if (nextNode == null) {
        Last = previosNode;
        if (previosNode != null)
          previosNode.Next = null;
      }
      else
        nextNode.Prev = previosNode;
    }

    protected override Node AddInternal(object value, Node prev, Node next) {

      var newNode = (DuoNode)base.AddInternal(value, prev, next);
      if (next != null)
        ((DuoNode)next).Prev = newNode;
      else
        Last = newNode;
      return newNode;
    }

    public override void AddLast(object value) {

      Last = AddInternal(value, Last, null);
      if (First == null)
        First = Last;
    }
  }
}
