namespace DataStructures.Interfaces {

  public interface ILinkedNode {
    ILinkedNode Next { get; set; }
    object Value { get; }
  }

  public interface ILinkedList : ICollection {
    ILinkedNode First { get; }
    void AddFirst(object value);
    void AddLast(object value);
    ILinkedNode Insert(ILinkedNode node, object value);
    void RemoveFirst();
    void RemoveLast();
    bool Remove(object value);
    void Remove(ILinkedNode node);
    ILinkedNode Find(object value);
  }
}
