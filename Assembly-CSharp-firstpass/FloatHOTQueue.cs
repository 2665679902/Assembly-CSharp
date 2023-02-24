using System;
using System.Collections.Generic;

// Token: 0x02000135 RID: 309
public class FloatHOTQueue<TValue>
{
	// Token: 0x06000A79 RID: 2681 RVA: 0x000284D0 File Offset: 0x000266D0
	public KeyValuePair<float, TValue> Dequeue()
	{
		if (this.hotQueue.Count == 0)
		{
			FloatHOTQueue<TValue>.PriorityQueue priorityQueue = this.hotQueue;
			this.hotQueue = this.coldQueue;
			this.coldQueue = priorityQueue;
			this.hotThreshold = this.coldThreshold;
		}
		this.count--;
		return this.hotQueue.Dequeue();
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0002852C File Offset: 0x0002672C
	public void Enqueue(float priority, TValue value)
	{
		if (priority <= this.hotThreshold)
		{
			this.hotQueue.Enqueue(priority, value);
		}
		else
		{
			this.coldQueue.Enqueue(priority, value);
			this.coldThreshold = Math.Max(this.coldThreshold, priority);
		}
		this.count++;
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x00028580 File Offset: 0x00026780
	public KeyValuePair<float, TValue> Peek()
	{
		if (this.hotQueue.Count == 0)
		{
			FloatHOTQueue<TValue>.PriorityQueue priorityQueue = this.hotQueue;
			this.hotQueue = this.coldQueue;
			this.coldQueue = priorityQueue;
			this.hotThreshold = this.coldThreshold;
		}
		return this.hotQueue.Peek();
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x000285CB File Offset: 0x000267CB
	public void Clear()
	{
		this.count = 0;
		this.hotThreshold = float.MinValue;
		this.hotQueue.Clear();
		this.coldThreshold = float.MinValue;
		this.coldQueue.Clear();
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00028600 File Offset: 0x00026800
	public int Count
	{
		get
		{
			return this.count;
		}
	}

	// Token: 0x040006DF RID: 1759
	private FloatHOTQueue<TValue>.PriorityQueue hotQueue = new FloatHOTQueue<TValue>.PriorityQueue();

	// Token: 0x040006E0 RID: 1760
	private FloatHOTQueue<TValue>.PriorityQueue coldQueue = new FloatHOTQueue<TValue>.PriorityQueue();

	// Token: 0x040006E1 RID: 1761
	private float hotThreshold = float.MinValue;

	// Token: 0x040006E2 RID: 1762
	private float coldThreshold = float.MinValue;

	// Token: 0x040006E3 RID: 1763
	private int count;

	// Token: 0x02000A10 RID: 2576
	private class PriorityQueue
	{
		// Token: 0x0600543A RID: 21562 RVA: 0x0009CD82 File Offset: 0x0009AF82
		public PriorityQueue()
		{
			this._baseHeap = new List<KeyValuePair<float, TValue>>();
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x0009CD95 File Offset: 0x0009AF95
		public void Enqueue(float priority, TValue value)
		{
			this.Insert(priority, value);
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x0009CD9F File Offset: 0x0009AF9F
		public KeyValuePair<float, TValue> Dequeue()
		{
			KeyValuePair<float, TValue> keyValuePair = this._baseHeap[0];
			this.DeleteRoot();
			return keyValuePair;
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x0009CDB3 File Offset: 0x0009AFB3
		public KeyValuePair<float, TValue> Peek()
		{
			if (this.Count > 0)
			{
				return this._baseHeap[0];
			}
			throw new InvalidOperationException("Priority queue is empty");
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0009CDD8 File Offset: 0x0009AFD8
		private void ExchangeElements(int pos1, int pos2)
		{
			KeyValuePair<float, TValue> keyValuePair = this._baseHeap[pos1];
			this._baseHeap[pos1] = this._baseHeap[pos2];
			this._baseHeap[pos2] = keyValuePair;
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0009CE18 File Offset: 0x0009B018
		private void Insert(float priority, TValue value)
		{
			KeyValuePair<float, TValue> keyValuePair = new KeyValuePair<float, TValue>(priority, value);
			this._baseHeap.Add(keyValuePair);
			this.HeapifyFromEndToBeginning(this._baseHeap.Count - 1);
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0009CE50 File Offset: 0x0009B050
		private int HeapifyFromEndToBeginning(int pos)
		{
			if (pos >= this._baseHeap.Count)
			{
				return -1;
			}
			while (pos > 0)
			{
				int num = (pos - 1) / 2;
				if (this._baseHeap[num].Key - this._baseHeap[pos].Key <= 0f)
				{
					break;
				}
				this.ExchangeElements(num, pos);
				pos = num;
			}
			return pos;
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x0009CEB4 File Offset: 0x0009B0B4
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

		// Token: 0x06005442 RID: 21570 RVA: 0x0009CF20 File Offset: 0x0009B120
		private void HeapifyFromBeginningToEnd(int pos)
		{
			int count = this._baseHeap.Count;
			if (pos >= count)
			{
				return;
			}
			for (;;)
			{
				int num = pos;
				int num2 = 2 * pos + 1;
				int num3 = 2 * pos + 2;
				if (num2 < count && this._baseHeap[num].Key - this._baseHeap[num2].Key > 0f)
				{
					num = num2;
				}
				if (num3 < count && this._baseHeap[num].Key - this._baseHeap[num3].Key > 0f)
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

		// Token: 0x06005443 RID: 21571 RVA: 0x0009CFD0 File Offset: 0x0009B1D0
		public void Clear()
		{
			this._baseHeap.Clear();
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06005444 RID: 21572 RVA: 0x0009CFDD File Offset: 0x0009B1DD
		public int Count
		{
			get
			{
				return this._baseHeap.Count;
			}
		}

		// Token: 0x04002281 RID: 8833
		private List<KeyValuePair<float, TValue>> _baseHeap;
	}
}
