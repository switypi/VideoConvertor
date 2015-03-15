using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoConvertor
{
    public enum ProcessResultState
    {
        Initialized = 1,
        Finished = 2
    }

    public class ProcessResultArgs : EventArgs
    {
        public ProcessResultState MediaState { get; set; }
        public string MediaName { get; set; }
        public TimeSpan TotalDuration { get; set; }
    }
}
