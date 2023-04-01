namespace Domain;

public class Project
{
    public readonly string Name;

    public TeamMember ScrumMaster { get; set; }
    public TeamMember Tester { get; set; }
    public TeamMember ProductOwner { get; set; }

    public Project(string name, TeamMember scrumMaster, TeamMember tester, TeamMember productOwner)
    {
        Name = name;
        ScrumMaster = scrumMaster;
        Tester = tester;
        ProductOwner = productOwner;
    }
}