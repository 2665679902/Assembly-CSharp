using System;

// Token: 0x0200011D RID: 285
public class ValueArray<T>
{
	// Token: 0x060009B7 RID: 2487 RVA: 0x0002627A File Offset: 0x0002447A
	public ValueArray(int reserve_size)
	{
		this.Values = new T[reserve_size];
	}

	// Token: 0x170000F7 RID: 247
	public T this[int idx]
	{
		get
		{
			return this.Values[idx];
		}
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0002629C File Offset: 0x0002449C
	public void Add(ref T value)
	{
		if (this.Count == this.Values.Length)
		{
			this.Resize(this.Values.Length * 2);
		}
		this.Values[this.Count] = value;
		this.Count++;
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x000262F0 File Offset: 0x000244F0
	public void Resize(int new_size)
	{
		T[] array = new T[new_size];
		for (int i = 0; i < this.Values.Length; i++)
		{
			array[i] = this.Values[i];
		}
		this.Values = array;
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x00026331 File Offset: 0x00024531
	public void Remove(int index)
	{
		if (this.Count > 0)
		{
			this.Values[index] = this.Values[this.Count - 1];
		}
		this.Count--;
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x00026369 File Offset: 0x00024569
	public void Clear()
	{
		this.Count = 0;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00026374 File Offset: 0x00024574
	public bool IsEqual(ValueArray<T> array)
	{
		if (this.Count != array.Count)
		{
			return false;
		}
		for (int i = 0; i < this.Count; i++)
		{
			if (!this.Values[i].Equals(array.Values[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x000263D4 File Offset: 0x000245D4
	public void CopyFrom(ValueArray<T> array)
	{
		this.Clear();
		for (int i = 0; i < array.Count; i++)
		{
			T t = array[i];
			this.Add(ref t);
		}
	}

	// Token: 0x040006AC RID: 1708
	public int Count;

	// Token: 0x040006AD RID: 1709
	public T[] Values;
}
