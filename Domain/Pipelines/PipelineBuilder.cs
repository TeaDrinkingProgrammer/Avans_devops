using Domain.Pipelines.Actions;

namespace Domain.Pipelines;

//Pattern used: Builder
public class PipelineBuilder
{
    public PipelineBuilder(string pipelineName, IVisitor visitor)
    {
        _pipeline = new Pipeline(pipelineName, visitor);
    }

    public static TaskBuilder CreateTask(string taskName)
    {
        return new TaskBuilder(taskName);
    }
    
    public PipelineBuilder AddTask(Task task)
    {
        _pipeline.Tasks.Add(task);
        return this;
    }

    public IPipeline Build()
    {
        return _pipeline;
    }

    private readonly Pipeline _pipeline;
    
    public class TaskBuilder
    {
        public TaskBuilder(string taskName)
        {
            _task = new Task(taskName);
        }

        public TaskBuilder AddTask(Task task)
        {
            _task.Jobs.Add(task);
            return this;
        }

        public TaskBuilder AddSource(string args)
        {
            _task.Jobs.Add(new Source(args));
            return this;
        }
        
        public TaskBuilder AddPackage(string args)
        {
            _task.Jobs.Add(new Package(args));
            return this;
        }
        
        public TaskBuilder AddBuild(string args)
        {
            _task.Jobs.Add(new Build(args));
            return this;
        }
        
        public TaskBuilder AddTest(string args)
        {
            _task.Jobs.Add(new Test(args));
            return this;
        }
        
        public TaskBuilder AddAnalyse(string args)
        {
            _task.Jobs.Add(new Analyse(args));
            return this;
        }
        
        public TaskBuilder AddDeploy(string args)
        {
            _task.Jobs.Add(new Deploy(args));
            return this;
        }
        
        public TaskBuilder AddUtility(string args)
        {
            _task.Jobs.Add(new Utility(args));
            return this;
        }

        public Task Build()
        {
            return _task;
        }

        private readonly Task _task;
    }
}