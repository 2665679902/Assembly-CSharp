using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x02000618 RID: 1560
[SerializationConfig(MemberSerialization.OptIn)]
public class OilRefinery : StateMachineComponent<OilRefinery.StatesInstance>
{
	// Token: 0x060028CE RID: 10446 RVA: 0x000D83F4 File Offset: 0x000D65F4
	protected override void OnSpawn()
	{
		base.Subscribe<OilRefinery>(-1697596308, OilRefinery.OnStorageChangedDelegate);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.meter = new MeterController(component, "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Vector3.zero, null);
		base.smi.StartSM();
		this.maxSrcMass = base.GetComponent<ConduitConsumer>().capacityKG;
	}

	// Token: 0x060028CF RID: 10447 RVA: 0x000D8454 File Offset: 0x000D6654
	private void OnStorageChanged(object data)
	{
		float num = Mathf.Clamp01(this.storage.GetMassAvailable(SimHashes.CrudeOil) / this.maxSrcMass);
		this.meter.SetPositionPercent(num);
	}

	// Token: 0x060028D0 RID: 10448 RVA: 0x000D848C File Offset: 0x000D668C
	private static bool UpdateStateCb(int cell, object data)
	{
		OilRefinery oilRefinery = data as OilRefinery;
		if (Grid.Element[cell].IsGas)
		{
			oilRefinery.cellCount += 1f;
			oilRefinery.envPressure += Grid.Mass[cell];
		}
		return true;
	}

	// Token: 0x060028D1 RID: 10449 RVA: 0x000D84DC File Offset: 0x000D66DC
	private void TestAreaPressure()
	{
		this.envPressure = 0f;
		this.cellCount = 0f;
		if (this.occupyArea != null && base.gameObject != null)
		{
			this.occupyArea.TestArea(Grid.PosToCell(base.gameObject), this, new Func<int, object, bool>(OilRefinery.UpdateStateCb));
			this.envPressure /= this.cellCount;
		}
	}

	// Token: 0x060028D2 RID: 10450 RVA: 0x000D8552 File Offset: 0x000D6752
	private bool IsOverPressure()
	{
		return this.envPressure >= this.overpressureMass;
	}

	// Token: 0x060028D3 RID: 10451 RVA: 0x000D8565 File Offset: 0x000D6765
	private bool IsOverWarningPressure()
	{
		return this.envPressure >= this.overpressureWarningMass;
	}

	// Token: 0x040017F9 RID: 6137
	private bool wasOverPressure;

	// Token: 0x040017FA RID: 6138
	[SerializeField]
	public float overpressureWarningMass = 4.5f;

	// Token: 0x040017FB RID: 6139
	[SerializeField]
	public float overpressureMass = 5f;

	// Token: 0x040017FC RID: 6140
	private float maxSrcMass;

	// Token: 0x040017FD RID: 6141
	private float envPressure;

	// Token: 0x040017FE RID: 6142
	private float cellCount;

	// Token: 0x040017FF RID: 6143
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001800 RID: 6144
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001801 RID: 6145
	[MyCmpAdd]
	private OilRefinery.WorkableTarget workable;

	// Token: 0x04001802 RID: 6146
	[MyCmpReq]
	private OccupyArea occupyArea;

	// Token: 0x04001803 RID: 6147
	private const bool hasMeter = true;

	// Token: 0x04001804 RID: 6148
	private MeterController meter;

	// Token: 0x04001805 RID: 6149
	private static readonly EventSystem.IntraObjectHandler<OilRefinery> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<OilRefinery>(delegate(OilRefinery component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x0200128C RID: 4748
	public class StatesInstance : GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.GameInstance
	{
		// Token: 0x06007AB0 RID: 31408 RVA: 0x002C89F4 File Offset: 0x002C6BF4
		public StatesInstance(OilRefinery smi)
			: base(smi)
		{
		}

		// Token: 0x06007AB1 RID: 31409 RVA: 0x002C8A00 File Offset: 0x002C6C00
		public void TestAreaPressure()
		{
			base.smi.master.TestAreaPressure();
			bool flag = base.smi.master.IsOverPressure();
			bool flag2 = base.smi.master.IsOverWarningPressure();
			if (flag)
			{
				base.smi.master.wasOverPressure = true;
				base.sm.isOverPressure.Set(true, this, false);
				return;
			}
			if (base.smi.master.wasOverPressure && !flag2)
			{
				base.sm.isOverPressure.Set(false, this, false);
			}
		}
	}

	// Token: 0x0200128D RID: 4749
	public class States : GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery>
	{
		// Token: 0x06007AB2 RID: 31410 RVA: 0x002C8A90 File Offset: 0x002C6C90
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.root.EventTransition(GameHashes.OperationalChanged, this.disabled, (OilRefinery.StatesInstance smi) => !smi.master.operational.IsOperational);
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.needResources, (OilRefinery.StatesInstance smi) => smi.master.operational.IsOperational);
			this.needResources.EventTransition(GameHashes.OnStorageChange, this.ready, (OilRefinery.StatesInstance smi) => smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false));
			this.ready.Update("Test Pressure Update", delegate(OilRefinery.StatesInstance smi, float dt)
			{
				smi.TestAreaPressure();
			}, UpdateRate.SIM_1000ms, false).ParamTransition<bool>(this.isOverPressure, this.overpressure, GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.IsTrue).Transition(this.needResources, (OilRefinery.StatesInstance smi) => !smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false), UpdateRate.SIM_200ms)
				.ToggleChore((OilRefinery.StatesInstance smi) => new WorkChore<OilRefinery.WorkableTarget>(Db.Get().ChoreTypes.Fabricate, smi.master.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true), this.needResources);
			this.overpressure.Update("Test Pressure Update", delegate(OilRefinery.StatesInstance smi, float dt)
			{
				smi.TestAreaPressure();
			}, UpdateRate.SIM_1000ms, false).ParamTransition<bool>(this.isOverPressure, this.ready, GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.IsFalse).ToggleStatusItem(Db.Get().BuildingStatusItems.PressureOk, null);
		}

