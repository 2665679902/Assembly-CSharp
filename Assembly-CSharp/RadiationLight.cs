using System;
using UnityEngine;

// Token: 0x020008B1 RID: 2225
public class RadiationLight : StateMachineComponent<RadiationLight.StatesInstance>
{
	// Token: 0x06003FFD RID: 16381 RVA: 0x0016570C File Offset: 0x0016390C
	public void UpdateMeter()
	{
		this.meter.SetPositionPercent(Mathf.Clamp01(this.storage.MassStored() / this.storage.capacityKg));
	}

	// Token: 0x06003FFE RID: 16382 RVA: 0x00165735 File Offset: 0x00163935
	public bool HasEnoughFuel()
	{
		return this.elementConverter.HasEnoughMassToStartConverting(false);
	}

	// Token: 0x06003FFF RID: 16383 RVA: 0x00165743 File Offset: 0x00163943
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.UpdateMeter();
	}

	// Token: 0x040029F3 RID: 10739
	[MyCmpGet]
	private Operational operational;

	// Token: 0x040029F4 RID: 10740
	[MyCmpGet]
	private Storage storage;

	// Token: 0x040029F5 RID: 10741
	[MyCmpGet]
	private RadiationEmitter emitter;

	// Token: 0x040029F6 RID: 10742
	[MyCmpGet]
	private ElementConverter elementConverter;

	// Token: 0x040029F7 RID: 10743
	private MeterController meter;

	// Token: 0x040029F8 RID: 10744
	public Tag elementToConsume;

	// Token: 0x040029F9 RID: 10745
	public float consumptionRate;

	// Token: 0x02001684 RID: 5764
	public class StatesInstance : GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight, object>.GameInstance
	{
		// Token: 0x060087E1 RID: 34785 RVA: 0x002F4184 File Offset: 0x002F2384
		public StatesInstance(RadiationLight smi)
			: base(smi)
		{
			if (base.GetComponent<Rotatable>().IsRotated)
			{
				RadiationEmitter component = base.GetComponent<RadiationEmitter>();
				component.emitDirection = 180f;
				component.emissionOffset = Vector3.left;
			}
			this.ToggleEmitter(false);
			smi.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target" });
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, true);
		}

		// Token: 0x060087E2 RID: 34786 RVA: 0x002F4201 File Offset: 0x002F2401
		public void ToggleEmitter(bool on)
		{
			base.smi.master.operational.SetActive(on, false);
			base.smi.master.emitter.SetEmitting(on);
		}
	}

	// Token: 0x02001685 RID: 5765
	public class States : GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight>
	{
		// Token: 0x060087E3 RID: 34787 RVA: 0x002F4230 File Offset: 0x002F2430
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.ready.idle;
			this.root.EventHandler(GameHashes.OnStorageChange, delegate(RadiationLight.StatesInstance smi)
			{
				smi.master.UpdateMeter();
			});
			this.waiting.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.ready.idle, (RadiationLight.StatesInstance smi) => smi.master.operational.IsOperational);
			this.ready.EventTransition(GameHashes.OperationalChanged, this.waiting, (RadiationLight.StatesInstance smi) => !smi.master.operational.IsOperational).DefaultState(this.ready.idle);
			this.ready.idle.PlayAnim("off").EventTransition(GameHashes.OnStorageChange, this.ready.on, (RadiationLight.StatesInstance smi) => smi.master.HasEnoughFuel());
			this.ready.on.PlayAnim("on").Enter(delegate(RadiationLight.StatesInstance smi)
			{
				smi.ToggleEmitter(true);
			}).EventTransition(GameHashes.OnStorageChange, this.ready.idle, (RadiationLight.StatesInstance smi) => !smi.master.HasEnoughFuel())
				.Exit(delegate(RadiationLight.StatesInstance smi)
				{
					smi.ToggleEmitter(false);
				});
		}

		// Token: 0x04006A0F RID: 27151
		public GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight, object>.State waiting;

		// Token: 0x04006A10 RID: 27152
		public RadiationLight.States.ReadyStates ready;

		// Token: 0x020020A3 RID: 8355
		public class ReadyStates : GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight, object>.State
		{
			// Token: 0x04009177 RID: 37239
			public GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight, object>.State idle;

			// Token: 0x04009178 RID: 37240
			public GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight, object>.State on;
		}
	}
}
