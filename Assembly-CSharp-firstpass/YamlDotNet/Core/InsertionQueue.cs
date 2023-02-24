using System;
using System.Collections.Generic;

namespace YamlDotNet.Core
{
	// Token: 0x02000208 RID: 520
	[Serializable]
	public class InsertionQueue<T>
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x000402FB File Offset: 0x0003E4FB
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00040308 File Offset: 0x0003E508
		public void Enqueue(T item)
		{
			this.items.Add(item);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00040316 File Offset: 0x0003E516
		public T Dequeue()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("The queue is empty");
			}
			T t = this.items[0];
			this.items.RemoveAt(0);
			return t;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00040343 File Offset: 0x0003E543
		public void Insert(int index, T item)
		{
			this.items.Insert(index, item);
		}

		// Token: 0x040008B9 RID: 2233
		private readonly IList<T> items = new List<T>();
	}
}
