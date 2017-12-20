using DataStructures.Interfaces;

namespace DataStructures {

  public partial class BinaryTree {

    private class MoveState : BinaryTreeEnumeratorState {

      [System.Flags]
      private enum MoveStateStatus {

        New = 0,
        LeftDone = 1,
        RightDone = 2,
        AllDone = LeftDone | RightDone,
      }

      private MoveStateStatus status = MoveStateStatus.New;

      public MoveState(IBinaryTreeNode previos, IBinaryTreeNode current, BinaryTreeEnumeratorState prevState)
        : base(previos, current, prevState) {
      }

      public override BinaryTreeEnumeratorState ChangeState() {

        if (CurrentNode.Left != null && !status.HasFlag(MoveStateStatus.LeftDone)) {
          status |= MoveStateStatus.LeftDone;
          return new MoveState(CurrentNode, CurrentNode.Left, this);
        }
        else if (CurrentNode.Right != null && !(status.HasFlag(MoveStateStatus.RightDone))) {
          status |= MoveStateStatus.RightDone;
          return new MoveState(CurrentNode, CurrentNode.Right, this);
        }
        else if (PreviosState != null)
          return PreviosState.ChangeState();
        else
          return new DoneState();
      }
    }

    private class DoneState : BinaryTreeEnumeratorState {
      public DoneState() : base(null, null, null) { }

      public override BinaryTreeEnumeratorState ChangeState() => this;
    }

    private abstract class BinaryTreeEnumeratorState {
      public readonly IBinaryTreeNode CurrentNode;
      public readonly IBinaryTreeNode PreviosNode;
      public readonly BinaryTreeEnumeratorState PreviosState;

      public BinaryTreeEnumeratorState(IBinaryTreeNode previos, IBinaryTreeNode current, BinaryTreeEnumeratorState prevState) {
        this.PreviosNode = previos;
        this.CurrentNode = current;
        this.PreviosState = prevState;
      }

      public abstract BinaryTreeEnumeratorState ChangeState();
    }
  }
}
