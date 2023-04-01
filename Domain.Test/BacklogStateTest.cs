using Domain.Exceptions;
using Domain.Notifier;
using Domain.Sprints;
using NSubstitute;

namespace Domain.Test;

//FR-2.1
//FR-4.1
public class BacklogStateTest
{
    //FR-2.4
    [Fact]
    public void ScrumMasterShouldBeNotifiedByEmailWhenBacklogItemMovesFromDoneToTodo()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();
        
        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"));

        var backlogItem = new BacklogItem("1", writer,
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        
        var notificationService = new NotificationService(new EmailService(notificationWriter), new SlackService(notificationWriter));
        sprint.AddBacklogItem(backlogItem);
        
        sprint.ScrumMaster.Subscribe(notificationService);
       
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTesting();
        backlogItem.ToTested();
        backlogItem.ToDone();
        backlogItem.ToTodo();

        notificationWriter.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Backlogitem 1 has been moved from Done to Todo");
    }
    
    //FR-2.4
    //FR-10
    [Fact]
    public void TesterShouldBeNotifiedWhenItemMovesToReadyForTesting()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"));

        var backlogItem = new BacklogItem("1", writer, new TeamMember("Linus Torvalds"));
        sprint.AddBacklogItem(backlogItem);
        
        var notificationService = new NotificationService(new EmailService(notificationWriter), new SlackService(notificationWriter));
        backlogItem.Sprint?.Project.Tester.Subscribe(notificationService);
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        
        notificationWriter.Received().WriteLine("To: Henk de Testerman <henkdetesterman@gmail.com>: Backlogitem 1 is ready for testing");
    }

    //FR-2.4
    //FR-11
    [Fact]
    public void ScrumMasterShouldBeNotifiedWhenBacklogitemMovesFromReadyForTestingToTodo()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"));
        
        var backlogItem = new BacklogItem("1", writer, new TeamMember("Linus Torvalds"));
        sprint.AddBacklogItem(backlogItem);
        
        var notificationService = new NotificationService(new EmailService(notificationWriter), new SlackService(notificationWriter));
        sprint.ScrumMaster.Subscribe(notificationService);
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTodo();
        
        notificationWriter.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Backlogitem 1 has been moved from Ready For Testing to Todo");
    }
    
    //FR-2.4
    [Fact]
    public void ScrumMasterShouldBeNotifiedWhenBacklogitemMovesToDoing()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"));
        
        var backlogItem = new BacklogItem("1", writer, new TeamMember("Linus Torvalds"));
        sprint.AddBacklogItem(backlogItem);
        
        var notificationService = new NotificationService(new EmailService(notificationWriter), new SlackService(notificationWriter));
        sprint.ScrumMaster.Subscribe(notificationService);
        
        backlogItem.ToDoing();

        notificationWriter.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Backlogitem 1 has been moved to Doing");
    }

    [Fact]
    public void BacklogItemShouldThrowExceptionWhenItMovesFromTestingToDoing()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"));
        
        var backlogItem = new BacklogItem("1", writer, new TeamMember("Linus Torvalds"));
        sprint.AddBacklogItem(backlogItem);
        
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTesting();

        Assert.Throws<IllegalStateAdvanceException>(() => backlogItem.ToDoing());
    }
    
    [Fact]
    public void BacklogItemShouldThrowExceptionWhenItGoesFromTodoToTodo()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"));
        
        var backlogItem = new BacklogItem("1", writer, new TeamMember("Linus Torvalds"));
        sprint.AddBacklogItem(backlogItem);
        
        IllegalStateAdvanceException ex = Assert.Throws<IllegalStateAdvanceException>(
            () => backlogItem.ToTodo());

        Assert.Equal("This backlog item is already in Todo", ex.Message);
    }
    
    //FR-2.2
    //FR-3.1
    [Fact]
    public void BacklogItemShouldThrowExceptionWhenItMovesToDoneAndAnActivityIsNotDoneYet()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"));
        
        var backlogItem = new BacklogItem("1", writer,
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        var activity = new BacklogItem("2", writer, new TeamMember("Henk de steen"));
        
        sprint.AddBacklogItem(backlogItem);
        backlogItem.Activities.Add(activity);
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTesting();
        backlogItem.ToTested();

        var ex = Assert.Throws<IllegalStateAdvanceException>(
            () => backlogItem.ToDone());

        Assert.Equal("Cannot move backlogitem to Done: activity 2 is not done yet.", ex.Message);
    }
    
    //FR-2.3
    [Fact]
    public void BacklogItemShouldThrowExceptionWhenItMovesFromReadyForTestingToDoing()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"));
        
        var backlogItem = new BacklogItem("1", writer,
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        var activity = new BacklogItem("2", writer, new TeamMember("Henk de steen"));
        
        sprint.AddBacklogItem(backlogItem);
        backlogItem.Activities.Add(activity);
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();

        Assert.Throws<IllegalStateAdvanceException>(
            () => backlogItem.ToDoing());
    }
    
    //FR-2.3
    [Fact]
    public void BacklogItemShouldThrowExceptionWhenItMovesFromDoneToDoing()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"));
        
        var backlogItem = new BacklogItem("1", writer,
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));

        sprint.AddBacklogItem(backlogItem);

        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTesting();
        backlogItem.ToTested();
        backlogItem.ToDone();

        Assert.Throws<IllegalStateAdvanceException>(
            () => backlogItem.ToDoing());
    }

    
}