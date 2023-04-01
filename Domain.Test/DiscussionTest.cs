using Domain.Exceptions;
using Domain.Forum;
using Domain.Notifier;
using Domain.Sprints;
using NSubstitute;

namespace Domain.Test;

public class DiscussionTest
{
    [Fact]
    public void TeamMemberShouldBeAbleToReplyToDiscussion()
    {
        var writer = Substitute.For<IWriter>();

        var user = new TeamMember("Linus Torvalds");
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprintFactory = new SprintFactory();
        var sprint = SprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, user);
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
        
        var user = new TeamMember("Linus Torvalds");
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprintFactory = new SprintFactory();
        var sprint = SprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, user);
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
    
    [Fact]
    public void TeamMembersThatHaveRepliedToADiscussionShouldBeNotifiedWhenThereIsANewReply()
    {
        var writer = Substitute.For<IWriter>();
        var notificationWriter = Substitute.For<IWriter>();
        
        var user = new TeamMember("Linus Torvalds", "linustorvalds@gmail.com");
        var user2 = new TeamMember("John Doe", "johndoe@gmail.com");
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprintFactory = new SprintFactory();
        var sprint = SprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("1", writer, user);
        sprint.AddBacklogItem(backlogItem);
        
        backlogItem.ToDoing();
        backlogItem.ToReadyForTesting();
        backlogItem.ToTesting();
        backlogItem.ToTested();
        backlogItem.ToDone();
        backlogItem.ToTodo();

        user.Subscribe(new NotificationService(new EmailService(notificationWriter), new SlackService(notificationWriter)));
        
        backlogItem.Discussion.Reply(new Reply("Hi there", user));
        backlogItem.Discussion.Reply(new Reply("Hi all", user2));
        
        Assert.Equal(2, backlogItem.Discussion.Replies.Count);
        notificationWriter.Received().WriteLine("To: Linus Torvalds <linustorvalds@gmail.com>: A new message has been posted in 'Backlog: 1' discussion by 'John Doe': Hi all");
    }
}