namespace Domain.Sprints.Export;

public interface IExportStrategy
{
    public void Export(string content);
}