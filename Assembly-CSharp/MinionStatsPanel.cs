using System;
using System.Collections.Generic;
using Database;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000B2E RID: 2862
public class MinionStatsPanel : TargetScreen
{
	// Token: 0x0600585B RID: 22619 RVA: 0x001FFC9E File Offset: 0x001FDE9E
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<MinionIdentity>();
	}

	// Token: 0x0600585C RID: 22620 RVA: 0x001FFCAC File Offset: 0x001FDEAC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.resumePanel = Util.KInstantiateUI(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		this.attributesPanel = Util.KInstantiateUI(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		this.resumeDrawer = new DetailsPanelDrawer(this.attributesLabelTemplate, this.resumePanel.GetComponent<CollapsibleDetailContentPanel>().Content.gameObject);
		this.attributesDrawer = new DetailsPanelDrawer(this.attributesLabelTemplate, this.attributesPanel.GetComponent<CollapsibleDetailContentPanel>().Content.gameObject);
	}

	// Token: 0x0600585D RID: 22621 RVA: 0x001FFD43 File Offset: 0x001FDF43
	protected override void OnCleanUp()
	{
		this.updateHandle.ClearScheduler();
		base.OnCleanUp();
	}

	// Token: 0x0600585E RID: 22622 RVA: 0x001FFD56 File Offset: 0x001FDF56
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Refresh();
		this.ScheduleUpdate();
	}

	// Token: 0x0600585F RID: 22623 RVA: 0x001FFD6A File Offset: 0x001FDF6A
	public override void OnSelectTarget(GameObject target)
	{
		base.OnSelectTarget(target);
		this.Refresh();
	}

	// Token: 0x06005860 RID: 22624 RVA: 0x001FFD79 File Offset: 0x001FDF79
	private void ScheduleUpdate()
	{
		this.updateHandle = UIScheduler.Instance.Schedule("RefreshMinionStatsPanel", 1f, delegate(object o)
		{
			this.Refresh();
			this.ScheduleUpdate();
		}, null, null);
	}

	// Token: 0x06005861 RID: 22625 RVA: 0x001FFDA4 File Offset: 0x001FDFA4
	private GameObject AddOrGetLabel(Dictionary<string, GameObject> labels, GameObject panel, string id)
	{
		GameObject gameObject;
		if (labels.ContainsKey(id))
		{
			gameObject = labels[id];
		}
		else
		{
			gameObject = Util.KInstantiate(this.attributesLabelTemplate, panel.GetComponent<CollapsibleDetailContentPanel>().Content.gameObject, null);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			labels[id] = gameObject;
		}
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06005862 RID: 22626 RVA: 0x001FFE12 File Offset: 0x001FE012
	private void Refresh()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (this.selectedTarget == null || this.selectedTarget.GetComponent<MinionIdentity>() == null)
		{
			return;
		}
		this.RefreshResume();
		this.RefreshAttributes();
	}

	// Token: 0x06005863 RID: 22627 RVA: 0x001FFE50 File Offset: 0x001FE050
	private void RefreshAttributes()
	{
		if (!this.selectedTarget.GetComponent<MinionIdentity>())
		{
			this.attributesPanel.SetActive(false);
			return;
		}
		this.attributesPanel.SetActive(true);
		this.attributesPanel.GetComponent<CollapsibleDetailContentPanel>().HeaderLabel.text = UI.DETAILTABS.STATS.GROUPNAME_ATTRIBUTES;
		List<AttributeInstance> list = new List<AttributeInstance>(this.selectedTarget.GetAttributes().AttributeTable).FindAll((AttributeInstance a) => a.Attribute.ShowInUI == Klei.AI.Attribute.Display.Skill);
		this.attributesDrawer.BeginDrawing();
		if (list.Count > 0)
		{
			foreach (AttributeInstance attributeInstance in list)
			{
				this.attributesDrawer.NewLabel(string.Format("{0}: {1}", attributeInstance.Name, attributeInstance.GetFormattedValue())).Tooltip(attributeInstance.GetAttributeValueTooltip());
			}
		}
		this.attributesDrawer.EndDrawing();
	}

	// Token: 0x06005864 RID: 22628 RVA: 0x001FFF6C File Offset: 0x001FE16C
	private void RefreshResume()
	{
		MinionResume component = this.selectedTarget.GetComponent<MinionResume>();
		if (!component)
		{
			this.resumePanel.SetActive(false);
			return;
		}
		this.resumePanel.SetActive(true);
		this.resumePanel.GetComponent<CollapsibleDetailContentPanel>().HeaderLabel.text = string.Format(UI.DETAILTABS.PERSONALITY.GROUPNAME_RESUME, this.selectedTarget.name.ToUpper());
		this.resumeDrawer.BeginDrawing();
		List<Skill> list = new List<Skill>();
		foreach (KeyValuePair<string, bool> keyValuePair in component.MasteryBySkillID)
		{
			if (keyValuePair.Value)
			{
				Skill skill = Db.Get().Skills.Get(keyValuePair.Key);
				list.Add(skill);
			}
		}
		this.resumeDrawer.NewLabel(UI.DETAILTABS.PERSONALITY.RESUME.MASTERED_SKILLS).Tooltip(UI.DETAILTABS.PERSONALITY.RESUME.MASTERED_SKILLS_TOOLTIP);
		if (list.Count == 0)
		{
			this.resumeDrawer.NewLabel("  • " + UI.DETAILTABS.PERSONALITY.RESUME.NO_MASTERED_SKILLS.NAME).Tooltip(string.Format(UI.DETAILTABS.PERSONALITY.RESUME.NO_MASTERED_SKILLS.TOOLTIP, this.selectedTarget.name));
		}
		else
		{
			foreach (Skill skill2 in list)
			{
				string text = "";
				foreach (SkillPerk skillPerk in skill2.perks)
				{
					text = text + "  • " + skillPerk.Name + "\n";
				}
				this.resumeDrawer.NewLabel("  • " + skill2.Name).Tooltip(skill2.description + "\n" + text);
			}
		}
		this.resumeDrawer.EndDrawing();
	}

	// Token: 0x04003BC8 RID: 15304
	public GameObject attributesLabelTemplate;

	// Token: 0x04003BC9 RID: 15305
	private GameObject resumePanel;

	// Token: 0x04003BCA RID: 15306
	private GameObject attributesPanel;

	// Token: 0x04003BCB RID: 15307
	private DetailsPanelDrawer resumeDrawer;

	// Token: 0x04003BCC RID: 15308
	private DetailsPanelDrawer attributesDrawer;

	// Token: 0x04003BCD RID: 15309
	private SchedulerHandle updateHandle;
}
