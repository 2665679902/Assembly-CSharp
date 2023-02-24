using System;
using System.Collections.Generic;
using Database;
using KSerialization;

// Token: 0x02000581 RID: 1409
[SerializationConfig(MemberSerialization.OptIn)]
public class BuildingFacade : KMonoBehaviour
{
	// Token: 0x170001BB RID: 443
	// (get) Token: 0x06002248 RID: 8776 RVA: 0x000BA062 File Offset: 0x000B8262
	public string CurrentFacade
	{
		get
		{
			return this.currentFacade;
		}
	}

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x06002249 RID: 8777 RVA: 0x000BA06A File Offset: 0x000B826A
	public bool IsOriginal
	{
		get
		{
			return this.currentFacade.IsNullOrWhiteSpace();
		}
	}

	// Token: 0x0600224A RID: 8778 RVA: 0x000BA077 File Offset: 0x000B8277
	protected override void OnPrefabInit()
	{
	}

	// Token: 0x0600224B RID: 8779 RVA: 0x000BA079 File Offset: 0x000B8279
	protected override void OnSpawn()
	{
		if (!this.IsOriginal)
		{
			this.ApplyBuildingFacade(Db.GetBuildingFacades().TryGet(this.currentFacade));
		}
	}

	// Token: 0x0600224C RID: 8780 RVA: 0x000BA09C File Offset: 0x000B829C
	public void ApplyBuildingFacade(BuildingFacadeResource facade)
	{
		if (facade == null)
		{
			this.ClearFacade();
			return;
		}
		this.currentFacade = facade.Id;
		KAnimFile[] array = new KAnimFile[] { Assets.GetAnim(facade.AnimFile) };
		this.ChangeBuilding(array, facade.Name, facade.Description, facade.InteractFile);
	}

	// Token: 0x0600224D RID: 8781 RVA: 0x000BA0F4 File Offset: 0x000B82F4
	private void ClearFacade()
	{
		Building component = base.GetComponent<Building>();
		this.ChangeBuilding(component.Def.AnimFiles, component.Def.Name, component.Def.Desc, null);
	}

	// Token: 0x0600224E RID: 8782 RVA: 0x000BA130 File Offset: 0x000B8330
	private void ChangeBuilding(KAnimFile[] animFiles, string displayName, string desc, Dictionary<string, string> interactAnimsNames = null)
	{
		this.interactAnims.Clear();
		if (interactAnimsNames != null && interactAnimsNames.Count > 0)
		{
			this.interactAnims = new Dictionary<string, KAnimFile[]>();
			foreach (KeyValuePair<string, string> keyValuePair in interactAnimsNames)
			{
				this.interactAnims.Add(keyValuePair.Key, new KAnimFile[] { Assets.GetAnim(keyValuePair.Value) });
			}
		}
		Building[] components = base.GetComponents<Building>();
		foreach (Building building in components)
		{
			building.SetDescription(desc);
			building.GetComponent<KBatchedAnimController>().SwapAnims(animFiles);
		}
		base.GetComponent<KSelectable>().SetName(displayName);
		if (base.GetComponent<AnimTileable>() != null && components.Length != 0)
		{
			GameScenePartitioner.Instance.TriggerEvent(components[0].GetExtents(), GameScenePartitioner.Instance.objectLayers[1], null);
		}
	}

	// Token: 0x0600224F RID: 8783 RVA: 0x000BA238 File Offset: 0x000B8438
	public string GetNextFacade()
	{
		BuildingDef def = base.GetComponent<Building>().Def;
		int num = def.AvailableFacades.FindIndex((string s) => s == this.currentFacade) + 1;
		if (num >= def.AvailableFacades.Count)
		{
			num = 0;
		}
		return def.AvailableFacades[num];
	}

	// Token: 0x040013CD RID: 5069
	[Serialize]
	private string currentFacade;

	// Token: 0x040013CE RID: 5070
	public KAnimFile[] animFiles;

	// Token: 0x040013CF RID: 5071
	public Dictionary<string, KAnimFile[]> interactAnims = new Dictionary<string, KAnimFile[]>();
}
