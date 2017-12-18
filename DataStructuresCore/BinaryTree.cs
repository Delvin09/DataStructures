using DataStructures.Interfaces;
using System;

namespace DataStructures {

  public class BinaryTree : IBinaryTree, System.Collections.ICollection {

    private readonly object syncRoot = new object();

    public int Count { get; private set; }

    public object SyncRoot => syncRoot;

    public bool IsSynchronized => false;

    public IBinaryTreeNode Root { get; private set; }

    public IBinaryTreeNode Add(object value) {
      throw new NotImplementedException();
    }

    public void Clear() => Root = null;

    public bool Contains(object value) {
      throw new NotImplementedException();
    }

    public void CopyTo(Array array, int index) {
      throw new NotImplementedException();
    }

    public IBinaryTreeNode Find(object value) {
      throw new NotImplementedException();
    }

    public System.Collections.IEnumerator GetEnumerator() {
      throw new NotImplementedException();
    }

    public void Remove(object value) {
      throw new NotImplementedException();
    }

    public void Remove(IBinaryTreeNode value) {
      throw new NotImplementedException();
    }

    public object[] ToArray() {
      throw new NotImplementedException();
    }
  }
}
