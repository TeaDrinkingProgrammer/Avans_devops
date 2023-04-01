namespace Domain.Sprints.Export;

public interface ISprintVisitable
{
    public void Accept(ISprintVisitor visitor);
}