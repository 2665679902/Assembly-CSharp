using System;
using System.Collections.Generic;

// Token: 0x0200009E RID: 158
public class FrameDelayedHandleVector<T> : HandleVector<T>
{
	// Token: 0x0600061B RID: 1563 RVA: 0x0001C078 File Offset: 0x0001A278
	public FrameDelayedHandleVector(int initial_size)
		: base(initial_size)
	{
		for (int i = 0; i < this.frameDelayedFreeHandles.Length; i++)
		{
			this.frameDelayedFreeHandles[i] = new List<HandleVector<T>.Handle>();
		}
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0001C0B8 File Offset: 0x0001A2B8
	public override void Clear()
	{
		this.freeHandles.Clear();
		this.items.Clear();
		List<HandleVector<T>.Handle>[] array = this.frameDelayedFreeHandles;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Clear();
		}
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0001C0F8 File Offset: 0x0001A2F8
	public override T Release(HandleVector<T>.Handle handle)
	{
		this.frameDelayedFreeHandles[this.curFrame].Add(handle);
		return base.GetItem(handle);
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0001C114 File Offset: 0x0001A314
	public void NextFrame()
	{
		int num = (this.curFrame + 1) % this.frameDelayedFreeHandles.Length;
		List<HandleVector<T>.Handle> list = this.frameDelayedFreeHandles[num];
		foreach (HandleVector<T>.Handle handle in list)
		{
			base.Release(handle);
		}
		list.Clear();
		this.curFrame = num;
	}

	// Token: 0x04000598 RID: 1432
	private List<HandleVector<T>.Handle>[] frameDelayedFreeHandles = new List<HandleVector<T>.Handle>[3];

	// Token: 0x04000599 RID: 1433
	private int curFrame;
}
