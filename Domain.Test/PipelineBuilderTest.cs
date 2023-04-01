using Domain.Pipelines;
using NSubstitute;

namespace Domain.Test;

public class PipelineBuilderTest
{
    [Fact] private void PipelineBuilderShouldCreateTasks()
    {
        var pipelineWriter = Substitute.For<IWriter>();
        
        var plb = new PipelineBuilder("deployment pipeline", new DeploymentVisitor(pipelineWriter));
        var pipeline = plb.AddTask(PipelineBuilder.CreateTask("build and test")
                .AddSource("./src/")
                .AddPackage("xUnit")
                .AddBuild("./out")
                .AddTest("-coverage")
                .Build())
            .AddTask(PipelineBuilder.CreateTask("analyse and deploy")
                .AddAnalyse("-report")
                .AddUtility("saveLogs.cs")
                .AddDeploy("https://application.z22.web.core.windows.net")
                .Build())
            .Build();
        
        Assert.Equal(2, pipeline.Tasks.Count);
    }
    
    [Fact] private void PipelineBuilderShouldCreateJobs()
    {
        var pipelineWriter = Substitute.For<IWriter>();
        
        var plb = new PipelineBuilder("deployment pipeline", new DeploymentVisitor(pipelineWriter));
        var pipeline = plb.AddTask(PipelineBuilder.CreateTask("build and test")
                .AddSource("./src/")
                .AddPackage("xUnit")
                .AddBuild("./out")
                .AddTest("-coverage")
                .Build())
            .AddTask(PipelineBuilder.CreateTask("analyse and deploy")
                .AddAnalyse("-report")
                .AddUtility("saveLogs.cs")
                .AddDeploy("https://application.z22.web.core.windows.net")
                .Build())
            .Build();
        
        Assert.Equal(7, pipeline.Tasks[0].Jobs.Count + pipeline.Tasks[1].Jobs.Count);
    }
}