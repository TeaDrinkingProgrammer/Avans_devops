using Domain.Notifier;
using Domain.Sprints;
using NSubstitute;

namespace Domain.Test;

//FR-9
public class TeamMemberNotifierTest
{
    [Fact]
    public void NotifyScrumMasterViaEmail()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"));
        
        var notificationService = new NotificationService(new EmailService(writer), new SlackService(writer));
        sprint.ScrumMaster.Subscribe(notificationService);
        sprint.ScrumMaster.Notify("Hello scrummaster!");

        writer.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Hello scrummaster!");
    }
    
    [Fact]
    public void UnsubscribeEmailNotifier()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"));

        var notificationService = new NotificationService(new EmailService(writer), new SlackService(writer));
        var unsubscriber = sprint.ScrumMaster.Subscribe(notificationService);
        unsubscriber.Dispose();
        
        sprint.ScrumMaster.Notify("Hello scrummaster!");
        
        writer.DidNotReceive().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Hello scrummaster!");
    }
}