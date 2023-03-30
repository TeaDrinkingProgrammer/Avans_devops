using System.Diagnostics;
using Domain.Notifier;
using NSubstitute;

namespace Domain.Test;

public class TeamMemberNotifierTest
{
    [Fact]
    public void NotifyScrumMasterViaEmail()
    {
        var writer = Substitute.For<IWriter>();

        var sprint = new Sprint(
            new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"), 
            new TeamMember("Henk de Testerman", "jandescrumman@gmail.com")
            );
        var notificationService = new NotificationService(new EmailService(writer), new SlackService(writer));
        sprint.Subscribe(notificationService);
        sprint.NotifyScrumMaster("Hello scrummaster!");

        writer.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Hello scrummaster!");
    }
    [Fact]
    public void UnsubscribeEmailNotifier()
    {
        var writer = Substitute.For<IWriter>();

        var sprint = new Sprint(
            new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"), 
            new TeamMember("Henk de Testerman", "jandescrumman@gmail.com")
            );
        var notificationService = new NotificationService(new EmailService(writer), new SlackService(writer));
        var unsubscriber = sprint.Subscribe(notificationService);
        unsubscriber.Dispose();
        
        sprint.NotifyScrumMaster("Hello scrummaster!");
        
        writer.DidNotReceive().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Hello scrummaster!");
    }
}