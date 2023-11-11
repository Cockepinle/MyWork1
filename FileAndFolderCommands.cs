using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp36;

public static class FileAndFolderCommands
{
  
    public static void AddFile(string rootPath, string name)
    {
        var newFilePath = Path.Combine(rootPath, name);
        using var createdFile = File.Create(newFilePath);
        Console.WriteLine($"���� {newFilePath} ������!");
    }


    public static void DeleteFile(string path)
    {
        var currentTryCount = 0;
        var maxTryCount = 5;

        while (maxTryCount > currentTryCount)
        {
            try
            {
                File.Delete(path);
                Console.WriteLine($"���� {path} ������!");
                return;
            }
            catch (Exception)
            {
                currentTryCount++;
                Thread.Sleep(1000);
            }
        }
       
    }


    public static void AddFolder(string rootPath, string name)
    {
        var newDirPath = Path.Combine(rootPath, name);
        var dirInfo = Directory.CreateDirectory(newDirPath);
        dirInfo.Refresh();
      
        Console.WriteLine($"���������� {newDirPath} �������!");
    }


    public static void DeleteFolder(string path)
    {
        var currentTryCount = 0;
        var maxTryCount = 5;

        while (maxTryCount > currentTryCount)
        {
            try
            {
                Directory.Delete(path, true);
                Console.WriteLine($"���������� {path} �������!");
                return;
            }
            catch (Exception)
            {
                currentTryCount++;
                Thread.Sleep(1000);
            }
        }
    }
}