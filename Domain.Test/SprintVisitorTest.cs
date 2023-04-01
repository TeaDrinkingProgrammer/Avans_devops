using Domain.Sprints;
using Domain.Sprints.Export;
using NSubstitute;

namespace Domain.Test;

public class SprintVisitorTest
{
    [Fact]
    public void SprintReportBuilderShouldReturnBacklogListWhenBuildIsCalled()
    {
        var writer = Substitute.For<IWriter>();
        var exportStrategy = Substitute.For<IExportStrategy>();
        
        var developer = new TeamMember("Linus Torvalds", "linustorvalds@gmail.com");

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman", "henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = SprintFactory.NewReleaseSprint(project);

        var backlogItem = new BacklogItem("1", writer, developer);
        var backlogItem2 = new BacklogItem("2", writer,  developer);
        
        sprint.AddBacklogItem(backlogItem);
        sprint.AddBacklogItem(backlogItem2);
        
        var sprintReportBuilder = new SprintReportBuilder(exportStrategy, sprint);
        
        sprintReportBuilder.AddBacklogItemsList();
        sprintReportBuilder.Build();
        
        exportStrategy.Received().Export("--------------------BacklogItems--------------------\n--------------------\n1\nDeveloper: Linus Torvalds\nState: Todo\n--------------------\n2\nDeveloper: Linus Torvalds\nState: Todo\n--------------------\n----------------------------------------------------\n\n");
    }
    
    //FR-20
    [Fact]
    public void SprintReportBuilderShouldReturnHeaderAndFooterWhenBuildIsCalledWithAddHeaderAndAddFooter()
    {
        var writer = Substitute.For<IWriter>();
        var exportStrategy = Substitute.For<IExportStrategy>();
        
        var developer = new TeamMember("Linus Torvalds", "linustorvalds@gmail.com");

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman", "henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprintFactory = new SprintFactory();
        var sprint = SprintFactory.NewReleaseSprint(project);

        var backlogItem = new BacklogItem("1", writer, developer);
        var backlogItem2 = new BacklogItem("2", writer,  developer);
        
        sprint.AddBacklogItem(backlogItem);
        sprint.AddBacklogItem(backlogItem2);
        
        var sprintReportBuilder = new SprintReportBuilder(exportStrategy, sprint);

        string[] header = {"Company header: " + sprint.Project.Name, "Author name 1, Author name 2"};
        string[] footer = {"-------------------"};
        sprintReportBuilder.AddHeader(header);
        sprintReportBuilder.AddFooter(footer);
        sprintReportBuilder.Build();
        
        exportStrategy.Received().Export("Company header\nAuthor name 1, Author name 2\n\n\n-------------------\n\n");
    }

}