using System.Diagnostics;
using NSubstitute;

namespace Domain.Test;

public class TeamMemberNotifierTest
{
    [Fact]
    public void NotifyScrumMasterViaEmail()
    {
        var writer = Substitute.For<IWriter>();

        var sprint = new Sprint(new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        sprint.ScrumMaster.Subscribe(new EmailNotifier("jandescrumman@gmail.com", writer));
        sprint.ScrumMaster.Notify(new TeamMemberNotification(sprint.ScrumMaster, "Hello scrummaster!"));

        writer.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Hello scrummaster!");
    }
}