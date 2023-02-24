using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000779 RID: 1913
[AddComponentMenu("KMonoBehaviour/scripts/FishOvercrowingManager")]
public class FishOvercrowingManager : KMonoBehaviour, ISim1000ms
{
	// Token: 0x060034AA RID: 13482 RVA: 0x0011C00B File Offset: 0x0011A20B
	public static void DestroyInstance()
	{
		FishOvercrowingManager.Instance = null;
	}

	// Token: 0x060034AB RID: 13483 RVA: 0x0011C013 File Offset: 0x0011A213
	protected override void OnPrefabInit()
	{
		FishOvercrowingManager.Instance = this;
		this.cells = new FishOvercrowingManager.Cell[Grid.CellCount];
	}

	// Token: 0x060034AC RID: 13484 RVA: 0x0011C02B File Offset: 0x0011A22B
	public void Add(FishOvercrowdingMonitor.Instance fish)
	{
		this.fishes.Add(fish);
	}

	// Token: 0x060034AD RID: 13485 RVA: 0x0011C039 File Offset: 0x0011A239
	public void Remove(FishOvercrowdingMonitor.Instance fish)
	{
		this.fishes.Remove(fish);
	}

	// Token: 0x060034AE RID: 13486 RVA: 0x0011C048 File Offset: 0x0011A248
	public void Sim1000ms(float dt)
	{
		int num = this.versionCounter;
		this.versionCounter = num + 1;
		int num2 = num;
		int num3 = 1;
		this.cavityIdToCavityInfo.Clear();
		this.cellToFishCount.Clear();
		ListPool<FishOvercrowingManager.FishInfo, FishOvercrowingManager>.PooledList pooledList = ListPool<FishOvercrowingManager.FishInfo, FishOvercrowingManager>.Allocate();
		foreach (FishOvercrowdingMonitor.Instance instance in this.fishes)
		{
			int num4 = Grid.PosToCell(instance);
			if (Grid.IsValidCell(num4))
			{
				FishOvercrowingManager.FishInfo fishInfo = new FishOvercrowingManager.FishInfo
				{
					cell = num4,
					fish = instance
				};
				pooledList.Add(fishInfo);
				int num5 = 0;
				this.cellToFishCount.TryGetValue(num4, out num5);
				num5++;
				this.cellToFishCount[num4] = num5;
			}
		}
		foreach (FishOvercrowingManager.FishInfo fishInfo2 in pooledList)
		{
			ListPool<int, FishOvercrowingManager>.PooledList pooledList2 = ListPool<int, FishOvercrowingManager>.Allocate();
			pooledList2.Add(fishInfo2.cell);
			int i = 0;
			int num6 = num3++;
			while (i < pooledList2.Count)
			{
				int num7 = pooledList2[i++];
				if (Grid.IsValidCell(num7))
				{
					FishOvercrowingManager.Cell cell = this.cells[num7];
					if (cell.version != num2 && Grid.IsLiquid(num7))
					{
						cell.cavityId = num6;
						cell.version = num2;
						int num8 = 0;
						this.cellToFishCount.TryGetValue(num7, out num8);
						FishOvercrowingManager.CavityInfo cavityInfo = default(FishOvercrowingManager.CavityInfo);
						if (!this.cavityIdToCavityInfo.TryGetValue(num6, out cavityInfo))
						{
							cavityInfo = default(FishOvercrowingManager.CavityInfo);
						}
						cavityInfo.fishCount += num8;
						cavityInfo.cellCount++;
						this.cavityIdToCavityInfo[num6] = cavityInfo;
						pooledList2.Add(Grid.CellLeft(num7));
						pooledList2.Add(Grid.CellRight(num7));
						pooledList2.Add(Grid.CellAbove(num7));
						pooledList2.Add(Grid.CellBelow(num7));
						this.cells[num7] = cell;
					}
				}
			}
			pooledList2.Recycle();
		}
		foreach (FishOvercrowingManager.FishInfo fishInfo3 in pooledList)
		{
			FishOvercrowingManager.Cell cell2 = this.cells[fishInfo3.cell];
			FishOvercrowingManager.CavityInfo cavityInfo2 = default(FishOvercrowingManager.CavityInfo);
			this.cavityIdToCavityInfo.TryGetValue(cell2.cavityId, out cavityInfo2);
			fishInfo3.fish.SetOvercrowdingInfo(cavityInfo2.cellCount, cavityInfo2.fishCount);
		}
		pooledList.Recycle();
	}

	// Token: 0x04002098 RID: 8344
	public static FishOvercrowingManager Instance;

	// Token: 0x04002099 RID: 8345
	private List<FishOvercrowdingMonitor.Instance> fishes = new List<FishOvercrowdingMonitor.Instance>();

	// Token: 0x0400209A RID: 8346
	private Dictionary<int, FishOvercrowingManager.CavityInfo> cavityIdToCavityInfo = new Dictionary<int, FishOvercrowingManager.CavityInfo>();

	// Token: 0x0400209B RID: 8347
	private Dictionary<int, int> cellToFishCount = new Dictionary<int, int>();

	// Token: 0x0400209C RID: 8348
	private FishOvercrowingManager.Cell[] cells;

	// Token: 0x0400209D RID: 8349
	private int versionCounter = 1;

	// Token: 0x0200146F RID: 5231
	private struct Cell
	{
		// Token: 0x04006363 RID: 25443
		public int version;

		// Token: 0x04006364 RID: 25444
		public int cavityId;
	}

	// Token: 0x02001470 RID: 5232
	private struct FishInfo
	{
		// Token: 0x04006365 RID: 25445
		public int cell;

		// Token: 0x04006366 RID: 25446
		public FishOvercrowdingMonitor.Instance fish;
	}

	// Token: 0x02001471 RID: 5233
	private struct CavityInfo
	{
		// Token: 0x04006367 RID: 25447
		public int fishCount;

		// Token: 0x04006368 RID: 25448
		public int cellCount;
	}
}
