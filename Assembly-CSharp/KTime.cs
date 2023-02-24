using System;
using UnityEngine;

// Token: 0x020007EE RID: 2030
[AddComponentMenu("KMonoBehaviour/scripts/KTime")]
public class KTime : KMonoBehaviour
{
	// Token: 0x1700042A RID: 1066
	// (get) Token: 0x06003A8A RID: 14986 RVA: 0x0014450F File Offset: 0x0014270F
	// (set) Token: 0x06003A8B RID: 14987 RVA: 0x00144517 File Offset: 0x00142717
	public float UnscaledGameTime { get; set; }

	// Token: 0x1700042B RID: 1067
	// (get) Token: 0x06003A8C RID: 14988 RVA: 0x00144520 File Offset: 0x00142720
	// (set) Token: 0x06003A8D RID: 14989 RVA: 0x00144527 File Offset: 0x00142727
	public static KTime Instance { get; private set; }

	// Token: 0x06003A8E RID: 14990 RVA: 0x0014452F File Offset: 0x0014272F
	public static void DestroyInstance()
	{
		KTime.Instance = null;
	}

	// Token: 0x06003A8F RID: 14991 RVA: 0x00144537 File Offset: 0x00142737
	protected override void OnPrefabInit()
	{
		KTime.Instance = this;
		this.UnscaledGameTime = Time.unscaledTime;
	}

	// Token: 0x06003A90 RID: 14992 RVA: 0x0014454A File Offset: 0x0014274A
	protected override void OnCleanUp()
	{
		KTime.Instance = null;
	}

	// Token: 0x06003A91 RID: 14993 RVA: 0x00144552 File Offset: 0x00142752
	public void Update()
	{
		if (!SpeedControlScreen.Instance.IsPaused)
		{
			this.UnscaledGameTime += Time.unscaledDeltaTime;
		}
	}
}
