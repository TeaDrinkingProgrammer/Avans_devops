namespace Domain.Pipelines;

//Pattern used: Composite
public class Task : IJob
{
    public ICollection<IJob> Jobs { get; }
    public readonly string Name;

    public Task(string name)
    {
        Name = name;
        Jobs = new List<IJob>();
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitTask(this);
        foreach (var job in Jobs)
        {
            job.Accept(visitor);
        }
    }
}