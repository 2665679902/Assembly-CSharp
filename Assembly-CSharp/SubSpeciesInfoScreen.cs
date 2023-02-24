using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C08 RID: 3080
public class SubSpeciesInfoScreen : KModalScreen
{
	// Token: 0x060061A5 RID: 24997 RVA: 0x00241038 File Offset: 0x0023F238
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x060061A6 RID: 24998 RVA: 0x0024103B File Offset: 0x0023F23B
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060061A7 RID: 24999 RVA: 0x00241044 File Offset: 0x0023F244
	private void ClearMutations()
	{
		for (int i = this.mutationLineItems.Count - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.mutationLineItems[i]);
		}
		this.mutationLineItems.Clear();
	}

	// Token: 0x060061A8 RID: 25000 RVA: 0x00241085 File Offset: 0x0023F285
	public void DisplayDiscovery(Tag speciesID, Tag subSpeciesID, GeneticAnalysisStation station)
	{
		this.SetSubspecies(speciesID, subSpeciesID);
		this.targetStation = station;
	}

	// Token: 0x060061A9 RID: 25001 RVA: 0x00241098 File Offset: 0x0023F298
	private void SetSubspecies(Tag speciesID, Tag subSpeciesID)
	{
		this.ClearMutations();
		ref PlantSubSpeciesCatalog.SubSpeciesInfo subSpecies = PlantSubSpeciesCatalog.Instance.GetSubSpecies(speciesID, subSpeciesID);
		this.plantIcon.sprite = Def.GetUISprite(Assets.GetPrefab(speciesID), "ui", false).first;
		foreach (string text in subSpecies.mutationIDs)
		{
			PlantMutation plantMutation = Db.Get().PlantMutations.Get(text);
			GameObject gameObject = Util.KInstantiateUI(this.mutationsItemPrefab, this.mutationsList.gameObject, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("nameLabel").text = plantMutation.Name;
			component.GetReference<LocText>("descriptionLabel").text = plantMutation.description;
			this.mutationLineItems.Add(gameObject);
		}
	}

	// Token: 0x0400437F RID: 17279
	[SerializeField]
	private KButton renameButton;

	// Token: 0x04004380 RID: 17280
	[SerializeField]
	private KButton saveButton;

	// Token: 0x04004381 RID: 17281
	[SerializeField]
	private KButton discardButton;

	// Token: 0x04004382 RID: 17282
	[SerializeField]
	private RectTransform mutationsList;

	// Token: 0x04004383 RID: 17283
	[SerializeField]
	private Image plantIcon;

	// Token: 0x04004384 RID: 17284
	[SerializeField]
	private GameObject mutationsItemPrefab;

	// Token: 0x04004385 RID: 17285
	private List<GameObject> mutationLineItems = new List<GameObject>();

	// Token: 0x04004386 RID: 17286
	private GeneticAnalysisStation targetStation;
}
