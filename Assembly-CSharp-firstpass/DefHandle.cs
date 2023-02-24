using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BB RID: 187
[Serializable]
public struct DefHandle
{
	// Token: 0x06000705 RID: 1797 RVA: 0x0001E487 File Offset: 0x0001C687
	public bool IsValid()
	{
		return this.defIdx > 0;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0001E494 File Offset: 0x0001C694
	public DefType Get<DefType>() where DefType : class, new()
	{
		if (this.defIdx == 0)
		{
			DefHandle.defs.Add(new DefType());
			this.defIdx = DefHandle.defs.Count;
		}
		return DefHandle.defs[this.defIdx - 1] as DefType;
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0001E4E9 File Offset: 0x0001C6E9
	public void Set<DefType>(DefType value) where DefType : class, new()
	{
		DefHandle.defs.Add(value);
		this.defIdx = DefHandle.defs.Count;
	}

	// Token: 0x040005D7 RID: 1495
	[SerializeField]
	private int defIdx;

	// Token: 0x040005D8 RID: 1496
	private static List<object> defs = new List<object>();
}
