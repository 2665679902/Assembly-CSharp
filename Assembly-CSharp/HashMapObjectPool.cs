using System;
using System.Collections.Generic;

// Token: 0x0200035E RID: 862
public class HashMapObjectPool<PoolKey, PoolValue>
{
	// Token: 0x0600115C RID: 4444 RVA: 0x0005D3F9 File Offset: 0x0005B5F9
	public HashMapObjectPool(Func<PoolKey, PoolValue> instantiator, int initialCount = 0)
	{
		this.initialCount = initialCount;
		this.instantiator = instantiator;
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x0005D41C File Offset: 0x0005B61C
	public HashMapObjectPool(HashMapObjectPool<PoolKey, PoolValue>.IPoolDescriptor[] descriptors, int initialCount = 0)
	{
		this.initialCount = initialCount;
		for (int i = 0; i < descriptors.Length; i++)
		{
			if (this.objectPoolMap.ContainsKey(descriptors[i].PoolId))
			{
				Debug.LogWarning(string.Format("HshMapObjectPool alaready contains key of {0}! Skipping!", descriptors[i].PoolId));
			}
			else
			{
				this.objectPoolMap[descriptors[i].PoolId] = new ObjectPool<PoolValue>(new Func<PoolValue>(descriptors[i].GetInstance), initialCount);
			}
		}
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x0005D4AC File Offset: 0x0005B6AC
	public PoolValue GetInstance(PoolKey poolId)
	{
		ObjectPool<PoolValue> objectPool;
		if (!this.objectPoolMap.TryGetValue(poolId, out objectPool))
		{
			objectPool = (this.objectPoolMap[poolId] = new ObjectPool<PoolValue>(new Func<PoolValue>(this.PoolInstantiator), this.initialCount));
		}
		this.currentPoolId = poolId;
		return objectPool.GetInstance();
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x0005D500 File Offset: 0x0005B700
	public void ReleaseInstance(PoolKey poolId, PoolValue inst)
	{
		ObjectPool<PoolValue> objectPool;
		if (inst == null || !this.objectPoolMap.TryGetValue(poolId, out objectPool))
		{
			return;
		}
		objectPool.ReleaseInstance(inst);
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x0005D530 File Offset: 0x0005B730
	private PoolValue PoolInstantiator()
	{
		if (this.instantiator == null)
		{
			return default(PoolValue);
		}
		return this.instantiator(this.currentPoolId);
	}

	// Token: 0x0400097B RID: 2427
	private Dictionary<PoolKey, ObjectPool<PoolValue>> objectPoolMap = new Dictionary<PoolKey, ObjectPool<PoolValue>>();

	// Token: 0x0400097C RID: 2428
	private int initialCount;

	// Token: 0x0400097D RID: 2429
	private PoolKey currentPoolId;

	// Token: 0x0400097E RID: 2430
	private Func<PoolKey, PoolValue> instantiator;

	// Token: 0x02000F22 RID: 3874
	public interface IPoolDescriptor
	{
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06006E28 RID: 28200
		PoolKey PoolId { get; }

		// Token: 0x06006E29 RID: 28201
		PoolValue GetInstance();
	}
}
