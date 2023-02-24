﻿using System;
using System.Collections.Generic;
using Database;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

// Token: 0x02000BF7 RID: 3063
[AddComponentMenu("KMonoBehaviour/scripts/SkillWidget")]
public class SkillWidget : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
	// Token: 0x170006B4 RID: 1716
	// (get) Token: 0x060060CD RID: 24781 RVA: 0x0023811B File Offset: 0x0023631B
	// (set) Token: 0x060060CE RID: 24782 RVA: 0x00238123 File Offset: 0x00236323
	public string skillID { get; private set; }

	// Token: 0x060060CF RID: 24783 RVA: 0x0023812C File Offset: 0x0023632C
	public void Refresh(string skillID)
	{
		Skill skill = Db.Get().Skills.Get(skillID);
		if (skill == null)
		{
			global::Debug.LogWarning("DbSkills is missing skillId " + skillID);
			return;
		}
		this.Name.text = skill.Name;
		LocText name = this.Name;
		name.text = name.text + "\n(" + Db.Get().SkillGroups.Get(skill.skillGroup).Name + ")";
		this.skillID = skillID;
		this.tooltip.SetSimpleTooltip(this.SkillTooltip(skill));
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.skillsScreen.GetMinionIdentity(this.skillsScreen.CurrentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		MinionResume minionResume = null;
		if (minionIdentity != null)
		{
			minionResume = minionIdentity.GetComponent<MinionResume>();
			MinionResume.SkillMasteryConditions[] skillMasteryConditions = minionResume.GetSkillMasteryConditions(skillID);
			bool flag = minionResume.CanMasterSkill(skillMasteryConditions);
			if (!(minionResume == null) && (minionResume.HasMasteredSkill(skillID) || flag))
			{
				this.TitleBarBG.color = (minionResume.HasMasteredSkill(skillID) ? this.header_color_has_skill : this.header_color_can_assign);
				this.hatImage.material = this.defaultMaterial;
			}
			else
			{
				this.TitleBarBG.color = this.header_color_disabled;
				this.hatImage.material = this.desaturatedMaterial;
			}
		}
		else if (storedMinionIdentity != null)
		{
			if (storedMinionIdentity.HasMasteredSkill(skillID))
			{
				this.TitleBarBG.color = this.header_color_has_skill;
				this.hatImage.material = this.defaultMaterial;
			}
			else
			{
				this.TitleBarBG.color = this.header_color_disabled;
				this.hatImage.material = this.desaturatedMaterial;
			}
		}
		this.hatImage.sprite = Assets.GetSprite(skill.badge);
		bool flag2 = false;
		bool flag3 = false;
		if (minionResume != null)
		{
			flag3 = minionResume.HasBeenGrantedSkill(skill);
			float num;
			minionResume.AptitudeBySkillGroup.TryGetValue(skill.skillGroup, out num);
			flag2 = num > 0f && !flag3;
		}
		this.aptitudeBox.SetActive(flag2);
		this.grantedBox.SetActive(flag3);
		this.traitDisabledIcon.SetActive(minionResume != null && !minionResume.IsAbleToLearnSkill(skill.Id));
		string text = "";
		List<string> list = new List<string>();
		foreach (MinionIdentity minionIdentity2 in Components.LiveMinionIdentities.Items)
		{
			MinionResume component = minionIdentity2.GetComponent<MinionResume>();
			if (component != null && component.HasMasteredSkill(skillID))
			{
				list.Add(component.GetProperName());
			}
		}
		foreach (MinionStorage minionStorage in Components.MinionStorages.Items)
		{
			foreach (MinionStorage.Info info in minionStorage.GetStoredMinionInfo())
			{
				if (info.serializedMinion != null)
				{
					StoredMinionIdentity storedMinionIdentity2 = info.serializedMinion.Get<StoredMinionIdentity>();
					if (storedMinionIdentity2 != null && storedMinionIdentity2.HasMasteredSkill(skillID))
					{
						list.Add(storedMinionIdentity2.GetProperName());
					}
				}
			}
		}
		this.masteryCount.gameObject.SetActive(list.Count > 0);
		foreach (string text2 in list)
		{
			text = text + "\n    • " + text2;
		}
		this.masteryCount.SetSimpleTooltip((list.Count > 0) ? string.Format(UI.ROLES_SCREEN.WIDGET.NUMBER_OF_MASTERS_TOOLTIP, text) : UI.ROLES_SCREEN.WIDGET.NO_MASTERS_TOOLTIP.text);
		this.masteryCount.GetComponentInChildren<LocText>().text = list.Count.ToString();
	}

	// Token: 0x060060D0 RID: 24784 RVA: 0x00238558 File Offset: 0x00236758
	public void RefreshLines()
	{
		this.prerequisiteSkillWidgets.Clear();
		List<Vector2> list = new List<Vector2>();
		foreach (string text in Db.Get().Skills.Get(this.skillID).priorSkills)
		{
			list.Add(this.skillsScreen.GetSkillWidgetLineTargetPosition(text));
			this.prerequisiteSkillWidgets.Add(this.skillsScreen.GetSkillWidget(text));
		}
		if (this.lines != null)
		{
			for (int i = this.lines.Length - 1; i >= 0; i--)
			{
				UnityEngine.Object.Destroy(this.lines[i].gameObject);
			}
		}
		this.linePoints.Clear();
		for (int j = 0; j < list.Count; j++)
		{
			float num = this.lines_left.GetPosition().x - list[j].x - 12f;
			float num2 = 0f;
			this.linePoints.Add(new Vector2(0f, num2));
			this.linePoints.Add(new Vector2(-num, num2));
			this.linePoints.Add(new Vector2(-num, num2));
			this.linePoints.Add(new Vector2(-num, -(this.lines_left.GetPosition().y - list[j].y)));
			this.linePoints.Add(new Vector2(-num, -(this.lines_left.GetPosition().y - list[j].y)));
			this.linePoints.Add(new Vector2(-(this.lines_left.GetPosition().x - list[j].x), -(this.lines_left.GetPosition().y - list[j].y)));
		}
		this.lines = new UILineRenderer[this.linePoints.Count / 2];
		int num3 = 0;
		for (int k = 0; k < this.linePoints.Count; k += 2)
		{
			GameObject gameObject = new GameObject("Line");
			gameObject.AddComponent<RectTransform>();
			gameObject.transform.SetParent(this.lines_left.transform);
			gameObject.transform.SetLocalPosition(Vector3.zero);
			gameObject.rectTransform().sizeDelta = Vector2.zero;
			this.lines[num3] = gameObject.AddComponent<UILineRenderer>();
			this.lines[num3].color = new Color(0.6509804f, 0.6509804f, 0.6509804f, 1f);
			this.lines[num3].Points = new Vector2[]
			{
				this.linePoints[k],
				this.linePoints[k + 1]
			};
			num3++;
		}
	}

	// Token: 0x060060D1 RID: 24785 RVA: 0x0023886C File Offset: 0x00236A6C
	public void ToggleBorderHighlight(bool on)
	{
		this.borderHighlight.SetActive(on);
		if (this.lines != null)
		{
			foreach (UILineRenderer uilineRenderer in this.lines)
			{
				uilineRenderer.color = (on ? this.line_color_active : this.line_color_default);
				uilineRenderer.LineThickness = (float)(on ? 4 : 2);
				uilineRenderer.SetAllDirty();
			}
		}
		for (int j = 0; j < this.prerequisiteSkillWidgets.Count; j++)
		{
			this.prerequisiteSkillWidgets[j].ToggleBorderHighlight(on);
		}
	}

	// Token: 0x060060D2 RID: 24786 RVA: 0x002388F7 File Offset: 0x00236AF7
	public string SkillTooltip(Skill skill)
	{
		return "" + SkillWidget.SkillPerksString(skill) + "\n" + this.DuplicantSkillString(skill);
	}

	// Token: 0x060060D3 RID: 24787 RVA: 0x0023891C File Offset: 0x00236B1C
	public static string SkillPerksString(Skill skill)
	{
		string text = "";
		foreach (SkillPerk skillPerk in skill.perks)
		{
			if (!string.IsNullOrEmpty(text))
			{
				text += "\n";
			}
			text = text + "• " + skillPerk.Name;
		}
		return text;
	}

	// Token: 0x060060D4 RID: 24788 RVA: 0x00238998 File Offset: 0x00236B98
	public string CriteriaString(Skill skill)
	{
		bool flag = false;
		string text = "";
		text = text + "<b>" + UI.ROLES_SCREEN.ASSIGNMENT_REQUIREMENTS.TITLE + "</b>\n";
		SkillGroup skillGroup = Db.Get().SkillGroups.Get(skill.skillGroup);
		if (skillGroup != null && skillGroup.relevantAttributes != null)
		{
			foreach (Klei.AI.Attribute attribute in skillGroup.relevantAttributes)
			{
				if (attribute != null)
				{
					text = text + "    • " + string.Format(UI.SKILLS_SCREEN.ASSIGNMENT_REQUIREMENTS.SKILLGROUP_ENABLED.DESCRIPTION, attribute.Name) + "\n";
					flag = true;
				}
			}
		}
		if (skill.priorSkills.Count > 0)
		{
			flag = true;
			for (int i = 0; i < skill.priorSkills.Count; i++)
			{
				text = text + "    • " + string.Format("{0}", Db.Get().Skills.Get(skill.priorSkills[i]).Name);
				text += "</color>";
				if (i != skill.priorSkills.Count - 1)
				{
					text += "\n";
				}
			}
		}
		if (!flag)
		{
			text = text + "    • " + string.Format(UI.ROLES_SCREEN.ASSIGNMENT_REQUIREMENTS.NONE, skill.Name);
		}
		return text;
	}

	// Token: 0x060060D5 RID: 24789 RVA: 0x00238B08 File Offset: 0x00236D08
	public string DuplicantSkillString(Skill skill)
	{
		string text = "";
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.skillsScreen.GetMinionIdentity(this.skillsScreen.CurrentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		if (minionIdentity != null)
		{
			MinionResume component = minionIdentity.GetComponent<MinionResume>();
			if (component == null)
			{
				return "";
			}
			LocString locString = UI.SKILLS_SCREEN.ASSIGNMENT_REQUIREMENTS.MASTERY.CAN_MASTER;
			if (component.HasMasteredSkill(skill.Id))
			{
				if (component.HasBeenGrantedSkill(skill))
				{
					text += "\n";
					locString = UI.SKILLS_SCREEN.ASSIGNMENT_REQUIREMENTS.MASTERY.SKILL_GRANTED;
					text += string.Format(locString, minionIdentity.GetProperName(), skill.Name);
				}
			}
			else
			{
				MinionResume.SkillMasteryConditions[] skillMasteryConditions = component.GetSkillMasteryConditions(skill.Id);
				if (!component.CanMasterSkill(skillMasteryConditions))
				{
					bool flag = false;
					text += "\n";
					locString = UI.SKILLS_SCREEN.ASSIGNMENT_REQUIREMENTS.MASTERY.CANNOT_MASTER;
					text += string.Format(locString, minionIdentity.GetProperName(), skill.Name);
					if (Array.Exists<MinionResume.SkillMasteryConditions>(skillMasteryConditions, (MinionResume.SkillMasteryConditions element) => element == MinionResume.SkillMasteryConditions.UnableToLearn))
					{
						flag = true;
						string choreGroupID = Db.Get().SkillGroups.Get(skill.skillGroup).choreGroupID;
						Trait trait;
						minionIdentity.GetComponent<Traits>().IsChoreGroupDisabled(choreGroupID, out trait);
						text += "\n";
						locString = UI.SKILLS_SCREEN.ASSIGNMENT_REQUIREMENTS.MASTERY.PREVENTED_BY_TRAIT;
						text += string.Format(locString, trait.Name);
					}
					if (!flag)
					{
						if (Array.Exists<MinionResume.SkillMasteryConditions>(skillMasteryConditions, (MinionResume.SkillMasteryConditions element) => element == MinionResume.SkillMasteryConditions.MissingPreviousSkill))
						{
							text += "\n";
							locString = UI.SKILLS_SCREEN.ASSIGNMENT_REQUIREMENTS.MASTERY.REQUIRES_PREVIOUS_SKILLS;
							text += string.Format(locString, Array.Empty<object>());
						}
					}
					if (!flag)
					{
						if (Array.Exists<MinionResume.SkillMasteryConditions>(skillMasteryConditions, (MinionResume.SkillMasteryConditions element) => element == MinionResume.SkillMasteryConditions.NeedsSkillPoints))
						{
							text += "\n";
							locString = UI.SKILLS_SCREEN.ASSIGNMENT_REQUIREMENTS.MASTERY.REQUIRES_MORE_SKILL_POINTS;
							text += string.Format(locString, Array.Empty<object>());
						}
					}
				}
				else
				{
					if (Array.Exists<MinionResume.SkillMasteryConditions>(skillMasteryConditions, (MinionResume.SkillMasteryConditions element) => element == MinionResume.SkillMasteryConditions.StressWarning))
					{
						text += "\n";
						locString = UI.SKILLS_SCREEN.ASSIGNMENT_REQUIREMENTS.MASTERY.STRESS_WARNING_MESSAGE;
						text += string.Format(locString, skill.Name, minionIdentity.GetProperName());
					}
					if (Array.Exists<MinionResume.SkillMasteryConditions>(skillMasteryConditions, (MinionResume.SkillMasteryConditions element) => element == MinionResume.SkillMasteryConditions.SkillAptitude))
					{
						text += "\n";
						locString = UI.SKILLS_SCREEN.ASSIGNMENT_REQUIREMENTS.MASTERY.SKILL_APTITUDE;
						text += string.Format(locString, minionIdentity.GetProperName(), skill.Name);
					}
				}
			}
		}
		return text;
	}

	// Token: 0x060060D6 RID: 24790 RVA: 0x00238DF6 File Offset: 0x00236FF6
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.ToggleBorderHighlight(true);
		this.skillsScreen.HoverSkill(this.skillID);
	}

	// Token: 0x060060D7 RID: 24791 RVA: 0x00238E10 File Offset: 0x00237010
	public void OnPointerExit(PointerEventData eventData)
	{
		this.ToggleBorderHighlight(false);
		this.skillsScreen.HoverSkill(null);
	}

	// Token: 0x060060D8 RID: 24792 RVA: 0x00238E28 File Offset: 0x00237028
	public void OnPointerClick(PointerEventData eventData)
	{
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.skillsScreen.GetMinionIdentity(this.skillsScreen.CurrentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		if (minionIdentity != null)
		{
			MinionResume component = minionIdentity.GetComponent<MinionResume>();
			if (DebugHandler.InstantBuildMode && component.AvailableSkillpoints < 1)
			{
				component.ForceAddSkillPoint();
			}
			MinionResume.SkillMasteryConditions[] skillMasteryConditions = component.GetSkillMasteryConditions(this.skillID);
			bool flag = component.CanMasterSkill(skillMasteryConditions);
			if (component != null && !component.HasMasteredSkill(this.skillID) && flag)
			{
				component.MasterSkill(this.skillID);
				this.skillsScreen.RefreshAll();
			}
		}
	}

	// Token: 0x060060D9 RID: 24793 RVA: 0x00238EC4 File Offset: 0x002370C4
	public void OnPointerDown(PointerEventData eventData)
	{
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.skillsScreen.GetMinionIdentity(this.skillsScreen.CurrentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		MinionResume minionResume = null;
		bool flag = false;
		if (minionIdentity != null)
		{
			minionResume = minionIdentity.GetComponent<MinionResume>();
			MinionResume.SkillMasteryConditions[] skillMasteryConditions = minionResume.GetSkillMasteryConditions(this.skillID);
			flag = minionResume.CanMasterSkill(skillMasteryConditions);
		}
		if (minionResume != null && !minionResume.HasMasteredSkill(this.skillID) && flag)
		{
			KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Click", false));
			return;
		}
		KFMOD.PlayUISound(GlobalAssets.GetSound("Negative", false));
	}

	// Token: 0x0400427B RID: 17019
	[SerializeField]
	private LocText Name;

	// Token: 0x0400427C RID: 17020
	[SerializeField]
	private LocText Description;

	// Token: 0x0400427D RID: 17021
	[SerializeField]
	private Image TitleBarBG;

	// Token: 0x0400427E RID: 17022
	[SerializeField]
	private SkillsScreen skillsScreen;

	// Token: 0x0400427F RID: 17023
	[SerializeField]
	private ToolTip tooltip;

	// Token: 0x04004280 RID: 17024
	[SerializeField]
	private RectTransform lines_left;

	// Token: 0x04004281 RID: 17025
	[SerializeField]
	public RectTransform lines_right;

	// Token: 0x04004282 RID: 17026
	[SerializeField]
	private Color header_color_has_skill;

	// Token: 0x04004283 RID: 17027
	[SerializeField]
	private Color header_color_can_assign;

	// Token: 0x04004284 RID: 17028
	[SerializeField]
	private Color header_color_disabled;

	// Token: 0x04004285 RID: 17029
	[SerializeField]
	private Color line_color_default;

	// Token: 0x04004286 RID: 17030
	[SerializeField]
	private Color line_color_active;

	// Token: 0x04004287 RID: 17031
	[SerializeField]
	private Image hatImage;

	// Token: 0x04004288 RID: 17032
	[SerializeField]
	private GameObject borderHighlight;

	// Token: 0x04004289 RID: 17033
	[SerializeField]
	private ToolTip masteryCount;

	// Token: 0x0400428A RID: 17034
	[SerializeField]
	private GameObject aptitudeBox;

	// Token: 0x0400428B RID: 17035
	[SerializeField]
	private GameObject grantedBox;

	// Token: 0x0400428C RID: 17036
	[SerializeField]
	private GameObject traitDisabledIcon;

	// Token: 0x0400428D RID: 17037
	public TextStyleSetting TooltipTextStyle_Header;

	// Token: 0x0400428E RID: 17038
	public TextStyleSetting TooltipTextStyle_AbilityNegativeModifier;

	// Token: 0x0400428F RID: 17039
	private List<SkillWidget> prerequisiteSkillWidgets = new List<SkillWidget>();

	// Token: 0x04004290 RID: 17040
	private UILineRenderer[] lines;

	// Token: 0x04004291 RID: 17041
	private List<Vector2> linePoints = new List<Vector2>();

	// Token: 0x04004292 RID: 17042
	public Material defaultMaterial;

	// Token: 0x04004293 RID: 17043
	public Material desaturatedMaterial;
}
