using System;
using Klei.AI;

// Token: 0x02000831 RID: 2097
public class HygieneMonitor : GameStateMachine<HygieneMonitor, HygieneMonitor.Instance>
{
	// Token: 0x06003C9C RID: 15516 RVA: 0x00152234 File Offset: 0x00150434
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.needsshower;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.clean.EventTransition(GameHashes.EffectRemoved, this.needsshower, (HygieneMonitor.Instance smi) => smi.NeedsShower());
		this.needsshower.EventTransition(GameHashes.EffectAdded, this.clean, (HygieneMonitor.Instance smi) => !smi.NeedsShower()).ToggleUrge(Db.Get().Urges.Shower).Enter(delegate(HygieneMonitor.Instance smi)
		{
			smi.SetDirtiness(1f);
		});
	}

	// Token: 0x0400278A RID: 10122
	public StateMachine<HygieneMonitor, HygieneMonitor.Instance, IStateMachineTarget, object>.FloatParameter dirtiness;

	// Token: 0x0400278B RID: 10123
	public GameStateMachine<HygieneMonitor, HygieneMonitor.Instance, IStateMachineTarget, object>.State clean;

	// Token: 0x0400278C RID: 10124
	public GameStateMachine<HygieneMonitor, HygieneMonitor.Instance, IStateMachineTarget, object>.State needsshower;

	// Token: 0x020015B6 RID: 5558
	public new class Instance : GameStateMachine<HygieneMonitor, HygieneMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600850D RID: 34061 RVA: 0x002EBD1C File Offset: 0x002E9F1C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.effects = master.GetComponent<Effects>();
		}

		// Token: 0x0600850E RID: 34062 RVA: 0x002EBD31 File Offset: 0x002E9F31
		public float GetDirtiness()
		{
			return base.sm.dirtiness.Get(this);
		}

		// Token: 0x0600850F RID: 34063 RVA: 0x002EBD44 File Offset: 0x002E9F44
		public void SetDirtiness(float dirtiness)
		{
			base.sm.dirtiness.Set(dirtiness, this, false);
		}

		// Token: 0x06008510 RID: 34064 RVA: 0x002EBD5A File Offset: 0x002E9F5A
		public bool NeedsShower()
		{
			return !this.effects.HasEffect(Shower.SHOWER_EFFECT);
		}

		// Token: 0x040067B3 RID: 26547
		private Effects effects;
	}
}
