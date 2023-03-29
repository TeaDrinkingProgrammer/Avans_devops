using Domain.Sprints.SprintStates;

namespace Domain.Sprints;

public class ReviewSprint : Sprint
{
    public string SprintReview { get; set; }
    public ReviewState ReviewState { get;}
    public ReviewSprint(DateOnly date, Project project) : base(date, project)
    {
        ReviewState = new ReviewState(this);
    }

    public override void AddBacklogItem(BacklogItem backlogItem)
    {
        throw new NotImplementedException();
    }

    public override void RemoveBacklogItem(BacklogItem backlogItem)
    {
        throw new NotImplementedException();
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