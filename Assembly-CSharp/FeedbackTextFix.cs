using System;
using Steamworks;
using UnityEngine;

// Token: 0x02000A9C RID: 2716
public class FeedbackTextFix : MonoBehaviour
{
	// Token: 0x06005342 RID: 21314 RVA: 0x001E36FC File Offset: 0x001E18FC
	private void Awake()
	{
		if (!DistributionPlatform.Initialized || !SteamUtils.IsSteamRunningOnSteamDeck())
		{
			UnityEngine.Object.DestroyImmediate(this);
			return;
		}
		this.locText.key = this.newKey;
	}

	// Token: 0x0400386F RID: 14447
	public string newKey;

	// Token: 0x04003870 RID: 14448
	public LocText locText;
}
