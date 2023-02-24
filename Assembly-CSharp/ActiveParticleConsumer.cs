using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class ActiveParticleConsumer : GameStateMachine<ActiveParticleConsumer, ActiveParticleConsumer.Instance, IStateMachineTarget, ActiveParticleConsumer.Def>
{
	// Token: 0x06000159 RID: 345 RVA: 0x000099CC File Offset: 0x00007BCC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.root.Enter(delegate(ActiveParticleConsumer.Instance smi)
		{
			smi.GetComponent<Operational>().SetFlag(ActiveParticleConsumer.canConsumeParticlesFlag, false);
		});
		this.inoperational.EventTransition(GameHashes.OnParticleStorageChanged, this.operational, new StateMachine<ActiveParticleConsumer, ActiveParticleConsumer.Instance, IStateMachineTarget, ActiveParticleConsumer.Def>.Transition.ConditionCallback(this.IsReady)).ToggleStatusItem(Db.Get().BuildingStatusItems.WaitingForHighEnergyParticles, null);
		this.operational.DefaultState(this.operational.waiting).EventTransition(GameHashes.OnParticleStorageChanged, this.inoperational, GameStateMachine<ActiveParticleConsumer, ActiveParticleConsumer.Instance, IStateMachineTarget, ActiveParticleConsumer.Def>.Not(new StateMachine<ActiveParticleConsumer, ActiveParticleConsumer.Instance, IStateMachineTarget, ActiveParticleConsumer.Def>.Transition.ConditionCallback(this.IsReady))).ToggleOperationalFlag(ActiveParticleConsumer.canConsumeParticlesFlag);
		this.operational.waiting.EventTransition(GameHashes.ActiveChanged, this.operational.consuming, (ActiveParticleConsumer.Instance smi) => smi.GetComponent<Operational>().IsActive);
		this.operational.consuming.EventTransition(GameHashes.ActiveChanged, this.operational.waiting, (ActiveParticleConsumer.Instance smi) => !smi.GetComponent<Operational>().IsActive).Update(delegate(ActiveParticleConsumer.Instance smi, float dt)
		{
			smi.Update(dt);
		}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00009B2C File Offset: 0x00007D2C
	public bool IsReady(ActiveParticleConsumer.Instance smi)
	{
		return smi.storage.Particles >= smi.def.minParticlesForOperational;
	}

	// Token: 0x040000B7 RID: 183
	public static Operational.Flag canConsumeParticlesFlag = new Operational.Flag("canConsumeParticles", Operational.Flag.Type.Requirement);

	// Token: 0x040000B8 RID: 184
	public GameStateMachine<ActiveParticleConsumer, ActiveParticleConsumer.Instance, IStateMachineTarget, ActiveParticleConsumer.Def>.State inoperational;

	// Token: 0x040000B9 RID: 185
	public ActiveParticleConsumer.OperationalStates operational;

	// Token: 0x02000DC3 RID: 3523
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06006ABE RID: 27326 RVA: 0x00295900 File Offset: 0x00293B00
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			return new List<Descriptor>
			{
				new Descriptor(UI.BUILDINGEFFECTS.ACTIVE_PARTICLE_CONSUMPTION.Replace("{Rate}", GameUtil.GetFormattedHighEnergyParticles(this.activeConsumptionRate, GameUtil.TimeSlice.PerSecond, true)), UI.BUILDINGEFFECTS.TOOLTIPS.ACTIVE_PARTICLE_CONSUMPTION.Replace("{Rate}", GameUtil.GetFormattedHighEnergyParticles(this.activeConsumptionRate, GameUtil.TimeSlice.PerSecond, true)), Descriptor.DescriptorType.Requirement, false)
			};
		}

		// Token: 0x04005033 RID: 20531
		public float activeConsumptionRate = 1f;

		// Token: 0x04005034 RID: 20532
		public float minParticlesForOperational = 1f;

		// Token: 0x04005035 RID: 20533
		public string meterSymbolName;
	}

	// Token: 0x02000DC4 RID: 3524
	public class OperationalStates : GameStateMachine<ActiveParticleConsumer, ActiveParticleConsumer.Instance, IStateMachineTarget, ActiveParticleConsumer.Def>.State
	{
		// Token: 0x04005036 RID: 20534
		public GameStateMachine<ActiveParticleConsumer, ActiveParticleConsumer.Instance, IStateMachineTarget, ActiveParticleConsumer.Def>.State waiting;

		// Token: 0x04005037 RID: 20535
		public GameStateMachine<ActiveParticleConsumer, ActiveParticleConsumer.Instance, IStateMachineTarget, ActiveParticleConsumer.Def>.State consuming;
	}

	// Token: 0x02000DC5 RID: 3525
	public new class Instance : GameStateMachine<ActiveParticleConsumer, ActiveParticleConsumer.Instance, IStateMachineTarget, ActiveParticleConsumer.Def>.GameInstance
	{
		// Token: 0x06006AC1 RID: 27329 RVA: 0x0029597D File Offset: 0x00293B7D
		public Instance(IStateMachineTarget master, ActiveParticleConsumer.Def def)
			: base(master, def)
		{
			this.storage = master.GetComponent<HighEnergyParticleStorage>();
		}

		// Token: 0x06006AC2 RID: 27330 RVA: 0x00295993 File Offset: 0x00293B93
		public void Update(float dt)
		{
			this.storage.ConsumeAndGet(dt * base.def.activeConsumptionRate);
		}

		// Token: 0x04005038 RID: 20536
		public bool ShowWorkingStatus;

		// Token: 0x04005039 RID: 20537
		public HighEnergyParticleStorage storage;
	}
}
