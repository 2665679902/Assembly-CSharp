using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000809 RID: 2057
public static class LoreBearerUtil
{
	// Token: 0x06003B95 RID: 15253 RVA: 0x0014A3F5 File Offset: 0x001485F5
	public static void AddLoreTo(GameObject prefabOrGameObject)
	{
		prefabOrGameObject.AddOrGet<LoreBearer>();
	}

	// Token: 0x06003B96 RID: 15254 RVA: 0x0014A400 File Offset: 0x00148600
	public static void AddLoreTo(GameObject prefabOrGameObject, LoreBearerAction unlockLoreFn)
	{
		KPrefabID component = prefabOrGameObject.GetComponent<KPrefabID>();
		if (component.IsInitialized())
		{
			prefabOrGameObject.AddOrGet<LoreBearer>().Internal_SetContent(unlockLoreFn);
			return;
		}
		prefabOrGameObject.AddComponent<LoreBearer>();
		component.prefabInitFn += delegate(GameObject gameObject)
		{
			gameObject.GetComponent<LoreBearer>().Internal_SetContent(unlockLoreFn);
		};
	}

	// Token: 0x06003B97 RID: 15255 RVA: 0x0014A455 File Offset: 0x00148655
	public static LoreBearerAction UnlockSpecificEntry(string unlockId, string searchDisplayText)
	{
		return delegate(InfoDialogScreen screen)
		{
			Game.Instance.unlocks.Unlock(unlockId, true);
			screen.AddPlainText(searchDisplayText);
			screen.AddOption(UI.USERMENUACTIONS.READLORE.GOTODATABASE, LoreBearerUtil.OpenCodexByLockKeyID(unlockId, false), false);
		};
	}

	// Token: 0x06003B98 RID: 15256 RVA: 0x0014A478 File Offset: 0x00148678
	public static void UnlockNextEmail(InfoDialogScreen screen)
	{
		string text = Game.Instance.unlocks.UnlockNext("emails");
		if (text != null)
		{
			string text2 = "SEARCH" + UnityEngine.Random.Range(1, 6).ToString();
			screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_COMPUTER_SUCCESS." + text2));
			screen.AddOption(UI.USERMENUACTIONS.READLORE.GOTODATABASE, LoreBearerUtil.OpenCodexByLockKeyID(text, false), false);
			return;
		}
		string text3 = "SEARCH" + UnityEngine.Random.Range(1, 8).ToString();
		screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_COMPUTER_FAIL." + text3));
	}

	// Token: 0x06003B99 RID: 15257 RVA: 0x0014A524 File Offset: 0x00148724
	public static void UnlockNextResearchNote(InfoDialogScreen screen)
	{
		string text = Game.Instance.unlocks.UnlockNext("researchnotes");
		if (text != null)
		{
			string text2 = "SEARCH" + UnityEngine.Random.Range(1, 3).ToString();
			screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_TECHNOLOGY_SUCCESS." + text2));
			screen.AddOption(UI.USERMENUACTIONS.READLORE.GOTODATABASE, LoreBearerUtil.OpenCodexByLockKeyID(text, false), false);
			return;
		}
		string text3 = "SEARCH1";
		screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_OBJECT_FAIL." + text3));
	}

	// Token: 0x06003B9A RID: 15258 RVA: 0x0014A5BC File Offset: 0x001487BC
	public static void UnlockNextJournalEntry(InfoDialogScreen screen)
	{
		string text = Game.Instance.unlocks.UnlockNext("journals");
		if (text != null)
		{
			string text2 = "SEARCH" + UnityEngine.Random.Range(1, 6).ToString();
			screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_OBJECT_SUCCESS." + text2));
			screen.AddOption(UI.USERMENUACTIONS.READLORE.GOTODATABASE, LoreBearerUtil.OpenCodexByLockKeyID(text, false), false);
			return;
		}
		string text3 = "SEARCH1";
		screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_OBJECT_FAIL." + text3));
	}

