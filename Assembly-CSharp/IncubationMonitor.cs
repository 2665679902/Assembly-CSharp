using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006DC RID: 1756
public class IncubationMonitor : GameStateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>
{
	// Token: 0x06002FCE RID: 12238 RVA: 0x000FC604 File Offset: 0x000FA804
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.incubating;
		this.root.Enter(delegate(IncubationMonitor.Instance smi)
		{
			smi.OnOperationalChanged(null);
		}).Enter(delegate(IncubationMonitor.Instance smi)
		{
			Components.IncubationMonitors.Add(smi);
		}).Exit(delegate(IncubationMonitor.Instance smi)
		{
			Components.IncubationMonitors.Remove(smi);
		});
		this.incubating.PlayAnim("idle", KAnim.PlayMode.Loop).Transition(this.hatching_pre, new StateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.Transition.ConditionCallback(IncubationMonitor.IsReadyToHatch), UpdateRate.SIM_1000ms).TagTransition(GameTags.Entombed, this.entombed, false)
			.ParamTransition<bool>(this.isSuppressed, this.suppressed, GameStateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.IsTrue)
			.ToggleEffect((IncubationMonitor.Instance smi) => smi.incubatingEffect);
		this.entombed.TagTransition(GameTags.Entombed, this.incubating, true);
		this.suppressed.ToggleEffect((IncubationMonitor.Instance smi) => this.suppressedEffect).ParamTransition<bool>(this.isSuppressed, this.incubating, GameStateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.IsFalse).TagTransition(GameTags.Entombed, this.entombed, false)
			.Transition(this.not_viable, new StateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.Transition.ConditionCallback(IncubationMonitor.NoLongerViable), UpdateRate.SIM_1000ms);
		this.hatching_pre.Enter(new StateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State.Callback(IncubationMonitor.DropSelfFromStorage)).PlayAnim("hatching_pre").OnAnimQueueComplete(this.hatching_pst);
		this.hatching_pst.Enter(new StateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State.Callback(IncubationMonitor.SpawnBaby)).PlayAnim("hatching_pst").OnAnimQueueComplete(null)
			.Exit(new StateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State.Callback(IncubationMonitor.DeleteSelf));
		this.not_viable.Enter(new StateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State.Callback(IncubationMonitor.SpawnGenericEgg)).GoTo(null).Exit(new StateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State.Callback(IncubationMonitor.DeleteSelf));
		this.suppressedEffect = new Effect("IncubationSuppressed", CREATURES.MODIFIERS.INCUBATING_SUPPRESSED.NAME, CREATURES.MODIFIERS.INCUBATING_SUPPRESSED.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		this.suppressedEffect.Add(new AttributeModifier(Db.Get().Amounts.Viability.deltaAttribute.Id, -0.016666668f, CREATURES.MODIFIERS.INCUBATING_SUPPRESSED.NAME, false, false, true));
	}

	// Token: 0x06002FCF RID: 12239 RVA: 0x000FC87D File Offset: 0x000FAA7D
	private static bool IsReadyToHatch(IncubationMonitor.Instance smi)
	{
		return !smi.gameObject.HasTag(GameTags.Entombed) && smi.incubation.value >= smi.incubation.GetMax();
	}

