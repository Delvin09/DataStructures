using DataStructures.Interfaces;

namespace DataStructures {

  public class DuoNode : Node, IDuoLinkedNode {

    public ILinkedNode Prev {
      get; set;
    }

    public DuoNode(object value)
      : base(value) {
    }

    public DuoNode(object value, ILinkedNode prev, ILinkedNode next) : base(value, next) {
      this.Prev = prev;
    }
  }

  public class DuoLinkedList : LinkedList, IDuoLinkedList {

    public ILinkedNode Last {
      get;
      private set;
    }

    public override void Clear() {

      Last = null;
      base.Clear();
    }

    protected override ILinkedNode CreateNode(object value, ILinkedNode prev = null, ILinkedNode next = null)
      => new DuoNode(value, prev, next);

    protected override void RemoveNodeInternal(ILinkedNode nodeToRemove, ILinkedNode previosNode) {

      base.RemoveNodeInternal(nodeToRemove, previosNode);
      var nextNode = (IDuoLinkedNode)nodeToRemove.Next;
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

    protected override ILinkedNode AddInternal(object value, ILinkedNode prev, ILinkedNode next) {

      var newNode = (IDuoLinkedNode)base.AddInternal(value, prev, next);
      if (next != null)
        ((IDuoLinkedNode)next).Prev = newNode;
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
