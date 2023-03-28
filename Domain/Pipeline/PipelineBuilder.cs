using Domain.Pipeline.Actions;

namespace Domain.Pipeline;

public class PipelineBuilder
{
    public PipelineBuilder(string pipelineName)
    {
        _pipeline = new Pipeline(pipelineName);
    }

    public TaskBuilder CreateTask(string taskName)
    {
        return new TaskBuilder(taskName);
    }
    
    public PipelineBuilder AddTask(Task task)
    {
        _pipeline.tasks.Add(task);
        return this;
    }

    public Pipeline Build()
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
        
        public TaskBuilder CreateTask(string taskName)
        {
            return new TaskBuilder(taskName);
        }
        
        public TaskBuilder AddTask(Task task)
        {
            _task.jobs.Add(task);
            return this;
        }

        public TaskBuilder AddSource(string args)
        {
            _task.jobs.Add(new Source(args));
            return this;
        }
        
        public TaskBuilder AddPackage(string args)
        {
            _task.jobs.Add(new Package(args));
            return this;
        }
        
        public TaskBuilder AddBuild(string args)
        {
            _task.jobs.Add(new Build(args));
            return this;
        }
        
        public TaskBuilder AddTest(string args)
        {
            _task.jobs.Add(new Test(args));
            return this;
        }
        
        public TaskBuilder AddAnalyse(string args)
        {
            _task.jobs.Add(new Analyse(args));
            return this;
        }
        
        public TaskBuilder AddDeploy(string args)
        {
            _task.jobs.Add(new Deploy(args));
            return this;
        }
        
        public TaskBuilder AddUtility(string args)
        {
            _task.jobs.Add(new Utility(args));
            return this;
        }

        public Task Build()
        {
            return _task;
        }

        private readonly Task _task;
    }
}