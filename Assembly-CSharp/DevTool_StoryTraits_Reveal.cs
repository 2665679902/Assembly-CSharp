using System;
using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

// Token: 0x02000534 RID: 1332
public class DevTool_StoryTraits_Reveal : DevTool
{
	// Token: 0x06001FF5 RID: 8181 RVA: 0x000AE674 File Offset: 0x000AC874
	protected override void RenderTo(DevPanel panel)
	{
		Option<int> cellIndexForUniqueBuilding = DevToolUtil.GetCellIndexForUniqueBuilding("Headquarters");
		bool flag = cellIndexForUniqueBuilding.IsSome();
		if (ImGuiEx.Button("Focus on headquaters", flag))
		{
			DevToolUtil.FocusCameraOnCell(cellIndexForUniqueBuilding.Unwrap());
		}
		if (!flag)
		{
			ImGuiEx.TooltipForPrevious("Couldn't find headquaters");
		}
		if (ImGui.CollapsingHeader("Search world for entity", ImGuiTreeNodeFlags.DefaultOpen))
		{
			Option<IReadOnlyList<WorldGenSpawner.Spawnable>> allSpawnables = this.GetAllSpawnables();
			if (!allSpawnables.HasValue)
			{
				ImGui.Text("Couldn't find a list of spawnables");
				return;
			}
			foreach (string text in this.GetPrefabIDsToSearchFor())
			{
				Option<int> cellIndexForSpawnable = this.GetCellIndexForSpawnable(text, allSpawnables.Value);
				string text2 = "\"" + text + "\"";
				bool hasValue = cellIndexForSpawnable.HasValue;
				if (ImGuiEx.Button("Reveal and focus on " + text2, hasValue))
				{
					DevToolUtil.RevealAndFocusAt(cellIndexForSpawnable.Value);
				}
				if (!hasValue)
				{
					ImGuiEx.TooltipForPrevious("Couldn't find a cell that contained a spawnable with component " + text2);
				}
			}
		}
	}

	// Token: 0x06001FF6 RID: 8182 RVA: 0x000AE784 File Offset: 0x000AC984
	public IEnumerable<string> GetPrefabIDsToSearchFor()
	{
		yield return "MegaBrainTank";
		yield return "GravitasCreatureManipulator";
		yield break;
	}

	// Token: 0x06001FF7 RID: 8183 RVA: 0x000AE78D File Offset: 0x000AC98D
	private Option<ClusterManager> GetClusterManager()
	{
		if (ClusterManager.Instance == null)
		{
			return Option.None;
		}
		return ClusterManager.Instance;
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x000AE7B4 File Offset: 0x000AC9B4
	private Option<int> GetCellIndexForSpawnable(string prefabId, IReadOnlyList<WorldGenSpawner.Spawnable> spawnablesToSearch)
	{
		foreach (WorldGenSpawner.Spawnable spawnable in spawnablesToSearch)
		{
			if (prefabId == spawnable.spawnInfo.id)
			{
				return spawnable.cell;
			}
		}
		return Option.None;
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x000AE824 File Offset: 0x000ACA24
	private Option<IReadOnlyList<WorldGenSpawner.Spawnable>> GetAllSpawnables()
	{
		WorldGenSpawner worldGenSpawner = UnityEngine.Object.FindObjectOfType<WorldGenSpawner>(true);
		if (worldGenSpawner == null)
		{
			return Option.None;
		}
		IReadOnlyList<WorldGenSpawner.Spawnable> spawnables = worldGenSpawner.GetSpawnables();
		if (spawnables == null)
		{
			return Option.None;
		}
		return Option.Some<IReadOnlyList<WorldGenSpawner.Spawnable>>(spawnables);
	}
}
