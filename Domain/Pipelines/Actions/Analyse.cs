namespace Domain.Pipelines.Actions;

public class Analyse : Action
{
    public override void Accept(IVisitor visitor)
    {
        visitor.VisitAnalyse(this);
    }

    public Analyse(string args) : base(args)
    {
    }
}