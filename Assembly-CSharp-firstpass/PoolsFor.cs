using System;

// Token: 0x02000089 RID: 137
public static class PoolsFor<PoolIdentifier>
{
	// Token: 0x06000564 RID: 1380 RVA: 0x0001A494 File Offset: 0x00018694
	public static ListPool<ObjectType, PoolIdentifier>.PooledList AllocateList<ObjectType>()
	{
		return ListPool<ObjectType, PoolIdentifier>.Allocate();
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0001A49B File Offset: 0x0001869B
	public static HashSetPool<ObjectType, PoolIdentifier>.PooledHashSet AllocateHashSet<ObjectType>()
	{
		return HashSetPool<ObjectType, PoolIdentifier>.Allocate();
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0001A4A2 File Offset: 0x000186A2
	public static DictionaryPool<KeyType, ObjectType, PoolIdentifier>.PooledDictionary AllocateDict<KeyType, ObjectType>()
	{
		return DictionaryPool<KeyType, ObjectType, PoolIdentifier>.Allocate();
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0001A4A9 File Offset: 0x000186A9
	public static QueuePool<ObjectType, PoolIdentifier>.PooledQueue AllocateQueue<ObjectType>()
	{
		return QueuePool<ObjectType, PoolIdentifier>.Allocate();
	}
}
