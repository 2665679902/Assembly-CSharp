using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020009A5 RID: 2469
public class Telephone : StateMachineComponent<Telephone.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06004948 RID: 18760 RVA: 0x0019A324 File Offset: 0x00198524
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		Components.Telephones.Add(this);
		GameScheduler.Instance.Schedule("Scheduling Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, true);
		}, null, null);
	}

	// Token: 0x06004949 RID: 18761 RVA: 0x0019A383 File Offset: 0x00198583
	protected override void OnCleanUp()
	{
		Components.Telephones.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x0600494A RID: 18762 RVA: 0x0019A398 File Offset: 0x00198598
	public void AddModifierDescriptions(List<Descriptor> descs, string effect_id)
	{
		Effect effect = Db.Get().effects.Get(effect_id);
		string text;
		string text2;
		if (effect.Id == this.babbleEffect)
		{
			text = BUILDINGS.PREFABS.TELEPHONE.EFFECT_BABBLE;
			text2 = BUILDINGS.PREFABS.TELEPHONE.EFFECT_BABBLE_TOOLTIP;
		}
		else if (effect.Id == this.chatEffect)
		{
			text = BUILDINGS.PREFABS.TELEPHONE.EFFECT_CHAT;
			text2 = BUILDINGS.PREFABS.TELEPHONE.EFFECT_CHAT_TOOLTIP;
		}
		else
		{
			text = BUILDINGS.PREFABS.TELEPHONE.EFFECT_LONG_DISTANCE;
			text2 = BUILDINGS.PREFABS.TELEPHONE.EFFECT_LONG_DISTANCE_TOOLTIP;
		}
		foreach (AttributeModifier attributeModifier in effect.SelfModifiers)
		{
			Descriptor descriptor = new Descriptor(text.Replace("{attrib}", Strings.Get("STRINGS.DUPLICANTS.ATTRIBUTES." + attributeModifier.AttributeId.ToUpper() + ".NAME")).Replace("{amount}", attributeModifier.GetFormattedString()), text2.Replace("{attrib}", Strings.Get("STRINGS.DUPLICANTS.ATTRIBUTES." + attributeModifier.AttributeId.ToUpper() + ".NAME")).Replace("{amount}", attributeModifier.GetFormattedString()), Descriptor.DescriptorType.Effect, false);
			descriptor.IncreaseIndent();
			descs.Add(descriptor);
		}
	}

	// Token: 0x0600494B RID: 18763 RVA: 0x0019A504 File Offset: 0x00198704
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		this.AddModifierDescriptions(list, this.babbleEffect);
		this.AddModifierDescriptions(list, this.chatEffect);
		this.AddModifierDescriptions(list, this.longDistanceEffect);
		return list;
	}

	// Token: 0x0600494C RID: 18764 RVA: 0x0019A56A File Offset: 0x0019876A
	public void HangUp()
	{
		this.isInUse = false;
		this.wasAnswered = false;
		this.RemoveTag(GameTags.LongDistanceCall);
	}

	// Token: 0x0400301B RID: 12315
	public string babbleEffect;

	// Token: 0x0400301C RID: 12316
	public string chatEffect;

	// Token: 0x0400301D RID: 12317
	public string longDistanceEffect;

	// Token: 0x0400301E RID: 12318
	public string trackingEffect;

	// Token: 0x0400301F RID: 12319
	public bool isInUse;

	// Token: 0x04003020 RID: 12320
	public bool wasAnswered;

	// Token: 0x020017A8 RID: 6056
	public class States : GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone>
	{
		// Token: 0x06008B7B RID: 35707 RVA: 0x002FF69C File Offset: 0x002FD89C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			Telephone.States.CreateStatusItems();
			default_state = this.unoperational;
			this.unoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.ready, false);
			this.ready.TagTransition(GameTags.Operational, this.unoperational, true).DefaultState(this.ready.idle).ToggleRecurringChore(new Func<Telephone.StatesInstance, Chore>(this.CreateChore), null)
				.Enter(delegate(Telephone.StatesInstance smi)
				{
					using (List<Telephone>.Enumerator enumerator = Components.Telephones.Items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.isInUse)
							{
								smi.GoTo(this.ready.speaker);
							}
						}
					}
				});
			this.ready.idle.WorkableStartTransition((Telephone.StatesInstance smi) => smi.master.GetComponent<TelephoneCallerWorkable>(), this.ready.calling.dial).TagTransition(GameTags.TelephoneRinging, this.ready.ringing, false).PlayAnim("off");
			this.ready.calling.ScheduleGoTo(15f, this.ready.talking.babbling);
			this.ready.calling.dial.PlayAnim("on_pre").OnAnimQueueComplete(this.ready.calling.animHack);
			this.ready.calling.animHack.ScheduleActionNextFrame("animHack_delay", delegate(Telephone.StatesInstance smi)
			{
				smi.GoTo(this.ready.calling.pre);
			});
			this.ready.calling.pre.PlayAnim("on").Enter(delegate(Telephone.StatesInstance smi)
			{
				this.RingAllTelephones(smi);
			}).OnAnimQueueComplete(this.ready.calling.wait);
			this.ready.calling.wait.PlayAnim("on", KAnim.PlayMode.Loop).Transition(this.ready.talking.chatting, (Telephone.StatesInstance smi) => smi.CallAnswered(), UpdateRate.SIM_4000ms);
			this.ready.ringing.PlayAnim("on_receiving", KAnim.PlayMode.Loop).Transition(this.ready.answer, (Telephone.StatesInstance smi) => smi.GetComponent<Telephone>().isInUse, UpdateRate.SIM_33ms).TagTransition(GameTags.TelephoneRinging, this.ready.speaker, true)
				.ScheduleGoTo(15f, this.ready.speaker)
				.Exit(delegate(Telephone.StatesInstance smi)
				{
					smi.GetComponent<Telephone>().RemoveTag(GameTags.TelephoneRinging);
				});
			this.ready.answer.PlayAnim("on_pre_loop_receiving").OnAnimQueueComplete(this.ready.talking.chatting);
			this.ready.talking.ScheduleGoTo(25f, this.ready.hangup).Enter(delegate(Telephone.StatesInstance smi)
			{
				this.UpdatePartyLine(smi);
			});
			this.ready.talking.babbling.PlayAnim("on_loop", KAnim.PlayMode.Loop).Transition(this.ready.talking.chatting, (Telephone.StatesInstance smi) => smi.CallAnswered(), UpdateRate.SIM_33ms).ToggleStatusItem(Telephone.States.babbling, null);
			this.ready.talking.chatting.PlayAnim("on_loop_pre").QueueAnim("on_loop", true, null).Transition(this.ready.talking.babbling, (Telephone.StatesInstance smi) => !smi.CallAnswered(), UpdateRate.SIM_33ms)
				.ToggleStatusItem(Telephone.States.partyLine, null);
			this.ready.speaker.PlayAnim("on_loop_nobody", KAnim.PlayMode.Loop).Transition(this.ready, (Telephone.StatesInstance smi) => !smi.CallAnswered(), UpdateRate.SIM_4000ms).Transition(this.ready.answer, (Telephone.StatesInstance smi) => smi.GetComponent<Telephone>().isInUse, UpdateRate.SIM_33ms);
			this.ready.hangup.OnAnimQueueComplete(this.ready);
		}

		// Token: 0x06008B7C RID: 35708 RVA: 0x002FFAD4 File Offset: 0x002FDCD4
		private Chore CreateChore(Telephone.StatesInstance smi)
		{
			Workable component = smi.master.GetComponent<TelephoneCallerWorkable>();
			WorkChore<TelephoneCallerWorkable> workChore = new WorkChore<TelephoneCallerWorkable>(Db.Get().ChoreTypes.Relax, component, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
			workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, component);
			return workChore;
		}

		// Token: 0x06008B7D RID: 35709 RVA: 0x002FFB34 File Offset: 0x002FDD34
		public void UpdatePartyLine(Telephone.StatesInstance smi)
		{
			int myWorldId = smi.GetMyWorldId();
			bool flag = false;
			foreach (Telephone telephone in Components.Telephones.Items)
			{
				telephone.RemoveTag(GameTags.TelephoneRinging);
				if (telephone.isInUse && myWorldId != telephone.GetMyWorldId())
				{
					flag = true;
					telephone.AddTag(GameTags.LongDistanceCall);
				}
			}
			Telephone component = smi.GetComponent<Telephone>();
			component.RemoveTag(GameTags.TelephoneRinging);
			if (flag)
			{
				component.AddTag(GameTags.LongDistanceCall);
			}
		}

		// Token: 0x06008B7E RID: 35710 RVA: 0x002FFBDC File Offset: 0x002FDDDC
		public void RingAllTelephones(Telephone.StatesInstance smi)
		{
			Telephone component = smi.master.GetComponent<Telephone>();
			foreach (Telephone telephone in Components.Telephones.Items)
			{
				if (component != telephone && telephone.GetComponent<Operational>().IsOperational)
				{
					TelephoneCallerWorkable component2 = telephone.GetComponent<TelephoneCallerWorkable>();
					if (component2 != null && component2.worker == null)
					{
						telephone.AddTag(GameTags.TelephoneRinging);
					}
				}
			}
		}

		// Token: 0x06008B7F RID: 35711 RVA: 0x002FFC78 File Offset: 0x002FDE78
		private static void CreateStatusItems()
		{
			if (Telephone.States.partyLine == null)
			{
				Telephone.States.partyLine = new StatusItem("PartyLine", BUILDING.STATUSITEMS.TELEPHONE.CONVERSATION.TALKING_TO, "", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022, true, null);
				Telephone.States.partyLine.resolveStringCallback = delegate(string str, object obj)
				{
					Telephone component = ((Telephone.StatesInstance)obj).GetComponent<Telephone>();
					int num = 0;
					foreach (Telephone telephone in Components.Telephones.Items)
					{
						if (telephone.isInUse && telephone != component)
						{
							num++;
							if (num == 1)
							{
								str = str.Replace("{Asteroid}", telephone.GetMyWorld().GetProperName());
								str = str.Replace("{Duplicant}", telephone.GetComponent<TelephoneCallerWorkable>().worker.GetProperName());
							}
						}
					}
					if (num > 1)
					{
						str = string.Format(BUILDING.STATUSITEMS.TELEPHONE.CONVERSATION.TALKING_TO_NUM, num);
					}
					return str;
				};
				Telephone.States.partyLine.resolveTooltipCallback = delegate(string str, object obj)
				{
					Telephone component2 = ((Telephone.StatesInstance)obj).GetComponent<Telephone>();
					foreach (Telephone telephone2 in Components.Telephones.Items)
					{
						if (telephone2.isInUse && telephone2 != component2)
						{
							string text = BUILDING.STATUSITEMS.TELEPHONE.CONVERSATION.TALKING_TO;
							text = text.Replace("{Duplicant}", telephone2.GetComponent<TelephoneCallerWorkable>().worker.GetProperName());
							text = text.Replace("{Asteroid}", telephone2.GetMyWorld().GetProperName());
							str = str + text + "\n";
						}
					}
					return str;
				};
			}
			if (Telephone.States.babbling == null)
			{
				Telephone.States.babbling = new StatusItem("Babbling", BUILDING.STATUSITEMS.TELEPHONE.BABBLE.NAME, BUILDING.STATUSITEMS.TELEPHONE.BABBLE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022, true, null);
				Telephone.States.babbling.resolveTooltipCallback = delegate(string str, object obj)
				{
					Telephone.StatesInstance statesInstance = (Telephone.StatesInstance)obj;
					str = str.Replace("{Duplicant}", statesInstance.GetComponent<TelephoneCallerWorkable>().worker.GetProperName());
					return str;
				};
			}
		}

		// Token: 0x04006DAE RID: 28078
		private GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State unoperational;

		// Token: 0x04006DAF RID: 28079
		private Telephone.States.ReadyStates ready;

		// Token: 0x04006DB0 RID: 28080
		private static StatusItem partyLine;

		// Token: 0x04006DB1 RID: 28081
		private static StatusItem babbling;

		// Token: 0x020020D1 RID: 8401
		public class ReadyStates : GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State
		{
			// Token: 0x04009220 RID: 37408
			public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State idle;

			// Token: 0x04009221 RID: 37409
			public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State ringing;

			// Token: 0x04009222 RID: 37410
			public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State answer;

			// Token: 0x04009223 RID: 37411
			public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State speaker;

			// Token: 0x04009224 RID: 37412
			public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State hangup;

			// Token: 0x04009225 RID: 37413
			public Telephone.States.ReadyStates.CallingStates calling;

			// Token: 0x04009226 RID: 37414
			public Telephone.States.ReadyStates.TalkingStates talking;

			// Token: 0x02002DB5 RID: 11701
			public class CallingStates : GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State
			{
				// Token: 0x0400BA58 RID: 47704
				public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State dial;

				// Token: 0x0400BA59 RID: 47705
				public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State animHack;

				// Token: 0x0400BA5A RID: 47706
				public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State pre;

				// Token: 0x0400BA5B RID: 47707
				public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State wait;
			}

			// Token: 0x02002DB6 RID: 11702
			public class TalkingStates : GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State
			{
				// Token: 0x0400BA5C RID: 47708
				public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State babbling;

				// Token: 0x0400BA5D RID: 47709
				public GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.State chatting;
			}
		}
	}

	// Token: 0x020017A9 RID: 6057
	public class StatesInstance : GameStateMachine<Telephone.States, Telephone.StatesInstance, Telephone, object>.GameInstance
	{
		// Token: 0x06008B85 RID: 35717 RVA: 0x002FFE16 File Offset: 0x002FE016
		public StatesInstance(Telephone smi)
			: base(smi)
		{
		}

		// Token: 0x06008B86 RID: 35718 RVA: 0x002FFE20 File Offset: 0x002FE020
		public bool CallAnswered()
		{
			foreach (Telephone telephone in Components.Telephones.Items)
			{
				if (telephone.isInUse && telephone != base.smi.GetComponent<Telephone>())
				{
					telephone.wasAnswered = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008B87 RID: 35719 RVA: 0x002FFE9C File Offset: 0x002FE09C
		public bool CallEnded()
		{
			using (List<Telephone>.Enumerator enumerator = Components.Telephones.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.isInUse)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
