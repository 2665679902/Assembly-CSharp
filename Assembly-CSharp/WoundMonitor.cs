using System;

// Token: 0x02000854 RID: 2132
public class WoundMonitor : GameStateMachine<WoundMonitor, WoundMonitor.Instance>
{
	// Token: 0x06003D47 RID: 15687 RVA: 0x00156A48 File Offset: 0x00154C48
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.healthy;
		this.root.ToggleAnims("anim_hits_kanim", 0f, "").EventHandler(GameHashes.HealthChanged, delegate(WoundMonitor.Instance smi, object data)
		{
			smi.OnHealthChanged(data);
		});
		this.healthy.EventTransition(GameHashes.HealthChanged, this.wounded, (WoundMonitor.Instance smi) => smi.health.State > Health.HealthState.Perfect);
		this.wounded.ToggleUrge(Db.Get().Urges.Heal).Enter(delegate(WoundMonitor.Instance smi)
		{
			switch (smi.health.State)
			{
			case Health.HealthState.Scuffed:
				smi.GoTo(this.wounded.light);
				return;
			case Health.HealthState.Injured:
				smi.GoTo(this.wounded.medium);
				return;
			case Health.HealthState.Critical:
				smi.GoTo(this.wounded.heavy);
				return;
			default:
				return;
			}
		}).EventHandler(GameHashes.HealthChanged, delegate(WoundMonitor.Instance smi)
		{
			smi.GoToProperHeathState();
		});
		this.wounded.medium.ToggleAnims("anim_loco_wounded_kanim", 1f, "");
		this.wounded.heavy.ToggleAnims("anim_loco_wounded_kanim", 3f, "").Update("LookForAvailableClinic", delegate(WoundMonitor.Instance smi, float dt)
		{
			smi.FindAvailableMedicalBed();
		}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x04002824 RID: 10276
	public GameStateMachine<WoundMonitor, WoundMonitor.Instance, IStateMachineTarget, object>.State healthy;

	// Token: 0x04002825 RID: 10277
	public WoundMonitor.Wounded wounded;

	// Token: 0x02001608 RID: 5640
	public class Wounded : GameStateMachine<WoundMonitor, WoundMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040068CA RID: 26826
		public GameStateMachine<WoundMonitor, WoundMonitor.Instance, IStateMachineTarget, object>.State light;

		// Token: 0x040068CB RID: 26827
		public GameStateMachine<WoundMonitor, WoundMonitor.Instance, IStateMachineTarget, object>.State medium;

		// Token: 0x040068CC RID: 26828
		public GameStateMachine<WoundMonitor, WoundMonitor.Instance, IStateMachineTarget, object>.State heavy;
	}

