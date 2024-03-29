using Domain.Pipelines;
using Domain.Sprints.SprintStates;

namespace Domain.Sprints;

public class ReviewSprint : Sprint
{
    public string? SprintReview { get; set; }
    public ReviewState ReviewState { get;}
    public ReviewSprint(Project project, TeamMember scrumMaster, IPipeline? pipeline) : base(project, scrumMaster, pipeline)
    {
        ReviewState = new ReviewState(this);
    }

    public override void AddBacklogItem(BacklogItem backlogItem)
    {
        State.AddBacklogItem(backlogItem);
    }

    public override void RemoveBacklogItem(BacklogItem backlogItem)
    {
        State.RemoveBacklogItem(backlogItem);
    }

    public override void ToNextState()
    {
        State.ToNextState();
    }

    public override void CancelSprint()
    {
        State.CancelSprint();
    }
    public void Review()
    {
        State.ReviewSprint();
    }
    public void UploadReview(string review)
    {
        State.UploadReview(review);
    }
}