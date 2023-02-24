using System;
using UnityEngine;

// Token: 0x02000372 RID: 882
public class RobotAi : GameStateMachine<RobotAi, RobotAi.Instance>
{
	// Token: 0x06001214 RID: 4628 RVA: 0x0005F9AC File Offset: 0x0005DBAC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleStateMachine((RobotAi.Instance smi) => new DeathMonitor.Instance(smi.master, new DeathMonitor.Def())).Enter(delegate(RobotAi.Instance smi)
		{
			if (smi.HasTag(GameTags.Dead))
			{
				smi.GoTo(this.dead);
				return;
			}
			smi.GoTo(this.alive);
		});
		this.alive.DefaultState(this.alive.normal).TagTransition(GameTags.Dead, this.dead, false);
		this.alive.normal.TagTransition(GameTags.Stored, this.alive.stored, false).ToggleStateMachine((RobotAi.Instance smi) => new FallMonitor.Instance(smi.master, false, null));
		this.alive.stored.PlayAnim("in_storage").TagTransition(GameTags.Stored, this.alive.normal, true).ToggleBrain("stored")
			.Enter(delegate(RobotAi.Instance smi)
			{
				smi.GetComponent<Navigator>().Pause("stored");
			})
			.Exit(delegate(RobotAi.Instance smi)
			{
				smi.GetComponent<Navigator>().Unpause("unstored");
			});
		this.dead.ToggleBrain("dead").ToggleComponent<Deconstructable>(false).ToggleStateMachine((RobotAi.Instance smi) => new FallWhenDeadMonitor.Instance(smi.master))
			.Enter("RefreshUserMenu", delegate(RobotAi.Instance smi)
			{
				smi.RefreshUserMenu();
			})
			.Enter("DropStorage", delegate(RobotAi.Instance smi)
			{
				smi.GetComponent<Storage>().DropAll(false, false, default(Vector3), true, null);
			});
	}

	// Token: 0x040009AE RID: 2478
	public RobotAi.AliveStates alive;

	// Token: 0x040009AF RID: 2479
	public GameStateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.State dead;

	// Token: 0x02000F44 RID: 3908
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000F45 RID: 3909
	public class AliveStates : GameStateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040053AE RID: 21422
		public GameStateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.State normal;

		// Token: 0x040053AF RID: 21423
		public GameStateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.State stored;
	}

	// Token: 0x02000F46 RID: 3910
	public new class Instance : GameStateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06006EC4 RID: 28356 RVA: 0x0029D285 File Offset: 0x0029B485
		public Instance(IStateMachineTarget master, RobotAi.Def def)
			: base(master, def)
		{
			ChoreConsumer component = base.GetComponent<ChoreConsumer>();
			component.AddUrge(Db.Get().Urges.EmoteHighPriority);
			component.AddUrge(Db.Get().Urges.EmoteIdle);
		}

		// Token: 0x06006EC5 RID: 28357 RVA: 0x0029D2BE File Offset: 0x0029B4BE
		public void RefreshUserMenu()
		{
			Game.Instance.userMenu.Refresh(base.master.gameObject);
		}
	}
}
