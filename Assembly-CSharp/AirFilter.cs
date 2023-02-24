using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000543 RID: 1347
[SerializationConfig(MemberSerialization.OptIn)]
public class AirFilter : StateMachineComponent<AirFilter.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06002025 RID: 8229 RVA: 0x000AFB57 File Offset: 0x000ADD57
	public bool HasFilter()
	{
		return this.elementConverter.HasEnoughMass(this.filterTag, false);
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x000AFB6B File Offset: 0x000ADD6B
	public bool IsConvertable()
	{
		return this.elementConverter.HasEnoughMassToStartConverting(false);
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x000AFB79 File Offset: 0x000ADD79
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x000AFB8C File Offset: 0x000ADD8C
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return null;
	}

	// Token: 0x0400126A RID: 4714
	[MyCmpGet]
	private Operational operational;

	// Token: 0x0400126B RID: 4715
	[MyCmpGet]
	private Storage storage;

	// Token: 0x0400126C RID: 4716
	[MyCmpGet]
	private ElementConverter elementConverter;

	// Token: 0x0400126D RID: 4717
	[MyCmpGet]
	private ElementConsumer elementConsumer;

	// Token: 0x0400126E RID: 4718
	public Tag filterTag;

	// Token: 0x02001174 RID: 4468
	public class StatesInstance : GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter, object>.GameInstance
	{
		// Token: 0x060076A1 RID: 30369 RVA: 0x002B853A File Offset: 0x002B673A
		public StatesInstance(AirFilter smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001175 RID: 4469
	public class States : GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter>
	{
		// Token: 0x060076A2 RID: 30370 RVA: 0x002B8544 File Offset: 0x002B6744
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.waiting;
			this.waiting.EventTransition(GameHashes.OnStorageChange, this.hasFilter, (AirFilter.StatesInstance smi) => smi.master.HasFilter() && smi.master.operational.IsOperational).EventTransition(GameHashes.OperationalChanged, this.hasFilter, (AirFilter.StatesInstance smi) => smi.master.HasFilter() && smi.master.operational.IsOperational);
			this.hasFilter.EventTransition(GameHashes.OperationalChanged, this.waiting, (AirFilter.StatesInstance smi) => !smi.master.operational.IsOperational).Enter("EnableConsumption", delegate(AirFilter.StatesInstance smi)
			{
				smi.master.elementConsumer.EnableConsumption(true);
			}).Exit("DisableConsumption", delegate(AirFilter.StatesInstance smi)
			{
				smi.master.elementConsumer.EnableConsumption(false);
			})
				.DefaultState(this.hasFilter.idle);
			this.hasFilter.idle.EventTransition(GameHashes.OnStorageChange, this.hasFilter.converting, (AirFilter.StatesInstance smi) => smi.master.IsConvertable());
			this.hasFilter.converting.Enter("SetActive(true)", delegate(AirFilter.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit("SetActive(false)", delegate(AirFilter.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).EventTransition(GameHashes.OnStorageChange, this.hasFilter.idle, (AirFilter.StatesInstance smi) => !smi.master.IsConvertable());
		}

		// Token: 0x04005ADA RID: 23258
		public AirFilter.States.ReadyStates hasFilter;

		// Token: 0x04005ADB RID: 23259
		public GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter, object>.State waiting;

		// Token: 0x02001F81 RID: 8065
		public class ReadyStates : GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter, object>.State
		{
			// Token: 0x04008C00 RID: 35840
			public GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter, object>.State idle;

			// Token: 0x04008C01 RID: 35841
			public GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter, object>.State converting;
		}
	}
}
