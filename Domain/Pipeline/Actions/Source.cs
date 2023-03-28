namespace Domain.Pipeline.Actions
{
    public class Source : Action
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.VisitSource(this);
        }

        public Source(string args) : base(args)
        {
        }
    }
}
