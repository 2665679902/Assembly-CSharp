using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000564 RID: 1380
public class BuildingComplete : Building
{
	// Token: 0x06002145 RID: 8517 RVA: 0x000B5341 File Offset: 0x000B3541
	private bool WasReplaced()
	{
		return this.replacingTileLayer != ObjectLayer.NumLayers;
	}

	// Token: 0x06002146 RID: 8518 RVA: 0x000B5350 File Offset: 0x000B3550
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Vector3 position = base.transform.GetPosition();
		position.z = Grid.GetLayerZ(this.Def.SceneLayer);
		base.transform.SetPosition(position);
		base.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Default"));
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		Rotatable component2 = base.GetComponent<Rotatable>();
		if (component != null && component2 == null)
		{
			component.Offset = this.Def.GetVisualizerOffset();
		}
		KBoxCollider2D component3 = base.GetComponent<KBoxCollider2D>();
		if (component3 != null)
		{
			Vector3 visualizerOffset = this.Def.GetVisualizerOffset();
			component3.offset += new Vector2(visualizerOffset.x, visualizerOffset.y);
		}
		Attributes attributes = this.GetAttributes();
		foreach (Klei.AI.Attribute attribute in this.Def.attributes)
		{
			attributes.Add(attribute);
		}
		foreach (AttributeModifier attributeModifier in this.Def.attributeModifiers)
		{
			Klei.AI.Attribute attribute2 = Db.Get().BuildingAttributes.Get(attributeModifier.AttributeId);
			if (attributes.Get(attribute2) == null)
			{
				attributes.Add(attribute2);
			}
			attributes.Add(attributeModifier);
		}
		foreach (AttributeInstance attributeInstance in attributes)
		{
			AttributeModifier attributeModifier2 = new AttributeModifier(attributeInstance.Id, attributeInstance.GetTotalValue(), null, false, false, true);
			this.regionModifiers.Add(attributeModifier2);
		}
		if (this.Def.UseStructureTemperature)
		{
			GameComps.StructureTemperatures.Add(base.gameObject);
		}
		base.Subscribe<BuildingComplete>(1606648047, BuildingComplete.OnObjectReplacedDelegate);
		if (this.Def.Entombable)
		{
			base.Subscribe<BuildingComplete>(-1089732772, BuildingComplete.OnEntombedChange);
		}
	}

	// Token: 0x06002147 RID: 8519 RVA: 0x000B5594 File Offset: 0x000B3794
	private void OnEntombedChanged()
	{
		if (base.gameObject.HasTag(GameTags.Entombed))
		{
			Components.EntombedBuildings.Add(this);
			return;
		}
		Components.EntombedBuildings.Remove(this);
	}

	// Token: 0x06002148 RID: 8520 RVA: 0x000B55BF File Offset: 0x000B37BF
	public override void UpdatePosition()
	{
		base.UpdatePosition();
		GameScenePartitioner.Instance.UpdatePosition(this.scenePartitionerEntry, base.GetExtents());
	}

	// Token: 0x06002149 RID: 8521 RVA: 0x000B55DD File Offset: 0x000B37DD
	private void OnObjectReplaced(object data)
	{
		this.replacingTileLayer = (ObjectLayer)data;
	}

	// Token: 0x0600214A RID: 8522 RVA: 0x000B55EC File Offset: 0x000B37EC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.primaryElement = base.GetComponent<PrimaryElement>();
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (this.Def.IsFoundation)
		{
			foreach (int num2 in base.PlacementCells)
			{
				Grid.Foundation[num2] = true;
				Game.Instance.roomProber.SolidChangedEvent(num2, false);
			}
		}
		if (Grid.IsValidCell(num))
		{
			Vector3 vector = Grid.CellToPosCBC(num, this.Def.SceneLayer);
			base.transform.SetPosition(vector);
		}
		if (this.primaryElement != null)
		{
			if (this.primaryElement.Mass == 0f)
			{
				this.primaryElement.Mass = this.Def.Mass[0];
			}
			float temperature = this.primaryElement.Temperature;
			if (temperature > 0f && !float.IsNaN(temperature) && !float.IsInfinity(temperature))
			{
				BuildingComplete.MinKelvinSeen = Mathf.Min(BuildingComplete.MinKelvinSeen, temperature);
			}
			PrimaryElement primaryElement = this.primaryElement;
			primaryElement.setTemperatureCallback = (PrimaryElement.SetTemperatureCallback)Delegate.Combine(primaryElement.setTemperatureCallback, new PrimaryElement.SetTemperatureCallback(this.OnSetTemperature));
		}
		if (!base.gameObject.HasTag(GameTags.RocketInSpace))
		{
			this.Def.MarkArea(num, base.Orientation, this.Def.ObjectLayer, base.gameObject);
			if (this.Def.IsTilePiece)
			{
				this.Def.MarkArea(num, base.Orientation, this.Def.TileLayer, base.gameObject);
				this.Def.RunOnArea(num, base.Orientation, delegate(int c)
				{
					TileVisualizer.RefreshCell(c, this.Def.TileLayer, this.Def.ReplacementLayer);
				});
			}
		}
		base.RegisterBlockTileRenderer();
		if (this.Def.PreventIdleTraversalPastBuilding)
		{
			for (int j = 0; j < base.PlacementCells.Length; j++)
			{
				Grid.PreventIdleTraversal[base.PlacementCells[j]] = true;
			}
		}
		Components.BuildingCompletes.Add(this);
		BuildingConfigManager.Instance.AddBuildingCompleteKComponents(base.gameObject, this.Def.Tag);
		this.hasSpawnedKComponents = true;
		this.scenePartitionerEntry = GameScenePartitioner.Instance.Add(base.name, this, base.GetExtents(), GameScenePartitioner.Instance.completeBuildings, null);
		if (this.prefabid.HasTag(GameTags.TemplateBuilding))
		{
			Components.TemplateBuildings.Add(this);
		}
		Attributes attributes = this.GetAttributes();
		if (attributes != null)
		{
			Deconstructable component = base.GetComponent<Deconstructable>();
			if (component != null)
			{
				int k = 1;
				while (k < component.constructionElements.Length)
				{
					Tag tag = component.constructionElements[k];
					Element element = ElementLoader.GetElement(tag);
					if (element != null)
					{
						using (List<AttributeModifier>.Enumerator enumerator = element.attributeModifiers.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								AttributeModifier attributeModifier = enumerator.Current;
								attributes.Add(attributeModifier);
							}
							goto IL_341;
						}
						goto IL_2E1;
					}
					goto IL_2E1;
					IL_341:
					k++;
					continue;
					IL_2E1:
					GameObject gameObject = Assets.TryGetPrefab(tag);
					if (!(gameObject != null))
					{
						goto IL_341;
					}
					PrefabAttributeModifiers component2 = gameObject.GetComponent<PrefabAttributeModifiers>();
					if (component2 != null)
					{
						foreach (AttributeModifier attributeModifier2 in component2.descriptors)
						{
							attributes.Add(attributeModifier2);
						}
						goto IL_341;
					}
					goto IL_341;
				}
			}
		}
		BuildingInventory.Instance.RegisterBuilding(this);
	}

	// Token: 0x0600214B RID: 8523 RVA: 0x000B5978 File Offset: 0x000B3B78
	private void OnSetTemperature(PrimaryElement primary_element, float temperature)
	{
		BuildingComplete.MinKelvinSeen = Mathf.Min(BuildingComplete.MinKelvinSeen, temperature);
	}

	// Token: 0x0600214C RID: 8524 RVA: 0x000B598A File Offset: 0x000B3B8A
	public void SetCreationTime(float time)
	{
		this.creationTime = time;
	}

	// Token: 0x0600214D RID: 8525 RVA: 0x000B5993 File Offset: 0x000B3B93
	private string GetInspectSound()
	{
		return GlobalAssets.GetSound("AI_Inspect_" + base.GetComponent<KPrefabID>().PrefabTag.Name, false);
	}

	// Token: 0x0600214E RID: 8526 RVA: 0x000B59B8 File Offset: 0x000B3BB8
	protected override void OnCleanUp()
	{
		if (Game.quitting)
		{
			return;
		}
		GameScenePartitioner.Instance.Free(ref this.scenePartitionerEntry);
		if (this.hasSpawnedKComponents)
		{
			BuildingConfigManager.Instance.DestroyBuildingCompleteKComponents(base.gameObject, this.Def.Tag);
		}
		if (this.Def.UseStructureTemperature)
		{
			GameComps.StructureTemperatures.Remove(base.gameObject);
		}
		base.OnCleanUp();
		if (!this.WasReplaced() && base.gameObject.GetMyWorldId() != (int)ClusterManager.INVALID_WORLD_IDX)
		{
			int num = Grid.PosToCell(this);
			this.Def.UnmarkArea(num, base.Orientation, this.Def.ObjectLayer, base.gameObject);
			if (this.Def.IsTilePiece)
			{
				this.Def.UnmarkArea(num, base.Orientation, this.Def.TileLayer, base.gameObject);
				this.Def.RunOnArea(num, base.Orientation, delegate(int c)
				{
					TileVisualizer.RefreshCell(c, this.Def.TileLayer, this.Def.ReplacementLayer);
				});
			}
			if (this.Def.IsFoundation)
			{
				foreach (int num2 in base.PlacementCells)
				{
					Grid.Foundation[num2] = false;
					Game.Instance.roomProber.SolidChangedEvent(num2, false);
				}
			}
			if (this.Def.PreventIdleTraversalPastBuilding)
			{
				for (int j = 0; j < base.PlacementCells.Length; j++)
				{
					Grid.PreventIdleTraversal[base.PlacementCells[j]] = false;
				}
			}
		}
		if (this.WasReplaced() && this.Def.IsTilePiece && this.replacingTileLayer != this.Def.TileLayer)
		{
			int num3 = Grid.PosToCell(this);
			this.Def.UnmarkArea(num3, base.Orientation, this.Def.TileLayer, base.gameObject);
			this.Def.RunOnArea(num3, base.Orientation, delegate(int c)
			{
				TileVisualizer.RefreshCell(c, this.Def.TileLayer, this.Def.ReplacementLayer);
			});
		}
		Components.BuildingCompletes.Remove(this);
		Components.EntombedBuildings.Remove(this);
		Components.TemplateBuildings.Remove(this);
		base.UnregisterBlockTileRenderer();
		BuildingInventory.Instance.UnregisterBuilding(this);
		base.Trigger(-21016276, this);
	}

	// Token: 0x04001318 RID: 4888
	[MyCmpReq]
	private Modifiers modifiers;

	// Token: 0x04001319 RID: 4889
	[MyCmpGet]
	public KPrefabID prefabid;

	// Token: 0x0400131A RID: 4890
	public bool isManuallyOperated;

	// Token: 0x0400131B RID: 4891
	public bool isArtable;

	// Token: 0x0400131C RID: 4892
	public PrimaryElement primaryElement;

	// Token: 0x0400131D RID: 4893
	[Serialize]
	public float creationTime = -1f;

	// Token: 0x0400131E RID: 4894
	private bool hasSpawnedKComponents;

	// Token: 0x0400131F RID: 4895
	private ObjectLayer replacingTileLayer = ObjectLayer.NumLayers;

	// Token: 0x04001320 RID: 4896
	public List<AttributeModifier> regionModifiers = new List<AttributeModifier>();

	// Token: 0x04001321 RID: 4897
	private static readonly EventSystem.IntraObjectHandler<BuildingComplete> OnEntombedChange = new EventSystem.IntraObjectHandler<BuildingComplete>(delegate(BuildingComplete component, object data)
	{
		component.OnEntombedChanged();
	});

	// Token: 0x04001322 RID: 4898
	private static readonly EventSystem.IntraObjectHandler<BuildingComplete> OnObjectReplacedDelegate = new EventSystem.IntraObjectHandler<BuildingComplete>(delegate(BuildingComplete component, object data)
	{
		component.OnObjectReplaced(data);
	});

	// Token: 0x04001323 RID: 4899
	private HandleVector<int>.Handle scenePartitionerEntry;

	// Token: 0x04001324 RID: 4900
	public static float MinKelvinSeen = float.MaxValue;
}
