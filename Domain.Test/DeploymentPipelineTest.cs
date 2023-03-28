﻿using Domain.Pipeline;
using NSubstitute;

namespace Domain.Test;

public class DeploymentPipelineTest
{
    [Fact]
    public void PipelineShouldExecuteSourceActions()
    {
        var pipelineWriter = Substitute.For<IWriter>();
        
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

        pipeline.Accept(new DeploymentVisitor(pipelineWriter));
        pipelineWriter.Received().WriteLine("retrieving source from ./src/");
    }
    
    [Fact]
    public void PipelineShouldExecutePackageActions()
    {
        var pipelineWriter = Substitute.For<IWriter>();
        
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

        pipeline.Accept(new DeploymentVisitor(pipelineWriter));
        pipelineWriter.Received().WriteLine("installing package xUnit");
    }
    
    [Fact]
    public void PipelineShouldExecuteBuildActions()
    {
        var pipelineWriter = Substitute.For<IWriter>();
        
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

        pipeline.Accept(new DeploymentVisitor(pipelineWriter));
        pipelineWriter.Received().WriteLine("building to directory ./out");
    }
    
    [Fact]
    public void PipelineShouldExecuteTestActions()
    {
        var pipelineWriter = Substitute.For<IWriter>();
        
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

        pipeline.Accept(new DeploymentVisitor(pipelineWriter));
        pipelineWriter.Received().WriteLine("executing tests -coverage");
    }
    
    [Fact]
    public void PipelineShouldExecuteAnalyseActions()
    {
        var pipelineWriter = Substitute.For<IWriter>();
        
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

        pipeline.Accept(new DeploymentVisitor(pipelineWriter));
        pipelineWriter.Received().WriteLine("generating code analysis -report");
    }
    
    [Fact]
    public void PipelineShouldExecuteUtilityActions()
    {
        var pipelineWriter = Substitute.For<IWriter>();
        
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

        pipeline.Accept(new DeploymentVisitor(pipelineWriter));
        pipelineWriter.Received().WriteLine("executing script saveLogs.cs");
    }
    
    [Fact]
    public void PipelineShouldExecuteDeployActions()
    {
        var pipelineWriter = Substitute.For<IWriter>();
        
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

        pipeline.Accept(new DeploymentVisitor(pipelineWriter));
        pipelineWriter.Received().WriteLine("deploying to https://application.z22.web.core.windows.net");
    }
    
    [Fact]
    public void PipelineShouldExecuteNestedTasks()
    {
        var pipelineWriter = Substitute.For<IWriter>();
        
        var plb = new PipelineBuilder("deployment pipeline");
        var pipeline = plb.AddTask(plb.CreateTask("build and test")
                .AddTask(plb.CreateTask("execute scripts")
                    .AddUtility("saveLogs.cs")
                    .AddUtility("saveResults.cs")
                    .Build())
                .AddTask(plb.CreateTask("build application")
                    .AddSource("./src/")
                    .AddPackage("xUnit")
                    .AddBuild("./out")
                    .Build())
                .Build())
            .Build();
             

        pipeline.Accept(new DeploymentVisitor(pipelineWriter));
        pipelineWriter.Received().WriteLine("executing execute scripts task");
    }
}