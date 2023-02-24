using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006E3 RID: 1763
[AddComponentMenu("KMonoBehaviour/scripts/PlantableSeed")]
public class PlantableSeed : KMonoBehaviour, IReceptacleDirection, IGameObjectEffectDescriptor
{
	// Token: 0x17000361 RID: 865
	// (get) Token: 0x06002FF2 RID: 12274 RVA: 0x000FD66C File Offset: 0x000FB86C
	public SingleEntityReceptacle.ReceptacleDirection Direction
	{
		get
		{
			return this.direction;
		}
	}

	// Token: 0x06002FF3 RID: 12275 RVA: 0x000FD674 File Offset: 0x000FB874
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.timeUntilSelfPlant = Util.RandomVariance(2400f, 600f);
	}

	// Token: 0x06002FF4 RID: 12276 RVA: 0x000FD691 File Offset: 0x000FB891
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.PlantableSeeds.Add(this);
	}

	// Token: 0x06002FF5 RID: 12277 RVA: 0x000FD6A4 File Offset: 0x000FB8A4
	protected override void OnCleanUp()
	{
		Components.PlantableSeeds.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002FF6 RID: 12278 RVA: 0x000FD6B8 File Offset: 0x000FB8B8
	public void TryPlant(bool allow_plant_from_storage = false)
	{
		this.timeUntilSelfPlant = Util.RandomVariance(2400f, 600f);
		if (!allow_plant_from_storage && base.gameObject.HasTag(GameTags.Stored))
		{
			return;
		}
		int num = Grid.PosToCell(base.gameObject);
		if (this.TestSuitableGround(num))
		{
			Vector3 vector = Grid.CellToPosCBC(num, Grid.SceneLayer.BuildingFront);
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(this.PlantID), vector, Grid.SceneLayer.BuildingFront, null, 0);
			MutantPlant component = gameObject.GetComponent<MutantPlant>();
			if (component != null)
			{
				base.GetComponent<MutantPlant>().CopyMutationsTo(component);
			}
			gameObject.SetActive(true);
			Pickupable pickupable = this.pickupable.Take(1f);
			if (pickupable != null)
			{
				gameObject.GetComponent<Crop>() != null;
				Util.KDestroyGameObject(pickupable.gameObject);
				return;
			}
			KCrashReporter.Assert(false, "Seed has fractional total amount < 1f");
		}
	}

	// Token: 0x06002FF7 RID: 12279 RVA: 0x000FD78C File Offset: 0x000FB98C
	public bool TestSuitableGround(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		int num;
		if (this.Direction == SingleEntityReceptacle.ReceptacleDirection.Bottom)
		{
			num = Grid.CellAbove(cell);
		}
		else
		{
			num = Grid.CellBelow(cell);
		}
		if (!Grid.IsValidCell(num))
		{
			return false;
		}
		if (Grid.Foundation[num])
		{
			return false;
		}
		if (Grid.Element[num].hardness >= 150)
		{
			return false;
		}
		if (this.replantGroundTag.IsValid && !Grid.Element[num].HasTag(this.replantGroundTag))
		{
			return false;
		}
		GameObject prefab = Assets.GetPrefab(this.PlantID);
		EntombVulnerable component = prefab.GetComponent<EntombVulnerable>();
		if (component != null && !component.IsCellSafe(cell))
		{
			return false;
		}
		DrowningMonitor component2 = prefab.GetComponent<DrowningMonitor>();
		if (component2 != null && !component2.IsCellSafe(cell))
		{
			return false;
		}
		TemperatureVulnerable component3 = prefab.GetComponent<TemperatureVulnerable>();
		if (component3 != null && !component3.IsCellSafe(cell))
		{
			return false;
		}
		UprootedMonitor component4 = prefab.GetComponent<UprootedMonitor>();
		if (component4 != null && !component4.IsSuitableFoundation(cell))
		{
			return false;
		}
		OccupyArea component5 = prefab.GetComponent<OccupyArea>();
		return !(component5 != null) || component5.CanOccupyArea(cell, ObjectLayer.Building);
	}

	// Token: 0x06002FF8 RID: 12280 RVA: 0x000FD8AC File Offset: 0x000FBAAC
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.direction == SingleEntityReceptacle.ReceptacleDirection.Bottom)
		{
			Descriptor descriptor = new Descriptor(UI.GAMEOBJECTEFFECTS.SEED_REQUIREMENT_CEILING, UI.GAMEOBJECTEFFECTS.TOOLTIPS.SEED_REQUIREMENT_CEILING, Descriptor.DescriptorType.Requirement, false);
			list.Add(descriptor);
		}
		else if (this.direction == SingleEntityReceptacle.ReceptacleDirection.Side)
		{
			Descriptor descriptor2 = new Descriptor(UI.GAMEOBJECTEFFECTS.SEED_REQUIREMENT_WALL, UI.GAMEOBJECTEFFECTS.TOOLTIPS.SEED_REQUIREMENT_WALL, Descriptor.DescriptorType.Requirement, false);
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x04001CE7 RID: 7399
	public Tag PlantID;

	// Token: 0x04001CE8 RID: 7400
	public Tag PreviewID;

	// Token: 0x04001CE9 RID: 7401
	[Serialize]
	public float timeUntilSelfPlant;

	// Token: 0x04001CEA RID: 7402
	public Tag replantGroundTag;

	// Token: 0x04001CEB RID: 7403
	public string domesticatedDescription;

	// Token: 0x04001CEC RID: 7404
	public SingleEntityReceptacle.ReceptacleDirection direction;

	// Token: 0x04001CED RID: 7405
	[MyCmpGet]
	private Pickupable pickupable;
}
