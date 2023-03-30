namespace Domain.Sprints;

public class SprintFactory
{
    public ReleaseSprint NewReleaseSprint(Project project)
    {
        return new ReleaseSprint(DateOnly.FromDateTime(DateTime.Now), project);
    }

    public ReviewSprint NewReviewSprint(Project project)
    {
        return new ReviewSprint(DateOnly.FromDateTime(DateTime.Now), project);
    }
}