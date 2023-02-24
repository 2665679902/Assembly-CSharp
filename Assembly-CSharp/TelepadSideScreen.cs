using System;
using System.Collections.Generic;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BE7 RID: 3047
public class TelepadSideScreen : SideScreenContent
{
	// Token: 0x06005FF8 RID: 24568 RVA: 0x00231D14 File Offset: 0x0022FF14
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.viewImmigrantsBtn.onClick += delegate
		{
			ImmigrantScreen.InitializeImmigrantScreen(this.targetTelepad);
			Game.Instance.Trigger(288942073, null);
		};
		this.viewColonySummaryBtn.onClick += delegate
		{
			this.newAchievementsEarned.gameObject.SetActive(false);
			MainMenu.ActivateRetiredColoniesScreenFromData(PauseScreen.Instance.transform.parent.gameObject, RetireColonyUtility.GetCurrentColonyRetiredColonyData());
		};
		this.openRolesScreenButton.onClick += delegate
		{
			ManagementMenu.Instance.ToggleSkills();
		};
		this.BuildVictoryConditions();
	}

	// Token: 0x06005FF9 RID: 24569 RVA: 0x00231D85 File Offset: 0x0022FF85
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Telepad>() != null;
	}

	// Token: 0x06005FFA RID: 24570 RVA: 0x00231D94 File Offset: 0x0022FF94
	public override void SetTarget(GameObject target)
	{
		Telepad component = target.GetComponent<Telepad>();
		if (component == null)
		{
			global::Debug.LogError("Target doesn't have a telepad associated with it.");
			return;
		}
		this.targetTelepad = component;
		if (this.targetTelepad != null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06005FFB RID: 24571 RVA: 0x00231DEC File Offset: 0x0022FFEC
	private void Update()
	{
		if (this.targetTelepad != null)
		{
			if (GameFlowManager.Instance != null && GameFlowManager.Instance.IsGameOver())
			{
				base.gameObject.SetActive(false);
				this.timeLabel.text = UI.UISIDESCREENS.TELEPADSIDESCREEN.GAMEOVER;
				this.SetContentState(true);
			}
			else
			{
				if (this.targetTelepad.GetComponent<Operational>().IsOperational)
				{
					this.timeLabel.text = string.Format(UI.UISIDESCREENS.TELEPADSIDESCREEN.NEXTPRODUCTION, GameUtil.GetFormattedCycles(this.targetTelepad.GetTimeRemaining(), "F1", false));
				}
				else
				{
					base.gameObject.SetActive(false);
				}
				this.SetContentState(!Immigration.Instance.ImmigrantsAvailable);
			}
			this.UpdateVictoryConditions();
			this.UpdateAchievementsUnlocked();
			this.UpdateSkills();
		}
	}

	// Token: 0x06005FFC RID: 24572 RVA: 0x00231EC4 File Offset: 0x002300C4
	private void SetContentState(bool isLabel)
	{
		if (this.timeLabel.gameObject.activeInHierarchy != isLabel)
		{
			this.timeLabel.gameObject.SetActive(isLabel);
		}
		if (this.viewImmigrantsBtn.gameObject.activeInHierarchy == isLabel)
		{
			this.viewImmigrantsBtn.gameObject.SetActive(!isLabel);
		}
	}

	// Token: 0x06005FFD RID: 24573 RVA: 0x00231F1C File Offset: 0x0023011C
	private void BuildVictoryConditions()
	{
		foreach (ColonyAchievement colonyAchievement in Db.Get().ColonyAchievements.resources)
		{
			if (colonyAchievement.isVictoryCondition && !colonyAchievement.Disabled)
			{
				Dictionary<ColonyAchievementRequirement, GameObject> dictionary = new Dictionary<ColonyAchievementRequirement, GameObject>();
				this.victoryAchievementWidgets.Add(colonyAchievement, dictionary);
				GameObject gameObject = Util.KInstantiateUI(this.conditionContainerTemplate, this.victoryConditionsContainer, true);
				gameObject.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(colonyAchievement.Name);
				foreach (ColonyAchievementRequirement colonyAchievementRequirement in colonyAchievement.requirementChecklist)
				{
					VictoryColonyAchievementRequirement victoryColonyAchievementRequirement = colonyAchievementRequirement as VictoryColonyAchievementRequirement;
					if (victoryColonyAchievementRequirement != null)
					{
						GameObject gameObject2 = Util.KInstantiateUI(this.checkboxLinePrefab, gameObject, true);
						gameObject2.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(victoryColonyAchievementRequirement.Name());
						gameObject2.GetComponent<ToolTip>().SetSimpleTooltip(victoryColonyAchievementRequirement.Description());
						dictionary.Add(colonyAchievementRequirement, gameObject2);
					}
					else
					{
						global::Debug.LogWarning(string.Format("Colony achievement {0} is not a victory requirement but it is attached to a victory achievement {1}.", colonyAchievementRequirement.GetType().ToString(), colonyAchievement.Name));
					}
				}
				this.entries.Add(colonyAchievement.Id, dictionary);
			}
		}
	}

	// Token: 0x06005FFE RID: 24574 RVA: 0x002320B4 File Offset: 0x002302B4
	private void UpdateVictoryConditions()
	{
		foreach (ColonyAchievement colonyAchievement in Db.Get().ColonyAchievements.resources)
		{
			if (colonyAchievement.isVictoryCondition && !colonyAchievement.Disabled)
			{
				foreach (ColonyAchievementRequirement colonyAchievementRequirement in colonyAchievement.requirementChecklist)
				{
					this.entries[colonyAchievement.Id][colonyAchievementRequirement].GetComponent<HierarchyReferences>().GetReference<Image>("Check").enabled = colonyAchievementRequirement.Success();
				}
			}
		}
		foreach (KeyValuePair<ColonyAchievement, Dictionary<ColonyAchievementRequirement, GameObject>> keyValuePair in this.victoryAchievementWidgets)
		{
			foreach (KeyValuePair<ColonyAchievementRequirement, GameObject> keyValuePair2 in keyValuePair.Value)
			{
				keyValuePair2.Value.GetComponent<ToolTip>().SetSimpleTooltip(keyValuePair2.Key.GetProgress(keyValuePair2.Key.Success()));
			}
		}
	}

	// Token: 0x06005FFF RID: 24575 RVA: 0x0023222C File Offset: 0x0023042C
	private void UpdateAchievementsUnlocked()
	{
		if (SaveGame.Instance.GetComponent<ColonyAchievementTracker>().achievementsToDisplay.Count > 0)
		{
			this.newAchievementsEarned.gameObject.SetActive(true);
		}
	}

	// Token: 0x06006000 RID: 24576 RVA: 0x00232258 File Offset: 0x00230458
	private void UpdateSkills()
	{
		bool flag = false;
		foreach (object obj in Components.MinionResumes)
		{
			MinionResume minionResume = (MinionResume)obj;
			if (!minionResume.HasTag(GameTags.Dead) && minionResume.TotalSkillPointsGained - minionResume.SkillsMastered > 0)
			{
				flag = true;
				break;
			}
		}
		this.skillPointsAvailable.gameObject.SetActive(flag);
	}

	// Token: 0x040041B7 RID: 16823
	[SerializeField]
	private LocText timeLabel;

	// Token: 0x040041B8 RID: 16824
	[SerializeField]
	private KButton viewImmigrantsBtn;

	// Token: 0x040041B9 RID: 16825
	[SerializeField]
	private Telepad targetTelepad;

	// Token: 0x040041BA RID: 16826
	[SerializeField]
	private KButton viewColonySummaryBtn;

	// Token: 0x040041BB RID: 16827
	[SerializeField]
	private Image newAchievementsEarned;

	// Token: 0x040041BC RID: 16828
	[SerializeField]
	private KButton openRolesScreenButton;

	// Token: 0x040041BD RID: 16829
	[SerializeField]
	private Image skillPointsAvailable;

	// Token: 0x040041BE RID: 16830
	[SerializeField]
	private GameObject victoryConditionsContainer;

	// Token: 0x040041BF RID: 16831
	[SerializeField]
	private GameObject conditionContainerTemplate;

	// Token: 0x040041C0 RID: 16832
	[SerializeField]
	private GameObject checkboxLinePrefab;

	// Token: 0x040041C1 RID: 16833
	private Dictionary<string, Dictionary<ColonyAchievementRequirement, GameObject>> entries = new Dictionary<string, Dictionary<ColonyAchievementRequirement, GameObject>>();

	// Token: 0x040041C2 RID: 16834
	private Dictionary<ColonyAchievement, Dictionary<ColonyAchievementRequirement, GameObject>> victoryAchievementWidgets = new Dictionary<ColonyAchievement, Dictionary<ColonyAchievementRequirement, GameObject>>();
}
