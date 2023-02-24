using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000210 RID: 528
	internal class RecursionLevel
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x000419EC File Offset: 0x0003FBEC
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x000419F4 File Offset: 0x0003FBF4
		public int Maximum { get; private set; }

		// Token: 0x06001035 RID: 4149 RVA: 0x000419FD File Offset: 0x0003FBFD
		public RecursionLevel(int maximum)
		{
			this.Maximum = maximum;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00041A0C File Offset: 0x0003FC0C
		public void Increment()
		{
			if (!this.TryIncrement())
			{
				throw new MaximumRecursionLevelReachedException();
			}
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00041A1C File Offset: 0x0003FC1C
		public bool TryIncrement()
		{
			if (this.current < this.Maximum)
			{
				this.current++;
				return true;
			}
			return false;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00041A3D File Offset: 0x0003FC3D
		public void Decrement()
		{
			if (this.current == 0)
			{
				throw new InvalidOperationException("Attempted to decrement RecursionLevel to a negative value");
			}
			this.current--;
		}

		// Token: 0x040008E7 RID: 2279
		private int current;
	}
}
