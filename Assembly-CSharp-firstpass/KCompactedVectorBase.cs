using System;
using System.Collections.Generic;

// Token: 0x020000AD RID: 173
public abstract class KCompactedVectorBase
{
	// Token: 0x06000668 RID: 1640 RVA: 0x0001CC3D File Offset: 0x0001AE3D
	protected KCompactedVectorBase(int initial_count)
	{
		this.handles = new HandleVector<int>(initial_count);
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0001CC5C File Offset: 0x0001AE5C
	protected HandleVector<int>.Handle Allocate(int item)
	{
		HandleVector<int>.Handle handle = this.handles.Add(item);
		byte b;
		int num;
		this.handles.UnpackHandle(handle, out b, out num);
		this.dataHandleIndices.Add(num);
		return handle;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0001CC94 File Offset: 0x0001AE94
	protected bool Free(HandleVector<int>.Handle handle, int last_idx, out int free_component_idx)
	{
		free_component_idx = -1;
		if (!handle.IsValid())
		{
			return false;
		}
		free_component_idx = this.handles.Release(handle);
		if (free_component_idx < last_idx)
		{
			int num = this.dataHandleIndices[last_idx];
			if (this.handles.Items[num] != last_idx)
			{
				DebugUtil.LogErrorArgs(new object[] { "KCompactedVector: Bad state after attempting to free handle", handle.index });
			}
			this.handles.Items[num] = free_component_idx;
			this.dataHandleIndices[free_component_idx] = num;
		}
		this.dataHandleIndices.RemoveAt(last_idx);
		return true;
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0001CD33 File Offset: 0x0001AF33
	public bool IsValid(HandleVector<int>.Handle handle)
	{
		return this.handles.IsValid(handle);
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0001CD41 File Offset: 0x0001AF41
	public bool IsVersionValid(HandleVector<int>.Handle handle)
	{
		return this.handles.IsVersionValid(handle);
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0001CD50 File Offset: 0x0001AF50
	protected int ComputeIndex(HandleVector<int>.Handle handle)
	{
		byte b;
		int num;
		this.handles.UnpackHandle(handle, out b, out num);
		return this.handles.Items[num];
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0001CD7E File Offset: 0x0001AF7E
	protected void Clear()
	{
		this.dataHandleIndices.Clear();
		this.handles.Clear();
	}

	// Token: 0x040005B5 RID: 1461
	protected List<int> dataHandleIndices = new List<int>();

	// Token: 0x040005B6 RID: 1462
	protected HandleVector<int> handles;
}
