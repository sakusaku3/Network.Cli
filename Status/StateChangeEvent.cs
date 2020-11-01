using System.Collections.ObjectModel;
using System.Linq;

namespace Status
{
    public class StateChangeEvent
    {
        public string Name { get; set; }

        public ObservableCollection<StateChange> StateChanges { get; } = 
            new ObservableCollection<StateChange>();

        public StateChangeEvent(string name)
        {
            this.Name = name;
        }

        public Status GetNext(Status current)
        {
            var states = current.States.ToDictionary(x => x);

            foreach (var sc in this.StateChanges)
            {
                if (states.ContainsKey(sc.CurrentState))
                {
                    states[sc.CurrentState] = sc.NextState.Clone();
                }
            }

            return new Status(states.Values.ToArray());
        }

        public EventEdge GetEdge(Status current, int cost)
        {
            var next = this.GetNext(current);
            return new EventEdge(current, next, cost, this);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
