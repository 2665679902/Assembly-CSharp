using System;
using UnityEngine;

// Token: 0x020006E1 RID: 1761
[AddComponentMenu("KMonoBehaviour/scripts/NotCapturable")]
public class NotCapturable : KMonoBehaviour
{
	// Token: 0x06002FE7 RID: 12263 RVA: 0x000FD106 File Offset: 0x000FB306
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (base.GetComponent<Capturable>() != null)
		{
			DebugUtil.LogErrorArgs(this, new object[] { "Entity has both Capturable and NotCapturable!" });
		}
		Components.NotCapturables.Add(this);
	}

	// Token: 0x06002FE8 RID: 12264 RVA: 0x000FD13B File Offset: 0x000FB33B
	protected override void OnCleanUp()
	{
		Components.NotCapturables.Remove(this);
		base.OnCleanUp();
	}
}
