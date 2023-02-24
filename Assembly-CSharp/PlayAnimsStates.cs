using System;

// Token: 0x020000AF RID: 175
public class PlayAnimsStates : GameStateMachine<PlayAnimsStates, PlayAnimsStates.Instance, IStateMachineTarget, PlayAnimsStates.Def>
{
	// Token: 0x0600030C RID: 780 RVA: 0x000183EC File Offset: 0x000165EC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.animating;
		this.root.ToggleStatusItem("Unused", "Unused", "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, (string str, PlayAnimsStates.Instance smi) => smi.def.statusItemName, (string str, PlayAnimsStates.Instance smi) => smi.def.statusItemTooltip, Db.Get().StatusItemCategories.Main);
		this.animating.Enter("PlayAnims", delegate(PlayAnimsStates.Instance smi)
		{
			smi.PlayAnims();
		}).OnAnimQueueComplete(this.done).EventHandler(GameHashes.TagsChanged, delegate(PlayAnimsStates.Instance smi, object obj)
		{
			smi.HandleTagsChanged(obj);
		});
		this.done.BehaviourComplete((PlayAnimsStates.Instance smi) => smi.def.tag, false);
	}

	// Token: 0x040001FF RID: 511
	public GameStateMachine<PlayAnimsStates, PlayAnimsStates.Instance, IStateMachineTarget, PlayAnimsStates.Def>.State animating;

	// Token: 0x04000200 RID: 512
	public GameStateMachine<PlayAnimsStates, PlayAnimsStates.Instance, IStateMachineTarget, PlayAnimsStates.Def>.State done;

	// Token: 0x02000E24 RID: 3620
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06006B9F RID: 27551 RVA: 0x00296BB4 File Offset: 0x00294DB4
		public Def(Tag tag, bool loop, string anim, string status_item_name, string status_item_tooltip)
			: this(tag, loop, new string[] { anim }, status_item_name, status_item_tooltip)
		{
		}

		// Token: 0x06006BA0 RID: 27552 RVA: 0x00296BCC File Offset: 0x00294DCC
		public Def(Tag tag, bool loop, string[] anims, string status_item_name, string status_item_tooltip)
		{
			this.tag = tag;
			this.loop = loop;
			this.anims = anims;
			this.statusItemName = status_item_name;
			this.statusItemTooltip = status_item_tooltip;
		}

		// Token: 0x06006BA1 RID: 27553 RVA: 0x00296BF9 File Offset: 0x00294DF9
		public override string ToString()
		{
			return this.tag.ToString() + "(PlayAnimsStates)";
		}

		// Token: 0x040050EF RID: 20719
		public Tag tag;

		// Token: 0x040050F0 RID: 20720
		public string[] anims;

		// Token: 0x040050F1 RID: 20721
		public bool loop;

		// Token: 0x040050F2 RID: 20722
		public string statusItemName;

		// Token: 0x040050F3 RID: 20723
		public string statusItemTooltip;
	}

	// Token: 0x02000E25 RID: 3621
	public new class Instance : GameStateMachine<PlayAnimsStates, PlayAnimsStates.Instance, IStateMachineTarget, PlayAnimsStates.Def>.GameInstance
	{
		// Token: 0x06006BA2 RID: 27554 RVA: 0x00296C16 File Offset: 0x00294E16
		public Instance(Chore<PlayAnimsStates.Instance> chore, PlayAnimsStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, def.tag);
		}

		// Token: 0x06006BA3 RID: 27555 RVA: 0x00296C3C File Offset: 0x00294E3C
		public void PlayAnims()
		{
			if (base.def.anims == null || base.def.anims.Length == 0)
			{
				return;
			}
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			for (int i = 0; i < base.def.anims.Length; i++)
			{
				KAnim.PlayMode playMode = KAnim.PlayMode.Once;
				if (base.def.loop && i == base.def.anims.Length - 1)
				{
					playMode = KAnim.PlayMode.Loop;
				}
				if (i == 0)
				{
					component.Play(base.def.anims[i], playMode, 1f, 0f);
				}
				else
				{
					component.Queue(base.def.anims[i], playMode, 1f, 0f);
				}
			}
		}

		// Token: 0x06006BA4 RID: 27556 RVA: 0x00296CF5 File Offset: 0x00294EF5
		public void HandleTagsChanged(object obj)
		{
			if (!base.smi.HasTag(base.smi.def.tag))
			{
				base.smi.GoTo(null);
			}
		}
	}
}
