using Domain.Exceptions;
using Domain.Sprints;
using NSubstitute;

namespace Domain.Test;

public class BacklogTest
{
    //FR-7.2
    [Fact]
    public void BacklogItemsShouldHaveTheTodoStateWhenTheyAreCreated()
    {
        var writer = Substitute.For<IWriter>();

        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        var sprint = SprintFactory.NewReleaseSprint(project, new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"));
        
        var backlogItem = new BacklogItem("1", writer,
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));

        sprint.AddBacklogItem(backlogItem);

        Assert.Equal(1, sprint.BacklogItems.Count);
        Assert.Equal(backlogItem.TodoBacklogState, backlogItem.State);
    }
}