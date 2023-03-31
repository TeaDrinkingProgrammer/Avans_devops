namespace Domain.Pipelines;

public class Pipeline
{
    public List<Task> tasks { get; set; }
    public string name;
    private readonly IVisitor _visitor;

    public Pipeline(string name, IVisitor visitor)
    {
        this.name = name;
        tasks = new List<Task>();
        
        _visitor = visitor;
    }

    public bool Run()
    {
        try
        {
            Accept(_visitor);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
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