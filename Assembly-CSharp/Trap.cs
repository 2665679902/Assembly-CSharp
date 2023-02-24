using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020009AD RID: 2477
public class Trap : StateMachineComponent<Trap.StatesInstance>
{
	// Token: 0x0600498C RID: 18828 RVA: 0x0019C070 File Offset: 0x0019A270
	private static void CreateStatusItems()
	{
		if (Trap.statusSprung == null)
		{
			Trap.statusReady = new StatusItem("Ready", BUILDING.STATUSITEMS.CREATURE_TRAP.READY.NAME, BUILDING.STATUSITEMS.CREATURE_TRAP.READY.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022, true, null);
			Trap.statusSprung = new StatusItem("Sprung", BUILDING.STATUSITEMS.CREATURE_TRAP.SPRUNG.NAME, BUILDING.STATUSITEMS.CREATURE_TRAP.SPRUNG.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022, true, null);
			Trap.statusSprung.resolveTooltipCallback = delegate(string str, object obj)
			{
				Trap.StatesInstance statesInstance = (Trap.StatesInstance)obj;
				return string.Format(str, statesInstance.master.contents.Get().GetProperName());
			};
		}
	}

	// Token: 0x0600498D RID: 18829 RVA: 0x0019C11E File Offset: 0x0019A31E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.contents = new Ref<KPrefabID>();
		Trap.CreateStatusItems();
	}

	// Token: 0x0600498E RID: 18830 RVA: 0x0019C138 File Offset: 0x0019A338
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Storage component = base.GetComponent<Storage>();
		base.smi.StartSM();
		if (!component.IsEmpty())
		{
			KPrefabID component2 = component.items[0].GetComponent<KPrefabID>();
			if (component2 != null)
			{
				this.contents.Set(component2);
				base.smi.GoTo(base.smi.sm.occupied);
				return;
			}
			component.DropAll(false, false, default(Vector3), true, null);
		}
	}

	// Token: 0x04003059 RID: 12377
	[Serialize]
	private Ref<KPrefabID> contents;

	// Token: 0x0400305A RID: 12378
	public TagSet captureTags = new TagSet();

	// Token: 0x0400305B RID: 12379
	private static StatusItem statusReady;

	// Token: 0x0400305C RID: 12380
	private static StatusItem statusSprung;

	// Token: 0x020017AF RID: 6063
	public class StatesInstance : GameStateMachine<Trap.States, Trap.StatesInstance, Trap, object>.GameInstance
	{
		// Token: 0x06008B9B RID: 35739 RVA: 0x0030008D File Offset: 0x002FE28D
		public StatesInstance(Trap master)
			: base(master)
		{
		}

		// Token: 0x06008B9C RID: 35740 RVA: 0x00300098 File Offset: 0x002FE298
		public void OnTrapTriggered(object data)
		{
			KPrefabID component = ((GameObject)data).GetComponent<KPrefabID>();
			base.master.contents.Set(component);
			base.smi.sm.trapTriggered.Trigger(base.smi);
		}
	}

	// Token: 0x020017B0 RID: 6064
	public class States : GameStateMachine<Trap.States, Trap.StatesInstance, Trap>
	{
		// Token: 0x06008B9D RID: 35741 RVA: 0x003000E0 File Offset: 0x002FE2E0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.ready;
			base.serializable = StateMachine.SerializeType.Never;
			Trap.CreateStatusItems();
			this.ready.EventHandler(GameHashes.TrapTriggered, delegate(Trap.StatesInstance smi, object data)
			{
				smi.OnTrapTriggered(data);
			}).OnSignal(this.trapTriggered, this.trapping).ToggleStatusItem(Trap.statusReady, null);
			this.trapping.PlayAnim("working_pre").OnAnimQueueComplete(this.occupied);
			this.occupied.ToggleTag(GameTags.Trapped).ToggleStatusItem(Trap.statusSprung, (Trap.StatesInstance smi) => smi).DefaultState(this.occupied.idle)
				.EventTransition(GameHashes.OnStorageChange, this.finishedUsing, (Trap.StatesInstance smi) => smi.master.GetComponent<Storage>().IsEmpty());
			this.occupied.idle.PlayAnim("working_loop", KAnim.PlayMode.Loop);
			this.finishedUsing.PlayAnim("working_pst").OnAnimQueueComplete(this.destroySelf);
			this.destroySelf.Enter(delegate(Trap.StatesInstance smi)
			{
				Util.KDestroyGameObject(smi.master.gameObject);
			});
		}

		// Token: 0x04006DBB RID: 28091
		public GameStateMachine<Trap.States, Trap.StatesInstance, Trap, object>.State ready;

		// Token: 0x04006DBC RID: 28092
		public GameStateMachine<Trap.States, Trap.StatesInstance, Trap, object>.State trapping;

		// Token: 0x04006DBD RID: 28093
		public GameStateMachine<Trap.States, Trap.StatesInstance, Trap, object>.State finishedUsing;

		// Token: 0x04006DBE RID: 28094
		public GameStateMachine<Trap.States, Trap.StatesInstance, Trap, object>.State destroySelf;

		// Token: 0x04006DBF RID: 28095
		public StateMachine<Trap.States, Trap.StatesInstance, Trap, object>.Signal trapTriggered;

		// Token: 0x04006DC0 RID: 28096
		public Trap.States.OccupiedStates occupied;

		// Token: 0x020020D3 RID: 8403
		public class OccupiedStates : GameStateMachine<Trap.States, Trap.StatesInstance, Trap, object>.State
		{
			// Token: 0x04009233 RID: 37427
			public GameStateMachine<Trap.States, Trap.StatesInstance, Trap, object>.State idle;
		}
	}
}
