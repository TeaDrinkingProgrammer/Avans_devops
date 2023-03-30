//Write an nunit boilerplate class
using Domain.Exceptions;
using Domain.Notifier;
using Domain.Sprints;
using Domain.Sprints.SprintStates;
using NSubstitute;

namespace Domain.Test;

public class SprintStateTest
{
    [Fact]
    public void SprintShouldHaveReviewStateAfterCallingReviewSprintOnFinishedStateOnAReviewSprint()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
                    new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com")); 
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReviewSprint(project);
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.UploadReview("Sprint review");
        //Act
        sprint.Review();
        //Assert
        Assert.IsType<ReviewState>(sprint.State);
    }
    [Fact]
    public void SprintShouldThrowIllegalStateAdvanceExceptionAfterCallingReviewOnFinishedStateWithoutSprintReview()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com")); 
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReviewSprint(project);
        sprint.ToNextState();
        sprint.ToNextState();
        //Act
        
        //Assert
        Assert.Throws<IllegalStateAdvanceException>(
            () => sprint.Review());
    }
    
    [Fact]
    public void SprintShouldHaveReleaseStateAfterCallingReleaseOnFinishedStateOnAReleaseSprint()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com")); 
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        sprint.ToNextState();
        sprint.ToNextState();
        //Act
        sprint.Release();
        //Assert
        Assert.IsType<ReleasedState>(sprint.State);
    }
    [Fact]
    public void ScrumMasterAndOwnerShouldGetNotificationWhenSprintHasBeenCancelled()
    {
        //Arrange
        var scrumMasterWriter = Substitute.For<IWriter>();
        var productOwnerWriter = Substitute.For<IWriter>();
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com")); 
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var scrumMasterNotificationService = new NotificationService(new EmailService(scrumMasterWriter), new SlackService(scrumMasterWriter));
        var productOwnerNotificationService = new NotificationService(new EmailService(productOwnerWriter), new SlackService(productOwnerWriter));

        project.SubscribeToScrumMaster(scrumMasterNotificationService);
        project.SubscribeToProductOwner(productOwnerNotificationService);
        //Act
        sprint.CancelSprint();
        
        //Assert
        scrumMasterWriter.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Sprint has been cancelled");
        productOwnerWriter.Received().WriteLine("To: Jan de Productowner <jandeproductowner@gmail.com>: Sprint has been cancelled");
    }
    [Fact]
    public void ScrumMasterAndOwnerShouldGetNotificationWhenSprintHasBeenReleased()
    {
        //Arrange
        var scrumMasterWriter = Substitute.For<IWriter>();
        var productOwnerWriter = Substitute.For<IWriter>();
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com")); 
        var sprintFactory = new SprintFactory();
        var sprint = sprintFactory.NewReleaseSprint(project);
        
        var scrumMasterNotificationService = new NotificationService(new EmailService(scrumMasterWriter), new SlackService(scrumMasterWriter));
        var productOwnerNotificationService = new NotificationService(new EmailService(productOwnerWriter), new SlackService(productOwnerWriter));
        
        project.SubscribeToScrumMaster(scrumMasterNotificationService);
        project.SubscribeToProductOwner(productOwnerNotificationService);
        
        sprint.ToNextState();
        sprint.ToNextState();
        
        //Act
        sprint.Release();
        
        //Assert
        scrumMasterWriter.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Sprint has been released");
        productOwnerWriter.Received().WriteLine("To: Jan de Productowner <jandeproductowner@gmail.com>: Sprint has been released");
    }
}