using System;
using KSerialization;

// Token: 0x0200086F RID: 2159
public class NuclearWaste : GameStateMachine<NuclearWaste, NuclearWaste.Instance, IStateMachineTarget, NuclearWaste.Def>
{
	// Token: 0x06003E03 RID: 15875 RVA: 0x00159F3C File Offset: 0x0015813C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.PlayAnim((NuclearWaste.Instance smi) => smi.GetAnimToPlay(), KAnim.PlayMode.Once).Update(delegate(NuclearWaste.Instance smi, float dt)
		{
			smi.timeAlive += dt;
			string animToPlay = smi.GetAnimToPlay();
			if (smi.GetComponent<KBatchedAnimController>().GetCurrentAnim().name != animToPlay)
			{
				smi.Play(animToPlay, KAnim.PlayMode.Once);
			}
			if (smi.timeAlive >= 600f)
			{
				smi.GoTo(this.decayed);
			}
		}, UpdateRate.SIM_4000ms, false).EventHandler(GameHashes.Absorb, delegate(NuclearWaste.Instance smi, object otherObject)
		{
			Pickupable pickupable = (Pickupable)otherObject;
			float timeAlive = pickupable.GetSMI<NuclearWaste.Instance>().timeAlive;
			float mass = pickupable.GetComponent<PrimaryElement>().Mass;
			float mass2 = smi.master.GetComponent<PrimaryElement>().Mass;
			float num = ((mass2 - mass) * smi.timeAlive + mass * timeAlive) / mass2;
			smi.timeAlive = num;
			string animToPlay2 = smi.GetAnimToPlay();
			if (smi.GetComponent<KBatchedAnimController>().GetCurrentAnim().name != animToPlay2)
			{
				smi.Play(animToPlay2, KAnim.PlayMode.Once);
			}
			if (smi.timeAlive >= 600f)
			{
				smi.GoTo(this.decayed);
			}
		});
		this.decayed.Enter(delegate(NuclearWaste.Instance smi)
		{
			smi.GetComponent<Dumpable>().Dump();
			Util.KDestroyGameObject(smi.master.gameObject);
		});
	}

	// Token: 0x0400289C RID: 10396
	private const float lifetime = 600f;

	// Token: 0x0400289D RID: 10397
	public GameStateMachine<NuclearWaste, NuclearWaste.Instance, IStateMachineTarget, NuclearWaste.Def>.State idle;

	// Token: 0x0400289E RID: 10398
	public GameStateMachine<NuclearWaste, NuclearWaste.Instance, IStateMachineTarget, NuclearWaste.Def>.State decayed;

	// Token: 0x02001636 RID: 5686
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001637 RID: 5687
	public new class Instance : GameStateMachine<NuclearWaste, NuclearWaste.Instance, IStateMachineTarget, NuclearWaste.Def>.GameInstance
	{
		// Token: 0x0600870E RID: 34574 RVA: 0x002F0D41 File Offset: 0x002EEF41
		public Instance(IStateMachineTarget master, NuclearWaste.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600870F RID: 34575 RVA: 0x002F0D4C File Offset: 0x002EEF4C
		public string GetAnimToPlay()
		{
			this.percentageRemaining = 1f - base.smi.timeAlive / 600f;
			if (this.percentageRemaining <= 0.33f)
			{
				return "idle1";
			}
			if (this.percentageRemaining <= 0.66f)
			{
				return "idle2";
			}
			return "idle3";
		}

		// Token: 0x04006934 RID: 26932
		[Serialize]
		public float timeAlive;

		// Token: 0x04006935 RID: 26933
		private float percentageRemaining;
	}
}
