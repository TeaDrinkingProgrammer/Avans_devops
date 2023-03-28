using Domain.Pipeline.Actions;

namespace Domain.Pipeline
{
    public interface IVisitor
    {
        public void VisitPipeline(Pipeline pipeline);
        public void VisitTask(Task task);
        public void VisitSource(Source source);
        public void VisitPackage(Package package);
        public void VisitBuild(Build build);
        public void VisitTest(Test test);
        public void VisitAnalyse(Analyse analyse);
        public void VisitDeploy(Deploy deploy);
        public void VisitUtility(Utility utility);
    }
}
