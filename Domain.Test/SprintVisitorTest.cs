using Domain.Sprints;
using Domain.Sprints.Export;
using NSubstitute;

namespace Domain.Test;

//FR-19
//FR-21
public class SprintVisitorTest
{
    [Fact]
    public void SprintReportBuilderShouldReturnBacklogListWhenBuildIsCalled()
    {
        var writer = Substitute.For<IWriter>();
        var exportStrategy = Substitute.For<IExportStrategy>();
        
        var developer = new TeamMember("Linus Torvalds", "linustorvalds@gmail.com");

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman", "henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"));

        var backlogItem = new BacklogItem("1", writer, developer);
        var backlogItem2 = new BacklogItem("2", writer,  developer);
        
        sprint.AddBacklogItem(backlogItem);
        sprint.AddBacklogItem(backlogItem2);
        
        var sprintReportBuilder = new SprintReportBuilder(exportStrategy, sprint);
        
        sprintReportBuilder.AddBacklogItemsList();
        sprintReportBuilder.Build();
        
        exportStrategy.Received().Export($"--------------------BacklogItems--------------------{Environment.NewLine}--------------------{Environment.NewLine}1{Environment.NewLine}Developer: Linus Torvalds{Environment.NewLine}State: Todo{Environment.NewLine}--------------------{Environment.NewLine}2{Environment.NewLine}Developer: Linus Torvalds{Environment.NewLine}State: Todo{Environment.NewLine}--------------------{Environment.NewLine}----------------------------------------------------{Environment.NewLine}{Environment.NewLine}");
    }
    
    //FR-20
    [Fact]
    public void SprintReportBuilderShouldReturnHeaderAndFooterWhenBuildIsCalledWithAddHeaderAndAddFooter()
    {
        var writer = Substitute.For<IWriter>();
        var exportStrategy = Substitute.For<IExportStrategy>();
        
        var developer = new TeamMember("Linus Torvalds", "linustorvalds@gmail.com");

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman", "henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"));

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
        
        exportStrategy.Received().Export($"Company header: SO&A 2{Environment.NewLine}Author name 1, Author name 2{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}-------------------{Environment.NewLine}{Environment.NewLine}");
    }

}