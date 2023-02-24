using System;
using KSerialization;

// Token: 0x0200007A RID: 122
[SerializationConfig(MemberSerialization.OptIn)]
public struct ArrayRef<T>
{
	// Token: 0x170000A6 RID: 166
	public T this[int i]
	{
		get
		{
			this.ValidateIndex(i);
			return this.elements[i];
		}
		set
		{
			this.ValidateIndex(i);
			this.elements[i] = value;
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001850E File Offset: 0x0001670E
	public int size
	{
		get
		{
			return this.sizeImpl;
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00018516 File Offset: 0x00016716
	public int Count
	{
		get
		{
			return this.size;
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001851E File Offset: 0x0001671E
	public int capacity
	{
		get
		{
			return this.capacityImpl;
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00018526 File Offset: 0x00016726
	public ArrayRef(int initialCapacity)
	{
		this.capacityImpl = initialCapacity;
		this.elements = new T[initialCapacity];
		this.sizeImpl = 0;
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x00018542 File Offset: 0x00016742
	public ArrayRef(T[] elements, int size)
	{
		Debug.Assert(size <= elements.Length);
		this.elements = elements;
		this.sizeImpl = size;
		this.capacityImpl = elements.Length;
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00018569 File Offset: 0x00016769
	public int Add(T item)
	{
		this.MaybeGrow(this.size);
		this.elements[this.size] = item;
		this.sizeImpl++;
		return this.size;
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x000185A0 File Offset: 0x000167A0
	public bool RemoveFirst(Predicate<T> match)
	{
		int num = this.FindIndex(match);
		if (num != -1)
		{
			this.RemoveAt(num);
			return true;
		}
		return false;
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x000185C4 File Offset: 0x000167C4
	public bool RemoveFirstSwap(Predicate<T> match)
	{
		int num = this.FindIndex(match);
		if (num != -1)
		{
			this.RemoveAtSwap(num);
			return true;
		}
		return false;
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x000185E8 File Offset: 0x000167E8
	public void RemoveAt(int index)
	{
		this.ValidateIndex(index);
		for (int num = index; num != this.size - 1; num++)
		{
			this.elements[num] = this.elements[num + 1];
		}
		this.sizeImpl--;
		DebugUtil.Assert(this.sizeImpl >= 0);
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00018648 File Offset: 0x00016848
	public void RemoveAtSwap(int index)
	{
		this.ValidateIndex(index);
		this.elements[index] = this.elements[this.size - 1];
		this.sizeImpl--;
		DebugUtil.Assert(this.sizeImpl >= 0);
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0001869C File Offset: 0x0001689C
	public void RemoveAll(Predicate<T> match)
	{
		for (int num = this.size - 1; num != -1; num--)
		{
			if (match(this.elements[num]))
			{
				this.RemoveAt(num);
			}
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x000186D8 File Offset: 0x000168D8
	public void RemoveAllSwap(Predicate<T> match)
	{
		int num = 0;
		while (num != this.size)
		{
			if (match(this.elements[num]))
			{
				this.elements[num] = this.elements[this.size - 1];
				this.sizeImpl--;
				DebugUtil.Assert(this.sizeImpl >= 0);
			}
			else
			{
				num++;
			}
		}
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0001874A File Offset: 0x0001694A
	public void Clear()
	{
		this.sizeImpl = 0;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x00018754 File Offset: 0x00016954
	public int FindIndex(Predicate<T> match)
	{
		for (int num = 0; num != this.size; num++)
		{
			if (match(this.elements[num]))
			{
				return num;
			}
		}
		return -1;
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x00018789 File Offset: 0x00016989
	public void ShrinkToFit()
	{
		if (this.size == this.capacity)
		{
			return;
		}
		this.Reallocate(this.size);
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x000187A6 File Offset: 0x000169A6
	private void ValidateIndex(int index)
	{
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x000187A8 File Offset: 0x000169A8
	private void MaybeGrow(int index)
	{
		DebugUtil.Assert(this.capacity == 0 || this.capacity == this.elements.Length);
		DebugUtil.Assert(index >= 0);
		if (index < this.capacity)
		{
			return;
		}
		this.Reallocate((this.capacity == 0) ? 1 : (this.capacity * 2));
		DebugUtil.Assert(this.capacity == 0 || this.capacity == this.elements.Length);
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00018824 File Offset: 0x00016A24
	private void Reallocate(int newCapacity)
	{
		Debug.Assert(this.size <= newCapacity);
		this.capacityImpl = newCapacity;
		T[] array = new T[this.capacity];
		for (int num = 0; num != this.size; num++)
		{
			array[num] = this.elements[num];
		}
		this.elements = array;
	}

	// Token: 0x04000514 RID: 1300
	[Serialize]
	private T[] elements;

	// Token: 0x04000515 RID: 1301
	[Serialize]
	private int sizeImpl;

	// Token: 0x04000516 RID: 1302
	[Serialize]
	private int capacityImpl;
}
