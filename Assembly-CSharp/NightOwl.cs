using System;
using Klei.AI;
using STRINGS;
using TUNING;

// Token: 0x02000867 RID: 2151
[SkipSaveFileSerialization]
public class NightOwl : StateMachineComponent<NightOwl.StatesInstance>
{
	// Token: 0x06003DBC RID: 15804 RVA: 0x00159158 File Offset: 0x00157358
	protected override void OnSpawn()
	{
		this.attributeModifiers = new AttributeModifier[]
		{
			new AttributeModifier("Construction", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true),
			new AttributeModifier("Digging", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true),
			new AttributeModifier("Machinery", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true),
			new AttributeModifier("Athletics", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true),
			new AttributeModifier("Learning", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true),
			new AttributeModifier("Cooking", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true),
			new AttributeModifier("Art", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true),
			new AttributeModifier("Strength", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true),
			new AttributeModifier("Caring", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true),
			new AttributeModifier("Botanist", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true),
			new AttributeModifier("Ranching", TRAITS.NIGHTOWL_MODIFIER, DUPLICANTS.TRAITS.NIGHTOWL.NAME, false, false, true)
		};
		base.smi.StartSM();
	}

	// Token: 0x06003DBD RID: 15805 RVA: 0x001592D4 File Offset: 0x001574D4
	public void ApplyModifiers()
	{
		Attributes attributes = base.gameObject.GetAttributes();
		for (int i = 0; i < this.attributeModifiers.Length; i++)
		{
			AttributeModifier attributeModifier = this.attributeModifiers[i];
			attributes.Add(attributeModifier);
		}
	}

	// Token: 0x06003DBE RID: 15806 RVA: 0x00159310 File Offset: 0x00157510
	public void RemoveModifiers()
	{
		Attributes attributes = base.gameObject.GetAttributes();
		for (int i = 0; i < this.attributeModifiers.Length; i++)
		{
			AttributeModifier attributeModifier = this.attributeModifiers[i];
			attributes.Remove(attributeModifier);
		}
	}

	// Token: 0x04002877 RID: 10359
	[MyCmpReq]
	private KPrefabID kPrefabID;

	// Token: 0x04002878 RID: 10360
	private AttributeModifier[] attributeModifiers;

	// Token: 0x02001630 RID: 5680
	public class StatesInstance : GameStateMachine<NightOwl.States, NightOwl.StatesInstance, NightOwl, object>.GameInstance
	{
		// Token: 0x060086F5 RID: 34549 RVA: 0x002F059B File Offset: 0x002EE79B
		public StatesInstance(NightOwl master)
			: base(master)
		{
		}

		// Token: 0x060086F6 RID: 34550 RVA: 0x002F05A4 File Offset: 0x002EE7A4
		public bool IsNight()
		{
			return !(GameClock.Instance == null) && !(base.master.kPrefabID.PrefabTag == GameTags.MinionSelectPreview) && GameClock.Instance.IsNighttime();
		}
	}

	// Token: 0x02001631 RID: 5681
	public class States : GameStateMachine<NightOwl.States, NightOwl.StatesInstance, NightOwl>
	{
		// Token: 0x060086F7 RID: 34551 RVA: 0x002F05DC File Offset: 0x002EE7DC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.TagTransition(GameTags.Dead, null, false);
			this.idle.Transition(this.early, (NightOwl.StatesInstance smi) => smi.IsNight(), UpdateRate.SIM_200ms);
			this.early.Enter("Night", delegate(NightOwl.StatesInstance smi)
			{
				smi.master.ApplyModifiers();
			}).Exit("NotNight", delegate(NightOwl.StatesInstance smi)
			{
				smi.master.RemoveModifiers();
			}).ToggleStatusItem(Db.Get().DuplicantStatusItems.NightTime, null)
				.ToggleExpression(Db.Get().Expressions.Happy, null)
				.Transition(this.idle, (NightOwl.StatesInstance smi) => !smi.IsNight(), UpdateRate.SIM_200ms);
		}

		// Token: 0x0400692C RID: 26924
		public GameStateMachine<NightOwl.States, NightOwl.StatesInstance, NightOwl, object>.State idle;

		// Token: 0x0400692D RID: 26925
		public GameStateMachine<NightOwl.States, NightOwl.StatesInstance, NightOwl, object>.State early;
	}
}
