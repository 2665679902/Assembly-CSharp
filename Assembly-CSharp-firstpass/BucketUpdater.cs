using System;

// Token: 0x02000100 RID: 256
public class BucketUpdater<DataType> : UpdateBucketWithUpdater<DataType>.IUpdater
{
	// Token: 0x060008B1 RID: 2225 RVA: 0x00022EF0 File Offset: 0x000210F0
	public BucketUpdater(Action<DataType, float> callback)
	{
		this.callback = callback;
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x00022EFF File Offset: 0x000210FF
	public void Update(DataType data, float dt)
	{
		this.callback(data, dt);
	}

	// Token: 0x04000662 RID: 1634
	private Action<DataType, float> callback;
}
