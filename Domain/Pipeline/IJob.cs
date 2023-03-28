namespace Domain.Pipeline
{
    public interface IJob
    {
        public void Accept(IVisitor visitor);
    }
}
