using System;

// Token: 0x02000841 RID: 2113
public class RoomMonitor : GameStateMachine<RoomMonitor, RoomMonitor.Instance>
{
	// Token: 0x06003CF6 RID: 15606 RVA: 0x00154711 File Offset: 0x00152911
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EventHandler(GameHashes.PathAdvanced, new StateMachine<RoomMonitor, RoomMonitor.Instance, IStateMachineTarget, object>.State.Callback(RoomMonitor.UpdateRoomType));
	}

	// Token: 0x06003CF7 RID: 15607 RVA: 0x00154738 File Offset: 0x00152938
	private static void UpdateRoomType(RoomMonitor.Instance smi)
	{
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(smi.master.gameObject);
		if (roomOfGameObject != smi.sm.currentRoom)
		{
			smi.sm.currentRoom = roomOfGameObject;
			if (roomOfGameObject != null)
			{
				roomOfGameObject.cavity.OnEnter(smi.master.gameObject);
			}
		}
	}

	// Token: 0x040027D9 RID: 10201
	public Room currentRoom;

	// Token: 0x020015DB RID: 5595
	public new class Instance : GameStateMachine<RoomMonitor, RoomMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600859D RID: 34205 RVA: 0x002ECC10 File Offset: 0x002EAE10
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.navigator = base.GetComponent<Navigator>();
		}

		// Token: 0x0600859E RID: 34206 RVA: 0x002ECC25 File Offset: 0x002EAE25
		public Room GetCurrentRoomType()
		{
			return base.sm.currentRoom;
		}

		// Token: 0x04006818 RID: 26648
		public Navigator navigator;
	}
}
