using Domain.Notifier.Events;

namespace Domain.Notifier;

//Pattern used: Observer
public class NotificationService : IObserver<Notification>
{
    private readonly EmailService _emailService;
    private readonly SlackService _slackService;

    public NotificationService(EmailService emailService, SlackService slackService)
    {
        _emailService = emailService;
        _slackService = slackService;
    }

    void IObserver<Notification>.OnCompleted()
    {
        // not implemented
    }

    void IObserver<Notification>.OnError(Exception error)
    {
        // not implemented
    }

    public void OnNext(Notification notification)
    {
        if (notification.TeamMember.Email != null)
                _emailService.Send($"{notification.TeamMember.Name} <{notification.TeamMember.Email}>", notification.Message);

        if (notification.TeamMember.SlackHandle != null)
            _slackService.Send(notification.TeamMember.SlackHandle, notification.Message);
    }
}