using System;
using UnityEngine;

// Token: 0x02000A82 RID: 2690
public class DebugElementMenu : KButtonMenu
{
	// Token: 0x06005258 RID: 21080 RVA: 0x001DC133 File Offset: 0x001DA333
	protected override void OnPrefabInit()
	{
		DebugElementMenu.Instance = this;
		base.OnPrefabInit();
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005259 RID: 21081 RVA: 0x001DC148 File Offset: 0x001DA348
	protected override void OnForcedCleanUp()
	{
		DebugElementMenu.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x0600525A RID: 21082 RVA: 0x001DC156 File Offset: 0x001DA356
	public void Turnoff()
	{
		this.root.gameObject.SetActive(false);
	}

	// Token: 0x0400378C RID: 14220
	public static DebugElementMenu Instance;

	// Token: 0x0400378D RID: 14221
	public GameObject root;
}
