using Domain.Exceptions;

namespace Domain;

public class DoneBacklogState : BacklogState
{
    public DoneBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Done")
    {
    }
    public override void SetState()
    {
        foreach (var activity in _backlogItem.Activities)
        {
            if (activity.State.GetType() != typeof(DoneBacklogState))
            {
                throw new IllegalStateAdvanceException($"Cannot move backlogitem to Done: activity {activity.Name} is not done yet.");
            }
        }
        _backlogItem.Discussion.Close();
    }

    public override void ToTodo()
    {
        _backlogItem.Sprint.Project.NotifyScrumMaster($"Backlogitem {_backlogItem.Name} has been moved from Done to Todo");
        AdvanceState(_backlogItem.TodoBacklogState);
        _backlogItem.Discussion.Open();
    }

    public override void ToDoing()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToReadyForTesting()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToTesting()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToTested()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToDone()
    {
        CurrentBranchMessage();
    }
}