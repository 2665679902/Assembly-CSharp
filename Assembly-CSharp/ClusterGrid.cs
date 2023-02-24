using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using STRINGS;
using UnityEngine;

// Token: 0x0200096A RID: 2410
public class ClusterGrid
{
	// Token: 0x0600477A RID: 18298 RVA: 0x00192508 File Offset: 0x00190708
	public static void DestroyInstance()
	{
		ClusterGrid.Instance = null;
	}

	// Token: 0x0600477B RID: 18299 RVA: 0x00192510 File Offset: 0x00190710
	private ClusterFogOfWarManager.Instance GetFOWManager()
	{
		if (this.m_fowManager == null)
		{
			this.m_fowManager = SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>();
		}
		return this.m_fowManager;
	}

	// Token: 0x0600477C RID: 18300 RVA: 0x00192530 File Offset: 0x00190730
	public bool IsValidCell(AxialI cell)
	{
		return this.cellContents.ContainsKey(cell);
	}

	// Token: 0x0600477D RID: 18301 RVA: 0x0019253E File Offset: 0x0019073E
	public ClusterGrid(int numRings)
	{
		ClusterGrid.Instance = this;
		this.GenerateGrid(numRings);
		this.m_onClusterLocationChangedDelegate = new Action<object>(this.OnClusterLocationChanged);
	}

	// Token: 0x0600477E RID: 18302 RVA: 0x00192570 File Offset: 0x00190770
	public ClusterRevealLevel GetCellRevealLevel(AxialI cell)
	{
		return this.GetFOWManager().GetCellRevealLevel(cell);
	}

	// Token: 0x0600477F RID: 18303 RVA: 0x0019257E File Offset: 0x0019077E
	public bool IsCellVisible(AxialI cell)
	{
		return this.GetFOWManager().IsLocationRevealed(cell);
	}

	// Token: 0x06004780 RID: 18304 RVA: 0x0019258C File Offset: 0x0019078C
	public float GetRevealCompleteFraction(AxialI cell)
	{
		return this.GetFOWManager().GetRevealCompleteFraction(cell);
	}

	// Token: 0x06004781 RID: 18305 RVA: 0x0019259A File Offset: 0x0019079A
	public bool IsVisible(ClusterGridEntity entity)
	{
		return entity.IsVisible && this.IsCellVisible(entity.Location);
	}

	// Token: 0x06004782 RID: 18306 RVA: 0x001925B4 File Offset: 0x001907B4
	public List<ClusterGridEntity> GetVisibleEntitiesAtCell(AxialI cell)
	{
		if (this.IsValidCell(cell) && this.GetFOWManager().IsLocationRevealed(cell))
		{
			return this.cellContents[cell].Where((ClusterGridEntity entity) => entity.IsVisible).ToList<ClusterGridEntity>();
		}
		return new List<ClusterGridEntity>();
	}

	// Token: 0x06004783 RID: 18307 RVA: 0x00192614 File Offset: 0x00190814
	public ClusterGridEntity GetVisibleEntityOfLayerAtCell(AxialI cell, EntityLayer entityLayer)
	{
		if (this.IsValidCell(cell) && this.GetFOWManager().IsLocationRevealed(cell))
		{
			foreach (ClusterGridEntity clusterGridEntity in this.cellContents[cell])
			{
				if (clusterGridEntity.IsVisible && clusterGridEntity.Layer == entityLayer)
				{
					return clusterGridEntity;
				}
			}
		}
		return null;
	}

