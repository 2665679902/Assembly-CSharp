using System;
using System.Collections.Generic;

// Token: 0x02000681 RID: 1665
public class CheckedHandleVector<T> where T : new()
{
	// Token: 0x06002CD8 RID: 11480 RVA: 0x000EB4D8 File Offset: 0x000E96D8
	public CheckedHandleVector(int initial_size)
	{
		this.handleVector = new HandleVector<T>(initial_size);
		this.isFree = new List<bool>(initial_size);
		for (int i = 0; i < initial_size; i++)
		{
			this.isFree.Add(true);
		}
	}

	// Token: 0x06002CD9 RID: 11481 RVA: 0x000EB528 File Offset: 0x000E9728
	public HandleVector<T>.Handle Add(T item, string debug_info)
	{
		HandleVector<T>.Handle handle = this.handleVector.Add(item);
		if (handle.index >= this.isFree.Count)
		{
			this.isFree.Add(false);
		}
		else
		{
			this.isFree[handle.index] = false;
		}
		int i = this.handleVector.Items.Count;
		while (i > this.debugInfo.Count)
		{
			this.debugInfo.Add(null);
		}
		this.debugInfo[handle.index] = debug_info;
		return handle;
	}

	// Token: 0x06002CDA RID: 11482 RVA: 0x000EB5B8 File Offset: 0x000E97B8
	public T Release(HandleVector<T>.Handle handle)
	{
		if (this.isFree[handle.index])
		{
			DebugUtil.LogErrorArgs(new object[]
			{
				"Tried to double free checked handle ",
				handle.index,
				"- Debug info:",
				this.debugInfo[handle.index]
			});
		}
		this.isFree[handle.index] = true;
		return this.handleVector.Release(handle);
	}

	// Token: 0x06002CDB RID: 11483 RVA: 0x000EB637 File Offset: 0x000E9837
	public T Get(HandleVector<T>.Handle handle)
	{
		return this.handleVector.GetItem(handle);
	}

	// Token: 0x04001ACA RID: 6858
	private HandleVector<T> handleVector;

	// Token: 0x04001ACB RID: 6859
	private List<string> debugInfo = new List<string>();

	// Token: 0x04001ACC RID: 6860
	private List<bool> isFree;
}
