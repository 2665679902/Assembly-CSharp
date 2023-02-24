using System;
using STRINGS;

// Token: 0x02000823 RID: 2083
public class DeathMonitor : GameStateMachine<DeathMonitor, DeathMonitor.Instance, IStateMachineTarget, DeathMonitor.Def>
{
	// Token: 0x06003C63 RID: 15459 RVA: 0x00150694 File Offset: 0x0014E894
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.alive;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.alive.ParamTransition<Death>(this.death, this.dying_duplicant, (DeathMonitor.Instance smi, Death p) => p != null && smi.IsDuplicant).ParamTransition<Death>(this.death, this.dying_creature, (DeathMonitor.Instance smi, Death p) => p != null && !smi.IsDuplicant);
		this.dying_duplicant.ToggleAnims("anim_emotes_default_kanim", 0f, "").ToggleTag(GameTags.Dying).ToggleChore((DeathMonitor.Instance smi) => new DieChore(smi.master, this.death.Get(smi)), this.die);
		this.dying_creature.ToggleBehaviour(GameTags.Creatures.Die, (DeathMonitor.Instance smi) => true, delegate(DeathMonitor.Instance smi)
		{
			smi.GoTo(this.dead_creature);
		});
		this.die.ToggleTag(GameTags.Dying).Enter("Die", delegate(DeathMonitor.Instance smi)
		{
			smi.gameObject.AddTag(GameTags.PreventChoreInterruption);
			Death death = this.death.Get(smi);
			if (smi.IsDuplicant)
			{
				DeathMessage deathMessage = new DeathMessage(smi.gameObject, death);
				KFMOD.PlayOneShot(GlobalAssets.GetSound("Death_Notification_localized", false), smi.master.transform.GetPosition(), 1f);
				KFMOD.PlayUISound(GlobalAssets.GetSound("Death_Notification_ST", false));
				Messenger.Instance.QueueMessage(deathMessage);
			}
		}).TriggerOnExit(GameHashes.Died, null)
			.GoTo(this.dead);
		this.dead.ToggleAnims("anim_emotes_default_kanim", 0f, "").DefaultState(this.dead.ground).ToggleTag(GameTags.Dead)
			.Enter(delegate(DeathMonitor.Instance smi)
			{
				smi.ApplyDeath();
				Game.Instance.Trigger(282337316, smi.gameObject);
			});
		this.dead.ground.Enter(delegate(DeathMonitor.Instance smi)
		{
			Death death2 = this.death.Get(smi);
			if (death2 == null)
			{
				death2 = Db.Get().Deaths.Generic;
			}
			if (smi.IsDuplicant)
			{
				smi.GetComponent<KAnimControllerBase>().Play(death2.loopAnim, KAnim.PlayMode.Loop, 1f, 0f);
			}
		}).EventTransition(GameHashes.OnStore, this.dead.carried, (DeathMonitor.Instance smi) => smi.IsDuplicant && smi.HasTag(GameTags.Stored));
		this.dead.carried.ToggleAnims("anim_dead_carried_kanim", 0f, "").PlayAnim("idle_default", KAnim.PlayMode.Loop).EventTransition(GameHashes.OnStore, this.dead.ground, (DeathMonitor.Instance smi) => !smi.HasTag(GameTags.Stored));
		this.dead_creature.Enter(delegate(DeathMonitor.Instance smi)
		{
			smi.gameObject.AddTag(GameTags.Dead);
		}).PlayAnim("idle_dead", KAnim.PlayMode.Loop);
	}

	// Token: 0x04002751 RID: 10065
	public GameStateMachine<DeathMonitor, DeathMonitor.Instance, IStateMachineTarget, DeathMonitor.Def>.State alive;

	// Token: 0x04002752 RID: 10066
	public GameStateMachine<DeathMonitor, DeathMonitor.Instance, IStateMachineTarget, DeathMonitor.Def>.State dying_duplicant;

	// Token: 0x04002753 RID: 10067
	public GameStateMachine<DeathMonitor, DeathMonitor.Instance, IStateMachineTarget, DeathMonitor.Def>.State dying_creature;

	// Token: 0x04002754 RID: 10068
	public GameStateMachine<DeathMonitor, DeathMonitor.Instance, IStateMachineTarget, DeathMonitor.Def>.State die;

	// Token: 0x04002755 RID: 10069
	public DeathMonitor.Dead dead;

	// Token: 0x04002756 RID: 10070
	public DeathMonitor.Dead dead_creature;

	// Token: 0x04002757 RID: 10071
	public StateMachine<DeathMonitor, DeathMonitor.Instance, IStateMachineTarget, DeathMonitor.Def>.ResourceParameter<Death> death;

	// Token: 0x02001590 RID: 5520
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001591 RID: 5521
	public class Dead : GameStateMachine<DeathMonitor, DeathMonitor.Instance, IStateMachineTarget, DeathMonitor.Def>.State
	{
		// Token: 0x04006714 RID: 26388
		public GameStateMachine<DeathMonitor, DeathMonitor.Instance, IStateMachineTarget, DeathMonitor.Def>.State ground;

		// Token: 0x04006715 RID: 26389
		public GameStateMachine<DeathMonitor, DeathMonitor.Instance, IStateMachineTarget, DeathMonitor.Def>.State carried;
	}

	// Token: 0x02001592 RID: 5522
	public new class Instance : GameStateMachine<DeathMonitor, DeathMonitor.Instance, IStateMachineTarget, DeathMonitor.Def>.GameInstance
	{
		// Token: 0x06008458 RID: 33880 RVA: 0x002E9B0C File Offset: 0x002E7D0C
		public Instance(IStateMachineTarget master, DeathMonitor.Def def)
			: base(master, def)
		{
			this.isDuplicant = base.GetComponent<MinionIdentity>();
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06008459 RID: 33881 RVA: 0x002E9B27 File Offset: 0x002E7D27
		public bool IsDuplicant
		{
			get
			{
				return this.isDuplicant;
			}
		}

		// Token: 0x0600845A RID: 33882 RVA: 0x002E9B2F File Offset: 0x002E7D2F
		public void Kill(Death death)
		{
			base.sm.death.Set(death, base.smi, false);
		}

		// Token: 0x0600845B RID: 33883 RVA: 0x002E9B4A File Offset: 0x002E7D4A
		public void PickedUp(object data = null)
		{
			if (data is Storage || (data != null && (bool)data))
			{
				base.smi.GoTo(base.sm.dead.carried);
			}
		}

		// Token: 0x0600845C RID: 33884 RVA: 0x002E9B80 File Offset: 0x002E7D80
		public bool IsDead()
		{
			return base.smi.IsInsideState(base.smi.sm.dead);
		}

		// Token: 0x0600845D RID: 33885 RVA: 0x002E9BA0 File Offset: 0x002E7DA0
		public void ApplyDeath()
		{
			if (this.isDuplicant)
			{
				Game.Instance.assignmentManager.RemoveFromAllGroups(base.GetComponent<MinionIdentity>().assignableProxy.Get());
				base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().DuplicantStatusItems.Dead, base.smi.sm.death.Get(base.smi));
				float num = 600f - GameClock.Instance.GetTimeSinceStartOfReport();
				ReportManager.Instance.ReportValue(ReportManager.ReportType.PersonalTime, num, string.Format(UI.ENDOFDAYREPORT.NOTES.PERSONAL_TIME, DUPLICANTS.CHORES.IS_DEAD_TASK), base.smi.master.gameObject.GetProperName());
				Pickupable component = base.GetComponent<Pickupable>();
				if (component != null)
				{
					component.RegisterListeners();
				}
			}
			base.GetComponent<KPrefabID>().AddTag(GameTags.Corpse, false);
		}

		// Token: 0x04006716 RID: 26390
		private bool isDuplicant;
	}
}
