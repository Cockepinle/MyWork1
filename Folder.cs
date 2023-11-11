using System.IO;
using System.Net.Http.Headers;
using System.Xml.Schema;

namespace ConsoleApp36;


public static class Folder
{
 
    public static PathInfo ShowFolderAndFiles(string path)
    {
        Console.Clear();
        string[] dirs = Array.Empty<string>();
        string[] files= Array.Empty<string>(); 
        List<string> lines = new List<string>();
        try
        {
            dirs = Directory.GetDirectories(path);
            files = Directory.GetFiles(path);

            foreach (var dir in dirs)
            {
                var directoryInfo = new DirectoryInfo(dir);
                var directoryInfoResult = $"\t {directoryInfo.Name}, последнее изменение: {directoryInfo.LastWriteTime} - путь: {directoryInfo.FullName}";
                lines.Add(directoryInfoResult);
                Console.WriteLine(directoryInfoResult);
            }
          

            foreach (var file in files)
            {
                if (dirs.Contains(file))continue;

                var fileInfo = new FileInfo(file);
                var fileInfoResult = $"\t {fileInfo.Name}, {fileInfo.Length} байтов, последнее изменение: {fileInfo.LastWriteTime} - путь: {fileInfo.FullName}";
                lines.Add(fileInfoResult);
                Console.WriteLine(fileInfoResult);
            }
        }
        catch (Exception e)
        {
            PrintException(e.Message);
            Thread.Sleep(2000);
            Console.Clear();
            throw;
        }

        var pathInfo = new PathInfo
        {
            Min = 0,
            Max = dirs.Length + files.Length - 1,
            ValuesInfo = lines.Distinct().ToList(),
            Values = new List<string>()
        };
        pathInfo.Values.AddRange(dirs);
        pathInfo.Values.AddRange(files);
        pathInfo.Values.Distinct();

        WriteCommands();
        return pathInfo;
    }

    public static PathInfo ShowDrives()
    {
        Console.Clear();
        var dis = DriveInfo.GetDrives();
        var consoleRows = 0;
        var drivesInfo = new List<string>();  
        try
        {
            Console.WriteLine("\t Выберите диск из имеющихся на ваших устройствах:");
            consoleRows++;
            Console.WriteLine("\t ------------------------------------------------");
            consoleRows++;

            foreach (var di in dis)
            {
                var diInfo = $"\t Диск {di.Name} имеется в системе и его тип {di.DriveType}.";
                Console.WriteLine(diInfo);
                drivesInfo.Add(diInfo); 
            }
        }
        catch (IOException e)
        {
            PrintException(e.Message);
        }
        catch (Exception e)
        {
            PrintException(e.Message);
        }

        var pathInfo = new PathInfo()
        {
            Min = consoleRows,
            Max = dis.Length + consoleRows,
            Values = dis.Select(item => item.Name).ToList(),
            ValuesInfo = drivesInfo
        };

        WriteCommands();
        return pathInfo;
    }

    public static void WriteCommands()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine("1 - добавить файл \t 2 - удалить файл \t 3 - добавить директорию \t 4 - удалить директорию \t 5 - завершить");
    }

  
    public static PathInfo ShowInformation(PathInfo info)
    {
        var directory = Path.GetDirectoryName(Path.GetDirectoryName(info.Values[0])); // получаем имя директории
        PathInfo pathInfo;

        
        if (!string.IsNullOrEmpty(directory))
        {
            try
            {
                pathInfo = Folder.ShowFolderAndFiles(directory);
                Arrow.SetMinAndMaxValues(pathInfo.Min, pathInfo.Max);
            }
            catch (Exception) 
            {
                info.ValuesInfo.ForEach(item => Console.WriteLine(item));
                WriteCommands();
                return info;
            }
        }
        else
        {
            pathInfo = Folder.ShowDrives();
            Arrow.SetMinAndMaxValues(pathInfo.Min, pathInfo.Max - 1);
        }

        return pathInfo;
    }
    private static void PrintException(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
    }
}