		// Token: 0x04005E21 RID: 24097
		public StateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.BoolParameter isOverPressure;

		// Token: 0x04005E22 RID: 24098
		public StateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.BoolParameter isOverPressureWarning;

		// Token: 0x04005E23 RID: 24099
		public GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.State disabled;

		// Token: 0x04005E24 RID: 24100
		public GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.State overpressure;

		// Token: 0x04005E25 RID: 24101
		public GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.State needResources;

		// Token: 0x04005E26 RID: 24102
		public GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.State ready;
	}

	// Token: 0x0200128E RID: 4750
	[AddComponentMenu("KMonoBehaviour/Workable/WorkableTarget")]
	public class WorkableTarget : Workable
	{
		// Token: 0x06007AB4 RID: 31412 RVA: 0x002C8C4C File Offset: 0x002C6E4C
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.showProgressBar = false;
			this.workerStatusItem = null;
			this.skillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
			this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
			this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_oilrefinery_kanim") };
		}

		// Token: 0x06007AB5 RID: 31413 RVA: 0x002C8CB0 File Offset: 0x002C6EB0
		protected override void OnSpawn()
		{
			base.OnSpawn();
			base.SetWorkTime(float.PositiveInfinity);
		}

		// Token: 0x06007AB6 RID: 31414 RVA: 0x002C8CC3 File Offset: 0x002C6EC3
		protected override void OnStartWork(Worker worker)
		{
			this.operational.SetActive(true, false);
		}

		// Token: 0x06007AB7 RID: 31415 RVA: 0x002C8CD2 File Offset: 0x002C6ED2
		protected override void OnStopWork(Worker worker)
		{
			this.operational.SetActive(false, false);
		}

		// Token: 0x06007AB8 RID: 31416 RVA: 0x002C8CE1 File Offset: 0x002C6EE1
		protected override void OnCompleteWork(Worker worker)
		{
			this.operational.SetActive(false, false);
		}

		// Token: 0x06007AB9 RID: 31417 RVA: 0x002C8CF0 File Offset: 0x002C6EF0
		public override bool InstantlyFinish(Worker worker)
		{
			return false;
		}

		// Token: 0x04005E27 RID: 24103
		[MyCmpGet]
		public Operational operational;
	}
}
