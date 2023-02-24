using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000BF6 RID: 3062
[AddComponentMenu("KMonoBehaviour/scripts/SkillMinionWidget")]
public class SkillMinionWidget : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x170006B3 RID: 1715
	// (get) Token: 0x060060BE RID: 24766 RVA: 0x00237A2B File Offset: 0x00235C2B
	// (set) Token: 0x060060BF RID: 24767 RVA: 0x00237A33 File Offset: 0x00235C33
	public IAssignableIdentity assignableIdentity { get; private set; }

	// Token: 0x060060C0 RID: 24768 RVA: 0x00237A3C File Offset: 0x00235C3C
	public void SetMinon(IAssignableIdentity identity)
	{
		this.assignableIdentity = identity;
		this.portrait.SetIdentityObject(this.assignableIdentity, true);
		base.GetComponent<NotificationHighlightTarget>().targetKey = identity.GetSoleOwner().gameObject.GetInstanceID().ToString();
	}

	// Token: 0x060060C1 RID: 24769 RVA: 0x00237A85 File Offset: 0x00235C85
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.ToggleHover(true);
	}

	// Token: 0x060060C2 RID: 24770 RVA: 0x00237A8E File Offset: 0x00235C8E
	public void OnPointerExit(PointerEventData eventData)
	{
		this.ToggleHover(false);
	}

	// Token: 0x060060C3 RID: 24771 RVA: 0x00237A97 File Offset: 0x00235C97
	private void ToggleHover(bool on)
	{
		if (this.skillsScreen.CurrentlySelectedMinion != this.assignableIdentity)
		{
			this.SetColor(on ? this.hover_color : this.unselected_color);
		}
	}

	// Token: 0x060060C4 RID: 24772 RVA: 0x00237AC3 File Offset: 0x00235CC3
	private void SetColor(Color color)
	{
		this.background.color = color;
		if (this.assignableIdentity != null && this.assignableIdentity as StoredMinionIdentity != null)
		{
			base.GetComponent<CanvasGroup>().alpha = 0.6f;
		}
	}

	// Token: 0x060060C5 RID: 24773 RVA: 0x00237AFC File Offset: 0x00235CFC
	public void OnPointerClick(PointerEventData eventData)
	{
		this.skillsScreen.CurrentlySelectedMinion = this.assignableIdentity;
		base.GetComponent<NotificationHighlightTarget>().View();
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Click", false));
	}

	// Token: 0x060060C6 RID: 24774 RVA: 0x00237B2C File Offset: 0x00235D2C
	public void Refresh()
	{
		if (this.assignableIdentity.IsNullOrDestroyed())
		{
			return;
		}
		this.portrait.SetIdentityObject(this.assignableIdentity, true);
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.skillsScreen.GetMinionIdentity(this.assignableIdentity, out minionIdentity, out storedMinionIdentity);
		this.hatDropDown.gameObject.SetActive(true);
		string text;
		if (minionIdentity != null)
		{
			MinionResume component = minionIdentity.GetComponent<MinionResume>();
			int availableSkillpoints = component.AvailableSkillpoints;
			int totalSkillPointsGained = component.TotalSkillPointsGained;
			this.masteryPoints.text = ((availableSkillpoints > 0) ? GameUtil.ApplyBoldString(GameUtil.ColourizeString(new Color(0.5f, 1f, 0.5f, 1f), availableSkillpoints.ToString())) : "0");
			AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup(component);
			AttributeInstance attributeInstance2 = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(component);
			this.morale.text = string.Format("{0}/{1}", attributeInstance.GetTotalValue(), attributeInstance2.GetTotalValue());
			this.RefreshToolTip(component);
			List<IListableOption> list = new List<IListableOption>();
			foreach (KeyValuePair<string, bool> keyValuePair in component.MasteryBySkillID)
			{
				if (keyValuePair.Value)
				{
					list.Add(new SkillListable(keyValuePair.Key));
				}
			}
			this.hatDropDown.Initialize(list, new Action<IListableOption, object>(this.OnHatDropEntryClick), new Func<IListableOption, IListableOption, object, int>(this.hatDropDownSort), new Action<DropDownEntry, object>(this.hatDropEntryRefreshAction), false, minionIdentity);
			text = (string.IsNullOrEmpty(component.TargetHat) ? component.CurrentHat : component.TargetHat);
		}
		else
		{
			ToolTip component2 = base.GetComponent<ToolTip>();
			component2.ClearMultiStringTooltip();
			component2.AddMultiStringTooltip(string.Format(UI.TABLESCREENS.INFORMATION_NOT_AVAILABLE_TOOLTIP, storedMinionIdentity.GetStorageReason(), storedMinionIdentity.GetProperName()), null);
			text = (string.IsNullOrEmpty(storedMinionIdentity.targetHat) ? storedMinionIdentity.currentHat : storedMinionIdentity.targetHat);
			this.masteryPoints.text = UI.TABLESCREENS.NA;
			this.morale.text = UI.TABLESCREENS.NA;
		}
		bool flag = this.skillsScreen.CurrentlySelectedMinion == this.assignableIdentity;
		if (this.skillsScreen.CurrentlySelectedMinion != null && this.assignableIdentity != null)
		{
			flag = flag || this.skillsScreen.CurrentlySelectedMinion.GetSoleOwner() == this.assignableIdentity.GetSoleOwner();
		}
		this.SetColor(flag ? this.selected_color : this.unselected_color);
		HierarchyReferences component3 = base.GetComponent<HierarchyReferences>();
		this.RefreshHat(text);
		component3.GetReference("openButton").gameObject.SetActive(minionIdentity != null);
	}

	// Token: 0x060060C7 RID: 24775 RVA: 0x00237E10 File Offset: 0x00236010
	private void RefreshToolTip(MinionResume resume)
	{
		if (resume != null)
		{
			AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup(resume);
			AttributeInstance attributeInstance2 = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(resume);
			ToolTip component = base.GetComponent<ToolTip>();
			component.ClearMultiStringTooltip();
			component.AddMultiStringTooltip(this.assignableIdentity.GetProperName() + "\n\n", this.TooltipTextStyle_Header);
			component.AddMultiStringTooltip(string.Format(UI.SKILLS_SCREEN.CURRENT_MORALE, attributeInstance.GetTotalValue(), attributeInstance2.GetTotalValue()), null);
			component.AddMultiStringTooltip("\n" + UI.DETAILTABS.STATS.NAME + "\n\n", this.TooltipTextStyle_Header);
			foreach (AttributeInstance attributeInstance3 in resume.GetAttributes())
			{
				if (attributeInstance3.Attribute.ShowInUI == Klei.AI.Attribute.Display.Skill)
				{
					string text = UIConstants.ColorPrefixWhite;
					if (attributeInstance3.GetTotalValue() > 0f)
					{
						text = UIConstants.ColorPrefixGreen;
					}
					else if (attributeInstance3.GetTotalValue() < 0f)
					{
						text = UIConstants.ColorPrefixRed;
					}
					component.AddMultiStringTooltip(string.Concat(new string[]
					{
						"    • ",
						attributeInstance3.Name,
						": ",
						text,
						attributeInstance3.GetTotalValue().ToString(),
						UIConstants.ColorSuffix
					}), null);
				}
			}
		}
	}

	// Token: 0x060060C8 RID: 24776 RVA: 0x00237FA4 File Offset: 0x002361A4
	public void RefreshHat(string hat)
	{
		base.GetComponent<HierarchyReferences>().GetReference("selectedHat").GetComponent<Image>()
			.sprite = Assets.GetSprite(string.IsNullOrEmpty(hat) ? "hat_role_none" : hat);
	}

	// Token: 0x060060C9 RID: 24777 RVA: 0x00237FDC File Offset: 0x002361DC
	private void OnHatDropEntryClick(IListableOption skill, object data)
	{
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.skillsScreen.GetMinionIdentity(this.assignableIdentity, out minionIdentity, out storedMinionIdentity);
		if (minionIdentity == null)
		{
			return;
		}
		MinionResume component = minionIdentity.GetComponent<MinionResume>();
		if (skill != null)
		{
			base.GetComponent<HierarchyReferences>().GetReference("selectedHat").GetComponent<Image>()
				.sprite = Assets.GetSprite((skill as SkillListable).skillHat);
			if (component != null)
			{
				string skillHat = (skill as SkillListable).skillHat;
				component.SetHats(component.CurrentHat, skillHat);
				if (component.OwnsHat(skillHat))
				{
					new PutOnHatChore(component, Db.Get().ChoreTypes.SwitchHat);
				}
			}
		}
		else
		{
			base.GetComponent<HierarchyReferences>().GetReference("selectedHat").GetComponent<Image>()
				.sprite = Assets.GetSprite("hat_role_none");
			if (component != null)
			{
				component.SetHats(component.CurrentHat, null);
				component.ApplyTargetHat();
			}
		}
		this.skillsScreen.RefreshAll();
	}

	// Token: 0x060060CA RID: 24778 RVA: 0x002380D4 File Offset: 0x002362D4
	private void hatDropEntryRefreshAction(DropDownEntry entry, object targetData)
	{
		if (entry.entryData != null)
		{
			SkillListable skillListable = entry.entryData as SkillListable;
			entry.image.sprite = Assets.GetSprite(skillListable.skillHat);
		}
	}

	// Token: 0x060060CB RID: 24779 RVA: 0x00238110 File Offset: 0x00236310
	private int hatDropDownSort(IListableOption a, IListableOption b, object targetData)
	{
		return 0;
	}

	// Token: 0x0400426E RID: 17006
	[SerializeField]
	private SkillsScreen skillsScreen;

	// Token: 0x0400426F RID: 17007
	[SerializeField]
	private CrewPortrait portrait;

	// Token: 0x04004270 RID: 17008
	[SerializeField]
	private LocText masteryPoints;

	// Token: 0x04004271 RID: 17009
	[SerializeField]
	private LocText morale;

	// Token: 0x04004272 RID: 17010
	[SerializeField]
	private Image background;

	// Token: 0x04004273 RID: 17011
	[SerializeField]
	private Image hat_background;

	// Token: 0x04004274 RID: 17012
	[SerializeField]
	private Color selected_color;

	// Token: 0x04004275 RID: 17013
	[SerializeField]
	private Color unselected_color;

	// Token: 0x04004276 RID: 17014
	[SerializeField]
	private Color hover_color;

	// Token: 0x04004277 RID: 17015
	[SerializeField]
	private DropDown hatDropDown;

	// Token: 0x04004278 RID: 17016
	[SerializeField]
	private TextStyleSetting TooltipTextStyle_Header;

	// Token: 0x04004279 RID: 17017
	[SerializeField]
	private TextStyleSetting TooltipTextStyle_AbilityNegativeModifier;
}
