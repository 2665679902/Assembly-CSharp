using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000643 RID: 1603
[SerializationConfig(MemberSerialization.OptIn)]
public class SolidConduitDropper : StateMachineComponent<SolidConduitDropper.SMInstance>
{
	// Token: 0x06002A89 RID: 10889 RVA: 0x000E09DD File Offset: 0x000DEBDD
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002A8A RID: 10890 RVA: 0x000E09E5 File Offset: 0x000DEBE5
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06002A8B RID: 10891 RVA: 0x000E09F8 File Offset: 0x000DEBF8
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06002A8C RID: 10892 RVA: 0x000E0A00 File Offset: 0x000DEC00
	private void Update()
	{
		base.smi.sm.consuming.Set(this.consumer.IsConsuming, base.smi, false);
		base.smi.sm.isclosed.Set(!this.operational.IsOperational, base.smi, false);
		this.storage.DropAll(false, false, default(Vector3), true, null);
	}

	// Token: 0x04001923 RID: 6435
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001924 RID: 6436
	[MyCmpReq]
	private SolidConduitConsumer consumer;

	// Token: 0x04001925 RID: 6437
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x020012DF RID: 4831
	public class SMInstance : GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.GameInstance
	{
		// Token: 0x06007BDE RID: 31710 RVA: 0x002CD336 File Offset: 0x002CB536
		public SMInstance(SolidConduitDropper master)
			: base(master)
		{
		}
	}

	// Token: 0x020012E0 RID: 4832
	public class States : GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper>
	{
		// Token: 0x06007BDF RID: 31711 RVA: 0x002CD340 File Offset: 0x002CB540
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.Update("Update", delegate(SolidConduitDropper.SMInstance smi, float dt)
			{
				smi.master.Update();
			}, UpdateRate.SIM_1000ms, false);
			this.idle.PlayAnim("on").ParamTransition<bool>(this.consuming, this.working, GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.IsTrue).ParamTransition<bool>(this.isclosed, this.closed, GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.IsTrue);
			this.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).ParamTransition<bool>(this.consuming, this.post, GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.IsFalse);
			this.post.PlayAnim("working_pst").OnAnimQueueComplete(this.idle);
			this.closed.PlayAnim("closed").ParamTransition<bool>(this.consuming, this.working, GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.IsTrue).ParamTransition<bool>(this.isclosed, this.idle, GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.IsFalse);
		}

		// Token: 0x04005EEB RID: 24299
		public StateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.BoolParameter consuming;

		// Token: 0x04005EEC RID: 24300
		public StateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.BoolParameter isclosed;

		// Token: 0x04005EED RID: 24301
		public GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.State idle;

		// Token: 0x04005EEE RID: 24302
		public GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.State working;

		// Token: 0x04005EEF RID: 24303
		public GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.State post;

		// Token: 0x04005EF0 RID: 24304
		public GameStateMachine<SolidConduitDropper.States, SolidConduitDropper.SMInstance, SolidConduitDropper, object>.State closed;
	}
}
