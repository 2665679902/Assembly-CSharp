using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AF1 RID: 2801
public class CrewJobsEntry : CrewListEntry
{
	// Token: 0x17000641 RID: 1601
	// (get) Token: 0x060055EE RID: 21998 RVA: 0x001F11F1 File Offset: 0x001EF3F1
	// (set) Token: 0x060055EF RID: 21999 RVA: 0x001F11F9 File Offset: 0x001EF3F9
	public ChoreConsumer consumer { get; private set; }

	// Token: 0x060055F0 RID: 22000 RVA: 0x001F1204 File Offset: 0x001EF404
	public override void Populate(MinionIdentity _identity)
	{
		base.Populate(_identity);
		this.consumer = _identity.GetComponent<ChoreConsumer>();
		ChoreConsumer consumer = this.consumer;
		consumer.choreRulesChanged = (System.Action)Delegate.Combine(consumer.choreRulesChanged, new System.Action(this.Dirty));
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			this.CreateChoreButton(choreGroup);
		}
		this.CreateAllTaskButton();
		this.dirty = true;
	}

	// Token: 0x060055F1 RID: 22001 RVA: 0x001F12A8 File Offset: 0x001EF4A8
	private void CreateChoreButton(ChoreGroup chore_group)
	{
		GameObject gameObject = Util.KInstantiateUI(this.Prefab_JobPriorityButton, base.transform.gameObject, false);
		gameObject.GetComponent<OverviewColumnIdentity>().columnID = chore_group.Id;
		gameObject.GetComponent<OverviewColumnIdentity>().Column_DisplayName = chore_group.Name;
		CrewJobsEntry.PriorityButton priorityButton = default(CrewJobsEntry.PriorityButton);
		priorityButton.button = gameObject.GetComponent<Button>();
		priorityButton.border = gameObject.transform.GetChild(1).GetComponent<Image>();
		priorityButton.baseBorderColor = priorityButton.border.color;
		priorityButton.background = gameObject.transform.GetChild(0).GetComponent<Image>();
		priorityButton.baseBackgroundColor = priorityButton.background.color;
		priorityButton.choreGroup = chore_group;
		priorityButton.ToggleIcon = gameObject.transform.GetChild(2).gameObject;
		priorityButton.tooltip = gameObject.GetComponent<ToolTip>();
		priorityButton.tooltip.OnToolTip = () => this.OnPriorityButtonTooltip(priorityButton);
		priorityButton.button.onClick.AddListener(delegate
		{
			this.OnPriorityPress(chore_group);
		});
		this.PriorityButtons.Add(priorityButton);
	}

	// Token: 0x060055F2 RID: 22002 RVA: 0x001F1424 File Offset: 0x001EF624
	private void CreateAllTaskButton()
	{
		GameObject gameObject = Util.KInstantiateUI(this.Prefab_JobPriorityButtonAllTasks, base.transform.gameObject, false);
		gameObject.GetComponent<OverviewColumnIdentity>().columnID = "AllTasks";
		gameObject.GetComponent<OverviewColumnIdentity>().Column_DisplayName = "";
		Button b = gameObject.GetComponent<Button>();
		b.onClick.AddListener(delegate
		{
			this.ToggleTasksAll(b);
		});
		CrewJobsEntry.PriorityButton priorityButton = default(CrewJobsEntry.PriorityButton);
		priorityButton.button = gameObject.GetComponent<Button>();
		priorityButton.border = gameObject.transform.GetChild(1).GetComponent<Image>();
		priorityButton.baseBorderColor = priorityButton.border.color;
		priorityButton.background = gameObject.transform.GetChild(0).GetComponent<Image>();
		priorityButton.baseBackgroundColor = priorityButton.background.color;
		priorityButton.ToggleIcon = gameObject.transform.GetChild(2).gameObject;
		priorityButton.tooltip = gameObject.GetComponent<ToolTip>();
		this.AllTasksButton = priorityButton;
	}

	// Token: 0x060055F3 RID: 22003 RVA: 0x001F1534 File Offset: 0x001EF734
	private void ToggleTasksAll(Button button)
	{
		bool flag = this.rowToggleState != CrewJobsScreen.everyoneToggleState.on;
		string text = "HUD_Click_Deselect";
		if (flag)
		{
			text = "HUD_Click";
		}
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound(text, false));
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			this.consumer.SetPermittedByUser(choreGroup, flag);
		}
	}

	// Token: 0x060055F4 RID: 22004 RVA: 0x001F15C0 File Offset: 0x001EF7C0
	private void OnPriorityPress(ChoreGroup chore_group)
	{
		bool flag = this.consumer.IsPermittedByUser(chore_group);
		string text = "HUD_Click";
		if (flag)
		{
			text = "HUD_Click_Deselect";
		}
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound(text, false));
		this.consumer.SetPermittedByUser(chore_group, !this.consumer.IsPermittedByUser(chore_group));
	}

	// Token: 0x060055F5 RID: 22005 RVA: 0x001F1614 File Offset: 0x001EF814
	private void Refresh(object data = null)
	{
		if (this.identity == null)
		{
			this.dirty = false;
			return;
		}
		if (this.dirty)
		{
			Attributes attributes = this.identity.GetAttributes();
			foreach (CrewJobsEntry.PriorityButton priorityButton in this.PriorityButtons)
			{
				bool flag = this.consumer.IsPermittedByUser(priorityButton.choreGroup);
				if (priorityButton.ToggleIcon.activeSelf != flag)
				{
					priorityButton.ToggleIcon.SetActive(flag);
				}
				float num = Mathf.Min(attributes.Get(priorityButton.choreGroup.attribute).GetTotalValue() / 10f, 1f);
				Color baseBorderColor = priorityButton.baseBorderColor;
				baseBorderColor.r = Mathf.Lerp(priorityButton.baseBorderColor.r, 0.72156864f, num);
				baseBorderColor.g = Mathf.Lerp(priorityButton.baseBorderColor.g, 0.44313726f, num);
				baseBorderColor.b = Mathf.Lerp(priorityButton.baseBorderColor.b, 0.5803922f, num);
				if (priorityButton.border.color != baseBorderColor)
				{
					priorityButton.border.color = baseBorderColor;
				}
				Color color = priorityButton.baseBackgroundColor;
				color.a = Mathf.Lerp(0f, 1f, num);
				bool flag2 = this.consumer.IsPermittedByTraits(priorityButton.choreGroup);
				if (!flag2)
				{
					color = Color.clear;
					priorityButton.border.color = Color.clear;
					priorityButton.ToggleIcon.SetActive(false);
				}
				priorityButton.button.interactable = flag2;
				if (priorityButton.background.color != color)
				{
					priorityButton.background.color = color;
				}
			}
			int num2 = 0;
			int num3 = 0;
			foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
			{
				if (this.consumer.IsPermittedByTraits(choreGroup))
				{
					num3++;
					if (this.consumer.IsPermittedByUser(choreGroup))
					{
						num2++;
					}
				}
			}
			if (num2 == 0)
			{
				this.rowToggleState = CrewJobsScreen.everyoneToggleState.off;
			}
			else if (num2 < num3)
			{
				this.rowToggleState = CrewJobsScreen.everyoneToggleState.mixed;
			}
			else
			{
				this.rowToggleState = CrewJobsScreen.everyoneToggleState.on;
			}
			ImageToggleState component = this.AllTasksButton.ToggleIcon.GetComponent<ImageToggleState>();
			switch (this.rowToggleState)
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
			this.dirty = false;
		}
	}

	// Token: 0x060055F6 RID: 22006 RVA: 0x001F1904 File Offset: 0x001EFB04
	private string OnPriorityButtonTooltip(CrewJobsEntry.PriorityButton b)
	{
		b.tooltip.ClearMultiStringTooltip();
		if (this.identity != null)
		{
			Attributes attributes = this.identity.GetAttributes();
			if (attributes != null)
			{
				if (!this.consumer.IsPermittedByTraits(b.choreGroup))
				{
					string text = string.Format(UI.TOOLTIPS.JOBSSCREEN_CANNOTPERFORMTASK, this.consumer.GetComponent<MinionIdentity>().GetProperName());
					b.tooltip.AddMultiStringTooltip(text, this.TooltipTextStyle_AbilityNegativeModifier);
					return "";
				}
				b.tooltip.AddMultiStringTooltip(UI.TOOLTIPS.JOBSSCREEN_RELEVANT_ATTRIBUTES, this.TooltipTextStyle_Ability);
				Klei.AI.Attribute attribute = b.choreGroup.attribute;
				AttributeInstance attributeInstance = attributes.Get(attribute);
				float totalValue = attributeInstance.GetTotalValue();
				TextStyleSetting textStyleSetting = this.TooltipTextStyle_Ability;
				if (totalValue > 0f)
				{
					textStyleSetting = this.TooltipTextStyle_AbilityPositiveModifier;
				}
				else if (totalValue < 0f)
				{
					textStyleSetting = this.TooltipTextStyle_AbilityNegativeModifier;
				}
				b.tooltip.AddMultiStringTooltip(attribute.Name + " " + attributeInstance.GetTotalValue().ToString(), textStyleSetting);
			}
		}
		return "";
	}

	// Token: 0x060055F7 RID: 22007 RVA: 0x001F1A1D File Offset: 0x001EFC1D
	private void LateUpdate()
	{
		this.Refresh(null);
	}

	// Token: 0x060055F8 RID: 22008 RVA: 0x001F1A26 File Offset: 0x001EFC26
	private void OnLevelUp(object data)
	{
		this.Dirty();
	}

	// Token: 0x060055F9 RID: 22009 RVA: 0x001F1A2E File Offset: 0x001EFC2E
	private void Dirty()
	{
		this.dirty = true;
		CrewJobsScreen.Instance.Dirty(null);
	}

	// Token: 0x060055FA RID: 22010 RVA: 0x001F1A42 File Offset: 0x001EFC42
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.consumer != null)
		{
			ChoreConsumer consumer = this.consumer;
			consumer.choreRulesChanged = (System.Action)Delegate.Remove(consumer.choreRulesChanged, new System.Action(this.Dirty));
		}
	}

	// Token: 0x04003A76 RID: 14966
	public GameObject Prefab_JobPriorityButton;

	// Token: 0x04003A77 RID: 14967
	public GameObject Prefab_JobPriorityButtonAllTasks;

	// Token: 0x04003A78 RID: 14968
	private List<CrewJobsEntry.PriorityButton> PriorityButtons = new List<CrewJobsEntry.PriorityButton>();

	// Token: 0x04003A79 RID: 14969
	private CrewJobsEntry.PriorityButton AllTasksButton;

	// Token: 0x04003A7A RID: 14970
	public TextStyleSetting TooltipTextStyle_Title;

	// Token: 0x04003A7B RID: 14971
	public TextStyleSetting TooltipTextStyle_Ability;

	// Token: 0x04003A7C RID: 14972
	public TextStyleSetting TooltipTextStyle_AbilityPositiveModifier;

	// Token: 0x04003A7D RID: 14973
	public TextStyleSetting TooltipTextStyle_AbilityNegativeModifier;

	// Token: 0x04003A7E RID: 14974
	private bool dirty;

	// Token: 0x04003A80 RID: 14976
	private CrewJobsScreen.everyoneToggleState rowToggleState;

	// Token: 0x02001972 RID: 6514
	[Serializable]
	public struct PriorityButton
	{
		// Token: 0x0400745C RID: 29788
		public Button button;

		// Token: 0x0400745D RID: 29789
		public GameObject ToggleIcon;

		// Token: 0x0400745E RID: 29790
		public ChoreGroup choreGroup;

		// Token: 0x0400745F RID: 29791
		public ToolTip tooltip;

		// Token: 0x04007460 RID: 29792
		public Image border;

		// Token: 0x04007461 RID: 29793
		public Image background;

		// Token: 0x04007462 RID: 29794
		public Color baseBorderColor;

		// Token: 0x04007463 RID: 29795
		public Color baseBackgroundColor;
	}
}
