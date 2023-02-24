using System;
using UnityEngine;

// Token: 0x02000531 RID: 1329
public static class DevToolUtil
{
	// Token: 0x06001FE2 RID: 8162 RVA: 0x000AE228 File Offset: 0x000AC428
	public static DevPanel Open(DevTool devTool)
	{
		return DevToolManager.Instance.panels.AddPanelFor(devTool);
	}

	// Token: 0x06001FE3 RID: 8163 RVA: 0x000AE23A File Offset: 0x000AC43A
	public static DevPanel Open<T>() where T : DevTool, new()
	{
		return DevToolManager.Instance.panels.AddPanelFor<T>();
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x000AE24B File Offset: 0x000AC44B
	public static void Close(DevTool devTool)
	{
		devTool.ClosePanel();
	}

	// Token: 0x06001FE5 RID: 8165 RVA: 0x000AE253 File Offset: 0x000AC453
	public static void Close(DevPanel devPanel)
	{
		devPanel.Close();
	}

	// Token: 0x06001FE6 RID: 8166 RVA: 0x000AE25B File Offset: 0x000AC45B
	public static string GenerateDevToolName(DevTool devTool)
	{
		return DevToolUtil.GenerateDevToolName(devTool.GetType());
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x000AE268 File Offset: 0x000AC468
	public static string GenerateDevToolName(Type devToolType)
	{
		string text;
		if (DevToolManager.Instance != null && DevToolManager.Instance.devToolNameDict.TryGetValue(devToolType, out text))
		{
			return text;
		}
		string text2 = devToolType.Name;
		if (text2.StartsWith("DevTool_"))
		{
			text2 = text2.Substring("DevTool_".Length);
		}
		else if (text2.StartsWith("DevTool"))
		{
			text2 = text2.Substring("DevTool".Length);
		}
		return text2;
	}

	// Token: 0x06001FE8 RID: 8168 RVA: 0x000AE2D8 File Offset: 0x000AC4D8
	public static bool CanRevealAndFocus(GameObject gameObject)
	{
		return DevToolUtil.GetCellIndexFor(gameObject).HasValue;
	}

	// Token: 0x06001FE9 RID: 8169 RVA: 0x000AE2F4 File Offset: 0x000AC4F4
	public static void RevealAndFocus(GameObject gameObject)
	{
		Option<int> cellIndexFor = DevToolUtil.GetCellIndexFor(gameObject);
		if (!cellIndexFor.HasValue)
		{
			return;
		}
		DevToolUtil.RevealAndFocusAt(cellIndexFor.Value);
		if (!gameObject.GetComponent<KSelectable>().IsNullOrDestroyed())
		{
			SelectTool.Instance.Select(gameObject.GetComponent<KSelectable>(), false);
			return;
		}
		SelectTool.Instance.Select(null, false);
	}

	// Token: 0x06001FEA RID: 8170 RVA: 0x000AE34C File Offset: 0x000AC54C
	public static void FocusCameraOnCell(int cellIndex)
	{
		Vector3 vector = Grid.CellToPos2D(cellIndex);
		CameraController.Instance.SetPosition(vector);
	}

	// Token: 0x06001FEB RID: 8171 RVA: 0x000AE36B File Offset: 0x000AC56B
	public static Option<int> GetCellIndexFor(GameObject gameObject)
	{
		if (gameObject.IsNullOrDestroyed())
		{
			return Option.None;
		}
		if (!gameObject.GetComponent<RectTransform>().IsNullOrDestroyed())
		{
			return Option.None;
		}
		return Grid.PosToCell(gameObject);
	}

	// Token: 0x06001FEC RID: 8172 RVA: 0x000AE3A4 File Offset: 0x000AC5A4
	public static Option<int> GetCellIndexForUniqueBuilding(string prefabId)
	{
		BuildingComplete[] array = UnityEngine.Object.FindObjectsOfType<BuildingComplete>(true);
		if (array == null)
		{
			return Option.None;
		}
		foreach (BuildingComplete buildingComplete in array)
		{
			if (prefabId == buildingComplete.Def.PrefabID)
			{
				return buildingComplete.GetCell();
			}
		}
		return Option.None;
	}

	// Token: 0x06001FED RID: 8173 RVA: 0x000AE404 File Offset: 0x000AC604
	public static void RevealAndFocusAt(int cellIndex)
	{
		int num;
		int num2;
		Grid.CellToXY(cellIndex, out num, out num2);
		GridVisibility.Reveal(num + 2, num2 + 2, 10, 10f);
		DevToolUtil.FocusCameraOnCell(cellIndex);
		Option<int> cellIndexForUniqueBuilding = DevToolUtil.GetCellIndexForUniqueBuilding("Headquarters");
		if (cellIndexForUniqueBuilding.IsSome())
		{
			Vector3 vector = Grid.CellToPos2D(cellIndex);
			Vector3 vector2 = Grid.CellToPos2D(cellIndexForUniqueBuilding.Unwrap());
			float num3 = 2f / Vector3.Distance(vector, vector2);
			for (float num4 = 0f; num4 < 1f; num4 += num3)
			{
				int num5;
				int num6;
				Grid.PosToXY(Vector3.Lerp(vector, vector2, num4), out num5, out num6);
				GridVisibility.Reveal(num5 + 2, num6 + 2, 4, 4f);
			}
		}
	}
}
