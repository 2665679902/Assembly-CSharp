using System;
using Klei.Actions;
using UnityEngine;

namespace Klei.Input
{
	// Token: 0x02000DAD RID: 3501
	[ActionType("InterfaceTool", "Dig", true)]
	public abstract class DigAction
	{
		// Token: 0x06006A80 RID: 27264 RVA: 0x002953B0 File Offset: 0x002935B0
		public void Uproot(int cell)
		{
			ListPool<ScenePartitionerEntry, GameScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, GameScenePartitioner>.Allocate();
			int num;
			int num2;
			Grid.CellToXY(cell, out num, out num2);
			GameScenePartitioner.Instance.GatherEntries(num, num2, 1, 1, GameScenePartitioner.Instance.plants, pooledList);
			if (pooledList.Count > 0)
			{
				this.Uproot((pooledList[0].obj as Component).GetComponent<Uprootable>());
			}
			pooledList.Recycle();
		}

		// Token: 0x06006A81 RID: 27265
		public abstract void Dig(int cell, int distFromOrigin);

		// Token: 0x06006A82 RID: 27266
		protected abstract void Uproot(Uprootable uprootable);
	}
}