	// Token: 0x06003B9B RID: 15259 RVA: 0x0014A654 File Offset: 0x00148854
	public static void UnlockNextDimensionalLore(InfoDialogScreen screen)
	{
		string text = Game.Instance.unlocks.UnlockNext("dimensionallore");
		if (text != null)
		{
			string text2 = "SEARCH" + UnityEngine.Random.Range(1, 6).ToString();
			screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_OBJECT_SUCCESS." + text2));
			screen.AddOption(UI.USERMENUACTIONS.READLORE.GOTODATABASE, LoreBearerUtil.OpenCodexByLockKeyID(text, false), false);
			return;
		}
		string text3 = "SEARCH1";
		screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_OBJECT_FAIL." + text3));
	}

	// Token: 0x06003B9C RID: 15260 RVA: 0x0014A6EC File Offset: 0x001488EC
	public static void UnlockNextSpaceEntry(InfoDialogScreen screen)
	{
		string text = Game.Instance.unlocks.UnlockNext("space");
		if (text != null)
		{
			string text2 = "SEARCH" + UnityEngine.Random.Range(1, 7).ToString();
			screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_SPACEPOI_SUCCESS." + text2));
			screen.AddOption(UI.USERMENUACTIONS.READLORE.GOTODATABASE, LoreBearerUtil.OpenCodexByLockKeyID(text, false), false);
			return;
		}
		string text3 = "SEARCH" + UnityEngine.Random.Range(1, 4).ToString();
		screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_SPACEPOI_FAIL." + text3));
	}

	// Token: 0x06003B9D RID: 15261 RVA: 0x0014A798 File Offset: 0x00148998
	public static void UnlockNextDeskPodiumEntry(InfoDialogScreen screen)
	{
		if (!Game.Instance.unlocks.IsUnlocked("story_trait_critter_manipulator_parking"))
		{
			Game.Instance.unlocks.Unlock("story_trait_critter_manipulator_parking", true);
			string text = "SEARCH" + UnityEngine.Random.Range(1, 1).ToString();
			screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_COMPUTER_PODIUM." + text));
			screen.AddOption(UI.USERMENUACTIONS.READLORE.GOTODATABASE, LoreBearerUtil.OpenCodexByLockKeyID("story_trait_critter_manipulator_parking", false), false);
			return;
		}
		string text2 = "SEARCH" + UnityEngine.Random.Range(1, 8).ToString();
		screen.AddPlainText(Strings.Get("STRINGS.UI.USERMENUACTIONS.READLORE.SEARCH_COMPUTER_FAIL." + text2));
	}

	// Token: 0x06003B9E RID: 15262 RVA: 0x0014A85A File Offset: 0x00148A5A
	public static void NerualVacillator(InfoDialogScreen screen)
	{
		Game.Instance.unlocks.Unlock("neuralvacillator", true);
		LoreBearerUtil.UnlockNextResearchNote(screen);
	}

	// Token: 0x06003B9F RID: 15263 RVA: 0x0014A877 File Offset: 0x00148A77
	public static Action<InfoDialogScreen> OpenCodexByLockKeyID(string key, bool focusContent = false)
	{
		return delegate(InfoDialogScreen dialog)
		{
			dialog.Deactivate();
			string entryForLock = CodexCache.GetEntryForLock(key);
			if (entryForLock == null)
			{
				DebugUtil.DevLogError("Missing codex entry for lock: " + key);
				return;
			}
			ContentContainer contentContainer = null;
			if (focusContent)
			{
				CodexEntry codexEntry = CodexCache.FindEntry(entryForLock);
				int num = 0;
				while (contentContainer == null && num < codexEntry.contentContainers.Count)
				{
					if (!(codexEntry.contentContainers[num].lockID != key))
					{
						contentContainer = codexEntry.contentContainers[num];
					}
					num++;
				}
			}
			ManagementMenu.Instance.OpenCodexToEntry(entryForLock, contentContainer);
		};
	}

	// Token: 0x06003BA0 RID: 15264 RVA: 0x0014A897 File Offset: 0x00148A97
	public static Action<InfoDialogScreen> OpenCodexByEntryID(string id)
	{
		return delegate(InfoDialogScreen dialog)
		{
			dialog.Deactivate();
			ManagementMenu.Instance.OpenCodexToEntry(id, null);
		};
	}
}
