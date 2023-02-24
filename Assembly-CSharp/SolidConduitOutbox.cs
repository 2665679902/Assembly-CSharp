using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000645 RID: 1605
[SerializationConfig(MemberSerialization.OptIn)]
public class SolidConduitOutbox : StateMachineComponent<SolidConduitOutbox.SMInstance>
{
	// Token: 0x06002A93 RID: 10899 RVA: 0x000E0B0B File Offset: 0x000DED0B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002A94 RID: 10900 RVA: 0x000E0B13 File Offset: 0x000DED13
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(this, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		base.Subscribe<SolidConduitOutbox>(-1697596308, SolidConduitOutbox.OnStorageChangedDelegate);
		this.UpdateMeter();
		base.smi.StartSM();
	}

	// Token: 0x06002A95 RID: 10901 RVA: 0x000E0B51 File Offset: 0x000DED51
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06002A96 RID: 10902 RVA: 0x000E0B59 File Offset: 0x000DED59
	private void OnStorageChanged(object data)
	{
		this.UpdateMeter();
	}

	// Token: 0x06002A97 RID: 10903 RVA: 0x000E0B64 File Offset: 0x000DED64
	private void UpdateMeter()
	{
		float num = Mathf.Clamp01(this.storage.MassStored() / this.storage.capacityKg);
		this.meter.SetPositionPercent(num);
	}

	// Token: 0x06002A98 RID: 10904 RVA: 0x000E0B9A File Offset: 0x000DED9A
	private void UpdateConsuming()
	{
		base.smi.sm.consuming.Set(this.consumer.IsConsuming, base.smi, false);
	}

	// Token: 0x0400192A RID: 6442
	[MyCmpReq]
	private Operational operational;

	// Token: 0x0400192B RID: 6443
	[MyCmpReq]
	private SolidConduitConsumer consumer;

	// Token: 0x0400192C RID: 6444
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x0400192D RID: 6445
	private MeterController meter;

	// Token: 0x0400192E RID: 6446
	private static readonly EventSystem.IntraObjectHandler<SolidConduitOutbox> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<SolidConduitOutbox>(delegate(SolidConduitOutbox component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x020012E3 RID: 4835
	public class SMInstance : GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.GameInstance
	{
		// Token: 0x06007BE4 RID: 31716 RVA: 0x002CD5D4 File Offset: 0x002CB7D4
		public SMInstance(SolidConduitOutbox master)
			: base(master)
		{
		}
	}

	// Token: 0x020012E4 RID: 4836
	public class States : GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox>
	{
		// Token: 0x06007BE5 RID: 31717 RVA: 0x002CD5E0 File Offset: 0x002CB7E0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.Update("RefreshConsuming", delegate(SolidConduitOutbox.SMInstance smi, float dt)
			{
				smi.master.UpdateConsuming();
			}, UpdateRate.SIM_1000ms, false);
			this.idle.PlayAnim("on").ParamTransition<bool>(this.consuming, this.working, GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.IsTrue);
			this.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).ParamTransition<bool>(this.consuming, this.post, GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.IsFalse);
			this.post.PlayAnim("working_pst").OnAnimQueueComplete(this.idle);
		}

		// Token: 0x04005EF3 RID: 24307
		public StateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.BoolParameter consuming;

		// Token: 0x04005EF4 RID: 24308
		public GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.State idle;

		// Token: 0x04005EF5 RID: 24309
		public GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.State working;

		// Token: 0x04005EF6 RID: 24310
		public GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.State post;
	}
}
