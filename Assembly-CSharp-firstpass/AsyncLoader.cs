using System;
using System.Collections.Generic;

// Token: 0x0200007E RID: 126
public abstract class AsyncLoader
{
	// Token: 0x060004FD RID: 1277 RVA: 0x000188D9 File Offset: 0x00016AD9
	public virtual Type[] GatherDependencies()
	{
		return null;
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x000188DC File Offset: 0x00016ADC
	public virtual void CollectLoaders(List<AsyncLoader> loaders)
	{
	}

	// Token: 0x060004FF RID: 1279
	public abstract void Run();
}
