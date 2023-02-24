using System;
using KSerialization;

// Token: 0x02000644 RID: 1604
[SerializationConfig(MemberSerialization.OptIn)]
public class SolidConduitInbox : StateMachineComponent<SolidConduitInbox.SMInstance>, ISim1000ms
{
	// Token: 0x06002A8E RID: 10894 RVA: 0x000E0A80 File Offset: 0x000DEC80
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.filteredStorage = new FilteredStorage(this, null, null, false, Db.Get().ChoreTypes.StorageFetch);
	}

	// Token: 0x06002A8F RID: 10895 RVA: 0x000E0AA6 File Offset: 0x000DECA6
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.filteredStorage.FilterChanged();
		base.smi.StartSM();
	}

	// Token: 0x06002A90 RID: 10896 RVA: 0x000E0AC4 File Offset: 0x000DECC4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06002A91 RID: 10897 RVA: 0x000E0ACC File Offset: 0x000DECCC
	public void Sim1000ms(float dt)
	{
		if (this.operational.IsOperational && this.dispenser.IsDispensing)
		{
			this.operational.SetActive(true, false);
			return;
		}
		this.operational.SetActive(false, false);
	}

	// Token: 0x04001926 RID: 6438
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001927 RID: 6439
	[MyCmpReq]
	private SolidConduitDispenser dispenser;

	// Token: 0x04001928 RID: 6440
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001929 RID: 6441
	private FilteredStorage filteredStorage;

	// Token: 0x020012E1 RID: 4833
	public class SMInstance : GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.GameInstance
	{
		// Token: 0x06007BE1 RID: 31713 RVA: 0x002CD458 File Offset: 0x002CB658
		public SMInstance(SolidConduitInbox master)
			: base(master)
		{
		}
	}

	// Token: 0x020012E2 RID: 4834
	public class States : GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox>
	{
		// Token: 0x06007BE2 RID: 31714 RVA: 0x002CD464 File Offset: 0x002CB664
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.DoNothing();
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (SolidConduitInbox.SMInstance smi) => smi.GetComponent<Operational>().IsOperational);
			this.on.DefaultState(this.on.idle).EventTransition(GameHashes.OperationalChanged, this.off, (SolidConduitInbox.SMInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.on.idle.PlayAnim("on").EventTransition(GameHashes.ActiveChanged, this.on.working, (SolidConduitInbox.SMInstance smi) => smi.GetComponent<Operational>().IsActive);
			this.on.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).EventTransition(GameHashes.ActiveChanged, this.on.post, (SolidConduitInbox.SMInstance smi) => !smi.GetComponent<Operational>().IsActive);
			this.on.post.PlayAnim("working_pst").OnAnimQueueComplete(this.on);
		}

		// Token: 0x04005EF1 RID: 24305
		public GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.State off;

		// Token: 0x04005EF2 RID: 24306
		public SolidConduitInbox.States.ReadyStates on;

		// Token: 0x02001FF8 RID: 8184
		public class ReadyStates : GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.State
		{
			// Token: 0x04008E89 RID: 36489
			public GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.State idle;

			// Token: 0x04008E8A RID: 36490
			public GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.State working;

			// Token: 0x04008E8B RID: 36491
			public GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.State post;
		}
	}
}
