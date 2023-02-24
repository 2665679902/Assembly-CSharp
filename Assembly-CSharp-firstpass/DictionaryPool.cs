using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x0200008C RID: 140
public static class DictionaryPool<KeyType, ObjectType, PoolIdentifier>
{
	// Token: 0x06000571 RID: 1393 RVA: 0x0001A527 File Offset: 0x00018727
	public static DictionaryPool<KeyType, ObjectType, PoolIdentifier>.PooledDictionary Allocate()
	{
		return DictionaryPool<KeyType, ObjectType, PoolIdentifier>.pool.Allocate();
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0001A533 File Offset: 0x00018733
	private static void Free(DictionaryPool<KeyType, ObjectType, PoolIdentifier>.PooledDictionary dictionary)
	{
		dictionary.Clear();
		DictionaryPool<KeyType, ObjectType, PoolIdentifier>.pool.Free(dictionary);
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x0001A546 File Offset: 0x00018746
	public static ContainerPool GetPool()
	{
		return DictionaryPool<KeyType, ObjectType, PoolIdentifier>.pool;
	}

	// Token: 0x04000541 RID: 1345
	private static ContainerPool<DictionaryPool<KeyType, ObjectType, PoolIdentifier>.PooledDictionary, PoolIdentifier> pool = new ContainerPool<DictionaryPool<KeyType, ObjectType, PoolIdentifier>.PooledDictionary, PoolIdentifier>();

	// Token: 0x020009D3 RID: 2515
	[DebuggerDisplay("Count={Count}")]
	public class PooledDictionary : Dictionary<KeyType, ObjectType>, IDisposable
	{
		// Token: 0x06005391 RID: 21393 RVA: 0x0009C0A4 File Offset: 0x0009A2A4
		public void Recycle()
		{
			DictionaryPool<KeyType, ObjectType, PoolIdentifier>.Free(this);
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x0009C0AC File Offset: 0x0009A2AC
		public void Dispose()
		{
			this.Recycle();
		}
	}
}
