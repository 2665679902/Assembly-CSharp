using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000471 RID: 1137
[AddComponentMenu("KMonoBehaviour/Workable/Deconstructable")]
public class Deconstructable : Workable
{
	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x06001923 RID: 6435 RVA: 0x000867EC File Offset: 0x000849EC
	private CellOffset[] placementOffsets
	{
		get
		{
			Building component = base.GetComponent<Building>();
			if (component != null)
			{
				CellOffset[] array = component.Def.PlacementOffsets;
				Rotatable component2 = component.GetComponent<Rotatable>();
				if (component2 != null)
				{
					array = new CellOffset[component.Def.PlacementOffsets.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = component2.GetRotatedCellOffset(component.Def.PlacementOffsets[i]);
					}
				}
				return array;
			}
			OccupyArea component3 = base.GetComponent<OccupyArea>();
			if (component3 != null)
			{
				return component3.OccupiedCellsOffsets;
			}
			if (this.looseEntityDeconstructable)
			{
				return new CellOffset[]
				{
					new CellOffset(0, 0)
				};
			}
			global::Debug.Assert(false, "Ack! We put a Deconstructable on something that's neither a Building nor OccupyArea!", this);
			return null;
		}
	}

	// Token: 0x06001924 RID: 6436 RVA: 0x000868B0 File Offset: 0x00084AB0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Deconstructing;
		this.attributeConverter = Db.Get().AttributeConverters.ConstructionSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.minimumAttributeMultiplier = 0.75f;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Building.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		Building component = base.GetComponent<Building>();
		if (component != null && component.Def.IsTilePiece)
		{
			base.SetWorkTime(component.Def.ConstructionTime * 0.5f);
			return;
		}
		base.SetWorkTime(30f);
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x000869A4 File Offset: 0x00084BA4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		CellOffset[] array = null;
		CellOffset[][] array2 = OffsetGroups.InvertedStandardTable;
		Building component = base.GetComponent<Building>();
		if (component != null && component.Def.IsTilePiece)
		{
			array2 = OffsetGroups.InvertedStandardTableWithCorners;
			array = component.Def.ConstructionOffsetFilter;
		}
		CellOffset[][] array3 = OffsetGroups.BuildReachabilityTable(this.placementOffsets, array2, array);
		base.SetOffsetTable(array3);
		base.Subscribe<Deconstructable>(493375141, Deconstructable.OnRefreshUserMenuDelegate);
		base.Subscribe<Deconstructable>(-111137758, Deconstructable.OnRefreshUserMenuDelegate);
		base.Subscribe<Deconstructable>(2127324410, Deconstructable.OnCancelDelegate);
		base.Subscribe<Deconstructable>(-790448070, Deconstructable.OnDeconstructDelegate);
		if (this.constructionElements == null || this.constructionElements.Length == 0)
		{
			this.constructionElements = new Tag[1];
			this.constructionElements[0] = base.GetComponent<PrimaryElement>().Element.tag;
		}
		if (this.isMarkedForDeconstruction)
		{
			this.QueueDeconstruction();
		}
	}

	// Token: 0x06001926 RID: 6438 RVA: 0x00086A90 File Offset: 0x00084C90
	protected override void OnStartWork(Worker worker)
	{
		this.progressBar.barColor = ProgressBarsConfig.Instance.GetBarColor("DeconstructBar");
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.PendingDeconstruction, false);
		base.Trigger(1830962028, this);
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x00086AE0 File Offset: 0x00084CE0
	protected override void OnCompleteWork(Worker worker)
	{
		Building component = base.GetComponent<Building>();
		SimCellOccupier component2 = base.GetComponent<SimCellOccupier>();
		if (DetailsScreen.Instance != null && DetailsScreen.Instance.CompareTargetWith(base.gameObject))
		{
			DetailsScreen.Instance.Show(false);
		}
		PrimaryElement component3 = base.GetComponent<PrimaryElement>();
		float temperature = component3.Temperature;
		byte disease_idx = component3.DiseaseIdx;
		int disease_count = component3.DiseaseCount;
		if (component2 != null)
		{
			if (component.Def.TileLayer != ObjectLayer.NumLayers)
			{
				int num = Grid.PosToCell(base.transform.GetPosition());
				if (Grid.Objects[num, (int)component.Def.TileLayer] == base.gameObject)
				{
					Grid.Objects[num, (int)component.Def.ObjectLayer] = null;
					Grid.Objects[num, (int)component.Def.TileLayer] = null;
					Grid.Foundation[num] = false;
					TileVisualizer.RefreshCell(num, component.Def.TileLayer, component.Def.ReplacementLayer);
				}
			}
			component2.DestroySelf(delegate
			{
				this.TriggerDestroy(temperature, disease_idx, disease_count);
			});
		}
		else
		{
			this.TriggerDestroy(temperature, disease_idx, disease_count);
		}
		if (component == null || component.Def.PlayConstructionSounds)
		{
			string sound = GlobalAssets.GetSound("Finish_Deconstruction_" + ((!this.audioSize.IsNullOrWhiteSpace()) ? this.audioSize : component.Def.AudioSize), false);
			if (sound != null)
			{
				KMonoBehaviour.PlaySound3DAtLocation(sound, base.gameObject.transform.GetPosition());
			}
		}
		base.Trigger(-702296337, this);
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06001928 RID: 6440 RVA: 0x00086CA7 File Offset: 0x00084EA7
	public bool HasBeenDestroyed
	{
		get
		{
			return this.destroyed;
		}
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x00086CB0 File Offset: 0x00084EB0
	public List<GameObject> ForceDestroyAndGetMaterials()
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		float temperature = component.Temperature;
		byte diseaseIdx = component.DiseaseIdx;
		int diseaseCount = component.DiseaseCount;
		return this.TriggerDestroy(temperature, diseaseIdx, diseaseCount);
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x00086CE0 File Offset: 0x00084EE0
	private List<GameObject> TriggerDestroy(float temperature, byte disease_idx, int disease_count)
	{
		if (this == null || this.destroyed)
		{
			return null;
		}
		List<GameObject> list = this.SpawnItemsFromConstruction(temperature, disease_idx, disease_count);
		this.destroyed = true;
		base.gameObject.DeleteObject();
		return list;
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x00086D10 File Offset: 0x00084F10
	private void QueueDeconstruction()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.OnCompleteWork(null);
			return;
		}
		if (this.chore == null)
		{
			BuildingComplete component = base.GetComponent<BuildingComplete>();
			if (component != null && component.Def.ReplacementLayer != ObjectLayer.NumLayers)
			{
				int num = Grid.PosToCell(component);
				if (Grid.Objects[num, (int)component.Def.ReplacementLayer] != null)
				{
					return;
				}
			}
			Prioritizable.AddRef(base.gameObject);
			this.chore = new WorkChore<Deconstructable>(Db.Get().ChoreTypes.Deconstruct, this, null, true, null, null, null, true, null, false, false, null, true, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.PendingDeconstruction, this);
			this.isMarkedForDeconstruction = true;
			base.Trigger(2108245096, "Deconstruct");
		}
	}

	// Token: 0x0600192C RID: 6444 RVA: 0x00086DE6 File Offset: 0x00084FE6
	private void OnDeconstruct()
	{
		if (this.chore == null)
		{
			this.QueueDeconstruction();
			return;
		}
		this.CancelDeconstruction();
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x00086DFD File Offset: 0x00084FFD
	public bool IsMarkedForDeconstruction()
	{
		return this.chore != null;
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x00086E08 File Offset: 0x00085008
	public void SetAllowDeconstruction(bool allow)
	{
		this.allowDeconstruction = allow;
		if (!this.allowDeconstruction)
		{
			this.CancelDeconstruction();
		}
	}

	// Token: 0x0600192F RID: 6447 RVA: 0x00086E20 File Offset: 0x00085020
	public void SpawnItemsFromConstruction()
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		float temperature = component.Temperature;
		byte diseaseIdx = component.DiseaseIdx;
		int diseaseCount = component.DiseaseCount;
		this.SpawnItemsFromConstruction(temperature, diseaseIdx, diseaseCount);
	}

	// Token: 0x06001930 RID: 6448 RVA: 0x00086E54 File Offset: 0x00085054
	private List<GameObject> SpawnItemsFromConstruction(float temperature, byte disease_idx, int disease_count)
	{
		List<GameObject> list = new List<GameObject>();
		if (!this.allowDeconstruction)
		{
			return list;
		}
		Building component = base.GetComponent<Building>();
		float[] array;
		if (component != null)
		{
			array = component.Def.Mass;
		}
		else
		{
			array = new float[] { base.GetComponent<PrimaryElement>().Mass };
		}
		int num = 0;
		while (num < this.constructionElements.Length && array.Length > num)
		{
			GameObject gameObject = this.SpawnItem(base.transform.GetPosition(), this.constructionElements[num], array[num], temperature, disease_idx, disease_count);
			int num2 = Grid.PosToCell(gameObject.transform.GetPosition());
			int num3 = Grid.CellAbove(num2);
			Vector2 zero;
			if ((Grid.IsValidCell(num2) && Grid.Solid[num2]) || (Grid.IsValidCell(num3) && Grid.Solid[num3]))
			{
				zero = Vector2.zero;
			}
			else
			{
				zero = new Vector2(UnityEngine.Random.Range(-1f, 1f) * Deconstructable.INITIAL_VELOCITY_RANGE.x, Deconstructable.INITIAL_VELOCITY_RANGE.y);
			}
			if (GameComps.Fallers.Has(gameObject))
			{
				GameComps.Fallers.Remove(gameObject);
			}
			GameComps.Fallers.Add(gameObject, zero);
			list.Add(gameObject);
			num++;
		}
		return list;
	}

	// Token: 0x06001931 RID: 6449 RVA: 0x00086F9C File Offset: 0x0008519C
	public GameObject SpawnItem(Vector3 position, Tag src_element, float src_mass, float src_temperature, byte disease_idx, int disease_count)
	{
		GameObject gameObject = null;
		int num = Grid.PosToCell(position);
		CellOffset[] placementOffsets = this.placementOffsets;
		Element element = ElementLoader.GetElement(src_element);
		if (element != null)
		{
			float num2 = src_mass;
			int num3 = 0;
			while ((float)num3 < src_mass / 400f)
			{
				int num4 = num3 % placementOffsets.Length;
				int num5 = Grid.OffsetCell(num, placementOffsets[num4]);
				float num6 = num2;
				if (num2 > 400f)
				{
					num6 = 400f;
					num2 -= 400f;
				}
				gameObject = element.substance.SpawnResource(Grid.CellToPosCBC(num5, Grid.SceneLayer.Ore), num6, src_temperature, disease_idx, disease_count, false, false, false);
				num3++;
			}
		}
		else
		{
			int num7 = 0;
			while ((float)num7 < src_mass)
			{
				int num8 = num7 % placementOffsets.Length;
				int num9 = Grid.OffsetCell(num, placementOffsets[num8]);
				gameObject = GameUtil.KInstantiate(Assets.GetPrefab(src_element), Grid.CellToPosCBC(num9, Grid.SceneLayer.Ore), Grid.SceneLayer.Ore, null, 0);
				gameObject.SetActive(true);
				num7++;
			}
		}
		return gameObject;
	}

	// Token: 0x06001932 RID: 6450 RVA: 0x00087080 File Offset: 0x00085280
	private void OnRefreshUserMenu(object data)
	{
		if (!this.allowDeconstruction)
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((this.chore == null) ? new KIconButtonMenu.ButtonInfo("action_deconstruct", UI.USERMENUACTIONS.DECONSTRUCT.NAME, new System.Action(this.OnDeconstruct), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DECONSTRUCT.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_deconstruct", UI.USERMENUACTIONS.DECONSTRUCT.NAME_OFF, new System.Action(this.OnDeconstruct), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DECONSTRUCT.TOOLTIP_OFF, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 0f);
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x00087124 File Offset: 0x00085324
	public void CancelDeconstruction()
	{
		if (this.chore != null)
		{
			this.chore.Cancel("Cancelled deconstruction");
			this.chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.PendingDeconstruction, false);
			base.ShowProgressBar(false);
			this.isMarkedForDeconstruction = false;
			Prioritizable.RemoveRef(base.gameObject);
		}
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x00087185 File Offset: 0x00085385
	private void OnCancel(object data)
	{
		this.CancelDeconstruction();
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x0008718D File Offset: 0x0008538D
	private void OnDeconstruct(object data)
	{
		if (this.allowDeconstruction || DebugHandler.InstantBuildMode)
		{
			this.QueueDeconstruction();
		}
	}

	// Token: 0x04000E11 RID: 3601
	public Chore chore;

	// Token: 0x04000E12 RID: 3602
	public bool allowDeconstruction = true;

	// Token: 0x04000E13 RID: 3603
	public string audioSize;

	// Token: 0x04000E14 RID: 3604
	[Serialize]
	private bool isMarkedForDeconstruction;

	// Token: 0x04000E15 RID: 3605
	[Serialize]
	public Tag[] constructionElements;

	// Token: 0x04000E16 RID: 3606
	public bool looseEntityDeconstructable;

	// Token: 0x04000E17 RID: 3607
	private static readonly EventSystem.IntraObjectHandler<Deconstructable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Deconstructable>(delegate(Deconstructable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04000E18 RID: 3608
	private static readonly EventSystem.IntraObjectHandler<Deconstructable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Deconstructable>(delegate(Deconstructable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x04000E19 RID: 3609
	private static readonly EventSystem.IntraObjectHandler<Deconstructable> OnDeconstructDelegate = new EventSystem.IntraObjectHandler<Deconstructable>(delegate(Deconstructable component, object data)
	{
		component.OnDeconstruct(data);
	});

	// Token: 0x04000E1A RID: 3610
	private static readonly Vector2 INITIAL_VELOCITY_RANGE = new Vector2(0.5f, 4f);

	// Token: 0x04000E1B RID: 3611
	private bool destroyed;
}
