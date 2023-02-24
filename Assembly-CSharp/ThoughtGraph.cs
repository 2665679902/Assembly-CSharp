using System;
using System.Collections.Generic;

// Token: 0x020004DC RID: 1244
public class ThoughtGraph : GameStateMachine<ThoughtGraph, ThoughtGraph.Instance>
{
	// Token: 0x06001D7E RID: 7550 RVA: 0x0009D4F8 File Offset: 0x0009B6F8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.initialdelay;
		this.initialdelay.ScheduleGoTo(1f, this.nothoughts);
		this.nothoughts.OnSignal(this.thoughtsChanged, this.displayingthought, (ThoughtGraph.Instance smi) => smi.HasThoughts()).OnSignal(this.thoughtsChangedImmediate, this.displayingthought, (ThoughtGraph.Instance smi) => smi.HasThoughts());
		this.displayingthought.DefaultState(this.displayingthought.pre).Enter("CreateBubble", delegate(ThoughtGraph.Instance smi)
		{
			smi.CreateBubble();
		}).Exit("DestroyBubble", delegate(ThoughtGraph.Instance smi)
		{
			smi.DestroyBubble();
		})
			.ScheduleGoTo((ThoughtGraph.Instance smi) => this.thoughtDisplayTime.Get(smi), this.cooldown);
		this.displayingthought.pre.ScheduleGoTo((ThoughtGraph.Instance smi) => TuningData<ThoughtGraph.Tuning>.Get().preLengthInSeconds, this.displayingthought.talking);
		this.displayingthought.talking.Enter(new StateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.State.Callback(ThoughtGraph.BeginTalking));
		this.cooldown.OnSignal(this.thoughtsChangedImmediate, this.displayingthought, (ThoughtGraph.Instance smi) => smi.HasImmediateThought()).ScheduleGoTo(20f, this.nothoughts);
	}

	// Token: 0x06001D7F RID: 7551 RVA: 0x0009D6AA File Offset: 0x0009B8AA
	private static void BeginTalking(ThoughtGraph.Instance smi)
	{
		if (smi.currentThought == null)
		{
			return;
		}
		if (SpeechMonitor.IsAllowedToPlaySpeech(smi.gameObject))
		{
			smi.GetSMI<SpeechMonitor.Instance>().PlaySpeech(smi.currentThought.speechPrefix, smi.currentThought.sound);
		}
	}

	// Token: 0x040010A5 RID: 4261
	public StateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.Signal thoughtsChanged;

	// Token: 0x040010A6 RID: 4262
	public StateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.Signal thoughtsChangedImmediate;

	// Token: 0x040010A7 RID: 4263
	public StateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.FloatParameter thoughtDisplayTime;

	// Token: 0x040010A8 RID: 4264
	public GameStateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.State initialdelay;

	// Token: 0x040010A9 RID: 4265
	public GameStateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.State nothoughts;

	// Token: 0x040010AA RID: 4266
	public ThoughtGraph.DisplayingThoughtState displayingthought;

	// Token: 0x040010AB RID: 4267
	public GameStateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.State cooldown;

	// Token: 0x02001120 RID: 4384
	public class Tuning : TuningData<ThoughtGraph.Tuning>
	{
		// Token: 0x040059EA RID: 23018
		public float preLengthInSeconds;
	}

	// Token: 0x02001121 RID: 4385
	public class DisplayingThoughtState : GameStateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040059EB RID: 23019
		public GameStateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.State pre;

		// Token: 0x040059EC RID: 23020
		public GameStateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.State talking;
	}

	// Token: 0x02001122 RID: 4386
	public new class Instance : GameStateMachine<ThoughtGraph, ThoughtGraph.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600759E RID: 30110 RVA: 0x002B6944 File Offset: 0x002B4B44
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			NameDisplayScreen.Instance.RegisterComponent(base.gameObject, this, false);
		}

		// Token: 0x0600759F RID: 30111 RVA: 0x002B696A File Offset: 0x002B4B6A
		public bool HasThoughts()
		{
			return this.thoughts.Count > 0;
		}

		// Token: 0x060075A0 RID: 30112 RVA: 0x002B697C File Offset: 0x002B4B7C
		public bool HasImmediateThought()
		{
			bool flag = false;
			for (int i = 0; i < this.thoughts.Count; i++)
			{
				if (this.thoughts[i].showImmediately)
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x060075A1 RID: 30113 RVA: 0x002B69BC File Offset: 0x002B4BBC
		public void AddThought(Thought thought)
		{
			if (this.thoughts.Contains(thought))
			{
				return;
			}
			this.thoughts.Add(thought);
			if (thought.showImmediately)
			{
				base.sm.thoughtsChangedImmediate.Trigger(base.smi);
				return;
			}
			base.sm.thoughtsChanged.Trigger(base.smi);
		}

		// Token: 0x060075A2 RID: 30114 RVA: 0x002B6A19 File Offset: 0x002B4C19
		public void RemoveThought(Thought thought)
		{
			if (!this.thoughts.Contains(thought))
			{
				return;
			}
			this.thoughts.Remove(thought);
			base.sm.thoughtsChanged.Trigger(base.smi);
		}

		// Token: 0x060075A3 RID: 30115 RVA: 0x002B6A4D File Offset: 0x002B4C4D
		private int SortThoughts(Thought a, Thought b)
		{
			if (a.showImmediately == b.showImmediately)
			{
				return b.priority.CompareTo(a.priority);
			}
			if (!a.showImmediately)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x060075A4 RID: 30116 RVA: 0x002B6A7C File Offset: 0x002B4C7C
		public void CreateBubble()
		{
			if (this.thoughts.Count == 0)
			{
				return;
			}
			this.thoughts.Sort(new Comparison<Thought>(this.SortThoughts));
			Thought thought = this.thoughts[0];
			if (thought.modeSprite != null)
			{
				NameDisplayScreen.Instance.SetThoughtBubbleConvoDisplay(base.gameObject, true, thought.hoverText, thought.bubbleSprite, thought.sprite, thought.modeSprite);
			}
			else
			{
				NameDisplayScreen.Instance.SetThoughtBubbleDisplay(base.gameObject, true, thought.hoverText, thought.bubbleSprite, thought.sprite);
			}
			base.sm.thoughtDisplayTime.Set(thought.showTime, this, false);
			this.currentThought = thought;
			if (thought.showImmediately)
			{
				this.thoughts.RemoveAt(0);
			}
		}

		// Token: 0x060075A5 RID: 30117 RVA: 0x002B6B55 File Offset: 0x002B4D55
		public void DestroyBubble()
		{
			NameDisplayScreen.Instance.SetThoughtBubbleDisplay(base.gameObject, false, null, null, null);
			NameDisplayScreen.Instance.SetThoughtBubbleConvoDisplay(base.gameObject, false, null, null, null, null);
		}

		// Token: 0x040059ED RID: 23021
		private List<Thought> thoughts = new List<Thought>();

		// Token: 0x040059EE RID: 23022
		public Thought currentThought;
	}
}
