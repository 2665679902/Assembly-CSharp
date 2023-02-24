using System;
using Database;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D9C RID: 3484
	public abstract class StoryTraitStateMachine<TStateMachine, TInstance, TDef> : GameStateMachine<TStateMachine, TInstance, StateMachineController, TDef> where TStateMachine : StoryTraitStateMachine<TStateMachine, TInstance, TDef> where TInstance : StoryTraitStateMachine<TStateMachine, TInstance, TDef>.TraitInstance where TDef : StoryTraitStateMachine<TStateMachine, TInstance, TDef>.TraitDef
	{
		// Token: 0x02001E70 RID: 7792
		public class TraitDef : StateMachine.BaseDef
		{
			// Token: 0x040088B0 RID: 34992
			public string InitalLoreId;

			// Token: 0x040088B1 RID: 34993
			public string CompleteLoreId;

			// Token: 0x040088B2 RID: 34994
			public Story Story;

			// Token: 0x040088B3 RID: 34995
			public StoryCompleteData CompletionData;

			// Token: 0x040088B4 RID: 34996
			public StoryManager.PopupInfo EventIntroInfo = new StoryManager.PopupInfo
			{
				PopupType = EventInfoDataHelper.PopupType.NONE
			};

			// Token: 0x040088B5 RID: 34997
			public StoryManager.PopupInfo EventCompleteInfo = new StoryManager.PopupInfo
			{
				PopupType = EventInfoDataHelper.PopupType.NONE
			};
		}

		// Token: 0x02001E71 RID: 7793
		public class TraitInstance : GameStateMachine<TStateMachine, TInstance, StateMachineController, TDef>.GameInstance
		{
			// Token: 0x06009BBD RID: 39869 RVA: 0x00339240 File Offset: 0x00337440
			public TraitInstance(StateMachineController master)
				: base(master)
			{
				StoryManager.Instance.ForceCreateStory(base.def.Story, base.gameObject.GetMyWorldId());
				this.buildingActivatedHandle = master.Subscribe(-1909216579, new Action<object>(this.OnBuildingActivated));
			}

			// Token: 0x06009BBE RID: 39870 RVA: 0x003392A0 File Offset: 0x003374A0
			public TraitInstance(StateMachineController master, TDef def)
				: base(master, def)
			{
				StoryManager.Instance.ForceCreateStory(def.Story, base.gameObject.GetMyWorldId());
				this.buildingActivatedHandle = master.Subscribe(-1909216579, new Action<object>(this.OnBuildingActivated));
			}

			// Token: 0x06009BBF RID: 39871 RVA: 0x003392FC File Offset: 0x003374FC
			public override void StartSM()
			{
				this.selectable = base.GetComponent<KSelectable>();
				this.notifier = base.gameObject.AddOrGet<Notifier>();
				base.StartSM();
				base.Subscribe(-1503271301, new Action<object>(this.OnObjectSelect));
				if (this.buildingActivatedHandle == -1)
				{
					this.buildingActivatedHandle = base.master.Subscribe(-1909216579, new Action<object>(this.OnBuildingActivated));
				}
				this.TriggerStoryEvent(StoryInstance.State.DISCOVERED);
			}

			// Token: 0x06009BC0 RID: 39872 RVA: 0x00339377 File Offset: 0x00337577
			public override void StopSM(string reason)
			{
				base.StopSM(reason);
				base.Unsubscribe(-1503271301, new Action<object>(this.OnObjectSelect));
				base.Unsubscribe(-1909216579, new Action<object>(this.OnBuildingActivated));
				this.buildingActivatedHandle = -1;
			}

			// Token: 0x06009BC1 RID: 39873 RVA: 0x003393B8 File Offset: 0x003375B8
			public void TriggerStoryEvent(StoryInstance.State storyEvent)
			{
				switch (storyEvent)
				{
				case StoryInstance.State.RETROFITTED:
				case StoryInstance.State.NOT_STARTED:
					break;
				case StoryInstance.State.DISCOVERED:
					StoryManager.Instance.DiscoverStoryEvent(base.def.Story);
					return;
				case StoryInstance.State.IN_PROGRESS:
					StoryManager.Instance.BeginStoryEvent(base.def.Story);
					return;
				case StoryInstance.State.COMPLETE:
				{
					Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(Grid.PosToCell(base.master), base.def.CompletionData.KeepSakeSpawnOffset), Grid.SceneLayer.Ore);
					StoryManager.Instance.CompleteStoryEvent(base.def.Story, vector);
					break;
				}
				default:
					return;
				}
			}

			// Token: 0x06009BC2 RID: 39874 RVA: 0x00339460 File Offset: 0x00337660
			protected virtual void OnBuildingActivated(object activated)
			{
				if (!(bool)activated)
				{
					return;
				}
				this.TriggerStoryEvent(StoryInstance.State.IN_PROGRESS);
			}

			// Token: 0x06009BC3 RID: 39875 RVA: 0x00339474 File Offset: 0x00337674
			protected virtual void OnObjectSelect(object clicked)
			{
				if (!(bool)clicked)
				{
					return;
				}
				StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(base.def.Story.HashId);
				if (storyInstance != null && storyInstance.PendingType != EventInfoDataHelper.PopupType.NONE)
				{
					this.OnNotificationClicked(null);
					return;
				}
				if (!StoryManager.Instance.HasDisplayedPopup(base.def.Story, EventInfoDataHelper.PopupType.BEGIN))
				{
					this.DisplayPopup(base.def.EventIntroInfo);
				}
			}

			// Token: 0x06009BC4 RID: 39876 RVA: 0x003394F4 File Offset: 0x003376F4
			public virtual void CompleteEvent()
			{
				StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(base.def.Story.HashId);
				if (storyInstance == null || storyInstance.CurrentState == StoryInstance.State.COMPLETE)
				{
					return;
				}
				this.DisplayPopup(base.def.EventCompleteInfo);
			}

			// Token: 0x06009BC5 RID: 39877 RVA: 0x00339544 File Offset: 0x00337744
			public virtual void OnCompleteStorySequence()
			{
				this.TriggerStoryEvent(StoryInstance.State.COMPLETE);
			}

			// Token: 0x06009BC6 RID: 39878 RVA: 0x00339550 File Offset: 0x00337750
			protected void DisplayPopup(StoryManager.PopupInfo info)
			{
				if (info.PopupType == EventInfoDataHelper.PopupType.NONE)
				{
					return;
				}
				StoryInstance storyInstance = StoryManager.Instance.DisplayPopup(base.def.Story, info, new System.Action(this.OnPopupClosed), new Notification.ClickCallback(this.OnNotificationClicked));
				if (storyInstance != null && !info.DisplayImmediate)
				{
					this.selectable.AddStatusItem(Db.Get().MiscStatusItems.AttentionRequired, base.smi);
					this.notifier.Add(storyInstance.Notification, "");
				}
			}

			// Token: 0x06009BC7 RID: 39879 RVA: 0x003395E4 File Offset: 0x003377E4
			public void OnNotificationClicked(object data = null)
			{
				StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(base.def.Story.HashId);
				if (storyInstance == null)
				{
					return;
				}
				this.selectable.RemoveStatusItem(Db.Get().MiscStatusItems.AttentionRequired, false);
				this.notifier.Remove(storyInstance.Notification);
				if (storyInstance.PendingType == EventInfoDataHelper.PopupType.COMPLETE)
				{
					this.ShowEventCompleteUI();
					return;
				}
				this.ShowEventBeginUI();
			}

			// Token: 0x06009BC8 RID: 39880 RVA: 0x00339658 File Offset: 0x00337858
			public void OnPopupClosed()
			{
				StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(base.def.Story.HashId);
				if (storyInstance == null)
				{
					return;
				}
				if (storyInstance.HasDisplayedPopup(EventInfoDataHelper.PopupType.COMPLETE))
				{
					Game.Instance.unlocks.Unlock(base.def.CompleteLoreId, true);
					return;
				}
				Game.Instance.unlocks.Unlock(base.def.InitalLoreId, true);
			}

			// Token: 0x06009BC9 RID: 39881 RVA: 0x003396D3 File Offset: 0x003378D3
			protected virtual void ShowEventBeginUI()
			{
			}

			// Token: 0x06009BCA RID: 39882 RVA: 0x003396D8 File Offset: 0x003378D8
			protected virtual void ShowEventCompleteUI()
			{
				StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(base.def.Story.HashId);
				if (storyInstance == null)
				{
					return;
				}
				Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(Grid.PosToCell(base.master), base.def.CompletionData.CameraTargetOffset), Grid.SceneLayer.Ore);
				StoryManager.Instance.CompleteStoryEvent(base.def.Story, base.master, new FocusTargetSequence.Data
				{
					WorldId = base.master.GetMyWorldId(),
					OrthographicSize = 6f,
					TargetSize = 6f,
					Target = vector,
					PopupData = storyInstance.EventInfo,
					CompleteCB = new System.Action(this.OnCompleteStorySequence),
					CanCompleteCB = null
				});
			}

			// Token: 0x040088B6 RID: 34998
			protected int buildingActivatedHandle = -1;

			// Token: 0x040088B7 RID: 34999
			protected Notifier notifier;

			// Token: 0x040088B8 RID: 35000
			protected KSelectable selectable;
		}
	}
}
