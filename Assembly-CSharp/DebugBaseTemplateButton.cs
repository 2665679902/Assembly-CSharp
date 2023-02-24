using System;
using System.Collections.Generic;
using Klei.AI;
using TemplateClasses;
using UnityEngine;

// Token: 0x02000A81 RID: 2689
public class DebugBaseTemplateButton : KScreen
{
	// Token: 0x17000624 RID: 1572
	// (get) Token: 0x0600523B RID: 21051 RVA: 0x001DAEF1 File Offset: 0x001D90F1
	// (set) Token: 0x0600523C RID: 21052 RVA: 0x001DAEF8 File Offset: 0x001D90F8
	public static DebugBaseTemplateButton Instance { get; private set; }

	// Token: 0x0600523D RID: 21053 RVA: 0x001DAF00 File Offset: 0x001D9100
	public static void DestroyInstance()
	{
		DebugBaseTemplateButton.Instance = null;
	}

	// Token: 0x0600523E RID: 21054 RVA: 0x001DAF08 File Offset: 0x001D9108
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DebugBaseTemplateButton.Instance = this;
		base.gameObject.SetActive(false);
		this.SetupLocText();
		base.ConsumeMouseScroll = true;
		KInputTextField kinputTextField = this.nameField;
		kinputTextField.onFocus = (System.Action)Delegate.Combine(kinputTextField.onFocus, new System.Action(delegate
		{
			base.isEditing = true;
		}));
		this.nameField.onEndEdit.AddListener(delegate
		{
			base.isEditing = false;
		});
		this.nameField.onValueChanged.AddListener(delegate
		{
			Util.ScrubInputField(this.nameField, true, false);
		});
	}

	// Token: 0x0600523F RID: 21055 RVA: 0x001DAF99 File Offset: 0x001D9199
	protected override void OnActivate()
	{
		base.OnActivate();
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005240 RID: 21056 RVA: 0x001DAFA8 File Offset: 0x001D91A8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.saveBaseButton != null)
		{
			this.saveBaseButton.onClick -= this.OnClickSaveBase;
			this.saveBaseButton.onClick += this.OnClickSaveBase;
		}
		if (this.clearButton != null)
		{
			this.clearButton.onClick -= this.OnClickClear;
			this.clearButton.onClick += this.OnClickClear;
		}
		if (this.AddSelectionButton != null)
		{
			this.AddSelectionButton.onClick -= this.OnClickAddSelection;
			this.AddSelectionButton.onClick += this.OnClickAddSelection;
		}
		if (this.RemoveSelectionButton != null)
		{
			this.RemoveSelectionButton.onClick -= this.OnClickRemoveSelection;
			this.RemoveSelectionButton.onClick += this.OnClickRemoveSelection;
		}
		if (this.clearSelectionButton != null)
		{
			this.clearSelectionButton.onClick -= this.OnClickClearSelection;
			this.clearSelectionButton.onClick += this.OnClickClearSelection;
		}
		if (this.MoveButton != null)
		{
			this.MoveButton.onClick -= this.OnClickMove;
			this.MoveButton.onClick += this.OnClickMove;
		}
		if (this.DestroyButton != null)
		{
			this.DestroyButton.onClick -= this.OnClickDestroySelection;
			this.DestroyButton.onClick += this.OnClickDestroySelection;
		}
		if (this.DeconstructButton != null)
		{
			this.DeconstructButton.onClick -= this.OnClickDeconstructSelection;
			this.DeconstructButton.onClick += this.OnClickDeconstructSelection;
		}
	}

	// Token: 0x06005241 RID: 21057 RVA: 0x001DB19B File Offset: 0x001D939B
	private void SetupLocText()
	{
	}

	// Token: 0x06005242 RID: 21058 RVA: 0x001DB19D File Offset: 0x001D939D
	private void OnClickDestroySelection()
	{
		DebugTool.Instance.Activate(DebugTool.Type.Destroy);
	}

	// Token: 0x06005243 RID: 21059 RVA: 0x001DB1AA File Offset: 0x001D93AA
	private void OnClickDeconstructSelection()
	{
		DebugTool.Instance.Activate(DebugTool.Type.Deconstruct);
	}

	// Token: 0x06005244 RID: 21060 RVA: 0x001DB1B7 File Offset: 0x001D93B7
	private void OnClickMove()
	{
		DebugTool.Instance.DeactivateTool(null);
		this.moveAsset = this.GetSelectionAsAsset();
		StampTool.Instance.Activate(this.moveAsset, false, false);
	}

	// Token: 0x06005245 RID: 21061 RVA: 0x001DB1E2 File Offset: 0x001D93E2
	private void OnClickAddSelection()
	{
		DebugTool.Instance.Activate(DebugTool.Type.AddSelection);
	}

	// Token: 0x06005246 RID: 21062 RVA: 0x001DB1EF File Offset: 0x001D93EF
	private void OnClickRemoveSelection()
	{
		DebugTool.Instance.Activate(DebugTool.Type.RemoveSelection);
	}

	// Token: 0x06005247 RID: 21063 RVA: 0x001DB1FC File Offset: 0x001D93FC
	private void OnClickClearSelection()
	{
		this.ClearSelection();
		this.nameField.text = "";
	}

	// Token: 0x06005248 RID: 21064 RVA: 0x001DB214 File Offset: 0x001D9414
	private void OnClickClear()
	{
		DebugTool.Instance.Activate(DebugTool.Type.Clear);
	}

	// Token: 0x06005249 RID: 21065 RVA: 0x001DB221 File Offset: 0x001D9421
	protected override void OnDeactivate()
	{
		if (DebugTool.Instance != null)
		{
			DebugTool.Instance.DeactivateTool(null);
		}
		base.OnDeactivate();
	}

	// Token: 0x0600524A RID: 21066 RVA: 0x001DB241 File Offset: 0x001D9441
	private void OnDisable()
	{
		if (DebugTool.Instance != null)
		{
			DebugTool.Instance.DeactivateTool(null);
		}
	}

	// Token: 0x0600524B RID: 21067 RVA: 0x001DB25C File Offset: 0x001D945C
	private TemplateContainer GetSelectionAsAsset()
	{
		List<Cell> list = new List<Cell>();
		List<Prefab> list2 = new List<Prefab>();
		List<Prefab> list3 = new List<Prefab>();
		List<Prefab> list4 = new List<Prefab>();
		List<Prefab> list5 = new List<Prefab>();
		HashSet<GameObject> hashSet = new HashSet<GameObject>();
		float num = 0f;
		float num2 = 0f;
		foreach (int num3 in this.SelectedCells)
		{
			num += (float)Grid.CellToXY(num3).x;
			num2 += (float)Grid.CellToXY(num3).y;
		}
		float num4 = num / (float)this.SelectedCells.Count;
		float num5;
		num2 = (num5 = num2 / (float)this.SelectedCells.Count);
		int rootX;
		int rootY;
		Grid.CellToXY(Grid.PosToCell(new Vector3(num4, num5, 0f)), out rootX, out rootY);
		for (int i = 0; i < this.SelectedCells.Count; i++)
		{
			int num6 = this.SelectedCells[i];
			int num7;
			int num8;
			Grid.CellToXY(this.SelectedCells[i], out num7, out num8);
			Element element = ElementLoader.elements[(int)Grid.ElementIdx[num6]];
			string text = ((Grid.DiseaseIdx[num6] != byte.MaxValue) ? Db.Get().Diseases[(int)Grid.DiseaseIdx[num6]].Id : null);
			int num9 = Grid.DiseaseCount[num6];
			if (num9 <= 0)
			{
				num9 = 0;
				text = null;
			}
			list.Add(new Cell(num7 - rootX, num8 - rootY, element.id, Grid.Temperature[num6], Grid.Mass[num6], text, num9, Grid.PreventFogOfWarReveal[this.SelectedCells[i]]));
		}
		for (int j = 0; j < Components.BuildingCompletes.Count; j++)
		{
			BuildingComplete buildingComplete = Components.BuildingCompletes[j];
			if (!hashSet.Contains(buildingComplete.gameObject))
			{
				int num10 = Grid.PosToCell(buildingComplete);
				int num11;
				int num12;
				Grid.CellToXY(num10, out num11, out num12);
				if (this.SaveAllBuildings || this.SelectedCells.Contains(num10))
				{
					int[] placementCells = buildingComplete.PlacementCells;
					string text2;
					for (int k = 0; k < placementCells.Length; k++)
					{
						int num13 = placementCells[k];
						int xplace;
						int yplace;
						Grid.CellToXY(num13, out xplace, out yplace);
						text2 = ((Grid.DiseaseIdx[num13] != byte.MaxValue) ? Db.Get().Diseases[(int)Grid.DiseaseIdx[num13]].Id : null);
						if (list.Find((Cell c) => c.location_x == xplace - rootX && c.location_y == yplace - rootY) == null)
						{
							list.Add(new Cell(xplace - rootX, yplace - rootY, Grid.Element[num13].id, Grid.Temperature[num13], Grid.Mass[num13], text2, Grid.DiseaseCount[num13], false));
						}
					}
					Orientation orientation = Orientation.Neutral;
					Rotatable component = buildingComplete.gameObject.GetComponent<Rotatable>();
					if (component != null)
					{
						orientation = component.GetOrientation();
					}
					SimHashes simHashes = SimHashes.Void;
					float num14 = 280f;
					text2 = null;
					int num15 = 0;
					PrimaryElement component2 = buildingComplete.GetComponent<PrimaryElement>();
					if (component2 != null)
					{
						simHashes = component2.ElementID;
						num14 = component2.Temperature;
						text2 = ((component2.DiseaseIdx != byte.MaxValue) ? Db.Get().Diseases[(int)component2.DiseaseIdx].Id : null);
						num15 = component2.DiseaseCount;
					}
					List<Prefab.template_amount_value> list6 = new List<Prefab.template_amount_value>();
					List<Prefab.template_amount_value> list7 = new List<Prefab.template_amount_value>();
					foreach (AmountInstance amountInstance in buildingComplete.gameObject.GetAmounts())
					{
						list6.Add(new Prefab.template_amount_value(amountInstance.amount.Id, amountInstance.value));
					}
					Battery component3 = buildingComplete.GetComponent<Battery>();
					if (component3 != null)
					{
						float joulesAvailable = component3.JoulesAvailable;
						list7.Add(new Prefab.template_amount_value("joulesAvailable", joulesAvailable));
					}
					Unsealable component4 = buildingComplete.GetComponent<Unsealable>();
					if (component4 != null)
					{
						float num16 = (float)(component4.facingRight ? 1 : 0);
						list7.Add(new Prefab.template_amount_value("sealedDoorDirection", num16));
					}
					LogicSwitch component5 = buildingComplete.GetComponent<LogicSwitch>();
					if (component5 != null)
					{
						float num17 = (float)(component5.IsSwitchedOn ? 1 : 0);
						list7.Add(new Prefab.template_amount_value("switchSetting", num17));
					}
					int num18 = 0;
					IHaveUtilityNetworkMgr component6 = buildingComplete.GetComponent<IHaveUtilityNetworkMgr>();
					if (component6 != null)
					{
						num18 = (int)component6.GetNetworkManager().GetConnections(num10, true);
					}
					num11 -= rootX;
					num12 -= rootY;
					num14 = Mathf.Clamp(num14, 1f, 99999f);
					Prefab prefab = new Prefab(buildingComplete.PrefabID().Name, Prefab.Type.Building, num11, num12, simHashes, num14, 0f, text2, num15, orientation, list6.ToArray(), list7.ToArray(), num18);
					Storage component7 = buildingComplete.gameObject.GetComponent<Storage>();
					if (component7 != null)
					{
						foreach (GameObject gameObject in component7.items)
						{
							float num19 = 0f;
							SimHashes simHashes2 = SimHashes.Vacuum;
							float num20 = 280f;
							string text3 = null;
							int num21 = 0;
							bool flag = false;
							PrimaryElement component8 = gameObject.GetComponent<PrimaryElement>();
							if (component8 != null)
							{
								num19 = component8.Units;
								simHashes2 = component8.ElementID;
								num20 = component8.Temperature;
								text3 = ((component8.DiseaseIdx != byte.MaxValue) ? Db.Get().Diseases[(int)component8.DiseaseIdx].Id : null);
								num21 = component8.DiseaseCount;
							}
							global::Rottable.Instance smi = gameObject.gameObject.GetSMI<global::Rottable.Instance>();
							if (gameObject.GetComponent<ElementChunk>() != null)
							{
								flag = true;
							}
							StorageItem storageItem = new StorageItem(gameObject.PrefabID().Name, num19, num20, simHashes2, text3, num21, flag);
							if (smi != null)
							{
								storageItem.rottable.rotAmount = smi.RotValue;
							}
							prefab.AssignStorage(storageItem);
							hashSet.Add(gameObject);
						}
					}
					list2.Add(prefab);
					hashSet.Add(buildingComplete.gameObject);
				}
			}
		}
		for (int l = 0; l < Components.Pickupables.Count; l++)
		{
			if (Components.Pickupables[l].gameObject.activeSelf)
			{
				Pickupable pickupable = Components.Pickupables[l];
				if (!hashSet.Contains(pickupable.gameObject))
				{
					int num22 = Grid.PosToCell(pickupable);
					if ((this.SaveAllPickups || this.SelectedCells.Contains(num22)) && !Components.Pickupables[l].gameObject.GetComponent<MinionBrain>())
					{
						int num23;
						int num24;
						Grid.CellToXY(num22, out num23, out num24);
						num23 -= rootX;
						num24 -= rootY;
						SimHashes simHashes3 = SimHashes.Void;
						float num25 = 280f;
						float num26 = 1f;
						string text4 = null;
						int num27 = 0;
						float num28 = 0f;
						global::Rottable.Instance smi2 = pickupable.gameObject.GetSMI<global::Rottable.Instance>();
						if (smi2 != null)
						{
							num28 = smi2.RotValue;
						}
						PrimaryElement component9 = pickupable.gameObject.GetComponent<PrimaryElement>();
						if (component9 != null)
						{
							simHashes3 = component9.ElementID;
							num26 = component9.Units;
							num25 = component9.Temperature;
							text4 = ((component9.DiseaseIdx != byte.MaxValue) ? Db.Get().Diseases[(int)component9.DiseaseIdx].Id : null);
							num27 = component9.DiseaseCount;
						}
						if (pickupable.gameObject.GetComponent<ElementChunk>() != null)
						{
							Prefab prefab2 = new Prefab(pickupable.PrefabID().Name, Prefab.Type.Ore, num23, num24, simHashes3, num25, num26, text4, num27, Orientation.Neutral, null, null, 0);
							list4.Add(prefab2);
						}
						else
						{
							list3.Add(new Prefab(pickupable.PrefabID().Name, Prefab.Type.Pickupable, num23, num24, simHashes3, num25, num26, text4, num27, Orientation.Neutral, null, null, 0)
							{
								rottable = new TemplateClasses.Rottable(),
								rottable = 
								{
									rotAmount = num28
								}
							});
						}
						hashSet.Add(pickupable.gameObject);
					}
				}
			}
		}
		this.GetEntities<Crop>(Components.Crops.Items, rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<Health>(Components.Health.Items, rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<Harvestable>(Components.Harvestables.Items, rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<Edible>(Components.Edibles.Items, rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<Geyser>(rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<OccupyArea>(rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<FogOfWarMask>(rootX, rootY, ref list4, ref list5, ref hashSet);
		TemplateContainer templateContainer = new TemplateContainer();
		templateContainer.Init(list, list2, list3, list4, list5);
		return templateContainer;
	}

	// Token: 0x0600524C RID: 21068 RVA: 0x001DBC70 File Offset: 0x001D9E70
	private void GetEntities<T>(int rootX, int rootY, ref List<Prefab> _primaryElementOres, ref List<Prefab> _otherEntities, ref HashSet<GameObject> _excludeEntities)
	{
		object[] array = UnityEngine.Object.FindObjectsOfType(typeof(T));
		object[] array2 = array;
		this.GetEntities<object>(array2, rootX, rootY, ref _primaryElementOres, ref _otherEntities, ref _excludeEntities);
	}

	// Token: 0x0600524D RID: 21069 RVA: 0x001DBCA0 File Offset: 0x001D9EA0
	private void GetEntities<T>(IEnumerable<T> component_collection, int rootX, int rootY, ref List<Prefab> _primaryElementOres, ref List<Prefab> _otherEntities, ref HashSet<GameObject> _excludeEntities)
	{
		foreach (T t in component_collection)
		{
			if (!_excludeEntities.Contains((t as KMonoBehaviour).gameObject) && (t as KMonoBehaviour).gameObject.activeSelf)
			{
				int num = Grid.PosToCell(t as KMonoBehaviour);
				if (this.SelectedCells.Contains(num) && !(t as KMonoBehaviour).gameObject.GetComponent<MinionBrain>())
				{
					int num2;
					int num3;
					Grid.CellToXY(num, out num2, out num3);
					num2 -= rootX;
					num3 -= rootY;
					SimHashes simHashes = SimHashes.Void;
					float num4 = 280f;
					float num5 = 1f;
					string text = null;
					int num6 = 0;
					PrimaryElement component = (t as KMonoBehaviour).gameObject.GetComponent<PrimaryElement>();
					if (component != null)
					{
						simHashes = component.ElementID;
						num5 = component.Units;
						num4 = component.Temperature;
						text = ((component.DiseaseIdx != byte.MaxValue) ? Db.Get().Diseases[(int)component.DiseaseIdx].Id : null);
						num6 = component.DiseaseCount;
					}
					List<Prefab.template_amount_value> list = new List<Prefab.template_amount_value>();
					if ((t as KMonoBehaviour).gameObject.GetAmounts() != null)
					{
						foreach (AmountInstance amountInstance in (t as KMonoBehaviour).gameObject.GetAmounts())
						{
							list.Add(new Prefab.template_amount_value(amountInstance.amount.Id, amountInstance.value));
						}
					}
					if ((t as KMonoBehaviour).gameObject.GetComponent<ElementChunk>() != null)
					{
						Prefab prefab = new Prefab((t as KMonoBehaviour).PrefabID().Name, Prefab.Type.Ore, num2, num3, simHashes, num4, num5, text, num6, Orientation.Neutral, list.ToArray(), null, 0);
						_primaryElementOres.Add(prefab);
						_excludeEntities.Add((t as KMonoBehaviour).gameObject);
					}
					else
					{
						Prefab prefab = new Prefab((t as KMonoBehaviour).PrefabID().Name, Prefab.Type.Other, num2, num3, simHashes, num4, num5, text, num6, Orientation.Neutral, list.ToArray(), null, 0);
						_otherEntities.Add(prefab);
						_excludeEntities.Add((t as KMonoBehaviour).gameObject);
					}
				}
			}
		}
	}

	// Token: 0x0600524E RID: 21070 RVA: 0x001DBF74 File Offset: 0x001DA174
	private void OnClickSaveBase()
	{
		TemplateContainer selectionAsAsset = this.GetSelectionAsAsset();
		if (this.SelectedCells.Count <= 0)
		{
			global::Debug.LogWarning("No cells selected. Use buttons above to select the area you want to save.");
			return;
		}
		this.SaveName = this.nameField.text;
		if (this.SaveName == null || this.SaveName == "")
		{
			global::Debug.LogWarning("Invalid save name. Please enter a name in the input field.");
			return;
		}
		selectionAsAsset.SaveToYaml(this.SaveName);
		TemplateCache.Clear();
		TemplateCache.Init();
		PasteBaseTemplateScreen.Instance.RefreshStampButtons();
	}

	// Token: 0x0600524F RID: 21071 RVA: 0x001DBFF8 File Offset: 0x001DA1F8
	public void ClearSelection()
	{
		for (int i = this.SelectedCells.Count - 1; i >= 0; i--)
		{
			this.RemoveFromSelection(this.SelectedCells[i]);
		}
	}

	// Token: 0x06005250 RID: 21072 RVA: 0x001DC02F File Offset: 0x001DA22F
	public void DestroySelection()
	{
	}

	// Token: 0x06005251 RID: 21073 RVA: 0x001DC031 File Offset: 0x001DA231
	public void DeconstructSelection()
	{
	}

	// Token: 0x06005252 RID: 21074 RVA: 0x001DC034 File Offset: 0x001DA234
	public void AddToSelection(int cell)
	{
		if (!this.SelectedCells.Contains(cell))
		{
			GameObject gameObject = Util.KInstantiate(this.Placer, null, null);
			Grid.Objects[cell, 7] = gameObject;
			Vector3 vector = Grid.CellToPosCBC(cell, this.visualizerLayer);
			float num = -0.15f;
			vector.z += num;
			gameObject.transform.SetPosition(vector);
			this.SelectedCells.Add(cell);
		}
	}

	// Token: 0x06005253 RID: 21075 RVA: 0x001DC0A8 File Offset: 0x001DA2A8
	public void RemoveFromSelection(int cell)
	{
		if (this.SelectedCells.Contains(cell))
		{
			GameObject gameObject = Grid.Objects[cell, 7];
			if (gameObject != null)
			{
				gameObject.DeleteObject();
			}
			this.SelectedCells.Remove(cell);
		}
	}

	// Token: 0x0400377A RID: 14202
	private bool SaveAllBuildings;

	// Token: 0x0400377B RID: 14203
	private bool SaveAllPickups;

	// Token: 0x0400377C RID: 14204
	public KButton saveBaseButton;

	// Token: 0x0400377D RID: 14205
	public KButton clearButton;

	// Token: 0x0400377E RID: 14206
	private TemplateContainer pasteAndSelectAsset;

	// Token: 0x0400377F RID: 14207
	public KButton AddSelectionButton;

	// Token: 0x04003780 RID: 14208
	public KButton RemoveSelectionButton;

	// Token: 0x04003781 RID: 14209
	public KButton clearSelectionButton;

	// Token: 0x04003782 RID: 14210
	public KButton DestroyButton;

	// Token: 0x04003783 RID: 14211
	public KButton DeconstructButton;

	// Token: 0x04003784 RID: 14212
	public KButton MoveButton;

	// Token: 0x04003785 RID: 14213
	public TemplateContainer moveAsset;

	// Token: 0x04003786 RID: 14214
	public KInputTextField nameField;

	// Token: 0x04003787 RID: 14215
	private string SaveName = "enter_template_name";

	// Token: 0x04003788 RID: 14216
	public GameObject Placer;

	// Token: 0x04003789 RID: 14217
	public Grid.SceneLayer visualizerLayer = Grid.SceneLayer.Move;

	// Token: 0x0400378A RID: 14218
	public List<int> SelectedCells = new List<int>();
}
