using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000082 RID: 130
public class BinaryHeap<T> : IEnumerable<T>, IEnumerable
{
	// Token: 0x06000524 RID: 1316 RVA: 0x00018F13 File Offset: 0x00017113
	public BinaryHeap()
		: this(Comparer<T>.Default)
	{
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00018F20 File Offset: 0x00017120
	public BinaryHeap(IComparer<T> comp)
	{
		this.Comparer = comp;
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06000526 RID: 1318 RVA: 0x00018F3A File Offset: 0x0001713A
	public int Count
	{
		get
		{
			return this.Items.Count;
		}
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00018F47 File Offset: 0x00017147
	public void Clear()
	{
		this.Items.Clear();
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x00018F54 File Offset: 0x00017154
	public void TrimExcess()
	{
		this.Items.TrimExcess();
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00018F64 File Offset: 0x00017164
	public void Insert(T newItem)
	{
		int num = this.Count;
		this.Items.Add(newItem);
		while (num > 0 && this.Comparer.Compare(this.Items[(num - 1) / 2], newItem) > 0)
		{
			this.Items[num] = this.Items[(num - 1) / 2];
			num = (num - 1) / 2;
		}
		this.Items[num] = newItem;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00018FD8 File Offset: 0x000171D8
	public T Peek()
	{
		if (this.Items.Count == 0)
		{
			throw new InvalidOperationException("The heap is empty.");
		}
		return this.Items[0];
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00019000 File Offset: 0x00017200
	public T RemoveRoot()
	{
		if (this.Items.Count == 0)
		{
			throw new InvalidOperationException("The heap is empty.");
		}
		T t = this.Items[0];
		T t2 = this.Items[this.Items.Count - 1];
		this.Items.RemoveAt(this.Items.Count - 1);
		if (this.Items.Count > 0)
		{
			int i;
			int num;
			for (i = 0; i < this.Items.Count / 2; i = num)
			{
				num = 2 * i + 1;
				if (num < this.Items.Count - 1 && this.Comparer.Compare(this.Items[num], this.Items[num + 1]) > 0)
				{
					num++;
				}
				if (this.Comparer.Compare(this.Items[num], t2) >= 0)
				{
					break;
				}
				this.Items[i] = this.Items[num];
			}
			this.Items[i] = t2;
		}
		return t;
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x00019110 File Offset: 0x00017310
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		foreach (T t in this.Items)
		{
			yield return t;
		}
		List<T>.Enumerator enumerator = default(List<T>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x0001911F File Offset: 0x0001731F
	public IEnumerator GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x04000526 RID: 1318
	private IComparer<T> Comparer;

	// Token: 0x04000527 RID: 1319
	private List<T> Items = new List<T>();
}
