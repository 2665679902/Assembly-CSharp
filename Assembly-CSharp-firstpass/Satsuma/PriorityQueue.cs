using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000275 RID: 629
	public sealed class PriorityQueue<TElement, TPriority> : IPriorityQueue<TElement, TPriority>, IReadOnlyPriorityQueue<TElement, TPriority>, IClearable where TPriority : IComparable<TPriority>
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x0004A631 File Offset: 0x00048831
		public void Clear()
		{
			this.payloads.Clear();
			this.priorities.Clear();
			this.positions.Clear();
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0004A654 File Offset: 0x00048854
		public int Count
		{
			get
			{
				return this.payloads.Count;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0004A661 File Offset: 0x00048861
		public IEnumerable<KeyValuePair<TElement, TPriority>> Items
		{
			get
			{
				int i = 0;
				int j = this.Count;
				while (i < j)
				{
					yield return new KeyValuePair<TElement, TPriority>(this.payloads[i], this.priorities[i]);
					int num = i;
					i = num + 1;
				}
				yield break;
			}
		}

		// Token: 0x17000271 RID: 625
		public TPriority this[TElement element]
		{
			get
			{
				return this.priorities[this.positions[element]];
			}
			set
			{
				int num;
				if (this.positions.TryGetValue(element, out num))
				{
					TPriority tpriority = this.priorities[num];
					this.priorities[num] = value;
					int num2 = value.CompareTo(tpriority);
					if (num2 < 0)
					{
						this.MoveUp(num);
						return;
					}
					if (num2 > 0)
					{
						this.MoveDown(num);
						return;
					}
				}
				else
				{
					this.payloads.Add(element);
					this.priorities.Add(value);
					num = this.Count - 1;
					this.positions[element] = num;
					this.MoveUp(num);
				}
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004A71F File Offset: 0x0004891F
		public bool Contains(TElement element)
		{
			return this.positions.ContainsKey(element);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0004A730 File Offset: 0x00048930
		public bool TryGetPriority(TElement element, out TPriority priority)
		{
			int num;
			if (!this.positions.TryGetValue(element, out num))
			{
				priority = default(TPriority);
				return false;
			}
			priority = this.priorities[num];
			return true;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0004A76C File Offset: 0x0004896C
		private void RemoveAt(int pos)
		{
			int count = this.Count;
			TElement telement = this.payloads[pos];
			TPriority tpriority = this.priorities[pos];
			this.positions.Remove(telement);
			bool flag = count <= 1;
			if (!flag && pos != count - 1)
			{
				this.payloads[pos] = this.payloads[count - 1];
				this.priorities[pos] = this.priorities[count - 1];
				this.positions[this.payloads[pos]] = pos;
			}
			this.payloads.RemoveAt(count - 1);
			this.priorities.RemoveAt(count - 1);
			if (!flag && pos != count - 1)
			{
				TPriority tpriority2 = this.priorities[pos];
				int num = tpriority2.CompareTo(tpriority);
				if (num > 0)
				{
					this.MoveDown(pos);
					return;
				}
				if (num < 0)
				{
					this.MoveUp(pos);
				}
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0004A85C File Offset: 0x00048A5C
		public bool Remove(TElement element)
		{
			int num;
			bool flag = this.positions.TryGetValue(element, out num);
			if (flag)
			{
				this.RemoveAt(num);
			}
			return flag;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0004A881 File Offset: 0x00048A81
		public TElement Peek()
		{
			return this.payloads[0];
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0004A88F File Offset: 0x00048A8F
		public TElement Peek(out TPriority priority)
		{
			priority = this.priorities[0];
			return this.payloads[0];
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0004A8AF File Offset: 0x00048AAF
		public bool Pop()
		{
			if (this.Count == 0)
			{
				return false;
			}
			this.RemoveAt(0);
			return true;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0004A8C4 File Offset: 0x00048AC4
		private void MoveUp(int index)
		{
			TElement telement = this.payloads[index];
			TPriority tpriority = this.priorities[index];
			int i;
			int num;
			for (i = index; i > 0; i = num)
			{
				num = i / 2;
				if (tpriority.CompareTo(this.priorities[num]) >= 0)
				{
					break;
				}
				this.payloads[i] = this.payloads[num];
				this.priorities[i] = this.priorities[num];
				this.positions[this.payloads[i]] = i;
			}
			if (i != index)
			{
				this.payloads[i] = telement;
				this.priorities[i] = tpriority;
				this.positions[telement] = i;
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0004A988 File Offset: 0x00048B88
		private void MoveDown(int index)
		{
			TElement telement = this.payloads[index];
			TPriority tpriority = this.priorities[index];
			int num = index;
			while (2 * num < this.Count)
			{
				int num2 = num;
				int num3 = 2 * num;
				if (tpriority.CompareTo(this.priorities[num3]) >= 0)
				{
					num2 = num3;
				}
				num3++;
				if (num3 < this.Count)
				{
					TPriority tpriority2 = this.priorities[num2];
					if (tpriority2.CompareTo(this.priorities[num3]) >= 0)
					{
						num2 = num3;
					}
				}
				if (num2 == num)
				{
					break;
				}
				this.payloads[num] = this.payloads[num2];
				this.priorities[num] = this.priorities[num2];
				this.positions[this.payloads[num]] = num;
				num = num2;
			}
			if (num != index)
			{
				this.payloads[num] = telement;
				this.priorities[num] = tpriority;
				this.positions[telement] = num;
			}
		}

		// Token: 0x040009EF RID: 2543
		private List<TElement> payloads = new List<TElement>();

		// Token: 0x040009F0 RID: 2544
		private List<TPriority> priorities = new List<TPriority>();

		// Token: 0x040009F1 RID: 2545
		private Dictionary<TElement, int> positions = new Dictionary<TElement, int>();
	}
}
