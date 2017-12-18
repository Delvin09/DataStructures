namespace DataStructures.Interfaces {
  public interface IPeekedCollection {
    object Peek();
    bool TryPeek(out object result);
  }
}
