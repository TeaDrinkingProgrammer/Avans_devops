namespace Domain.Pipelines.Actions;

public class Deploy : Action
{
    public override void Accept(IVisitor visitor)
    {
        visitor.VisitDeploy(this);
    }

    public Deploy(string args) : base(args)
    {
    }
}