	// Token: 0x02001609 RID: 5641
	public new class Instance : GameStateMachine<WoundMonitor, WoundMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008685 RID: 34437 RVA: 0x002EF320 File Offset: 0x002ED520
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.health = master.GetComponent<Health>();
			this.worker = master.GetComponent<Worker>();
		}

		// Token: 0x06008686 RID: 34438 RVA: 0x002EF344 File Offset: 0x002ED544
		public void OnHealthChanged(object data)
		{
			float num = (float)data;
			if (this.health.hitPoints != 0f && num < 0f)
			{
				this.PlayHitAnimation();
			}
		}

		// Token: 0x06008687 RID: 34439 RVA: 0x002EF378 File Offset: 0x002ED578
		private void PlayHitAnimation()
		{
			string text = null;
			KBatchedAnimController kbatchedAnimController = base.smi.Get<KBatchedAnimController>();
			if (kbatchedAnimController.CurrentAnim != null)
			{
				text = kbatchedAnimController.CurrentAnim.name;
			}
			KAnim.PlayMode playMode = kbatchedAnimController.PlayMode;
			if (text != null)
			{
				if (text.Contains("hit"))
				{
					return;
				}
				if (text.Contains("2_0"))
				{
					return;
				}
				if (text.Contains("2_1"))
				{
					return;
				}
				if (text.Contains("2_-1"))
				{
					return;
				}
				if (text.Contains("2_-2"))
				{
					return;
				}
				if (text.Contains("1_-1"))
				{
					return;
				}
				if (text.Contains("1_-2"))
				{
					return;
				}
				if (text.Contains("1_1"))
				{
					return;
				}
				if (text.Contains("1_2"))
				{
					return;
				}
				if (text.Contains("breathe_"))
				{
					return;
				}
				if (text.Contains("death_"))
				{
					return;
				}
				if (text.Contains("impact"))
				{
					return;
				}
			}
			string text2 = "hit";
			AttackChore.StatesInstance smi = base.gameObject.GetSMI<AttackChore.StatesInstance>();
			if (smi != null && smi.GetCurrentState() == smi.sm.attack)
			{
				text2 = smi.master.GetHitAnim();
			}
			if (this.worker.GetComponent<Navigator>().CurrentNavType == NavType.Ladder)
			{
				text2 = "hit_ladder";
			}
			else if (this.worker.GetComponent<Navigator>().CurrentNavType == NavType.Pole)
			{
				text2 = "hit_pole";
			}
			kbatchedAnimController.Play(text2, KAnim.PlayMode.Once, 1f, 0f);
			if (text != null)
			{
				kbatchedAnimController.Queue(text, playMode, 1f, 0f);
			}
		}

		// Token: 0x06008688 RID: 34440 RVA: 0x002EF4FC File Offset: 0x002ED6FC
		public void PlayKnockedOverImpactAnimation()
		{
			string text = null;
			KBatchedAnimController kbatchedAnimController = base.smi.Get<KBatchedAnimController>();
			if (kbatchedAnimController.CurrentAnim != null)
			{
				text = kbatchedAnimController.CurrentAnim.name;
			}
			KAnim.PlayMode playMode = kbatchedAnimController.PlayMode;
			if (text != null)
			{
				if (text.Contains("impact"))
				{
					return;
				}
				if (text.Contains("2_0"))
				{
					return;
				}
				if (text.Contains("2_1"))
				{
					return;
				}
				if (text.Contains("2_-1"))
				{
					return;
				}
				if (text.Contains("2_-2"))
				{
					return;
				}
				if (text.Contains("1_-1"))
				{
					return;
				}
				if (text.Contains("1_-2"))
				{
					return;
				}
				if (text.Contains("1_1"))
				{
					return;
				}
				if (text.Contains("1_2"))
				{
					return;
				}
				if (text.Contains("breathe_"))
				{
					return;
				}
				if (text.Contains("death_"))
				{
					return;
				}
			}
			string text2 = "impact";
			kbatchedAnimController.Play(text2, KAnim.PlayMode.Once, 1f, 0f);
			if (text != null)
			{
				kbatchedAnimController.Queue(text, playMode, 1f, 0f);
			}
		}

		// Token: 0x06008689 RID: 34441 RVA: 0x002EF60C File Offset: 0x002ED80C
		public void GoToProperHeathState()
		{
			switch (base.smi.health.State)
			{
			case Health.HealthState.Perfect:
				base.smi.GoTo(base.sm.healthy);
				return;
			case Health.HealthState.Alright:
				break;
			case Health.HealthState.Scuffed:
				base.smi.GoTo(base.sm.wounded.light);
				break;
			case Health.HealthState.Injured:
				base.smi.GoTo(base.sm.wounded.medium);
				return;
			case Health.HealthState.Critical:
				base.smi.GoTo(base.sm.wounded.heavy);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600868A RID: 34442 RVA: 0x002EF6AF File Offset: 0x002ED8AF
		public bool ShouldExitInfirmary()
		{
			return this.health.State == Health.HealthState.Perfect;
		}

		// Token: 0x0600868B RID: 34443 RVA: 0x002EF6C4 File Offset: 0x002ED8C4
		public void FindAvailableMedicalBed()
		{
			AssignableSlot clinic = Db.Get().AssignableSlots.Clinic;
			Ownables soleOwner = base.gameObject.GetComponent<MinionIdentity>().GetSoleOwner();
			if (soleOwner.GetSlot(clinic).assignable == null)
			{
				soleOwner.AutoAssignSlot(clinic);
			}
		}

		// Token: 0x040068CD RID: 26829
		public Health health;

		// Token: 0x040068CE RID: 26830
		private Worker worker;
	}
}
