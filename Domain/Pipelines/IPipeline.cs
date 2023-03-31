namespace Domain.Pipelines;

public interface IPipeline
{
    public List<Task> tasks { get; set; }
    public bool Run();
    public void Accept(IVisitor visitor);
}