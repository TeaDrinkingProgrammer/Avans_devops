namespace Domain.Pipelines.Actions;

public abstract class Action : IJob
{
    public readonly string Args;
    public abstract void Accept(IVisitor visitor);

    protected Action(string args)
    {
        Args = args;
    }
}