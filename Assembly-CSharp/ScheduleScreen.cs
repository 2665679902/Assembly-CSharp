using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000B7E RID: 2942
public class ScheduleScreen : KScreen
{
	// Token: 0x06005C80 RID: 23680 RVA: 0x0021D3A1 File Offset: 0x0021B5A1
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x06005C81 RID: 23681 RVA: 0x0021D3A8 File Offset: 0x0021B5A8
	protected override void OnPrefabInit()
	{
		base.ConsumeMouseScroll = true;
		this.entries = new List<ScheduleScreenEntry>();
		this.paintStyles = new Dictionary<string, ColorStyleSetting>();
		this.paintStyles["Hygene"] = this.hygene_color;
		this.paintStyles["Worktime"] = this.work_color;
		this.paintStyles["Recreation"] = this.recreation_color;
		this.paintStyles["Sleep"] = this.sleep_color;
	}

	// Token: 0x06005C82 RID: 23682 RVA: 0x0021D42C File Offset: 0x0021B62C
	protected override void OnSpawn()
	{
		this.paintButtons = new List<SchedulePaintButton>();
		foreach (ScheduleGroup scheduleGroup in Db.Get().ScheduleGroups.allGroups)
		{
			this.AddPaintButton(scheduleGroup);
		}
		foreach (Schedule schedule in ScheduleManager.Instance.GetSchedules())
		{
			this.AddScheduleEntry(schedule);
		}
		this.addScheduleButton.onClick += this.OnAddScheduleClick;
		this.closeButton.onClick += delegate
		{
			ManagementMenu.Instance.CloseAll();
		};
		ScheduleManager.Instance.onSchedulesChanged += this.OnSchedulesChanged;
		Game.Instance.Subscribe(1983128072, new Action<object>(this.RefreshWidgetWorldData));
	}

