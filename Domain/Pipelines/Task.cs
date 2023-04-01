namespace Domain.Pipelines;

public class Task : IJob
{
    public List<IJob> Jobs;
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