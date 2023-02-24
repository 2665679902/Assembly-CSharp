using System;
using Rendering;
using UnityEngine;

// Token: 0x0200048C RID: 1164
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/KAnimGridTileVisualizer")]
public class KAnimGridTileVisualizer : KMonoBehaviour, IBlockTileInfo
{
	// Token: 0x06001A03 RID: 6659 RVA: 0x0008B2E3 File Offset: 0x000894E3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<KAnimGridTileVisualizer>(-1503271301, KAnimGridTileVisualizer.OnSelectionChangedDelegate);
		base.Subscribe<KAnimGridTileVisualizer>(-1201923725, KAnimGridTileVisualizer.OnHighlightChangedDelegate);
	}

	// Token: 0x06001A04 RID: 6660 RVA: 0x0008B310 File Offset: 0x00089510
	protected override void OnCleanUp()
	{
		Building component = base.GetComponent<Building>();
		if (component != null)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			ObjectLayer tileLayer = component.Def.TileLayer;
			if (Grid.Objects[num, (int)tileLayer] == base.gameObject)
			{
				Grid.Objects[num, (int)tileLayer] = null;
			}
			TileVisualizer.RefreshCell(num, tileLayer, component.Def.ReplacementLayer);
		}
		base.OnCleanUp();
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x0008B388 File Offset: 0x00089588
	private void OnSelectionChanged(object data)
	{
		bool flag = (bool)data;
		World.Instance.blockTileRenderer.SelectCell(Grid.PosToCell(base.transform.GetPosition()), flag);
	}

	// Token: 0x06001A06 RID: 6662 RVA: 0x0008B3BC File Offset: 0x000895BC
	private void OnHighlightChanged(object data)
	{
		bool flag = (bool)data;
		World.Instance.blockTileRenderer.HighlightCell(Grid.PosToCell(base.transform.GetPosition()), flag);
	}

	// Token: 0x06001A07 RID: 6663 RVA: 0x0008B3F0 File Offset: 0x000895F0
	public int GetBlockTileConnectorID()
	{
		return this.blockTileConnectorID;
	}

	// Token: 0x04000E8F RID: 3727
	[SerializeField]
	public int blockTileConnectorID;

	// Token: 0x04000E90 RID: 3728
	private static readonly EventSystem.IntraObjectHandler<KAnimGridTileVisualizer> OnSelectionChangedDelegate = new EventSystem.IntraObjectHandler<KAnimGridTileVisualizer>(delegate(KAnimGridTileVisualizer component, object data)
	{
		component.OnSelectionChanged(data);
	});

	// Token: 0x04000E91 RID: 3729
	private static readonly EventSystem.IntraObjectHandler<KAnimGridTileVisualizer> OnHighlightChangedDelegate = new EventSystem.IntraObjectHandler<KAnimGridTileVisualizer>(delegate(KAnimGridTileVisualizer component, object data)
	{
		component.OnHighlightChanged(data);
	});
}
