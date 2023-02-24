using System;
using UnityEngine;

// Token: 0x020009AA RID: 2474
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/TileTemperature")]
public class TileTemperature : KMonoBehaviour
{
	// Token: 0x0600495F RID: 18783 RVA: 0x0019B1A4 File Offset: 0x001993A4
	protected override void OnPrefabInit()
	{
		this.primaryElement.getTemperatureCallback = new PrimaryElement.GetTemperatureCallback(TileTemperature.OnGetTemperature);
		this.primaryElement.setTemperatureCallback = new PrimaryElement.SetTemperatureCallback(TileTemperature.OnSetTemperature);
		base.OnPrefabInit();
	}

	// Token: 0x06004960 RID: 18784 RVA: 0x0019B1DA File Offset: 0x001993DA
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06004961 RID: 18785 RVA: 0x0019B1E4 File Offset: 0x001993E4
	private static float OnGetTemperature(PrimaryElement primary_element)
	{
		SimCellOccupier component = primary_element.GetComponent<SimCellOccupier>();
		if (component != null && component.IsReady())
		{
			int num = Grid.PosToCell(primary_element.transform.GetPosition());
			return Grid.Temperature[num];
		}
		return primary_element.InternalTemperature;
	}

	// Token: 0x06004962 RID: 18786 RVA: 0x0019B22C File Offset: 0x0019942C
	private static void OnSetTemperature(PrimaryElement primary_element, float temperature)
	{
		SimCellOccupier component = primary_element.GetComponent<SimCellOccupier>();
		if (component != null && component.IsReady())
		{
			global::Debug.LogWarning("Only set a tile's temperature during initialization. Otherwise you should be modifying the cell via the sim!");
			return;
		}
		primary_element.InternalTemperature = temperature;
	}

	// Token: 0x04003039 RID: 12345
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x0400303A RID: 12346
	[MyCmpReq]
	private KSelectable selectable;
}
