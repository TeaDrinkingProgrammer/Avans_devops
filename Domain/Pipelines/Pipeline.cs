namespace Domain.Pipelines;

public class Pipeline
{
    public List<Task> tasks { get; set; }
    public string name;

    public Pipeline(string name)
    {
        this.name = name;
        tasks = new List<Task>();
    }
        
    public void Accept(IVisitor visitor)
    {
        visitor.VisitPipeline(this);
        foreach (var task in tasks)
        {
            task.Accept(visitor);
        }
    }
}