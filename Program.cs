using System.Diagnostics;

namespace ConsoleApp36
{
    public static class Program
    {
        static void Main()
        {
            Console.Clear();
            var cashInfoAboutFiles = new CashInfo();
            cashInfoAboutFiles.PathInfo = Folder.ShowDrives();
            Arrow.SetMinAndMaxValues(cashInfoAboutFiles.PathInfo.Min, cashInfoAboutFiles.PathInfo.Max - 1);

            while (true)
            {
                var position = Arrow.Select(cashInfoAboutFiles);
              
                if (position == -1)
                {
                    Console.Clear();
                    Console.WriteLine("Работа программы завершена");
                    return;
                }
                   

                if (position == 1)
                {
                    cashInfoAboutFiles.PathInfo = Folder.ShowInformation(cashInfoAboutFiles.PathInfo);
                    cashInfoAboutFiles.PreviousPathInfos.Remove(cashInfoAboutFiles.PreviousPathInfos.Last());
                    continue;
                }


                string value = cashInfoAboutFiles.PathInfo.Values[position - cashInfoAboutFiles.PathInfo.Min];
                var fileAttr = File.GetAttributes(value);
                if (!fileAttr.HasFlag(FileAttributes.Directory))
                {
                    try
                    {
                        var process = new Process();
                        process.StartInfo = new ProcessStartInfo(value)
                        {
                            UseShellExecute = true
                        };
                        process.Start();
                        continue;
                    }
                    catch (Exception)
                    {

                    }
                    
                }

                try
                {
                    cashInfoAboutFiles.PreviousPathInfos.Add(cashInfoAboutFiles.PathInfo);
                    cashInfoAboutFiles.PathInfo = Folder.ShowFolderAndFiles(value);
                    Arrow.SetMinAndMaxValues(cashInfoAboutFiles.PathInfo.Min, cashInfoAboutFiles.PathInfo.Max);
                }
                catch (Exception)
                {
                    cashInfoAboutFiles.PreviousPathInfos.Remove(cashInfoAboutFiles.PathInfo);
                    cashInfoAboutFiles.PathInfo.ValuesInfo.ForEach(x => Console.WriteLine(x));
                    Folder.WriteCommands();
                }
            }
        }
        
    }
}