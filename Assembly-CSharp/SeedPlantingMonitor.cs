using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class SeedPlantingMonitor : GameStateMachine<SeedPlantingMonitor, SeedPlantingMonitor.Instance, IStateMachineTarget, SeedPlantingMonitor.Def>
{
	// Token: 0x060003D5 RID: 981 RVA: 0x0001D83C File Offset: 0x0001BA3C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToPlantSeed, new StateMachine<SeedPlantingMonitor, SeedPlantingMonitor.Instance, IStateMachineTarget, SeedPlantingMonitor.Def>.Transition.ConditionCallback(SeedPlantingMonitor.ShouldSearchForSeeds), delegate(SeedPlantingMonitor.Instance smi)
		{
			smi.RefreshSearchTime();
		});
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x0001D88D File Offset: 0x0001BA8D
	public static bool ShouldSearchForSeeds(SeedPlantingMonitor.Instance smi)
	{
		return Time.time >= smi.nextSearchTime;
	}

	// Token: 0x02000EA9 RID: 3753
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040051EF RID: 20975
		public float searchMinInterval = 60f;

		// Token: 0x040051F0 RID: 20976
		public float searchMaxInterval = 300f;
	}

	// Token: 0x02000EAA RID: 3754
	public new class Instance : GameStateMachine<SeedPlantingMonitor, SeedPlantingMonitor.Instance, IStateMachineTarget, SeedPlantingMonitor.Def>.GameInstance
	{
		// Token: 0x06006CD2 RID: 27858 RVA: 0x00298C0B File Offset: 0x00296E0B
		public Instance(IStateMachineTarget master, SeedPlantingMonitor.Def def)
			: base(master, def)
		{
			this.RefreshSearchTime();
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x00298C1B File Offset: 0x00296E1B
		public void RefreshSearchTime()
		{
			this.nextSearchTime = Time.time + Mathf.Lerp(base.def.searchMinInterval, base.def.searchMaxInterval, UnityEngine.Random.value);
		}

		// Token: 0x040051F1 RID: 20977
		public float nextSearchTime;
	}
}
