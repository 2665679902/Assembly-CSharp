using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000625 RID: 1573
[SerializationConfig(MemberSerialization.OptIn)]
public class PlantablePlot : SingleEntityReceptacle, ISaveLoadable, IGameObjectEffectDescriptor
{
	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x0600292D RID: 10541 RVA: 0x000D9AF9 File Offset: 0x000D7CF9
	// (set) Token: 0x0600292E RID: 10542 RVA: 0x000D9B06 File Offset: 0x000D7D06
	public KPrefabID plant
	{
		get
		{
			return this.plantRef.Get();
		}
		set
		{
			this.plantRef.Set(value);
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x0600292F RID: 10543 RVA: 0x000D9B14 File Offset: 0x000D7D14
	public bool ValidPlant
	{
		get
		{
			return this.plantPreview == null || this.plantPreview.Valid;
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x06002930 RID: 10544 RVA: 0x000D9B31 File Offset: 0x000D7D31
	public bool AcceptsFertilizer
	{
		get
		{
			return this.accepts_fertilizer;
		}
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x06002931 RID: 10545 RVA: 0x000D9B39 File Offset: 0x000D7D39
	public bool AcceptsIrrigation
	{
		get
		{
			return this.accepts_irrigation;
		}
	}

	// Token: 0x06002932 RID: 10546 RVA: 0x000D9B44 File Offset: 0x000D7D44
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (!DlcManager.FeaturePlantMutationsEnabled())
		{
			this.requestedEntityAdditionalFilterTag = Tag.Invalid;
			return;
		}
		if (this.requestedEntityTag.IsValid && this.requestedEntityAdditionalFilterTag.IsValid && !PlantSubSpeciesCatalog.Instance.IsValidPlantableSeed(this.requestedEntityTag, this.requestedEntityAdditionalFilterTag))
		{
			this.requestedEntityAdditionalFilterTag = Tag.Invalid;
		}
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x000D9BA4 File Offset: 0x000D7DA4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.choreType = Db.Get().ChoreTypes.FarmFetch;
		this.statusItemNeed = Db.Get().BuildingStatusItems.NeedSeed;
		this.statusItemNoneAvailable = Db.Get().BuildingStatusItems.NoAvailableSeed;
		this.statusItemAwaitingDelivery = Db.Get().BuildingStatusItems.AwaitingSeedDelivery;
		this.plantRef = new Ref<KPrefabID>();
		base.Subscribe<PlantablePlot>(-905833192, PlantablePlot.OnCopySettingsDelegate);
		base.Subscribe<PlantablePlot>(144050788, PlantablePlot.OnUpdateRoomDelegate);
		if (this.HasTag(GameTags.FarmTiles))
		{
			this.storage.SetOffsetTable(OffsetGroups.InvertedStandardTableWithCorners);
			DropAllWorkable component = base.GetComponent<DropAllWorkable>();
			if (component != null)
			{
				component.SetOffsetTable(OffsetGroups.InvertedStandardTableWithCorners);
			}
			Toggleable component2 = base.GetComponent<Toggleable>();
			if (component2 != null)
			{
				component2.SetOffsetTable(OffsetGroups.InvertedStandardTableWithCorners);
			}
		}
	}

	// Token: 0x06002934 RID: 10548 RVA: 0x000D9C8C File Offset: 0x000D7E8C
	private void OnCopySettings(object data)
	{
		PlantablePlot component = ((GameObject)data).GetComponent<PlantablePlot>();
		if (component != null)
		{
			if (base.occupyingObject == null && (this.requestedEntityTag != component.requestedEntityTag || this.requestedEntityAdditionalFilterTag != component.requestedEntityAdditionalFilterTag || component.occupyingObject != null))
			{
				Tag tag = component.requestedEntityTag;
				Tag tag2 = component.requestedEntityAdditionalFilterTag;
				if (component.occupyingObject != null)
				{
					SeedProducer component2 = component.occupyingObject.GetComponent<SeedProducer>();
					if (component2 != null)
					{
						tag = TagManager.Create(component2.seedInfo.seedId);
						MutantPlant component3 = component.occupyingObject.GetComponent<MutantPlant>();
						tag2 = (component3 ? component3.SubSpeciesID : Tag.Invalid);
					}
				}
				base.CancelActiveRequest();
				this.CreateOrder(tag, tag2);
			}
			if (base.occupyingObject != null)
			{
				Prioritizable component4 = base.GetComponent<Prioritizable>();
				if (component4 != null)
				{
					Prioritizable component5 = base.occupyingObject.GetComponent<Prioritizable>();
					if (component5 != null)
					{
						component5.SetMasterPriority(component4.GetMasterPriority());
					}
				}
			}
		}
	}

	// Token: 0x06002935 RID: 10549 RVA: 0x000D9DB0 File Offset: 0x000D7FB0
	public override void CreateOrder(Tag entityTag, Tag additionalFilterTag)
	{
		this.SetPreview(entityTag, false);
		if (this.ValidPlant)
		{
			base.CreateOrder(entityTag, additionalFilterTag);
			return;
		}
		this.SetPreview(Tag.Invalid, false);
	}

	// Token: 0x06002936 RID: 10550 RVA: 0x000D9DD8 File Offset: 0x000D7FD8
	private void SyncPriority(PrioritySetting priority)
	{
		Prioritizable component = base.GetComponent<Prioritizable>();
		if (!object.Equals(component.GetMasterPriority(), priority))
		{
			component.SetMasterPriority(priority);
		}
		if (base.occupyingObject != null)
		{
			Prioritizable component2 = base.occupyingObject.GetComponent<Prioritizable>();
			if (component2 != null && !object.Equals(component2.GetMasterPriority(), priority))
			{
				component2.SetMasterPriority(component.GetMasterPriority());
			}
		}
	}

	// Token: 0x06002937 RID: 10551 RVA: 0x000D9E54 File Offset: 0x000D8054
	protected override void OnSpawn()
	{
		if (this.plant != null)
		{
			this.RegisterWithPlant(this.plant.gameObject);
		}
		base.OnSpawn();
		this.autoReplaceEntity = false;
		Components.PlantablePlots.Add(base.gameObject.GetMyWorldId(), this);
		Prioritizable component = base.GetComponent<Prioritizable>();
		component.onPriorityChanged = (Action<PrioritySetting>)Delegate.Combine(component.onPriorityChanged, new Action<PrioritySetting>(this.SyncPriority));
	}

	// Token: 0x06002938 RID: 10552 RVA: 0x000D9ECA File Offset: 0x000D80CA
	public void SetFertilizationFlags(bool fertilizer, bool liquid_piping)
	{
		this.accepts_fertilizer = fertilizer;
		this.has_liquid_pipe_input = liquid_piping;
	}

	// Token: 0x06002939 RID: 10553 RVA: 0x000D9EDC File Offset: 0x000D80DC
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.plantPreview != null)
		{
			Util.KDestroyGameObject(this.plantPreview.gameObject);
		}
		if (base.occupyingObject)
		{
			base.occupyingObject.Trigger(-216549700, null);
		}
		Components.PlantablePlots.Remove(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x0600293A RID: 10554 RVA: 0x000D9F44 File Offset: 0x000D8144
	protected override GameObject SpawnOccupyingObject(GameObject depositedEntity)
	{
		PlantableSeed component = depositedEntity.GetComponent<PlantableSeed>();
		if (component != null)
		{
			Vector3 vector = Grid.CellToPosCBC(Grid.PosToCell(this), this.plantLayer);
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(component.PlantID), vector, this.plantLayer, null, 0);
			MutantPlant component2 = gameObject.GetComponent<MutantPlant>();
			if (component2 != null)
			{
				component.GetComponent<MutantPlant>().CopyMutationsTo(component2);
			}
			gameObject.SetActive(true);
			this.destroyEntityOnDeposit = true;
			return gameObject;
		}
		this.destroyEntityOnDeposit = false;
		return depositedEntity;
	}

	// Token: 0x0600293B RID: 10555 RVA: 0x000D9FC0 File Offset: 0x000D81C0
	protected override void ConfigureOccupyingObject(GameObject newPlant)
	{
		KPrefabID component = newPlant.GetComponent<KPrefabID>();
		this.plantRef.Set(component);
		this.RegisterWithPlant(newPlant);
		UprootedMonitor component2 = newPlant.GetComponent<UprootedMonitor>();
		if (component2)
		{
			component2.canBeUprooted = false;
		}
		this.autoReplaceEntity = false;
		Prioritizable component3 = base.GetComponent<Prioritizable>();
		if (component3 != null)
		{
			Prioritizable component4 = newPlant.GetComponent<Prioritizable>();
			if (component4 != null)
			{
				component4.SetMasterPriority(component3.GetMasterPriority());
				Prioritizable prioritizable = component4;
				prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Combine(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.SyncPriority));
			}
		}
	}

	// Token: 0x0600293C RID: 10556 RVA: 0x000DA052 File Offset: 0x000D8252
	public void ReplacePlant(GameObject plant, bool keepStorage)
	{
		if (keepStorage)
		{
			this.UnsubscribeFromOccupant();
			base.occupyingObject = null;
		}
		base.ForceDeposit(plant);
	}

	// Token: 0x0600293D RID: 10557 RVA: 0x000DA06C File Offset: 0x000D826C
	protected override void PositionOccupyingObject()
	{
		base.PositionOccupyingObject();
		KBatchedAnimController component = base.occupyingObject.GetComponent<KBatchedAnimController>();
		component.SetSceneLayer(this.plantLayer);
		this.OffsetAnim(component, this.occupyingObjectVisualOffset);
	}

	// Token: 0x0600293E RID: 10558 RVA: 0x000DA0A4 File Offset: 0x000D82A4
	private void RegisterWithPlant(GameObject plant)
	{
		base.occupyingObject = plant;
		ReceptacleMonitor component = plant.GetComponent<ReceptacleMonitor>();
		if (component)
		{
			component.SetReceptacle(this);
		}
		plant.Trigger(1309017699, this.storage);
	}

	// Token: 0x0600293F RID: 10559 RVA: 0x000DA0DF File Offset: 0x000D82DF
	protected override void SubscribeToOccupant()
	{
		base.SubscribeToOccupant();
		if (base.occupyingObject != null)
		{
			base.Subscribe(base.occupyingObject, -216549700, new Action<object>(this.OnOccupantUprooted));
		}
	}

	// Token: 0x06002940 RID: 10560 RVA: 0x000DA113 File Offset: 0x000D8313
	protected override void UnsubscribeFromOccupant()
	{
		base.UnsubscribeFromOccupant();
		if (base.occupyingObject != null)
		{
			base.Unsubscribe(base.occupyingObject, -216549700, new Action<object>(this.OnOccupantUprooted));
		}
	}

	// Token: 0x06002941 RID: 10561 RVA: 0x000DA146 File Offset: 0x000D8346
	private void OnOccupantUprooted(object data)
	{
		this.autoReplaceEntity = false;
		this.requestedEntityTag = Tag.Invalid;
		this.requestedEntityAdditionalFilterTag = Tag.Invalid;
	}

	// Token: 0x06002942 RID: 10562 RVA: 0x000DA168 File Offset: 0x000D8368
	public override void OrderRemoveOccupant()
	{
		if (base.Occupant == null)
		{
			return;
		}
		Uprootable component = base.Occupant.GetComponent<Uprootable>();
		if (component == null)
		{
			return;
		}
		component.MarkForUproot(true);
	}

	// Token: 0x06002943 RID: 10563 RVA: 0x000DA1A4 File Offset: 0x000D83A4
	public override void SetPreview(Tag entityTag, bool solid = false)
	{
		PlantableSeed plantableSeed = null;
		if (entityTag.IsValid)
		{
			GameObject prefab = Assets.GetPrefab(entityTag);
			if (prefab == null)
			{
				DebugUtil.LogWarningArgs(base.gameObject, new object[] { "Planter tried previewing a tag with no asset! If this was the 'Empty' tag, ignore it, that will go away in new save games. Otherwise... Eh? Tag was: ", entityTag });
				return;
			}
			plantableSeed = prefab.GetComponent<PlantableSeed>();
		}
		if (this.plantPreview != null)
		{
			KPrefabID component = this.plantPreview.GetComponent<KPrefabID>();
			if (plantableSeed != null && component != null && component.PrefabTag == plantableSeed.PreviewID)
			{
				return;
			}
			this.plantPreview.gameObject.Unsubscribe(-1820564715, new Action<object>(this.OnValidChanged));
			Util.KDestroyGameObject(this.plantPreview.gameObject);
		}
		if (plantableSeed != null)
		{
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(plantableSeed.PreviewID), Grid.SceneLayer.Front, null, 0);
			this.plantPreview = gameObject.GetComponent<EntityPreview>();
			gameObject.transform.SetPosition(Vector3.zero);
			gameObject.transform.SetParent(base.gameObject.transform, false);
			gameObject.transform.SetLocalPosition(Vector3.zero);
			if (this.rotatable != null)
			{
				if (plantableSeed.direction == SingleEntityReceptacle.ReceptacleDirection.Top)
				{
					gameObject.transform.SetLocalPosition(this.occupyingObjectRelativePosition);
				}
				else if (plantableSeed.direction == SingleEntityReceptacle.ReceptacleDirection.Side)
				{
					gameObject.transform.SetLocalPosition(Rotatable.GetRotatedOffset(this.occupyingObjectRelativePosition, Orientation.R90));
				}
				else
				{
					gameObject.transform.SetLocalPosition(Rotatable.GetRotatedOffset(this.occupyingObjectRelativePosition, Orientation.R180));
				}
			}
			else
			{
				gameObject.transform.SetLocalPosition(this.occupyingObjectRelativePosition);
			}
			KBatchedAnimController component2 = gameObject.GetComponent<KBatchedAnimController>();
			this.OffsetAnim(component2, this.occupyingObjectVisualOffset);
			gameObject.SetActive(true);
			gameObject.Subscribe(-1820564715, new Action<object>(this.OnValidChanged));
			if (solid)
			{
				this.plantPreview.SetSolid();
			}
			this.plantPreview.UpdateValidity();
		}
	}

	// Token: 0x06002944 RID: 10564 RVA: 0x000DA394 File Offset: 0x000D8594
	private void OffsetAnim(KBatchedAnimController kanim, Vector3 offset)
	{
		if (this.rotatable != null)
		{
			offset = this.rotatable.GetRotatedOffset(offset);
		}
		kanim.Offset = offset;
	}

	// Token: 0x06002945 RID: 10565 RVA: 0x000DA3B9 File Offset: 0x000D85B9
	private void OnValidChanged(object obj)
	{
		base.Trigger(-1820564715, obj);
		if (!this.plantPreview.Valid && base.GetActiveRequest != null)
		{
			base.CancelActiveRequest();
		}
	}

	// Token: 0x06002946 RID: 10566 RVA: 0x000DA3E4 File Offset: 0x000D85E4
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.ENABLESDOMESTICGROWTH, UI.BUILDINGEFFECTS.TOOLTIPS.ENABLESDOMESTICGROWTH, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x04001844 RID: 6212
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001845 RID: 6213
	[Serialize]
	private Ref<KPrefabID> plantRef;

	// Token: 0x04001846 RID: 6214
	public Vector3 occupyingObjectVisualOffset = Vector3.zero;

	// Token: 0x04001847 RID: 6215
	public Grid.SceneLayer plantLayer = Grid.SceneLayer.BuildingBack;

	// Token: 0x04001848 RID: 6216
	private EntityPreview plantPreview;

	// Token: 0x04001849 RID: 6217
	[SerializeField]
	private bool accepts_fertilizer;

	// Token: 0x0400184A RID: 6218
	[SerializeField]
	private bool accepts_irrigation = true;

	// Token: 0x0400184B RID: 6219
	[SerializeField]
	public bool has_liquid_pipe_input;

	// Token: 0x0400184C RID: 6220
	private static readonly EventSystem.IntraObjectHandler<PlantablePlot> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<PlantablePlot>(delegate(PlantablePlot component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0400184D RID: 6221
	private static readonly EventSystem.IntraObjectHandler<PlantablePlot> OnUpdateRoomDelegate = new EventSystem.IntraObjectHandler<PlantablePlot>(delegate(PlantablePlot component, object data)
	{
		if (component.plantRef.Get() != null)
		{
			component.plantRef.Get().Trigger(144050788, data);
		}
	});
}
