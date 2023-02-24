using System;
using System.Collections.Generic;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x02000B60 RID: 2912
public class PriorityScreen : KScreen
{
	// Token: 0x06005AC4 RID: 23236 RVA: 0x0020F098 File Offset: 0x0020D298
	public void InstantiateButtons(Action<PrioritySetting> on_click, bool playSelectionSound = true)
	{
		this.onClick = on_click;
		for (int i = 1; i <= 9; i++)
		{
			int num = i;
			PriorityButton priorityButton = global::Util.KInstantiateUI<PriorityButton>(this.buttonPrefab_basic.gameObject, this.buttonPrefab_basic.transform.parent.gameObject, false);
			this.buttons_basic.Add(priorityButton);
			priorityButton.playSelectionSound = playSelectionSound;
			priorityButton.onClick = this.onClick;
			priorityButton.text.text = num.ToString();
			priorityButton.priority = new PrioritySetting(PriorityScreen.PriorityClass.basic, num);
			priorityButton.tooltip.SetSimpleTooltip(string.Format(UI.PRIORITYSCREEN.BASIC, num));
		}
		this.buttonPrefab_basic.gameObject.SetActive(false);
		this.button_emergency.playSelectionSound = playSelectionSound;
		this.button_emergency.onClick = this.onClick;
		this.button_emergency.priority = new PrioritySetting(PriorityScreen.PriorityClass.topPriority, 1);
		this.button_emergency.tooltip.SetSimpleTooltip(UI.PRIORITYSCREEN.TOP_PRIORITY);
		this.button_toggleHigh.gameObject.SetActive(false);
		this.PriorityMenuContainer.SetActive(true);
		this.button_priorityMenu.gameObject.SetActive(true);
		this.button_priorityMenu.onClick += this.PriorityButtonClicked;
		this.button_priorityMenu.GetComponent<ToolTip>().SetSimpleTooltip(UI.PRIORITYSCREEN.OPEN_JOBS_SCREEN);
		this.diagram.SetActive(false);
		this.SetScreenPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, 5), false);
	}

	// Token: 0x06005AC5 RID: 23237 RVA: 0x0020F219 File Offset: 0x0020D419
	private void OnClick(PrioritySetting priority)
	{
		if (this.onClick != null)
		{
			this.onClick(priority);
		}
	}

	// Token: 0x06005AC6 RID: 23238 RVA: 0x0020F22F File Offset: 0x0020D42F
	public void ShowDiagram(bool show)
	{
		this.diagram.SetActive(show);
	}

	// Token: 0x06005AC7 RID: 23239 RVA: 0x0020F23D File Offset: 0x0020D43D
	public void ResetPriority()
	{
		this.SetScreenPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, 5), false);
	}

	// Token: 0x06005AC8 RID: 23240 RVA: 0x0020F24D File Offset: 0x0020D44D
	public void PriorityButtonClicked()
	{
		ManagementMenu.Instance.TogglePriorities();
	}

	// Token: 0x06005AC9 RID: 23241 RVA: 0x0020F25C File Offset: 0x0020D45C
	private void RefreshButton(PriorityButton b, PrioritySetting priority, bool play_sound)
	{
		if (b.priority == priority)
		{
			b.toggle.Select();
			b.toggle.isOn = true;
			if (play_sound)
			{
				b.toggle.soundPlayer.Play(0);
				return;
			}
		}
		else
		{
			b.toggle.isOn = false;
		}
	}

	// Token: 0x06005ACA RID: 23242 RVA: 0x0020F2B0 File Offset: 0x0020D4B0
	public void SetScreenPriority(PrioritySetting priority, bool play_sound = false)
	{
		if (this.lastSelectedPriority == priority)
		{
			return;
		}
		this.lastSelectedPriority = priority;
		if (priority.priority_class == PriorityScreen.PriorityClass.high)
		{
			this.button_toggleHigh.isOn = true;
		}
		else if (priority.priority_class == PriorityScreen.PriorityClass.basic)
		{
			this.button_toggleHigh.isOn = false;
		}
		for (int i = 0; i < this.buttons_basic.Count; i++)
		{
			this.buttons_basic[i].priority = new PrioritySetting(this.button_toggleHigh.isOn ? PriorityScreen.PriorityClass.high : PriorityScreen.PriorityClass.basic, i + 1);
			this.buttons_basic[i].tooltip.SetSimpleTooltip(string.Format(this.button_toggleHigh.isOn ? UI.PRIORITYSCREEN.HIGH : UI.PRIORITYSCREEN.BASIC, i + 1));
			this.RefreshButton(this.buttons_basic[i], this.lastSelectedPriority, play_sound);
		}
		this.RefreshButton(this.button_emergency, this.lastSelectedPriority, play_sound);
	}

	// Token: 0x06005ACB RID: 23243 RVA: 0x0020F3B1 File Offset: 0x0020D5B1
	public PrioritySetting GetLastSelectedPriority()
	{
		return this.lastSelectedPriority;
	}

	// Token: 0x06005ACC RID: 23244 RVA: 0x0020F3BC File Offset: 0x0020D5BC
	public static void PlayPriorityConfirmSound(PrioritySetting priority)
	{
		EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound("Priority_Tool_Confirm", false), Vector3.zero, 1f);
		if (eventInstance.isValid())
		{
			float num = 0f;
			if (priority.priority_class >= PriorityScreen.PriorityClass.high)
			{
				num += 10f;
			}
			if (priority.priority_class >= PriorityScreen.PriorityClass.topPriority)
			{
				num += 0f;
			}
			num += (float)priority.priority_value;
			eventInstance.setParameterByName("priority", num, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x04003D7E RID: 15742
	[SerializeField]
	protected PriorityButton buttonPrefab_basic;

	// Token: 0x04003D7F RID: 15743
	[SerializeField]
	protected GameObject EmergencyContainer;

	// Token: 0x04003D80 RID: 15744
	[SerializeField]
	protected PriorityButton button_emergency;

	// Token: 0x04003D81 RID: 15745
	[SerializeField]
	protected GameObject PriorityMenuContainer;

	// Token: 0x04003D82 RID: 15746
	[SerializeField]
	protected KButton button_priorityMenu;

	// Token: 0x04003D83 RID: 15747
	[SerializeField]
	protected KToggle button_toggleHigh;

	// Token: 0x04003D84 RID: 15748
	[SerializeField]
	protected GameObject diagram;

	// Token: 0x04003D85 RID: 15749
	protected List<PriorityButton> buttons_basic = new List<PriorityButton>();

	// Token: 0x04003D86 RID: 15750
	protected List<PriorityButton> buttons_emergency = new List<PriorityButton>();

	// Token: 0x04003D87 RID: 15751
	private PrioritySetting priority;

	// Token: 0x04003D88 RID: 15752
	private PrioritySetting lastSelectedPriority = new PrioritySetting(PriorityScreen.PriorityClass.basic, -1);

	// Token: 0x04003D89 RID: 15753
	private Action<PrioritySetting> onClick;

	// Token: 0x02001A08 RID: 6664
	public enum PriorityClass
	{
		// Token: 0x04007646 RID: 30278
		idle = -1,
		// Token: 0x04007647 RID: 30279
		basic,
		// Token: 0x04007648 RID: 30280
		high,
		// Token: 0x04007649 RID: 30281
		personalNeeds,
		// Token: 0x0400764A RID: 30282
		topPriority,
		// Token: 0x0400764B RID: 30283
		compulsory
	}
}
