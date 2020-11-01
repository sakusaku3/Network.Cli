using System;
using System.Collections.Generic;
using System.Linq;

namespace Status
{
    public class Status : IEquatable<Status>
    {
        public string Name { get; set; }

        public IReadOnlyList<State> States { get; }

        public Status(
            IReadOnlyList<State> state)
        {
            this.States = state;
        }

        public Status(Status other)
        {
            this.States = other.States
                .Select(x => x.Clone())
                .ToArray();
        }

        #region IEquatable implements

        public bool Equals(Status other)
        {
            if (other is null) return false;
            if (object.ReferenceEquals(this, other)) return true;
            return this.States.SequenceEqual(other.States);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Status);
        }

        public override int GetHashCode()
        {
            return this.States.Aggregate(
                typeof(Status).GetHashCode(), 
                (x, next) => x ^ next.GetHashCode());
        }

        public static bool operator ==(Status x, Status y)
        {
            if (x is null) return false;
            return x.Equals(y);
        }

        public static bool operator !=(Status x, Status y)
        {
            return !(x == y);
        }

        #endregion

        public Status Clone()
        {
            return new Status(this);
        }
    }
}