	// Token: 0x06004784 RID: 18308 RVA: 0x00192698 File Offset: 0x00190898
	public ClusterGridEntity GetVisibleEntityOfLayerAtAdjacentCell(AxialI cell, EntityLayer entityLayer)
	{
		return AxialUtil.GetRing(cell, 1).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetVisibleEntitiesAtCell)).FirstOrDefault((ClusterGridEntity entity) => entity.Layer == entityLayer);
	}

	// Token: 0x06004785 RID: 18309 RVA: 0x001926DC File Offset: 0x001908DC
	public List<ClusterGridEntity> GetHiddenEntitiesOfLayerAtCell(AxialI cell, EntityLayer entityLayer)
	{
		return (from entity in AxialUtil.GetRing(cell, 0).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetHiddenEntitiesAtCell))
			where entity.Layer == entityLayer
			select entity).ToList<ClusterGridEntity>();
	}

	// Token: 0x06004786 RID: 18310 RVA: 0x00192724 File Offset: 0x00190924
	public ClusterGridEntity GetEntityOfLayerAtCell(AxialI cell, EntityLayer entityLayer)
	{
		return AxialUtil.GetRing(cell, 0).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetEntitiesOnCell)).FirstOrDefault((ClusterGridEntity entity) => entity.Layer == entityLayer);
	}

	// Token: 0x06004787 RID: 18311 RVA: 0x00192768 File Offset: 0x00190968
	public List<ClusterGridEntity> GetHiddenEntitiesAtCell(AxialI cell)
	{
		if (this.cellContents.ContainsKey(cell) && !this.GetFOWManager().IsLocationRevealed(cell))
		{
			return this.cellContents[cell].Where((ClusterGridEntity entity) => entity.IsVisible).ToList<ClusterGridEntity>();
		}
		return new List<ClusterGridEntity>();
	}

	// Token: 0x06004788 RID: 18312 RVA: 0x001927CC File Offset: 0x001909CC
	public List<ClusterGridEntity> GetNotVisibleEntitiesAtAdjacentCell(AxialI cell)
	{
		return AxialUtil.GetRing(cell, 1).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetHiddenEntitiesAtCell)).ToList<ClusterGridEntity>();
	}

	// Token: 0x06004789 RID: 18313 RVA: 0x001927EC File Offset: 0x001909EC
	public List<ClusterGridEntity> GetNotVisibleEntitiesOfLayerAtAdjacentCell(AxialI cell, EntityLayer entityLayer)
	{
		return (from entity in AxialUtil.GetRing(cell, 1).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetHiddenEntitiesAtCell))
			where entity.Layer == entityLayer
			select entity).ToList<ClusterGridEntity>();
	}

	// Token: 0x0600478A RID: 18314 RVA: 0x00192834 File Offset: 0x00190A34
	public ClusterGridEntity GetAsteroidAtCell(AxialI cell)
	{
		if (!this.cellContents.ContainsKey(cell))
		{
			return null;
		}
		return this.cellContents[cell].Where((ClusterGridEntity e) => e.Layer == EntityLayer.Asteroid).FirstOrDefault<ClusterGridEntity>();
	}

	// Token: 0x0600478B RID: 18315 RVA: 0x00192886 File Offset: 0x00190A86
	public bool HasVisibleAsteroidAtCell(AxialI cell)
	{
		return this.GetVisibleEntityOfLayerAtCell(cell, EntityLayer.Asteroid) != null;
	}

	// Token: 0x0600478C RID: 18316 RVA: 0x00192896 File Offset: 0x00190A96
	public void RegisterEntity(ClusterGridEntity entity)
	{
		this.cellContents[entity.Location].Add(entity);
		entity.Subscribe(-1298331547, this.m_onClusterLocationChangedDelegate);
	}

	// Token: 0x0600478D RID: 18317 RVA: 0x001928C1 File Offset: 0x00190AC1
	public void UnregisterEntity(ClusterGridEntity entity)
	{
		this.cellContents[entity.Location].Remove(entity);
		entity.Unsubscribe(-1298331547, this.m_onClusterLocationChangedDelegate);
	}

	// Token: 0x0600478E RID: 18318 RVA: 0x001928EC File Offset: 0x00190AEC
	public void OnClusterLocationChanged(object data)
	{
		ClusterLocationChangedEvent clusterLocationChangedEvent = (ClusterLocationChangedEvent)data;
		global::Debug.Assert(this.IsValidCell(clusterLocationChangedEvent.oldLocation), string.Format("ChangeEntityCell move order FROM invalid location: {0} {1}", clusterLocationChangedEvent.oldLocation, clusterLocationChangedEvent.entity));
		global::Debug.Assert(this.IsValidCell(clusterLocationChangedEvent.newLocation), string.Format("ChangeEntityCell move order TO invalid location: {0} {1}", clusterLocationChangedEvent.newLocation, clusterLocationChangedEvent.entity));
		this.cellContents[clusterLocationChangedEvent.oldLocation].Remove(clusterLocationChangedEvent.entity);
		this.cellContents[clusterLocationChangedEvent.newLocation].Add(clusterLocationChangedEvent.entity);
	}

	// Token: 0x0600478F RID: 18319 RVA: 0x00192991 File Offset: 0x00190B91
	private AxialI GetNeighbor(AxialI cell, AxialI direction)
	{
		return cell + direction;
	}

	// Token: 0x06004790 RID: 18320 RVA: 0x0019299C File Offset: 0x00190B9C
	public int GetCellRing(AxialI cell)
	{
		Vector3I vector3I = cell.ToCube();
		Vector3I vector3I2 = new Vector3I(vector3I.x, vector3I.y, vector3I.z);
		Vector3I vector3I3 = new Vector3I(0, 0, 0);
		return (int)((float)((Mathf.Abs(vector3I2.x - vector3I3.x) + Mathf.Abs(vector3I2.y - vector3I3.y) + Mathf.Abs(vector3I2.z - vector3I3.z)) / 2));
	}

	// Token: 0x06004791 RID: 18321 RVA: 0x00192A10 File Offset: 0x00190C10
	private void CleanUpGrid()
	{
		this.cellContents.Clear();
	}

	// Token: 0x06004792 RID: 18322 RVA: 0x00192A20 File Offset: 0x00190C20
	private int GetHexDistance(AxialI a, AxialI b)
	{
		Vector3I vector3I = a.ToCube();
		Vector3I vector3I2 = b.ToCube();
		return Mathf.Max(new int[]
		{
			Mathf.Abs(vector3I.x - vector3I2.x),
			Mathf.Abs(vector3I.y - vector3I2.y),
			Mathf.Abs(vector3I.z - vector3I2.z)
		});
	}

	// Token: 0x06004793 RID: 18323 RVA: 0x00192A88 File Offset: 0x00190C88
	public List<ClusterGridEntity> GetEntitiesInRange(AxialI center, int range = 1)
	{
		List<ClusterGridEntity> list = new List<ClusterGridEntity>();
		foreach (KeyValuePair<AxialI, List<ClusterGridEntity>> keyValuePair in this.cellContents)
		{
			if (this.GetHexDistance(keyValuePair.Key, center) <= range)
			{
				list.AddRange(keyValuePair.Value);
			}
		}
		return list;
	}

	// Token: 0x06004794 RID: 18324 RVA: 0x00192AFC File Offset: 0x00190CFC
	public List<ClusterGridEntity> GetEntitiesOnCell(AxialI cell)
	{
		return this.cellContents[cell];
	}

	// Token: 0x06004795 RID: 18325 RVA: 0x00192B0A File Offset: 0x00190D0A
	public bool IsInRange(AxialI a, AxialI b, int range = 1)
	{
		return this.GetHexDistance(a, b) <= range;
	}

	// Token: 0x06004796 RID: 18326 RVA: 0x00192B1C File Offset: 0x00190D1C
	private void GenerateGrid(int rings)
	{
		this.CleanUpGrid();
		this.numRings = rings;
		for (int i = -rings + 1; i < rings; i++)
		{
			for (int j = -rings + 1; j < rings; j++)
			{
				for (int k = -rings + 1; k < rings; k++)
				{
					if (i + j + k == 0)
					{
						AxialI axialI = new AxialI(i, j);
						this.cellContents.Add(axialI, new List<ClusterGridEntity>());
					}
				}
			}
		}
	}

	// Token: 0x06004797 RID: 18327 RVA: 0x00192B84 File Offset: 0x00190D84
	public Vector3 GetPosition(ClusterGridEntity entity)
	{
		float num = (float)entity.Location.R;
		float num2 = (float)entity.Location.Q;
		List<ClusterGridEntity> list = this.cellContents[entity.Location];
		if (list.Count <= 1 || !entity.SpaceOutInSameHex())
		{
			return AxialUtil.AxialToWorld(num, num2);
		}
		int num3 = 0;
		int num4 = 0;
		foreach (ClusterGridEntity clusterGridEntity in list)
		{
			if (entity == clusterGridEntity)
			{
				num3 = num4;
			}
			if (clusterGridEntity.SpaceOutInSameHex())
			{
				num4++;
			}
		}
		if (list.Count > num4)
		{
			num4 += 5;
			num3 += 5;
		}
		else if (num4 > 0)
		{
			num4++;
			num3++;
		}
		if (num4 == 0 || num4 == 1)
		{
			return AxialUtil.AxialToWorld(num, num2);
		}
		float num5 = Mathf.Min(Mathf.Pow((float)num4, 0.5f), 1f) * 0.5f;
		float num6 = Mathf.Pow((float)num3 / (float)num4, 0.5f);
		float num7 = 0.81f;
		float num8 = Mathf.Pow((float)num4, 0.5f) * num7;
		float num9 = 6.2831855f * num8 * num6;
		float num10 = Mathf.Cos(num9) * num5 * num6;
		float num11 = Mathf.Sin(num9) * num5 * num6;
		return AxialUtil.AxialToWorld(num, num2) + new Vector3(num10, num11, 0f);
	}

	// Token: 0x06004798 RID: 18328 RVA: 0x00192D08 File Offset: 0x00190F08
	public List<AxialI> GetPath(AxialI start, AxialI end, ClusterDestinationSelector destination_selector)
	{
		string text;
		return this.GetPath(start, end, destination_selector, out text);
	}

	// Token: 0x06004799 RID: 18329 RVA: 0x00192D20 File Offset: 0x00190F20
	public List<AxialI> GetPath(AxialI start, AxialI end, ClusterDestinationSelector destination_selector, out string fail_reason)
	{
		ClusterGrid.<>c__DisplayClass38_0 CS$<>8__locals1;
		CS$<>8__locals1.<>4__this = this;
		CS$<>8__locals1.destination_selector = destination_selector;
		CS$<>8__locals1.start = start;
		CS$<>8__locals1.end = end;
		fail_reason = null;
		if (!CS$<>8__locals1.destination_selector.canNavigateFogOfWar && !this.IsCellVisible(CS$<>8__locals1.end))
		{
			fail_reason = UI.CLUSTERMAP.TOOLTIP_INVALID_DESTINATION_FOG_OF_WAR;
			return null;
		}
		ClusterGridEntity visibleEntityOfLayerAtCell = this.GetVisibleEntityOfLayerAtCell(CS$<>8__locals1.end, EntityLayer.Asteroid);
		if (visibleEntityOfLayerAtCell != null && CS$<>8__locals1.destination_selector.requireLaunchPadOnAsteroidDestination)
		{
			bool flag = false;
			using (IEnumerator enumerator = Components.LaunchPads.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((LaunchPad)enumerator.Current).GetMyWorldLocation() == visibleEntityOfLayerAtCell.Location)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				fail_reason = UI.CLUSTERMAP.TOOLTIP_INVALID_DESTINATION_NO_LAUNCH_PAD;
				return null;
			}
		}
		if (visibleEntityOfLayerAtCell == null && CS$<>8__locals1.destination_selector.requireAsteroidDestination)
		{
			fail_reason = UI.CLUSTERMAP.TOOLTIP_INVALID_DESTINATION_REQUIRE_ASTEROID;
			return null;
		}
		CS$<>8__locals1.frontier = new HashSet<AxialI>();
		CS$<>8__locals1.visited = new HashSet<AxialI>();
		CS$<>8__locals1.buffer = new HashSet<AxialI>();
		CS$<>8__locals1.cameFrom = new Dictionary<AxialI, AxialI>();
		CS$<>8__locals1.frontier.Add(CS$<>8__locals1.start);
		while (!CS$<>8__locals1.frontier.Contains(CS$<>8__locals1.end) && CS$<>8__locals1.frontier.Count > 0)
		{
			this.<GetPath>g__ExpandFrontier|38_0(ref CS$<>8__locals1);
		}
		if (CS$<>8__locals1.frontier.Contains(CS$<>8__locals1.end))
		{
			List<AxialI> list = new List<AxialI>();
			AxialI axialI = CS$<>8__locals1.end;
			while (axialI != CS$<>8__locals1.start)
			{
				list.Add(axialI);
				axialI = CS$<>8__locals1.cameFrom[axialI];
			}
			list.Reverse();
			return list;
		}
		fail_reason = UI.CLUSTERMAP.TOOLTIP_INVALID_DESTINATION_NO_PATH;
		return null;
	}

	// Token: 0x0600479A RID: 18330 RVA: 0x00192F04 File Offset: 0x00191104
	public void GetLocationDescription(AxialI location, out Sprite sprite, out string label, out string sublabel)
	{
		ClusterGridEntity clusterGridEntity = this.GetVisibleEntitiesAtCell(location).Find((ClusterGridEntity x) => x.Layer == EntityLayer.Asteroid);
		ClusterGridEntity visibleEntityOfLayerAtAdjacentCell = this.GetVisibleEntityOfLayerAtAdjacentCell(location, EntityLayer.Asteroid);
		if (clusterGridEntity != null)
		{
			sprite = clusterGridEntity.GetUISprite();
			label = clusterGridEntity.Name;
			WorldContainer component = clusterGridEntity.GetComponent<WorldContainer>();
			sublabel = Strings.Get(component.worldType);
			return;
		}
		if (visibleEntityOfLayerAtAdjacentCell != null)
		{
			sprite = visibleEntityOfLayerAtAdjacentCell.GetUISprite();
			label = UI.SPACEDESTINATIONS.ORBIT.NAME_FMT.Replace("{Name}", visibleEntityOfLayerAtAdjacentCell.Name);
			WorldContainer component2 = visibleEntityOfLayerAtAdjacentCell.GetComponent<WorldContainer>();
			sublabel = Strings.Get(component2.worldType);
			return;
		}
		if (this.IsCellVisible(location))
		{
			sprite = Assets.GetSprite("hex_unknown");
			label = UI.SPACEDESTINATIONS.EMPTY_SPACE.NAME;
			sublabel = "";
			return;
		}
		sprite = Assets.GetSprite("unknown_far");
		label = UI.SPACEDESTINATIONS.FOG_OF_WAR_SPACE.NAME;
		sublabel = "";
	}

	// Token: 0x0600479B RID: 18331 RVA: 0x00193014 File Offset: 0x00191214
	[CompilerGenerated]
	private void <GetPath>g__ExpandFrontier|38_0(ref ClusterGrid.<>c__DisplayClass38_0 A_1)
	{
		A_1.buffer.Clear();
		foreach (AxialI axialI in A_1.frontier)
		{
			foreach (AxialI axialI2 in AxialI.DIRECTIONS)
			{
				AxialI neighbor = this.GetNeighbor(axialI, axialI2);
				if (!A_1.visited.Contains(neighbor) && this.IsValidCell(neighbor) && (this.IsCellVisible(neighbor) || A_1.destination_selector.canNavigateFogOfWar) && (!this.HasVisibleAsteroidAtCell(neighbor) || !(neighbor != A_1.start) || !(neighbor != A_1.end)))
				{
					A_1.buffer.Add(neighbor);
					if (!A_1.cameFrom.ContainsKey(neighbor))
					{
						A_1.cameFrom.Add(neighbor, axialI);
					}
				}
			}
			A_1.visited.Add(axialI);
		}
		HashSet<AxialI> frontier = A_1.frontier;
		A_1.frontier = A_1.buffer;
		A_1.buffer = frontier;
	}

	// Token: 0x04002F3B RID: 12091
	public static ClusterGrid Instance;

	// Token: 0x04002F3C RID: 12092
	public const float NodeDistanceScale = 600f;

	// Token: 0x04002F3D RID: 12093
	private const float MAX_OFFSET_RADIUS = 0.5f;

	// Token: 0x04002F3E RID: 12094
	public int numRings;

	// Token: 0x04002F3F RID: 12095
	private ClusterFogOfWarManager.Instance m_fowManager;

	// Token: 0x04002F40 RID: 12096
	private Action<object> m_onClusterLocationChangedDelegate;

	// Token: 0x04002F41 RID: 12097
	public Dictionary<AxialI, List<ClusterGridEntity>> cellContents = new Dictionary<AxialI, List<ClusterGridEntity>>();
}
