using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200063E RID: 1598
[AddComponentMenu("KMonoBehaviour/Workable/Shower")]
public class Shower : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x06002A45 RID: 10821 RVA: 0x000DF5BD File Offset: 0x000DD7BD
	private Shower()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06002A46 RID: 10822 RVA: 0x000DF5CD File Offset: 0x000DD7CD
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.resetProgressOnStop = true;
		this.smi = new Shower.ShowerSM.Instance(this);
		this.smi.StartSM();
	}

	// Token: 0x06002A47 RID: 10823 RVA: 0x000DF5F4 File Offset: 0x000DD7F4
	protected override void OnStartWork(Worker worker)
	{
		HygieneMonitor.Instance instance = worker.GetSMI<HygieneMonitor.Instance>();
		base.WorkTimeRemaining = this.workTime * instance.GetDirtiness();
		this.accumulatedDisease = SimUtil.DiseaseInfo.Invalid;
		this.smi.SetActive(true);
		base.OnStartWork(worker);
	}

	// Token: 0x06002A48 RID: 10824 RVA: 0x000DF639 File Offset: 0x000DD839
	protected override void OnStopWork(Worker worker)
	{
		this.smi.SetActive(false);
	}

	// Token: 0x06002A49 RID: 10825 RVA: 0x000DF648 File Offset: 0x000DD848
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		Effects component = worker.GetComponent<Effects>();
		for (int i = 0; i < Shower.EffectsRemoved.Length; i++)
		{
			string text = Shower.EffectsRemoved[i];
			component.Remove(text);
		}
		if (!worker.HasTag(GameTags.HasSuitTank))
		{
			GasLiquidExposureMonitor.Instance instance = worker.GetSMI<GasLiquidExposureMonitor.Instance>();
			if (instance != null)
			{
				instance.ResetExposure();
			}
		}
		component.Add(Shower.SHOWER_EFFECT, true);
		HygieneMonitor.Instance instance2 = worker.GetSMI<HygieneMonitor.Instance>();
		if (instance2 != null)
		{
			instance2.SetDirtiness(0f);
		}
	}

	// Token: 0x06002A4A RID: 10826 RVA: 0x000DF6C8 File Offset: 0x000DD8C8
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		PrimaryElement component = worker.GetComponent<PrimaryElement>();
		if (component.DiseaseCount > 0)
		{
			SimUtil.DiseaseInfo diseaseInfo = new SimUtil.DiseaseInfo
			{
				idx = component.DiseaseIdx,
				count = Mathf.CeilToInt((float)component.DiseaseCount * (1f - Mathf.Pow(this.fractionalDiseaseRemoval, dt)) - (float)this.absoluteDiseaseRemoval)
			};
			component.ModifyDiseaseCount(-diseaseInfo.count, "Shower.RemoveDisease");
			this.accumulatedDisease = SimUtil.CalculateFinalDiseaseInfo(this.accumulatedDisease, diseaseInfo);
			PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(this.outputTargetElement);
			if (primaryElement != null)
			{
				primaryElement.GetComponent<PrimaryElement>().AddDisease(this.accumulatedDisease.idx, this.accumulatedDisease.count, "Shower.RemoveDisease");
				this.accumulatedDisease = SimUtil.DiseaseInfo.Invalid;
			}
		}
		return false;
	}

	// Token: 0x06002A4B RID: 10827 RVA: 0x000DF7A0 File Offset: 0x000DD9A0
	protected override void OnAbortWork(Worker worker)
	{
		base.OnAbortWork(worker);
		HygieneMonitor.Instance instance = worker.GetSMI<HygieneMonitor.Instance>();
		if (instance != null)
		{
			instance.SetDirtiness(1f - this.GetPercentComplete());
		}
	}

	// Token: 0x06002A4C RID: 10828 RVA: 0x000DF7D0 File Offset: 0x000DD9D0
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		if (Shower.EffectsRemoved.Length != 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.REMOVESEFFECTSUBTITLE, UI.BUILDINGEFFECTS.TOOLTIPS.REMOVESEFFECTSUBTITLE, Descriptor.DescriptorType.Effect);
			descriptors.Add(descriptor);
			for (int i = 0; i < Shower.EffectsRemoved.Length; i++)
			{
				string text = Shower.EffectsRemoved[i];
				string text2 = Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text.ToUpper() + ".NAME");
				string text3 = Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text.ToUpper() + ".CAUSE");
				Descriptor descriptor2 = default(Descriptor);
				descriptor2.IncreaseIndent();
				descriptor2.SetupDescriptor("• " + string.Format(UI.BUILDINGEFFECTS.REMOVEDEFFECT, text2), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.REMOVEDEFFECT, text3), Descriptor.DescriptorType.Effect);
				descriptors.Add(descriptor2);
			}
		}
		Effect.AddModifierDescriptions(base.gameObject, descriptors, Shower.SHOWER_EFFECT, true);
		return descriptors;
	}

	// Token: 0x040018FD RID: 6397
	private Shower.ShowerSM.Instance smi;

	// Token: 0x040018FE RID: 6398
	public static string SHOWER_EFFECT = "Showered";

	// Token: 0x040018FF RID: 6399
	public SimHashes outputTargetElement;

	// Token: 0x04001900 RID: 6400
	public float fractionalDiseaseRemoval;

	// Token: 0x04001901 RID: 6401
	public int absoluteDiseaseRemoval;

	// Token: 0x04001902 RID: 6402
	private SimUtil.DiseaseInfo accumulatedDisease;

	// Token: 0x04001903 RID: 6403
	public const float WATER_PER_USE = 5f;

	// Token: 0x04001904 RID: 6404
	private static readonly string[] EffectsRemoved = new string[] { "SoakingWet", "WetFeet", "MinorIrritation", "MajorIrritation" };

	// Token: 0x020012D8 RID: 4824
	public class ShowerSM : GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower>
	{
		// Token: 0x06007BCB RID: 31691 RVA: 0x002CD05C File Offset: 0x002CB25C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.root.Update(new Action<Shower.ShowerSM.Instance, float>(this.UpdateStatusItems), UpdateRate.SIM_200ms, false);
			this.unoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (Shower.ShowerSM.Instance smi) => smi.IsOperational).PlayAnim("off");
			this.operational.DefaultState(this.operational.not_ready).EventTransition(GameHashes.OperationalChanged, this.unoperational, (Shower.ShowerSM.Instance smi) => !smi.IsOperational);
			this.operational.not_ready.EventTransition(GameHashes.OnStorageChange, this.operational.ready, (Shower.ShowerSM.Instance smi) => smi.IsReady()).PlayAnim("off");
			this.operational.ready.ToggleChore(new Func<Shower.ShowerSM.Instance, Chore>(this.CreateShowerChore), this.operational.not_ready);
		}

		// Token: 0x06007BCC RID: 31692 RVA: 0x002CD184 File Offset: 0x002CB384
		private Chore CreateShowerChore(Shower.ShowerSM.Instance smi)
		{
			return new WorkChore<Shower>(Db.Get().ChoreTypes.Shower, smi.master, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Hygiene, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
		}

		// Token: 0x06007BCD RID: 31693 RVA: 0x002CD1CC File Offset: 0x002CB3CC
		private void UpdateStatusItems(Shower.ShowerSM.Instance smi, float dt)
		{
			if (smi.OutputFull())
			{
				smi.master.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.OutputPipeFull, this);
				return;
			}
			smi.master.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.OutputPipeFull, false);
		}

		// Token: 0x04005EDF RID: 24287
		public GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower, object>.State unoperational;

		// Token: 0x04005EE0 RID: 24288
		public Shower.ShowerSM.OperationalState operational;

		// Token: 0x02001FF4 RID: 8180
		public class OperationalState : GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower, object>.State
		{
			// Token: 0x04008E7E RID: 36478
			public GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower, object>.State not_ready;

			// Token: 0x04008E7F RID: 36479
			public GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower, object>.State ready;
		}

		// Token: 0x02001FF5 RID: 8181
		public new class Instance : GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower, object>.GameInstance
		{
			// Token: 0x0600A1AB RID: 41387 RVA: 0x003435F9 File Offset: 0x003417F9
			public Instance(Shower master)
				: base(master)
			{
				this.operational = master.GetComponent<Operational>();
				this.consumer = master.GetComponent<ConduitConsumer>();
				this.dispenser = master.GetComponent<ConduitDispenser>();
			}

			// Token: 0x17000A00 RID: 2560
			// (get) Token: 0x0600A1AC RID: 41388 RVA: 0x00343626 File Offset: 0x00341826
			public bool IsOperational
			{
				get
				{
					return this.operational.IsOperational && this.consumer.IsConnected && this.dispenser.IsConnected;
				}
			}

			// Token: 0x0600A1AD RID: 41389 RVA: 0x0034364F File Offset: 0x0034184F
			public void SetActive(bool active)
			{
				this.operational.SetActive(active, false);
			}

			// Token: 0x0600A1AE RID: 41390 RVA: 0x00343660 File Offset: 0x00341860
			private bool HasSufficientMass()
			{
				bool flag = false;
				PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(SimHashes.Water);
				if (primaryElement != null)
				{
					flag = primaryElement.Mass >= 5f;
				}
				return flag;
			}

			// Token: 0x0600A1AF RID: 41391 RVA: 0x0034369C File Offset: 0x0034189C
			public bool OutputFull()
			{
				PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(SimHashes.DirtyWater);
				return primaryElement != null && primaryElement.Mass >= 5f;
			}

			// Token: 0x0600A1B0 RID: 41392 RVA: 0x003436D5 File Offset: 0x003418D5
			public bool IsReady()
			{
				return this.HasSufficientMass() && !this.OutputFull();
			}

			// Token: 0x04008E80 RID: 36480
			private Operational operational;

			// Token: 0x04008E81 RID: 36481
			private ConduitConsumer consumer;

			// Token: 0x04008E82 RID: 36482
			private ConduitDispenser dispenser;
		}
	}
}
