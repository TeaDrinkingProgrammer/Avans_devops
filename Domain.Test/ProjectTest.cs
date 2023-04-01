namespace Domain.Test;

public class ProjectTest
{
    //FR-1
    [Fact]
    public void ProjectShouldHaveName()
    {
        //Act
        var project = new Project("SO&A 2", new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner"));
        //Assert
        Assert.Equal("SO&A 2", project.Name);
    }
}