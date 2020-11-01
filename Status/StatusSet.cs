using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace Status
{
    public class StatusSet
    {
        public Status Status { get; }

        public Dictionary<StateChangeEvent, int> EventCostTable { get; } = 
            new Dictionary<StateChangeEvent, int>();

        public StatusSet(
            Status status,
            IEnumerable<StateChangeEvent> events)
        {
            this.Status = status;
            foreach (var e in events)
                this.EventCostTable[e] = 0;
        }
    }
}
