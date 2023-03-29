namespace Domain;

public class Project
{
    private string _title;

    public TeamMember ScrumMaster { get; set; }
    public TeamMember Tester { get; set; }
    public TeamMember ProductOwner { get; set; }

    public Project(string title, TeamMember scrumMaster, TeamMember tester, TeamMember productOwner)
    {
        _title = title;
        ScrumMaster = scrumMaster;
        Tester = tester;
        ProductOwner = productOwner;
    }
}