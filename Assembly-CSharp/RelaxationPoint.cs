using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020008B8 RID: 2232
[AddComponentMenu("KMonoBehaviour/Workable/RelaxationPoint")]
public class RelaxationPoint : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x0600403D RID: 16445 RVA: 0x00166B9A File Offset: 0x00164D9A
	public RelaxationPoint()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
		this.showProgressBar = false;
	}

	// Token: 0x0600403E RID: 16446 RVA: 0x00166BB4 File Offset: 0x00164DB4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.lightEfficiencyBonus = false;
		base.GetComponent<KPrefabID>().AddTag(TagManager.Create("RelaxationPoint", MISC.TAGS.RELAXATION_POINT), false);
		if (RelaxationPoint.stressReductionEffect == null)
		{
			RelaxationPoint.stressReductionEffect = this.CreateEffect();
			RelaxationPoint.roomStressReductionEffect = this.CreateRoomEffect();
		}
	}

	// Token: 0x0600403F RID: 16447 RVA: 0x00166C0C File Offset: 0x00164E0C
	public Effect CreateEffect()
	{
		Effect effect = new Effect("StressReduction", DUPLICANTS.MODIFIERS.STRESSREDUCTION.NAME, DUPLICANTS.MODIFIERS.STRESSREDUCTION.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		AttributeModifier attributeModifier = new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, this.stressModificationValue / 600f, DUPLICANTS.MODIFIERS.STRESSREDUCTION.NAME, false, false, true);
		effect.Add(attributeModifier);
		return effect;
	}

	// Token: 0x06004040 RID: 16448 RVA: 0x00166C90 File Offset: 0x00164E90
	public Effect CreateRoomEffect()
	{
		Effect effect = new Effect("RoomRelaxationEffect", DUPLICANTS.MODIFIERS.STRESSREDUCTION_CLINIC.NAME, DUPLICANTS.MODIFIERS.STRESSREDUCTION_CLINIC.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		AttributeModifier attributeModifier = new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, this.roomStressModificationValue / 600f, DUPLICANTS.MODIFIERS.STRESSREDUCTION_CLINIC.NAME, false, false, true);
		effect.Add(attributeModifier);
		return effect;
	}

	// Token: 0x06004041 RID: 16449 RVA: 0x00166D13 File Offset: 0x00164F13
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new RelaxationPoint.RelaxationPointSM.Instance(this);
		this.smi.StartSM();
		base.SetWorkTime(float.PositiveInfinity);
	}

	// Token: 0x06004042 RID: 16450 RVA: 0x00166D40 File Offset: 0x00164F40
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		if (this.roomTracker != null && this.roomTracker.room != null && this.roomTracker.room.roomType == Db.Get().RoomTypes.MassageClinic)
		{
			worker.GetComponent<Effects>().Add(RelaxationPoint.roomStressReductionEffect, false);
		}
		else
		{
			worker.GetComponent<Effects>().Add(RelaxationPoint.stressReductionEffect, false);
		}
		base.GetComponent<Operational>().SetActive(true, false);
	}

	// Token: 0x06004043 RID: 16451 RVA: 0x00166DC3 File Offset: 0x00164FC3
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (Db.Get().Amounts.Stress.Lookup(worker.gameObject).value <= this.stopStressingValue)
		{
			return true;
		}
		base.OnWorkTick(worker, dt);
		return false;
	}

	// Token: 0x06004044 RID: 16452 RVA: 0x00166DF8 File Offset: 0x00164FF8
	protected override void OnStopWork(Worker worker)
	{
		worker.GetComponent<Effects>().Remove(RelaxationPoint.stressReductionEffect);
		worker.GetComponent<Effects>().Remove(RelaxationPoint.roomStressReductionEffect);
		base.GetComponent<Operational>().SetActive(false, false);
		base.OnStopWork(worker);
	}

	// Token: 0x06004045 RID: 16453 RVA: 0x00166E2E File Offset: 0x0016502E
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
	}

	// Token: 0x06004046 RID: 16454 RVA: 0x00166E37 File Offset: 0x00165037
	public override bool InstantlyFinish(Worker worker)
	{
		return false;
	}

	// Token: 0x06004047 RID: 16455 RVA: 0x00166E3C File Offset: 0x0016503C
	protected virtual WorkChore<RelaxationPoint> CreateWorkChore()
	{
		return new WorkChore<RelaxationPoint>(Db.Get().ChoreTypes.Relax, this, null, false, null, null, null, false, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x06004048 RID: 16456 RVA: 0x00166E70 File Offset: 0x00165070
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.STRESSREDUCEDPERMINUTE, GameUtil.GetFormattedPercent(this.stressModificationValue / 600f * 60f, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.STRESSREDUCEDPERMINUTE, GameUtil.GetFormattedPercent(this.stressModificationValue / 600f * 60f, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect);
		descriptors.Add(descriptor);
		return descriptors;
	}

	// Token: 0x04002A1B RID: 10779
	[MyCmpGet]
	private RoomTracker roomTracker;

	// Token: 0x04002A1C RID: 10780
	[Serialize]
	protected float stopStressingValue;

	// Token: 0x04002A1D RID: 10781
	public float stressModificationValue;

	// Token: 0x04002A1E RID: 10782
	public float roomStressModificationValue;

	// Token: 0x04002A1F RID: 10783
	private RelaxationPoint.RelaxationPointSM.Instance smi;

	// Token: 0x04002A20 RID: 10784
	private static Effect stressReductionEffect;

	// Token: 0x04002A21 RID: 10785
	private static Effect roomStressReductionEffect;

	// Token: 0x0200168B RID: 5771
	public class RelaxationPointSM : GameStateMachine<RelaxationPoint.RelaxationPointSM, RelaxationPoint.RelaxationPointSM.Instance, RelaxationPoint>
	{
		// Token: 0x060087F0 RID: 34800 RVA: 0x002F44C4 File Offset: 0x002F26C4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.unoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (RelaxationPoint.RelaxationPointSM.Instance smi) => smi.GetComponent<Operational>().IsOperational).PlayAnim("off");
			this.operational.ToggleChore((RelaxationPoint.RelaxationPointSM.Instance smi) => smi.master.CreateWorkChore(), this.unoperational);
		}

		// Token: 0x04006A1C RID: 27164
		public GameStateMachine<RelaxationPoint.RelaxationPointSM, RelaxationPoint.RelaxationPointSM.Instance, RelaxationPoint, object>.State unoperational;

		// Token: 0x04006A1D RID: 27165
		public GameStateMachine<RelaxationPoint.RelaxationPointSM, RelaxationPoint.RelaxationPointSM.Instance, RelaxationPoint, object>.State operational;

		// Token: 0x020020A6 RID: 8358
		public new class Instance : GameStateMachine<RelaxationPoint.RelaxationPointSM, RelaxationPoint.RelaxationPointSM.Instance, RelaxationPoint, object>.GameInstance
		{
			// Token: 0x0600A494 RID: 42132 RVA: 0x003481BD File Offset: 0x003463BD
			public Instance(RelaxationPoint master)
				: base(master)
			{
			}
		}
	}
}
