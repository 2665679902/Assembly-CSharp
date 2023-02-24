using System;
using System.Collections.Generic;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BB6 RID: 2998
public class GeneticAnalysisStationSideScreen : SideScreenContent
{
	// Token: 0x06005E3D RID: 24125 RVA: 0x00226F7C File Offset: 0x0022517C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Refresh();
	}

	// Token: 0x06005E3E RID: 24126 RVA: 0x00226F8A File Offset: 0x0022518A
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetSMI<GeneticAnalysisStation.StatesInstance>() != null;
	}

	// Token: 0x06005E3F RID: 24127 RVA: 0x00226F95 File Offset: 0x00225195
	public override void SetTarget(GameObject target)
	{
		this.target = target.GetSMI<GeneticAnalysisStation.StatesInstance>();
		target.GetComponent<GeneticAnalysisStationWorkable>();
		this.Refresh();
	}

	// Token: 0x06005E40 RID: 24128 RVA: 0x00226FB0 File Offset: 0x002251B0
	private void Refresh()
	{
		if (this.target == null)
		{
			return;
		}
		this.DrawPickerMenu();
	}

	// Token: 0x06005E41 RID: 24129 RVA: 0x00226FC4 File Offset: 0x002251C4
	private void DrawPickerMenu()
	{
		Dictionary<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>> dictionary = new Dictionary<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>>();
		foreach (Tag tag in PlantSubSpeciesCatalog.Instance.GetAllDiscoveredSpecies())
		{
			dictionary[tag] = new List<PlantSubSpeciesCatalog.SubSpeciesInfo>(PlantSubSpeciesCatalog.Instance.GetAllSubSpeciesForSpecies(tag));
		}
		int num = 0;
		foreach (KeyValuePair<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>> keyValuePair in dictionary)
		{
			if (PlantSubSpeciesCatalog.Instance.GetAllSubSpeciesForSpecies(keyValuePair.Key).Count > 1)
			{
				GameObject prefab = Assets.GetPrefab(keyValuePair.Key);
				if (!(prefab == null))
				{
					SeedProducer component = prefab.GetComponent<SeedProducer>();
					if (!(component == null))
					{
						Tag tag2 = component.seedInfo.seedId.ToTag();
						if (DiscoveredResources.Instance.IsDiscovered(tag2))
						{
							HierarchyReferences hierarchyReferences;
							if (num < this.rows.Count)
							{
								hierarchyReferences = this.rows[num];
							}
							else
							{
								hierarchyReferences = Util.KInstantiateUI<HierarchyReferences>(this.rowPrefab.gameObject, this.rowContainer, false);
								this.rows.Add(hierarchyReferences);
							}
							this.ConfigureButton(hierarchyReferences, keyValuePair.Key);
							this.rows[num].gameObject.SetActive(true);
							num++;
						}
					}
				}
			}
		}
		for (int i = num; i < this.rows.Count; i++)
		{
			this.rows[i].gameObject.SetActive(false);
		}
		if (num == 0)
		{
			this.message.text = UI.UISIDESCREENS.GENETICANALYSISSIDESCREEN.NONE_DISCOVERED;
			this.contents.gameObject.SetActive(false);
			return;
		}
		this.message.text = UI.UISIDESCREENS.GENETICANALYSISSIDESCREEN.SELECT_SEEDS;
		this.contents.gameObject.SetActive(true);
	}

	// Token: 0x06005E42 RID: 24130 RVA: 0x002271D0 File Offset: 0x002253D0
	private void ConfigureButton(HierarchyReferences button, Tag speciesID)
	{
		TMP_Text reference = button.GetReference<LocText>("Label");
		Image reference2 = button.GetReference<Image>("Icon");
		LocText reference3 = button.GetReference<LocText>("ProgressLabel");
		button.GetReference<ToolTip>("ToolTip");
		Tag seedID = this.GetSeedIDFromPlantID(speciesID);
		bool isForbidden = this.target.GetSeedForbidden(seedID);
		reference.text = seedID.ProperName();
		reference2.sprite = Def.GetUISprite(seedID, "ui", false).first;
		if (PlantSubSpeciesCatalog.Instance.GetAllSubSpeciesForSpecies(speciesID).Count > 0)
		{
			reference3.text = (isForbidden ? UI.UISIDESCREENS.GENETICANALYSISSIDESCREEN.SEED_FORBIDDEN : UI.UISIDESCREENS.GENETICANALYSISSIDESCREEN.SEED_ALLOWED);
		}
		else
		{
			reference3.text = UI.UISIDESCREENS.GENETICANALYSISSIDESCREEN.SEED_NO_MUTANTS;
		}
		KToggle component = button.GetComponent<KToggle>();
		component.isOn = !isForbidden;
		component.ClearOnClick();
		component.onClick += delegate
		{
			this.target.SetSeedForbidden(seedID, !isForbidden);
			this.Refresh();
		};
	}

	// Token: 0x06005E43 RID: 24131 RVA: 0x002272DE File Offset: 0x002254DE
	private Tag GetSeedIDFromPlantID(Tag speciesID)
	{
		return Assets.GetPrefab(speciesID).GetComponent<SeedProducer>().seedInfo.seedId;
	}

	// Token: 0x04004079 RID: 16505
	[SerializeField]
	private LocText message;

	// Token: 0x0400407A RID: 16506
	[SerializeField]
	private GameObject contents;

	// Token: 0x0400407B RID: 16507
	[SerializeField]
	private GameObject rowContainer;

	// Token: 0x0400407C RID: 16508
	[SerializeField]
	private HierarchyReferences rowPrefab;

	// Token: 0x0400407D RID: 16509
	private List<HierarchyReferences> rows = new List<HierarchyReferences>();

	// Token: 0x0400407E RID: 16510
	private GeneticAnalysisStation.StatesInstance target;

	// Token: 0x0400407F RID: 16511
	private Dictionary<Tag, bool> expandedSeeds = new Dictionary<Tag, bool>();
}
