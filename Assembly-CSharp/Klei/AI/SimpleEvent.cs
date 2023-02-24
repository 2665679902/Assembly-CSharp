using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D90 RID: 3472
	public class SimpleEvent : GameplayEvent<SimpleEvent.StatesInstance>
	{
		// Token: 0x060069C7 RID: 27079 RVA: 0x002926FF File Offset: 0x002908FF
		public SimpleEvent(string id, string title, string description, string animFileName, string buttonText = null, string buttonTooltip = null)
			: base(id, 0, 0)
		{
			this.title = title;
			this.description = description;
			this.buttonText = buttonText;
			this.buttonTooltip = buttonTooltip;
			this.animFileName = animFileName;
		}

		// Token: 0x060069C8 RID: 27080 RVA: 0x00292735 File Offset: 0x00290935
		public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
		{
			return new SimpleEvent.StatesInstance(manager, eventInstance, this);
		}

		// Token: 0x04004F98 RID: 20376
		private string buttonText;

		// Token: 0x04004F99 RID: 20377
		private string buttonTooltip;

		// Token: 0x02001E6A RID: 7786
		public class States : GameplayEventStateMachine<SimpleEvent.States, SimpleEvent.StatesInstance, GameplayEventManager, SimpleEvent>
		{
			// Token: 0x06009BAE RID: 39854 RVA: 0x00338FD2 File Offset: 0x003371D2
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				default_state = this.root;
				this.ending.ReturnSuccess();
			}

			// Token: 0x06009BAF RID: 39855 RVA: 0x00338FE8 File Offset: 0x003371E8
			public override EventInfoData GenerateEventPopupData(SimpleEvent.StatesInstance smi)
			{
				EventInfoData eventInfoData = new EventInfoData(smi.gameplayEvent.title, smi.gameplayEvent.description, smi.gameplayEvent.animFileName);
				eventInfoData.minions = smi.minions;
				eventInfoData.artifact = smi.artifact;
				EventInfoData.Option option = eventInfoData.AddOption(smi.gameplayEvent.buttonText, null);
				option.callback = delegate
				{
					if (smi.callback != null)
					{
						smi.callback();
					}
					smi.StopSM("SimpleEvent Finished");
				};
				option.tooltip = smi.gameplayEvent.buttonTooltip;
				if (smi.textParameters != null)
				{
					foreach (global::Tuple<string, string> tuple in smi.textParameters)
					{
						eventInfoData.SetTextParameter(tuple.first, tuple.second);
					}
				}
				return eventInfoData;
			}

			// Token: 0x040088A6 RID: 34982
			public GameStateMachine<SimpleEvent.States, SimpleEvent.StatesInstance, GameplayEventManager, object>.State ending;
		}

		// Token: 0x02001E6B RID: 7787
		public class StatesInstance : GameplayEventStateMachine<SimpleEvent.States, SimpleEvent.StatesInstance, GameplayEventManager, SimpleEvent>.GameplayEventStateMachineInstance
		{
			// Token: 0x06009BB1 RID: 39857 RVA: 0x00339104 File Offset: 0x00337304
			public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, SimpleEvent simpleEvent)
				: base(master, eventInstance, simpleEvent)
			{
			}

			// Token: 0x06009BB2 RID: 39858 RVA: 0x0033910F File Offset: 0x0033730F
			public void SetTextParameter(string key, string value)
			{
				if (this.textParameters == null)
				{
					this.textParameters = new List<global::Tuple<string, string>>();
				}
				this.textParameters.Add(new global::Tuple<string, string>(key, value));
			}

			// Token: 0x06009BB3 RID: 39859 RVA: 0x00339136 File Offset: 0x00337336
			public void ShowEventPopup()
			{
				EventInfoScreen.ShowPopup(base.smi.sm.GenerateEventPopupData(base.smi));
			}

			// Token: 0x040088A7 RID: 34983
			public GameObject[] minions;

			// Token: 0x040088A8 RID: 34984
			public GameObject artifact;

			// Token: 0x040088A9 RID: 34985
			public List<global::Tuple<string, string>> textParameters;

			// Token: 0x040088AA RID: 34986
			public System.Action callback;
		}
	}
}
