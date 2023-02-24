using System;
using STRINGS;

// Token: 0x020000B8 RID: 184
public class DisabledCreatureStates : GameStateMachine<DisabledCreatureStates, DisabledCreatureStates.Instance, IStateMachineTarget, DisabledCreatureStates.Def>
{
	// Token: 0x06000340 RID: 832 RVA: 0x00019BB4 File Offset: 0x00017DB4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.disableCreature;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.DISABLED.NAME, CREATURES.STATUSITEMS.DISABLED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).TagTransition(GameTags.Creatures.Behaviours.DisableCreature, this.behaviourcomplete, true);
		this.disableCreature.PlayAnim((DisabledCreatureStates.Instance smi) => smi.def.disabledAnim, KAnim.PlayMode.Once);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Behaviours.DisableCreature, false);
	}

	// Token: 0x0400021D RID: 541
	public GameStateMachine<DisabledCreatureStates, DisabledCreatureStates.Instance, IStateMachineTarget, DisabledCreatureStates.Def>.State disableCreature;

	// Token: 0x0400021E RID: 542
	public GameStateMachine<DisabledCreatureStates, DisabledCreatureStates.Instance, IStateMachineTarget, DisabledCreatureStates.Def>.State behaviourcomplete;

	// Token: 0x02000E45 RID: 3653
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06006BED RID: 27629 RVA: 0x002972B4 File Offset: 0x002954B4
		public Def(string anim)
		{
			this.disabledAnim = anim;
		}

		// Token: 0x0400513E RID: 20798
		public string disabledAnim = "off";
	}

	// Token: 0x02000E46 RID: 3654
	public new class Instance : GameStateMachine<DisabledCreatureStates, DisabledCreatureStates.Instance, IStateMachineTarget, DisabledCreatureStates.Def>.GameInstance
	{
		// Token: 0x06006BEE RID: 27630 RVA: 0x002972CE File Offset: 0x002954CE
		public Instance(Chore<DisabledCreatureStates.Instance> chore, DisabledCreatureStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.HasTag, GameTags.Creatures.Behaviours.DisableCreature);
		}
	}
}
