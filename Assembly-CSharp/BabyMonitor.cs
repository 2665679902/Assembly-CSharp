using System;
using Klei;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006BE RID: 1726
public class BabyMonitor : GameStateMachine<BabyMonitor, BabyMonitor.Instance, IStateMachineTarget, BabyMonitor.Def>
{
	// Token: 0x06002EFD RID: 12029 RVA: 0x000F8A2C File Offset: 0x000F6C2C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.baby;
		this.root.Enter(new StateMachine<BabyMonitor, BabyMonitor.Instance, IStateMachineTarget, BabyMonitor.Def>.State.Callback(BabyMonitor.AddBabyEffect));
		this.baby.Transition(this.spawnadult, new StateMachine<BabyMonitor, BabyMonitor.Instance, IStateMachineTarget, BabyMonitor.Def>.Transition.ConditionCallback(BabyMonitor.IsReadyToSpawnAdult), UpdateRate.SIM_4000ms);
		this.spawnadult.ToggleBehaviour(GameTags.Creatures.Behaviours.GrowUpBehaviour, (BabyMonitor.Instance smi) => true, null);
		this.babyEffect = new Effect("IsABaby", CREATURES.MODIFIERS.BABY.NAME, CREATURES.MODIFIERS.BABY.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		this.babyEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Metabolism.Id, -0.9f, CREATURES.MODIFIERS.BABY.NAME, true, false, true));
		this.babyEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, 5f, CREATURES.MODIFIERS.BABY.NAME, false, false, true));
	}

	// Token: 0x06002EFE RID: 12030 RVA: 0x000F8B52 File Offset: 0x000F6D52
	private static void AddBabyEffect(BabyMonitor.Instance smi)
	{
		smi.Get<Effects>().Add(smi.sm.babyEffect, false);
	}

	// Token: 0x06002EFF RID: 12031 RVA: 0x000F8B6C File Offset: 0x000F6D6C
	private static bool IsReadyToSpawnAdult(BabyMonitor.Instance smi)
	{
		AmountInstance amountInstance = Db.Get().Amounts.Age.Lookup(smi.gameObject);
		float num = smi.def.adultThreshold;
		if (GenericGameSettings.instance.acceleratedLifecycle)
		{
			num = 0.005f;
		}
		return amountInstance.value > num;
	}

	// Token: 0x04001C44 RID: 7236
	public GameStateMachine<BabyMonitor, BabyMonitor.Instance, IStateMachineTarget, BabyMonitor.Def>.State baby;

	// Token: 0x04001C45 RID: 7237
	public GameStateMachine<BabyMonitor, BabyMonitor.Instance, IStateMachineTarget, BabyMonitor.Def>.State spawnadult;

	// Token: 0x04001C46 RID: 7238
	public Effect babyEffect;

	// Token: 0x02001398 RID: 5016
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006126 RID: 24870
		public Tag adultPrefab;

		// Token: 0x04006127 RID: 24871
		public string onGrowDropID;

		// Token: 0x04006128 RID: 24872
		public bool forceAdultNavType;

		// Token: 0x04006129 RID: 24873
		public float adultThreshold = 5f;
	}

	// Token: 0x02001399 RID: 5017
	public new class Instance : GameStateMachine<BabyMonitor, BabyMonitor.Instance, IStateMachineTarget, BabyMonitor.Def>.GameInstance
	{
		// Token: 0x06007E68 RID: 32360 RVA: 0x002D916A File Offset: 0x002D736A
		public Instance(IStateMachineTarget master, BabyMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007E69 RID: 32361 RVA: 0x002D9174 File Offset: 0x002D7374
		public void SpawnAdult()
		{
			Vector3 position = base.smi.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Creatures);
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(base.smi.def.adultPrefab), position);
			gameObject.SetActive(true);
			gameObject.GetSMI<AnimInterruptMonitor.Instance>().PlayAnim("growup_pst");
			if (base.smi.def.onGrowDropID != null)
			{
				Util.KInstantiate(Assets.GetPrefab(base.smi.def.onGrowDropID), position).SetActive(true);
			}
			foreach (AmountInstance amountInstance in base.gameObject.GetAmounts())
			{
				AmountInstance amountInstance2 = amountInstance.amount.Lookup(gameObject);
				if (amountInstance2 != null)
				{
					float num = amountInstance.value / amountInstance.GetMax();
					amountInstance2.value = num * amountInstance2.GetMax();
				}
			}
			if (!base.smi.def.forceAdultNavType)
			{
				Navigator component = base.smi.GetComponent<Navigator>();
				gameObject.GetComponent<Navigator>().SetCurrentNavType(component.CurrentNavType);
			}
			gameObject.Trigger(-2027483228, base.gameObject);
			KSelectable component2 = base.gameObject.GetComponent<KSelectable>();
			if (SelectTool.Instance != null && SelectTool.Instance.selected != null && SelectTool.Instance.selected == component2)
			{
				SelectTool.Instance.Select(gameObject.GetComponent<KSelectable>(), false);
			}
			base.smi.gameObject.DeleteObject();
		}
	}
}
