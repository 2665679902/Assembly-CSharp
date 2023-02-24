using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000627 RID: 1575
[SerializationConfig(MemberSerialization.OptIn)]
public class Polymerizer : StateMachineComponent<Polymerizer.StatesInstance>
{
	// Token: 0x0600294B RID: 10571 RVA: 0x000DA494 File Offset: 0x000D8694
	protected override void OnSpawn()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.plasticMeter = new MeterController(component, "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new Vector3(0f, 0f, 0f), null);
		this.oilMeter = new MeterController(component, "meter2_target", "meter2", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new Vector3(0f, 0f, 0f), null);
		component.SetSymbolVisiblity("meter_target", true);
		float num = 0f;
		PrimaryElement primaryElement = this.storage.FindPrimaryElement(SimHashes.Petroleum);
		if (primaryElement != null)
		{
			num = Mathf.Clamp01(primaryElement.Mass / this.consumer.capacityKG);
		}
		this.oilMeter.SetPositionPercent(num);
		base.smi.StartSM();
		base.Subscribe<Polymerizer>(-1697596308, Polymerizer.OnStorageChangedDelegate);
	}

	// Token: 0x0600294C RID: 10572 RVA: 0x000DA578 File Offset: 0x000D8778
	private void TryEmit()
	{
		GameObject gameObject = this.storage.FindFirst(this.emitTag);
		if (gameObject != null)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			this.UpdatePercentDone(component);
			this.TryEmit(component);
		}
	}

	// Token: 0x0600294D RID: 10573 RVA: 0x000DA5B8 File Offset: 0x000D87B8
	private void TryEmit(PrimaryElement primary_elem)
	{
		if (primary_elem.Mass >= this.emitMass)
		{
			this.plasticMeter.SetPositionPercent(0f);
			GameObject gameObject = this.storage.Drop(primary_elem.gameObject, true);
			Rotatable component = base.GetComponent<Rotatable>();
			Vector3 vector = component.transform.GetPosition() + component.GetRotatedOffset(this.emitOffset);
			int num = Grid.PosToCell(vector);
			if (Grid.Solid[num])
			{
				vector += component.GetRotatedOffset(Vector3.left);
			}
			gameObject.transform.SetPosition(vector);
			PrimaryElement primaryElement = this.storage.FindPrimaryElement(this.exhaustElement);
			if (primaryElement != null)
			{
				SimMessages.AddRemoveSubstance(Grid.PosToCell(vector), primaryElement.ElementID, null, primaryElement.Mass, primaryElement.Temperature, primaryElement.DiseaseIdx, primaryElement.DiseaseCount, true, -1);
				primaryElement.Mass = 0f;
				primaryElement.ModifyDiseaseCount(int.MinValue, "Polymerizer.Exhaust");
			}
		}
	}

	// Token: 0x0600294E RID: 10574 RVA: 0x000DA6B0 File Offset: 0x000D88B0
	private void UpdatePercentDone(PrimaryElement primary_elem)
	{
		float num = Mathf.Clamp01(primary_elem.Mass / this.emitMass);
		this.plasticMeter.SetPositionPercent(num);
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x000DA6DC File Offset: 0x000D88DC
	private void OnStorageChanged(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		if (component.ElementID == SimHashes.Petroleum)
		{
			float num = Mathf.Clamp01(component.Mass / this.consumer.capacityKG);
			this.oilMeter.SetPositionPercent(num);
		}
	}

	// Token: 0x0400184F RID: 6223
	[SerializeField]
	public float maxMass = 2.5f;

	// Token: 0x04001850 RID: 6224
	[SerializeField]
	public float emitMass = 1f;

	// Token: 0x04001851 RID: 6225
	[SerializeField]
	public Tag emitTag;

	// Token: 0x04001852 RID: 6226
	[SerializeField]
	public Vector3 emitOffset = Vector3.zero;

	// Token: 0x04001853 RID: 6227
	[SerializeField]
	public SimHashes exhaustElement = SimHashes.Vacuum;

	// Token: 0x04001854 RID: 6228
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001855 RID: 6229
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001856 RID: 6230
	[MyCmpGet]
	private ConduitConsumer consumer;

	// Token: 0x04001857 RID: 6231
	[MyCmpGet]
	private ElementConverter converter;

	// Token: 0x04001858 RID: 6232
	private MeterController plasticMeter;

	// Token: 0x04001859 RID: 6233
	private MeterController oilMeter;

	// Token: 0x0400185A RID: 6234
	private static readonly EventSystem.IntraObjectHandler<Polymerizer> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<Polymerizer>(delegate(Polymerizer component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x020012A6 RID: 4774
	public class StatesInstance : GameStateMachine<Polymerizer.States, Polymerizer.StatesInstance, Polymerizer, object>.GameInstance
	{
		// Token: 0x06007AFA RID: 31482 RVA: 0x002C9A91 File Offset: 0x002C7C91
		public StatesInstance(Polymerizer smi)
			: base(smi)
		{
		}
	}

	// Token: 0x020012A7 RID: 4775
	public class States : GameStateMachine<Polymerizer.States, Polymerizer.StatesInstance, Polymerizer>
	{
		// Token: 0x06007AFB RID: 31483 RVA: 0x002C9A9C File Offset: 0x002C7C9C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.EventTransition(GameHashes.OperationalChanged, this.off, (Polymerizer.StatesInstance smi) => !smi.master.operational.IsOperational);
			this.off.EventTransition(GameHashes.OperationalChanged, this.on, (Polymerizer.StatesInstance smi) => smi.master.operational.IsOperational);
			this.on.EventTransition(GameHashes.OnStorageChange, this.converting, (Polymerizer.StatesInstance smi) => smi.master.converter.CanConvertAtAll());
			this.converting.Enter("Ready", delegate(Polymerizer.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).EventHandler(GameHashes.OnStorageChange, delegate(Polymerizer.StatesInstance smi)
			{
				smi.master.TryEmit();
			}).EventTransition(GameHashes.OnStorageChange, this.on, (Polymerizer.StatesInstance smi) => !smi.master.converter.CanConvertAtAll())
				.Exit("Ready", delegate(Polymerizer.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
				});
		}

		// Token: 0x04005E47 RID: 24135
		public GameStateMachine<Polymerizer.States, Polymerizer.StatesInstance, Polymerizer, object>.State off;

		// Token: 0x04005E48 RID: 24136
		public GameStateMachine<Polymerizer.States, Polymerizer.StatesInstance, Polymerizer, object>.State on;

		// Token: 0x04005E49 RID: 24137
		public GameStateMachine<Polymerizer.States, Polymerizer.StatesInstance, Polymerizer, object>.State converting;
	}
}
