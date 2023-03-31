namespace Domain.Pipelines
{
    public interface IJob
    {
        public void Accept(IVisitor visitor);
    }
}
