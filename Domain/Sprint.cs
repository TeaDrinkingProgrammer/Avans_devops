namespace Domain;

public class Sprint
{
    public TeamMember ScrumMaster { get; set; }
    public TeamMember Tester { get; set; }
    public TeamMember ProductOwner { get; set; }

    public Sprint(TeamMember scrumMaster, TeamMember tester, TeamMember productOwner)
    {
        ScrumMaster = scrumMaster;
        Tester = tester;
        ProductOwner = productOwner;
    }
}