using System;
using System.Collections.Generic;
using ProcGenGame;
using UnityEngine;

// Token: 0x02000C37 RID: 3127
[AddComponentMenu("KMonoBehaviour/scripts/CavityVisualizer")]
public class CavityVisualizer : KMonoBehaviour
{
	// Token: 0x060062EA RID: 25322 RVA: 0x00248C14 File Offset: 0x00246E14
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		foreach (TerrainCell terrainCell in MobSpawning.NaturalCavities.Keys)
		{
			foreach (HashSet<int> hashSet in MobSpawning.NaturalCavities[terrainCell])
			{
				foreach (int num in hashSet)
				{
					this.cavityCells.Add(num);
				}
			}
		}
	}

	// Token: 0x060062EB RID: 25323 RVA: 0x00248CEC File Offset: 0x00246EEC
	private void OnDrawGizmosSelected()
	{
		if (this.drawCavity)
		{
			Color[] array = new Color[]
			{
				Color.blue,
				Color.yellow
			};
			int num = 0;
			foreach (TerrainCell terrainCell in MobSpawning.NaturalCavities.Keys)
			{
				Gizmos.color = array[num % array.Length];
				Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.125f);
				num++;
				foreach (HashSet<int> hashSet in MobSpawning.NaturalCavities[terrainCell])
				{
					foreach (int num2 in hashSet)
					{
						Gizmos.DrawCube(Grid.CellToPos(num2) + (Vector3.right / 2f + Vector3.up / 2f), Vector3.one);
					}
				}
			}
		}
		if (this.spawnCells != null && this.drawSpawnCells)
		{
			Gizmos.color = new Color(0f, 1f, 0f, 0.15f);
			foreach (int num3 in this.spawnCells)
			{
				Gizmos.DrawCube(Grid.CellToPos(num3) + (Vector3.right / 2f + Vector3.up / 2f), Vector3.one);
			}
		}
	}

	// Token: 0x040044A7 RID: 17575
	public List<int> cavityCells = new List<int>();

	// Token: 0x040044A8 RID: 17576
	public List<int> spawnCells = new List<int>();

	// Token: 0x040044A9 RID: 17577
	public bool drawCavity = true;

	// Token: 0x040044AA RID: 17578
	public bool drawSpawnCells = true;
}
