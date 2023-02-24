using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200035B RID: 859
public class ListWithEvents<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
{
	// Token: 0x1700004C RID: 76
	// (get) Token: 0x06001143 RID: 4419 RVA: 0x0005D0DD File Offset: 0x0005B2DD
	public int Count
	{
		get
		{
			return this.internalList.Count;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06001144 RID: 4420 RVA: 0x0005D0EA File Offset: 0x0005B2EA
	public bool IsReadOnly
	{
		get
		{
			return ((ICollection<T>)this.internalList).IsReadOnly;
		}
	}

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06001145 RID: 4421 RVA: 0x0005D0F8 File Offset: 0x0005B2F8
	// (remove) Token: 0x06001146 RID: 4422 RVA: 0x0005D130 File Offset: 0x0005B330
	public event Action<T> onAdd;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06001147 RID: 4423 RVA: 0x0005D168 File Offset: 0x0005B368
	// (remove) Token: 0x06001148 RID: 4424 RVA: 0x0005D1A0 File Offset: 0x0005B3A0
	public event Action<T> onRemove;

	// Token: 0x1700004E RID: 78
	public T this[int index]
	{
		get
		{
			return this.internalList[index];
		}
		set
		{
			this.internalList[index] = value;
		}
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x0005D1F2 File Offset: 0x0005B3F2
	public void Add(T item)
	{
		this.internalList.Add(item);
		if (this.onAdd != null)
		{
			this.onAdd(item);
		}
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x0005D214 File Offset: 0x0005B414
	public void Insert(int index, T item)
	{
		this.internalList.Insert(index, item);
		if (this.onAdd != null)
		{
			this.onAdd(item);
		}
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x0005D238 File Offset: 0x0005B438
	public void RemoveAt(int index)
	{
		T t = this.internalList[index];
		this.internalList.RemoveAt(index);
		if (this.onRemove != null)
		{
			this.onRemove(t);
		}
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x0005D272 File Offset: 0x0005B472
	public bool Remove(T item)
	{
		bool flag = this.internalList.Remove(item);
		if (flag && this.onRemove != null)
		{
			this.onRemove(item);
		}
		return flag;
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x0005D297 File Offset: 0x0005B497
	public void Clear()
	{
		while (this.Count > 0)
		{
			this.RemoveAt(0);
		}
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x0005D2AB File Offset: 0x0005B4AB
	public int IndexOf(T item)
	{
		return this.internalList.IndexOf(item);
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0005D2B9 File Offset: 0x0005B4B9
	public void CopyTo(T[] array, int arrayIndex)
	{
		this.internalList.CopyTo(array, arrayIndex);
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x0005D2C8 File Offset: 0x0005B4C8
	public bool Contains(T item)
	{
		return this.internalList.Contains(item);
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x0005D2D6 File Offset: 0x0005B4D6
	public IEnumerator<T> GetEnumerator()
	{
		return this.internalList.GetEnumerator();
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x0005D2E8 File Offset: 0x0005B4E8
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.internalList.GetEnumerator();
	}

	// Token: 0x04000976 RID: 2422
	private List<T> internalList = new List<T>();
}
