using System.Collections;

namespace DataStructures.Interfaces {

  public interface ISet : ICollection {
    bool Add(object item);
    void ExceptWith(IEnumerable other);
    void IntersectWith(IEnumerable other);
    bool IsProperSubsetOf(IEnumerable other);
    bool IsProperSupersetOf(IEnumerable other);
    bool IsSubsetOf(IEnumerable other);
    bool IsSupersetOf(IEnumerable other);
    bool Overlaps(IEnumerable other);
    bool SetEquals(IEnumerable other);
    void SymmetricExceptWith(IEnumerable other);
    void UnionWith(IEnumerable other);
  }
}
