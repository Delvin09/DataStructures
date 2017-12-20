using DataStructures.Interfaces;

namespace DataStructures {

  public partial class BinaryTree {

    private class BinaryTreeEnumerator : System.Collections.IEnumerator {

      private IBinaryTreeNode root;
      private BinaryTreeEnumeratorState state;

      public BinaryTreeEnumerator(IBinaryTreeNode root) {
        this.root = root;
      }

      public object Current => state.CurrentNode.Value;

      public bool MoveNext() {
        if (state == null) {
          state = new MoveState(null, root, null);
          return true;
        }
        return !((state = state.ChangeState()) is DoneState);
      }

      public void Reset() {
        state = null;
      }
    }
  }
}
