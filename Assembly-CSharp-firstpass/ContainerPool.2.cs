using System;
using System.Collections.Generic;

// Token: 0x0200008F RID: 143
public class ContainerPool<ContainerType, PoolIdentifier> : ContainerPool where ContainerType : new()
{
	// Token: 0x0600057B RID: 1403 RVA: 0x0001A594 File Offset: 0x00018794
	public ContainerType Allocate()
	{
		Stack<ContainerType> stack = this.freeContainers;
		ContainerType containerType;
		lock (stack)
		{
			if (this.freeContainers.Count == 0)
			{
				containerType = new ContainerType();
			}
			else
			{
				containerType = this.freeContainers.Pop();
			}
		}
		return containerType;
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x0001A5F0 File Offset: 0x000187F0
	public void Free(ContainerType container)
	{
		Stack<ContainerType> stack = this.freeContainers;
		lock (stack)
		{
			this.freeContainers.Push(container);
		}
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x0001A638 File Offset: 0x00018838
	public override string GetName()
	{
		return typeof(PoolIdentifier).Name + "." + typeof(ContainerType).Name;
	}

	// Token: 0x04000543 RID: 1347
	private Stack<ContainerType> freeContainers = new Stack<ContainerType>();
}
