namespace ConsoleApp36
{
    internal static class Arrow
    {
        private static int _minValue;
        private static int _maxValue;

        private static int _position;
        private static CashInfo _cashInfo;

        public static void SetMinAndMaxValues(int min, int max)
        {
            _minValue = min;
            _maxValue = max;
        } 

        public static int Select(CashInfo cashInfo)
        {
            _cashInfo = cashInfo;
            _position = _minValue;
            ConsoleKeyInfo key;

            do
            {
                
                Console.BufferHeight = 5000;
                Console.BufferWidth = 5000;
                Console.CursorTop = 0;
                Console.SetCursorPosition(0, _position);
                Console.WriteLine("->");

                key = Console.ReadKey();

                Console.SetCursorPosition(0, _position);
                Console.WriteLine("  ");

                if (key.Key == ConsoleKey.UpArrow && _position != _minValue)
                    --_position;
                else if (key.Key == ConsoleKey.DownArrow && _position != _maxValue)
                    ++_position;
                else if (key.Key == ConsoleKey.Escape)
                    return 1;
                else if (key.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    Console.WriteLine("Укажите название файла, который вы хотите создать:");
                    var path = Console.ReadLine();
                    FileAndFolderCommands.AddFile(Path.GetDirectoryName(_cashInfo.PathInfo.Values[0]) ?? Path.GetPathRoot(_cashInfo.PathInfo.Values[0]), path);
                    _cashInfo.PathInfo = Folder.ShowInformation(_cashInfo.PathInfo);
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    var filePath = _cashInfo.PathInfo.Values[_position];
                    var fileAttr = File.GetAttributes(filePath);
                    if (fileAttr.HasFlag(FileAttributes.Directory))
                    {
                        Console.WriteLine("Нельзя удалить директорию, попробуйте выбрать файл.");
                        continue;
                    }
                        
                    FileAndFolderCommands.DeleteFile(filePath);
                    _cashInfo.PathInfo = Folder.ShowInformation(_cashInfo.PathInfo);
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    Console.Clear();
                    Console.WriteLine("Укажите название директории, которую вы хотите создать:");
                    var dirName = Console.ReadLine();
                    FileAndFolderCommands.AddFolder(Path.GetDirectoryName(_cashInfo.PathInfo.Values[0]) ?? Path.GetPathRoot(_cashInfo.PathInfo.Values[0]), dirName);
                    _cashInfo.PathInfo = Folder.ShowInformation(_cashInfo.PathInfo);
                }
                else if (key.Key == ConsoleKey.D4)
                {
                    Console.Clear(); 
                    var dirPath = _cashInfo.PathInfo.Values[_position];
                    var fileAttr = File.GetAttributes(dirPath);
                    if (!fileAttr.HasFlag(FileAttributes.Directory))
                    {
                        Console.WriteLine("Нельзя удалить файл, попробуйте выбрать директорию.");
                        continue;
                    }
                    FileAndFolderCommands.DeleteFolder(dirPath);
                    _cashInfo.PathInfo = Folder.ShowInformation(_cashInfo.PathInfo);
                }
                else if (key.Key == ConsoleKey.D5)
                    return -1;

            } while (key.Key != ConsoleKey.Enter);

            return _position;
        }
    }
}




    
