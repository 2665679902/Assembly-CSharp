using System;
using UnityEngine;

// Token: 0x020009CA RID: 2506
[AddComponentMenu("KMonoBehaviour/scripts/VisibilityTester")]
public class VisibilityTester : KMonoBehaviour
{
	// Token: 0x06004A87 RID: 19079 RVA: 0x001A1887 File Offset: 0x0019FA87
	public static void DestroyInstance()
	{
		VisibilityTester.Instance = null;
	}

	// Token: 0x06004A88 RID: 19080 RVA: 0x001A188F File Offset: 0x0019FA8F
	protected override void OnPrefabInit()
	{
		VisibilityTester.Instance = this;
	}

	// Token: 0x06004A89 RID: 19081 RVA: 0x001A1898 File Offset: 0x0019FA98
	private void Update()
	{
		if (SelectTool.Instance == null || SelectTool.Instance.selected == null || !this.enableTesting)
		{
			return;
		}
		int num = Grid.PosToCell(SelectTool.Instance.selected);
		int mouseCell = DebugHandler.GetMouseCell();
		string text = "";
		text = text + "Source Cell: " + num.ToString() + "\n";
		text = text + "Target Cell: " + mouseCell.ToString() + "\n";
		text = text + "Visible: " + Grid.VisibilityTest(num, mouseCell, false).ToString();
		for (int i = 0; i < 10000; i++)
		{
			Grid.VisibilityTest(num, mouseCell, false);
		}
		DebugText.Instance.Draw(text, Grid.CellToPosCCC(mouseCell, Grid.SceneLayer.Move), Color.white);
	}

	// Token: 0x040030F0 RID: 12528
	public static VisibilityTester Instance;

	// Token: 0x040030F1 RID: 12529
	public bool enableTesting;
}
