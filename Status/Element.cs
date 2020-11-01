using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Status
{
    public class Element : IEquatable<Element>
    {
        public string Name { get; }

        public ObservableCollection<string> Items { get; } = 
            new ObservableCollection<string>();

        public Element() : this(string.Empty) { } 

        public Element(string name) : 
            this(name, Enumerable.Empty<string>()) { }

        public Element(
            string name,
            IEnumerable<string> items)
        {
            this.Name = name;
            foreach (var item in items)
                this.Items.Add(item);
        }

        public Element(Element other)
        {
            this.Name = other.Name;
            foreach (var item in other.Items)
                this.Items.Add(item);
        }

        #region IEquatable implements

        public bool Equals(Element other)
        {
            if (other is null) return false;
            if (object.ReferenceEquals(this, other)) return true;
            return this.Name == other.Name &&
                this.Items.SequenceEqual(other.Items);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Element);
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

        public static bool operator ==(Element x, Element y)
        {
            if (x is null) return false;
            return x.Equals(y);
        }

        public static bool operator !=(Element x, Element y)
        {
            return !(x == y);
        }

        #endregion

        public Element Clone()
        {
            return new Element(this);
        }
    }
}
