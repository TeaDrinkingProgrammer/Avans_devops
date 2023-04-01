namespace Domain.Pipelines.Actions;

public class Package : Action
{
    public override void Accept(IVisitor visitor)
    {
        visitor.VisitPackage(this);
    }

    public Package(string args) : base(args)
    {
    }
}