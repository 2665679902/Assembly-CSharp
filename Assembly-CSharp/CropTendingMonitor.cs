using System;
using UnityEngine;

// Token: 0x020006CB RID: 1739
public class CropTendingMonitor : GameStateMachine<CropTendingMonitor, CropTendingMonitor.Instance, IStateMachineTarget, CropTendingMonitor.Def>
{
	// Token: 0x06002F50 RID: 12112 RVA: 0x000F9FE9 File Offset: 0x000F81E9
	private bool InterestedInTendingCrops(CropTendingMonitor.Instance smi)
	{
		return !smi.HasTag(GameTags.Creatures.Hungry) || UnityEngine.Random.value <= smi.def.unsatisfiedTendChance;
	}

	// Token: 0x06002F51 RID: 12113 RVA: 0x000FA010 File Offset: 0x000F8210
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.cooldown;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.cooldown.ParamTransition<float>(this.cooldownTimer, this.lookingForCrop, (CropTendingMonitor.Instance smi, float p) => this.cooldownTimer.Get(smi) <= 0f && this.InterestedInTendingCrops(smi)).ParamTransition<float>(this.cooldownTimer, this.reset, (CropTendingMonitor.Instance smi, float p) => this.cooldownTimer.Get(smi) <= 0f && !this.InterestedInTendingCrops(smi)).Update(delegate(CropTendingMonitor.Instance smi, float dt)
		{
			this.cooldownTimer.Delta(-dt, smi);
		}, UpdateRate.SIM_1000ms, false);
		this.lookingForCrop.ToggleBehaviour(GameTags.Creatures.WantsToTendCrops, (CropTendingMonitor.Instance smi) => true, delegate(CropTendingMonitor.Instance smi)
		{
			smi.GoTo(this.reset);
		});
		this.reset.Exit(delegate(CropTendingMonitor.Instance smi)
		{
			this.cooldownTimer.Set(600f / smi.def.numCropsTendedPerCycle, smi, false);
		}).GoTo(this.cooldown);
	}

	// Token: 0x04001C6F RID: 7279
	private StateMachine<CropTendingMonitor, CropTendingMonitor.Instance, IStateMachineTarget, CropTendingMonitor.Def>.FloatParameter cooldownTimer;

	// Token: 0x04001C70 RID: 7280
	private GameStateMachine<CropTendingMonitor, CropTendingMonitor.Instance, IStateMachineTarget, CropTendingMonitor.Def>.State cooldown;

	// Token: 0x04001C71 RID: 7281
	private GameStateMachine<CropTendingMonitor, CropTendingMonitor.Instance, IStateMachineTarget, CropTendingMonitor.Def>.State lookingForCrop;

	// Token: 0x04001C72 RID: 7282
	private GameStateMachine<CropTendingMonitor, CropTendingMonitor.Instance, IStateMachineTarget, CropTendingMonitor.Def>.State reset;

	// Token: 0x020013B4 RID: 5044
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400615C RID: 24924
		public float numCropsTendedPerCycle = 8f;

		// Token: 0x0400615D RID: 24925
		public float unsatisfiedTendChance = 0.5f;
	}

	// Token: 0x020013B5 RID: 5045
	public new class Instance : GameStateMachine<CropTendingMonitor, CropTendingMonitor.Instance, IStateMachineTarget, CropTendingMonitor.Def>.GameInstance
	{
		// Token: 0x06007EB1 RID: 32433 RVA: 0x002D9A2C File Offset: 0x002D7C2C
		public Instance(IStateMachineTarget master, CropTendingMonitor.Def def)
			: base(master, def)
		{
		}
	}
}
