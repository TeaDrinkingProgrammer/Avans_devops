namespace Domain.Pipelines;

//Pattern used: Composite
public class Task : IJob
{
    public readonly List<IJob> Jobs;
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