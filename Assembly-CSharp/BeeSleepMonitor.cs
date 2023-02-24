using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class BeeSleepMonitor : GameStateMachine<BeeSleepMonitor, BeeSleepMonitor.Instance, IStateMachineTarget, BeeSleepMonitor.Def>
{
	// Token: 0x060002FF RID: 767 RVA: 0x00017F4E File Offset: 0x0001614E
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Update(new Action<BeeSleepMonitor.Instance, float>(this.UpdateCO2Exposure), UpdateRate.SIM_1000ms, false).ToggleBehaviour(GameTags.Creatures.BeeWantsToSleep, new StateMachine<BeeSleepMonitor, BeeSleepMonitor.Instance, IStateMachineTarget, BeeSleepMonitor.Def>.Transition.ConditionCallback(this.ShouldSleep), null);
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00017F89 File Offset: 0x00016189
	public bool ShouldSleep(BeeSleepMonitor.Instance smi)
	{
		return smi.CO2Exposure >= 5f;
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00017F9C File Offset: 0x0001619C
	public void UpdateCO2Exposure(BeeSleepMonitor.Instance smi, float dt)
	{
		if (this.IsInCO2(smi))
		{
			smi.CO2Exposure += 1f;
		}
		else
		{
			smi.CO2Exposure -= 0.5f;
		}
		smi.CO2Exposure = Mathf.Clamp(smi.CO2Exposure, 0f, 10f);
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00017FF4 File Offset: 0x000161F4
	public bool IsInCO2(BeeSleepMonitor.Instance smi)
	{
		int num = Grid.PosToCell(smi.gameObject);
		return Grid.IsValidCell(num) && Grid.Element[num].id == SimHashes.CarbonDioxide;
	}

	// Token: 0x02000E1B RID: 3611
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E1C RID: 3612
	public new class Instance : GameStateMachine<BeeSleepMonitor, BeeSleepMonitor.Instance, IStateMachineTarget, BeeSleepMonitor.Def>.GameInstance
	{
		// Token: 0x06006B8F RID: 27535 RVA: 0x00296A7B File Offset: 0x00294C7B
		public Instance(IStateMachineTarget master, BeeSleepMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x040050E3 RID: 20707
		public float CO2Exposure;
	}
}
