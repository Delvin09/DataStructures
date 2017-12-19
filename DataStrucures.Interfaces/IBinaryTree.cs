using System;

namespace DataStructures.Interfaces {
  public interface IBinaryTreeNode : IComparable {
    IBinaryTreeNode Left {
      get; set;
    }
    IBinaryTreeNode Right {
      get; set;
    }
    IComparable Value { get; }
  }

  public interface IBinaryTree : ICollection {
    IBinaryTreeNode Root { get; }
    IBinaryTreeNode Add(IComparable value);
    void Remove(IComparable value);
    IBinaryTreeNode Find(IComparable value);
  }
}
