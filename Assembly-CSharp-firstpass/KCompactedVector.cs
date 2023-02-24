using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000AE RID: 174
public class KCompactedVector<T> : KCompactedVectorBase, ICollection, IEnumerable
{
	// Token: 0x0600066F RID: 1647 RVA: 0x0001CD96 File Offset: 0x0001AF96
	public KCompactedVector(int initial_count = 0)
		: base(initial_count)
	{
		this.data = new List<T>(initial_count);
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0001CDAB File Offset: 0x0001AFAB
	public HandleVector<int>.Handle Allocate(T initial_data)
	{
		this.data.Add(initial_data);
		return base.Allocate(this.data.Count - 1);
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0001CDCC File Offset: 0x0001AFCC
	public HandleVector<int>.Handle Free(HandleVector<int>.Handle handle)
	{
		int num = this.data.Count - 1;
		int num2;
		bool flag = base.Free(handle, num, out num2);
		if (flag)
		{
			if (num2 < num)
			{
				this.data[num2] = this.data[num];
			}
			this.data.RemoveAt(num);
		}
		if (!flag)
		{
			return handle;
		}
		return HandleVector<int>.InvalidHandle;
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0001CE25 File Offset: 0x0001B025
	public T GetData(HandleVector<int>.Handle handle)
	{
		return this.data[base.ComputeIndex(handle)];
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0001CE39 File Offset: 0x0001B039
	public void SetData(HandleVector<int>.Handle handle, T new_data)
	{
		this.data[base.ComputeIndex(handle)] = new_data;
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0001CE4E File Offset: 0x0001B04E
	public new virtual void Clear()
	{
		base.Clear();
		this.data.Clear();
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001CE61 File Offset: 0x0001B061
	public int Count
	{
		get
		{
			return this.data.Count;
		}
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0001CE6E File Offset: 0x0001B06E
	public List<T> GetDataList()
	{
		return this.data;
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001CE76 File Offset: 0x0001B076
	public bool IsSynchronized
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001CE7D File Offset: 0x0001B07D
	public object SyncRoot
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0001CE84 File Offset: 0x0001B084
	public void CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0001CE8B File Offset: 0x0001B08B
	public IEnumerator GetEnumerator()
	{
		return this.data.GetEnumerator();
	}

	// Token: 0x040005B7 RID: 1463
	protected List<T> data;
}
