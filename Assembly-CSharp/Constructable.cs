using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200045F RID: 1119
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Constructable")]
public class Constructable : Workable, ISaveLoadable
{
	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00083846 File Offset: 0x00081A46
	public Recipe Recipe
	{
		get
		{
			return this.building.Def.CraftRecipe;
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00083858 File Offset: 0x00081A58
	// (set) Token: 0x060018CA RID: 6346 RVA: 0x00083860 File Offset: 0x00081A60
	public IList<Tag> SelectedElementsTags
	{
		get
		{
			return this.selectedElementsTags;
		}
		set
		{
			if (this.selectedElementsTags == null || this.selectedElementsTags.Length != value.Count)
			{
				this.selectedElementsTags = new Tag[value.Count];
			}
			value.CopyTo(this.selectedElementsTags, 0);
		}
	}

	// Token: 0x060018CB RID: 6347 RVA: 0x00083898 File Offset: 0x00081A98
	public override string GetConversationTopic()
	{
		return this.building.Def.PrefabID;
	}

	// Token: 0x060018CC RID: 6348 RVA: 0x000838AC File Offset: 0x00081AAC
	protected override void OnCompleteWork(Worker worker)
	{
		float num = 0f;
		float num2 = 0f;
		bool flag = true;
		foreach (GameObject gameObject in this.storage.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null))
				{
					num += component.Mass;
					num2 += component.Temperature * component.Mass;
					flag = flag && component.HasTag(GameTags.Liquifiable);
				}
			}
		}
		if (num <= 0f)
		{
			DebugUtil.LogWarningArgs(base.gameObject, new object[]
			{
				"uhhh this constructable is about to generate a nan",
				"Item Count: ",
				this.storage.items.Count
			});
			return;
		}
		if (flag)
		{
			this.initialTemperature = Mathf.Min(num2 / num, 318.15f);
		}
		else
		{
			this.initialTemperature = Mathf.Clamp(num2 / num, 288.15f, 318.15f);
		}
		KAnimGraphTileVisualizer component2 = base.GetComponent<KAnimGraphTileVisualizer>();
		UtilityConnections connections = ((component2 == null) ? ((UtilityConnections)0) : component2.Connections);
		bool flag2 = true;
		if (this.IsReplacementTile)
		{
			int num3 = Grid.PosToCell(base.transform.GetLocalPosition());
			GameObject replacementCandidate = this.building.Def.GetReplacementCandidate(num3);
			if (replacementCandidate != null)
			{
				flag2 = false;
				SimCellOccupier component3 = replacementCandidate.GetComponent<SimCellOccupier>();
				if (component3 != null)
				{
					component3.DestroySelf(delegate
					{
						if (this != null && this.gameObject != null)
						{
							this.FinishConstruction(connections, worker);
						}
					});
				}
				else
				{
					Conduit component4 = replacementCandidate.GetComponent<Conduit>();
					if (component4 != null)
					{
						component4.GetFlowManager().MarkForReplacement(num3);
					}
					BuildingComplete component5 = replacementCandidate.GetComponent<BuildingComplete>();
					if (component5 != null)
					{
						component5.Subscribe(-21016276, delegate(object data)
						{
							this.FinishConstruction(connections, worker);
						});
					}
					else
					{
						global::Debug.LogWarning("Why am I trying to replace a: " + replacementCandidate.name);
						this.FinishConstruction(connections, worker);
					}
				}
				KAnimGraphTileVisualizer component6 = replacementCandidate.GetComponent<KAnimGraphTileVisualizer>();
				if (component6 != null)
				{
					component6.skipCleanup = true;
				}
				Deconstructable component7 = replacementCandidate.GetComponent<Deconstructable>();
				if (component7 != null)
				{
					component7.SpawnItemsFromConstruction();
				}
				replacementCandidate.Trigger(1606648047, this.building.Def.TileLayer);
				replacementCandidate.DeleteObject();
			}
		}
		if (flag2)
		{
			this.FinishConstruction(connections, worker);
		}
		PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Building, base.GetComponent<KSelectable>().GetName(), base.transform, 1.5f, false);
	}

	// Token: 0x060018CD RID: 6349 RVA: 0x00083B84 File Offset: 0x00081D84
	private void FinishConstruction(UtilityConnections connections, Worker workerForGameplayEvent)
	{
		Rotatable component = base.GetComponent<Rotatable>();
		Orientation orientation = ((component != null) ? component.GetOrientation() : Orientation.Neutral);
		int num = Grid.PosToCell(base.transform.GetLocalPosition());
		this.UnmarkArea();
		GameObject gameObject = this.building.Def.Build(num, orientation, this.storage, this.selectedElementsTags, this.initialTemperature, base.GetComponent<BuildingFacade>().CurrentFacade, true, GameClock.Instance.GetTime());
		BonusEvent.GameplayEventData gameplayEventData = new BonusEvent.GameplayEventData();
		gameplayEventData.building = gameObject.GetComponent<BuildingComplete>();
		gameplayEventData.workable = this;
		gameplayEventData.worker = workerForGameplayEvent;
		gameplayEventData.eventTrigger = GameHashes.NewBuilding;
		GameplayEventManager.Instance.Trigger(-1661515756, gameplayEventData);
		gameObject.transform.rotation = base.transform.rotation;
		Rotatable component2 = gameObject.GetComponent<Rotatable>();
		if (component2 != null)
		{
			component2.SetOrientation(orientation);
		}
		KAnimGraphTileVisualizer component3 = base.GetComponent<KAnimGraphTileVisualizer>();
		if (component3 != null)
		{
			gameObject.GetComponent<KAnimGraphTileVisualizer>().Connections = connections;
			component3.skipCleanup = true;
		}
		KSelectable component4 = base.GetComponent<KSelectable>();
		if (component4 != null && component4.IsSelected && gameObject.GetComponent<KSelectable>() != null)
		{
			component4.Unselect();
			if (PlayerController.Instance.ActiveTool.name == "SelectTool")
			{
				((SelectTool)PlayerController.Instance.ActiveTool).SelectNextFrame(gameObject.GetComponent<KSelectable>(), false);
			}
		}
		gameObject.Trigger(2121280625, this);
		this.storage.ConsumeAllIgnoringDisease();
		this.finished = true;
		this.DeleteObject();
	}

	// Token: 0x060018CE RID: 6350 RVA: 0x00083D20 File Offset: 0x00081F20
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.invalidLocation = new Notification(MISC.NOTIFICATIONS.INVALIDCONSTRUCTIONLOCATION.NAME, NotificationType.BadMinor, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.INVALIDCONSTRUCTIONLOCATION.TOOLTIP + notificationList.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false);
		this.faceTargetWhenWorking = true;
		base.Subscribe<Constructable>(-1432940121, Constructable.OnReachableChangedDelegate);
		if (this.rotatable == null)
		{
			this.MarkArea();
		}
		if (Db.Get().TechItems.GetTechTierForItem(this.building.Def.PrefabID) > 2)
		{
			this.requireMinionToWork = true;
		}
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Building;
		this.workingStatusItem = null;
		this.attributeConverter = Db.Get().AttributeConverters.ConstructionSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.minimumAttributeMultiplier = 0.75f;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Building.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		Prioritizable.AddRef(base.gameObject);
		this.synchronizeAnims = false;
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x00083E78 File Offset: 0x00082078
	protected override void OnSpawn()
	{
		base.OnSpawn();
		CellOffset[][] array = OffsetGroups.InvertedStandardTable;
		if (this.building.Def.IsTilePiece)
		{
			array = OffsetGroups.InvertedStandardTableWithCorners;
		}
		CellOffset[] array2 = this.building.Def.PlacementOffsets;
		if (this.rotatable != null)
		{
			array2 = new CellOffset[this.building.Def.PlacementOffsets.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = this.rotatable.GetRotatedCellOffset(this.building.Def.PlacementOffsets[i]);
			}
		}
		CellOffset[][] array3 = OffsetGroups.BuildReachabilityTable(array2, array, this.building.Def.ConstructionOffsetFilter);
		base.SetOffsetTable(array3);
		this.storage.SetOffsetTable(array3);
		base.Subscribe<Constructable>(2127324410, Constructable.OnCancelDelegate);
		if (this.rotatable != null)
		{
			this.MarkArea();
		}
		this.fetchList = new FetchList2(this.storage, Db.Get().ChoreTypes.BuildFetch);
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		Element element = ElementLoader.GetElement(this.SelectedElementsTags[0]);
		global::Debug.Assert(element != null, "Missing primary element for Constructable");
		component.ElementID = element.id;
		component.Temperature = (component.Temperature = 293.15f);
		foreach (Recipe.Ingredient ingredient in this.Recipe.GetAllIngredients(this.selectedElementsTags))
		{
			this.fetchList.Add(ingredient.tag, null, ingredient.amount, Operational.State.None);
			MaterialNeeds.UpdateNeed(ingredient.tag, ingredient.amount, base.gameObject.GetMyWorldId());
		}
		if (!this.building.Def.IsTilePiece)
		{
			base.gameObject.layer = LayerMask.NameToLayer("Construction");
		}
		this.building.RunOnArea(delegate(int offset_cell)
		{
			if (base.gameObject.GetComponent<ConduitBridge>() == null)
			{
				GameObject gameObject3 = Grid.Objects[offset_cell, 7];
				if (gameObject3 != null)
				{
					gameObject3.DeleteObject();
				}
			}
		});
		if (this.IsReplacementTile && this.building.Def.ReplacementLayer != ObjectLayer.NumLayers)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			GameObject gameObject = Grid.Objects[num, (int)this.building.Def.ReplacementLayer];
			if (gameObject == null || gameObject == base.gameObject)
			{
				Grid.Objects[num, (int)this.building.Def.ReplacementLayer] = base.gameObject;
				if (base.gameObject.GetComponent<SimCellOccupier>() != null)
				{
					int num2 = LayerMask.NameToLayer("Overlay");
					World.Instance.blockTileRenderer.AddBlock(num2, this.building.Def, this.IsReplacementTile, SimHashes.Void, num);
				}
				TileVisualizer.RefreshCell(num, this.building.Def.TileLayer, this.building.Def.ReplacementLayer);
			}
			else
			{
				global::Debug.LogError("multiple replacement tiles on the same cell!");
				Util.KDestroyGameObject(base.gameObject);
			}
			GameObject gameObject2 = Grid.Objects[num, (int)this.building.Def.ObjectLayer];
			if (gameObject2 != null)
			{
				Deconstructable component2 = gameObject2.GetComponent<Deconstructable>();
				if (component2 != null)
				{
					component2.CancelDeconstruction();
				}
			}
		}
		bool flag = this.building.Def.BuildingComplete.GetComponent<Ladder>();
		this.waitForFetchesBeforeDigging = flag || this.building.Def.BuildingComplete.GetComponent<SimCellOccupier>() || this.building.Def.BuildingComplete.GetComponent<Door>() || this.building.Def.BuildingComplete.GetComponent<LiquidPumpingStation>();
		if (flag)
		{
			int num3 = 0;
			int num4 = 0;
			Grid.CellToXY(Grid.PosToCell(this), out num3, out num4);
			int num5 = num4 - 3;
			this.ladderDetectionExtents = new Extents(num3, num5, 1, 5);
			this.ladderParititonerEntry = GameScenePartitioner.Instance.Add("Constructable.OnNearbyBuildingLayerChanged", base.gameObject, this.ladderDetectionExtents, GameScenePartitioner.Instance.objectLayers[1], new Action<object>(this.OnNearbyBuildingLayerChanged));
			this.OnNearbyBuildingLayerChanged(null);
		}
		this.fetchList.Submit(new System.Action(this.OnFetchListComplete), true);
		this.PlaceDiggables();
		new ReachabilityMonitor.Instance(this).StartSM();
		base.Subscribe<Constructable>(493375141, Constructable.OnRefreshUserMenuDelegate);
		Prioritizable component3 = base.GetComponent<Prioritizable>();
		Prioritizable prioritizable = component3;
		prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Combine(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.OnPriorityChanged));
		this.OnPriorityChanged(component3.GetMasterPriority());
	}

	// Token: 0x060018D0 RID: 6352 RVA: 0x00084330 File Offset: 0x00082530
	private void OnPriorityChanged(PrioritySetting priority)
	{
		this.building.RunOnArea(delegate(int cell)
		{
			Diggable diggable = Diggable.GetDiggable(cell);
			if (diggable != null)
			{
				diggable.GetComponent<Prioritizable>().SetMasterPriority(priority);
			}
		});
	}

	// Token: 0x060018D1 RID: 6353 RVA: 0x00084364 File Offset: 0x00082564
	private void MarkArea()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		BuildingDef def = this.building.Def;
		Orientation orientation = this.building.Orientation;
		ObjectLayer objectLayer = (this.IsReplacementTile ? def.ReplacementLayer : def.ObjectLayer);
		def.MarkArea(num, orientation, objectLayer, base.gameObject);
		if (def.IsTilePiece)
		{
			if (Grid.Objects[num, (int)def.TileLayer] == null)
			{
				def.MarkArea(num, orientation, def.TileLayer, base.gameObject);
				def.RunOnArea(num, orientation, delegate(int c)
				{
					TileVisualizer.RefreshCell(c, def.TileLayer, def.ReplacementLayer);
				});
			}
			Grid.IsTileUnderConstruction[num] = true;
		}
	}

	// Token: 0x060018D2 RID: 6354 RVA: 0x00084448 File Offset: 0x00082648
	private void UnmarkArea()
	{
		if (this.unmarked)
		{
			return;
		}
		this.unmarked = true;
		int num = Grid.PosToCell(base.transform.GetPosition());
		BuildingDef def = this.building.Def;
		ObjectLayer objectLayer = (this.IsReplacementTile ? this.building.Def.ReplacementLayer : this.building.Def.ObjectLayer);
		def.UnmarkArea(num, this.building.Orientation, objectLayer, base.gameObject);
		if (def.IsTilePiece)
		{
			Grid.IsTileUnderConstruction[num] = false;
		}
	}

	// Token: 0x060018D3 RID: 6355 RVA: 0x000844D8 File Offset: 0x000826D8
	private void OnNearbyBuildingLayerChanged(object data)
	{
		this.hasLadderNearby = false;
		for (int i = this.ladderDetectionExtents.y; i < this.ladderDetectionExtents.y + this.ladderDetectionExtents.height; i++)
		{
			int num = Grid.OffsetCell(0, this.ladderDetectionExtents.x, i);
			if (Grid.IsValidCell(num))
			{
				GameObject gameObject = null;
				Grid.ObjectLayers[1].TryGetValue(num, out gameObject);
				if (gameObject != null && gameObject.GetComponent<Ladder>() != null)
				{
					this.hasLadderNearby = true;
					return;
				}
			}
		}
	}

	// Token: 0x060018D4 RID: 6356 RVA: 0x00084564 File Offset: 0x00082764
	private bool IsWire()
	{
		return this.building.Def.name.Contains("Wire");
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x00084580 File Offset: 0x00082780
	public bool IconConnectionAnimation(float delay, int connectionCount, string defName, string soundName)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (this.building.Def.Name.Contains(defName))
		{
			Building building = null;
			GameObject gameObject = Grid.Objects[num, 1];
			if (gameObject != null)
			{
				building = gameObject.GetComponent<Building>();
			}
			if (building != null)
			{
				bool flag = this.IsWire();
				int num2 = (flag ? building.GetPowerInputCell() : building.GetUtilityInputCell());
				int num3 = (flag ? num2 : building.GetUtilityOutputCell());
				if (num == num2 || num == num3)
				{
					BuildingCellVisualizer component = building.gameObject.GetComponent<BuildingCellVisualizer>();
					if (component != null && (flag ? component.RequiresPower : component.RequiresUtilityConnection))
					{
						component.ConnectedEventWithDelay(delay, connectionCount, num, soundName);
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x00084650 File Offset: 0x00082850
	protected override void OnCleanUp()
	{
		if (this.IsReplacementTile && this.building.Def.isKAnimTile)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			GameObject gameObject = Grid.Objects[num, (int)this.building.Def.ReplacementLayer];
			if (gameObject == base.gameObject && gameObject.GetComponent<SimCellOccupier>() != null)
			{
				World.Instance.blockTileRenderer.RemoveBlock(this.building.Def, this.IsReplacementTile, SimHashes.Void, num);
			}
		}
		GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.digPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.ladderParititonerEntry);
		SaveLoadRoot component = base.GetComponent<SaveLoadRoot>();
		if (component != null)
		{
			SaveLoader.Instance.saveManager.Unregister(component);
		}
		if (this.fetchList != null)
		{
			this.fetchList.Cancel("Constructable destroyed");
		}
		this.UnmarkArea();
		int[] placementCells = this.building.PlacementCells;
		for (int i = 0; i < placementCells.Length; i++)
		{
			Diggable diggable = Diggable.GetDiggable(placementCells[i]);
			if (diggable != null)
			{
				diggable.gameObject.DeleteObject();
			}
		}
		base.OnCleanUp();
	}

	// Token: 0x060018D7 RID: 6359 RVA: 0x0008479C File Offset: 0x0008299C
	private void OnDiggableReachabilityChanged(object data)
	{
		if (!this.IsReplacementTile)
		{
			int diggable_count = 0;
			int unreachable_count = 0;
			this.building.RunOnArea(delegate(int offset_cell)
			{
				Diggable diggable = Diggable.GetDiggable(offset_cell);
				if (diggable != null)
				{
					int num = diggable_count + 1;
					diggable_count = num;
					if (!diggable.GetComponent<KPrefabID>().HasTag(GameTags.Reachable))
					{
						num = unreachable_count + 1;
						unreachable_count = num;
					}
				}
			});
			bool flag = unreachable_count > 0 && unreachable_count == diggable_count;
			if (flag != this.hasUnreachableDigs)
			{
				if (flag)
				{
					base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ConstructableDigUnreachable, null);
				}
				else
				{
					base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ConstructableDigUnreachable, false);
				}
				this.hasUnreachableDigs = flag;
			}
		}
	}

	// Token: 0x060018D8 RID: 6360 RVA: 0x00084848 File Offset: 0x00082A48
	private void PlaceDiggables()
	{
		if (this.waitForFetchesBeforeDigging && this.fetchList != null && !this.hasLadderNearby)
		{
			this.OnDiggableReachabilityChanged(null);
			return;
		}
		bool digs_complete = true;
		if (!this.solidPartitionerEntry.IsValid())
		{
			Extents validPlacementExtents = this.building.GetValidPlacementExtents();
			this.solidPartitionerEntry = GameScenePartitioner.Instance.Add("Constructable.OnFetchListComplete", base.gameObject, validPlacementExtents, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChangedOrDigDestroyed));
			this.digPartitionerEntry = GameScenePartitioner.Instance.Add("Constructable.OnFetchListComplete", base.gameObject, validPlacementExtents, GameScenePartitioner.Instance.digDestroyedLayer, new Action<object>(this.OnSolidChangedOrDigDestroyed));
		}
		if (!this.IsReplacementTile)
		{
			this.building.RunOnArea(delegate(int offset_cell)
			{
				PrioritySetting masterPriority = this.GetComponent<Prioritizable>().GetMasterPriority();
				if (Diggable.IsDiggable(offset_cell))
				{
					digs_complete = false;
					Diggable diggable = Diggable.GetDiggable(offset_cell);
					if (diggable == null)
					{
						diggable = GameUtil.KInstantiate(Assets.GetPrefab(new Tag("DigPlacer")), Grid.SceneLayer.Move, null, 0).GetComponent<Diggable>();
						diggable.gameObject.SetActive(true);
						diggable.transform.SetPosition(Grid.CellToPosCBC(offset_cell, Grid.SceneLayer.Move));
						diggable.Subscribe(-1432940121, new Action<object>(this.OnDiggableReachabilityChanged));
						Grid.Objects[offset_cell, 7] = diggable.gameObject;
					}
					else
					{
						diggable.Unsubscribe(-1432940121, new Action<object>(this.OnDiggableReachabilityChanged));
						diggable.Subscribe(-1432940121, new Action<object>(this.OnDiggableReachabilityChanged));
					}
					diggable.choreTypeIdHash = Db.Get().ChoreTypes.BuildDig.IdHash;
					diggable.GetComponent<Prioritizable>().SetMasterPriority(masterPriority);
					RenderUtil.EnableRenderer(diggable.transform, false);
					SaveLoadRoot component = diggable.GetComponent<SaveLoadRoot>();
					if (component != null)
					{
						UnityEngine.Object.Destroy(component);
					}
				}
			});
			this.OnDiggableReachabilityChanged(null);
		}
		bool flag = this.building.Def.IsValidBuildLocation(base.gameObject, base.transform.GetPosition(), this.building.Orientation, this.IsReplacementTile);
		if (flag)
		{
			this.notifier.Remove(this.invalidLocation);
		}
		else
		{
			this.notifier.Add(this.invalidLocation, "");
		}
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.InvalidBuildingLocation, !flag, this);
		bool flag2 = digs_complete && flag && this.fetchList == null;
		if (flag2 && this.buildChore == null)
		{
			this.buildChore = new WorkChore<Constructable>(Db.Get().ChoreTypes.Build, this, null, true, new Action<Chore>(this.UpdateBuildState), new Action<Chore>(this.UpdateBuildState), new Action<Chore>(this.UpdateBuildState), true, null, false, true, null, true, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			this.UpdateBuildState(this.buildChore);
			return;
		}
		if (!flag2 && this.buildChore != null)
		{
			this.buildChore.Cancel("Need to dig");
			this.buildChore = null;
		}
	}

	// Token: 0x060018D9 RID: 6361 RVA: 0x00084A49 File Offset: 0x00082C49
	private void OnFetchListComplete()
	{
		this.fetchList = null;
		this.PlaceDiggables();
		this.ClearMaterialNeeds();
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x00084A60 File Offset: 0x00082C60
	private void ClearMaterialNeeds()
	{
		if (this.materialNeedsCleared)
		{
			return;
		}
		foreach (Recipe.Ingredient ingredient in this.Recipe.GetAllIngredients(this.SelectedElementsTags))
		{
			MaterialNeeds.UpdateNeed(ingredient.tag, -ingredient.amount, base.gameObject.GetMyWorldId());
		}
		this.materialNeedsCleared = true;
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x00084ABE File Offset: 0x00082CBE
	private void OnSolidChangedOrDigDestroyed(object data)
	{
		if (this == null || this.finished)
		{
			return;
		}
		this.PlaceDiggables();
	}

	// Token: 0x060018DC RID: 6364 RVA: 0x00084AD8 File Offset: 0x00082CD8
	private void UpdateBuildState(Chore chore)
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (chore.InProgress())
		{
			component.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.UnderConstruction, null);
			return;
		}
		component.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.UnderConstructionNoWorker, null);
	}

	// Token: 0x060018DD RID: 6365 RVA: 0x00084B44 File Offset: 0x00082D44
	[OnDeserialized]
	internal void OnDeserialized()
	{
		if (this.ids != null)
		{
			this.selectedElements = new Element[this.ids.Length];
			for (int i = 0; i < this.ids.Length; i++)
			{
				this.selectedElements[i] = ElementLoader.FindElementByHash((SimHashes)this.ids[i]);
			}
			if (this.selectedElementsTags == null)
			{
				this.selectedElementsTags = new Tag[this.ids.Length];
				for (int j = 0; j < this.ids.Length; j++)
				{
					this.selectedElementsTags[j] = ElementLoader.FindElementByHash((SimHashes)this.ids[j]).tag;
				}
			}
			global::Debug.Assert(this.selectedElements.Length == this.selectedElementsTags.Length);
			for (int k = 0; k < this.selectedElements.Length; k++)
			{
				global::Debug.Assert(this.selectedElements[k].tag == this.SelectedElementsTags[k]);
			}
		}
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x00084C30 File Offset: 0x00082E30
	private void OnReachableChanged(object data)
	{
		KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
		if ((bool)data)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ConstructionUnreachable, false);
			if (component != null)
			{
				component.TintColour = Game.Instance.uiColours.Build.validLocation;
				return;
			}
		}
		else
		{
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ConstructionUnreachable, this);
			if (component != null)
			{
				component.TintColour = Game.Instance.uiColours.Build.unreachable;
			}
		}
	}

	// Token: 0x060018DF RID: 6367 RVA: 0x00084CD8 File Offset: 0x00082ED8
	private void OnRefreshUserMenu(object data)
	{
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_cancel", UI.USERMENUACTIONS.CANCELCONSTRUCTION.NAME, new System.Action(this.OnPressCancel), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCELCONSTRUCTION.TOOLTIP, true), 1f);
	}

	// Token: 0x060018E0 RID: 6368 RVA: 0x00084D32 File Offset: 0x00082F32
	private void OnPressCancel()
	{
		base.gameObject.Trigger(2127324410, null);
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x00084D45 File Offset: 0x00082F45
	private void OnCancel(object data = null)
	{
		DetailsScreen.Instance.Show(false);
		this.ClearMaterialNeeds();
	}

	// Token: 0x04000DCE RID: 3534
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04000DCF RID: 3535
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x04000DD0 RID: 3536
	[MyCmpAdd]
	private Prioritizable prioritizable;

	// Token: 0x04000DD1 RID: 3537
	[MyCmpReq]
	private Building building;

	// Token: 0x04000DD2 RID: 3538
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04000DD3 RID: 3539
	private Notification invalidLocation;

	// Token: 0x04000DD4 RID: 3540
	private float initialTemperature = -1f;

	// Token: 0x04000DD5 RID: 3541
	[Serialize]
	private bool isPrioritized;

	// Token: 0x04000DD6 RID: 3542
	private FetchList2 fetchList;

	// Token: 0x04000DD7 RID: 3543
	private Chore buildChore;

	// Token: 0x04000DD8 RID: 3544
	private bool materialNeedsCleared;

	// Token: 0x04000DD9 RID: 3545
	private bool hasUnreachableDigs;

	// Token: 0x04000DDA RID: 3546
	private bool finished;

	// Token: 0x04000DDB RID: 3547
	private bool unmarked;

	// Token: 0x04000DDC RID: 3548
	public bool isDiggingRequired = true;

	// Token: 0x04000DDD RID: 3549
	private bool waitForFetchesBeforeDigging;

	// Token: 0x04000DDE RID: 3550
	private bool hasLadderNearby;

	// Token: 0x04000DDF RID: 3551
	private Extents ladderDetectionExtents;

	// Token: 0x04000DE0 RID: 3552
	[Serialize]
	public bool IsReplacementTile;

	// Token: 0x04000DE1 RID: 3553
	private HandleVector<int>.Handle solidPartitionerEntry;

	// Token: 0x04000DE2 RID: 3554
	private HandleVector<int>.Handle digPartitionerEntry;

	// Token: 0x04000DE3 RID: 3555
	private HandleVector<int>.Handle ladderParititonerEntry;

	// Token: 0x04000DE4 RID: 3556
	private LoggerFSS log = new LoggerFSS("Constructable", 35);

	// Token: 0x04000DE5 RID: 3557
	[Serialize]
	private Tag[] selectedElementsTags;

	// Token: 0x04000DE6 RID: 3558
	private Element[] selectedElements;

	// Token: 0x04000DE7 RID: 3559
	[Serialize]
	private int[] ids;

	// Token: 0x04000DE8 RID: 3560
	private static readonly EventSystem.IntraObjectHandler<Constructable> OnReachableChangedDelegate = new EventSystem.IntraObjectHandler<Constructable>(delegate(Constructable component, object data)
	{
		component.OnReachableChanged(data);
	});

	// Token: 0x04000DE9 RID: 3561
	private static readonly EventSystem.IntraObjectHandler<Constructable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Constructable>(delegate(Constructable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x04000DEA RID: 3562
	private static readonly EventSystem.IntraObjectHandler<Constructable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Constructable>(delegate(Constructable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
