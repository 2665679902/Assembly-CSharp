using System;
using UnityEngine;

// Token: 0x02000BF5 RID: 3061
public class SimpleInfoPanel
{
	// Token: 0x060060BC RID: 24764 RVA: 0x00237A1A File Offset: 0x00235C1A
	public SimpleInfoPanel(SimpleInfoScreen simpleInfoRoot)
	{
		this.simpleInfoRoot = simpleInfoRoot;
	}

	// Token: 0x060060BD RID: 24765 RVA: 0x00237A29 File Offset: 0x00235C29
	public virtual void Refresh(CollapsibleDetailContentPanel panel, GameObject selectedTarget)
	{
	}

	// Token: 0x0400426D RID: 17005
	protected SimpleInfoScreen simpleInfoRoot;
}
