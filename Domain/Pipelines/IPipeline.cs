namespace Domain.Pipelines;

public interface IPipeline
{
    public List<Task> Tasks { get; set; }
    public bool Run();
    public void Accept(IVisitor visitor);
}