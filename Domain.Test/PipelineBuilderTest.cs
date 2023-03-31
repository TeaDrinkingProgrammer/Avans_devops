using Domain.Pipelines;
using Domain.Pipelines.Actions;

namespace Domain.Test;

public class PipelineBuilderTest
{
    [Fact] private void PipelineBuilderShouldCreateTasks()
    {
        var plb = new PipelineBuilder("deployment pipeline");
        var pipeline = plb.AddTask(plb.CreateTask("build and test")
                .AddSource("./src/")
                .AddPackage("xUnit")
                .AddBuild("./out")
                .AddTest("-coverage")
                .Build())
            .AddTask(plb.CreateTask("analyse and deploy")
                .AddAnalyse("-report")
                .AddUtility("saveLogs.cs")
                .AddDeploy("https://application.z22.web.core.windows.net")
                .Build())
            .Build();
        
        Assert.Equal(2, pipeline.tasks.Count);
    }
    
    [Fact] private void PipelineBuilderShouldCreateJobs()
    {
        var plb = new PipelineBuilder("deployment pipeline");
        var pipeline = plb.AddTask(plb.CreateTask("build and test")
                .AddSource("./src/")
                .AddPackage("xUnit")
                .AddBuild("./out")
                .AddTest("-coverage")
                .Build())
            .AddTask(plb.CreateTask("analyse and deploy")
                .AddAnalyse("-report")
                .AddUtility("saveLogs.cs")
                .AddDeploy("https://application.z22.web.core.windows.net")
                .Build())
            .Build();
        
        Assert.Equal(7, pipeline.tasks[0].jobs.Count + pipeline.tasks[1].jobs.Count);
    }
}