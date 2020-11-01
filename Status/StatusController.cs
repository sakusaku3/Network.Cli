using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Status
{
    public class StatusController
    {
        public ObservableCollection<Element> Elements { get; } = 
            new ObservableCollection<Element>();

        public ObservableCollection<StateChangeEvent> Events { get; } = 
            new ObservableCollection<StateChangeEvent>();

        public ObservableCollection<StatusSet> StatusSets { get; } = 
            new ObservableCollection<StatusSet>();

		public StatusController() { }

		public StatusController(
			IReadOnlyList<Element> elements,
			IReadOnlyList<StateChangeEvent> events) 
		{
			foreach (var e in elements)
				this.Elements.Add(e);

			foreach (var e in events)
				this.Events.Add(e);

			this.SetStatus();
		}

        public void SetStatus()
        {
			var builders = this.recursiveGetBuilders(this.Elements);
            foreach (var (b, i) in builders.Select((x, i) => (x, i)))
                this.StatusSets.Add(new StatusSet(b.ToModel(i), this.Events));
        }

        private IEnumerable<StatusBuilder> recursiveGetBuilders(
			IReadOnlyList<Element> elements)
        {
            var rest = elements.Skip(1).ToArray();

            if (rest.Any())
            {
                foreach (var state in elements[0].Items.Select(x => new State(elements[0], x)))
                    foreach (var builder in this.recursiveGetBuilders(rest))
                        yield return builder.Prepended(state);
            }
            else
            {
                foreach (var state in elements[0].Items.Select(x => new State(elements[0], x)))
                    yield return new StatusBuilder(state);
            }
        }

        private class StatusBuilder
		{
			/// <summary>
			/// 状態リスト
			/// </summary>
			private readonly List<State> states = new List<State>();

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="adding">先頭の状態</param>
			public StatusBuilder(State adding)
			{
				this.states.Add(adding);
			}

			/// <summary>
			/// 状態を先頭に追加する
			/// </summary>
			/// <param name="adding">追加する状態</param>
			public void Prepend(State adding)
			{
				this.states.Insert(0, adding);
			}

			/// <summary>
			/// 状態を先頭に追加して自分を返す
			/// </summary>
			/// <param name="adding">追加する状態</param>
			/// <returns></returns>
			public StatusBuilder Prepended(State adding)
			{
				this.Prepend(adding);
				return this;
			}

			/// <summary>
			/// 状態を後尾に追加する
			/// </summary>
			/// <param name="adding">追加する状態</param>
			public void Append(State adding)
			{
				this.states.Add(adding);
			}

			/// <summary>
			/// 条件候補モデルへ変換する
			/// </summary>
			/// <param name="number">条件候補モデルへ設定する番号</param>
			/// <returns>条件候補モデル</returns>
			public Status ToModel(int number)
			{
				return new Status(this.states) { Name = number.ToString() };
			}
		}
    }
}
