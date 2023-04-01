namespace Domain.Sprints.Export;
public interface ISprintVisitor
{

    public abstract void VisitSprint(Sprint sprint);
    public abstract void VisitBacklogItem(BacklogItem backlogItem);
    
}