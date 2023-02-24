using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Klei;
using Klei.AI;
using ProcGen;
using STRINGS;
using UnityEngine;

// Token: 0x02000701 RID: 1793
[Serializable]
public class BuildingDef : Def
{
	// Token: 0x1700038D RID: 909
	// (get) Token: 0x06003112 RID: 12562 RVA: 0x001042A3 File Offset: 0x001024A3
	public override string Name
	{
		get
		{
			return Strings.Get("STRINGS.BUILDINGS.PREFABS." + this.PrefabID.ToUpper() + ".NAME");
		}
	}

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x06003113 RID: 12563 RVA: 0x001042C9 File Offset: 0x001024C9
	public string Desc
	{
		get
		{
			return Strings.Get("STRINGS.BUILDINGS.PREFABS." + this.PrefabID.ToUpper() + ".DESC");
		}
	}

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x06003114 RID: 12564 RVA: 0x001042EF File Offset: 0x001024EF
	public string Flavor
	{
		get
		{
			return "\"" + Strings.Get("STRINGS.BUILDINGS.PREFABS." + this.PrefabID.ToUpper() + ".FLAVOR") + "\"";
		}
	}

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x06003115 RID: 12565 RVA: 0x00104324 File Offset: 0x00102524
	public string Effect
	{
		get
		{
			return Strings.Get("STRINGS.BUILDINGS.PREFABS." + this.PrefabID.ToUpper() + ".EFFECT");
		}
	}

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x06003116 RID: 12566 RVA: 0x0010434A File Offset: 0x0010254A
	public bool IsTilePiece
	{
		get
		{
			return this.TileLayer != ObjectLayer.NumLayers;
		}
	}

	// Token: 0x06003117 RID: 12567 RVA: 0x00104359 File Offset: 0x00102559
	public bool CanReplace(GameObject go)
	{
		return this.ReplacementTags != null && go.GetComponent<KPrefabID>().HasAnyTags(this.ReplacementTags);
	}

	// Token: 0x06003118 RID: 12568 RVA: 0x00104376 File Offset: 0x00102576
	public bool IsAvailable()
	{
		return !this.Deprecated && (!this.DebugOnly || Game.Instance.DebugOnlyBuildingsAllowed);
	}

	// Token: 0x06003119 RID: 12569 RVA: 0x00104396 File Offset: 0x00102596
	public bool ShouldShowInBuildMenu()
	{
		return this.ShowInBuildMenu;
	}

