using System;
using Klei.AI;
using UnityEngine;

// Token: 0x0200057C RID: 1404
public class BeeHive : GameStateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>
{
	// Token: 0x0600222C RID: 8748 RVA: 0x000B9628 File Offset: 0x000B7828
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.enabled.grownStates;
		this.root.DoTutorial(Tutorial.TutorialMessages.TM_Radiation).Enter(delegate(BeeHive.StatesInstance smi)
		{
			AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(smi.gameObject);
			if (amountInstance != null)
			{
				amountInstance.hide = true;
			}
		}).EventHandler(GameHashes.Died, delegate(BeeHive.StatesInstance smi)
		{
			PrimaryElement component = smi.GetComponent<PrimaryElement>();
			Storage component2 = smi.GetComponent<Storage>();
			byte index = Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.id);
			component2.AddOre(SimHashes.NuclearWaste, BeeHiveTuning.WASTE_DROPPED_ON_DEATH, component.Temperature, index, BeeHiveTuning.GERMS_DROPPED_ON_DEATH, false, true);
			component2.DropAll(smi.master.transform.position, true, true, default(Vector3), true, null);
		});
		this.disabled.ToggleTag(GameTags.Creatures.Behaviours.DisableCreature).EventTransition(GameHashes.FoundationChanged, this.enabled, (BeeHive.StatesInstance smi) => !smi.IsDisabled()).EventTransition(GameHashes.EntombedChanged, this.enabled, (BeeHive.StatesInstance smi) => !smi.IsDisabled())
			.EventTransition(GameHashes.EnteredBreathableArea, this.enabled, (BeeHive.StatesInstance smi) => !smi.IsDisabled());
		this.enabled.EventTransition(GameHashes.FoundationChanged, this.disabled, (BeeHive.StatesInstance smi) => smi.IsDisabled()).EventTransition(GameHashes.EntombedChanged, this.disabled, (BeeHive.StatesInstance smi) => smi.IsDisabled()).EventTransition(GameHashes.Drowning, this.disabled, (BeeHive.StatesInstance smi) => smi.IsDisabled())
			.DefaultState(this.enabled.grownStates);
		this.enabled.growingStates.ParamTransition<float>(this.hiveGrowth, this.enabled.grownStates, (BeeHive.StatesInstance smi, float f) => f >= 1f).DefaultState(this.enabled.growingStates.idle);
		this.enabled.growingStates.idle.Update(delegate(BeeHive.StatesInstance smi, float dt)
		{
			smi.DeltaGrowth(dt / 600f / BeeHiveTuning.HIVE_GROWTH_TIME);
		}, UpdateRate.SIM_4000ms, false);
		this.enabled.grownStates.ParamTransition<float>(this.hiveGrowth, this.enabled.growingStates, (BeeHive.StatesInstance smi, float f) => f < 1f).DefaultState(this.enabled.grownStates.dayTime);
		this.enabled.grownStates.dayTime.EventTransition(GameHashes.Nighttime, (BeeHive.StatesInstance smi) => GameClock.Instance, this.enabled.grownStates.nightTime, (BeeHive.StatesInstance smi) => GameClock.Instance.IsNighttime());
		this.enabled.grownStates.nightTime.EventTransition(GameHashes.NewDay, (BeeHive.StatesInstance smi) => GameClock.Instance, this.enabled.grownStates.dayTime, (BeeHive.StatesInstance smi) => GameClock.Instance.GetTimeSinceStartOfCycle() <= 1f).Exit(delegate(BeeHive.StatesInstance smi)
		{
			if (!GameClock.Instance.IsNighttime())
			{
				smi.SpawnNewLarvaFromHive();
			}
		});
	}

	// Token: 0x040013BB RID: 5051
	public GameStateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>.State disabled;

	// Token: 0x040013BC RID: 5052
	public BeeHive.EnabledStates enabled;

	// Token: 0x040013BD RID: 5053
	public StateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>.FloatParameter hiveGrowth = new StateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>.FloatParameter(1f);

	// Token: 0x020011A4 RID: 4516
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005B71 RID: 23409
		public string beePrefabID;

		// Token: 0x04005B72 RID: 23410
		public string larvaPrefabID;
	}

	// Token: 0x020011A5 RID: 4517
	public class GrowingStates : GameStateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>.State
	{
		// Token: 0x04005B73 RID: 23411
		public GameStateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>.State idle;
	}

	// Token: 0x020011A6 RID: 4518
	public class GrownStates : GameStateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>.State
	{
		// Token: 0x04005B74 RID: 23412
		public GameStateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>.State dayTime;

		// Token: 0x04005B75 RID: 23413
		public GameStateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>.State nightTime;
	}

	// Token: 0x020011A7 RID: 4519
	public class EnabledStates : GameStateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>.State
	{
		// Token: 0x04005B76 RID: 23414
		public BeeHive.GrowingStates growingStates;

		// Token: 0x04005B77 RID: 23415
		public BeeHive.GrownStates grownStates;
	}

	// Token: 0x020011A8 RID: 4520
	public class StatesInstance : GameStateMachine<BeeHive, BeeHive.StatesInstance, IStateMachineTarget, BeeHive.Def>.GameInstance
	{
		// Token: 0x06007759 RID: 30553 RVA: 0x002BB31E File Offset: 0x002B951E
		public StatesInstance(IStateMachineTarget master, BeeHive.Def def)
			: base(master, def)
		{
			base.Subscribe(1119167081, new Action<object>(this.OnNewGameSpawn));
			Components.BeeHives.Add(this);
		}

		// Token: 0x0600775A RID: 30554 RVA: 0x002BB34A File Offset: 0x002B954A
		public void SetUpNewHive()
		{
			base.sm.hiveGrowth.Set(0f, this, false);
		}

		// Token: 0x0600775B RID: 30555 RVA: 0x002BB364 File Offset: 0x002B9564
		protected override void OnCleanUp()
		{
			Components.BeeHives.Remove(this);
			base.OnCleanUp();
		}

		// Token: 0x0600775C RID: 30556 RVA: 0x002BB377 File Offset: 0x002B9577
		private void OnNewGameSpawn(object data)
		{
			this.NewGamePopulateHive();
		}

		// Token: 0x0600775D RID: 30557 RVA: 0x002BB380 File Offset: 0x002B9580
		private void NewGamePopulateHive()
		{
			int num = 1;
			for (int i = 0; i < num; i++)
			{
				this.SpawnNewBeeFromHive();
			}
			num = 1;
			for (int j = 0; j < num; j++)
			{
				this.SpawnNewLarvaFromHive();
			}
		}

		// Token: 0x0600775E RID: 30558 RVA: 0x002BB3B5 File Offset: 0x002B95B5
		public bool IsFullyGrown()
		{
			return base.sm.hiveGrowth.Get(this) >= 1f;
		}

		// Token: 0x0600775F RID: 30559 RVA: 0x002BB3D4 File Offset: 0x002B95D4
		public void DeltaGrowth(float delta)
		{
			float num = base.sm.hiveGrowth.Get(this);
			num += delta;
			Mathf.Clamp01(num);
			base.sm.hiveGrowth.Set(num, this, false);
		}

		// Token: 0x06007760 RID: 30560 RVA: 0x002BB412 File Offset: 0x002B9612
		public void SpawnNewLarvaFromHive()
		{
			Util.KInstantiate(Assets.GetPrefab(base.def.larvaPrefabID), base.transform.GetPosition()).SetActive(true);
		}

		// Token: 0x06007761 RID: 30561 RVA: 0x002BB43F File Offset: 0x002B963F
		public void SpawnNewBeeFromHive()
		{
			Util.KInstantiate(Assets.GetPrefab(base.def.beePrefabID), base.transform.GetPosition()).SetActive(true);
		}

		// Token: 0x06007762 RID: 30562 RVA: 0x002BB46C File Offset: 0x002B966C
		public bool IsDisabled()
		{
			KPrefabID component = base.GetComponent<KPrefabID>();
			return component.HasTag(GameTags.Creatures.HasNoFoundation) || component.HasTag(GameTags.Entombed) || component.HasTag(GameTags.Creatures.Drowning);
		}
	}
}
