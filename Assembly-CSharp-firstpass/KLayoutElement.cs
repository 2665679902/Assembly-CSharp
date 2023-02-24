using System;
using UnityEngine.UI;

// Token: 0x0200005E RID: 94
public class KLayoutElement : LayoutElement
{
	// Token: 0x060003B2 RID: 946 RVA: 0x00013160 File Offset: 0x00011360
	protected override void OnEnable()
	{
		bool flag = this.makeDirtyOnDisable;
		if (!this.hasEnabledOnce)
		{
			this.hasEnabledOnce = true;
			flag = true;
		}
		if (flag)
		{
			base.OnEnable();
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0001318E File Offset: 0x0001138E
	protected override void OnDisable()
	{
		if (this.makeDirtyOnDisable)
		{
			base.OnDisable();
		}
	}

	// Token: 0x0400043A RID: 1082
	public bool makeDirtyOnDisable = true;

	// Token: 0x0400043B RID: 1083
	private bool hasEnabledOnce;
}
