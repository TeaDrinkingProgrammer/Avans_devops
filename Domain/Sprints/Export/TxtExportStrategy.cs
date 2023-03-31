using System.Text;

namespace Domain.Sprints.Export;

public class TxtExportStrategy : IExportStrategy
{
    // Write a function Export that writes the content string to a file
    public void Export(string content)
    {
        const string path = @"C:\temp\file"; // path to file
        using var fs = File.Create(path);
        
        // writing data in string
        var info = new UTF8Encoding(true).GetBytes(content);
        fs.Write(info, 0, info.Length);

        // writing data in bytes already
        var data = new byte[] { 0x0 };
        fs.Write(data, 0, data.Length);
    }
}