using Domain.Pipelines;
using Domain.Pipelines.Actions;
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
        
        Assert.Equal(2, pipeline.tasks.Count);
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
        
        Assert.Equal(7, pipeline.tasks[0].Jobs.Count + pipeline.tasks[1].Jobs.Count);
    }
}