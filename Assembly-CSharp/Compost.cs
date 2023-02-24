using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200058C RID: 1420
public class Compost : StateMachineComponent<Compost.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x060022DC RID: 8924 RVA: 0x000BD9D9 File Offset: 0x000BBBD9
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Compost>(-1697596308, Compost.OnStorageChangedDelegate);
	}

	// Token: 0x060022DD RID: 8925 RVA: 0x000BD9F4 File Offset: 0x000BBBF4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<ManualDeliveryKG>().ShowStatusItem = false;
		this.temperatureAdjuster = new SimulatedTemperatureAdjuster(this.simulatedInternalTemperature, this.simulatedInternalHeatCapacity, this.simulatedThermalConductivity, base.GetComponent<Storage>());
		base.smi.StartSM();
	}

	// Token: 0x060022DE RID: 8926 RVA: 0x000BDA41 File Offset: 0x000BBC41
	protected override void OnCleanUp()
	{
		this.temperatureAdjuster.CleanUp();
	}

	// Token: 0x060022DF RID: 8927 RVA: 0x000BDA4E File Offset: 0x000BBC4E
	private void OnStorageChanged(object data)
	{
		(GameObject)data == null;
	}

	// Token: 0x060022E0 RID: 8928 RVA: 0x000BDA5D File Offset: 0x000BBC5D
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return SimulatedTemperatureAdjuster.GetDescriptors(this.simulatedInternalTemperature);
	}

	// Token: 0x04001419 RID: 5145
	[MyCmpGet]
	private Operational operational;

	// Token: 0x0400141A RID: 5146
	[MyCmpGet]
	private Storage storage;

	// Token: 0x0400141B RID: 5147
	[SerializeField]
	public float flipInterval = 600f;

	// Token: 0x0400141C RID: 5148
	[SerializeField]
	public float simulatedInternalTemperature = 323.15f;

	// Token: 0x0400141D RID: 5149
	[SerializeField]
	public float simulatedInternalHeatCapacity = 400f;

	// Token: 0x0400141E RID: 5150
	[SerializeField]
	public float simulatedThermalConductivity = 1000f;

	// Token: 0x0400141F RID: 5151
	private SimulatedTemperatureAdjuster temperatureAdjuster;

	// Token: 0x04001420 RID: 5152
	private static readonly EventSystem.IntraObjectHandler<Compost> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<Compost>(delegate(Compost component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x020011C4 RID: 4548
	public class StatesInstance : GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.GameInstance
	{
		// Token: 0x060077F2 RID: 30706 RVA: 0x002BCDF5 File Offset: 0x002BAFF5
		public StatesInstance(Compost master)
			: base(master)
		{
		}

		// Token: 0x060077F3 RID: 30707 RVA: 0x002BCDFE File Offset: 0x002BAFFE
		public bool CanStartConverting()
		{
			return base.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false);
		}

		// Token: 0x060077F4 RID: 30708 RVA: 0x002BCE11 File Offset: 0x002BB011
		public bool CanContinueConverting()
		{
			return base.master.GetComponent<ElementConverter>().CanConvertAtAll();
		}

		// Token: 0x060077F5 RID: 30709 RVA: 0x002BCE23 File Offset: 0x002BB023
		public bool IsEmpty()
		{
			return base.master.storage.IsEmpty();
		}

		// Token: 0x060077F6 RID: 30710 RVA: 0x002BCE35 File Offset: 0x002BB035
		public void ResetWorkable()
		{
			CompostWorkable component = base.master.GetComponent<CompostWorkable>();
			component.ShowProgressBar(false);
			component.WorkTimeRemaining = component.GetWorkTime();
		}
	}

	// Token: 0x020011C5 RID: 4549
	public class States : GameStateMachine<Compost.States, Compost.StatesInstance, Compost>
	{
		// Token: 0x060077F7 RID: 30711 RVA: 0x002BCE54 File Offset: 0x002BB054
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.empty.Enter("empty", delegate(Compost.StatesInstance smi)
			{
				smi.ResetWorkable();
			}).EventTransition(GameHashes.OnStorageChange, this.insufficientMass, (Compost.StatesInstance smi) => !smi.IsEmpty()).EventTransition(GameHashes.OperationalChanged, this.disabledEmpty, (Compost.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.AwaitingWaste, null)
				.PlayAnim("off");
			this.insufficientMass.Enter("empty", delegate(Compost.StatesInstance smi)
			{
				smi.ResetWorkable();
			}).EventTransition(GameHashes.OnStorageChange, this.empty, (Compost.StatesInstance smi) => smi.IsEmpty()).EventTransition(GameHashes.OnStorageChange, this.inert, (Compost.StatesInstance smi) => smi.CanStartConverting())
				.ToggleStatusItem(Db.Get().BuildingStatusItems.AwaitingWaste, null)
				.PlayAnim("idle_half");
			this.inert.EventTransition(GameHashes.OperationalChanged, this.disabled, (Compost.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational).PlayAnim("on").ToggleStatusItem(Db.Get().BuildingStatusItems.AwaitingCompostFlip, null)
				.ToggleChore(new Func<Compost.StatesInstance, Chore>(this.CreateFlipChore), this.composting);
			this.composting.Enter("Composting", delegate(Compost.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).EventTransition(GameHashes.OnStorageChange, this.empty, (Compost.StatesInstance smi) => !smi.CanContinueConverting()).EventTransition(GameHashes.OperationalChanged, this.disabled, (Compost.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational)
				.ScheduleGoTo((Compost.StatesInstance smi) => smi.master.flipInterval, this.inert)
				.Exit(delegate(Compost.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
				});
			this.disabled.Enter("disabledEmpty", delegate(Compost.StatesInstance smi)
			{
				smi.ResetWorkable();
			}).PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.inert, (Compost.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational);
			this.disabledEmpty.Enter("disabledEmpty", delegate(Compost.StatesInstance smi)
			{
				smi.ResetWorkable();
			}).PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.empty, (Compost.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational);
		}

		// Token: 0x060077F8 RID: 30712 RVA: 0x002BD1E4 File Offset: 0x002BB3E4
		private Chore CreateFlipChore(Compost.StatesInstance smi)
		{
			return new WorkChore<CompostWorkable>(Db.Get().ChoreTypes.FlipCompost, smi.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x04005BFA RID: 23546
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State empty;

		// Token: 0x04005BFB RID: 23547
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State insufficientMass;

		// Token: 0x04005BFC RID: 23548
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State disabled;

		// Token: 0x04005BFD RID: 23549
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State disabledEmpty;

		// Token: 0x04005BFE RID: 23550
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State inert;

		// Token: 0x04005BFF RID: 23551
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State composting;
	}
}
