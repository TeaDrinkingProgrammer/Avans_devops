using Domain.Notifier;
using NSubstitute;

namespace Domain.Test;

public class TeamMemberNotifierTest
{
    [Fact]
    public void NotifyScrumMasterViaEmail()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));

        var notificationService = new NotificationService(new EmailService(writer), new SlackService(writer));
        project.ScrumMaster.Subscribe(notificationService);
        project.ScrumMaster.Notify("Hello scrummaster!");

        writer.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Hello scrummaster!");
    }
    
    [Fact]
    public void UnsubscribeEmailNotifier()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));

        var notificationService = new NotificationService(new EmailService(writer), new SlackService(writer));
        var unsubscriber = project.ScrumMaster.Subscribe(notificationService);
        unsubscriber.Dispose();
        
        project.ScrumMaster.Notify("Hello scrummaster!");
        
        writer.DidNotReceive().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Hello scrummaster!");
    }
}