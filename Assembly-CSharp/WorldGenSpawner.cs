using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using ProcGen;
using ProcGenGame;
using TemplateClasses;
using UnityEngine;

// Token: 0x020009D7 RID: 2519
[AddComponentMenu("KMonoBehaviour/scripts/WorldGenSpawner")]
public class WorldGenSpawner : KMonoBehaviour
{
	// Token: 0x06004B33 RID: 19251 RVA: 0x001A50BB File Offset: 0x001A32BB
	public bool SpawnsRemain()
	{
		return this.spawnables.Count > 0;
	}

	// Token: 0x06004B34 RID: 19252 RVA: 0x001A50CC File Offset: 0x001A32CC
	public void SpawnEverything()
	{
		for (int i = 0; i < this.spawnables.Count; i++)
		{
			this.spawnables[i].TrySpawn();
		}
	}

	// Token: 0x06004B35 RID: 19253 RVA: 0x001A5100 File Offset: 0x001A3300
	public void SpawnTag(string id)
	{
		for (int i = 0; i < this.spawnables.Count; i++)
		{
			if (this.spawnables[i].spawnInfo.id == id)
			{
				this.spawnables[i].TrySpawn();
			}
		}
	}

	// Token: 0x06004B36 RID: 19254 RVA: 0x001A5154 File Offset: 0x001A3354
	public void ClearSpawnersInArea(Vector2 root_position, CellOffset[] area)
	{
		for (int i = 0; i < this.spawnables.Count; i++)
		{
			if (Grid.IsCellOffsetOf(Grid.PosToCell(root_position), this.spawnables[i].cell, area))
			{
				this.spawnables[i].FreeResources();
			}
		}
	}

	// Token: 0x06004B37 RID: 19255 RVA: 0x001A51A7 File Offset: 0x001A33A7
	public IReadOnlyList<WorldGenSpawner.Spawnable> GetSpawnables()
	{
		return this.spawnables;
	}

	// Token: 0x06004B38 RID: 19256 RVA: 0x001A51B0 File Offset: 0x001A33B0
	protected override void OnSpawn()
	{
		if (!this.hasPlacedTemplates)
		{
			global::Debug.Assert(SaveLoader.Instance.ClusterLayout != null, "Trying to place templates for an already-loaded save, no worldgen data available");
			this.DoReveal(SaveLoader.Instance.ClusterLayout);
			this.PlaceTemplates(SaveLoader.Instance.ClusterLayout);
			this.hasPlacedTemplates = true;
		}
		if (this.spawnInfos == null)
		{
			return;
		}
		for (int i = 0; i < this.spawnInfos.Length; i++)
		{
			this.AddSpawnable(this.spawnInfos[i]);
		}
	}

	// Token: 0x06004B39 RID: 19257 RVA: 0x001A5230 File Offset: 0x001A3430
	[OnSerializing]
	private void OnSerializing()
	{
		List<Prefab> list = new List<Prefab>();
		for (int i = 0; i < this.spawnables.Count; i++)
		{
			WorldGenSpawner.Spawnable spawnable = this.spawnables[i];
			if (!spawnable.isSpawned)
			{
				list.Add(spawnable.spawnInfo);
			}
		}
		this.spawnInfos = list.ToArray();
	}

	// Token: 0x06004B3A RID: 19258 RVA: 0x001A5286 File Offset: 0x001A3486
	private void AddSpawnable(Prefab prefab)
	{
		this.spawnables.Add(new WorldGenSpawner.Spawnable(prefab));
	}

	// Token: 0x06004B3B RID: 19259 RVA: 0x001A529C File Offset: 0x001A349C
	public void AddLegacySpawner(Tag tag, int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		this.AddSpawnable(new Prefab(tag.Name, Prefab.Type.Other, vector2I.x, vector2I.y, SimHashes.Carbon, -1f, 1f, null, 0, Orientation.Neutral, null, null, 0));
	}

