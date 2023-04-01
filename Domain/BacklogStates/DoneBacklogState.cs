using Domain.Exceptions;

namespace Domain.BacklogStates;

public class DoneBacklogState : BacklogState
{
    public DoneBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Done")
    {
    }
    public override void SetState()
    {
        foreach (var activity in BacklogItem.Activities)
        {
            if (activity.State.GetType() != typeof(DoneBacklogState))
            {
                throw new IllegalStateAdvanceException($"Cannot move backlogitem to Done: activity {activity.Name} is not done yet.");
            }
        }
        BacklogItem.Discussion.Close();
    }

    public override void ToTodo()
    {
        BacklogItem.Sprint?.ScrumMaster.Notify($"Backlogitem {BacklogItem.Name} has been moved from Done to Todo");
        AdvanceState(BacklogItem.TodoBacklogState);
        BacklogItem.Discussion.Open();
    }

    public override void ToDone()
    {
        CurrentBranchMessage();
    }
}