	// Token: 0x06002FD0 RID: 12240 RVA: 0x000FC8B0 File Offset: 0x000FAAB0
	private static void SpawnBaby(IncubationMonitor.Instance smi)
	{
		Vector3 position = smi.transform.GetPosition();
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Creatures);
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(smi.def.spawnedCreature), position);
		gameObject.SetActive(true);
		gameObject.GetSMI<AnimInterruptMonitor.Instance>().Play("hatching_pst", KAnim.PlayMode.Once);
		KSelectable component = smi.gameObject.GetComponent<KSelectable>();
		if (SelectTool.Instance != null && SelectTool.Instance.selected != null && SelectTool.Instance.selected == component)
		{
			SelectTool.Instance.Select(gameObject.GetComponent<KSelectable>(), false);
		}
		Db.Get().Amounts.Wildness.Copy(gameObject, smi.gameObject);
		if (smi.incubator != null)
		{
			smi.incubator.StoreBaby(gameObject);
		}
		IncubationMonitor.SpawnShell(smi);
		SaveLoader.Instance.saveManager.Unregister(smi.GetComponent<SaveLoadRoot>());
	}

	// Token: 0x06002FD1 RID: 12241 RVA: 0x000FC9A5 File Offset: 0x000FABA5
	private static bool NoLongerViable(IncubationMonitor.Instance smi)
	{
		return !smi.gameObject.HasTag(GameTags.Entombed) && smi.viability.value <= smi.viability.GetMin();
	}

	// Token: 0x06002FD2 RID: 12242 RVA: 0x000FC9D8 File Offset: 0x000FABD8
	private static GameObject SpawnShell(IncubationMonitor.Instance smi)
	{
		Vector3 position = smi.transform.GetPosition();
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("EggShell"), position);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		PrimaryElement component2 = smi.GetComponent<PrimaryElement>();
		component.Mass = component2.Mass * 0.5f;
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06002FD3 RID: 12243 RVA: 0x000FCA2C File Offset: 0x000FAC2C
	private static GameObject SpawnEggInnards(IncubationMonitor.Instance smi)
	{
		Vector3 position = smi.transform.GetPosition();
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("RawEgg"), position);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		PrimaryElement component2 = smi.GetComponent<PrimaryElement>();
		component.Mass = component2.Mass * 0.5f;
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06002FD4 RID: 12244 RVA: 0x000FCA80 File Offset: 0x000FAC80
	private static void SpawnGenericEgg(IncubationMonitor.Instance smi)
	{
		IncubationMonitor.SpawnShell(smi);
		GameObject gameObject = IncubationMonitor.SpawnEggInnards(smi);
		KSelectable component = smi.gameObject.GetComponent<KSelectable>();
		if (SelectTool.Instance != null && SelectTool.Instance.selected != null && SelectTool.Instance.selected == component)
		{
			SelectTool.Instance.Select(gameObject.GetComponent<KSelectable>(), false);
		}
	}

	// Token: 0x06002FD5 RID: 12245 RVA: 0x000FCAE9 File Offset: 0x000FACE9
	private static void DeleteSelf(IncubationMonitor.Instance smi)
	{
		smi.gameObject.DeleteObject();
	}

	// Token: 0x06002FD6 RID: 12246 RVA: 0x000FCAF8 File Offset: 0x000FACF8
	private static void DropSelfFromStorage(IncubationMonitor.Instance smi)
	{
		if (!smi.sm.inIncubator.Get(smi))
		{
			Storage storage = smi.GetStorage();
			if (storage)
			{
				storage.Drop(smi.gameObject, true);
			}
			smi.gameObject.AddTag(GameTags.StoredPrivate);
		}
	}

	// Token: 0x04001CC9 RID: 7369
	public StateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.BoolParameter incubatorIsActive;

	// Token: 0x04001CCA RID: 7370
	public StateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.BoolParameter inIncubator;

	// Token: 0x04001CCB RID: 7371
	public StateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.BoolParameter isSuppressed;

	// Token: 0x04001CCC RID: 7372
	public GameStateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State incubating;

	// Token: 0x04001CCD RID: 7373
	public GameStateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State entombed;

	// Token: 0x04001CCE RID: 7374
	public GameStateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State suppressed;

	// Token: 0x04001CCF RID: 7375
	public GameStateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State hatching_pre;

	// Token: 0x04001CD0 RID: 7376
	public GameStateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State hatching_pst;

	// Token: 0x04001CD1 RID: 7377
	public GameStateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.State not_viable;

	// Token: 0x04001CD2 RID: 7378
	private Effect suppressedEffect;

	// Token: 0x020013E0 RID: 5088
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06007F5C RID: 32604 RVA: 0x002DC3BC File Offset: 0x002DA5BC
		public override void Configure(GameObject prefab)
		{
			List<string> initialAmounts = prefab.GetComponent<Modifiers>().initialAmounts;
			initialAmounts.Add(Db.Get().Amounts.Wildness.Id);
			initialAmounts.Add(Db.Get().Amounts.Incubation.Id);
			initialAmounts.Add(Db.Get().Amounts.Viability.Id);
		}

		// Token: 0x040061DD RID: 25053
		public float baseIncubationRate;

		// Token: 0x040061DE RID: 25054
		public Tag spawnedCreature;
	}

	// Token: 0x020013E1 RID: 5089
	public new class Instance : GameStateMachine<IncubationMonitor, IncubationMonitor.Instance, IStateMachineTarget, IncubationMonitor.Def>.GameInstance
	{
		// Token: 0x06007F5E RID: 32606 RVA: 0x002DC42C File Offset: 0x002DA62C
		public Instance(IStateMachineTarget master, IncubationMonitor.Def def)
			: base(master, def)
		{
			this.incubation = Db.Get().Amounts.Incubation.Lookup(base.gameObject);
			Action<object> action = new Action<object>(this.OnStore);
			master.Subscribe(856640610, action);
			master.Subscribe(1309017699, action);
			Action<object> action2 = new Action<object>(this.OnOperationalChanged);
			master.Subscribe(1628751838, action2);
			master.Subscribe(960378201, action2);
			this.wildness = Db.Get().Amounts.Wildness.Lookup(base.gameObject);
			this.wildness.value = this.wildness.GetMax();
			this.viability = Db.Get().Amounts.Viability.Lookup(base.gameObject);
			this.viability.value = this.viability.GetMax();
			float num = def.baseIncubationRate;
			if (GenericGameSettings.instance.acceleratedLifecycle)
			{
				num = 33.333332f;
			}
			AttributeModifier attributeModifier = new AttributeModifier(Db.Get().Amounts.Incubation.deltaAttribute.Id, num, CREATURES.MODIFIERS.BASE_INCUBATION_RATE.NAME, false, false, true);
			this.incubatingEffect = new Effect("Incubating", CREATURES.MODIFIERS.INCUBATING.NAME, CREATURES.MODIFIERS.INCUBATING.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
			this.incubatingEffect.Add(attributeModifier);
		}

		// Token: 0x06007F5F RID: 32607 RVA: 0x002DC5AA File Offset: 0x002DA7AA
		public Storage GetStorage()
		{
			if (!(base.transform.parent != null))
			{
				return null;
			}
			return base.transform.parent.GetComponent<Storage>();
		}

		// Token: 0x06007F60 RID: 32608 RVA: 0x002DC5D4 File Offset: 0x002DA7D4
		public void OnStore(object data)
		{
			Storage storage = data as Storage;
			bool flag = storage || (data != null && (bool)data);
			EggIncubator eggIncubator = (storage ? storage.GetComponent<EggIncubator>() : null);
			this.UpdateIncubationState(flag, eggIncubator);
		}

		// Token: 0x06007F61 RID: 32609 RVA: 0x002DC61C File Offset: 0x002DA81C
		public void OnOperationalChanged(object data = null)
		{
			bool flag = base.gameObject.HasTag(GameTags.Stored);
			Storage storage = this.GetStorage();
			EggIncubator eggIncubator = (storage ? storage.GetComponent<EggIncubator>() : null);
			this.UpdateIncubationState(flag, eggIncubator);
		}

		// Token: 0x06007F62 RID: 32610 RVA: 0x002DC65C File Offset: 0x002DA85C
		private void UpdateIncubationState(bool stored, EggIncubator incubator)
		{
			this.incubator = incubator;
			base.smi.sm.inIncubator.Set(incubator != null, base.smi, false);
			bool flag = stored && !incubator;
			base.smi.sm.isSuppressed.Set(flag, base.smi, false);
			Operational operational = (incubator ? incubator.GetComponent<Operational>() : null);
			bool flag2 = incubator && (operational == null || operational.IsOperational);
			base.smi.sm.incubatorIsActive.Set(flag2, base.smi, false);
		}

		// Token: 0x06007F63 RID: 32611 RVA: 0x002DC710 File Offset: 0x002DA910
		public void ApplySongBuff()
		{
			base.GetComponent<Effects>().Add("EggSong", true);
		}

		// Token: 0x06007F64 RID: 32612 RVA: 0x002DC724 File Offset: 0x002DA924
		public bool HasSongBuff()
		{
			return base.GetComponent<Effects>().HasEffect("EggSong");
		}

		// Token: 0x040061DF RID: 25055
		public AmountInstance incubation;

		// Token: 0x040061E0 RID: 25056
		public AmountInstance wildness;

		// Token: 0x040061E1 RID: 25057
		public AmountInstance viability;

		// Token: 0x040061E2 RID: 25058
		public EggIncubator incubator;

		// Token: 0x040061E3 RID: 25059
		public Effect incubatingEffect;
	}
}
