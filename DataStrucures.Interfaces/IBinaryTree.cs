namespace DataStructures.Interfaces {
  public interface IBinaryTreeNode {
    IBinaryTreeNode Left { get; }
    IBinaryTreeNode Right { get; }
    object Value { get; }
  }

  public interface IBinaryTree : ICollection {
    IBinaryTreeNode Root { get; }
    IBinaryTreeNode Add(object value);
    void Remove(object value);
    void Remove(IBinaryTreeNode value);
    IBinaryTreeNode Find(object value);
  }
}
