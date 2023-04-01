using Domain.Pipelines;

namespace Domain.Sprints;

public class SprintFactory
{
    public static ReleaseSprint NewReleaseSprint(Project project,TeamMember teamMember,  IPipeline? pipeline = null)
    {
        return new ReleaseSprint(project,teamMember, pipeline);
    }

    public static ReviewSprint NewReviewSprint(Project project,TeamMember teamMember, IPipeline? pipeline = null)
    {
        return new ReviewSprint(project,teamMember, pipeline);
    }
}