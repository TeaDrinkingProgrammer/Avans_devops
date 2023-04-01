namespace Domain.Sprints.Export;

//Pattern used: Visitor
public interface ISprintVisitor
{

    public abstract void VisitSprint(Sprint sprint);
    public abstract void VisitBacklogItem(BacklogItem backlogItem);
    
}