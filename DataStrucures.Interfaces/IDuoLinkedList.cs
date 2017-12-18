namespace DataStructures.Interfaces {

  public interface IDuoLinkedNode : ILinkedNode {
    ILinkedNode Prev { get; set; }
  }

  public interface IDuoLinkedList : ILinkedList {
    ILinkedNode Last { get; }
  }
}
