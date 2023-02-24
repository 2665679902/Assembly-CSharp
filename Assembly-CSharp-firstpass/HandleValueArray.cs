using System;

// Token: 0x020000A2 RID: 162
public class HandleValueArray<T> : HandleValueArrayBase
{
	// Token: 0x06000625 RID: 1573 RVA: 0x0001C224 File Offset: 0x0001A424
	public HandleValueArray(int reserve_size)
	{
		this.Entries = new HandleValueArray<T>.Entry[reserve_size];
		this.Indices = new int[reserve_size];
		for (int i = 0; i < this.Entries.Length; i++)
		{
			this.Entries[i].Handle = i;
		}
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0001C274 File Offset: 0x0001A474
	public ValueArrayHandle Add(ref T value)
	{
		if (this.Count == this.Entries.Length)
		{
			HandleValueArray<T>.Entry[] array = new HandleValueArray<T>.Entry[this.Entries.Length * 2];
			int[] array2 = new int[this.Entries.Length * 2];
			for (int i = 0; i < this.Entries.Length; i++)
			{
				array[i] = this.Entries[i];
				array2[i] = this.Indices[i];
			}
			for (int j = this.Entries.Length; j < array.Length; j++)
			{
				array[j].Handle = j;
			}
			this.Entries = array;
			this.Indices = array2;
		}
		int handle = this.Entries[this.Count].Handle;
		int count = this.Count;
		this.Entries[count].Value = value;
		this.Indices[handle] = count;
		this.Count++;
		return new ValueArrayHandle(handle);
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0001C375 File Offset: 0x0001A575
	public int GetIndex(ref ValueArrayHandle handle)
	{
		return this.Indices[handle.handle];
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0001C384 File Offset: 0x0001A584
	public void Remove(ref ValueArrayHandle handle)
	{
		int num = this.Indices[handle.handle];
		this.Count--;
		int handle2 = this.Entries[this.Count].Handle;
		this.Entries[num] = this.Entries[this.Count];
		this.Entries[this.Count].Handle = handle.handle;
		this.Indices[handle2] = num;
	}

	// Token: 0x0400059E RID: 1438
	public int Count;

	// Token: 0x0400059F RID: 1439
	public HandleValueArray<T>.Entry[] Entries;

	// Token: 0x040005A0 RID: 1440
	private int[] Indices;

	// Token: 0x020009E2 RID: 2530
	public struct Entry
	{
		// Token: 0x04002220 RID: 8736
		public T Value;

		// Token: 0x04002221 RID: 8737
		public int Handle;
	}
}
