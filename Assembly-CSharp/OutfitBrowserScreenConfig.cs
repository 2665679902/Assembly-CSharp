using System;
using UnityEngine;

// Token: 0x02000B4B RID: 2891
public readonly struct OutfitBrowserScreenConfig
{
	// Token: 0x06005994 RID: 22932 RVA: 0x00206496 File Offset: 0x00204696
	public OutfitBrowserScreenConfig(Option<ClothingOutfitTarget> selectedTarget, Option<Personality> minionPersonality, Option<GameObject> minionInstance)
	{
		this.selectedTarget = selectedTarget;
		this.minionPersonality = minionPersonality;
		this.isPickingOutfitForDupe = minionPersonality.HasValue || minionInstance.HasValue;
		this.targetMinionInstance = minionInstance;
		this.isValid = true;
	}

	// Token: 0x06005995 RID: 22933 RVA: 0x002064CD File Offset: 0x002046CD
	public OutfitBrowserScreenConfig WithOutfit(Option<ClothingOutfitTarget> sourceTarget)
	{
		return new OutfitBrowserScreenConfig(sourceTarget, this.minionPersonality, this.targetMinionInstance);
	}

	// Token: 0x06005996 RID: 22934 RVA: 0x002064E4 File Offset: 0x002046E4
	public string GetMinionName()
	{
		if (this.targetMinionInstance.HasValue)
		{
			return this.targetMinionInstance.Value.GetProperName();
		}
		if (this.minionPersonality.HasValue)
		{
			return this.minionPersonality.Value.Name;
		}
		return "-";
	}

	// Token: 0x06005997 RID: 22935 RVA: 0x00206532 File Offset: 0x00204732
	public static OutfitBrowserScreenConfig Mannequin()
	{
		return new OutfitBrowserScreenConfig(Option.None, Option.None, Option.None);
	}

	// Token: 0x06005998 RID: 22936 RVA: 0x00206557 File Offset: 0x00204757
	public static OutfitBrowserScreenConfig Minion(Personality personality)
	{
		return new OutfitBrowserScreenConfig(Option.None, personality, Option.None);
	}

	// Token: 0x06005999 RID: 22937 RVA: 0x00206578 File Offset: 0x00204778
	public static OutfitBrowserScreenConfig Minion(GameObject minionInstance)
	{
		Personality personality = Db.Get().Personalities.Get(minionInstance.GetComponent<MinionIdentity>().personalityResourceId);
		return new OutfitBrowserScreenConfig(ClothingOutfitTarget.FromMinion(minionInstance), personality, minionInstance);
	}

	// Token: 0x0600599A RID: 22938 RVA: 0x002065BC File Offset: 0x002047BC
	public static OutfitBrowserScreenConfig Minion(MinionBrowserScreen.GridItem item)
	{
		MinionBrowserScreen.GridItem.PersonalityTarget personalityTarget = item as MinionBrowserScreen.GridItem.PersonalityTarget;
		if (personalityTarget != null)
		{
			return OutfitBrowserScreenConfig.Minion(personalityTarget.personality);
		}
		MinionBrowserScreen.GridItem.MinionInstanceTarget minionInstanceTarget = item as MinionBrowserScreen.GridItem.MinionInstanceTarget;
		if (minionInstanceTarget != null)
		{
			return OutfitBrowserScreenConfig.Minion(minionInstanceTarget.minionInstance);
		}
		throw new NotImplementedException();
	}

	// Token: 0x0600599B RID: 22939 RVA: 0x002065FA File Offset: 0x002047FA
	public void ApplyAndOpenScreen()
	{
		LockerNavigator.Instance.outfitBrowserScreen.GetComponent<OutfitBrowserScreen>().Configure(this);
		LockerNavigator.Instance.PushScreen(LockerNavigator.Instance.outfitBrowserScreen, null);
	}

	// Token: 0x04003C83 RID: 15491
	public readonly Option<ClothingOutfitTarget> selectedTarget;

	// Token: 0x04003C84 RID: 15492
	public readonly Option<Personality> minionPersonality;

	// Token: 0x04003C85 RID: 15493
	public readonly Option<GameObject> targetMinionInstance;

	// Token: 0x04003C86 RID: 15494
	public readonly bool isValid;

	// Token: 0x04003C87 RID: 15495
	public readonly bool isPickingOutfitForDupe;
}
