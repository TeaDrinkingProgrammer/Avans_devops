//Write an nunit boilerplate class
using Domain.Exceptions;
using Domain.Pipelines;
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
        var sprint = SprintFactory.NewReviewSprint(project);
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
        var sprint = SprintFactory.NewReviewSprint(project);
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
        var pipeline = Substitute.For<IPipeline>();
        
        pipeline.Run().Returns(true);
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        sprint.Pipeline = pipeline;
        
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
        var sprint = SprintFactory.NewReleaseSprint(project);
        
        var scrumMasterNotificationService = new NotificationService(new EmailService(scrumMasterWriter), new SlackService(scrumMasterWriter));
        var productOwnerNotificationService = new NotificationService(new EmailService(productOwnerWriter), new SlackService(productOwnerWriter));

        project.ScrumMaster.Subscribe(scrumMasterNotificationService);
        project.ProductOwner.Subscribe(productOwnerNotificationService);
        
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
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        sprint.Pipeline = pipeline;
        
        var scrumMasterNotificationService = new NotificationService(new EmailService(scrumMasterWriter), new SlackService(scrumMasterWriter));
        var productOwnerNotificationService = new NotificationService(new EmailService(productOwnerWriter), new SlackService(productOwnerWriter));
        
        project.ScrumMaster.Subscribe(scrumMasterNotificationService);
        project.ProductOwner.Subscribe(productOwnerNotificationService);
        
        sprint.ToNextState();
        sprint.ToNextState();
        
        //Act
        sprint.Release();
        
        //Assert
        scrumMasterWriter.Received().WriteLine("To: Jan de Scrumman <jandescrumman@gmail.com>: Sprint has been released");
        productOwnerWriter.Received().WriteLine("To: Jan de Productowner <jandeproductowner@gmail.com>: Sprint has been released");
    }

    [Fact]
    public void ShouldAddBacklogItemToReviewSprintInPlannedState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        var item = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        //Act
        sprint.AddBacklogItem(item);

        //Assert
        Assert.Contains(item, sprint.BacklogItems);
    }
    
    [Fact]
    public void ShouldAddBacklogItemToReleaseSprintInPlannedState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        var item = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        //Act
        sprint.AddBacklogItem(item);

        //Assert
        Assert.Contains(item, sprint.BacklogItems);
    }
    
    [Fact]
    public void ShouldAddBacklogItemToReviewSprintInInProgressState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        var item = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        
        sprint.ToNextState();
        //Act
        sprint.AddBacklogItem(item);

        //Assert
        Assert.Contains(item, sprint.BacklogItems);
    }
    
    [Fact]
    public void ShouldAddBacklogItemToReleaseSprintInProgressState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com")); 
        
        var sprint = SprintFactory.NewReleaseSprint(project);
        var item = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        
        sprint.ToNextState();
        //Act
        sprint.AddBacklogItem(item);

        //Assert
        Assert.Contains(item, sprint.BacklogItems);
    }

    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReviewSprintWhenAddingBacklogItemsInFinishedState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        sprint.ToNextState();
        sprint.ToNextState();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.AddBacklogItem(new BacklogItem("", Substitute.For<IWriter>(), new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"))));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReleaseSprintWhenAddingBacklogItemsInFinishedState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        sprint.ToNextState();
        sprint.ToNextState();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.AddBacklogItem(new BacklogItem("", Substitute.For<IWriter>(), new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"))));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReviewSprintWhenAddingBacklogItemsInReviewState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.UploadReview("test review");
        sprint.Review();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.AddBacklogItem(new BacklogItem("", Substitute.For<IWriter>(), new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"))));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReleaseSprintWhenAddingBacklogItemsInReleasedState()
    {
        //Arrange
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        sprint.Pipeline = pipeline;
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.Release();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.AddBacklogItem(new BacklogItem("", Substitute.For<IWriter>(), new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"))));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReviewSprintWhenAddingBacklogItemsCancelledState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.CancelSprint();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.AddBacklogItem(new BacklogItem("", Substitute.For<IWriter>(), new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"))));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReleaseSprintWhenAddingBacklogItemsInCancelledState() {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.CancelSprint();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.AddBacklogItem(new BacklogItem("", Substitute.For<IWriter>(), new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"))));
    }
    
    [Fact]
    public void ShouldRemoveBacklogItemFromReviewSprintInPlannedState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        var item = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        //Act
        sprint.AddBacklogItem(item);
        sprint.RemoveBacklogItem(item);

        //Assert
        Assert.DoesNotContain(item, sprint.BacklogItems);
    }
    
    [Fact]
    public void ShouldRemoveBacklogItemFromReleaseSprintInPlannedState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        var item = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        //Act
        sprint.AddBacklogItem(item);
        sprint.RemoveBacklogItem(item);

        //Assert
        Assert.DoesNotContain(item, sprint.BacklogItems);
    }
    
    [Fact]
    public void ShouldARemoveBacklogItemToReviewSprintInInProgressState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        var item = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        
        sprint.ToNextState();
        //Act
        sprint.AddBacklogItem(item);
        sprint.RemoveBacklogItem(item);

        //Assert
        Assert.DoesNotContain(item, sprint.BacklogItems);
    }
    
    [Fact]
    public void ShouldRemoveBacklogItemFromReleaseSprintInProgressState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        var item = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        
        sprint.ToNextState();
        
        //Act
        sprint.AddBacklogItem(item);
        sprint.RemoveBacklogItem(item);
        
        //Assert
        Assert.DoesNotContain(item, sprint.BacklogItems);
    }

    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReviewSprintWhenRemovingBacklogItemsInFinishedState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        
        var backlogItem = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        sprint.AddBacklogItem(backlogItem);

        sprint.ToNextState();
        sprint.ToNextState();
        //Act
        
        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.RemoveBacklogItem(backlogItem));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReleaseSprintWhenRemovingBacklogItemsInFinishedState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        sprint.AddBacklogItem(backlogItem);
        
        sprint.ToNextState();
        sprint.ToNextState();
        //Act
        
        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.RemoveBacklogItem(backlogItem));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReviewSprintWhenRemovingBacklogItemsInReviewState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        
        var backlogItem = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        sprint.AddBacklogItem(backlogItem);
        
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.UploadReview("test review");
        sprint.Review();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.RemoveBacklogItem(backlogItem));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReleaseSprintWhenRemovingBacklogItemsInReleasedState()
    {
        //Arrange
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        sprint.AddBacklogItem(backlogItem);
        
        sprint.Pipeline = pipeline;
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.Release();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.RemoveBacklogItem(backlogItem));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReviewSprintWhenRemovingBacklogItemsCancelledState()
    {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        
        var backlogItem = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        sprint.AddBacklogItem(backlogItem);
        
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.CancelSprint();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.RemoveBacklogItem(backlogItem));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReleaseSprintWhenRemovingBacklogItemsInCancelledState() {
        //Arrange
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        
        var backlogItem = new BacklogItem("", Substitute.For<IWriter>(),
            new TeamMember("Linus Torvalds", "linustorvalds@gmail.com"));
        sprint.AddBacklogItem(backlogItem);
        
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.CancelSprint();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.RemoveBacklogItem(backlogItem));
    }

    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReviewSprintWhenUploadingReviewInPlannedState()
    {
        //Arrange
        var project = new Project("SO&A 2", new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"),
            new TeamMember("Henk de Testerman", "henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.UploadReview("test review"));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReviewSprintWhenUploadingReviewInInProgressState()
    {
        //Arrange
        var project = new Project("SO&A 2", new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"),
            new TeamMember("Henk de Testerman", "henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        sprint.ToNextState();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.UploadReview("test review"));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReviewSprintWhenUploadingReviewInReviewState()
    {
        //Arrange
        var project = new Project("SO&A 2", new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"),
            new TeamMember("Henk de Testerman", "henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.UploadReview("test review");
        sprint.Review();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.UploadReview("test review"));
    }
    
    [Fact]
    public void SprintShouldThrowInvalidOperationExceptionOnReviewSprintWhenUploadingReviewInCancelledState()
    {
        //Arrange
        var project = new Project("SO&A 2", new TeamMember("Jan de Scrumman", "jandescrumman@gmail.com"),
            new TeamMember("Henk de Testerman", "henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.CancelSprint();
        //Act

        //Assert
        Assert.Throws<InvalidOperationException>(
            () => sprint.UploadReview("test review"));
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationExceptionOnReviewSprintWhenRunningPipelineInPlannedState()
    {
        //Arrange
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        sprint.Pipeline = pipeline;

        //Assert
        Assert.Throws<InvalidOperationException>(() => sprint.RunPipeline());
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationExceptionOnReleaseSprintWhenRunningPipelineInPlannedState()
    {
        //Arrange
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        sprint.Pipeline = pipeline;

        //Assert
        Assert.Throws<InvalidOperationException>(() => sprint.RunPipeline());
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationExceptionOnReviewSprintWhenRunningPipelineInInProgressState()
    {
        //Arrange
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        sprint.Pipeline = pipeline;
        sprint.ToNextState();

        //Assert
        Assert.Throws<InvalidOperationException>(() => sprint.RunPipeline());
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationExceptionOnReleaseSprintWhenRunningPipelineInInProgressState()
    {
        //Arrange
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        sprint.Pipeline = pipeline;
        sprint.ToNextState();

        //Assert
        Assert.Throws<InvalidOperationException>(() => sprint.RunPipeline());
    }
    
    [Fact]
    public void ShouldThrowIllegalStateAdvanceExceptionOnReviewSprintWhenRunningPipelineWithNullReferenceInInFinishedState()
    {
        //Arrange

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);

        sprint.ToNextState();
        sprint.ToNextState();

        //Assert
        Assert.Throws<IllegalStateAdvanceException>(() => sprint.RunPipeline());
    }
    
    [Fact]
    public void ShouldThrowIllegalStateAdvanceExceptionOnReleaseSprintWhenRunningPipelineWithNullReferenceInInFinishedState()
    {
        //Arrange

        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        
        sprint.ToNextState();
        sprint.ToNextState();

        //Assert
        Assert.Throws<IllegalStateAdvanceException>(() => sprint.RunPipeline());
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationExceptionOnReviewSprintWhenRunningPipelineInReviewState()
    {
        //Arrange
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        sprint.Pipeline = pipeline;
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.UploadReview("test review");
        sprint.Review();

        //Assert
        Assert.Throws<InvalidOperationException>(() => sprint.RunPipeline());
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationExceptionOnReleaseSprintWhenRunningPipelineInReleasedState()
    {
        //Arrange
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        sprint.Pipeline = pipeline;
        sprint.ToNextState();
        sprint.ToNextState();
        sprint.Release();

        //Assert
        Assert.Throws<InvalidOperationException>(() => sprint.RunPipeline());
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationExceptionOnReviewSprintWhenRunningPipelineInCancelledState()
    {
        //Arrange
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReviewSprint(project);
        sprint.Pipeline = pipeline;
        sprint.ToNextState();

        //Assert
        Assert.Throws<InvalidOperationException>(() => sprint.RunPipeline());
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationExceptionOnReleaseSprintWhenRunningPipelineInCancelledState()
    {
        //Arrange
        var pipeline = Substitute.For<IPipeline>();
        pipeline.Run().Returns(true);
        
        var project = new Project("SO&A 2",new TeamMember("Jan de Scrumman","jandescrumman@gmail.com"), new TeamMember("Henk de Testerman","henkdetesterman@gmail.com"),
            new TeamMember("Jan de Productowner", "jandeproductowner@gmail.com"));
        var sprint = SprintFactory.NewReleaseSprint(project);
        sprint.Pipeline = pipeline;
        sprint.CancelSprint();

        //Assert
        Assert.Throws<InvalidOperationException>(() => sprint.RunPipeline());
    }
}