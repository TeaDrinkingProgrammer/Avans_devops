using Domain.Notifier;
using Domain.Notifier.Events;
using NSubstitute;

namespace Domain.Test;

public class NotificationTest
{
    [Fact]
    public void NotificationServiceShouldSendEmailNotifications()
    {
        var notificationWriter = Substitute.For<IWriter>();
        var notificationService = new NotificationService(new EmailService(notificationWriter), new SlackService(notificationWriter));
        var teamMember = new TeamMember("John Doe", "johndoe@mail.com", "johndoe");
        
        notificationService.OnNext(new Notification(teamMember, "Hello World!"));
        
        notificationWriter.Received().WriteLine("To: John Doe <johndoe@mail.com>: Hello World!");
    }
    
    [Fact]
    public void NotificationServiceShouldSendSlackNotifications()
    {
        var notificationWriter = Substitute.For<IWriter>();
        var notificationService = new NotificationService(new EmailService(notificationWriter), new SlackService(notificationWriter));
        var teamMember = new TeamMember("John Doe", "johndoe@mail.com", "johndoe");
        
        notificationService.OnNext(new Notification(teamMember, "Hello World!"));
        
        notificationWriter.Received().WriteLine("@<johndoe>: Hello World!");
    }
}