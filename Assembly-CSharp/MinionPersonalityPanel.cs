using System;
using System.Collections.Generic;
using Database;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000B2D RID: 2861
public class MinionPersonalityPanel : TargetScreen
{
	// Token: 0x0600584C RID: 22604 RVA: 0x001FF6F0 File Offset: 0x001FD8F0
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<MinionIdentity>() != null;
	}

	// Token: 0x0600584D RID: 22605 RVA: 0x001FF6FE File Offset: 0x001FD8FE
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
	}

	// Token: 0x0600584E RID: 22606 RVA: 0x001FF707 File Offset: 0x001FD907
	public override void OnSelectTarget(GameObject target)
	{
		this.panel.SetSelectedMinion(target);
		this.panel.Refresh(null);
		base.OnSelectTarget(target);
		this.Refresh();
	}

	// Token: 0x0600584F RID: 22607 RVA: 0x001FF72E File Offset: 0x001FD92E
	public override void OnDeselectTarget(GameObject target)
	{
	}

	// Token: 0x06005850 RID: 22608 RVA: 0x001FF730 File Offset: 0x001FD930
	protected override void OnActivate()
	{
		base.OnActivate();
		if (this.panel == null)
		{
			this.panel = base.GetComponent<MinionEquipmentPanel>();
		}
	}

	// Token: 0x06005851 RID: 22609 RVA: 0x001FF754 File Offset: 0x001FD954
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.bioPanel = Util.KInstantiateUI(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		this.traitsPanel = Util.KInstantiateUI(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		this.bioDrawer = new DetailsPanelDrawer(this.attributesLabelTemplate, this.bioPanel.GetComponent<CollapsibleDetailContentPanel>().Content.gameObject);
		this.traitsDrawer = new DetailsPanelDrawer(this.attributesLabelTemplate, this.traitsPanel.GetComponent<CollapsibleDetailContentPanel>().Content.gameObject);
	}

	// Token: 0x06005852 RID: 22610 RVA: 0x001FF7EB File Offset: 0x001FD9EB
	protected override void OnCleanUp()
	{
		this.updateHandle.ClearScheduler();
		base.OnCleanUp();
	}

	// Token: 0x06005853 RID: 22611 RVA: 0x001FF7FE File Offset: 0x001FD9FE
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.panel == null)
		{
			this.panel = base.GetComponent<MinionEquipmentPanel>();
		}
		this.Refresh();
		this.ScheduleUpdate();
	}

	// Token: 0x06005854 RID: 22612 RVA: 0x001FF82C File Offset: 0x001FDA2C
	private void ScheduleUpdate()
	{
		this.updateHandle = UIScheduler.Instance.Schedule("RefreshMinionPersonalityPanel", 1f, delegate(object o)
		{
			this.Refresh();
			this.ScheduleUpdate();
		}, null, null);
	}

	// Token: 0x06005855 RID: 22613 RVA: 0x001FF858 File Offset: 0x001FDA58
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

	// Token: 0x06005856 RID: 22614 RVA: 0x001FF8C6 File Offset: 0x001FDAC6
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
		this.RefreshBio();
		this.RefreshTraits();
	}

	// Token: 0x06005857 RID: 22615 RVA: 0x001FF904 File Offset: 0x001FDB04
	private void RefreshBio()
	{
		MinionIdentity component = this.selectedTarget.GetComponent<MinionIdentity>();
		if (!component)
		{
			this.bioPanel.SetActive(false);
			return;
		}
		this.bioPanel.SetActive(true);
		this.bioPanel.GetComponent<CollapsibleDetailContentPanel>().HeaderLabel.text = UI.DETAILTABS.PERSONALITY.GROUPNAME_BIO;
		this.bioDrawer.BeginDrawing().NewLabel(DUPLICANTS.NAMETITLE + component.name).NewLabel(DUPLICANTS.ARRIVALTIME + GameUtil.GetFormattedCycles(((float)GameClock.Instance.GetCycle() - component.arrivalTime) * 600f, "F0", true))
			.Tooltip(string.Format(DUPLICANTS.ARRIVALTIME_TOOLTIP, component.arrivalTime + 1f, component.name))
			.NewLabel(DUPLICANTS.GENDERTITLE + string.Format(Strings.Get(string.Format("STRINGS.DUPLICANTS.GENDER.{0}.NAME", component.genderStringKey.ToUpper())), component.gender))
			.NewLabel(string.Format(Strings.Get(string.Format("STRINGS.DUPLICANTS.PERSONALITIES.{0}.DESC", component.nameStringKey.ToUpper())), component.name))
			.Tooltip(string.Format(Strings.Get(string.Format("STRINGS.DUPLICANTS.DESC_TOOLTIP", component.nameStringKey.ToUpper())), component.name));
		MinionResume component2 = this.selectedTarget.GetComponent<MinionResume>();
		if (component2 != null && component2.AptitudeBySkillGroup.Count > 0)
		{
			this.bioDrawer.NewLabel(UI.DETAILTABS.PERSONALITY.RESUME.APTITUDES.NAME + "\n").Tooltip(string.Format(UI.DETAILTABS.PERSONALITY.RESUME.APTITUDES.TOOLTIP, this.selectedTarget.name));
			foreach (KeyValuePair<HashedString, float> keyValuePair in component2.AptitudeBySkillGroup)
			{
				if (keyValuePair.Value != 0f)
				{
					SkillGroup skillGroup = Db.Get().SkillGroups.TryGet(keyValuePair.Key);
					if (skillGroup != null)
					{
						this.bioDrawer.NewLabel("  • " + skillGroup.Name).Tooltip(string.Format(DUPLICANTS.ROLES.GROUPS.APTITUDE_DESCRIPTION, skillGroup.Name, keyValuePair.Value));
					}
				}
			}
		}
		this.bioDrawer.EndDrawing();
	}

	// Token: 0x06005858 RID: 22616 RVA: 0x001FFBA4 File Offset: 0x001FDDA4
	private void RefreshTraits()
	{
		if (!this.selectedTarget.GetComponent<MinionIdentity>())
		{
			this.traitsPanel.SetActive(false);
			return;
		}
		this.traitsPanel.SetActive(true);
		this.traitsPanel.GetComponent<CollapsibleDetailContentPanel>().HeaderLabel.text = UI.DETAILTABS.STATS.GROUPNAME_TRAITS;
		this.traitsDrawer.BeginDrawing();
		foreach (Trait trait in this.selectedTarget.GetComponent<Traits>().TraitList)
		{
			if (!string.IsNullOrEmpty(trait.Name))
			{
				this.traitsDrawer.NewLabel(trait.Name).Tooltip(trait.GetTooltip());
			}
		}
		this.traitsDrawer.EndDrawing();
	}

	// Token: 0x04003BC1 RID: 15297
	public GameObject attributesLabelTemplate;

	// Token: 0x04003BC2 RID: 15298
	private GameObject bioPanel;

	// Token: 0x04003BC3 RID: 15299
	private GameObject traitsPanel;

	// Token: 0x04003BC4 RID: 15300
	private DetailsPanelDrawer bioDrawer;

	// Token: 0x04003BC5 RID: 15301
	private DetailsPanelDrawer traitsDrawer;

	// Token: 0x04003BC6 RID: 15302
	public MinionEquipmentPanel panel;

	// Token: 0x04003BC7 RID: 15303
	private SchedulerHandle updateHandle;
}
