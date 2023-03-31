using System.Text;

namespace Domain.Sprints.Export;

public class SprintReportBuilder
{
    private string[] Header { get; set; }= Array.Empty<string>();
    private List<string> Body { get; set; } = new (Array.Empty<string>());
    private Sprint Sprint { get; set; }
    private string[] Footer { get; set; } = Array.Empty<string>();
    private IExportStrategy ExportStrategy { get; set; }
    
    public SprintReportBuilder(IExportStrategy exportStrategy, Sprint sprint)
    {
        ExportStrategy = exportStrategy;
        Sprint = sprint;
    }
    
    public void AddHeader(string[] header)
    {
        Header = header;
    }
    
    public void AddFooter(string[] footer)
    {
        Footer = footer;
    }
    public void AddBacklogItemsList()
    {
        var visitor = new BacklogItemsListVisitor(Sprint);
        Sprint.Accept(visitor);
        
        Body.Add("--------------------BacklogItems--------------------");
        Body.AddRange(visitor.Export());
        Body.Add("----------------------------------------------------");
    }
    public void AddTeamConsistency()
    {
        var visitor = new TeamConsistencyVisitor(Sprint);
        Sprint.Accept(visitor);
        
        Body.Add("");
        Body.AddRange(visitor.Export());
        Body.Add("");
    }
    

    public void Build()
    {
        var result = new StringBuilder();
        if (Header.Length != 0) result.Append(StringArrayToParagraphs(Header)).AppendLine();
        result.Append(StringArrayToParagraphs(Body)).AppendLine();
        if (Footer.Length != 0) result.Append(StringArrayToParagraphs(Footer)).AppendLine();
        ExportStrategy.Export(result.ToString());
    }
    private static StringBuilder StringArrayToParagraphs(IEnumerable<string> paragraphs)
    {
        var result = new StringBuilder();
        foreach (var paragraph in paragraphs)
        {
            result.Append(paragraph).AppendLine();
        }

        return result;
    }
}