	// Token: 0x06005C83 RID: 23683 RVA: 0x0021D54C File Offset: 0x0021B74C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		ScheduleManager.Instance.onSchedulesChanged -= this.OnSchedulesChanged;
	}

	// Token: 0x06005C84 RID: 23684 RVA: 0x0021D56A File Offset: 0x0021B76A
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			base.Activate();
		}
	}

	// Token: 0x06005C85 RID: 23685 RVA: 0x0021D57C File Offset: 0x0021B77C
	private void AddPaintButton(ScheduleGroup group)
	{
		SchedulePaintButton schedulePaintButton = Util.KInstantiateUI<SchedulePaintButton>(this.paintButtonPrefab.gameObject, this.paintButtonContainer, true);
		schedulePaintButton.SetGroup(group, this.paintStyles, new Action<SchedulePaintButton>(this.OnPaintButtonClick));
		schedulePaintButton.SetToggle(false);
		this.paintButtons.Add(schedulePaintButton);
	}

	// Token: 0x06005C86 RID: 23686 RVA: 0x0021D5CD File Offset: 0x0021B7CD
	private void OnAddScheduleClick()
	{
		ScheduleManager.Instance.AddDefaultSchedule(false);
	}

	// Token: 0x06005C87 RID: 23687 RVA: 0x0021D5DC File Offset: 0x0021B7DC
	private void OnPaintButtonClick(SchedulePaintButton clicked)
	{
		if (this.selectedPaint != clicked)
		{
			foreach (SchedulePaintButton schedulePaintButton in this.paintButtons)
			{
				schedulePaintButton.SetToggle(schedulePaintButton == clicked);
			}
			this.selectedPaint = clicked;
			return;
		}
		clicked.SetToggle(false);
		this.selectedPaint = null;
	}

	// Token: 0x06005C88 RID: 23688 RVA: 0x0021D658 File Offset: 0x0021B858
	private void OnPaintDragged(ScheduleScreenEntry entry, float ratio)
	{
		if (this.selectedPaint == null)
		{
			return;
		}
		int num = Mathf.FloorToInt(ratio * (float)entry.schedule.GetBlocks().Count);
		entry.schedule.SetGroup(num, this.selectedPaint.group);
	}

	// Token: 0x06005C89 RID: 23689 RVA: 0x0021D6A4 File Offset: 0x0021B8A4
	private void AddScheduleEntry(Schedule schedule)
	{
		ScheduleScreenEntry scheduleScreenEntry = Util.KInstantiateUI<ScheduleScreenEntry>(this.scheduleEntryPrefab.gameObject, this.scheduleEntryContainer, true);
		scheduleScreenEntry.Setup(schedule, this.paintStyles, new Action<ScheduleScreenEntry, float>(this.OnPaintDragged));
		this.entries.Add(scheduleScreenEntry);
	}

	// Token: 0x06005C8A RID: 23690 RVA: 0x0021D6F0 File Offset: 0x0021B8F0
	private void OnSchedulesChanged(List<Schedule> schedules)
	{
		foreach (ScheduleScreenEntry scheduleScreenEntry in this.entries)
		{
			Util.KDestroyGameObject(scheduleScreenEntry);
		}
		this.entries.Clear();
		foreach (Schedule schedule in schedules)
		{
			this.AddScheduleEntry(schedule);
		}
	}

	// Token: 0x06005C8B RID: 23691 RVA: 0x0021D788 File Offset: 0x0021B988
	private void RefreshWidgetWorldData(object data = null)
	{
		foreach (ScheduleScreenEntry scheduleScreenEntry in this.entries)
		{
			scheduleScreenEntry.RefreshWidgetWorldData();
		}
	}

	// Token: 0x06005C8C RID: 23692 RVA: 0x0021D7D8 File Offset: 0x0021B9D8
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.CheckBlockedInput())
		{
			if (!e.Consumed)
			{
				e.Consumed = true;
				return;
			}
		}
		else
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06005C8D RID: 23693 RVA: 0x0021D7FC File Offset: 0x0021B9FC
	private bool CheckBlockedInput()
	{
		bool flag = false;
		if (UnityEngine.EventSystems.EventSystem.current != null)
		{
			GameObject currentSelectedGameObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
			if (currentSelectedGameObject != null)
			{
				foreach (ScheduleScreenEntry scheduleScreenEntry in this.entries)
				{
					if (currentSelectedGameObject == scheduleScreenEntry.GetNameInputField())
					{
						flag = true;
						break;
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x04003F32 RID: 16178
	[SerializeField]
	private SchedulePaintButton paintButtonPrefab;

	// Token: 0x04003F33 RID: 16179
	[SerializeField]
	private GameObject paintButtonContainer;

	// Token: 0x04003F34 RID: 16180
	[SerializeField]
	private ScheduleScreenEntry scheduleEntryPrefab;

	// Token: 0x04003F35 RID: 16181
	[SerializeField]
	private GameObject scheduleEntryContainer;

	// Token: 0x04003F36 RID: 16182
	[SerializeField]
	private KButton addScheduleButton;

	// Token: 0x04003F37 RID: 16183
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003F38 RID: 16184
	[SerializeField]
	private ColorStyleSetting hygene_color;

	// Token: 0x04003F39 RID: 16185
	[SerializeField]
	private ColorStyleSetting work_color;

	// Token: 0x04003F3A RID: 16186
	[SerializeField]
	private ColorStyleSetting recreation_color;

	// Token: 0x04003F3B RID: 16187
	[SerializeField]
	private ColorStyleSetting sleep_color;

	// Token: 0x04003F3C RID: 16188
	private Dictionary<string, ColorStyleSetting> paintStyles;

	// Token: 0x04003F3D RID: 16189
	private List<ScheduleScreenEntry> entries;

	// Token: 0x04003F3E RID: 16190
	private List<SchedulePaintButton> paintButtons;

	// Token: 0x04003F3F RID: 16191
	private SchedulePaintButton selectedPaint;
}
