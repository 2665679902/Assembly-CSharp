using System;

// Token: 0x02000906 RID: 2310
public class ScenePartitionerEntry
{
	// Token: 0x06004356 RID: 17238 RVA: 0x0017CD90 File Offset: 0x0017AF90
	public ScenePartitionerEntry(string name, object obj, int x, int y, int width, int height, ScenePartitionerLayer layer, ScenePartitioner partitioner, Action<object> event_callback)
	{
		if (x < 0 || y < 0 || width >= 0)
		{
		}
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		this.layer = layer.layer;
		this.partitioner = partitioner;
		this.eventCallback = event_callback;
		this.obj = obj;
	}

	// Token: 0x06004357 RID: 17239 RVA: 0x0017CDF9 File Offset: 0x0017AFF9
	public void UpdatePosition(int x, int y)
	{
		this.partitioner.UpdatePosition(x, y, this);
	}

	// Token: 0x06004358 RID: 17240 RVA: 0x0017CE09 File Offset: 0x0017B009
	public void UpdatePosition(Extents e)
	{
		this.partitioner.UpdatePosition(e, this);
	}

	// Token: 0x06004359 RID: 17241 RVA: 0x0017CE18 File Offset: 0x0017B018
	public void Release()
	{
		if (this.partitioner != null)
		{
			this.partitioner.Remove(this);
		}
	}

	// Token: 0x04002CE5 RID: 11493
	public int x;

	// Token: 0x04002CE6 RID: 11494
	public int y;

	// Token: 0x04002CE7 RID: 11495
	public int width;

	// Token: 0x04002CE8 RID: 11496
	public int height;

	// Token: 0x04002CE9 RID: 11497
	public int layer;

	// Token: 0x04002CEA RID: 11498
	public int queryId;

	// Token: 0x04002CEB RID: 11499
	public ScenePartitioner partitioner;

	// Token: 0x04002CEC RID: 11500
	public Action<object> eventCallback;

	// Token: 0x04002CED RID: 11501
	public object obj;
}
