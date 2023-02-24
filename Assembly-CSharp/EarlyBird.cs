using System;
using Klei.AI;
using STRINGS;
using TUNING;

// Token: 0x0200072D RID: 1837
[SkipSaveFileSerialization]
public class EarlyBird : StateMachineComponent<EarlyBird.StatesInstance>
{
	// Token: 0x06003258 RID: 12888 RVA: 0x0010D50C File Offset: 0x0010B70C
	protected override void OnSpawn()
	{
		this.attributeModifiers = new AttributeModifier[]
		{
			new AttributeModifier("Construction", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true),
			new AttributeModifier("Digging", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true),
			new AttributeModifier("Machinery", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true),
			new AttributeModifier("Athletics", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true),
			new AttributeModifier("Learning", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true),
			new AttributeModifier("Cooking", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true),
			new AttributeModifier("Art", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true),
			new AttributeModifier("Strength", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true),
			new AttributeModifier("Caring", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true),
			new AttributeModifier("Botanist", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true),
			new AttributeModifier("Ranching", TRAITS.EARLYBIRD_MODIFIER, DUPLICANTS.TRAITS.EARLYBIRD.NAME, false, false, true)
		};
		base.smi.StartSM();
	}

	// Token: 0x06003259 RID: 12889 RVA: 0x0010D688 File Offset: 0x0010B888
	public void ApplyModifiers()
	{
		Attributes attributes = base.gameObject.GetAttributes();
		for (int i = 0; i < this.attributeModifiers.Length; i++)
		{
			AttributeModifier attributeModifier = this.attributeModifiers[i];
			attributes.Add(attributeModifier);
		}
	}

	// Token: 0x0600325A RID: 12890 RVA: 0x0010D6C4 File Offset: 0x0010B8C4
	public void RemoveModifiers()
	{
		Attributes attributes = base.gameObject.GetAttributes();
		for (int i = 0; i < this.attributeModifiers.Length; i++)
		{
			AttributeModifier attributeModifier = this.attributeModifiers[i];
			attributes.Remove(attributeModifier);
		}
	}

	// Token: 0x04001E96 RID: 7830
	[MyCmpReq]
	private KPrefabID kPrefabID;

	// Token: 0x04001E97 RID: 7831
	private AttributeModifier[] attributeModifiers;

	// Token: 0x0200142F RID: 5167
	public class StatesInstance : GameStateMachine<EarlyBird.States, EarlyBird.StatesInstance, EarlyBird, object>.GameInstance
	{
		// Token: 0x0600804D RID: 32845 RVA: 0x002DEE80 File Offset: 0x002DD080
		public StatesInstance(EarlyBird master)
			: base(master)
		{
		}

		// Token: 0x0600804E RID: 32846 RVA: 0x002DEE89 File Offset: 0x002DD089
		public bool IsMorning()
		{
			return !(ScheduleManager.Instance == null) && !(base.master.kPrefabID.PrefabTag == GameTags.MinionSelectPreview) && global::Schedule.GetBlockIdx() < TRAITS.EARLYBIRD_SCHEDULEBLOCK;
		}
	}

	// Token: 0x02001430 RID: 5168
	public class States : GameStateMachine<EarlyBird.States, EarlyBird.StatesInstance, EarlyBird>
	{
		// Token: 0x0600804F RID: 32847 RVA: 0x002DEEC4 File Offset: 0x002DD0C4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.TagTransition(GameTags.Dead, null, false);
			this.idle.Transition(this.early, (EarlyBird.StatesInstance smi) => smi.IsMorning(), UpdateRate.SIM_200ms);
			this.early.Enter("Morning", delegate(EarlyBird.StatesInstance smi)
			{
				smi.master.ApplyModifiers();
			}).Exit("NotMorning", delegate(EarlyBird.StatesInstance smi)
			{
				smi.master.RemoveModifiers();
			}).ToggleStatusItem(Db.Get().DuplicantStatusItems.EarlyMorning, null)
				.ToggleExpression(Db.Get().Expressions.Happy, null)
				.Transition(this.idle, (EarlyBird.StatesInstance smi) => !smi.IsMorning(), UpdateRate.SIM_200ms);
		}

		// Token: 0x040062B6 RID: 25270
		public GameStateMachine<EarlyBird.States, EarlyBird.StatesInstance, EarlyBird, object>.State idle;

		// Token: 0x040062B7 RID: 25271
		public GameStateMachine<EarlyBird.States, EarlyBird.StatesInstance, EarlyBird, object>.State early;
	}
}
