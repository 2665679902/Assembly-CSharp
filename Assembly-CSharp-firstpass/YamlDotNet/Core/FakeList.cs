using System;
using System.Collections.Generic;

namespace YamlDotNet.Core
{
	// Token: 0x02000201 RID: 513
	public class FakeList<T>
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x00040242 File Offset: 0x0003E442
		public FakeList(IEnumerator<T> collection)
		{
			this.collection = collection;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00040258 File Offset: 0x0003E458
		public FakeList(IEnumerable<T> collection)
			: this(collection.GetEnumerator())
		{
		}

		// Token: 0x170001AB RID: 427
		public T this[int index]
		{
			get
			{
				if (index < this.currentIndex)
				{
					this.collection.Reset();
					this.currentIndex = -1;
				}
				while (this.currentIndex < index)
				{
					if (!this.collection.MoveNext())
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.currentIndex++;
				}
				return this.collection.Current;
			}
		}

		// Token: 0x040008B7 RID: 2231
		private readonly IEnumerator<T> collection;

		// Token: 0x040008B8 RID: 2232
		private int currentIndex = -1;
	}
}
