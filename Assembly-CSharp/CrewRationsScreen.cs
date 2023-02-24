using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AF7 RID: 2807
public class CrewRationsScreen : CrewListScreen<CrewRationsEntry>
{
	// Token: 0x0600563D RID: 22077 RVA: 0x001F363C File Offset: 0x001F183C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.closebutton.onClick += delegate
		{
			ManagementMenu.Instance.CloseAll();
		};
	}

	// Token: 0x0600563E RID: 22078 RVA: 0x001F366E File Offset: 0x001F186E
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		base.RefreshCrewPortraitContent();
		this.SortByPreviousSelected();
	}

	// Token: 0x0600563F RID: 22079 RVA: 0x001F3684 File Offset: 0x001F1884
	private void SortByPreviousSelected()
	{
		if (this.sortToggleGroup == null)
		{
			return;
		}
		if (this.lastSortToggle == null)
		{
			return;
		}
		for (int i = 0; i < this.ColumnTitlesContainer.childCount; i++)
		{
			OverviewColumnIdentity component = this.ColumnTitlesContainer.GetChild(i).GetComponent<OverviewColumnIdentity>();
			if (this.ColumnTitlesContainer.GetChild(i).GetComponent<Toggle>() == this.lastSortToggle)
			{
				if (component.columnID == "name")
				{
					base.SortByName(this.lastSortReversed);
				}
				if (component.columnID == "health")
				{
					this.SortByAmount("HitPoints", this.lastSortReversed);
				}
				if (component.columnID == "stress")
				{
					this.SortByAmount("Stress", this.lastSortReversed);
				}
				if (component.columnID == "calories")
				{
					this.SortByAmount("Calories", this.lastSortReversed);
				}
			}
		}
	}

	// Token: 0x06005640 RID: 22080 RVA: 0x001F3788 File Offset: 0x001F1988
	protected override void PositionColumnTitles()
	{
		base.PositionColumnTitles();
		for (int i = 0; i < this.ColumnTitlesContainer.childCount; i++)
		{
			OverviewColumnIdentity component = this.ColumnTitlesContainer.GetChild(i).GetComponent<OverviewColumnIdentity>();
			if (component.Sortable)
			{
				Toggle toggle = this.ColumnTitlesContainer.GetChild(i).GetComponent<Toggle>();
				toggle.group = this.sortToggleGroup;
				ImageToggleState toggleImage = toggle.GetComponentInChildren<ImageToggleState>(true);
				if (component.columnID == "name")
				{
					toggle.onValueChanged.AddListener(delegate(bool value)
					{
						this.SortByName(!toggle.isOn);
						this.lastSortToggle = toggle;
						this.lastSortReversed = !toggle.isOn;
						this.ResetSortToggles(toggle);
						if (toggle.isOn)
						{
							toggleImage.SetActive();
							return;
						}
						toggleImage.SetInactive();
					});
				}
				if (component.columnID == "health")
				{
					toggle.onValueChanged.AddListener(delegate(bool value)
					{
						this.SortByAmount("HitPoints", !toggle.isOn);
						this.lastSortToggle = toggle;
						this.lastSortReversed = !toggle.isOn;
						this.ResetSortToggles(toggle);
						if (toggle.isOn)
						{
							toggleImage.SetActive();
							return;
						}
						toggleImage.SetInactive();
					});
				}
				if (component.columnID == "stress")
				{
					toggle.onValueChanged.AddListener(delegate(bool value)
					{
						this.SortByAmount("Stress", !toggle.isOn);
						this.lastSortToggle = toggle;
						this.lastSortReversed = !toggle.isOn;
						this.ResetSortToggles(toggle);
						if (toggle.isOn)
						{
							toggleImage.SetActive();
							return;
						}
						toggleImage.SetInactive();
					});
				}
				if (component.columnID == "calories")
				{
					toggle.onValueChanged.AddListener(delegate(bool value)
					{
						this.SortByAmount("Calories", !toggle.isOn);
						this.lastSortToggle = toggle;
						this.lastSortReversed = !toggle.isOn;
						this.ResetSortToggles(toggle);
						if (toggle.isOn)
						{
							toggleImage.SetActive();
							return;
						}
						toggleImage.SetInactive();
					});
				}
			}
		}
	}

	// Token: 0x06005641 RID: 22081 RVA: 0x001F38D4 File Offset: 0x001F1AD4
	protected override void SpawnEntries()
	{
		base.SpawnEntries();
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			CrewRationsEntry component = Util.KInstantiateUI(this.Prefab_CrewEntry, this.EntriesPanelTransform.gameObject, false).GetComponent<CrewRationsEntry>();
			component.Populate(minionIdentity);
			this.EntryObjects.Add(component);
		}
		this.SortByPreviousSelected();
	}

	// Token: 0x06005642 RID: 22082 RVA: 0x001F3960 File Offset: 0x001F1B60
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
		foreach (CrewRationsEntry crewRationsEntry in this.EntryObjects)
		{
			crewRationsEntry.Refresh();
		}
	}

	// Token: 0x06005643 RID: 22083 RVA: 0x001F39B8 File Offset: 0x001F1BB8
	private void SortByAmount(string amount_id, bool reverse)
	{
		List<CrewRationsEntry> list = new List<CrewRationsEntry>(this.EntryObjects);
		list.Sort(delegate(CrewRationsEntry a, CrewRationsEntry b)
		{
			float value = a.Identity.GetAmounts().GetValue(amount_id);
			float value2 = b.Identity.GetAmounts().GetValue(amount_id);
			return value.CompareTo(value2);
		});
		base.ReorderEntries(list, reverse);
	}

	// Token: 0x06005644 RID: 22084 RVA: 0x001F39F8 File Offset: 0x001F1BF8
	private void ResetSortToggles(Toggle exceptToggle)
	{
		for (int i = 0; i < this.ColumnTitlesContainer.childCount; i++)
		{
			Toggle component = this.ColumnTitlesContainer.GetChild(i).GetComponent<Toggle>();
			ImageToggleState componentInChildren = component.GetComponentInChildren<ImageToggleState>(true);
			if (component != exceptToggle)
			{
				componentInChildren.SetDisabled();
			}
		}
	}

	// Token: 0x04003AB6 RID: 15030
	[SerializeField]
	private KButton closebutton;
}
