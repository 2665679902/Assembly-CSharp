using System;

// Token: 0x02000822 RID: 2082
public class CringeMonitor : GameStateMachine<CringeMonitor, CringeMonitor.Instance>
{
	// Token: 0x06003C60 RID: 15456 RVA: 0x001505D0 File Offset: 0x0014E7D0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.EventHandler(GameHashes.Cringe, new GameStateMachine<CringeMonitor, CringeMonitor.Instance, IStateMachineTarget, object>.GameEvent.Callback(this.TriggerCringe));
		this.cringe.ToggleReactable((CringeMonitor.Instance smi) => smi.GetReactable()).ToggleStatusItem((CringeMonitor.Instance smi) => smi.GetStatusItem(), null).ScheduleGoTo(3f, this.idle);
	}

	// Token: 0x06003C61 RID: 15457 RVA: 0x00150662 File Offset: 0x0014E862
	private void TriggerCringe(CringeMonitor.Instance smi, object data)
	{
		if (smi.GetComponent<KPrefabID>().HasTag(GameTags.Suit))
		{
			return;
		}
		smi.SetCringeSourceData(data);
		smi.GoTo(this.cringe);
	}

	// Token: 0x0400274F RID: 10063
	public GameStateMachine<CringeMonitor, CringeMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x04002750 RID: 10064
	public GameStateMachine<CringeMonitor, CringeMonitor.Instance, IStateMachineTarget, object>.State cringe;

	// Token: 0x0200158E RID: 5518
	public new class Instance : GameStateMachine<CringeMonitor, CringeMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600844E RID: 33870 RVA: 0x002E9A1C File Offset: 0x002E7C1C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600844F RID: 33871 RVA: 0x002E9A28 File Offset: 0x002E7C28
		public void SetCringeSourceData(object data)
		{
			string text = (string)data;
			this.statusItem = new StatusItem("CringeSource", text, null, "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
		}

		// Token: 0x06008450 RID: 33872 RVA: 0x002E9A64 File Offset: 0x002E7C64
		public Reactable GetReactable()
		{
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, "Cringe", Db.Get().ChoreTypes.EmoteHighPriority, 0f, 0f, float.PositiveInfinity, 0f);
			selfEmoteReactable.SetEmote(Db.Get().Emotes.Minion.Cringe);
			selfEmoteReactable.preventChoreInterruption = true;
			return selfEmoteReactable;
		}

		// Token: 0x06008451 RID: 33873 RVA: 0x002E9AD0 File Offset: 0x002E7CD0
		public StatusItem GetStatusItem()
		{
			return this.statusItem;
		}

		// Token: 0x04006710 RID: 26384
		private StatusItem statusItem;
	}
}
