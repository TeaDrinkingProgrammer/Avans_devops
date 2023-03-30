using Domain.Exceptions;
using Domain.Forum;
using Domain.Pipeline;
using Domain.Sprints;
using NSubstitute;

namespace Domain.Test;

public class DiscussionTest
{
    [Fact]
    public void TeamMemberShouldBeAbleToReplyToDiscussion()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();
        var user = new TeamMember("Linus Torvalds");
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, sprint, user);
        sprint.AddBacklogItem(backlogItem);
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTesting();
        backlogItem.ToTested();
        backlogItem.ToDone();
        backlogItem.ToTodo();
        
        backlogItem.Discussion.Reply(new Reply("Hi there", user));
        
        Assert.Single(backlogItem.Discussion.Replies);
        Assert.Equal("Hi there", backlogItem.Discussion.Replies[0].Content);
    }
    [Fact]
    public void TeamMemberShouldNotBeAbleToReplyToDiscussionWhenBacklogItemIsDone()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();
        var user = new TeamMember("Linus Torvalds");
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, sprint, user);
        sprint.AddBacklogItem(backlogItem);
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTesting();
        backlogItem.ToTested();
        backlogItem.ToDone();
        
        Assert.Throws<DiscussionClosedException>(
            () => backlogItem.Discussion.Reply(new Reply("Hi there", user)));
        Assert.Empty(backlogItem.Discussion.Replies);
    }
}