using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005CE RID: 1486
public class HeatCompressor : StateMachineComponent<HeatCompressor.StatesInstance>
{
	// Token: 0x060024FD RID: 9469 RVA: 0x000C8138 File Offset: 0x000C6338
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		this.meter.gameObject.GetComponent<KBatchedAnimController>().SetDirty();
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("HeatCube"), base.transform.GetPosition());
		gameObject.SetActive(true);
		this.heatCubeStorage.Store(gameObject, true, false, true, false);
		base.smi.StartSM();
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x000C81E7 File Offset: 0x000C63E7
	public void SetStorage(Storage inputStorage, Storage outputStorage, Storage heatCubeStorage)
	{
		this.inputStorage = inputStorage;
		this.outputStorage = outputStorage;
		this.heatCubeStorage = heatCubeStorage;
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x000C8200 File Offset: 0x000C6400
	public void CompressHeat(HeatCompressor.StatesInstance smi, float dt)
	{
		smi.heatRemovalTimer -= dt;
		float num = this.heatRemovalRate * dt / (float)this.inputStorage.items.Count;
		foreach (GameObject gameObject in this.inputStorage.items)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			float lowTemp = component.Element.lowTemp;
			GameUtil.DeltaThermalEnergy(component, -num, lowTemp);
			this.energyCompressed += num;
		}
		if (smi.heatRemovalTimer <= 0f)
		{
			for (int i = this.inputStorage.items.Count; i > 0; i--)
			{
				GameObject gameObject2 = this.inputStorage.items[i - 1];
				if (gameObject2)
				{
					this.inputStorage.Transfer(gameObject2, this.outputStorage, false, true);
				}
			}
			smi.StartNewHeatRemoval();
		}
		foreach (GameObject gameObject3 in this.heatCubeStorage.items)
		{
			GameUtil.DeltaThermalEnergy(gameObject3.GetComponent<PrimaryElement>(), this.energyCompressed / (float)this.heatCubeStorage.items.Count, 100000f);
		}
		this.energyCompressed = 0f;
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x000C8374 File Offset: 0x000C6574
	public void EjectHeatCube()
	{
		this.heatCubeStorage.DropAll(base.transform.GetPosition(), false, false, default(Vector3), true, null);
	}

	// Token: 0x0400155C RID: 5468
	[MyCmpReq]
	private Operational operational;

	// Token: 0x0400155D RID: 5469
	private MeterController meter;

	// Token: 0x0400155E RID: 5470
	public Storage inputStorage;

	// Token: 0x0400155F RID: 5471
	public Storage outputStorage;

	// Token: 0x04001560 RID: 5472
	public Storage heatCubeStorage;

	// Token: 0x04001561 RID: 5473
	public float heatRemovalRate = 100f;

	// Token: 0x04001562 RID: 5474
	public float heatRemovalTime = 100f;

	// Token: 0x04001563 RID: 5475
	[Serialize]
	public float energyCompressed;

	// Token: 0x04001564 RID: 5476
	public float heat_sink_active_time = 9000f;

	// Token: 0x04001565 RID: 5477
	[Serialize]
	public float time_active;

	// Token: 0x04001566 RID: 5478
	public float MAX_CUBE_TEMPERATURE = 3000f;

	// Token: 0x02001221 RID: 4641
	public class StatesInstance : GameStateMachine<HeatCompressor.States, HeatCompressor.StatesInstance, HeatCompressor, object>.GameInstance
	{
		// Token: 0x0600792D RID: 31021 RVA: 0x002C2472 File Offset: 0x002C0672
		public StatesInstance(HeatCompressor master)
			: base(master)
		{
		}

		// Token: 0x0600792E RID: 31022 RVA: 0x002C247C File Offset: 0x002C067C
		public void UpdateMeter()
		{
			float remainingCharge = this.GetRemainingCharge();
			base.master.meter.SetPositionPercent(remainingCharge);
		}

		// Token: 0x0600792F RID: 31023 RVA: 0x002C24A4 File Offset: 0x002C06A4
		public float GetRemainingCharge()
		{
			PrimaryElement primaryElement = base.smi.master.heatCubeStorage.FindFirstWithMass(GameTags.IndustrialIngredient, 0f);
			float num = 1f;
			if (primaryElement != null)
			{
				num = Mathf.Clamp01(primaryElement.GetComponent<PrimaryElement>().Temperature / base.smi.master.MAX_CUBE_TEMPERATURE);
			}
			return num;
		}

		// Token: 0x06007930 RID: 31024 RVA: 0x002C2503 File Offset: 0x002C0703
		public bool CanWork()
		{
			return this.GetRemainingCharge() < 1f && base.smi.master.heatCubeStorage.items.Count > 0;
		}

		// Token: 0x06007931 RID: 31025 RVA: 0x002C2531 File Offset: 0x002C0731
		public void StartNewHeatRemoval()
		{
			this.heatRemovalTimer = base.smi.master.heatRemovalTime;
		}

		// Token: 0x04005D23 RID: 23843
		[Serialize]
		public float heatRemovalTimer;
	}

	// Token: 0x02001222 RID: 4642
	public class States : GameStateMachine<HeatCompressor.States, HeatCompressor.StatesInstance, HeatCompressor>
	{
		// Token: 0x06007932 RID: 31026 RVA: 0x002C254C File Offset: 0x002C074C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inactive;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.EventTransition(GameHashes.OperationalChanged, this.inactive, (HeatCompressor.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.inactive.Enter(delegate(HeatCompressor.StatesInstance smi)
			{
				smi.UpdateMeter();
			}).PlayAnim("idle").Transition(this.dropCube, (HeatCompressor.StatesInstance smi) => smi.GetRemainingCharge() >= 1f, UpdateRate.SIM_200ms)
				.Transition(this.active, (HeatCompressor.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational && smi.CanWork(), UpdateRate.SIM_200ms);
			this.active.Enter(delegate(HeatCompressor.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, false);
				smi.StartNewHeatRemoval();
			}).PlayAnim("working_loop", KAnim.PlayMode.Loop).Update(delegate(HeatCompressor.StatesInstance smi, float dt)
			{
				smi.master.time_active += dt;
				smi.UpdateMeter();
				smi.master.CompressHeat(smi, dt);
			}, UpdateRate.SIM_200ms, false)
				.Transition(this.dropCube, (HeatCompressor.StatesInstance smi) => smi.GetRemainingCharge() >= 1f, UpdateRate.SIM_200ms)
				.Transition(this.inactive, (HeatCompressor.StatesInstance smi) => !smi.CanWork(), UpdateRate.SIM_200ms)
				.Exit(delegate(HeatCompressor.StatesInstance smi)
				{
					smi.GetComponent<Operational>().SetActive(false, false);
				});
			this.dropCube.Enter(delegate(HeatCompressor.StatesInstance smi)
			{
				smi.master.EjectHeatCube();
				smi.GoTo(this.inactive);
			});
		}

		// Token: 0x04005D24 RID: 23844
		public GameStateMachine<HeatCompressor.States, HeatCompressor.StatesInstance, HeatCompressor, object>.State active;

		// Token: 0x04005D25 RID: 23845
		public GameStateMachine<HeatCompressor.States, HeatCompressor.StatesInstance, HeatCompressor, object>.State inactive;

		// Token: 0x04005D26 RID: 23846
		public GameStateMachine<HeatCompressor.States, HeatCompressor.StatesInstance, HeatCompressor, object>.State dropCube;
	}
}
