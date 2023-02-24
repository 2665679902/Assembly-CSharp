using System;
using UnityEngine;

// Token: 0x02000541 RID: 1345
[AddComponentMenu("KMonoBehaviour/scripts/Achievements")]
public class Achievements : KMonoBehaviour
{
	// Token: 0x0600201C RID: 8220 RVA: 0x000AF85D File Offset: 0x000ADA5D
	public void Unlock(string id)
	{
		if (SteamAchievementService.Instance)
		{
			SteamAchievementService.Instance.Unlock(id);
		}
	}
}
