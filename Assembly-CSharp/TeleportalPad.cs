using System;

// Token: 0x02000656 RID: 1622
public class TeleportalPad : StateMachineComponent<TeleportalPad.StatesInstance>
{
	// Token: 0x06002B74 RID: 11124 RVA: 0x000E48FA File Offset: 0x000E2AFA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x040019B9 RID: 6585
	[MyCmpReq]
	private Operational operational;

	// Token: 0x0200130A RID: 4874
	public class StatesInstance : GameStateMachine<TeleportalPad.States, TeleportalPad.StatesInstance, TeleportalPad, object>.GameInstance
	{
		// Token: 0x06007C4E RID: 31822 RVA: 0x002CFD26 File Offset: 0x002CDF26
		public StatesInstance(TeleportalPad master)
			: base(master)
		{
		}
	}

	// Token: 0x0200130B RID: 4875
	public class States : GameStateMachine<TeleportalPad.States, TeleportalPad.StatesInstance, TeleportalPad>
	{
		// Token: 0x06007C4F RID: 31823 RVA: 0x002CFD30 File Offset: 0x002CDF30
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inactive;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.EventTransition(GameHashes.OperationalChanged, this.inactive, (TeleportalPad.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.inactive.PlayAnim("idle").EventTransition(GameHashes.OperationalChanged, this.no_target, (TeleportalPad.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational);
			this.no_target.Enter(delegate(TeleportalPad.StatesInstance smi)
			{
				if (smi.master.GetComponent<Teleporter>().HasTeleporterTarget())
				{
					smi.GoTo(this.portal_on.turn_on);
				}
			}).PlayAnim("idle").EventTransition(GameHashes.TeleporterIDsChanged, this.portal_on.turn_on, (TeleportalPad.StatesInstance smi) => smi.master.GetComponent<Teleporter>().HasTeleporterTarget());
			this.portal_on.EventTransition(GameHashes.TeleporterIDsChanged, this.portal_on.turn_off, (TeleportalPad.StatesInstance smi) => !smi.master.GetComponent<Teleporter>().HasTeleporterTarget());
			this.portal_on.turn_on.PlayAnim("working_pre").OnAnimQueueComplete(this.portal_on.loop);
			this.portal_on.loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).Update(delegate(TeleportalPad.StatesInstance smi, float dt)
			{
				Teleporter component = smi.master.GetComponent<Teleporter>();
				Teleporter teleporter = component.FindTeleportTarget();
				component.SetTeleportTarget(teleporter);
				if (teleporter != null)
				{
					component.TeleportObjects();
				}
			}, UpdateRate.SIM_200ms, false);
			this.portal_on.turn_off.PlayAnim("working_pst").OnAnimQueueComplete(this.no_target);
		}

		// Token: 0x04005F54 RID: 24404
		public StateMachine<TeleportalPad.States, TeleportalPad.StatesInstance, TeleportalPad, object>.Signal targetTeleporter;

		// Token: 0x04005F55 RID: 24405
		public StateMachine<TeleportalPad.States, TeleportalPad.StatesInstance, TeleportalPad, object>.Signal doTeleport;

		// Token: 0x04005F56 RID: 24406
		public GameStateMachine<TeleportalPad.States, TeleportalPad.StatesInstance, TeleportalPad, object>.State inactive;

		// Token: 0x04005F57 RID: 24407
		public GameStateMachine<TeleportalPad.States, TeleportalPad.StatesInstance, TeleportalPad, object>.State no_target;

		// Token: 0x04005F58 RID: 24408
		public TeleportalPad.States.PortalOnStates portal_on;

		// Token: 0x0200200C RID: 8204
		public class PortalOnStates : GameStateMachine<TeleportalPad.States, TeleportalPad.StatesInstance, TeleportalPad, object>.State
		{
			// Token: 0x04008EF4 RID: 36596
			public GameStateMachine<TeleportalPad.States, TeleportalPad.StatesInstance, TeleportalPad, object>.State turn_on;

			// Token: 0x04008EF5 RID: 36597
			public GameStateMachine<TeleportalPad.States, TeleportalPad.StatesInstance, TeleportalPad, object>.State loop;

			// Token: 0x04008EF6 RID: 36598
			public GameStateMachine<TeleportalPad.States, TeleportalPad.StatesInstance, TeleportalPad, object>.State turn_off;
		}
	}
}
