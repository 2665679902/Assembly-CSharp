using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000853 RID: 2131
public class VignetteManager : GameStateMachine<VignetteManager, VignetteManager.Instance>
{
	// Token: 0x06003D42 RID: 15682 RVA: 0x001566F0 File Offset: 0x001548F0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.ParamTransition<bool>(this.isOn, this.on, GameStateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.IsTrue);
		this.on.Exit("VignetteOff", delegate(VignetteManager.Instance smi)
		{
			Vignette.Instance.Reset();
		}).ParamTransition<bool>(this.isRedAlert, this.on.red, (VignetteManager.Instance smi, bool p) => this.isRedAlert.Get(smi)).ParamTransition<bool>(this.isRedAlert, this.on.yellow, (VignetteManager.Instance smi, bool p) => this.isYellowAlert.Get(smi) && !this.isRedAlert.Get(smi))
			.ParamTransition<bool>(this.isYellowAlert, this.on.yellow, (VignetteManager.Instance smi, bool p) => this.isYellowAlert.Get(smi) && !this.isRedAlert.Get(smi))
			.ParamTransition<bool>(this.isOn, this.off, GameStateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.IsFalse);
		this.on.red.Enter("EnterEvent", delegate(VignetteManager.Instance smi)
		{
			Game.Instance.Trigger(1585324898, null);
		}).Exit("ExitEvent", delegate(VignetteManager.Instance smi)
		{
			Game.Instance.Trigger(-1393151672, null);
		}).Enter("EnableVignette", delegate(VignetteManager.Instance smi)
		{
			Vignette.Instance.SetColor(new Color(1f, 0f, 0f, 0.3f));
		})
			.Enter("SoundsOnRedAlert", delegate(VignetteManager.Instance smi)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("RedAlert_ON", false));
			})
			.Exit("SoundsOffRedAlert", delegate(VignetteManager.Instance smi)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("RedAlert_OFF", false));
			})
			.ToggleLoopingSound(GlobalAssets.GetSound("RedAlert_LP", false), null, true, false, true)
			.ToggleNotification((VignetteManager.Instance smi) => smi.redAlertNotification);
		this.on.yellow.Enter("EnterEvent", delegate(VignetteManager.Instance smi)
		{
			Game.Instance.Trigger(-741654735, null);
		}).Exit("ExitEvent", delegate(VignetteManager.Instance smi)
		{
			Game.Instance.Trigger(-2062778933, null);
		}).Enter("EnableVignette", delegate(VignetteManager.Instance smi)
		{
			Vignette.Instance.SetColor(new Color(1f, 1f, 0f, 0.3f));
		})
			.Enter("SoundsOnYellowAlert", delegate(VignetteManager.Instance smi)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("YellowAlert_ON", false));
			})
			.Exit("SoundsOffRedAlert", delegate(VignetteManager.Instance smi)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("YellowAlert_OFF", false));
			})
			.ToggleLoopingSound(GlobalAssets.GetSound("YellowAlert_LP", false), null, true, false, true);
	}

	// Token: 0x0400281F RID: 10271
	public GameStateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x04002820 RID: 10272
	public VignetteManager.OnStates on;

	// Token: 0x04002821 RID: 10273
	public StateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.BoolParameter isRedAlert = new StateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.BoolParameter();

	// Token: 0x04002822 RID: 10274
	public StateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.BoolParameter isYellowAlert = new StateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.BoolParameter();

	// Token: 0x04002823 RID: 10275
	public StateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.BoolParameter isOn = new StateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.BoolParameter();

	// Token: 0x02001605 RID: 5637
	public class OnStates : GameStateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040068B7 RID: 26807
		public GameStateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.State yellow;

		// Token: 0x040068B8 RID: 26808
		public GameStateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.State red;
	}

	// Token: 0x02001606 RID: 5638
	public new class Instance : GameStateMachine<VignetteManager, VignetteManager.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600866B RID: 34411 RVA: 0x002EF03A File Offset: 0x002ED23A
		public static void DestroyInstance()
		{
			VignetteManager.Instance.instance = null;
		}

		// Token: 0x0600866C RID: 34412 RVA: 0x002EF042 File Offset: 0x002ED242
		public static VignetteManager.Instance Get()
		{
			return VignetteManager.Instance.instance;
		}

		// Token: 0x0600866D RID: 34413 RVA: 0x002EF04C File Offset: 0x002ED24C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			VignetteManager.Instance.instance = this;
		}

		// Token: 0x0600866E RID: 34414 RVA: 0x002EF0A8 File Offset: 0x002ED2A8
		public void UpdateState(float dt)
		{
			if (this.IsRedAlert())
			{
				base.smi.GoTo(base.sm.on.red);
				return;
			}
			if (this.IsYellowAlert())
			{
				base.smi.GoTo(base.sm.on.yellow);
				return;
			}
			if (!this.IsOn())
			{
				base.smi.GoTo(base.sm.off);
			}
		}

		// Token: 0x0600866F RID: 34415 RVA: 0x002EF11B File Offset: 0x002ED31B
		public bool IsOn()
		{
			return base.sm.isYellowAlert.Get(base.smi) || base.sm.isRedAlert.Get(base.smi);
		}

		// Token: 0x06008670 RID: 34416 RVA: 0x002EF14D File Offset: 0x002ED34D
		public bool IsRedAlert()
		{
			return base.sm.isRedAlert.Get(base.smi);
		}

		// Token: 0x06008671 RID: 34417 RVA: 0x002EF165 File Offset: 0x002ED365
		public bool IsYellowAlert()
		{
			return base.sm.isYellowAlert.Get(base.smi);
		}

		// Token: 0x06008672 RID: 34418 RVA: 0x002EF17D File Offset: 0x002ED37D
		public bool IsRedAlertToggledOn()
		{
			return this.isToggled;
		}

		// Token: 0x06008673 RID: 34419 RVA: 0x002EF185 File Offset: 0x002ED385
		public void ToggleRedAlert(bool on)
		{
			this.isToggled = on;
			this.Refresh();
		}

		// Token: 0x06008674 RID: 34420 RVA: 0x002EF194 File Offset: 0x002ED394
		public void HasTopPriorityChore(bool on)
		{
			this.hasTopPriorityChore = on;
			this.Refresh();
		}

		// Token: 0x06008675 RID: 34421 RVA: 0x002EF1A4 File Offset: 0x002ED3A4
		private void Refresh()
		{
			base.sm.isYellowAlert.Set(this.hasTopPriorityChore, base.smi, false);
			base.sm.isRedAlert.Set(this.isToggled, base.smi, false);
			base.sm.isOn.Set(this.hasTopPriorityChore || this.isToggled, base.smi, false);
		}

		// Token: 0x040068B9 RID: 26809
		private static VignetteManager.Instance instance;

		// Token: 0x040068BA RID: 26810
		private bool isToggled;

		// Token: 0x040068BB RID: 26811
		private bool hasTopPriorityChore;

		// Token: 0x040068BC RID: 26812
		public Notification redAlertNotification = new Notification(MISC.NOTIFICATIONS.REDALERT.NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.REDALERT.TOOLTIP, null, false, 0f, null, null, null, true, false, false);
	}
}
