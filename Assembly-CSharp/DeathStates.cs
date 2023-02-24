using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public class DeathStates : GameStateMachine<DeathStates, DeathStates.Instance, IStateMachineTarget, DeathStates.Def>
{
	// Token: 0x06000334 RID: 820 RVA: 0x00019798 File Offset: 0x00017998
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		this.loop.ToggleStatusItem(CREATURES.STATUSITEMS.DEAD.NAME, CREATURES.STATUSITEMS.DEAD.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).Enter("EnableGravity", delegate(DeathStates.Instance smi)
		{
			smi.EnableGravityIfNecessary();
		}).PlayAnim("Death")
			.OnAnimQueueComplete(this.pst);
		this.pst.TriggerOnEnter(GameHashes.DeathAnimComplete, null).TriggerOnEnter(GameHashes.Died, null).Enter("Butcher", delegate(DeathStates.Instance smi)
		{
			if (smi.gameObject.GetComponent<Butcherable>() != null)
			{
				smi.GetComponent<Butcherable>().OnButcherComplete();
			}
		})
			.Enter("Destroy", delegate(DeathStates.Instance smi)
			{
				smi.gameObject.AddTag(GameTags.Dead);
				smi.gameObject.DeleteObject();
			})
			.BehaviourComplete(GameTags.Creatures.Die, false);
	}

	// Token: 0x04000213 RID: 531
	private GameStateMachine<DeathStates, DeathStates.Instance, IStateMachineTarget, DeathStates.Def>.State loop;

	// Token: 0x04000214 RID: 532
	private GameStateMachine<DeathStates, DeathStates.Instance, IStateMachineTarget, DeathStates.Def>.State pst;

	// Token: 0x02000E39 RID: 3641
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E3A RID: 3642
	public new class Instance : GameStateMachine<DeathStates, DeathStates.Instance, IStateMachineTarget, DeathStates.Def>.GameInstance
	{
		// Token: 0x06006BD7 RID: 27607 RVA: 0x002970C8 File Offset: 0x002952C8
		public Instance(Chore<DeathStates.Instance> chore, DeathStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Die);
		}

		// Token: 0x06006BD8 RID: 27608 RVA: 0x002970EC File Offset: 0x002952EC
		public void EnableGravityIfNecessary()
		{
			if (base.HasTag(GameTags.Creatures.Flyer))
			{
				GameComps.Gravities.Add(base.smi.gameObject, Vector2.zero, delegate
				{
					base.smi.DisableGravity();
				});
			}
		}

		// Token: 0x06006BD9 RID: 27609 RVA: 0x00297122 File Offset: 0x00295322
		public void DisableGravity()
		{
			if (GameComps.Gravities.Has(base.smi.gameObject))
			{
				GameComps.Gravities.Remove(base.smi.gameObject);
			}
		}
	}
}