	// Token: 0x06004B3C RID: 19260 RVA: 0x001A52E4 File Offset: 0x001A34E4
	public List<Tag> GetUnspawnedWithType<T>(int worldID) where T : KMonoBehaviour
	{
		List<Tag> list = new List<Tag>();
		List<WorldGenSpawner.Spawnable> list2 = this.spawnables;
		Predicate<WorldGenSpawner.Spawnable> <>9__0;
		Predicate<WorldGenSpawner.Spawnable> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = (WorldGenSpawner.Spawnable match) => !match.isSpawned && (int)Grid.WorldIdx[match.cell] == worldID && Assets.GetPrefab(match.spawnInfo.id) != null && Assets.GetPrefab(match.spawnInfo.id).GetComponent<T>() != null);
		}
		foreach (WorldGenSpawner.Spawnable spawnable in list2.FindAll(predicate))
		{
			list.Add(spawnable.spawnInfo.id);
		}
		return list;
	}

	// Token: 0x06004B3D RID: 19261 RVA: 0x001A5380 File Offset: 0x001A3580
	public List<Tag> GetSpawnersWithTag(Tag tag, int worldID, bool includeSpawned = false)
	{
		List<Tag> list = new List<Tag>();
		List<WorldGenSpawner.Spawnable> list2 = this.spawnables;
		Predicate<WorldGenSpawner.Spawnable> <>9__0;
		Predicate<WorldGenSpawner.Spawnable> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = (WorldGenSpawner.Spawnable match) => (includeSpawned || !match.isSpawned) && (int)Grid.WorldIdx[match.cell] == worldID && match.spawnInfo.id == tag);
		}
		foreach (WorldGenSpawner.Spawnable spawnable in list2.FindAll(predicate))
		{
			list.Add(spawnable.spawnInfo.id);
		}
		return list;
	}

	// Token: 0x06004B3E RID: 19262 RVA: 0x001A542C File Offset: 0x001A362C
	private void PlaceTemplates(Cluster clusterLayout)
	{
		this.spawnables = new List<WorldGenSpawner.Spawnable>();
		foreach (WorldGen worldGen in clusterLayout.worlds)
		{
			foreach (Prefab prefab in worldGen.SpawnData.buildings)
			{
				prefab.location_x += worldGen.data.world.offset.x;
				prefab.location_y += worldGen.data.world.offset.y;
				prefab.type = Prefab.Type.Building;
				this.AddSpawnable(prefab);
			}
			foreach (Prefab prefab2 in worldGen.SpawnData.elementalOres)
			{
				prefab2.location_x += worldGen.data.world.offset.x;
				prefab2.location_y += worldGen.data.world.offset.y;
				prefab2.type = Prefab.Type.Ore;
				this.AddSpawnable(prefab2);
			}
			foreach (Prefab prefab3 in worldGen.SpawnData.otherEntities)
			{
				prefab3.location_x += worldGen.data.world.offset.x;
				prefab3.location_y += worldGen.data.world.offset.y;
				prefab3.type = Prefab.Type.Other;
				this.AddSpawnable(prefab3);
			}
			foreach (Prefab prefab4 in worldGen.SpawnData.pickupables)
			{
				prefab4.location_x += worldGen.data.world.offset.x;
				prefab4.location_y += worldGen.data.world.offset.y;
				prefab4.type = Prefab.Type.Pickupable;
				this.AddSpawnable(prefab4);
			}
			worldGen.SpawnData.buildings.Clear();
			worldGen.SpawnData.elementalOres.Clear();
			worldGen.SpawnData.otherEntities.Clear();
			worldGen.SpawnData.pickupables.Clear();
		}
	}

	// Token: 0x06004B3F RID: 19263 RVA: 0x001A575C File Offset: 0x001A395C
	private void DoReveal(Cluster clusterLayout)
	{
		foreach (WorldGen worldGen in clusterLayout.worlds)
		{
			Game.Instance.Reset(worldGen.SpawnData, worldGen.WorldOffset);
		}
		for (int i = 0; i < Grid.CellCount; i++)
		{
			Grid.Revealed[i] = false;
			Grid.Spawnable[i] = 0;
		}
		float num = 16.5f;
		int num2 = 18;
		Vector2I vector2I = clusterLayout.currentWorld.SpawnData.baseStartPos;
		vector2I += clusterLayout.currentWorld.WorldOffset;
		GridVisibility.Reveal(vector2I.x, vector2I.y, num2, num);
	}

	// Token: 0x0400314E RID: 12622
	[Serialize]
	private Prefab[] spawnInfos;

	// Token: 0x0400314F RID: 12623
	[Serialize]
	private bool hasPlacedTemplates;

	// Token: 0x04003150 RID: 12624
	private List<WorldGenSpawner.Spawnable> spawnables = new List<WorldGenSpawner.Spawnable>();

	// Token: 0x020017D5 RID: 6101
	public class Spawnable
	{
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06008C0D RID: 35853 RVA: 0x0030159D File Offset: 0x002FF79D
		// (set) Token: 0x06008C0E RID: 35854 RVA: 0x003015A5 File Offset: 0x002FF7A5
		public Prefab spawnInfo { get; private set; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06008C0F RID: 35855 RVA: 0x003015AE File Offset: 0x002FF7AE
		// (set) Token: 0x06008C10 RID: 35856 RVA: 0x003015B6 File Offset: 0x002FF7B6
		public bool isSpawned { get; private set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06008C11 RID: 35857 RVA: 0x003015BF File Offset: 0x002FF7BF
		// (set) Token: 0x06008C12 RID: 35858 RVA: 0x003015C7 File Offset: 0x002FF7C7
		public int cell { get; private set; }

		// Token: 0x06008C13 RID: 35859 RVA: 0x003015D0 File Offset: 0x002FF7D0
		public Spawnable(Prefab spawn_info)
		{
			this.spawnInfo = spawn_info;
			int num = Grid.XYToCell(this.spawnInfo.location_x, this.spawnInfo.location_y);
			GameObject prefab = Assets.GetPrefab(spawn_info.id);
			if (prefab != null)
			{
				WorldSpawnableMonitor.Def def = prefab.GetDef<WorldSpawnableMonitor.Def>();
				if (def != null && def.adjustSpawnLocationCb != null)
				{
					num = def.adjustSpawnLocationCb(num);
				}
			}
			this.cell = num;
			global::Debug.Assert(Grid.IsValidCell(this.cell));
			if (Grid.Spawnable[this.cell] > 0)
			{
				this.TrySpawn();
				return;
			}
			this.fogOfWarPartitionerEntry = GameScenePartitioner.Instance.Add("WorldGenSpawner.OnReveal", this, this.cell, GameScenePartitioner.Instance.fogOfWarChangedLayer, new Action<object>(this.OnReveal));
		}

		// Token: 0x06008C14 RID: 35860 RVA: 0x0030169E File Offset: 0x002FF89E
		private void OnReveal(object data)
		{
			if (Grid.Spawnable[this.cell] > 0)
			{
				this.TrySpawn();
			}
		}

		// Token: 0x06008C15 RID: 35861 RVA: 0x003016B5 File Offset: 0x002FF8B5
		private void OnSolidChanged(object data)
		{
			if (!Grid.Solid[this.cell])
			{
				GameScenePartitioner.Instance.Free(ref this.solidChangedPartitionerEntry);
				Game.Instance.GetComponent<EntombedItemVisualizer>().RemoveItem(this.cell);
				this.Spawn();
			}
		}

		// Token: 0x06008C16 RID: 35862 RVA: 0x003016F4 File Offset: 0x002FF8F4
		public void FreeResources()
		{
			if (this.solidChangedPartitionerEntry.IsValid())
			{
				GameScenePartitioner.Instance.Free(ref this.solidChangedPartitionerEntry);
				if (Game.Instance != null)
				{
					Game.Instance.GetComponent<EntombedItemVisualizer>().RemoveItem(this.cell);
				}
			}
			GameScenePartitioner.Instance.Free(ref this.fogOfWarPartitionerEntry);
			this.isSpawned = true;
		}

		// Token: 0x06008C17 RID: 35863 RVA: 0x00301758 File Offset: 0x002FF958
		public void TrySpawn()
		{
			if (this.isSpawned)
			{
				return;
			}
			if (this.solidChangedPartitionerEntry.IsValid())
			{
				return;
			}
			WorldContainer world = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[this.cell]);
			bool flag = world != null && world.IsDiscovered;
			GameObject prefab = Assets.GetPrefab(this.GetPrefabTag());
			if (!(prefab != null))
			{
				if (flag)
				{
					GameScenePartitioner.Instance.Free(ref this.fogOfWarPartitionerEntry);
					this.Spawn();
				}
				return;
			}
			if (!(flag | prefab.HasTag(GameTags.WarpTech)))
			{
				return;
			}
			GameScenePartitioner.Instance.Free(ref this.fogOfWarPartitionerEntry);
			bool flag2 = false;
			if (prefab.GetComponent<Pickupable>() != null && !prefab.HasTag(GameTags.Creatures.Digger))
			{
				flag2 = true;
			}
			else if (prefab.GetDef<BurrowMonitor.Def>() != null)
			{
				flag2 = true;
			}
			if (flag2 && Grid.Solid[this.cell])
			{
				this.solidChangedPartitionerEntry = GameScenePartitioner.Instance.Add("WorldGenSpawner.OnSolidChanged", this, this.cell, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
				Game.Instance.GetComponent<EntombedItemVisualizer>().AddItem(this.cell);
				return;
			}
			this.Spawn();
		}

		// Token: 0x06008C18 RID: 35864 RVA: 0x0030188C File Offset: 0x002FFA8C
		private Tag GetPrefabTag()
		{
			Mob mob = SettingsCache.mobs.GetMob(this.spawnInfo.id);
			if (mob != null && mob.prefabName != null)
			{
				return new Tag(mob.prefabName);
			}
			return new Tag(this.spawnInfo.id);
		}

		// Token: 0x06008C19 RID: 35865 RVA: 0x003018D8 File Offset: 0x002FFAD8
		private void Spawn()
		{
			this.isSpawned = true;
			GameObject gameObject = WorldGenSpawner.Spawnable.GetSpawnableCallback(this.spawnInfo.type)(this.spawnInfo, 0);
			if (gameObject != null && gameObject)
			{
				gameObject.SetActive(true);
				gameObject.Trigger(1119167081, null);
			}
			this.FreeResources();
		}

		// Token: 0x06008C1A RID: 35866 RVA: 0x00301934 File Offset: 0x002FFB34
		public static WorldGenSpawner.Spawnable.PlaceEntityFn GetSpawnableCallback(Prefab.Type type)
		{
			switch (type)
			{
			case Prefab.Type.Building:
				return new WorldGenSpawner.Spawnable.PlaceEntityFn(TemplateLoader.PlaceBuilding);
			case Prefab.Type.Ore:
				return new WorldGenSpawner.Spawnable.PlaceEntityFn(TemplateLoader.PlaceElementalOres);
			case Prefab.Type.Pickupable:
				return new WorldGenSpawner.Spawnable.PlaceEntityFn(TemplateLoader.PlacePickupables);
			case Prefab.Type.Other:
				return new WorldGenSpawner.Spawnable.PlaceEntityFn(TemplateLoader.PlaceOtherEntities);
			default:
				return new WorldGenSpawner.Spawnable.PlaceEntityFn(TemplateLoader.PlaceOtherEntities);
			}
		}

		// Token: 0x04006E2D RID: 28205
		private HandleVector<int>.Handle fogOfWarPartitionerEntry;

		// Token: 0x04006E2E RID: 28206
		private HandleVector<int>.Handle solidChangedPartitionerEntry;

		// Token: 0x020020DD RID: 8413
		// (Invoke) Token: 0x0600A559 RID: 42329
		public delegate GameObject PlaceEntityFn(Prefab prefab, int root_cell);
	}
}
