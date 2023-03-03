using System.Diagnostics;
using NSubstitute;

namespace Domain.Test;

public class TeamMemberNotifierTest
{
    [Fact]
    public void NotifyScrumMasterViaEmail()
    {
        var writer = Substitute.For<IWriter>();
        
        var sprint = new Sprint();
        sprint.ScrumMaster = new TeamMember("Jan de Testerman");
        sprint.ScrumMaster.Subscribe(new EmailNotifier("jandetesterman@gmail.com", writer));
        sprint.ScrumMaster.Notify(new TeamMemberNotification(sprint.ScrumMaster, "Hello scrummaster!"));

        writer.Received().WriteLine("To: Jan de Testerman <jandetesterman@gmail.com>: Hello scrummaster!");
    }
}