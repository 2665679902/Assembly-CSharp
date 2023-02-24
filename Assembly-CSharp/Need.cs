using System;
using Klei.AI;

// Token: 0x0200085D RID: 2141
public abstract class Need : KMonoBehaviour
{
	// Token: 0x1700044D RID: 1101
	// (get) Token: 0x06003D92 RID: 15762 RVA: 0x001587CF File Offset: 0x001569CF
	// (set) Token: 0x06003D93 RID: 15763 RVA: 0x001587D7 File Offset: 0x001569D7
	public string Name { get; protected set; }

	// Token: 0x1700044E RID: 1102
	// (get) Token: 0x06003D94 RID: 15764 RVA: 0x001587E0 File Offset: 0x001569E0
	// (set) Token: 0x06003D95 RID: 15765 RVA: 0x001587E8 File Offset: 0x001569E8
	public string ExpectationTooltip { get; protected set; }

	// Token: 0x1700044F RID: 1103
	// (get) Token: 0x06003D96 RID: 15766 RVA: 0x001587F1 File Offset: 0x001569F1
	// (set) Token: 0x06003D97 RID: 15767 RVA: 0x001587F9 File Offset: 0x001569F9
	public string Tooltip { get; protected set; }

	// Token: 0x06003D98 RID: 15768 RVA: 0x00158802 File Offset: 0x00156A02
	public Klei.AI.Attribute GetExpectationAttribute()
	{
		return this.expectationAttribute.Attribute;
	}

	// Token: 0x06003D99 RID: 15769 RVA: 0x0015880F File Offset: 0x00156A0F
	protected void SetModifier(Need.ModifierType modifier)
	{
		if (this.currentStressModifier != modifier)
		{
			if (this.currentStressModifier != null)
			{
				this.UnapplyModifier(this.currentStressModifier);
			}
			if (modifier != null)
			{
				this.ApplyModifier(modifier);
			}
			this.currentStressModifier = modifier;
		}
	}

	// Token: 0x06003D9A RID: 15770 RVA: 0x00158840 File Offset: 0x00156A40
	private void ApplyModifier(Need.ModifierType modifier)
	{
		if (modifier.modifier != null)
		{
			this.GetAttributes().Add(modifier.modifier);
		}
		if (modifier.statusItem != null)
		{
			base.GetComponent<KSelectable>().AddStatusItem(modifier.statusItem, null);
		}
		if (modifier.thought != null)
		{
			this.GetSMI<ThoughtGraph.Instance>().AddThought(modifier.thought);
		}
	}

	// Token: 0x06003D9B RID: 15771 RVA: 0x0015889C File Offset: 0x00156A9C
	private void UnapplyModifier(Need.ModifierType modifier)
	{
		if (modifier.modifier != null)
		{
			this.GetAttributes().Remove(modifier.modifier);
		}
		if (modifier.statusItem != null)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(modifier.statusItem, false);
		}
		if (modifier.thought != null)
		{
			this.GetSMI<ThoughtGraph.Instance>().RemoveThought(modifier.thought);
		}
	}

	// Token: 0x0400286A RID: 10346
	protected AttributeInstance expectationAttribute;

	// Token: 0x0400286B RID: 10347
	protected Need.ModifierType stressBonus;

	// Token: 0x0400286C RID: 10348
	protected Need.ModifierType stressNeutral;

	// Token: 0x0400286D RID: 10349
	protected Need.ModifierType stressPenalty;

	// Token: 0x0400286E RID: 10350
	protected Need.ModifierType currentStressModifier;

	// Token: 0x0200161E RID: 5662
	protected class ModifierType
	{
		// Token: 0x0400691A RID: 26906
		public AttributeModifier modifier;

		// Token: 0x0400691B RID: 26907
		public StatusItem statusItem;

		// Token: 0x0400691C RID: 26908
		public Thought thought;
	}
}
