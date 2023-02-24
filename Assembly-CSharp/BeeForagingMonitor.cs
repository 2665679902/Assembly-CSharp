using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class BeeForagingMonitor : GameStateMachine<BeeForagingMonitor, BeeForagingMonitor.Instance, IStateMachineTarget, BeeForagingMonitor.Def>
{
	// Token: 0x060002EE RID: 750 RVA: 0x00017960 File Offset: 0x00015B60
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToForage, new StateMachine<BeeForagingMonitor, BeeForagingMonitor.Instance, IStateMachineTarget, BeeForagingMonitor.Def>.Transition.ConditionCallback(BeeForagingMonitor.ShouldForage), delegate(BeeForagingMonitor.Instance smi)
		{
			smi.RefreshSearchTime();
		});
	}

	// Token: 0x060002EF RID: 751 RVA: 0x000179B4 File Offset: 0x00015BB4
	public static bool ShouldForage(BeeForagingMonitor.Instance smi)
	{
		bool flag = GameClock.Instance.GetTimeInCycles() >= smi.nextSearchTime;
		KPrefabID kprefabID = smi.master.GetComponent<Bee>().FindHiveInRoom();
		if (kprefabID != null)
		{
			BeehiveCalorieMonitor.Instance smi2 = kprefabID.GetSMI<BeehiveCalorieMonitor.Instance>();
			if (smi2 == null || !smi2.IsHungry())
			{
				flag = false;
			}
		}
		return flag && kprefabID != null;
	}

	// Token: 0x02000E0D RID: 3597
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040050C9 RID: 20681
		public float searchMinInterval = 0.25f;

		// Token: 0x040050CA RID: 20682
		public float searchMaxInterval = 0.3f;
	}

	// Token: 0x02000E0E RID: 3598
	public new class Instance : GameStateMachine<BeeForagingMonitor, BeeForagingMonitor.Instance, IStateMachineTarget, BeeForagingMonitor.Def>.GameInstance
	{
		// Token: 0x06006B6F RID: 27503 RVA: 0x002967F7 File Offset: 0x002949F7
		public Instance(IStateMachineTarget master, BeeForagingMonitor.Def def)
			: base(master, def)
		{
			this.RefreshSearchTime();
		}

		// Token: 0x06006B70 RID: 27504 RVA: 0x00296807 File Offset: 0x00294A07
		public void RefreshSearchTime()
		{
			this.nextSearchTime = GameClock.Instance.GetTimeInCycles() + Mathf.Lerp(base.def.searchMinInterval, base.def.searchMaxInterval, UnityEngine.Random.value);
		}

		// Token: 0x040050CB RID: 20683
		public float nextSearchTime;
	}
}
