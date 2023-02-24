using System;
using System.Collections;
using System.Collections.Generic;

namespace PriorityQueueDemo
{
	// Token: 0x02000470 RID: 1136
	public class PriorityQueue<TPriority, TValue> : ICollection<KeyValuePair<TPriority, TValue>>, IEnumerable<KeyValuePair<TPriority, TValue>>, IEnumerable
	{
		// Token: 0x060030DD RID: 12509 RVA: 0x000610C2 File Offset: 0x0005F2C2
		public PriorityQueue()
			: this(Comparer<TPriority>.Default)
		{
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000610CF File Offset: 0x0005F2CF
		public PriorityQueue(int capacity)
			: this(capacity, Comparer<TPriority>.Default)
		{
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000610DD File Offset: 0x0005F2DD
		public PriorityQueue(int capacity, IComparer<TPriority> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException();
			}
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>(capacity);
			this._comparer = comparer;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x00061101 File Offset: 0x0005F301
		public PriorityQueue(IComparer<TPriority> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException();
			}
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>();
			this._comparer = comparer;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x00061124 File Offset: 0x0005F324
		public PriorityQueue(IEnumerable<KeyValuePair<TPriority, TValue>> data)
			: this(data, Comparer<TPriority>.Default)
		{
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x00061134 File Offset: 0x0005F334
		public PriorityQueue(IEnumerable<KeyValuePair<TPriority, TValue>> data, IComparer<TPriority> comparer)
		{
			if (data == null || comparer == null)
			{
				throw new ArgumentNullException();
			}
			this._comparer = comparer;
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>(data);
			for (int i = this._baseHeap.Count / 2 - 1; i >= 0; i--)
			{
				this.HeapifyFromBeginningToEnd(i);
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x00061187 File Offset: 0x0005F387
		public static PriorityQueue<TPriority, TValue> MergeQueues(PriorityQueue<TPriority, TValue> pq1, PriorityQueue<TPriority, TValue> pq2)
		{
			if (pq1 == null || pq2 == null)
			{
				throw new ArgumentNullException();
			}
			if (pq1._comparer != pq2._comparer)
			{
				throw new InvalidOperationException("Priority queues to be merged must have equal comparers");
			}
			return PriorityQueue<TPriority, TValue>.MergeQueues(pq1, pq2, pq1._comparer);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000611BC File Offset: 0x0005F3BC
		public static PriorityQueue<TPriority, TValue> MergeQueues(PriorityQueue<TPriority, TValue> pq1, PriorityQueue<TPriority, TValue> pq2, IComparer<TPriority> comparer)
		{
			if (pq1 == null || pq2 == null || comparer == null)
			{
				throw new ArgumentNullException();
			}
			PriorityQueue<TPriority, TValue> priorityQueue = new PriorityQueue<TPriority, TValue>(pq1.Count + pq2.Count, pq1._comparer);
			priorityQueue._baseHeap.AddRange(pq1._baseHeap);
			priorityQueue._baseHeap.AddRange(pq2._baseHeap);
			for (int i = priorityQueue._baseHeap.Count / 2 - 1; i >= 0; i--)
			{
				priorityQueue.HeapifyFromBeginningToEnd(i);
			}
			return priorityQueue;
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x00061235 File Offset: 0x0005F435
		public void Enqueue(TPriority priority, TValue value)
		{
			this.Insert(priority, value);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x0006123F File Offset: 0x0005F43F
		public KeyValuePair<TPriority, TValue> Dequeue()
		{
			if (!this.IsEmpty)
			{
				KeyValuePair<TPriority, TValue> keyValuePair = this._baseHeap[0];
				this.DeleteRoot();
				return keyValuePair;
			}
			throw new InvalidOperationException("Priority queue is empty");
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x00061268 File Offset: 0x0005F468
		public TValue DequeueValue()
		{
			return this.Dequeue().Value;
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x00061283 File Offset: 0x0005F483
		public KeyValuePair<TPriority, TValue> Peek()
		{
			if (!this.IsEmpty)
			{
				return this._baseHeap[0];
			}
			throw new InvalidOperationException("Priority queue is empty");
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000612A4 File Offset: 0x0005F4A4
		public TValue PeekValue()
		{
			return this.Peek().Value;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x000612BF File Offset: 0x0005F4BF
		public bool IsEmpty
		{
			get
			{
				return this._baseHeap.Count == 0;
			}
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000612D0 File Offset: 0x0005F4D0
		private void ExchangeElements(int pos1, int pos2)
		{
			KeyValuePair<TPriority, TValue> keyValuePair = this._baseHeap[pos1];
			this._baseHeap[pos1] = this._baseHeap[pos2];
			this._baseHeap[pos2] = keyValuePair;
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x00061310 File Offset: 0x0005F510
		private void Insert(TPriority priority, TValue value)
		{
			KeyValuePair<TPriority, TValue> keyValuePair = new KeyValuePair<TPriority, TValue>(priority, value);
			this._baseHeap.Add(keyValuePair);
			this.HeapifyFromEndToBeginning(this._baseHeap.Count - 1);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x00061348 File Offset: 0x0005F548
		private int HeapifyFromEndToBeginning(int pos)
		{
			if (pos >= this._baseHeap.Count)
			{
				return -1;
			}
			while (pos > 0)
			{
				int num = (pos - 1) / 2;
				if (this._comparer.Compare(this._baseHeap[num].Key, this._baseHeap[pos].Key) <= 0)
				{
					break;
				}
				this.ExchangeElements(num, pos);
				pos = num;
			}
			return pos;
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000613B4 File Offset: 0x0005F5B4
		private void DeleteRoot()
		{
			if (this._baseHeap.Count <= 1)
			{
				this._baseHeap.Clear();
				return;
			}
			this._baseHeap[0] = this._baseHeap[this._baseHeap.Count - 1];
			this._baseHeap.RemoveAt(this._baseHeap.Count - 1);
			this.HeapifyFromBeginningToEnd(0);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x00061420 File Offset: 0x0005F620
		private void HeapifyFromBeginningToEnd(int pos)
		{
			if (pos >= this._baseHeap.Count)
			{
				return;
			}
			for (;;)
			{
				int num = pos;
				int num2 = 2 * pos + 1;
				int num3 = 2 * pos + 2;
				if (num2 < this._baseHeap.Count && this._comparer.Compare(this._baseHeap[num].Key, this._baseHeap[num2].Key) > 0)
				{
					num = num2;
				}
				if (num3 < this._baseHeap.Count && this._comparer.Compare(this._baseHeap[num].Key, this._baseHeap[num3].Key) > 0)
				{
					num = num3;
				}
				if (num == pos)
				{
					break;
				}
				this.ExchangeElements(num, pos);
				pos = num;
			}
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000614EA File Offset: 0x0005F6EA
		public void Add(KeyValuePair<TPriority, TValue> item)
		{
			this.Enqueue(item.Key, item.Value);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x00061500 File Offset: 0x0005F700
		public void Clear()
		{
			this._baseHeap.Clear();
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x0006150D File Offset: 0x0005F70D
		public bool Contains(KeyValuePair<TPriority, TValue> item)
		{
			return this._baseHeap.Contains(item);
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x0006151B File Offset: 0x0005F71B
		public int Count
		{
			get
			{
				return this._baseHeap.Count;
			}
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x00061528 File Offset: 0x0005F728
		public void CopyTo(KeyValuePair<TPriority, TValue>[] array, int arrayIndex)
		{
			this._baseHeap.CopyTo(array, arrayIndex);
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x00061537 File Offset: 0x0005F737
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x0006153C File Offset: 0x0005F73C
		public bool Remove(KeyValuePair<TPriority, TValue> item)
		{
			int num = this._baseHeap.IndexOf(item);
			if (num < 0)
			{
				return false;
			}
			this._baseHeap[num] = this._baseHeap[this._baseHeap.Count - 1];
			this._baseHeap.RemoveAt(this._baseHeap.Count - 1);
			if (this.HeapifyFromEndToBeginning(num) == num)
			{
				this.HeapifyFromBeginningToEnd(num);
			}
			return true;
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000615AA File Offset: 0x0005F7AA
		public IEnumerator<KeyValuePair<TPriority, TValue>> GetEnumerator()
		{
			return this._baseHeap.GetEnumerator();
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000615BC File Offset: 0x0005F7BC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040010E8 RID: 4328
		private List<KeyValuePair<TPriority, TValue>> _baseHeap;

		// Token: 0x040010E9 RID: 4329
		private IComparer<TPriority> _comparer;
	}
}
