using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B81 RID: 2945
[AddComponentMenu("KMonoBehaviour/scripts/ScheduleScreenEntry")]
public class ScheduleScreenEntry : KMonoBehaviour
{
	// Token: 0x17000676 RID: 1654
	// (get) Token: 0x06005C94 RID: 23700 RVA: 0x0021D8C5 File Offset: 0x0021BAC5
	// (set) Token: 0x06005C95 RID: 23701 RVA: 0x0021D8CD File Offset: 0x0021BACD
	public Schedule schedule { get; private set; }

	// Token: 0x06005C96 RID: 23702 RVA: 0x0021D8D8 File Offset: 0x0021BAD8
	public void Setup(Schedule schedule, Dictionary<string, ColorStyleSetting> paintStyles, Action<ScheduleScreenEntry, float> onPaintDragged)
	{
		this.schedule = schedule;
		base.gameObject.name = "Schedule_" + schedule.name;
		this.title.SetTitle(schedule.name);
		this.title.OnNameChanged += this.OnNameChanged;
		this.blockButtonContainer.Setup(delegate(float f)
		{
			onPaintDragged(this, f);
		});
		int num = 0;
		this.blockButtons = new List<ScheduleBlockButton>();
		int count = schedule.GetBlocks().Count;
		foreach (ScheduleBlock scheduleBlock in schedule.GetBlocks())
		{
			ScheduleBlockButton scheduleBlockButton = Util.KInstantiateUI<ScheduleBlockButton>(this.blockButtonPrefab.gameObject, this.blockButtonContainer.gameObject, true);
			scheduleBlockButton.Setup(num++, paintStyles, count);
			scheduleBlockButton.SetBlockTypes(scheduleBlock.allowed_types);
			this.blockButtons.Add(scheduleBlockButton);
		}
		this.minionWidgets = new List<ScheduleMinionWidget>();
		this.blankMinionWidget = Util.KInstantiateUI<ScheduleMinionWidget>(this.minionWidgetPrefab.gameObject, this.minionWidgetContainer, false);
		this.blankMinionWidget.SetupBlank(schedule);
		this.RebuildMinionWidgets();
		this.RefreshNotes();
		this.RefreshAlarmButton();
		this.optionsButton.onClick += this.OnOptionsClicked;
		HierarchyReferences component = this.optionsPanel.GetComponent<HierarchyReferences>();
		MultiToggle reference = component.GetReference<MultiToggle>("AlarmButton");
		reference.onClick = (System.Action)Delegate.Combine(reference.onClick, new System.Action(this.OnAlarmClicked));
		component.GetReference<KButton>("ResetButton").onClick += this.OnResetClicked;
		component.GetReference<KButton>("DeleteButton").onClick += this.OnDeleteClicked;
		schedule.onChanged = (Action<Schedule>)Delegate.Combine(schedule.onChanged, new Action<Schedule>(this.OnScheduleChanged));
	}

