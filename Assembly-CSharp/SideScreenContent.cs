using System;
using UnityEngine;

// Token: 0x02000BDF RID: 3039
public abstract class SideScreenContent : KScreen
{
	// Token: 0x06005FC4 RID: 24516 RVA: 0x0023130A File Offset: 0x0022F50A
	public virtual void SetTarget(GameObject target)
	{
	}

	// Token: 0x06005FC5 RID: 24517 RVA: 0x0023130C File Offset: 0x0022F50C
	public virtual void ClearTarget()
	{
	}

	// Token: 0x06005FC6 RID: 24518
	public abstract bool IsValidForTarget(GameObject target);

	// Token: 0x06005FC7 RID: 24519 RVA: 0x0023130E File Offset: 0x0022F50E
	public virtual int GetSideScreenSortOrder()
	{
		return 0;
	}

	// Token: 0x06005FC8 RID: 24520 RVA: 0x00231311 File Offset: 0x0022F511
	public virtual string GetTitle()
	{
		return Strings.Get(this.titleKey);
	}

	// Token: 0x0400419F RID: 16799
	[SerializeField]
	protected string titleKey;

	// Token: 0x040041A0 RID: 16800
	public GameObject ContentContainer;
}
