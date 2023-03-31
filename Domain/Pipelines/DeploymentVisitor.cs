using Domain.Pipelines.Actions;

namespace Domain.Pipelines;

public class DeploymentVisitor : IVisitor
{
    private readonly IWriter _writer;

    public DeploymentVisitor(IWriter writer)
    {
        _writer = writer;
    }

    public void VisitPipeline(Pipeline pipeline)
    {
        _writer.WriteLine($"executing {pipeline.Name} pipeline"); 
    }

    public void VisitTask(Task task)
    {
        _writer.WriteLine($"executing {task.Name} task");
    }

    public void VisitSource(Source source)
    {
        _writer.WriteLine($"retrieving source from {source.Args}");
    }

    public void VisitPackage(Package package)
    {
        _writer.WriteLine($"installing package {package.Args}");
    }

    public void VisitBuild(Build build)
    {
        _writer.WriteLine($"building to directory {build.Args}");
    }

    public void VisitTest(Test test)
    {
       _writer.WriteLine($"executing tests {test.Args}");
    }

    public void VisitAnalyse(Analyse analyse)
    {
        _writer.WriteLine($"generating code analysis {analyse.Args}");
    }

    public void VisitDeploy(Deploy deploy)
    {
        _writer.WriteLine($"deploying to {deploy.Args}");
    }

    public void VisitUtility(Utility utility)
    {
        _writer.WriteLine($"executing script {utility.Args}");
    }
}