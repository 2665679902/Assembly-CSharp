using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004A7 RID: 1191
[AddComponentMenu("KMonoBehaviour/scripts/NavigationReservations")]
public class NavigationReservations : KMonoBehaviour
{
	// Token: 0x06001AF5 RID: 6901 RVA: 0x0009023A File Offset: 0x0008E43A
	public static void DestroyInstance()
	{
		NavigationReservations.Instance = null;
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x00090242 File Offset: 0x0008E442
	public int GetOccupancyCount(int cell)
	{
		if (this.cellOccupancyDensity.ContainsKey(cell))
		{
			return this.cellOccupancyDensity[cell];
		}
		return 0;
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x00090260 File Offset: 0x0008E460
	public void AddOccupancy(int cell)
	{
		if (!this.cellOccupancyDensity.ContainsKey(cell))
		{
			this.cellOccupancyDensity.Add(cell, 1);
			return;
		}
		Dictionary<int, int> dictionary = this.cellOccupancyDensity;
		dictionary[cell]++;
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x000902A4 File Offset: 0x0008E4A4
	public void RemoveOccupancy(int cell)
	{
		int num = 0;
		if (this.cellOccupancyDensity.TryGetValue(cell, out num))
		{
			if (num == 1)
			{
				this.cellOccupancyDensity.Remove(cell);
				return;
			}
			this.cellOccupancyDensity[cell] = num - 1;
		}
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x000902E4 File Offset: 0x0008E4E4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		NavigationReservations.Instance = this;
	}

	// Token: 0x04000EF7 RID: 3831
	public static NavigationReservations Instance;

	// Token: 0x04000EF8 RID: 3832
	public static int InvalidReservation = -1;

	// Token: 0x04000EF9 RID: 3833
	private Dictionary<int, int> cellOccupancyDensity = new Dictionary<int, int>();
}
