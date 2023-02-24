using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x0200008A RID: 138
public static class ListPool<ObjectType, PoolIdentifier>
{
	// Token: 0x06000568 RID: 1384 RVA: 0x0001A4B0 File Offset: 0x000186B0
	public static ListPool<ObjectType, PoolIdentifier>.PooledList Allocate(List<ObjectType> objects)
	{
		ListPool<ObjectType, PoolIdentifier>.PooledList pooledList = ListPool<ObjectType, PoolIdentifier>.pool.Allocate();
		pooledList.AddRange(objects);
		return pooledList;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0001A4C3 File Offset: 0x000186C3
	public static ListPool<ObjectType, PoolIdentifier>.PooledList Allocate()
	{
		return ListPool<ObjectType, PoolIdentifier>.pool.Allocate();
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0001A4CF File Offset: 0x000186CF
	private static void Free(ListPool<ObjectType, PoolIdentifier>.PooledList list)
	{
		list.Clear();
		ListPool<ObjectType, PoolIdentifier>.pool.Free(list);
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0001A4E2 File Offset: 0x000186E2
	public static ContainerPool GetPool()
	{
		return ListPool<ObjectType, PoolIdentifier>.pool;
	}

	// Token: 0x0400053F RID: 1343
	private static ContainerPool<ListPool<ObjectType, PoolIdentifier>.PooledList, PoolIdentifier> pool = new ContainerPool<ListPool<ObjectType, PoolIdentifier>.PooledList, PoolIdentifier>();

	// Token: 0x020009D1 RID: 2513
	[DebuggerDisplay("Count={Count}")]
	public class PooledList : List<ObjectType>, IDisposable
	{
		// Token: 0x0600538B RID: 21387 RVA: 0x0009C074 File Offset: 0x0009A274
		public void Recycle()
		{
			ListPool<ObjectType, PoolIdentifier>.Free(this);
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0009C07C File Offset: 0x0009A27C
		public void Dispose()
		{
			this.Recycle();
		}
	}
}
