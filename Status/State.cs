using System;

namespace Status
{
    public class State : IEquatable<State>
    {
        public StateElement Key { get; }

        public string Value { get; set; }

        public State(StateElement key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public State(State other)
        {
            this.Key = other.Key.Clone();
            this.Value = other.Value;
        }

        #region IEquatable implements

        public bool Equals(State other)
        {
            if (other is null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return this.Key.Equals(other.Key) &&
                this.Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as State);
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode() ^
                this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Key.Name}:{this.Value}";
        }

        public static bool operator ==(State x, State y)
        {
            if (x is null) return false;
            return x.Equals(y);
        }

        public static bool operator !=(State x, State y)
        {
            return !(x == y);
        }

        #endregion

        public State Clone()
        {
            return new State(this);
        }
    }
}
