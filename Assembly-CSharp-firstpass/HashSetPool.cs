using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x0200008B RID: 139
public static class HashSetPool<ObjectType, PoolIdentifier>
{
	// Token: 0x0600056D RID: 1389 RVA: 0x0001A4F5 File Offset: 0x000186F5
	public static HashSetPool<ObjectType, PoolIdentifier>.PooledHashSet Allocate()
	{
		return HashSetPool<ObjectType, PoolIdentifier>.pool.Allocate();
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x0001A501 File Offset: 0x00018701
	private static void Free(HashSetPool<ObjectType, PoolIdentifier>.PooledHashSet hash_set)
	{
		hash_set.Clear();
		HashSetPool<ObjectType, PoolIdentifier>.pool.Free(hash_set);
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0001A514 File Offset: 0x00018714
	public static ContainerPool GetPool()
	{
		return HashSetPool<ObjectType, PoolIdentifier>.pool;
	}

	// Token: 0x04000540 RID: 1344
	private static ContainerPool<HashSetPool<ObjectType, PoolIdentifier>.PooledHashSet, PoolIdentifier> pool = new ContainerPool<HashSetPool<ObjectType, PoolIdentifier>.PooledHashSet, PoolIdentifier>();

	// Token: 0x020009D2 RID: 2514
	[DebuggerDisplay("Count={Count}")]
	public class PooledHashSet : HashSet<ObjectType>, IDisposable
	{
		// Token: 0x0600538E RID: 21390 RVA: 0x0009C08C File Offset: 0x0009A28C
		public void Recycle()
		{
			HashSetPool<ObjectType, PoolIdentifier>.Free(this);
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x0009C094 File Offset: 0x0009A294
		public void Dispose()
		{
			this.Recycle();
		}
	}
}
