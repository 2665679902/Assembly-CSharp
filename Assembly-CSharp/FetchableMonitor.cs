using System;

// Token: 0x0200082C RID: 2092
public class FetchableMonitor : GameStateMachine<FetchableMonitor, FetchableMonitor.Instance>
{
	// Token: 0x06003C80 RID: 15488 RVA: 0x001515F0 File Offset: 0x0014F7F0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.unfetchable;
		base.serializable = StateMachine.SerializeType.Never;
		this.fetchable.Enter("RegisterFetchable", delegate(FetchableMonitor.Instance smi)
		{
			smi.RegisterFetchable();
		}).Exit("UnregisterFetchable", delegate(FetchableMonitor.Instance smi)
		{
			smi.UnregisterFetchable();
		}).EventTransition(GameHashes.ReachableChanged, this.unfetchable, GameStateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Not(new StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(this.IsFetchable)))
			.EventTransition(GameHashes.AssigneeChanged, this.unfetchable, GameStateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Not(new StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(this.IsFetchable)))
			.EventTransition(GameHashes.EntombedChanged, this.unfetchable, GameStateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Not(new StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(this.IsFetchable)))
			.EventTransition(GameHashes.TagsChanged, this.unfetchable, GameStateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Not(new StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(this.IsFetchable)))
			.EventHandler(GameHashes.OnStore, new GameStateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.GameEvent.Callback(this.UpdateStorage))
			.EventHandler(GameHashes.StoragePriorityChanged, new GameStateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.GameEvent.Callback(this.UpdateStorage))
			.EventHandler(GameHashes.TagsChanged, new GameStateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.GameEvent.Callback(this.UpdateTags))
			.ParamTransition<bool>(this.forceUnfetchable, this.unfetchable, (FetchableMonitor.Instance smi, bool p) => !smi.IsFetchable());
		this.unfetchable.EventTransition(GameHashes.ReachableChanged, this.fetchable, new StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(this.IsFetchable)).EventTransition(GameHashes.AssigneeChanged, this.fetchable, new StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(this.IsFetchable)).EventTransition(GameHashes.EntombedChanged, this.fetchable, new StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(this.IsFetchable))
			.EventTransition(GameHashes.TagsChanged, this.fetchable, new StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(this.IsFetchable))
			.ParamTransition<bool>(this.forceUnfetchable, this.fetchable, new StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.Parameter<bool>.Callback(this.IsFetchable));
	}

	// Token: 0x06003C81 RID: 15489 RVA: 0x001517EF File Offset: 0x0014F9EF
	private bool IsFetchable(FetchableMonitor.Instance smi, bool param)
	{
		return this.IsFetchable(smi);
	}

	// Token: 0x06003C82 RID: 15490 RVA: 0x001517F8 File Offset: 0x0014F9F8
	private bool IsFetchable(FetchableMonitor.Instance smi)
	{
		return smi.IsFetchable();
	}

	// Token: 0x06003C83 RID: 15491 RVA: 0x00151800 File Offset: 0x0014FA00
	private void UpdateStorage(FetchableMonitor.Instance smi, object data)
	{
		Game.Instance.fetchManager.UpdateStorage(smi.pickupable.PrefabID(), smi.fetchable, data as Storage);
	}

	// Token: 0x06003C84 RID: 15492 RVA: 0x00151828 File Offset: 0x0014FA28
	private void UpdateTags(FetchableMonitor.Instance smi, object data)
	{
		Game.Instance.fetchManager.UpdateTags(smi.pickupable.PrefabID(), smi.fetchable);
	}

	// Token: 0x04002774 RID: 10100
	public GameStateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.State fetchable;

	// Token: 0x04002775 RID: 10101
	public GameStateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.State unfetchable;

	// Token: 0x04002776 RID: 10102
	public StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.BoolParameter forceUnfetchable = new StateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.BoolParameter(false);

	// Token: 0x020015A7 RID: 5543
	public new class Instance : GameStateMachine<FetchableMonitor, FetchableMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060084C2 RID: 33986 RVA: 0x002EA9ED File Offset: 0x002E8BED
		public Instance(Pickupable pickupable)
			: base(pickupable)
		{
			this.pickupable = pickupable;
			this.equippable = base.GetComponent<Equippable>();
		}

		// Token: 0x060084C3 RID: 33987 RVA: 0x002EAA09 File Offset: 0x002E8C09
		public void RegisterFetchable()
		{
			this.fetchable = Game.Instance.fetchManager.Add(this.pickupable);
			Game.Instance.Trigger(-1588644844, base.gameObject);
		}

		// Token: 0x060084C4 RID: 33988 RVA: 0x002EAA3C File Offset: 0x002E8C3C
		public void UnregisterFetchable()
		{
			Game.Instance.fetchManager.Remove(base.smi.pickupable.KPrefabID.PrefabID(), this.fetchable);
			Game.Instance.Trigger(-1491270284, base.gameObject);
		}

		// Token: 0x060084C5 RID: 33989 RVA: 0x002EAA88 File Offset: 0x002E8C88
		public void SetForceUnfetchable(bool is_unfetchable)
		{
			base.sm.forceUnfetchable.Set(is_unfetchable, base.smi, false);
		}

		// Token: 0x060084C6 RID: 33990 RVA: 0x002EAAA4 File Offset: 0x002E8CA4
		public bool IsFetchable()
		{
			return !base.sm.forceUnfetchable.Get(this) && !this.pickupable.IsEntombed && this.pickupable.IsReachable() && (!(this.equippable != null) || !this.equippable.isEquipped) && !this.pickupable.KPrefabID.HasTag(GameTags.StoredPrivate) && !this.pickupable.KPrefabID.HasTag(GameTags.Creatures.ReservedByCreature);
		}

		// Token: 0x04006771 RID: 26481
		public Pickupable pickupable;

		// Token: 0x04006772 RID: 26482
		private Equippable equippable;

		// Token: 0x04006773 RID: 26483
		public HandleVector<int>.Handle fetchable;
	}
}
