using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp36
{
    public class CashInfo
    {
        public List<PathInfo> PreviousPathInfos { get; set; } = new(); 
        public PathInfo PathInfo { get; set; } = new();
    }
}
