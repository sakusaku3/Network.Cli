using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Status
{
	/// <summary>
	/// 状態の1要素クラス
	/// </summary>
    public class StateElement : IEquatable<StateElement>
    {
        public string Name { get; }

		public IReadOnlyList<string> Items { get; }

        public StateElement() : this(string.Empty) { } 

        public StateElement(string name) : 
            this(name, Enumerable.Empty<string>().ToArray()) { }

        public StateElement(
            string name,
            IReadOnlyList<string> items)
        {
            this.Name = name;
			this.Items = items;
        }

        public StateElement(StateElement other)
        {
            this.Name = other.Name;
			this.Items = other.Items.ToArray();
        }

        #region IEquatable implements

        public bool Equals(StateElement other)
        {
            if (other is null) return false;
            if (object.ReferenceEquals(this, other)) return true;
            return this.Name == other.Name &&
                this.Items.SequenceEqual(other.Items);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as StateElement);
        }

        public override int GetHashCode()
        {
            return this.Items.Aggregate(
                this.Name.GetHashCode(), 
                (x, next) => x ^ next.GetHashCode());
        }

        public override string ToString()
        {
            return $"{this.Name}:[{string.Join(",", this.Items)}]";
        }

        public static bool operator ==(StateElement x, StateElement y)
        {
            if (x is null) return false;
            return x.Equals(y);
        }

        public static bool operator !=(StateElement x, StateElement y)
        {
            return !(x == y);
        }

        #endregion

        public StateElement Clone()
        {
            return new StateElement(this);
        }
    }
}
