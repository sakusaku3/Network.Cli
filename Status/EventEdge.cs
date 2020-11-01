using System;
using System.Collections.Generic;
using System.Text;

namespace Status
{
    public class EventEdge : Network.IEdge<Status>
    {
        public Status Source { get; }

        public Status Target { get; }

        public int Cost { get; }

        public StateChangeEvent Event { get; }

        public EventEdge(
            Status source, 
            Status target,
            int cost,
            StateChangeEvent stateChangeEvent)
        {
            this.Source = source;
            this.Target = target;
            this.Cost = cost;
            this.Event = stateChangeEvent;
        }

        public override string ToString()
        {
            return $"{this.Source.Name}->{this.Target.Name}({this.Event.Name},{this.Cost})";
        }
    }
}
