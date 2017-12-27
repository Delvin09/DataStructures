
namespace DataStructures.Interfaces {

  public interface IMapKeyComparer {

    int Compare(object key1, object key2);
    int GetHash(object key);
  }

  public interface IMap : ICollection {

    object this[object key] { get; set; }

    bool ContainsKey(object key);
    void Add(object key, object value);
    bool Remove(object key);
    bool TryGetValue(object key, out object value);
  }
}
