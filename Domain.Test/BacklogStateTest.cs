using System.Diagnostics;
using NSubstitute;

namespace Domain.Test;

public class BacklogStateTest
{
    [Fact]
    public void ScrumMasterShouldBeNotifiedWhenBacklogItemMovesFromDoneToTodo()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();

        var sprint = new Sprint(new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var backlogItem = new BacklogItem("1", writer, sprint);
        sprint.ScrumMaster.Subscribe(new EmailNotifier("jandescrumman@gmail.com", notificationWriter));
       
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

        var sprint = new Sprint(new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var backlogItem = new BacklogItem("1", writer, sprint);
        
        sprint.Tester.Subscribe(new EmailNotifier("henkdetesterman@gmail.com", notificationWriter));
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        
        notificationWriter.Received().WriteLine("To: Henk de Testerman <henkdetesterman@gmail.com>: Backlogitem 1 is ready for testing");
    }

    [Fact]
    public void ScrumMasterShouldBeNotifiedWhenBacklogitemMovesFromReadyForTestingToTodo()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();

        var sprint = new Sprint(new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var backlogItem = new BacklogItem("1", writer, sprint);
        
        sprint.ScrumMaster.Subscribe(new EmailNotifier("jandescrumman@gmail.com", notificationWriter));
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTodo();
        
        notificationWriter.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Backlogitem 1 has been moved from Ready For Testing to Todo");
    }
    
}