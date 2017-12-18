namespace DataStructures.Interfaces {
  public interface ICollection : System.Collections.IEnumerable {
    int Count { get; }
    object[] ToArray();
    void Clear();
    bool Contains(object value);
  }
}
