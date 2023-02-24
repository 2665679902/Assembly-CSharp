using System;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AF2 RID: 2802
public class CrewJobsScreen : CrewListScreen<CrewJobsEntry>
{
	// Token: 0x060055FC RID: 22012 RVA: 0x001F1A94 File Offset: 0x001EFC94
	protected override void OnActivate()
	{
		CrewJobsScreen.Instance = this;
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			this.choreGroups.Add(choreGroup);
		}
		base.OnActivate();
	}

	// Token: 0x060055FD RID: 22013 RVA: 0x001F1B04 File Offset: 0x001EFD04
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		base.RefreshCrewPortraitContent();
		this.SortByPreviousSelected();
	}

	// Token: 0x060055FE RID: 22014 RVA: 0x001F1B18 File Offset: 0x001EFD18
	protected override void OnForcedCleanUp()
	{
		CrewJobsScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x060055FF RID: 22015 RVA: 0x001F1B28 File Offset: 0x001EFD28
	protected override void SpawnEntries()
	{
		base.SpawnEntries();
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			CrewJobsEntry component = Util.KInstantiateUI(this.Prefab_CrewEntry, this.EntriesPanelTransform.gameObject, false).GetComponent<CrewJobsEntry>();
			component.Populate(minionIdentity);
			this.EntryObjects.Add(component);
		}
		this.SortEveryoneToggle.group = this.sortToggleGroup;
		ImageToggleState toggleImage = this.SortEveryoneToggle.GetComponentInChildren<ImageToggleState>(true);
		this.SortEveryoneToggle.onValueChanged.AddListener(delegate(bool value)
		{
			this.SortByName(!this.SortEveryoneToggle.isOn);
			this.lastSortToggle = this.SortEveryoneToggle;
			this.lastSortReversed = !this.SortEveryoneToggle.isOn;
			this.ResetSortToggles(this.SortEveryoneToggle);
			if (this.SortEveryoneToggle.isOn)
			{
				toggleImage.SetActive();
				return;
			}
			toggleImage.SetInactive();
		});
		this.SortByPreviousSelected();
		this.dirty = true;
	}

	// Token: 0x06005600 RID: 22016 RVA: 0x001F1C08 File Offset: 0x001EFE08
	private void SortByPreviousSelected()
	{
		if (this.sortToggleGroup == null || this.lastSortToggle == null)
		{
			return;
		}
		int childCount = this.ColumnTitlesContainer.childCount;
		for (int i = 0; i < childCount; i++)
		{
			if (i < this.choreGroups.Count && this.ColumnTitlesContainer.GetChild(i).Find("Title").GetComponentInChildren<Toggle>() == this.lastSortToggle)
			{
				this.SortByEffectiveness(this.choreGroups[i], this.lastSortReversed, false);
				return;
			}
		}
		if (this.SortEveryoneToggle == this.lastSortToggle)
		{
			base.SortByName(this.lastSortReversed);
		}
	}

	// Token: 0x06005601 RID: 22017 RVA: 0x001F1CBC File Offset: 0x001EFEBC
	protected override void PositionColumnTitles()
	{
		base.PositionColumnTitles();
		int childCount = this.ColumnTitlesContainer.childCount;
		for (int i = 0; i < childCount; i++)
		{
			if (i < this.choreGroups.Count)
			{
				Toggle sortToggle = this.ColumnTitlesContainer.GetChild(i).Find("Title").GetComponentInChildren<Toggle>();
				this.ColumnTitlesContainer.GetChild(i).rectTransform().localScale = Vector3.one;
				ChoreGroup chore_group2 = this.choreGroups[i];
				ImageToggleState toggleImage = sortToggle.GetComponentInChildren<ImageToggleState>(true);
				sortToggle.group = this.sortToggleGroup;
				sortToggle.onValueChanged.AddListener(delegate(bool value)
				{
					bool flag = false;
					if (this.lastSortToggle == sortToggle)
					{
						flag = true;
					}
					this.SortByEffectiveness(chore_group2, !sortToggle.isOn, flag);
					this.lastSortToggle = sortToggle;
					this.lastSortReversed = !sortToggle.isOn;
					this.ResetSortToggles(sortToggle);
					if (sortToggle.isOn)
					{
						toggleImage.SetActive();
						return;
					}
					toggleImage.SetInactive();
				});
			}
			ToolTip JobTooltip = this.ColumnTitlesContainer.GetChild(i).GetComponent<ToolTip>();
			ToolTip jobTooltip = JobTooltip;
			jobTooltip.OnToolTip = (Func<string>)Delegate.Combine(jobTooltip.OnToolTip, new Func<string>(() => this.GetJobTooltip(JobTooltip.gameObject)));
			Button componentInChildren = this.ColumnTitlesContainer.GetChild(i).GetComponentInChildren<Button>();
			this.EveryoneToggles.Add(componentInChildren, CrewJobsScreen.everyoneToggleState.on);
		}
		for (int j = 0; j < this.choreGroups.Count; j++)
		{
			ChoreGroup chore_group = this.choreGroups[j];
			Button b = this.EveryoneToggles.Keys.ElementAt(j);
			this.EveryoneToggles.Keys.ElementAt(j).onClick.AddListener(delegate
			{
				this.ToggleJobEveryone(b, chore_group);
			});
		}
		Button key = this.EveryoneToggles.ElementAt(this.EveryoneToggles.Count - 1).Key;
		key.transform.parent.Find("Title").gameObject.GetComponentInChildren<Toggle>().gameObject.SetActive(false);
		key.onClick.AddListener(delegate
		{
			this.ToggleAllTasksEveryone();
		});
		this.EveryoneAllTaskToggle = new KeyValuePair<Button, CrewJobsScreen.everyoneToggleState>(key, this.EveryoneAllTaskToggle.Value);
	}

	// Token: 0x06005602 RID: 22018 RVA: 0x001F1F08 File Offset: 0x001F0108
	private string GetJobTooltip(GameObject go)
	{
		ToolTip component = go.GetComponent<ToolTip>();
		component.ClearMultiStringTooltip();
		OverviewColumnIdentity component2 = go.GetComponent<OverviewColumnIdentity>();
		if (component2.columnID != "AllTasks")
		{
			ChoreGroup choreGroup = Db.Get().ChoreGroups.Get(component2.columnID);
			component.AddMultiStringTooltip(component2.Column_DisplayName, this.TextStyle_JobTooltip_Title);
			component.AddMultiStringTooltip(choreGroup.description, this.TextStyle_JobTooltip_Description);
			component.AddMultiStringTooltip("\n", this.TextStyle_JobTooltip_Description);
			component.AddMultiStringTooltip(UI.TOOLTIPS.JOBSSCREEN_ATTRIBUTES, this.TextStyle_JobTooltip_Description);
			component.AddMultiStringTooltip("•  " + choreGroup.attribute.Name, this.TextStyle_JobTooltip_RelevantAttributes);
		}
		return "";
	}

	// Token: 0x06005603 RID: 22019 RVA: 0x001F1FC8 File Offset: 0x001F01C8
	private void ToggleAllTasksEveryone()
	{
		string text = "HUD_Click_Deselect";
		if (this.EveryoneAllTaskToggle.Value != CrewJobsScreen.everyoneToggleState.on)
		{
			text = "HUD_Click";
		}
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound(text, false));
		for (int i = 0; i < this.choreGroups.Count; i++)
		{
			this.SetJobEveryone(this.EveryoneAllTaskToggle.Value != CrewJobsScreen.everyoneToggleState.on, this.choreGroups[i]);
		}
	}

	// Token: 0x06005604 RID: 22020 RVA: 0x001F2034 File Offset: 0x001F0234
	private void SetJobEveryone(Button button, ChoreGroup chore_group)
	{
		this.SetJobEveryone(this.EveryoneToggles[button] != CrewJobsScreen.everyoneToggleState.on, chore_group);
	}

	// Token: 0x06005605 RID: 22021 RVA: 0x001F2050 File Offset: 0x001F0250
	private void SetJobEveryone(bool state, ChoreGroup chore_group)
	{
		foreach (CrewJobsEntry crewJobsEntry in this.EntryObjects)
		{
			crewJobsEntry.consumer.SetPermittedByUser(chore_group, state);
		}
	}

	// Token: 0x06005606 RID: 22022 RVA: 0x001F20A8 File Offset: 0x001F02A8
	private void ToggleJobEveryone(Button button, ChoreGroup chore_group)
	{
		string text = "HUD_Click_Deselect";
		if (this.EveryoneToggles[button] != CrewJobsScreen.everyoneToggleState.on)
		{
			text = "HUD_Click";
		}
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound(text, false));
		foreach (CrewJobsEntry crewJobsEntry in this.EntryObjects)
		{
			crewJobsEntry.consumer.SetPermittedByUser(chore_group, this.EveryoneToggles[button] != CrewJobsScreen.everyoneToggleState.on);
		}
	}

	// Token: 0x06005607 RID: 22023 RVA: 0x001F2138 File Offset: 0x001F0338
	private void SortByEffectiveness(ChoreGroup chore_group, bool reverse, bool playSound)
	{
		if (playSound)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click", false));
		}
		List<CrewJobsEntry> list = new List<CrewJobsEntry>(this.EntryObjects);
		list.Sort(delegate(CrewJobsEntry a, CrewJobsEntry b)
		{
			float value = a.Identity.GetAttributes().GetValue(chore_group.attribute.Id);
			float value2 = b.Identity.GetAttributes().GetValue(chore_group.attribute.Id);
			return value.CompareTo(value2);
		});
		base.ReorderEntries(list, reverse);
	}

	// Token: 0x06005608 RID: 22024 RVA: 0x001F218C File Offset: 0x001F038C
	private void ResetSortToggles(Toggle exceptToggle)
	{
		for (int i = 0; i < this.ColumnTitlesContainer.childCount; i++)
		{
			Toggle componentInChildren = this.ColumnTitlesContainer.GetChild(i).Find("Title").GetComponentInChildren<Toggle>();
			if (!(componentInChildren == null))
			{
				ImageToggleState componentInChildren2 = componentInChildren.GetComponentInChildren<ImageToggleState>(true);
				if (componentInChildren != exceptToggle)
				{
					componentInChildren2.SetDisabled();
				}
			}
		}
		ImageToggleState componentInChildren3 = this.SortEveryoneToggle.GetComponentInChildren<ImageToggleState>(true);
		if (this.SortEveryoneToggle != exceptToggle)
		{
			componentInChildren3.SetDisabled();
		}
	}

	// Token: 0x06005609 RID: 22025 RVA: 0x001F220C File Offset: 0x001F040C
	private void Refresh()
	{
		if (this.dirty)
		{
			int childCount = this.ColumnTitlesContainer.childCount;
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < childCount; i++)
			{
				bool flag3 = false;
				bool flag4 = false;
				if (this.choreGroups.Count - 1 >= i)
				{
					ChoreGroup choreGroup = this.choreGroups[i];
					for (int j = 0; j < this.EntryObjects.Count; j++)
					{
						ChoreConsumer consumer = this.EntryObjects[j].GetComponent<CrewJobsEntry>().consumer;
						if (consumer.IsPermittedByTraits(choreGroup))
						{
							if (consumer.IsPermittedByUser(choreGroup))
							{
								flag3 = true;
								flag = true;
							}
							else
							{
								flag4 = true;
								flag2 = true;
							}
						}
					}
					if (flag3 && flag4)
					{
						this.EveryoneToggles[this.EveryoneToggles.ElementAt(i).Key] = CrewJobsScreen.everyoneToggleState.mixed;
					}
					else if (flag3)
					{
						this.EveryoneToggles[this.EveryoneToggles.ElementAt(i).Key] = CrewJobsScreen.everyoneToggleState.on;
					}
					else
					{
						this.EveryoneToggles[this.EveryoneToggles.ElementAt(i).Key] = CrewJobsScreen.everyoneToggleState.off;
					}
					Button componentInChildren = this.ColumnTitlesContainer.GetChild(i).GetComponentInChildren<Button>();
					ImageToggleState component = componentInChildren.GetComponentsInChildren<Image>(true)[1].GetComponent<ImageToggleState>();
					switch (this.EveryoneToggles[componentInChildren])
					{
					case CrewJobsScreen.everyoneToggleState.off:
						component.SetDisabled();
						break;
					case CrewJobsScreen.everyoneToggleState.mixed:
						component.SetInactive();
						break;
					case CrewJobsScreen.everyoneToggleState.on:
						component.SetActive();
						break;
					}
				}
			}
			if (flag && flag2)
			{
				this.EveryoneAllTaskToggle = new KeyValuePair<Button, CrewJobsScreen.everyoneToggleState>(this.EveryoneAllTaskToggle.Key, CrewJobsScreen.everyoneToggleState.mixed);
			}
			else if (flag)
			{
				this.EveryoneAllTaskToggle = new KeyValuePair<Button, CrewJobsScreen.everyoneToggleState>(this.EveryoneAllTaskToggle.Key, CrewJobsScreen.everyoneToggleState.on);
			}
			else if (flag2)
			{
				this.EveryoneAllTaskToggle = new KeyValuePair<Button, CrewJobsScreen.everyoneToggleState>(this.EveryoneAllTaskToggle.Key, CrewJobsScreen.everyoneToggleState.off);
			}
			ImageToggleState component2 = this.EveryoneAllTaskToggle.Key.GetComponentsInChildren<Image>(true)[1].GetComponent<ImageToggleState>();
			switch (this.EveryoneAllTaskToggle.Value)
			{
			case CrewJobsScreen.everyoneToggleState.off:
				component2.SetDisabled();
				break;
			case CrewJobsScreen.everyoneToggleState.mixed:
				component2.SetInactive();
				break;
			case CrewJobsScreen.everyoneToggleState.on:
				component2.SetActive();
				break;
			}
			this.screenWidth = this.EntriesPanelTransform.rectTransform().sizeDelta.x;
			this.ScrollRectTransform.GetComponent<LayoutElement>().minWidth = this.screenWidth;
			float num = 31f;
			base.GetComponent<LayoutElement>().minWidth = this.screenWidth + num;
			this.dirty = false;
		}
	}

	// Token: 0x0600560A RID: 22026 RVA: 0x001F2497 File Offset: 0x001F0697
	private void Update()
	{
		this.Refresh();
	}

	// Token: 0x0600560B RID: 22027 RVA: 0x001F249F File Offset: 0x001F069F
	public void Dirty(object data = null)
	{
		this.dirty = true;
	}

	// Token: 0x04003A81 RID: 14977
	public static CrewJobsScreen Instance;

	// Token: 0x04003A82 RID: 14978
	private Dictionary<Button, CrewJobsScreen.everyoneToggleState> EveryoneToggles = new Dictionary<Button, CrewJobsScreen.everyoneToggleState>();

	// Token: 0x04003A83 RID: 14979
	private KeyValuePair<Button, CrewJobsScreen.everyoneToggleState> EveryoneAllTaskToggle;

	// Token: 0x04003A84 RID: 14980
	public TextStyleSetting TextStyle_JobTooltip_Title;

	// Token: 0x04003A85 RID: 14981
	public TextStyleSetting TextStyle_JobTooltip_Description;

	// Token: 0x04003A86 RID: 14982
	public TextStyleSetting TextStyle_JobTooltip_RelevantAttributes;

	// Token: 0x04003A87 RID: 14983
	public Toggle SortEveryoneToggle;

	// Token: 0x04003A88 RID: 14984
	private List<ChoreGroup> choreGroups = new List<ChoreGroup>();

	// Token: 0x04003A89 RID: 14985
	private bool dirty;

	// Token: 0x04003A8A RID: 14986
	private float screenWidth;

	// Token: 0x02001975 RID: 6517
	public enum everyoneToggleState
	{
		// Token: 0x0400746A RID: 29802
		off,
		// Token: 0x0400746B RID: 29803
		mixed,
		// Token: 0x0400746C RID: 29804
		on
	}
}
