namespace Domain.Sprints;

public class SprintFactory
{
    public static ReleaseSprint NewReleaseSprint(Project project)
    {
        return new ReleaseSprint(DateOnly.FromDateTime(DateTime.Now), project);
    }

    public static ReviewSprint NewReviewSprint(Project project)
    {
        return new ReviewSprint(DateOnly.FromDateTime(DateTime.Now), project);
    }
}