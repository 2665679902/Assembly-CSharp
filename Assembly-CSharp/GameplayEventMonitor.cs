using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200082D RID: 2093
public class GameplayEventMonitor : GameStateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>
{
	// Token: 0x06003C86 RID: 15494 RVA: 0x00151860 File Offset: 0x0014FA60
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.InitializeStates(out default_state);
		default_state = this.idle;
		this.root.EventHandler(GameHashes.GameplayEventMonitorStart, delegate(GameplayEventMonitor.Instance smi, object data)
		{
			smi.OnMonitorStart(data);
		}).EventHandler(GameHashes.GameplayEventMonitorEnd, delegate(GameplayEventMonitor.Instance smi, object data)
		{
			smi.OnMonitorEnd(data);
		}).EventHandler(GameHashes.GameplayEventMonitorChanged, delegate(GameplayEventMonitor.Instance smi, object data)
		{
			this.UpdateFX(smi);
		});
		this.idle.EventTransition(GameHashes.GameplayEventMonitorStart, this.activeState, new StateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.Transition.ConditionCallback(this.HasEvents)).Enter(new StateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.State.Callback(this.UpdateEventDisplay));
		this.activeState.DefaultState(this.activeState.unseenEvents);
		this.activeState.unseenEvents.ToggleFX(new Func<GameplayEventMonitor.Instance, StateMachine.Instance>(this.CreateFX)).EventHandler(GameHashes.SelectObject, delegate(GameplayEventMonitor.Instance smi, object data)
		{
			smi.OnSelect(data);
		}).EventTransition(GameHashes.GameplayEventMonitorChanged, this.activeState.seenAllEvents, new StateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.Transition.ConditionCallback(this.SeenAll));
		this.activeState.seenAllEvents.EventTransition(GameHashes.GameplayEventMonitorStart, this.activeState.unseenEvents, GameStateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.Not(new StateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.Transition.ConditionCallback(this.SeenAll))).Enter(new StateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.State.Callback(this.UpdateEventDisplay));
	}

	// Token: 0x06003C87 RID: 15495 RVA: 0x001519E0 File Offset: 0x0014FBE0
	private bool HasEvents(GameplayEventMonitor.Instance smi)
	{
		return smi.events.Count > 0;
	}

	// Token: 0x06003C88 RID: 15496 RVA: 0x001519F0 File Offset: 0x0014FBF0
	private bool SeenAll(GameplayEventMonitor.Instance smi)
	{
		return smi.UnseenCount() == 0;
	}

	// Token: 0x06003C89 RID: 15497 RVA: 0x001519FB File Offset: 0x0014FBFB
	private void UpdateFX(GameplayEventMonitor.Instance smi)
	{
		if (smi.fx != null)
		{
			smi.fx.sm.notificationCount.Set(smi.UnseenCount(), smi.fx, false);
		}
	}

	// Token: 0x06003C8A RID: 15498 RVA: 0x00151A28 File Offset: 0x0014FC28
	private GameplayEventFX.Instance CreateFX(GameplayEventMonitor.Instance smi)
	{
		if (!smi.isMasterNull)
		{
			smi.fx = new GameplayEventFX.Instance(smi.master, new Vector3(0f, 0f, -0.1f));
			return smi.fx;
		}
		return null;
	}

	// Token: 0x06003C8B RID: 15499 RVA: 0x00151A60 File Offset: 0x0014FC60
	public void UpdateEventDisplay(GameplayEventMonitor.Instance smi)
	{
		if (smi.events.Count == 0 || smi.UnseenCount() > 0)
		{
			NameDisplayScreen.Instance.SetGameplayEventDisplay(smi.master.gameObject, false, null, null);
			return;
		}
		int num = -1;
		GameplayEvent gameplayEvent = null;
		foreach (GameplayEventInstance gameplayEventInstance in smi.events)
		{
			Sprite displaySprite = gameplayEventInstance.gameplayEvent.GetDisplaySprite();
			if (gameplayEventInstance.gameplayEvent.importance > num && displaySprite != null)
			{
				num = gameplayEventInstance.gameplayEvent.importance;
				gameplayEvent = gameplayEventInstance.gameplayEvent;
			}
		}
		if (gameplayEvent != null)
		{
			NameDisplayScreen.Instance.SetGameplayEventDisplay(smi.master.gameObject, true, gameplayEvent.GetDisplayString(), gameplayEvent.GetDisplaySprite());
		}
	}

	// Token: 0x04002777 RID: 10103
	public GameStateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.State idle;

	// Token: 0x04002778 RID: 10104
	public GameplayEventMonitor.ActiveState activeState;

	// Token: 0x020015A9 RID: 5545
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020015AA RID: 5546
	public class ActiveState : GameStateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.State
	{
		// Token: 0x04006778 RID: 26488
		public GameStateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.State unseenEvents;

		// Token: 0x04006779 RID: 26489
		public GameStateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.State seenAllEvents;
	}

	// Token: 0x020015AB RID: 5547
	public new class Instance : GameStateMachine<GameplayEventMonitor, GameplayEventMonitor.Instance, IStateMachineTarget, GameplayEventMonitor.Def>.GameInstance
	{
		// Token: 0x060084CE RID: 33998 RVA: 0x002EAB73 File Offset: 0x002E8D73
		public Instance(IStateMachineTarget master, GameplayEventMonitor.Def def)
			: base(master, def)
		{
			NameDisplayScreen.Instance.RegisterComponent(base.gameObject, this, false);
		}

		// Token: 0x060084CF RID: 33999 RVA: 0x002EAB9C File Offset: 0x002E8D9C
		public void OnMonitorStart(object data)
		{
			GameplayEventInstance gameplayEventInstance = data as GameplayEventInstance;
			if (!this.events.Contains(gameplayEventInstance))
			{
				this.events.Add(gameplayEventInstance);
				gameplayEventInstance.RegisterMonitorCallback(base.gameObject);
			}
			base.smi.sm.UpdateFX(base.smi);
			base.smi.sm.UpdateEventDisplay(base.smi);
		}

		// Token: 0x060084D0 RID: 34000 RVA: 0x002EAC04 File Offset: 0x002E8E04
		public void OnMonitorEnd(object data)
		{
			GameplayEventInstance gameplayEventInstance = data as GameplayEventInstance;
			if (this.events.Contains(gameplayEventInstance))
			{
				this.events.Remove(gameplayEventInstance);
				gameplayEventInstance.UnregisterMonitorCallback(base.gameObject);
			}
			base.smi.sm.UpdateFX(base.smi);
			base.smi.sm.UpdateEventDisplay(base.smi);
			if (this.events.Count == 0)
			{
				base.smi.GoTo(base.sm.idle);
			}
		}

		// Token: 0x060084D1 RID: 34001 RVA: 0x002EAC90 File Offset: 0x002E8E90
		public void OnSelect(object data)
		{
			if (!(bool)data)
			{
				return;
			}
			foreach (GameplayEventInstance gameplayEventInstance in this.events)
			{
				if (!gameplayEventInstance.seenNotification && gameplayEventInstance.GetEventPopupData != null)
				{
					gameplayEventInstance.seenNotification = true;
					EventInfoScreen.ShowPopup(gameplayEventInstance.GetEventPopupData());
					break;
				}
			}
			if (this.UnseenCount() == 0)
			{
				base.smi.GoTo(base.sm.activeState.seenAllEvents);
			}
		}

		// Token: 0x060084D2 RID: 34002 RVA: 0x002EAD34 File Offset: 0x002E8F34
		public int UnseenCount()
		{
			return this.events.Count((GameplayEventInstance evt) => !evt.seenNotification);
		}

		// Token: 0x0400677A RID: 26490
		public List<GameplayEventInstance> events = new List<GameplayEventInstance>();

		// Token: 0x0400677B RID: 26491
		public GameplayEventFX.Instance fx;
	}
}
