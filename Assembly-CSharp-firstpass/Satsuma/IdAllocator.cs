using System;

namespace Satsuma
{
	// Token: 0x02000286 RID: 646
	internal abstract class IdAllocator
	{
		// Token: 0x060013E5 RID: 5093 RVA: 0x0004C85F File Offset: 0x0004AA5F
		public IdAllocator()
		{
			this.randomSeed = 205891132094649L;
			this.Rewind();
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0004C87C File Offset: 0x0004AA7C
		private long Random()
		{
			return this.randomSeed *= 3L;
		}

		// Token: 0x060013E7 RID: 5095
		protected abstract bool IsAllocated(long id);

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004C89B File Offset: 0x0004AA9B
		public void Rewind()
		{
			this.lastAllocated = 0L;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004C8A8 File Offset: 0x0004AAA8
		public long Allocate()
		{
			long num = this.lastAllocated + 1L;
			int num2 = 0;
			for (;;)
			{
				if (num == 0L)
				{
					num = 1L;
				}
				if (!this.IsAllocated(num))
				{
					break;
				}
				num += 1L;
				num2++;
				if (num2 >= 100)
				{
					num = this.Random();
					num2 = 0;
				}
			}
			this.lastAllocated = num;
			return num;
		}

		// Token: 0x04000A26 RID: 2598
		private long randomSeed;

		// Token: 0x04000A27 RID: 2599
		private long lastAllocated;
	}
}
