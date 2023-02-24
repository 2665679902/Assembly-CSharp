using System;
using System.Collections.Generic;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A9A RID: 2714
public class FacadeSelectionPanel : KMonoBehaviour
{
	// Token: 0x1700062E RID: 1582
	// (get) Token: 0x06005335 RID: 21301 RVA: 0x001E3252 File Offset: 0x001E1452
	public string SelectedBuildingDefID
	{
		get
		{
			return this.selectedBuildingDefID;
		}
	}

	// Token: 0x1700062F RID: 1583
	// (get) Token: 0x06005336 RID: 21302 RVA: 0x001E325A File Offset: 0x001E145A
	// (set) Token: 0x06005337 RID: 21303 RVA: 0x001E3262 File Offset: 0x001E1462
	public string SelectedFacade
	{
		get
		{
			return this._selectedFacade;
		}
		set
		{
			if (this._selectedFacade != value)
			{
				this._selectedFacade = value;
				this.RefreshToggles();
				if (this.OnFacadeSelectionChanged != null)
				{
					this.OnFacadeSelectionChanged();
				}
			}
		}
	}

	// Token: 0x06005338 RID: 21304 RVA: 0x001E3292 File Offset: 0x001E1492
	public void SetBuildingDef(string defID)
	{
		this.ClearToggles();
		this.selectedBuildingDefID = defID;
		this.SelectedFacade = "DEFAULT_FACADE";
		this.RefreshToggles();
		base.gameObject.SetActive(Assets.GetBuildingDef(defID).AvailableFacades.Count != 0);
	}

	// Token: 0x06005339 RID: 21305 RVA: 0x001E32D0 File Offset: 0x001E14D0
	private void ClearToggles()
	{
		foreach (KeyValuePair<string, FacadeSelectionPanel.FacadeToggle> keyValuePair in this.activeFacadeToggles)
		{
			this.pooledFacadeToggles.Add(keyValuePair.Value.gameObject);
			keyValuePair.Value.gameObject.SetActive(false);
		}
		this.activeFacadeToggles.Clear();
	}

	// Token: 0x0600533A RID: 21306 RVA: 0x001E3358 File Offset: 0x001E1558
	private void RefreshToggles()
	{
		this.AddDefaultFacadeToggle();
		foreach (string text in Assets.GetBuildingDef(this.selectedBuildingDefID).AvailableFacades)
		{
			PermitResource permitResource = Db.Get().Permits.TryGet(text);
			if (permitResource != null && permitResource.IsUnlocked())
			{
				this.AddNewToggle(text);
			}
		}
		foreach (KeyValuePair<string, FacadeSelectionPanel.FacadeToggle> keyValuePair in this.activeFacadeToggles)
		{
			keyValuePair.Value.multiToggle.ChangeState((this.SelectedFacade == keyValuePair.Key) ? 1 : 0);
		}
		this.activeFacadeToggles["DEFAULT_FACADE"].gameObject.transform.SetAsFirstSibling();
		this.storeButton.gameObject.transform.SetAsLastSibling();
		LayoutElement component = this.scrollRect.GetComponent<LayoutElement>();
		component.minHeight = (float)(58 * ((this.activeFacadeToggles.Count <= 5) ? 1 : 2));
		component.preferredHeight = component.minHeight;
	}

	// Token: 0x0600533B RID: 21307 RVA: 0x001E34AC File Offset: 0x001E16AC
	private void AddDefaultFacadeToggle()
	{
		this.AddNewToggle("DEFAULT_FACADE");
	}

