namespace Domain.Sprints.Export;

//Pattern used: Strategy
public interface IExportStrategy
{
    public void Export(string content);
}