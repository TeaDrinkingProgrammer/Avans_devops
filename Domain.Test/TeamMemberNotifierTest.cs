using System.Diagnostics;
using Domain.Sprints;
using NSubstitute;

namespace Domain.Test;

public class TeamMemberNotifierTest
{
    [Fact]
    public void NotifyScrumMasterViaEmail()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        project.ScrumMaster.Subscribe(new EmailNotifier("jandescrumman@gmail.com", writer));
        project.ScrumMaster.Notify("Hello scrummaster!");

        writer.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Hello scrummaster!");
    }
    [Fact]
    public void UnsubscribeEmailNotifier()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman"), new TeamMember("Henk de Testerman"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var emailNotifier = new EmailNotifier("jandescrumman@gmail.com", writer);
        var unsubscriber = project.ScrumMaster.Subscribe(emailNotifier);
        unsubscriber.Dispose();
        
        project.ScrumMaster.Notify("Hello scrummaster!");
        
        writer.DidNotReceive().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Hello scrummaster!");
    }
}