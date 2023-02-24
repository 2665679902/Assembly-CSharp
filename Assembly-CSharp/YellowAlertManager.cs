using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000855 RID: 2133
public class YellowAlertManager : GameStateMachine<YellowAlertManager, YellowAlertManager.Instance>
{
	// Token: 0x06003D4A RID: 15690 RVA: 0x00156C04 File Offset: 0x00154E04
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.off.ParamTransition<bool>(this.isOn, this.on, GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.IsTrue);
		this.on.Enter("EnterEvent", delegate(YellowAlertManager.Instance smi)
		{
			Game.Instance.Trigger(-741654735, null);
		}).Exit("ExitEvent", delegate(YellowAlertManager.Instance smi)
		{
			Game.Instance.Trigger(-2062778933, null);
		}).Enter("EnableVignette", delegate(YellowAlertManager.Instance smi)
		{
			Vignette.Instance.SetColor(new Color(1f, 1f, 0f, 0.1f));
		})
			.Exit("DisableVignette", delegate(YellowAlertManager.Instance smi)
			{
				Vignette.Instance.Reset();
			})
			.Enter("Sounds", delegate(YellowAlertManager.Instance smi)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("RedAlert_ON", false));
			})
			.ToggleLoopingSound(GlobalAssets.GetSound("RedAlert_LP", false), null, true, true, true)
			.ToggleNotification((YellowAlertManager.Instance smi) => smi.notification)
			.ParamTransition<bool>(this.isOn, this.off, GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.IsFalse);
		this.on_pst.Enter("Sounds", delegate(YellowAlertManager.Instance smi)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("RedAlert_OFF", false));
		});
	}

	// Token: 0x04002826 RID: 10278
	public GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x04002827 RID: 10279
	public GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.State on;

	// Token: 0x04002828 RID: 10280
	public GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.State on_pst;

	// Token: 0x04002829 RID: 10281
	public StateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.BoolParameter isOn = new StateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.BoolParameter();

	// Token: 0x0200160B RID: 5643
	public new class Instance : GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008692 RID: 34450 RVA: 0x002EF74B File Offset: 0x002ED94B
		public static void DestroyInstance()
		{
			YellowAlertManager.Instance.instance = null;
		}

		// Token: 0x06008693 RID: 34451 RVA: 0x002EF753 File Offset: 0x002ED953
		public static YellowAlertManager.Instance Get()
		{
			return YellowAlertManager.Instance.instance;
		}

		// Token: 0x06008694 RID: 34452 RVA: 0x002EF75C File Offset: 0x002ED95C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			YellowAlertManager.Instance.instance = this;
		}

		// Token: 0x06008695 RID: 34453 RVA: 0x002EF7B8 File Offset: 0x002ED9B8
		public bool IsOn()
		{
			return base.sm.isOn.Get(base.smi);
		}

		// Token: 0x06008696 RID: 34454 RVA: 0x002EF7D0 File Offset: 0x002ED9D0
		public void HasTopPriorityChore(bool on)
		{
			this.hasTopPriorityChore = on;
			this.Refresh();
		}

		// Token: 0x06008697 RID: 34455 RVA: 0x002EF7DF File Offset: 0x002ED9DF
		private void Refresh()
		{
			base.sm.isOn.Set(this.hasTopPriorityChore, base.smi, false);
		}

		// Token: 0x040068D4 RID: 26836
		private static YellowAlertManager.Instance instance;

		// Token: 0x040068D5 RID: 26837
		private bool hasTopPriorityChore;

		// Token: 0x040068D6 RID: 26838
		public Notification notification = new Notification(MISC.NOTIFICATIONS.YELLOWALERT.NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.YELLOWALERT.TOOLTIP, null, false, 0f, null, null, null, true, false, false);
	}
}