	// Token: 0x0600533C RID: 21308 RVA: 0x001E34BC File Offset: 0x001E16BC
	private void AddNewToggle(string facadeID)
	{
		if (this.activeFacadeToggles.ContainsKey(facadeID))
		{
			return;
		}
		GameObject gameObject;
		if (this.pooledFacadeToggles.Count > 0)
		{
			gameObject = this.pooledFacadeToggles[0];
			this.pooledFacadeToggles.RemoveAt(0);
		}
		else
		{
			gameObject = Util.KInstantiateUI(this.togglePrefab, this.toggleContainer.gameObject, false);
		}
		FacadeSelectionPanel.FacadeToggle newToggle = new FacadeSelectionPanel.FacadeToggle(facadeID, this.selectedBuildingDefID, gameObject);
		MultiToggle multiToggle = newToggle.multiToggle;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			this.SelectedFacade = newToggle.id;
		}));
		this.activeFacadeToggles.Add(newToggle.id, newToggle);
	}

	// Token: 0x0400385E RID: 14430
	[SerializeField]
	private GameObject togglePrefab;

	// Token: 0x0400385F RID: 14431
	[SerializeField]
	private RectTransform toggleContainer;

	// Token: 0x04003860 RID: 14432
	[SerializeField]
	private LayoutElement scrollRect;

	// Token: 0x04003861 RID: 14433
	private Dictionary<string, FacadeSelectionPanel.FacadeToggle> activeFacadeToggles = new Dictionary<string, FacadeSelectionPanel.FacadeToggle>();

	// Token: 0x04003862 RID: 14434
	private List<GameObject> pooledFacadeToggles = new List<GameObject>();

	// Token: 0x04003863 RID: 14435
	[SerializeField]
	private KButton storeButton;

	// Token: 0x04003864 RID: 14436
	public System.Action OnFacadeSelectionChanged;

	// Token: 0x04003865 RID: 14437
	private string selectedBuildingDefID;

	// Token: 0x04003866 RID: 14438
	private string _selectedFacade;

	// Token: 0x04003867 RID: 14439
	public const string DEFAULT_FACADE_ID = "DEFAULT_FACADE";

	// Token: 0x02001921 RID: 6433
	private struct FacadeToggle
	{
		// Token: 0x06008F52 RID: 36690 RVA: 0x00310068 File Offset: 0x0030E268
		public FacadeToggle(string facadeID, string buildingPrefabID, GameObject gameObject)
		{
			this.id = facadeID;
			this.gameObject = gameObject;
			gameObject.SetActive(true);
			this.multiToggle = gameObject.GetComponent<MultiToggle>();
			this.multiToggle.onClick = null;
			if (facadeID != "DEFAULT_FACADE")
			{
				BuildingFacadeResource buildingFacadeResource = Db.GetBuildingFacades().Get(facadeID);
				gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("FGImage").sprite = Def.GetUISpriteFromMultiObjectAnim(Assets.GetAnim(buildingFacadeResource.AnimFile), "ui", false, "");
				this.gameObject.GetComponent<ToolTip>().SetSimpleTooltip(GameUtil.ApplyBoldString(buildingFacadeResource.Name) + "\n\n" + buildingFacadeResource.Description);
				return;
			}
			gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("FGImage").sprite = Def.GetUISprite(buildingPrefabID, "ui", false).first;
			StringEntry stringEntry;
			Strings.TryGet(string.Concat(new string[]
			{
				"STRINGS.BUILDINGS.PREFABS.",
				buildingPrefabID.ToUpperInvariant(),
				".FACADES.DEFAULT_",
				buildingPrefabID.ToUpperInvariant(),
				".NAME"
			}), out stringEntry);
			StringEntry stringEntry2;
			Strings.TryGet(string.Concat(new string[]
			{
				"STRINGS.BUILDINGS.PREFABS.",
				buildingPrefabID.ToUpperInvariant(),
				".FACADES.DEFAULT_",
				buildingPrefabID.ToUpperInvariant(),
				".DESC"
			}), out stringEntry2);
			GameObject prefab = Assets.GetPrefab(buildingPrefabID);
			this.gameObject.GetComponent<ToolTip>().SetSimpleTooltip(GameUtil.ApplyBoldString((stringEntry != null) ? stringEntry.String : prefab.GetProperName()) + "\n\n" + ((stringEntry2 != null) ? stringEntry2.String : ""));
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06008F53 RID: 36691 RVA: 0x00310208 File Offset: 0x0030E408
		// (set) Token: 0x06008F54 RID: 36692 RVA: 0x00310210 File Offset: 0x0030E410
		public string id { readonly get; set; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06008F55 RID: 36693 RVA: 0x00310219 File Offset: 0x0030E419
		// (set) Token: 0x06008F56 RID: 36694 RVA: 0x00310221 File Offset: 0x0030E421
		public GameObject gameObject { readonly get; set; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06008F57 RID: 36695 RVA: 0x0031022A File Offset: 0x0030E42A
		// (set) Token: 0x06008F58 RID: 36696 RVA: 0x00310232 File Offset: 0x0030E432
		public MultiToggle multiToggle { readonly get; set; }
	}
}