	// Token: 0x0600311A RID: 12570 RVA: 0x001043A0 File Offset: 0x001025A0
	public bool IsReplacementLayerOccupied(int cell)
	{
		if (Grid.Objects[cell, (int)this.ReplacementLayer] != null)
		{
			return true;
		}
		if (this.EquivalentReplacementLayers != null)
		{
			foreach (ObjectLayer objectLayer in this.EquivalentReplacementLayers)
			{
				if (Grid.Objects[cell, (int)objectLayer] != null)
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x0600311B RID: 12571 RVA: 0x0010442C File Offset: 0x0010262C
	public GameObject GetReplacementCandidate(int cell)
	{
		if (this.ReplacementCandidateLayers != null)
		{
			using (List<ObjectLayer>.Enumerator enumerator = this.ReplacementCandidateLayers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ObjectLayer objectLayer = enumerator.Current;
					if (Grid.ObjectLayers[(int)objectLayer].ContainsKey(cell))
					{
						GameObject gameObject = Grid.ObjectLayers[(int)objectLayer][cell];
						if (gameObject != null && gameObject.GetComponent<BuildingComplete>() != null)
						{
							return gameObject;
						}
					}
				}
				goto IL_96;
			}
		}
		if (Grid.ObjectLayers[(int)this.TileLayer].ContainsKey(cell))
		{
			return Grid.ObjectLayers[(int)this.TileLayer][cell];
		}
		IL_96:
		return null;
	}

	// Token: 0x0600311C RID: 12572 RVA: 0x001044E4 File Offset: 0x001026E4
	public GameObject Create(Vector3 pos, Storage resource_storage, IList<Tag> selected_elements, Recipe recipe, float temperature, GameObject obj)
	{
		SimUtil.DiseaseInfo diseaseInfo = SimUtil.DiseaseInfo.Invalid;
		if (resource_storage != null)
		{
			Recipe.Ingredient[] allIngredients = recipe.GetAllIngredients(selected_elements);
			if (allIngredients != null)
			{
				foreach (Recipe.Ingredient ingredient in allIngredients)
				{
					float num;
					SimUtil.DiseaseInfo diseaseInfo2;
					float num2;
					resource_storage.ConsumeAndGetDisease(ingredient.tag, ingredient.amount, out num, out diseaseInfo2, out num2);
					diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(diseaseInfo, diseaseInfo2);
				}
			}
		}
		GameObject gameObject = GameUtil.KInstantiate(obj, pos, this.SceneLayer, null, 0);
		Element element = ElementLoader.GetElement(selected_elements[0]);
		global::Debug.Assert(element != null);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.ElementID = element.id;
		component.Temperature = temperature;
		component.AddDisease(diseaseInfo.idx, diseaseInfo.count, "BuildingDef.Create");
		gameObject.name = obj.name;
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x0600311D RID: 12573 RVA: 0x001045B4 File Offset: 0x001027B4
	public List<Tag> DefaultElements()
	{
		List<Tag> list = new List<Tag>();
		string[] materialCategory = this.MaterialCategory;
		for (int i = 0; i < materialCategory.Length; i++)
		{
			List<Tag> validMaterials = MaterialSelector.GetValidMaterials(materialCategory[i]);
			if (validMaterials.Count != 0)
			{
				list.Add(validMaterials[0]);
			}
		}
		return list;
	}

	// Token: 0x0600311E RID: 12574 RVA: 0x00104600 File Offset: 0x00102800
	public GameObject Build(int cell, Orientation orientation, Storage resource_storage, IList<Tag> selected_elements, float temperature, string facadeID, bool playsound = true, float timeBuilt = -1f)
	{
		GameObject gameObject = this.Build(cell, orientation, resource_storage, selected_elements, temperature, playsound, timeBuilt);
		if (facadeID != null && facadeID != "DEFAULT_FACADE")
		{
			gameObject.GetComponent<BuildingFacade>().ApplyBuildingFacade(Db.GetBuildingFacades().Get(facadeID));
		}
		return gameObject;
	}

	// Token: 0x0600311F RID: 12575 RVA: 0x0010464C File Offset: 0x0010284C
	public GameObject Build(int cell, Orientation orientation, Storage resource_storage, IList<Tag> selected_elements, float temperature, bool playsound = true, float timeBuilt = -1f)
	{
		Vector3 vector = Grid.CellToPosCBC(cell, this.SceneLayer);
		GameObject gameObject = this.Create(vector, resource_storage, selected_elements, this.CraftRecipe, temperature, this.BuildingComplete);
		Rotatable component = gameObject.GetComponent<Rotatable>();
		if (component != null)
		{
			component.SetOrientation(orientation);
		}
		this.MarkArea(cell, orientation, this.ObjectLayer, gameObject);
		if (this.IsTilePiece)
		{
			this.MarkArea(cell, orientation, this.TileLayer, gameObject);
			this.RunOnArea(cell, orientation, delegate(int c)
			{
				TileVisualizer.RefreshCell(c, this.TileLayer, this.ReplacementLayer);
			});
		}
		if (this.PlayConstructionSounds)
		{
			string sound = GlobalAssets.GetSound("Finish_Building_" + this.AudioSize, false);
			if (playsound && sound != null)
			{
				Vector3 position = gameObject.transform.GetPosition();
				position.z = 0f;
				KFMOD.PlayOneShot(sound, position, 1f);
			}
		}
		Deconstructable component2 = gameObject.GetComponent<Deconstructable>();
		if (component2 != null)
		{
			component2.constructionElements = new Tag[selected_elements.Count];
			for (int i = 0; i < selected_elements.Count; i++)
			{
				component2.constructionElements[i] = selected_elements[i];
			}
		}
		BuildingComplete component3 = gameObject.GetComponent<BuildingComplete>();
		if (component3)
		{
			component3.SetCreationTime(timeBuilt);
		}
		Game.Instance.Trigger(-1661515756, gameObject);
		gameObject.Trigger(-1661515756, gameObject);
		return gameObject;
	}

	// Token: 0x06003120 RID: 12576 RVA: 0x001047A4 File Offset: 0x001029A4
	public GameObject TryPlace(GameObject src_go, Vector3 pos, Orientation orientation, IList<Tag> selected_elements, int layer = 0)
	{
		GameObject gameObject = null;
		string text;
		if (this.IsValidPlaceLocation(src_go, pos, orientation, false, out text))
		{
			gameObject = this.Instantiate(pos, orientation, selected_elements, layer);
		}
		return gameObject;
	}

	// Token: 0x06003121 RID: 12577 RVA: 0x001047D0 File Offset: 0x001029D0
	public GameObject TryPlace(GameObject src_go, Vector3 pos, Orientation orientation, IList<Tag> selected_elements, string facadeID, int layer = 0)
	{
		GameObject gameObject = this.TryPlace(src_go, pos, orientation, selected_elements, layer);
		if (gameObject != null && facadeID != null && facadeID != "DEFAULT_FACADE")
		{
			gameObject.GetComponent<BuildingFacade>().ApplyBuildingFacade(Db.GetBuildingFacades().Get(facadeID));
			gameObject.GetComponent<KBatchedAnimController>().Play("place", KAnim.PlayMode.Once, 1f, 0f);
		}
		return gameObject;
	}

	// Token: 0x06003122 RID: 12578 RVA: 0x00104840 File Offset: 0x00102A40
	public GameObject TryReplaceTile(GameObject src_go, Vector3 pos, Orientation orientation, IList<Tag> selected_elements, int layer = 0)
	{
		GameObject gameObject = null;
		string text;
		if (this.IsValidPlaceLocation(src_go, pos, orientation, true, out text))
		{
			Constructable component = this.BuildingUnderConstruction.GetComponent<Constructable>();
			component.IsReplacementTile = true;
			gameObject = this.Instantiate(pos, orientation, selected_elements, layer);
			component.IsReplacementTile = false;
		}
		return gameObject;
	}

	// Token: 0x06003123 RID: 12579 RVA: 0x00104884 File Offset: 0x00102A84
	public GameObject TryReplaceTile(GameObject src_go, Vector3 pos, Orientation orientation, IList<Tag> selected_elements, string facadeID, int layer = 0)
	{
		GameObject gameObject = this.TryReplaceTile(src_go, pos, orientation, selected_elements, layer);
		if (gameObject != null && facadeID != null && facadeID != "DEFAULT_FACADE")
		{
			gameObject.GetComponent<BuildingFacade>().ApplyBuildingFacade(Db.GetBuildingFacades().Get(facadeID));
		}
		return gameObject;
	}

	// Token: 0x06003124 RID: 12580 RVA: 0x001048D4 File Offset: 0x00102AD4
	public GameObject Instantiate(Vector3 pos, Orientation orientation, IList<Tag> selected_elements, int layer = 0)
	{
		float num = -0.15f;
		pos.z += num;
		GameObject gameObject = GameUtil.KInstantiate(this.BuildingUnderConstruction, pos, Grid.SceneLayer.Front, null, layer);
		Element element = ElementLoader.GetElement(selected_elements[0]);
		global::Debug.Assert(element != null, "Missing primary element for BuildingDef");
		gameObject.GetComponent<PrimaryElement>().ElementID = element.id;
		gameObject.GetComponent<Constructable>().SelectedElementsTags = selected_elements;
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06003125 RID: 12581 RVA: 0x00104948 File Offset: 0x00102B48
	private bool IsAreaClear(GameObject source_go, int cell, Orientation orientation, ObjectLayer layer, ObjectLayer tile_layer, bool replace_tile, out string fail_reason)
	{
		bool flag = true;
		fail_reason = null;
		int i = 0;
		while (i < this.PlacementOffsets.Length)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.PlacementOffsets[i], orientation);
			if (!Grid.IsCellOffsetValid(cell, rotatedCellOffset))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL;
				flag = false;
				break;
			}
			int num = Grid.OffsetCell(cell, rotatedCellOffset);
			if ((int)Grid.WorldIdx[num] != ClusterManager.Instance.activeWorldId)
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL;
				return false;
			}
			if (!Grid.IsValidBuildingCell(num))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL;
				flag = false;
				break;
			}
			if (Grid.Element[num].id == SimHashes.Unobtanium)
			{
				fail_reason = null;
				flag = false;
				break;
			}
			bool flag2 = this.BuildLocationRule == BuildLocationRule.LogicBridge || this.BuildLocationRule == BuildLocationRule.Conduit || this.BuildLocationRule == BuildLocationRule.WireBridge;
			GameObject gameObject = null;
			if (replace_tile)
			{
				gameObject = this.GetReplacementCandidate(num);
			}
			if (!flag2)
			{
				GameObject gameObject2 = Grid.Objects[num, (int)layer];
				if (gameObject2 != null && gameObject2 != source_go && (gameObject == null || gameObject != gameObject2) && (gameObject2.GetComponent<Wire>() == null || this.BuildingComplete.GetComponent<Wire>() == null))
				{
					fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_OCCUPIED;
					flag = false;
					break;
				}
				if (tile_layer != ObjectLayer.NumLayers && (gameObject == null || gameObject == source_go) && Grid.Objects[num, (int)tile_layer] != null && Grid.Objects[num, (int)tile_layer].GetComponent<BuildingPreview>() == null)
				{
					fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_OCCUPIED;
					flag = false;
					break;
				}
			}
			if (layer == ObjectLayer.Building && this.AttachmentSlotTag != GameTags.Rocket && Grid.Objects[num, 39] != null)
			{
				if (this.BuildingComplete.GetComponent<Wire>() == null)
				{
					fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_OCCUPIED;
					flag = false;
					break;
				}
				break;
			}
			else
			{
				if (layer == ObjectLayer.Gantry)
				{
					bool flag3 = false;
					MakeBaseSolid.Def def = source_go.GetDef<MakeBaseSolid.Def>();
					for (int j = 0; j < def.solidOffsets.Length; j++)
					{
						CellOffset rotatedCellOffset2 = Rotatable.GetRotatedCellOffset(def.solidOffsets[j], orientation);
						flag3 |= rotatedCellOffset2 == rotatedCellOffset;
					}
					if (flag3 && !this.IsValidTileLocation(source_go, num, replace_tile, ref fail_reason))
					{
						flag = false;
						break;
					}
					GameObject gameObject3 = Grid.Objects[num, 1];
					if (gameObject3 != null && gameObject3.GetComponent<BuildingPreview>() == null)
					{
						Building component = gameObject3.GetComponent<Building>();
						if (flag3 || component == null || component.Def.AttachmentSlotTag != GameTags.Rocket)
						{
							fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_OCCUPIED;
							flag = false;
							break;
						}
					}
				}
				if (this.BuildLocationRule == BuildLocationRule.Tile)
				{
					if (!this.IsValidTileLocation(source_go, num, replace_tile, ref fail_reason))
					{
						flag = false;
						break;
					}
				}
				else if (this.BuildLocationRule == BuildLocationRule.OnFloorOverSpace && global::World.Instance.zoneRenderData.GetSubWorldZoneType(num) != SubWorld.ZoneType.Space)
				{
					fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_SPACE;
					flag = false;
					break;
				}
				i++;
			}
		}
		if (!flag)
		{
			return false;
		}
		if (layer == ObjectLayer.LiquidConduit)
		{
			GameObject gameObject4 = Grid.Objects[cell, 19];
			if (gameObject4 != null)
			{
				Building component2 = gameObject4.GetComponent<Building>();
				if (component2 != null && component2.Def.BuildLocationRule == BuildLocationRule.NoLiquidConduitAtOrigin && component2.GetCell() == cell)
				{
					fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_LIQUID_CONDUIT_FORBIDDEN;
					return false;
				}
			}
		}
		BuildLocationRule buildLocationRule = this.BuildLocationRule;
		switch (buildLocationRule)
		{
		case BuildLocationRule.NotInTiles:
		{
			GameObject gameObject5 = Grid.Objects[cell, 9];
			if (!replace_tile && gameObject5 != null && gameObject5 != source_go)
			{
				flag = false;
			}
			else if (Grid.HasDoor[cell])
			{
				flag = false;
			}
			else
			{
				GameObject gameObject6 = Grid.Objects[cell, (int)this.ObjectLayer];
				if (gameObject6 != null)
				{
					if (this.ReplacementLayer == ObjectLayer.NumLayers)
					{
						if (gameObject6 != source_go)
						{
							flag = false;
						}
					}
					else
					{
						Building component3 = gameObject6.GetComponent<Building>();
						if (component3 != null && component3.Def.ReplacementLayer != this.ReplacementLayer)
						{
							flag = false;
						}
					}
				}
			}
			if (!flag)
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_NOT_IN_TILES;
			}
			break;
		}
		case BuildLocationRule.Conduit:
		case BuildLocationRule.LogicBridge:
			break;
		case BuildLocationRule.WireBridge:
			return this.IsValidWireBridgeLocation(source_go, cell, orientation, out fail_reason);
		case BuildLocationRule.HighWattBridgeTile:
			flag = this.IsValidTileLocation(source_go, cell, replace_tile, ref fail_reason) && this.IsValidHighWattBridgeLocation(source_go, cell, orientation, out fail_reason);
			break;
		case BuildLocationRule.BuildingAttachPoint:
		{
			flag = false;
			int num2 = 0;
			while (num2 < Components.BuildingAttachPoints.Count && !flag)
			{
				for (int k = 0; k < Components.BuildingAttachPoints[num2].points.Length; k++)
				{
					if (Components.BuildingAttachPoints[num2].AcceptsAttachment(this.AttachmentSlotTag, Grid.OffsetCell(cell, this.attachablePosition)))
					{
						flag = true;
						break;
					}
				}
				num2++;
			}
			if (!flag)
			{
				fail_reason = string.Format(UI.TOOLTIPS.HELP_BUILDLOCATION_ATTACHPOINT, this.AttachmentSlotTag);
			}
			break;
		}
		default:
			if (buildLocationRule == BuildLocationRule.NoLiquidConduitAtOrigin)
			{
				flag = Grid.Objects[cell, 16] == null;
			}
			break;
		}
		flag = flag && this.ArePowerPortsInValidPositions(source_go, cell, orientation, out fail_reason);
		flag = flag && this.AreConduitPortsInValidPositions(source_go, cell, orientation, out fail_reason);
		return flag && this.AreLogicPortsInValidPositions(source_go, cell, out fail_reason);
	}

	// Token: 0x06003126 RID: 12582 RVA: 0x00104EC8 File Offset: 0x001030C8
	private bool IsValidTileLocation(GameObject source_go, int cell, bool replacement_tile, ref string fail_reason)
	{
		GameObject gameObject = Grid.Objects[cell, 27];
		if (gameObject != null && gameObject != source_go && gameObject.GetComponent<Building>().Def.BuildLocationRule == BuildLocationRule.NotInTiles)
		{
			fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_WIRE_OBSTRUCTION;
			return false;
		}
		gameObject = Grid.Objects[cell, 29];
		if (gameObject != null && gameObject != source_go && gameObject.GetComponent<Building>().Def.BuildLocationRule == BuildLocationRule.HighWattBridgeTile)
		{
			fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_WIRE_OBSTRUCTION;
			return false;
		}
		gameObject = Grid.Objects[cell, 2];
		if (gameObject != null && gameObject != source_go)
		{
			Building component = gameObject.GetComponent<Building>();
			if (!replacement_tile && component != null && component.Def.BuildLocationRule == BuildLocationRule.NotInTiles)
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_BACK_WALL;
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003127 RID: 12583 RVA: 0x00104FAC File Offset: 0x001031AC
	public void RunOnArea(int cell, Orientation orientation, Action<int> callback)
	{
		for (int i = 0; i < this.PlacementOffsets.Length; i++)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.PlacementOffsets[i], orientation);
			int num = Grid.OffsetCell(cell, rotatedCellOffset);
			callback(num);
		}
	}

	// Token: 0x06003128 RID: 12584 RVA: 0x00104FF0 File Offset: 0x001031F0
	public void MarkArea(int cell, Orientation orientation, ObjectLayer layer, GameObject go)
	{
		if (this.BuildLocationRule != BuildLocationRule.Conduit && this.BuildLocationRule != BuildLocationRule.WireBridge && this.BuildLocationRule != BuildLocationRule.LogicBridge)
		{
			for (int i = 0; i < this.PlacementOffsets.Length; i++)
			{
				CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.PlacementOffsets[i], orientation);
				int num = Grid.OffsetCell(cell, rotatedCellOffset);
				Grid.Objects[num, (int)layer] = go;
			}
		}
		if (this.InputConduitType != ConduitType.None)
		{
			CellOffset rotatedCellOffset2 = Rotatable.GetRotatedCellOffset(this.UtilityInputOffset, orientation);
			int num2 = Grid.OffsetCell(cell, rotatedCellOffset2);
			ObjectLayer objectLayerForConduitType = Grid.GetObjectLayerForConduitType(this.InputConduitType);
			this.MarkOverlappingPorts(Grid.Objects[num2, (int)objectLayerForConduitType], go);
			Grid.Objects[num2, (int)objectLayerForConduitType] = go;
		}
		if (this.OutputConduitType != ConduitType.None)
		{
			CellOffset rotatedCellOffset3 = Rotatable.GetRotatedCellOffset(this.UtilityOutputOffset, orientation);
			int num3 = Grid.OffsetCell(cell, rotatedCellOffset3);
			ObjectLayer objectLayerForConduitType2 = Grid.GetObjectLayerForConduitType(this.OutputConduitType);
			this.MarkOverlappingPorts(Grid.Objects[num3, (int)objectLayerForConduitType2], go);
			Grid.Objects[num3, (int)objectLayerForConduitType2] = go;
		}
		if (this.RequiresPowerInput)
		{
			CellOffset rotatedCellOffset4 = Rotatable.GetRotatedCellOffset(this.PowerInputOffset, orientation);
			int num4 = Grid.OffsetCell(cell, rotatedCellOffset4);
			this.MarkOverlappingPorts(Grid.Objects[num4, 29], go);
			Grid.Objects[num4, 29] = go;
		}
		if (this.RequiresPowerOutput)
		{
			CellOffset rotatedCellOffset5 = Rotatable.GetRotatedCellOffset(this.PowerOutputOffset, orientation);
			int num5 = Grid.OffsetCell(cell, rotatedCellOffset5);
			this.MarkOverlappingPorts(Grid.Objects[num5, 29], go);
			Grid.Objects[num5, 29] = go;
		}
		if (this.BuildLocationRule == BuildLocationRule.WireBridge || this.BuildLocationRule == BuildLocationRule.HighWattBridgeTile)
		{
			int num6;
			int num7;
			go.GetComponent<UtilityNetworkLink>().GetCells(cell, orientation, out num6, out num7);
			this.MarkOverlappingPorts(Grid.Objects[num6, 29], go);
			this.MarkOverlappingPorts(Grid.Objects[num7, 29], go);
			Grid.Objects[num6, 29] = go;
			Grid.Objects[num7, 29] = go;
		}
		if (this.BuildLocationRule == BuildLocationRule.LogicBridge)
		{
			LogicPorts component = go.GetComponent<LogicPorts>();
			if (component != null && component.inputPortInfo != null)
			{
				LogicPorts.Port[] inputPortInfo = component.inputPortInfo;
				for (int j = 0; j < inputPortInfo.Length; j++)
				{
					CellOffset rotatedCellOffset6 = Rotatable.GetRotatedCellOffset(inputPortInfo[j].cellOffset, orientation);
					int num8 = Grid.OffsetCell(cell, rotatedCellOffset6);
					this.MarkOverlappingPorts(Grid.Objects[num8, (int)layer], go);
					Grid.Objects[num8, (int)layer] = go;
				}
			}
		}
		ISecondaryInput[] components = this.BuildingComplete.GetComponents<ISecondaryInput>();
		if (components != null)
		{
			foreach (ISecondaryInput secondaryInput in components)
			{
				for (int k = 0; k < 4; k++)
				{
					ConduitType conduitType = (ConduitType)k;
					if (conduitType != ConduitType.None && secondaryInput.HasSecondaryConduitType(conduitType))
					{
						ObjectLayer objectLayerForConduitType3 = Grid.GetObjectLayerForConduitType(conduitType);
						CellOffset rotatedCellOffset7 = Rotatable.GetRotatedCellOffset(secondaryInput.GetSecondaryConduitOffset(conduitType), orientation);
						int num9 = Grid.OffsetCell(cell, rotatedCellOffset7);
						this.MarkOverlappingPorts(Grid.Objects[num9, (int)objectLayerForConduitType3], go);
						Grid.Objects[num9, (int)objectLayerForConduitType3] = go;
					}
				}
			}
		}
		ISecondaryOutput[] components2 = this.BuildingComplete.GetComponents<ISecondaryOutput>();
		if (components2 != null)
		{
			foreach (ISecondaryOutput secondaryOutput in components2)
			{
				for (int l = 0; l < 4; l++)
				{
					ConduitType conduitType2 = (ConduitType)l;
					if (conduitType2 != ConduitType.None && secondaryOutput.HasSecondaryConduitType(conduitType2))
					{
						ObjectLayer objectLayerForConduitType4 = Grid.GetObjectLayerForConduitType(conduitType2);
						CellOffset rotatedCellOffset8 = Rotatable.GetRotatedCellOffset(secondaryOutput.GetSecondaryConduitOffset(conduitType2), orientation);
						int num10 = Grid.OffsetCell(cell, rotatedCellOffset8);
						this.MarkOverlappingPorts(Grid.Objects[num10, (int)objectLayerForConduitType4], go);
						Grid.Objects[num10, (int)objectLayerForConduitType4] = go;
					}
				}
			}
		}
	}

	// Token: 0x06003129 RID: 12585 RVA: 0x001053C0 File Offset: 0x001035C0
	public void MarkOverlappingPorts(GameObject existing, GameObject replaced)
	{
		if (existing == null)
		{
			if (replaced != null)
			{
				replaced.RemoveTag(GameTags.HasInvalidPorts);
				return;
			}
		}
		else if (existing != replaced)
		{
			existing.AddTag(GameTags.HasInvalidPorts);
		}
	}

	// Token: 0x0600312A RID: 12586 RVA: 0x001053F4 File Offset: 0x001035F4
	public void UnmarkArea(int cell, Orientation orientation, ObjectLayer layer, GameObject go)
	{
		if (cell == Grid.InvalidCell)
		{
			return;
		}
		for (int i = 0; i < this.PlacementOffsets.Length; i++)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.PlacementOffsets[i], orientation);
			int num = Grid.OffsetCell(cell, rotatedCellOffset);
			if (Grid.Objects[num, (int)layer] == go)
			{
				Grid.Objects[num, (int)layer] = null;
			}
		}
		if (this.InputConduitType != ConduitType.None)
		{
			CellOffset rotatedCellOffset2 = Rotatable.GetRotatedCellOffset(this.UtilityInputOffset, orientation);
			int num2 = Grid.OffsetCell(cell, rotatedCellOffset2);
			ObjectLayer objectLayerForConduitType = Grid.GetObjectLayerForConduitType(this.InputConduitType);
			if (Grid.Objects[num2, (int)objectLayerForConduitType] == go)
			{
				Grid.Objects[num2, (int)objectLayerForConduitType] = null;
			}
		}
		if (this.OutputConduitType != ConduitType.None)
		{
			CellOffset rotatedCellOffset3 = Rotatable.GetRotatedCellOffset(this.UtilityOutputOffset, orientation);
			int num3 = Grid.OffsetCell(cell, rotatedCellOffset3);
			ObjectLayer objectLayerForConduitType2 = Grid.GetObjectLayerForConduitType(this.OutputConduitType);
			if (Grid.Objects[num3, (int)objectLayerForConduitType2] == go)
			{
				Grid.Objects[num3, (int)objectLayerForConduitType2] = null;
			}
		}
		if (this.RequiresPowerInput)
		{
			CellOffset rotatedCellOffset4 = Rotatable.GetRotatedCellOffset(this.PowerInputOffset, orientation);
			int num4 = Grid.OffsetCell(cell, rotatedCellOffset4);
			if (Grid.Objects[num4, 29] == go)
			{
				Grid.Objects[num4, 29] = null;
			}
		}
		if (this.RequiresPowerOutput)
		{
			CellOffset rotatedCellOffset5 = Rotatable.GetRotatedCellOffset(this.PowerOutputOffset, orientation);
			int num5 = Grid.OffsetCell(cell, rotatedCellOffset5);
			if (Grid.Objects[num5, 29] == go)
			{
				Grid.Objects[num5, 29] = null;
			}
		}
		if (this.BuildLocationRule == BuildLocationRule.HighWattBridgeTile)
		{
			int num6;
			int num7;
			go.GetComponent<UtilityNetworkLink>().GetCells(cell, orientation, out num6, out num7);
			if (Grid.Objects[num6, 29] == go)
			{
				Grid.Objects[num6, 29] = null;
			}
			if (Grid.Objects[num7, 29] == go)
			{
				Grid.Objects[num7, 29] = null;
			}
		}
		ISecondaryInput[] components = this.BuildingComplete.GetComponents<ISecondaryInput>();
		if (components != null)
		{
			foreach (ISecondaryInput secondaryInput in components)
			{
				for (int k = 0; k < 4; k++)
				{
					ConduitType conduitType = (ConduitType)k;
					if (conduitType != ConduitType.None && secondaryInput.HasSecondaryConduitType(conduitType))
					{
						ObjectLayer objectLayerForConduitType3 = Grid.GetObjectLayerForConduitType(conduitType);
						CellOffset rotatedCellOffset6 = Rotatable.GetRotatedCellOffset(secondaryInput.GetSecondaryConduitOffset(conduitType), orientation);
						int num8 = Grid.OffsetCell(cell, rotatedCellOffset6);
						if (Grid.Objects[num8, (int)objectLayerForConduitType3] == go)
						{
							Grid.Objects[num8, (int)objectLayerForConduitType3] = null;
						}
					}
				}
			}
		}
		ISecondaryOutput[] components2 = this.BuildingComplete.GetComponents<ISecondaryOutput>();
		if (components2 != null)
		{
			foreach (ISecondaryOutput secondaryOutput in components2)
			{
				for (int l = 0; l < 4; l++)
				{
					ConduitType conduitType2 = (ConduitType)l;
					if (conduitType2 != ConduitType.None && secondaryOutput.HasSecondaryConduitType(conduitType2))
					{
						ObjectLayer objectLayerForConduitType4 = Grid.GetObjectLayerForConduitType(conduitType2);
						CellOffset rotatedCellOffset7 = Rotatable.GetRotatedCellOffset(secondaryOutput.GetSecondaryConduitOffset(conduitType2), orientation);
						int num9 = Grid.OffsetCell(cell, rotatedCellOffset7);
						if (Grid.Objects[num9, (int)objectLayerForConduitType4] == go)
						{
							Grid.Objects[num9, (int)objectLayerForConduitType4] = null;
						}
					}
				}
			}
		}
	}

	// Token: 0x0600312B RID: 12587 RVA: 0x00105735 File Offset: 0x00103935
	public int GetBuildingCell(int cell)
	{
		return cell + (this.WidthInCells - 1) / 2;
	}

	// Token: 0x0600312C RID: 12588 RVA: 0x00105743 File Offset: 0x00103943
	public Vector3 GetVisualizerOffset()
	{
		return Vector3.right * (0.5f * (float)((this.WidthInCells + 1) % 2));
	}

	// Token: 0x0600312D RID: 12589 RVA: 0x00105760 File Offset: 0x00103960
	public bool IsValidPlaceLocation(GameObject source_go, Vector3 pos, Orientation orientation, out string fail_reason)
	{
		int num = Grid.PosToCell(pos);
		return this.IsValidPlaceLocation(source_go, num, orientation, false, out fail_reason);
	}

	// Token: 0x0600312E RID: 12590 RVA: 0x00105780 File Offset: 0x00103980
	public bool IsValidPlaceLocation(GameObject source_go, Vector3 pos, Orientation orientation, bool replace_tile, out string fail_reason)
	{
		int num = Grid.PosToCell(pos);
		return this.IsValidPlaceLocation(source_go, num, orientation, replace_tile, out fail_reason);
	}

	// Token: 0x0600312F RID: 12591 RVA: 0x001057A1 File Offset: 0x001039A1
	public bool IsValidPlaceLocation(GameObject source_go, int cell, Orientation orientation, out string fail_reason)
	{
		return this.IsValidPlaceLocation(source_go, cell, orientation, false, out fail_reason);
	}

	// Token: 0x06003130 RID: 12592 RVA: 0x001057B0 File Offset: 0x001039B0
	public bool IsValidPlaceLocation(GameObject source_go, int cell, Orientation orientation, bool replace_tile, out string fail_reason)
	{
		if (!Grid.IsValidBuildingCell(cell))
		{
			fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL;
			return false;
		}
		if ((int)Grid.WorldIdx[cell] != ClusterManager.Instance.activeWorldId)
		{
			fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL;
			return false;
		}
		if (this.BuildLocationRule == BuildLocationRule.OnRocketEnvelope)
		{
			if (!BuildingDef.CheckFoundation(cell, orientation, this.BuildLocationRule, this.WidthInCells, this.HeightInCells, GameTags.RocketEnvelopeTile))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_ONROCKETENVELOPE;
				return false;
			}
		}
		else if (this.BuildLocationRule == BuildLocationRule.OnWall)
		{
			if (!BuildingDef.CheckFoundation(cell, orientation, this.BuildLocationRule, this.WidthInCells, this.HeightInCells, default(Tag)))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_WALL;
				return false;
			}
		}
		else if (this.BuildLocationRule == BuildLocationRule.InCorner)
		{
			if (!BuildingDef.CheckFoundation(cell, orientation, this.BuildLocationRule, this.WidthInCells, this.HeightInCells, default(Tag)))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_CORNER;
				return false;
			}
		}
		else if (this.BuildLocationRule == BuildLocationRule.WallFloor)
		{
			if (!BuildingDef.CheckFoundation(cell, orientation, this.BuildLocationRule, this.WidthInCells, this.HeightInCells, default(Tag)))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_CORNER_FLOOR;
				return false;
			}
		}
		else if (this.BuildLocationRule == BuildLocationRule.BelowRocketCeiling)
		{
			WorldContainer world = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[cell]);
			if ((float)(Grid.CellToXY(cell).y + 35 + source_go.GetComponent<Building>().Def.HeightInCells) >= world.maximumBounds.y - (float)Grid.TopBorderHeight)
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_BELOWROCKETCEILING;
				return false;
			}
		}
		return this.IsAreaClear(source_go, cell, orientation, this.ObjectLayer, this.TileLayer, replace_tile, out fail_reason);
	}

	// Token: 0x06003131 RID: 12593 RVA: 0x00105968 File Offset: 0x00103B68
	public bool IsValidReplaceLocation(Vector3 pos, Orientation orientation, ObjectLayer replace_layer, ObjectLayer obj_layer)
	{
		if (replace_layer == ObjectLayer.NumLayers)
		{
			return false;
		}
		bool flag = true;
		int num = Grid.PosToCell(pos);
		for (int i = 0; i < this.PlacementOffsets.Length; i++)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.PlacementOffsets[i], orientation);
			int num2 = Grid.OffsetCell(num, rotatedCellOffset);
			if (!Grid.IsValidBuildingCell(num2))
			{
				return false;
			}
			if (Grid.Objects[num2, (int)obj_layer] == null || Grid.Objects[num2, (int)replace_layer] != null)
			{
				flag = false;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06003132 RID: 12594 RVA: 0x001059F0 File Offset: 0x00103BF0
	public bool IsValidBuildLocation(GameObject source_go, Vector3 pos, Orientation orientation, bool replace_tile = false)
	{
		string text = "";
		return this.IsValidBuildLocation(source_go, pos, orientation, out text, replace_tile);
	}

	// Token: 0x06003133 RID: 12595 RVA: 0x00105A10 File Offset: 0x00103C10
	public bool IsValidBuildLocation(GameObject source_go, Vector3 pos, Orientation orientation, out string reason, bool replace_tile = false)
	{
		int num = Grid.PosToCell(pos);
		return this.IsValidBuildLocation(source_go, num, orientation, replace_tile, out reason);
	}

	// Token: 0x06003134 RID: 12596 RVA: 0x00105A34 File Offset: 0x00103C34
	public bool IsValidBuildLocation(GameObject source_go, int cell, Orientation orientation, bool replace_tile, out string fail_reason)
	{
		if (!Grid.IsValidBuildingCell(cell))
		{
			fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL;
			return false;
		}
		if (!this.IsAreaValid(cell, orientation, out fail_reason))
		{
			return false;
		}
		bool flag = true;
		fail_reason = null;
		switch (this.BuildLocationRule)
		{
		case BuildLocationRule.Anywhere:
		case BuildLocationRule.Conduit:
		case BuildLocationRule.OnFloorOrBuildingAttachPoint:
			flag = true;
			break;
		case BuildLocationRule.OnFloor:
		case BuildLocationRule.OnCeiling:
		case BuildLocationRule.OnFoundationRotatable:
			if (!BuildingDef.CheckFoundation(cell, orientation, this.BuildLocationRule, this.WidthInCells, this.HeightInCells, default(Tag)))
			{
				flag = false;
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_FLOOR;
			}
			break;
		case BuildLocationRule.OnFloorOverSpace:
			if (!BuildingDef.CheckFoundation(cell, orientation, this.BuildLocationRule, this.WidthInCells, this.HeightInCells, default(Tag)))
			{
				flag = false;
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_FLOOR;
			}
			else if (!BuildingDef.AreAllCellsValid(cell, orientation, this.WidthInCells, this.HeightInCells, (int check_cell) => global::World.Instance.zoneRenderData.GetSubWorldZoneType(check_cell) == SubWorld.ZoneType.Space))
			{
				flag = false;
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_SPACE;
			}
			break;
		case BuildLocationRule.OnWall:
			if (!BuildingDef.CheckFoundation(cell, orientation, this.BuildLocationRule, this.WidthInCells, this.HeightInCells, default(Tag)))
			{
				flag = false;
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_WALL;
			}
			break;
		case BuildLocationRule.InCorner:
			if (!BuildingDef.CheckFoundation(cell, orientation, this.BuildLocationRule, this.WidthInCells, this.HeightInCells, default(Tag)))
			{
				flag = false;
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_CORNER;
			}
			break;
		case BuildLocationRule.Tile:
		{
			flag = true;
			GameObject gameObject = Grid.Objects[cell, 27];
			if (gameObject != null)
			{
				Building component = gameObject.GetComponent<Building>();
				if (component != null && component.Def.BuildLocationRule == BuildLocationRule.NotInTiles)
				{
					flag = false;
				}
			}
			gameObject = Grid.Objects[cell, 2];
			if (gameObject != null)
			{
				Building component2 = gameObject.GetComponent<Building>();
				if (component2 != null && component2.Def.BuildLocationRule == BuildLocationRule.NotInTiles)
				{
					flag = replace_tile;
				}
			}
			break;
		}
		case BuildLocationRule.NotInTiles:
		{
			GameObject gameObject2 = Grid.Objects[cell, 9];
			flag = replace_tile || gameObject2 == null || gameObject2 == source_go;
			flag = flag && !Grid.HasDoor[cell];
			if (flag)
			{
				GameObject gameObject3 = Grid.Objects[cell, (int)this.ObjectLayer];
				if (gameObject3 != null)
				{
					if (this.ReplacementLayer == ObjectLayer.NumLayers)
					{
						flag = flag && (gameObject3 == null || gameObject3 == source_go);
					}
					else
					{
						Building component3 = gameObject3.GetComponent<Building>();
						flag = component3 == null || component3.Def.ReplacementLayer == this.ReplacementLayer;
					}
				}
			}
			fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_NOT_IN_TILES;
			break;
		}
		case BuildLocationRule.BuildingAttachPoint:
		{
			flag = false;
			int num = 0;
			while (num < Components.BuildingAttachPoints.Count && !flag)
			{
				for (int i = 0; i < Components.BuildingAttachPoints[num].points.Length; i++)
				{
					if (Components.BuildingAttachPoints[num].AcceptsAttachment(this.AttachmentSlotTag, Grid.OffsetCell(cell, this.attachablePosition)))
					{
						flag = true;
						break;
					}
				}
				num++;
			}
			fail_reason = string.Format(UI.TOOLTIPS.HELP_BUILDLOCATION_ATTACHPOINT, this.AttachmentSlotTag);
			break;
		}
		case BuildLocationRule.OnRocketEnvelope:
			if (!BuildingDef.CheckFoundation(cell, orientation, this.BuildLocationRule, this.WidthInCells, this.HeightInCells, GameTags.RocketEnvelopeTile))
			{
				flag = false;
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_ONROCKETENVELOPE;
			}
			break;
		case BuildLocationRule.WallFloor:
			if (!BuildingDef.CheckFoundation(cell, orientation, this.BuildLocationRule, this.WidthInCells, this.HeightInCells, default(Tag)))
			{
				flag = false;
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_CORNER_FLOOR;
			}
			break;
		}
		flag = flag && this.ArePowerPortsInValidPositions(source_go, cell, orientation, out fail_reason);
		return flag && this.AreConduitPortsInValidPositions(source_go, cell, orientation, out fail_reason);
	}

	// Token: 0x06003135 RID: 12597 RVA: 0x00105E68 File Offset: 0x00104068
	private bool IsAreaValid(int cell, Orientation orientation, out string fail_reason)
	{
		bool flag = true;
		fail_reason = null;
		for (int i = 0; i < this.PlacementOffsets.Length; i++)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.PlacementOffsets[i], orientation);
			if (!Grid.IsCellOffsetValid(cell, rotatedCellOffset))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL;
				flag = false;
				break;
			}
			int num = Grid.OffsetCell(cell, rotatedCellOffset);
			if (!Grid.IsValidBuildingCell(num))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL;
				flag = false;
				break;
			}
			if (Grid.Element[num].id == SimHashes.Unobtanium)
			{
				fail_reason = null;
				flag = false;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06003136 RID: 12598 RVA: 0x00105EF4 File Offset: 0x001040F4
	private bool ArePowerPortsInValidPositions(GameObject source_go, int cell, Orientation orientation, out string fail_reason)
	{
		fail_reason = null;
		if (source_go == null)
		{
			return true;
		}
		if (this.RequiresPowerInput)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.PowerInputOffset, orientation);
			int num = Grid.OffsetCell(cell, rotatedCellOffset);
			GameObject gameObject = Grid.Objects[num, 29];
			if (gameObject != null && gameObject != source_go)
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_WIRECONNECTORS_OVERLAP;
				return false;
			}
		}
		if (this.RequiresPowerOutput)
		{
			CellOffset rotatedCellOffset2 = Rotatable.GetRotatedCellOffset(this.PowerOutputOffset, orientation);
			int num2 = Grid.OffsetCell(cell, rotatedCellOffset2);
			GameObject gameObject2 = Grid.Objects[num2, 29];
			if (gameObject2 != null && gameObject2 != source_go)
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_WIRECONNECTORS_OVERLAP;
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003137 RID: 12599 RVA: 0x00105FB0 File Offset: 0x001041B0
	private bool AreConduitPortsInValidPositions(GameObject source_go, int cell, Orientation orientation, out string fail_reason)
	{
		fail_reason = null;
		if (source_go == null)
		{
			return true;
		}
		bool flag = true;
		if (this.InputConduitType != ConduitType.None)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.UtilityInputOffset, orientation);
			int num = Grid.OffsetCell(cell, rotatedCellOffset);
			flag = this.IsValidConduitConnection(source_go, this.InputConduitType, num, ref fail_reason);
		}
		if (flag && this.OutputConduitType != ConduitType.None)
		{
			CellOffset rotatedCellOffset2 = Rotatable.GetRotatedCellOffset(this.UtilityOutputOffset, orientation);
			int num2 = Grid.OffsetCell(cell, rotatedCellOffset2);
			flag = this.IsValidConduitConnection(source_go, this.OutputConduitType, num2, ref fail_reason);
		}
		Building component = source_go.GetComponent<Building>();
		if (flag && component)
		{
			ISecondaryInput[] components = component.Def.BuildingComplete.GetComponents<ISecondaryInput>();
			if (components != null)
			{
				foreach (ISecondaryInput secondaryInput in components)
				{
					for (int j = 0; j < 4; j++)
					{
						ConduitType conduitType = (ConduitType)j;
						if (conduitType != ConduitType.None && secondaryInput.HasSecondaryConduitType(conduitType))
						{
							CellOffset rotatedCellOffset3 = Rotatable.GetRotatedCellOffset(secondaryInput.GetSecondaryConduitOffset(conduitType), orientation);
							int num3 = Grid.OffsetCell(cell, rotatedCellOffset3);
							flag = this.IsValidConduitConnection(source_go, conduitType, num3, ref fail_reason);
						}
					}
				}
			}
		}
		if (flag)
		{
			ISecondaryOutput[] components2 = component.Def.BuildingComplete.GetComponents<ISecondaryOutput>();
			if (components2 != null)
			{
				foreach (ISecondaryOutput secondaryOutput in components2)
				{
					for (int k = 0; k < 4; k++)
					{
						ConduitType conduitType2 = (ConduitType)k;
						if (conduitType2 != ConduitType.None && secondaryOutput.HasSecondaryConduitType(conduitType2))
						{
							CellOffset rotatedCellOffset4 = Rotatable.GetRotatedCellOffset(secondaryOutput.GetSecondaryConduitOffset(conduitType2), orientation);
							int num4 = Grid.OffsetCell(cell, rotatedCellOffset4);
							flag = this.IsValidConduitConnection(source_go, conduitType2, num4, ref fail_reason);
						}
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x06003138 RID: 12600 RVA: 0x00106150 File Offset: 0x00104350
	private bool IsValidWireBridgeLocation(GameObject source_go, int cell, Orientation orientation, out string fail_reason)
	{
		UtilityNetworkLink component = source_go.GetComponent<UtilityNetworkLink>();
		if (component != null)
		{
			int num;
			int num2;
			component.GetCells(out num, out num2);
			if (Grid.Objects[num, 29] != null || Grid.Objects[num2, 29] != null)
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_WIRECONNECTORS_OVERLAP;
				return false;
			}
		}
		fail_reason = null;
		return true;
	}

	// Token: 0x06003139 RID: 12601 RVA: 0x001061B8 File Offset: 0x001043B8
	private bool IsValidHighWattBridgeLocation(GameObject source_go, int cell, Orientation orientation, out string fail_reason)
	{
		UtilityNetworkLink component = source_go.GetComponent<UtilityNetworkLink>();
		if (component != null)
		{
			if (!component.AreCellsValid(cell, orientation))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL;
				return false;
			}
			int num;
			int num2;
			component.GetCells(out num, out num2);
			if (Grid.Objects[num, 29] != null || Grid.Objects[num2, 29] != null)
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_WIRECONNECTORS_OVERLAP;
				return false;
			}
			if (Grid.Objects[num, 9] != null || Grid.Objects[num2, 9] != null)
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_HIGHWATT_NOT_IN_TILE;
				return false;
			}
			if (Grid.HasDoor[num] || Grid.HasDoor[num2])
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_HIGHWATT_NOT_IN_TILE;
				return false;
			}
			GameObject gameObject = Grid.Objects[num, 1];
			GameObject gameObject2 = Grid.Objects[num2, 1];
			if (gameObject != null || gameObject2 != null)
			{
				BuildingUnderConstruction buildingUnderConstruction = (gameObject ? gameObject.GetComponent<BuildingUnderConstruction>() : null);
				BuildingUnderConstruction buildingUnderConstruction2 = (gameObject2 ? gameObject2.GetComponent<BuildingUnderConstruction>() : null);
				if ((buildingUnderConstruction && buildingUnderConstruction.Def.BuildingComplete.GetComponent<Door>()) || (buildingUnderConstruction2 && buildingUnderConstruction2.Def.BuildingComplete.GetComponent<Door>()))
				{
					fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_HIGHWATT_NOT_IN_TILE;
					return false;
				}
			}
		}
		fail_reason = null;
		return true;
	}

	// Token: 0x0600313A RID: 12602 RVA: 0x00106344 File Offset: 0x00104544
	private bool AreLogicPortsInValidPositions(GameObject source_go, int cell, out string fail_reason)
	{
		fail_reason = null;
		if (source_go == null)
		{
			return true;
		}
		ReadOnlyCollection<ILogicUIElement> visElements = Game.Instance.logicCircuitManager.GetVisElements();
		LogicPorts component = source_go.GetComponent<LogicPorts>();
		if (component != null)
		{
			component.HackRefreshVisualizers();
			if (this.DoLogicPortsConflict(component.inputPorts, visElements) || this.DoLogicPortsConflict(component.outputPorts, visElements))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_LOGIC_PORTS_OBSTRUCTED;
				return false;
			}
		}
		else
		{
			LogicGateBase component2 = source_go.GetComponent<LogicGateBase>();
			if (component2 != null && (this.IsLogicPortObstructed(component2.InputCellOne, visElements) || this.IsLogicPortObstructed(component2.OutputCellOne, visElements) || ((component2.RequiresTwoInputs || component2.RequiresFourInputs) && this.IsLogicPortObstructed(component2.InputCellTwo, visElements)) || (component2.RequiresFourInputs && (this.IsLogicPortObstructed(component2.InputCellThree, visElements) || this.IsLogicPortObstructed(component2.InputCellFour, visElements))) || (component2.RequiresFourOutputs && (this.IsLogicPortObstructed(component2.OutputCellTwo, visElements) || this.IsLogicPortObstructed(component2.OutputCellThree, visElements) || this.IsLogicPortObstructed(component2.OutputCellFour, visElements)))))
			{
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_LOGIC_PORTS_OBSTRUCTED;
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600313B RID: 12603 RVA: 0x00106474 File Offset: 0x00104674
	private bool DoLogicPortsConflict(IList<ILogicUIElement> ports_a, IList<ILogicUIElement> ports_b)
	{
		if (ports_a == null || ports_b == null)
		{
			return false;
		}
		foreach (ILogicUIElement logicUIElement in ports_a)
		{
			int logicUICell = logicUIElement.GetLogicUICell();
			foreach (ILogicUIElement logicUIElement2 in ports_b)
			{
				if (logicUIElement != logicUIElement2 && logicUICell == logicUIElement2.GetLogicUICell())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600313C RID: 12604 RVA: 0x00106510 File Offset: 0x00104710
	private bool IsLogicPortObstructed(int cell, IList<ILogicUIElement> ports)
	{
		int num = 0;
		using (IEnumerator<ILogicUIElement> enumerator = ports.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetLogicUICell() == cell)
				{
					num++;
				}
			}
		}
		return num > 0;
	}

	// Token: 0x0600313D RID: 12605 RVA: 0x00106564 File Offset: 0x00104764
	private bool IsValidConduitConnection(GameObject source_go, ConduitType conduit_type, int utility_cell, ref string fail_reason)
	{
		bool flag = true;
		switch (conduit_type)
		{
		case ConduitType.Gas:
		{
			GameObject gameObject = Grid.Objects[utility_cell, 15];
			if (gameObject != null && gameObject != source_go)
			{
				flag = false;
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_GASPORTS_OVERLAP;
			}
			break;
		}
		case ConduitType.Liquid:
		{
			GameObject gameObject2 = Grid.Objects[utility_cell, 19];
			if (gameObject2 != null && gameObject2 != source_go)
			{
				flag = false;
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_LIQUIDPORTS_OVERLAP;
			}
			break;
		}
		case ConduitType.Solid:
		{
			GameObject gameObject3 = Grid.Objects[utility_cell, 23];
			if (gameObject3 != null && gameObject3 != source_go)
			{
				flag = false;
				fail_reason = UI.TOOLTIPS.HELP_BUILDLOCATION_SOLIDPORTS_OVERLAP;
			}
			break;
		}
		}
		return flag;
	}

	// Token: 0x0600313E RID: 12606 RVA: 0x0010661E File Offset: 0x0010481E
	public static int GetXOffset(int width)
	{
		return -(width - 1) / 2;
	}

	// Token: 0x0600313F RID: 12607 RVA: 0x00106628 File Offset: 0x00104828
	public static bool CheckFoundation(int cell, Orientation orientation, BuildLocationRule location_rule, int width, int height, Tag optionalFoundationRequiredTag = default(Tag))
	{
		if (location_rule == BuildLocationRule.OnWall)
		{
			return BuildingDef.CheckWallFoundation(cell, width, height, orientation != Orientation.FlipH);
		}
		if (location_rule == BuildLocationRule.InCorner)
		{
			return BuildingDef.CheckBaseFoundation(cell, orientation, BuildLocationRule.OnCeiling, width, height, optionalFoundationRequiredTag) && BuildingDef.CheckWallFoundation(cell, width, height, orientation != Orientation.FlipH);
		}
		if (location_rule == BuildLocationRule.WallFloor)
		{
			return BuildingDef.CheckBaseFoundation(cell, orientation, BuildLocationRule.OnFloor, width, height, optionalFoundationRequiredTag) && BuildingDef.CheckWallFoundation(cell, width, height, orientation != Orientation.FlipH);
		}
		return BuildingDef.CheckBaseFoundation(cell, orientation, location_rule, width, height, optionalFoundationRequiredTag);
	}

	// Token: 0x06003140 RID: 12608 RVA: 0x001066A4 File Offset: 0x001048A4
	public static bool CheckBaseFoundation(int cell, Orientation orientation, BuildLocationRule location_rule, int width, int height, Tag optionalFoundationRequiredTag = default(Tag))
	{
		int num = -(width - 1) / 2;
		int num2 = width / 2;
		for (int i = num; i <= num2; i++)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset((location_rule == BuildLocationRule.OnCeiling) ? new CellOffset(i, height) : new CellOffset(i, -1), orientation);
			int num3 = Grid.OffsetCell(cell, rotatedCellOffset);
			if (!Grid.IsValidBuildingCell(num3) || !Grid.Solid[num3])
			{
				return false;
			}
			if (optionalFoundationRequiredTag.IsValid && (!Grid.ObjectLayers[9].ContainsKey(num3) || !Grid.ObjectLayers[9][num3].HasTag(optionalFoundationRequiredTag)))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003141 RID: 12609 RVA: 0x00106734 File Offset: 0x00104934
	public static bool CheckWallFoundation(int cell, int width, int height, bool leftWall)
	{
		for (int i = 0; i < height; i++)
		{
			CellOffset cellOffset = new CellOffset(leftWall ? (-(width - 1) / 2 - 1) : (width / 2 + 1), i);
			int num = Grid.OffsetCell(cell, cellOffset);
			GameObject gameObject = Grid.Objects[num, 1];
			bool flag = false;
			if (gameObject != null)
			{
				BuildingUnderConstruction component = gameObject.GetComponent<BuildingUnderConstruction>();
				if (component != null && component.Def.IsFoundation)
				{
					flag = true;
				}
			}
			if (!Grid.IsValidBuildingCell(num) || (!Grid.Solid[num] && !flag))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003142 RID: 12610 RVA: 0x001067D0 File Offset: 0x001049D0
	public static bool AreAllCellsValid(int base_cell, Orientation orientation, int width, int height, Func<int, bool> valid_cell_check)
	{
		int num = -(width - 1) / 2;
		int num2 = width / 2;
		if (orientation == Orientation.FlipH)
		{
			int num3 = num;
			num = -num2;
			num2 = -num3;
		}
		for (int i = 0; i < height; i++)
		{
			for (int j = num; j <= num2; j++)
			{
				int num4 = Grid.OffsetCell(base_cell, j, i);
				if (!valid_cell_check(num4))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06003143 RID: 12611 RVA: 0x00106822 File Offset: 0x00104A22
	public Sprite GetUISprite(string animName = "ui", bool centered = false)
	{
		return Def.GetUISpriteFromMultiObjectAnim(this.AnimFiles[0], animName, centered, "");
	}

	// Token: 0x06003144 RID: 12612 RVA: 0x00106838 File Offset: 0x00104A38
	public void GenerateOffsets()
	{
		this.GenerateOffsets(this.WidthInCells, this.HeightInCells);
	}

	// Token: 0x06003145 RID: 12613 RVA: 0x0010684C File Offset: 0x00104A4C
	public void GenerateOffsets(int width, int height)
	{
		if (!BuildingDef.placementOffsetsCache.TryGetValue(new CellOffset(width, height), out this.PlacementOffsets))
		{
			int num = width / 2 - width + 1;
			this.PlacementOffsets = new CellOffset[width * height];
			for (int num2 = 0; num2 != height; num2++)
			{
				int num3 = num2 * width;
				for (int num4 = 0; num4 != width; num4++)
				{
					int num5 = num3 + num4;
					this.PlacementOffsets[num5].x = num4 + num;
					this.PlacementOffsets[num5].y = num2;
				}
			}
			BuildingDef.placementOffsetsCache.Add(new CellOffset(width, height), this.PlacementOffsets);
		}
	}

	// Token: 0x06003146 RID: 12614 RVA: 0x001068E8 File Offset: 0x00104AE8
	public void PostProcess()
	{
		this.CraftRecipe = new Recipe(this.BuildingComplete.PrefabID().Name, 1f, (SimHashes)0, this.Name, null, 0);
		this.CraftRecipe.Icon = this.UISprite;
		for (int i = 0; i < this.MaterialCategory.Length; i++)
		{
			Recipe.Ingredient ingredient = new Recipe.Ingredient(this.MaterialCategory[i], (float)((int)this.Mass[i]));
			this.CraftRecipe.Ingredients.Add(ingredient);
		}
		if (this.DecorBlockTileInfo != null)
		{
			this.DecorBlockTileInfo.PostProcess();
		}
		if (this.DecorPlaceBlockTileInfo != null)
		{
			this.DecorPlaceBlockTileInfo.PostProcess();
		}
		if (!this.Deprecated)
		{
			Db.Get().TechItems.AddTechItem(this.PrefabID, this.Name, this.Effect, new Func<string, bool, Sprite>(this.GetUISprite), this.RequiredDlcIds);
		}
	}

	// Token: 0x06003147 RID: 12615 RVA: 0x001069DC File Offset: 0x00104BDC
	public bool MaterialsAvailable(IList<Tag> selected_elements, WorldContainer world)
	{
		bool flag = true;
		foreach (Recipe.Ingredient ingredient in this.CraftRecipe.GetAllIngredients(selected_elements))
		{
			if (world.worldInventory.GetAmount(ingredient.tag, true) < ingredient.amount)
			{
				flag = false;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06003148 RID: 12616 RVA: 0x00106A2C File Offset: 0x00104C2C
	public bool CheckRequiresBuildingCellVisualizer()
	{
		return this.CheckRequiresPowerInput() || this.CheckRequiresPowerOutput() || this.CheckRequiresGasInput() || this.CheckRequiresGasOutput() || this.CheckRequiresLiquidInput() || this.CheckRequiresLiquidOutput() || this.CheckRequiresSolidInput() || this.CheckRequiresSolidOutput() || this.CheckRequiresHighEnergyParticleInput() || this.CheckRequiresHighEnergyParticleOutput() || this.DiseaseCellVisName != null;
	}

	// Token: 0x06003149 RID: 12617 RVA: 0x00106A94 File Offset: 0x00104C94
	public bool CheckRequiresPowerInput()
	{
		return this.RequiresPowerInput;
	}

	// Token: 0x0600314A RID: 12618 RVA: 0x00106A9C File Offset: 0x00104C9C
	public bool CheckRequiresPowerOutput()
	{
		return this.RequiresPowerOutput;
	}

	// Token: 0x0600314B RID: 12619 RVA: 0x00106AA4 File Offset: 0x00104CA4
	public bool CheckRequiresGasInput()
	{
		return this.InputConduitType == ConduitType.Gas;
	}

	// Token: 0x0600314C RID: 12620 RVA: 0x00106AAF File Offset: 0x00104CAF
	public bool CheckRequiresGasOutput()
	{
		return this.OutputConduitType == ConduitType.Gas;
	}

	// Token: 0x0600314D RID: 12621 RVA: 0x00106ABA File Offset: 0x00104CBA
	public bool CheckRequiresLiquidInput()
	{
		return this.InputConduitType == ConduitType.Liquid;
	}

	// Token: 0x0600314E RID: 12622 RVA: 0x00106AC5 File Offset: 0x00104CC5
	public bool CheckRequiresLiquidOutput()
	{
		return this.OutputConduitType == ConduitType.Liquid;
	}

	// Token: 0x0600314F RID: 12623 RVA: 0x00106AD0 File Offset: 0x00104CD0
	public bool CheckRequiresSolidInput()
	{
		return this.InputConduitType == ConduitType.Solid;
	}

	// Token: 0x06003150 RID: 12624 RVA: 0x00106ADB File Offset: 0x00104CDB
	public bool CheckRequiresSolidOutput()
	{
		return this.OutputConduitType == ConduitType.Solid;
	}

	// Token: 0x06003151 RID: 12625 RVA: 0x00106AE6 File Offset: 0x00104CE6
	public bool CheckRequiresHighEnergyParticleInput()
	{
		return this.UseHighEnergyParticleInputPort;
	}

	// Token: 0x06003152 RID: 12626 RVA: 0x00106AEE File Offset: 0x00104CEE
	public bool CheckRequiresHighEnergyParticleOutput()
	{
		return this.UseHighEnergyParticleOutputPort;
	}

	// Token: 0x06003153 RID: 12627 RVA: 0x00106AF6 File Offset: 0x00104CF6
	public void AddFacade(string db_facade_id)
	{
		if (this.AvailableFacades == null)
		{
			this.AvailableFacades = new List<string>();
		}
		if (!this.AvailableFacades.Contains(db_facade_id))
		{
			this.AvailableFacades.Add(db_facade_id);
		}
	}

	// Token: 0x04001DB1 RID: 7601
	public string[] RequiredDlcIds;

	// Token: 0x04001DB2 RID: 7602
	public float EnergyConsumptionWhenActive;

	// Token: 0x04001DB3 RID: 7603
	public float GeneratorWattageRating;

	// Token: 0x04001DB4 RID: 7604
	public float GeneratorBaseCapacity;

	// Token: 0x04001DB5 RID: 7605
	public float MassForTemperatureModification;

	// Token: 0x04001DB6 RID: 7606
	public float ExhaustKilowattsWhenActive;

	// Token: 0x04001DB7 RID: 7607
	public float SelfHeatKilowattsWhenActive;

	// Token: 0x04001DB8 RID: 7608
	public float BaseMeltingPoint;

	// Token: 0x04001DB9 RID: 7609
	public float ConstructionTime;

	// Token: 0x04001DBA RID: 7610
	public float WorkTime;

	// Token: 0x04001DBB RID: 7611
	public float ThermalConductivity = 1f;

	// Token: 0x04001DBC RID: 7612
	public int WidthInCells;

	// Token: 0x04001DBD RID: 7613
	public int HeightInCells;

	// Token: 0x04001DBE RID: 7614
	public int HitPoints;

	// Token: 0x04001DBF RID: 7615
	public bool RequiresPowerInput;

	// Token: 0x04001DC0 RID: 7616
	public bool AddLogicPowerPort = true;

	// Token: 0x04001DC1 RID: 7617
	public bool RequiresPowerOutput;

	// Token: 0x04001DC2 RID: 7618
	public bool UseWhitePowerOutputConnectorColour;

	// Token: 0x04001DC3 RID: 7619
	public CellOffset ElectricalArrowOffset;

	// Token: 0x04001DC4 RID: 7620
	public ConduitType InputConduitType;

	// Token: 0x04001DC5 RID: 7621
	public ConduitType OutputConduitType;

	// Token: 0x04001DC6 RID: 7622
	public bool ModifiesTemperature;

	// Token: 0x04001DC7 RID: 7623
	public bool Floodable = true;

	// Token: 0x04001DC8 RID: 7624
	public bool Disinfectable = true;

	// Token: 0x04001DC9 RID: 7625
	public bool Entombable = true;

	// Token: 0x04001DCA RID: 7626
	public bool Replaceable = true;

	// Token: 0x04001DCB RID: 7627
	public bool Invincible;

	// Token: 0x04001DCC RID: 7628
	public bool Overheatable = true;

	// Token: 0x04001DCD RID: 7629
	public bool Repairable = true;

	// Token: 0x04001DCE RID: 7630
	public float OverheatTemperature = 348.15f;

	// Token: 0x04001DCF RID: 7631
	public float FatalHot = 533.15f;

	// Token: 0x04001DD0 RID: 7632
	public bool Breakable;

	// Token: 0x04001DD1 RID: 7633
	public bool ContinuouslyCheckFoundation;

	// Token: 0x04001DD2 RID: 7634
	public bool IsFoundation;

	// Token: 0x04001DD3 RID: 7635
	[Obsolete]
	public bool isSolidTile;

	// Token: 0x04001DD4 RID: 7636
	public bool DragBuild;

	// Token: 0x04001DD5 RID: 7637
	public bool UseStructureTemperature = true;

	// Token: 0x04001DD6 RID: 7638
	public global::Action HotKey = global::Action.NumActions;

	// Token: 0x04001DD7 RID: 7639
	public CellOffset attachablePosition = new CellOffset(0, 0);

	// Token: 0x04001DD8 RID: 7640
	public bool CanMove;

	// Token: 0x04001DD9 RID: 7641
	public bool Cancellable = true;

	// Token: 0x04001DDA RID: 7642
	public bool OnePerWorld;

	// Token: 0x04001DDB RID: 7643
	public bool PlayConstructionSounds = true;

	// Token: 0x04001DDC RID: 7644
	public List<Tag> ReplacementTags;

	// Token: 0x04001DDD RID: 7645
	public List<ObjectLayer> ReplacementCandidateLayers;

	// Token: 0x04001DDE RID: 7646
	public List<ObjectLayer> EquivalentReplacementLayers;

	// Token: 0x04001DDF RID: 7647
	[HashedEnum]
	[NonSerialized]
	public HashedString ViewMode = OverlayModes.None.ID;

	// Token: 0x04001DE0 RID: 7648
	public BuildLocationRule BuildLocationRule;

	// Token: 0x04001DE1 RID: 7649
	public ObjectLayer ObjectLayer = ObjectLayer.Building;

	// Token: 0x04001DE2 RID: 7650
	public ObjectLayer TileLayer = ObjectLayer.NumLayers;

	// Token: 0x04001DE3 RID: 7651
	public ObjectLayer ReplacementLayer = ObjectLayer.NumLayers;

	// Token: 0x04001DE4 RID: 7652
	public string DiseaseCellVisName;

	// Token: 0x04001DE5 RID: 7653
	public string[] MaterialCategory;

	// Token: 0x04001DE6 RID: 7654
	public string AudioCategory = "Metal";

	// Token: 0x04001DE7 RID: 7655
	public string AudioSize = "medium";

	// Token: 0x04001DE8 RID: 7656
	public float[] Mass;

	// Token: 0x04001DE9 RID: 7657
	public bool AlwaysOperational;

	// Token: 0x04001DEA RID: 7658
	public List<LogicPorts.Port> LogicInputPorts;

	// Token: 0x04001DEB RID: 7659
	public List<LogicPorts.Port> LogicOutputPorts;

	// Token: 0x04001DEC RID: 7660
	public bool Upgradeable;

	// Token: 0x04001DED RID: 7661
	public float BaseTimeUntilRepair = 600f;

	// Token: 0x04001DEE RID: 7662
	public bool ShowInBuildMenu = true;

	// Token: 0x04001DEF RID: 7663
	public bool DebugOnly;

	// Token: 0x04001DF0 RID: 7664
	public PermittedRotations PermittedRotations;

	// Token: 0x04001DF1 RID: 7665
	public Orientation InitialOrientation;

	// Token: 0x04001DF2 RID: 7666
	public bool Deprecated;

	// Token: 0x04001DF3 RID: 7667
	public bool UseHighEnergyParticleInputPort;

	// Token: 0x04001DF4 RID: 7668
	public bool UseHighEnergyParticleOutputPort;

	// Token: 0x04001DF5 RID: 7669
	public CellOffset HighEnergyParticleInputOffset;

	// Token: 0x04001DF6 RID: 7670
	public CellOffset HighEnergyParticleOutputOffset;

	// Token: 0x04001DF7 RID: 7671
	public CellOffset PowerInputOffset;

	// Token: 0x04001DF8 RID: 7672
	public CellOffset PowerOutputOffset;

	// Token: 0x04001DF9 RID: 7673
	public CellOffset UtilityInputOffset = new CellOffset(0, 1);

	// Token: 0x04001DFA RID: 7674
	public CellOffset UtilityOutputOffset = new CellOffset(1, 0);

	// Token: 0x04001DFB RID: 7675
	public Grid.SceneLayer SceneLayer = Grid.SceneLayer.Building;

	// Token: 0x04001DFC RID: 7676
	public Grid.SceneLayer ForegroundLayer = Grid.SceneLayer.BuildingFront;

	// Token: 0x04001DFD RID: 7677
	public string RequiredAttribute = "";

	// Token: 0x04001DFE RID: 7678
	public int RequiredAttributeLevel;

	// Token: 0x04001DFF RID: 7679
	public List<Descriptor> EffectDescription;

	// Token: 0x04001E00 RID: 7680
	public float MassTier;

	// Token: 0x04001E01 RID: 7681
	public float HeatTier;

	// Token: 0x04001E02 RID: 7682
	public float ConstructionTimeTier;

	// Token: 0x04001E03 RID: 7683
	public string PrimaryUse;

	// Token: 0x04001E04 RID: 7684
	public string SecondaryUse;

	// Token: 0x04001E05 RID: 7685
	public string PrimarySideEffect;

	// Token: 0x04001E06 RID: 7686
	public string SecondarySideEffect;

	// Token: 0x04001E07 RID: 7687
	public Recipe CraftRecipe;

	// Token: 0x04001E08 RID: 7688
	public Sprite UISprite;

	// Token: 0x04001E09 RID: 7689
	public bool isKAnimTile;

	// Token: 0x04001E0A RID: 7690
	public bool isUtility;

	// Token: 0x04001E0B RID: 7691
	public KAnimFile[] AnimFiles;

	// Token: 0x04001E0C RID: 7692
	public string DefaultAnimState = "off";

	// Token: 0x04001E0D RID: 7693
	public bool BlockTileIsTransparent;

	// Token: 0x04001E0E RID: 7694
	public TextureAtlas BlockTileAtlas;

	// Token: 0x04001E0F RID: 7695
	public TextureAtlas BlockTilePlaceAtlas;

	// Token: 0x04001E10 RID: 7696
	public TextureAtlas BlockTileShineAtlas;

	// Token: 0x04001E11 RID: 7697
	public Material BlockTileMaterial;

	// Token: 0x04001E12 RID: 7698
	public BlockTileDecorInfo DecorBlockTileInfo;

	// Token: 0x04001E13 RID: 7699
	public BlockTileDecorInfo DecorPlaceBlockTileInfo;

	// Token: 0x04001E14 RID: 7700
	public List<Klei.AI.Attribute> attributes = new List<Klei.AI.Attribute>();

	// Token: 0x04001E15 RID: 7701
	public List<AttributeModifier> attributeModifiers = new List<AttributeModifier>();

	// Token: 0x04001E16 RID: 7702
	public Tag AttachmentSlotTag;

	// Token: 0x04001E17 RID: 7703
	public bool PreventIdleTraversalPastBuilding;

	// Token: 0x04001E18 RID: 7704
	public GameObject BuildingComplete;

	// Token: 0x04001E19 RID: 7705
	public GameObject BuildingPreview;

	// Token: 0x04001E1A RID: 7706
	public GameObject BuildingUnderConstruction;

	// Token: 0x04001E1B RID: 7707
	public CellOffset[] PlacementOffsets;

	// Token: 0x04001E1C RID: 7708
	public CellOffset[] ConstructionOffsetFilter;

	// Token: 0x04001E1D RID: 7709
	public static CellOffset[] ConstructionOffsetFilter_OneDown = new CellOffset[]
	{
		new CellOffset(0, -1)
	};

	// Token: 0x04001E1E RID: 7710
	public float BaseDecor;

	// Token: 0x04001E1F RID: 7711
	public float BaseDecorRadius;

	// Token: 0x04001E20 RID: 7712
	public int BaseNoisePollution;

	// Token: 0x04001E21 RID: 7713
	public int BaseNoisePollutionRadius;

	// Token: 0x04001E22 RID: 7714
	public List<string> AvailableFacades = new List<string>();

	// Token: 0x04001E23 RID: 7715
	private static Dictionary<CellOffset, CellOffset[]> placementOffsetsCache = new Dictionary<CellOffset, CellOffset[]>();
}
