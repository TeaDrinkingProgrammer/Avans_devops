namespace Domain;

public class Project
{
    public readonly string Name;
    
    public TeamMember Tester { get; set; }
    public TeamMember ProductOwner { get; set; }

    public Project(string name, TeamMember tester, TeamMember productOwner)
    {
        Name = name;

        Tester = tester;
        ProductOwner = productOwner;
    }
}