using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    public class Processes
    {
        public int ProcId { get; set; }
        public Process RefProc { get; set; }
        public string ProcName { get; set; }

        public override string ToString()
        {
            return $"{ProcName} - ID: {ProcId}";
        }
    }
}
