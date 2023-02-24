using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x0200008D RID: 141
public static class QueuePool<ObjectType, PoolIdentifier>
{
	// Token: 0x06000575 RID: 1397 RVA: 0x0001A559 File Offset: 0x00018759
	public static QueuePool<ObjectType, PoolIdentifier>.PooledQueue Allocate()
	{
		return QueuePool<ObjectType, PoolIdentifier>.pool.Allocate();
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x0001A565 File Offset: 0x00018765
	private static void Free(QueuePool<ObjectType, PoolIdentifier>.PooledQueue queue)
	{
		queue.Clear();
		QueuePool<ObjectType, PoolIdentifier>.pool.Free(queue);
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0001A578 File Offset: 0x00018778
	public static ContainerPool GetPool()
	{
		return QueuePool<ObjectType, PoolIdentifier>.pool;
	}

	// Token: 0x04000542 RID: 1346
	private static ContainerPool<QueuePool<ObjectType, PoolIdentifier>.PooledQueue, PoolIdentifier> pool = new ContainerPool<QueuePool<ObjectType, PoolIdentifier>.PooledQueue, PoolIdentifier>();

	// Token: 0x020009D4 RID: 2516
	[DebuggerDisplay("Count={Count}")]
	public class PooledQueue : Queue<ObjectType>, IDisposable
	{
		// Token: 0x06005394 RID: 21396 RVA: 0x0009C0BC File Offset: 0x0009A2BC
		public void Recycle()
		{
			QueuePool<ObjectType, PoolIdentifier>.Free(this);
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x0009C0C4 File Offset: 0x0009A2C4
		public void Dispose()
		{
			this.Recycle();
		}
	}
}
