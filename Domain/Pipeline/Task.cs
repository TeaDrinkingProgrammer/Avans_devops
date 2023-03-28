namespace Domain.Pipeline;

public class Task : IJob
{
    public List<IJob> jobs;
    public string name;

    public Task(string name)
    {
        this.name = name;
        jobs = new List<IJob>();
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitTask(this);
        foreach (var job in jobs)
        {
            job.Accept(visitor);
        }
    }
}