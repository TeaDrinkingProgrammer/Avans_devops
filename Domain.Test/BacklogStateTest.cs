using Domain.Exceptions;
using Domain.Sprints;
using NSubstitute;

namespace Domain.Test;

public class BacklogStateTest
{
    [Fact]
    public void ScrumMasterShouldBeNotifiedWhenBacklogItemMovesFromDoneToTodo()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, sprint, new TeamMember("Linus Torvalds"));
        project.ScrumMaster.Subscribe(new EmailNotifier("jandescrumman@gmail.com", notificationWriter));
       
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTesting();
        backlogItem.ToTested();
        backlogItem.ToDone();
        backlogItem.ToTodo();

        notificationWriter.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Backlogitem 1 has been moved from Done to Todo");
    }
    
    [Fact]
    public void TesterShouldBeNotifiedWhenItemMovesToReadyForTesting()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);

        var backlogItem = new BacklogItem("1", writer, sprint, new TeamMember("Linus Torvalds"));
            
        project.Tester.Subscribe(new EmailNotifier("henkdetesterman@gmail.com", notificationWriter));
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        
        notificationWriter.Received().WriteLine("To: Henk de Testerman <henkdetesterman@gmail.com>: Backlogitem 1 is ready for testing");
    }

    [Fact]
    public void ScrumMasterShouldBeNotifiedWhenBacklogitemMovesFromReadyForTestingToTodo()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, sprint, new TeamMember("Linus Torvalds"));
        
        project.ScrumMaster.Subscribe(new EmailNotifier("jandescrumman@gmail.com", notificationWriter));
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTodo();
        
        notificationWriter.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Backlogitem 1 has been moved from Ready For Testing to Todo");
    }
    [Fact]
    public void ScrumMasterShouldBeNotifiedWhenBacklogitemMovesToDoing()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, sprint, new TeamMember("Linus Torvalds"));
        
        project.ScrumMaster.Subscribe(new EmailNotifier("jandescrumman@gmail.com", notificationWriter));
        
        backlogItem.ToDoing();

        notificationWriter.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Backlogitem 1 has been moved to Doing");
    }
    
    [Fact]
    public void BacklogItemShouldThrowExceptionWhenItMovesFromTestingToDoing()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, sprint, new TeamMember("Linus Torvalds"));
        
        project.ScrumMaster.Subscribe(new EmailNotifier("jandescrumman@gmail.com", notificationWriter));
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTesting();

        Assert.Throws<IllegalStateAdvanceException>(() => backlogItem.ToDoing());
    }
    
    [Fact]
    public void BacklogItemShouldThrowExceptionWhenItGoesFromTodoToTodo()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, sprint, new TeamMember("Linus Torvalds"));
        
        project.ScrumMaster.Subscribe(new EmailNotifier("jandescrumman@gmail.com", notificationWriter));
        
        IllegalStateAdvanceException ex = Assert.Throws<IllegalStateAdvanceException>(
            () => backlogItem.ToTodo());

        Assert.Equal("This backlog item is already in Todo", ex.Message);
    }    
    [Fact]
    public void BacklogItemShouldThrowExceptionWhenItMovesToDoneAndAnActivityIsNotDoneYet()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, sprint, new TeamMember("Linus Torvalds"));
        var activity = new BacklogItem("2", writer, sprint, new TeamMember("Henk de steen"));
        backlogItem.Activities.Add(activity);
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTesting();
        backlogItem.ToTested();

        var ex = Assert.Throws<IllegalStateAdvanceException>(
            () => backlogItem.ToDone());

        Assert.Equal("Cannot move backlogitem to Done: activity 2 is not done yet.", ex.Message);
    }
    
}