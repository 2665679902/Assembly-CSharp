using System;
using UnityEngine;

// Token: 0x02000B4E RID: 2894
public readonly struct OutfitDesignerScreenConfig
{
	// Token: 0x060059BF RID: 22975 RVA: 0x00207700 File Offset: 0x00205900
	public OutfitDesignerScreenConfig(Option<ClothingOutfitTarget> sourceTargetOpt, Option<Personality> minionPersonality, Option<GameObject> targetMinionInstance, Action<ClothingOutfitTarget> onWriteToOutfitTargetFn = null)
	{
		this.sourceTarget = (sourceTargetOpt.HasValue ? sourceTargetOpt.Value : ClothingOutfitTarget.ForNewOutfit());
		this.outfitTemplate = (this.sourceTarget.IsTemplateOutfit() ? Option.Some<ClothingOutfitTarget>(this.sourceTarget) : Option.None);
		this.minionPersonality = minionPersonality;
		this.targetMinionInstance = targetMinionInstance;
		this.onWriteToOutfitTargetFn = onWriteToOutfitTargetFn;
		this.isValid = true;
		ClothingOutfitTarget.MinionInstance minionInstance;
		if (this.sourceTarget.Is<ClothingOutfitTarget.MinionInstance>(out minionInstance))
		{
			global::Debug.Assert(targetMinionInstance.HasValue && targetMinionInstance == minionInstance.minionInstance);
		}
	}

	// Token: 0x060059C0 RID: 22976 RVA: 0x0020779D File Offset: 0x0020599D
	public OutfitDesignerScreenConfig WithOutfit(Option<ClothingOutfitTarget> sourceTarget)
	{
		return new OutfitDesignerScreenConfig(sourceTarget, this.minionPersonality, this.targetMinionInstance, this.onWriteToOutfitTargetFn);
	}

	// Token: 0x060059C1 RID: 22977 RVA: 0x002077B7 File Offset: 0x002059B7
	public OutfitDesignerScreenConfig OnWriteToOutfitTarget(Action<ClothingOutfitTarget> onWriteToOutfitTargetFn)
	{
		return new OutfitDesignerScreenConfig(this.sourceTarget, this.minionPersonality, this.targetMinionInstance, onWriteToOutfitTargetFn);
	}

	// Token: 0x060059C2 RID: 22978 RVA: 0x002077D6 File Offset: 0x002059D6
	public static OutfitDesignerScreenConfig Mannequin(Option<ClothingOutfitTarget> outfit)
	{
		return new OutfitDesignerScreenConfig(outfit, Option.None, Option.None, null);
	}

	// Token: 0x060059C3 RID: 22979 RVA: 0x002077F3 File Offset: 0x002059F3
	public static OutfitDesignerScreenConfig Minion(Option<ClothingOutfitTarget> outfit, Personality personality)
	{
		return new OutfitDesignerScreenConfig(outfit, personality, Option.None, null);
	}

	// Token: 0x060059C4 RID: 22980 RVA: 0x0020780C File Offset: 0x00205A0C
	public static OutfitDesignerScreenConfig Minion(Option<ClothingOutfitTarget> outfit, GameObject targetMinionInstance)
	{
		Personality personality = Db.Get().Personalities.Get(targetMinionInstance.GetComponent<MinionIdentity>().personalityResourceId);
		return new OutfitDesignerScreenConfig(outfit.HasValue ? outfit.Value : ClothingOutfitTarget.FromMinion(targetMinionInstance), personality, targetMinionInstance, null);
	}

	// Token: 0x060059C5 RID: 22981 RVA: 0x00207864 File Offset: 0x00205A64
	public static OutfitDesignerScreenConfig Minion(Option<ClothingOutfitTarget> outfit, MinionBrowserScreen.GridItem item)
	{
		MinionBrowserScreen.GridItem.PersonalityTarget personalityTarget = item as MinionBrowserScreen.GridItem.PersonalityTarget;
		if (personalityTarget != null)
		{
			return OutfitDesignerScreenConfig.Minion(outfit, personalityTarget.personality);
		}
		MinionBrowserScreen.GridItem.MinionInstanceTarget minionInstanceTarget = item as MinionBrowserScreen.GridItem.MinionInstanceTarget;
		if (minionInstanceTarget != null)
		{
			return OutfitDesignerScreenConfig.Minion(outfit, minionInstanceTarget.minionInstance);
		}
		throw new NotImplementedException();
	}

	// Token: 0x060059C6 RID: 22982 RVA: 0x002078A4 File Offset: 0x00205AA4
	public void ApplyAndOpenScreen()
	{
		LockerNavigator.Instance.outfitDesignerScreen.GetComponent<OutfitDesignerScreen>().Configure(this);
		LockerNavigator.Instance.PushScreen(LockerNavigator.Instance.outfitDesignerScreen, null);
	}

	// Token: 0x04003CA4 RID: 15524
	public readonly ClothingOutfitTarget sourceTarget;

	// Token: 0x04003CA5 RID: 15525
	public readonly Option<ClothingOutfitTarget> outfitTemplate;

	// Token: 0x04003CA6 RID: 15526
	public readonly Option<Personality> minionPersonality;

	// Token: 0x04003CA7 RID: 15527
	public readonly Option<GameObject> targetMinionInstance;

	// Token: 0x04003CA8 RID: 15528
	public readonly Action<ClothingOutfitTarget> onWriteToOutfitTargetFn;

	// Token: 0x04003CA9 RID: 15529
	public readonly bool isValid;
}