	// Token: 0x06005C97 RID: 23703 RVA: 0x0021DAE8 File Offset: 0x0021BCE8
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.schedule != null)
		{
			Schedule schedule = this.schedule;
			schedule.onChanged = (Action<Schedule>)Delegate.Remove(schedule.onChanged, new Action<Schedule>(this.OnScheduleChanged));
		}
	}

	// Token: 0x06005C98 RID: 23704 RVA: 0x0021DB1F File Offset: 0x0021BD1F
	public GameObject GetNameInputField()
	{
		return this.title.inputField.gameObject;
	}

	// Token: 0x06005C99 RID: 23705 RVA: 0x0021DB34 File Offset: 0x0021BD34
	private void RebuildMinionWidgets()
	{
		if (!this.MinionWidgetsNeedRebuild())
		{
			return;
		}
		foreach (ScheduleMinionWidget scheduleMinionWidget in this.minionWidgets)
		{
			Util.KDestroyGameObject(scheduleMinionWidget);
		}
		this.minionWidgets.Clear();
		foreach (Ref<Schedulable> @ref in this.schedule.GetAssigned())
		{
			ScheduleMinionWidget scheduleMinionWidget2 = Util.KInstantiateUI<ScheduleMinionWidget>(this.minionWidgetPrefab.gameObject, this.minionWidgetContainer, true);
			scheduleMinionWidget2.Setup(@ref.Get());
			this.minionWidgets.Add(scheduleMinionWidget2);
		}
		if (Components.LiveMinionIdentities.Count > this.schedule.GetAssigned().Count)
		{
			this.blankMinionWidget.transform.SetAsLastSibling();
			this.blankMinionWidget.gameObject.SetActive(true);
			return;
		}
		this.blankMinionWidget.gameObject.SetActive(false);
	}

	// Token: 0x06005C9A RID: 23706 RVA: 0x0021DC58 File Offset: 0x0021BE58
	private bool MinionWidgetsNeedRebuild()
	{
		List<Ref<Schedulable>> assigned = this.schedule.GetAssigned();
		if (assigned.Count != this.minionWidgets.Count)
		{
			return true;
		}
		if (assigned.Count != Components.LiveMinionIdentities.Count != this.blankMinionWidget.gameObject.activeSelf)
		{
			return true;
		}
		for (int i = 0; i < assigned.Count; i++)
		{
			if (assigned[i].Get() != this.minionWidgets[i].schedulable)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06005C9B RID: 23707 RVA: 0x0021DCE8 File Offset: 0x0021BEE8
	public void RefreshWidgetWorldData()
	{
		foreach (ScheduleMinionWidget scheduleMinionWidget in this.minionWidgets)
		{
			if (!scheduleMinionWidget.IsNullOrDestroyed())
			{
				scheduleMinionWidget.RefreshWidgetWorldData();
			}
		}
	}

	// Token: 0x06005C9C RID: 23708 RVA: 0x0021DD44 File Offset: 0x0021BF44
	private void OnNameChanged(string newName)
	{
		this.schedule.name = newName;
		base.gameObject.name = "Schedule_" + this.schedule.name;
	}

	// Token: 0x06005C9D RID: 23709 RVA: 0x0021DD72 File Offset: 0x0021BF72
	private void OnOptionsClicked()
	{
		this.optionsPanel.gameObject.SetActive(!this.optionsPanel.gameObject.activeSelf);
		this.optionsPanel.GetComponent<Selectable>().Select();
	}

	// Token: 0x06005C9E RID: 23710 RVA: 0x0021DDA7 File Offset: 0x0021BFA7
	private void OnAlarmClicked()
	{
		this.schedule.alarmActivated = !this.schedule.alarmActivated;
		this.RefreshAlarmButton();
	}

	// Token: 0x06005C9F RID: 23711 RVA: 0x0021DDC8 File Offset: 0x0021BFC8
	private void RefreshAlarmButton()
	{
		MultiToggle reference = this.optionsPanel.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("AlarmButton");
		reference.ChangeState(this.schedule.alarmActivated ? 1 : 0);
		ToolTip component = reference.GetComponent<ToolTip>();
		component.SetSimpleTooltip(this.schedule.alarmActivated ? UI.SCHEDULESCREEN.ALARM_BUTTON_ON_TOOLTIP : UI.SCHEDULESCREEN.ALARM_BUTTON_OFF_TOOLTIP);
		ToolTipScreen.Instance.MarkTooltipDirty(component);
		this.alarmField.text = (this.schedule.alarmActivated ? UI.SCHEDULESCREEN.ALARM_TITLE_ENABLED : UI.SCHEDULESCREEN.ALARM_TITLE_DISABLED);
	}

	// Token: 0x06005CA0 RID: 23712 RVA: 0x0021DE5F File Offset: 0x0021C05F
	private void OnResetClicked()
	{
		this.schedule.SetBlocksToGroupDefaults(Db.Get().ScheduleGroups.allGroups);
	}

	// Token: 0x06005CA1 RID: 23713 RVA: 0x0021DE7B File Offset: 0x0021C07B
	private void OnDeleteClicked()
	{
		ScheduleManager.Instance.DeleteSchedule(this.schedule);
	}

	// Token: 0x06005CA2 RID: 23714 RVA: 0x0021DE90 File Offset: 0x0021C090
	private void OnScheduleChanged(Schedule changedSchedule)
	{
		foreach (ScheduleBlockButton scheduleBlockButton in this.blockButtons)
		{
			scheduleBlockButton.SetBlockTypes(changedSchedule.GetBlock(scheduleBlockButton.idx).allowed_types);
		}
		this.RefreshNotes();
		this.RebuildMinionWidgets();
	}

	// Token: 0x06005CA3 RID: 23715 RVA: 0x0021DF00 File Offset: 0x0021C100
	private void RefreshNotes()
	{
		this.blockTypeCounts.Clear();
		foreach (ScheduleBlockType scheduleBlockType in Db.Get().ScheduleBlockTypes.resources)
		{
			this.blockTypeCounts[scheduleBlockType.Id] = 0;
		}
		foreach (ScheduleBlock scheduleBlock in this.schedule.GetBlocks())
		{
			foreach (ScheduleBlockType scheduleBlockType2 in scheduleBlock.allowed_types)
			{
				Dictionary<string, int> dictionary = this.blockTypeCounts;
				string id = scheduleBlockType2.Id;
				int num = dictionary[id];
				dictionary[id] = num + 1;
			}
		}
		if (this.noteEntryRight == null)
		{
			return;
		}
		ToolTip component = this.noteEntryRight.GetComponent<ToolTip>();
		component.ClearMultiStringTooltip();
		int num2 = 0;
		foreach (KeyValuePair<string, int> keyValuePair in this.blockTypeCounts)
		{
			if (keyValuePair.Value == 0)
			{
				num2++;
				component.AddMultiStringTooltip(string.Format(UI.SCHEDULEGROUPS.NOTIME, Db.Get().ScheduleBlockTypes.Get(keyValuePair.Key).Name), null);
			}
		}
		if (num2 > 0)
		{
			this.noteEntryRight.text = string.Format(UI.SCHEDULEGROUPS.MISSINGBLOCKS, num2);
		}
		else
		{
			this.noteEntryRight.text = "";
		}
		string breakBonus = QualityOfLifeNeed.GetBreakBonus(this.blockTypeCounts[Db.Get().ScheduleBlockTypes.Recreation.Id]);
		if (breakBonus != null)
		{
			Effect effect = Db.Get().effects.Get(breakBonus);
			if (effect != null)
			{
				foreach (AttributeModifier attributeModifier in effect.SelfModifiers)
				{
					if (attributeModifier.AttributeId == Db.Get().Attributes.QualityOfLife.Id)
					{
						this.noteEntryLeft.text = string.Format(UI.SCHEDULESCREEN.DOWNTIME_MORALE, attributeModifier.GetFormattedString());
						this.noteEntryLeft.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.SCHEDULESCREEN.SCHEDULE_DOWNTIME_MORALE, attributeModifier.GetFormattedString()));
					}
				}
			}
		}
	}

	// Token: 0x04003F43 RID: 16195
	[SerializeField]
	private ScheduleBlockButton blockButtonPrefab;

	// Token: 0x04003F44 RID: 16196
	[SerializeField]
	private ScheduleBlockPainter blockButtonContainer;

	// Token: 0x04003F45 RID: 16197
	[SerializeField]
	private ScheduleMinionWidget minionWidgetPrefab;

	// Token: 0x04003F46 RID: 16198
	[SerializeField]
	private GameObject minionWidgetContainer;

	// Token: 0x04003F47 RID: 16199
	private ScheduleMinionWidget blankMinionWidget;

	// Token: 0x04003F48 RID: 16200
	[SerializeField]
	private EditableTitleBar title;

	// Token: 0x04003F49 RID: 16201
	[SerializeField]
	private LocText alarmField;

	// Token: 0x04003F4A RID: 16202
	[SerializeField]
	private KButton optionsButton;

	// Token: 0x04003F4B RID: 16203
	[SerializeField]
	private DialogPanel optionsPanel;

	// Token: 0x04003F4C RID: 16204
	[SerializeField]
	private LocText noteEntryLeft;

	// Token: 0x04003F4D RID: 16205
	[SerializeField]
	private LocText noteEntryRight;

	// Token: 0x04003F4E RID: 16206
	private List<ScheduleBlockButton> blockButtons;

	// Token: 0x04003F4F RID: 16207
	private List<ScheduleMinionWidget> minionWidgets;

	// Token: 0x04003F51 RID: 16209
	private Dictionary<string, int> blockTypeCounts = new Dictionary<string, int>();
}
