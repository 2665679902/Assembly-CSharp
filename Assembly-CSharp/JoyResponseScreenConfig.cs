using System;
using Database;
using UnityEngine;

// Token: 0x02000AC2 RID: 2754
public readonly struct JoyResponseScreenConfig
{
	// Token: 0x0600543C RID: 21564 RVA: 0x001E94A2 File Offset: 0x001E76A2
	private JoyResponseScreenConfig(JoyResponseOutfitTarget target, Option<JoyResponseDesignerScreen.GalleryItem> initalSelectedItem)
	{
		this.target = target;
		this.initalSelectedItem = initalSelectedItem;
		this.isValid = true;
	}

	// Token: 0x0600543D RID: 21565 RVA: 0x001E94B9 File Offset: 0x001E76B9
	public JoyResponseScreenConfig WithInitialSelection(Option<BalloonArtistFacadeResource> initialSelectedItem)
	{
		return new JoyResponseScreenConfig(this.target, JoyResponseDesignerScreen.GalleryItem.Of(initialSelectedItem));
	}

	// Token: 0x0600543E RID: 21566 RVA: 0x001E94D1 File Offset: 0x001E76D1
	public static JoyResponseScreenConfig Minion(GameObject minionInstance)
	{
		return new JoyResponseScreenConfig(JoyResponseOutfitTarget.FromMinion(minionInstance), Option.None);
	}

	// Token: 0x0600543F RID: 21567 RVA: 0x001E94E8 File Offset: 0x001E76E8
	public static JoyResponseScreenConfig Personality(Personality personality)
	{
		return new JoyResponseScreenConfig(JoyResponseOutfitTarget.FromPersonality(personality), Option.None);
	}

	// Token: 0x06005440 RID: 21568 RVA: 0x001E9500 File Offset: 0x001E7700
	public static JoyResponseScreenConfig From(MinionBrowserScreen.GridItem item)
	{
		MinionBrowserScreen.GridItem.PersonalityTarget personalityTarget = item as MinionBrowserScreen.GridItem.PersonalityTarget;
		if (personalityTarget != null)
		{
			return JoyResponseScreenConfig.Personality(personalityTarget.personality);
		}
		MinionBrowserScreen.GridItem.MinionInstanceTarget minionInstanceTarget = item as MinionBrowserScreen.GridItem.MinionInstanceTarget;
		if (minionInstanceTarget != null)
		{
			return JoyResponseScreenConfig.Minion(minionInstanceTarget.minionInstance);
		}
		throw new NotImplementedException();
	}

	// Token: 0x06005441 RID: 21569 RVA: 0x001E953E File Offset: 0x001E773E
	public void ApplyAndOpenScreen()
	{
		LockerNavigator.Instance.joyResponseDesignerScreen.GetComponent<JoyResponseDesignerScreen>().Configure(this);
		LockerNavigator.Instance.PushScreen(LockerNavigator.Instance.joyResponseDesignerScreen, null);
	}

	// Token: 0x04003943 RID: 14659
	public readonly JoyResponseOutfitTarget target;

	// Token: 0x04003944 RID: 14660
	public readonly Option<JoyResponseDesignerScreen.GalleryItem> initalSelectedItem;

	// Token: 0x04003945 RID: 14661
	public readonly bool isValid;
}
