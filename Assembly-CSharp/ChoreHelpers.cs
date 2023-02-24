using System;
using UnityEngine;

// Token: 0x0200037B RID: 891
public static class ChoreHelpers
{
	// Token: 0x06001230 RID: 4656 RVA: 0x00061235 File Offset: 0x0005F435
	public static GameObject CreateLocator(string name, Vector3 pos)
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(ApproachableLocator.ID), null, null);
		gameObject.name = name;
		gameObject.transform.SetPosition(pos);
		gameObject.gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x0006126D File Offset: 0x0005F46D
	public static GameObject CreateSleepLocator(Vector3 pos)
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(SleepLocator.ID), null, null);
		gameObject.name = "SLeepLocator";
		gameObject.transform.SetPosition(pos);
		gameObject.gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x000612A9 File Offset: 0x0005F4A9
	public static void DestroyLocator(GameObject locator)
	{
		if (locator != null)
		{
			locator.gameObject.DeleteObject();
		}
	}
}
