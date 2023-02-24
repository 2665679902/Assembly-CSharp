using System;
using Klei.AI;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x02000835 RID: 2101
public class JoyBehaviourMonitor : GameStateMachine<JoyBehaviourMonitor, JoyBehaviourMonitor.Instance>
{
	// Token: 0x06003CAB RID: 15531 RVA: 0x00152760 File Offset: 0x00150960
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.EventHandler(GameHashes.TagsChanged, delegate(JoyBehaviourMonitor.Instance smi, object data)
		{
			TagChangedEventData tagChangedEventData = (TagChangedEventData)data;
			if (!tagChangedEventData.added)
			{
				return;
			}
			if (tagChangedEventData.tag == GameTags.PleasantConversation && UnityEngine.Random.Range(0f, 100f) <= 1f)
			{
				smi.GoToOverjoyed();
			}
			smi.GetComponent<KPrefabID>().RemoveTag(GameTags.PleasantConversation);
		}).EventHandler(GameHashes.SleepFinished, delegate(JoyBehaviourMonitor.Instance smi)
		{
			if (smi.ShouldBeOverjoyed())
			{
				smi.GoToOverjoyed();
			}
		});
		this.overjoyed.Transition(this.neutral, (JoyBehaviourMonitor.Instance smi) => GameClock.Instance.GetTime() >= smi.transitionTime, UpdateRate.SIM_200ms).ToggleExpression((JoyBehaviourMonitor.Instance smi) => smi.happyExpression).ToggleAnims((JoyBehaviourMonitor.Instance smi) => smi.happyLocoAnim)
			.ToggleAnims((JoyBehaviourMonitor.Instance smi) => smi.happyLocoWalkAnim)
			.ToggleTag(GameTags.Overjoyed)
			.Exit(delegate(JoyBehaviourMonitor.Instance smi)
			{
				smi.GetComponent<KPrefabID>().RemoveTag(GameTags.PleasantConversation);
			})
			.OnSignal(this.exitEarly, this.neutral);
	}

	// Token: 0x0400279B RID: 10139
	public StateMachine<JoyBehaviourMonitor, JoyBehaviourMonitor.Instance, IStateMachineTarget, object>.Signal exitEarly;

	// Token: 0x0400279C RID: 10140
	public GameStateMachine<JoyBehaviourMonitor, JoyBehaviourMonitor.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x0400279D RID: 10141
	public GameStateMachine<JoyBehaviourMonitor, JoyBehaviourMonitor.Instance, IStateMachineTarget, object>.State overjoyed;

	// Token: 0x020015BE RID: 5566
	public new class Instance : GameStateMachine<JoyBehaviourMonitor, JoyBehaviourMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600852A RID: 34090 RVA: 0x002EBFDC File Offset: 0x002EA1DC
		public Instance(IStateMachineTarget master, string happy_loco_anim, string happy_loco_walk_anim, Expression happy_expression)
			: base(master)
		{
			this.happyLocoAnim = happy_loco_anim;
			this.happyLocoWalkAnim = happy_loco_walk_anim;
			this.happyExpression = happy_expression;
			Attributes attributes = base.gameObject.GetAttributes();
			this.expectationAttribute = attributes.Add(Db.Get().Attributes.QualityOfLifeExpectation);
			this.qolAttribute = Db.Get().Attributes.QualityOfLife.Lookup(base.gameObject);
		}

		// Token: 0x0600852B RID: 34091 RVA: 0x002EC064 File Offset: 0x002EA264
		public bool ShouldBeOverjoyed()
		{
			float totalValue = this.qolAttribute.GetTotalValue();
			float totalValue2 = this.expectationAttribute.GetTotalValue();
			float num = totalValue - totalValue2;
			if (num >= TRAITS.JOY_REACTIONS.MIN_MORALE_EXCESS)
			{
				float num2 = MathUtil.ReRange(num, TRAITS.JOY_REACTIONS.MIN_MORALE_EXCESS, TRAITS.JOY_REACTIONS.MAX_MORALE_EXCESS, TRAITS.JOY_REACTIONS.MIN_REACTION_CHANCE, TRAITS.JOY_REACTIONS.MAX_REACTION_CHANCE);
				return UnityEngine.Random.Range(0f, 100f) <= num2;
			}
			return false;
		}

		// Token: 0x0600852C RID: 34092 RVA: 0x002EC0C5 File Offset: 0x002EA2C5
		public void GoToOverjoyed()
		{
			base.smi.transitionTime = GameClock.Instance.GetTime() + TRAITS.JOY_REACTIONS.JOY_REACTION_DURATION;
			base.smi.GoTo(base.smi.sm.overjoyed);
		}

		// Token: 0x040067C2 RID: 26562
		public string happyLocoAnim = "";

		// Token: 0x040067C3 RID: 26563
		public string happyLocoWalkAnim = "";

		// Token: 0x040067C4 RID: 26564
		public Expression happyExpression;

		// Token: 0x040067C5 RID: 26565
		[Serialize]
		public float transitionTime;

		// Token: 0x040067C6 RID: 26566
		private AttributeInstance expectationAttribute;

		// Token: 0x040067C7 RID: 26567
		private AttributeInstance qolAttribute;
	}
}
