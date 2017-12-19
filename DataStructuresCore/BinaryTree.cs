using DataStructures.Interfaces;
using System;

namespace DataStructures {

  [System.Diagnostics.DebuggerDisplay("Value={Value}  :  Left={Left?.Value};Right={Right?.Value}")]
  public class BinaryTreeNode : IBinaryTreeNode {

    public IBinaryTreeNode Left {
      get; set;
    }

    public IBinaryTreeNode Right {
      get; set;
    }

    public IComparable Value {
      get;
      private set;
    }

    public int Count {
      get;
      set;
    }

    public BinaryTreeNode(IComparable value, IBinaryTreeNode left = null, IBinaryTreeNode right = null) {
      this.Value = value;
      this.Left = left;
      this.Right = right;
    }

    public int CompareTo(object obj) {

      var otherNode = obj as IBinaryTreeNode;
      return Value.CompareTo(otherNode != null ? otherNode.Value : obj);
    }
  }

  public class BinaryTree : IBinaryTree, System.Collections.ICollection {

    private class BinaryTreeEnumerator : System.Collections.IEnumerator {

      public object Current => throw new NotImplementedException();

      public bool MoveNext() {
        throw new NotImplementedException();
      }

      public void Reset() {
        throw new NotImplementedException();
      }
    }

    private enum TreeState {
      None,
      OnDeleteNode,
      OnAddNode
    }

    private TreeState state = TreeState.None;

    private readonly object syncRoot = new object();

    public int Count {
      get; private set;
    }

    public object SyncRoot => syncRoot;

    public bool IsSynchronized => false;

    public IBinaryTreeNode Root {
      get; private set;
    }

    public IBinaryTreeNode Add(IComparable value) {

      try {
        state = TreeState.OnAddNode;
        var node = CreateNode(value);
        if (Root == null) {
          Root = node;
          Count++;
        }
        else
          InsertNode(Root, node);
        return node;
      }
      finally {
        state = TreeState.None;
      }
    }

    public void Clear() => Root = null;

    public bool Contains(object value) => (value is IComparable comp) && Find(comp) != null;

    public void CopyTo(Array array, int index) {

      foreach (var item in ToArray())
        array.SetValue(item, index++);
    }

    public IBinaryTreeNode Find(IComparable value) => Find(Root, value);

    private IBinaryTreeNode Find(IBinaryTreeNode current, IComparable value) {

      if (current == null)
        return null;

      int compResult = current.CompareTo(value);
      if (compResult > 0)
        return Find(current.Left, value);
      if (compResult < 0)
        return Find(current.Right, value);
      return current;
    }

    public System.Collections.IEnumerator GetEnumerator() {
      throw new NotImplementedException();
    }

    public void Remove(IComparable value) {

      if (Root == null)
        throw new InvalidOperationException();

      try {
        state = TreeState.OnDeleteNode;
        if (Root.CompareTo(value) == 0) {
          InternalRemove(null, Root);
          Count--;
        }
        else
          InternalRemove(Root, value);
      }
      finally {
        state = TreeState.None;
      }
    }

    public object[] ToArray() {

      var resultArray = new object[Count];
      int index = 0;
      Collacte(resultArray, ref index, Root);
      return resultArray;
    }

    private void InsertNode(IBinaryTreeNode current, IBinaryTreeNode node) {

      var compResult = node.CompareTo(current);
      if (compResult > 0) { // node is bigger then parent
        if (current.Right != null)
          InsertNode(current.Right, node);
        else {
          current.Right = node;
          if (state == TreeState.OnAddNode)
            Count++;
        }
      }
      else if (compResult < 0) { // node is smaller then parent
        if (current.Left != null)
          InsertNode(current.Left, node);
        else {
          current.Left = node;
          if (state == TreeState.OnAddNode)
            Count++;
        }
      }
      else { // node is equal to parent
        if (current is BinaryTreeNode equal)
          equal.Count++;
      }
    }

    private IBinaryTreeNode CreateNode(IComparable value, IBinaryTreeNode left = null, IBinaryTreeNode right = null) => new BinaryTreeNode(value);

    private void InternalRemove(IBinaryTreeNode parent, IComparable value) {

      int compResult = parent.CompareTo(value);
      if (compResult > 0 && parent.Left != null) {
        if (parent.Left.CompareTo(value) == 0)
          InternalRemove(parent, parent.Left);
        else
          InternalRemove(parent.Left, value);
      }
      else if (compResult < 0 && parent.Right != null) {
        if (parent.Right.CompareTo(value) == 0)
          InternalRemove(parent, parent.Right);
        else
          InternalRemove(parent.Right, value);
      }
    }

    private void InternalRemove(IBinaryTreeNode parent, IBinaryTreeNode nodeToRemove) {

      if (parent == null) { // it is root node
        var right = Root.Right;
        Root = Root.Left;
        if (right != null)
          InsertNode(Root, right);
      }
      else {
        if (parent.Left == nodeToRemove)
          parent.Left = null;
        else
          parent.Right = null;
        Count--;

        if (nodeToRemove.Left != null)
          InsertNode(parent, nodeToRemove.Left);
        if (nodeToRemove.Right != null)
          InsertNode(parent, nodeToRemove.Right);
      }
    }

    private void Collacte(object[] resultArray, ref int index, IBinaryTreeNode current) {

      if (current == null)
        return;

      Collacte(resultArray, ref index, current.Left);
      resultArray[index++] = current.Value;
      Collacte(resultArray, ref index, current.Right);
    }
  }
}
