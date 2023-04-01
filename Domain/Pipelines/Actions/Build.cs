namespace Domain.Pipelines.Actions;

public class Build : Action
{ 
    public override void Accept(IVisitor visitor)
    {
        visitor.VisitBuild(this);
    }

    public Build(string args) : base(args)
    {
    }
}