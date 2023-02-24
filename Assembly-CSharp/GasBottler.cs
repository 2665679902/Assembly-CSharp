using System;
using UnityEngine;

// Token: 0x020005C0 RID: 1472
[AddComponentMenu("KMonoBehaviour/Workable/GasBottler")]
public class GasBottler : Workable
{
	// Token: 0x06002489 RID: 9353 RVA: 0x000C5792 File Offset: 0x000C3992
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new GasBottler.Controller.Instance(this);
		this.smi.StartSM();
		this.UpdateStoredItemState();
	}

	// Token: 0x0600248A RID: 9354 RVA: 0x000C57B7 File Offset: 0x000C39B7
	protected override void OnCleanUp()
	{
		if (this.smi != null)
		{
			this.smi.StopSM("OnCleanUp");
		}
		base.OnCleanUp();
	}

	// Token: 0x0600248B RID: 9355 RVA: 0x000C57D8 File Offset: 0x000C39D8
	private void UpdateStoredItemState()
	{
		this.storage.allowItemRemoval = this.smi != null && this.smi.GetCurrentState() == this.smi.sm.ready;
		foreach (GameObject gameObject in this.storage.items)
		{
			if (gameObject != null)
			{
				gameObject.Trigger(-778359855, this.storage);
			}
		}
	}

	// Token: 0x0400150C RID: 5388
	public Storage storage;

	// Token: 0x0400150D RID: 5389
	private GasBottler.Controller.Instance smi;

	// Token: 0x020011FF RID: 4607
	private class Controller : GameStateMachine<GasBottler.Controller, GasBottler.Controller.Instance, GasBottler>
	{
		// Token: 0x060078A8 RID: 30888 RVA: 0x002C0214 File Offset: 0x002BE414
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			this.empty.PlayAnim("off").EventTransition(GameHashes.OnStorageChange, this.filling, (GasBottler.Controller.Instance smi) => smi.master.storage.IsFull());
			this.filling.PlayAnim("working").OnAnimQueueComplete(this.ready);
			this.ready.EventTransition(GameHashes.OnStorageChange, this.pickup, (GasBottler.Controller.Instance smi) => !smi.master.storage.IsFull()).Enter(delegate(GasBottler.Controller.Instance smi)
			{
				smi.master.storage.allowItemRemoval = true;
				foreach (GameObject gameObject in smi.master.storage.items)
				{
					gameObject.GetComponent<KPrefabID>().AddTag(GameTags.GasSource, false);
					gameObject.Trigger(-778359855, smi.master.storage);
				}
			}).Exit(delegate(GasBottler.Controller.Instance smi)
			{
				smi.master.storage.allowItemRemoval = false;
				foreach (GameObject gameObject2 in smi.master.storage.items)
				{
					gameObject2.Trigger(-778359855, smi.master.storage);
				}
			});
			this.pickup.PlayAnim("pick_up").OnAnimQueueComplete(this.empty);
		}

		// Token: 0x04005CA8 RID: 23720
		public GameStateMachine<GasBottler.Controller, GasBottler.Controller.Instance, GasBottler, object>.State empty;

		// Token: 0x04005CA9 RID: 23721
		public GameStateMachine<GasBottler.Controller, GasBottler.Controller.Instance, GasBottler, object>.State filling;

		// Token: 0x04005CAA RID: 23722
		public GameStateMachine<GasBottler.Controller, GasBottler.Controller.Instance, GasBottler, object>.State ready;

		// Token: 0x04005CAB RID: 23723
		public GameStateMachine<GasBottler.Controller, GasBottler.Controller.Instance, GasBottler, object>.State pickup;

		// Token: 0x02001FA8 RID: 8104
		public new class Instance : GameStateMachine<GasBottler.Controller, GasBottler.Controller.Instance, GasBottler, object>.GameInstance
		{
			// Token: 0x0600A02D RID: 41005 RVA: 0x003414F9 File Offset: 0x0033F6F9
			public Instance(GasBottler master)
				: base(master)
			{
			}
		}
	}
}
