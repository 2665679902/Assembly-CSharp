using System;
using System.Linq;
using KSerialization;

// Token: 0x0200066E RID: 1646
public class WarpReceiver : Workable
{
	// Token: 0x06002C6B RID: 11371 RVA: 0x000E94B0 File Offset: 0x000E76B0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002C6C RID: 11372 RVA: 0x000E94B8 File Offset: 0x000E76B8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.warpReceiverSMI = new WarpReceiver.WarpReceiverSM.Instance(this);
		this.warpReceiverSMI.StartSM();
		Components.WarpReceivers.Add(this);
	}

	// Token: 0x06002C6D RID: 11373 RVA: 0x000E94E4 File Offset: 0x000E76E4
	public void ReceiveWarpedDuplicant(Worker dupe)
	{
		dupe.transform.SetPosition(Grid.CellToPos(Grid.PosToCell(this), CellAlignment.Bottom, Grid.SceneLayer.Move));
		Debug.Assert(this.chore == null);
		KAnimFile anim = Assets.GetAnim("anim_interacts_warp_portal_receiver_kanim");
		ChoreType migrate = Db.Get().ChoreTypes.Migrate;
		KAnimFile kanimFile = anim;
		this.chore = new WorkChore<Workable>(migrate, this, dupe.GetComponent<ChoreProvider>(), true, delegate(Chore o)
		{
			this.CompleteChore();
		}, null, null, true, null, true, true, kanimFile, false, true, false, PriorityScreen.PriorityClass.compulsory, 5, false, true);
		Workable component = base.GetComponent<Workable>();
		component.workLayer = Grid.SceneLayer.Building;
		component.workAnims = new HashedString[] { "printing_pre", "printing_loop" };
		component.workingPstComplete = new HashedString[] { "printing_pst" };
		component.workingPstFailed = new HashedString[] { "printing_pst" };
		component.synchronizeAnims = true;
		float num = 0f;
		KAnimFileData data = anim.GetData();
		for (int i = 0; i < data.animCount; i++)
		{
			KAnim.Anim anim2 = data.GetAnim(i);
			if (component.workAnims.Contains(anim2.hash))
			{
				num += anim2.totalTime;
			}
		}
		component.SetWorkTime(num);
		this.Used = true;
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x000E963F File Offset: 0x000E783F
	private void CompleteChore()
	{
		this.chore.Cleanup();
		this.chore = null;
		this.warpReceiverSMI.GoTo(this.warpReceiverSMI.sm.idle);
	}

	// Token: 0x06002C6F RID: 11375 RVA: 0x000E966E File Offset: 0x000E786E
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.WarpReceivers.Remove(this);
	}

	// Token: 0x04001A71 RID: 6769
	[MyCmpAdd]
	public Notifier notifier;

	// Token: 0x04001A72 RID: 6770
	private WarpReceiver.WarpReceiverSM.Instance warpReceiverSMI;

	// Token: 0x04001A73 RID: 6771
	private Notification notification;

	// Token: 0x04001A74 RID: 6772
	[Serialize]
	public bool IsConsumed;

	// Token: 0x04001A75 RID: 6773
	private Chore chore;

	// Token: 0x04001A76 RID: 6774
	[Serialize]
	public bool Used;

	// Token: 0x02001336 RID: 4918
	public class WarpReceiverSM : GameStateMachine<WarpReceiver.WarpReceiverSM, WarpReceiver.WarpReceiverSM.Instance, WarpReceiver>
	{
		// Token: 0x06007D04 RID: 32004 RVA: 0x002D2C69 File Offset: 0x002D0E69
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.PlayAnim("idle");
		}

		// Token: 0x04005FE4 RID: 24548
		public GameStateMachine<WarpReceiver.WarpReceiverSM, WarpReceiver.WarpReceiverSM.Instance, WarpReceiver, object>.State idle;

		// Token: 0x0200201F RID: 8223
		public new class Instance : GameStateMachine<WarpReceiver.WarpReceiverSM, WarpReceiver.WarpReceiverSM.Instance, WarpReceiver, object>.GameInstance
		{
			// Token: 0x0600A282 RID: 41602 RVA: 0x0034452A File Offset: 0x0034272A
			public Instance(WarpReceiver master)
				: base(master)
			{
			}
		}
	}
}
