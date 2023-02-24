using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000B2A RID: 2858
public readonly struct MinionBrowserScreenConfig
{
	// Token: 0x06005829 RID: 22569 RVA: 0x001FE9EE File Offset: 0x001FCBEE
	public MinionBrowserScreenConfig(MinionBrowserScreen.GridItem[] items, Option<MinionBrowserScreen.GridItem> defaultSelectedItem)
	{
		this.items = items;
		this.defaultSelectedItem = defaultSelectedItem;
		this.isValid = true;
	}

	// Token: 0x0600582A RID: 22570 RVA: 0x001FEA08 File Offset: 0x001FCC08
	public static MinionBrowserScreenConfig Personalities(Option<Personality> defaultSelectedPersonality = default(Option<Personality>))
	{
		MinionBrowserScreen.GridItem.PersonalityTarget[] items = (from personality in Db.Get().Personalities.GetAll(true, false)
			select MinionBrowserScreen.GridItem.Of(personality)).ToArray<MinionBrowserScreen.GridItem.PersonalityTarget>();
		Option<MinionBrowserScreen.GridItem> option = defaultSelectedPersonality.AndThen<MinionBrowserScreen.GridItem>((Personality personality) => items.FirstOrDefault((MinionBrowserScreen.GridItem.PersonalityTarget item) => item.personality == personality));
		if (option.IsNone() && items.Length != 0)
		{
			option = items[0];
		}
		MinionBrowserScreen.GridItem[] array = items;
		return new MinionBrowserScreenConfig(array, option);
	}

	// Token: 0x0600582B RID: 22571 RVA: 0x001FEAA0 File Offset: 0x001FCCA0
	public static MinionBrowserScreenConfig MinionInstances(Option<GameObject> defaultSelectedMinionInstance = default(Option<GameObject>))
	{
		MinionBrowserScreen.GridItem.MinionInstanceTarget[] items = (from minionIdentity in UnityEngine.Object.FindObjectsOfType<MinionIdentity>()
			select MinionBrowserScreen.GridItem.Of(minionIdentity.gameObject)).ToArray<MinionBrowserScreen.GridItem.MinionInstanceTarget>();
		Option<MinionBrowserScreen.GridItem> option = defaultSelectedMinionInstance.AndThen<MinionBrowserScreen.GridItem>((GameObject minionInstance) => items.FirstOrDefault((MinionBrowserScreen.GridItem.MinionInstanceTarget item) => item.minionInstance == minionInstance));
		if (option.IsNone() && items.Length != 0)
		{
			option = items[0];
		}
		MinionBrowserScreen.GridItem[] array = items;
		return new MinionBrowserScreenConfig(array, option);
	}

	// Token: 0x0600582C RID: 22572 RVA: 0x001FEB29 File Offset: 0x001FCD29
	public void ApplyAndOpenScreen(System.Action onClose = null)
	{
		LockerNavigator.Instance.duplicantCatalogueScreen.GetComponent<MinionBrowserScreen>().Configure(this);
		LockerNavigator.Instance.PushScreen(LockerNavigator.Instance.duplicantCatalogueScreen, onClose);
	}

	// Token: 0x04003BA0 RID: 15264
	public readonly MinionBrowserScreen.GridItem[] items;

	// Token: 0x04003BA1 RID: 15265
	public readonly Option<MinionBrowserScreen.GridItem> defaultSelectedItem;

	// Token: 0x04003BA2 RID: 15266
	public readonly bool isValid;
}
