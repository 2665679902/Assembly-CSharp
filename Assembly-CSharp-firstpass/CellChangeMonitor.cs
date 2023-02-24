using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000087 RID: 135
public class CellChangeMonitor : Singleton<CellChangeMonitor>
{
	// Token: 0x06000544 RID: 1348 RVA: 0x00019C80 File Offset: 0x00017E80
	public void MarkDirty(Transform transform)
	{
		if (this.gridWidth == 0)
		{
			return;
		}
		this.pendingDirtyTransforms.Add(transform.GetInstanceID());
		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			this.MarkDirty(transform.GetChild(i));
		}
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00019CC8 File Offset: 0x00017EC8
	public bool IsMoving(Transform transform)
	{
		return this.movingTransforms.Contains(transform.GetInstanceID());
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00019CDC File Offset: 0x00017EDC
	public void RegisterMovementStateChanged(Transform transform, Action<Transform, bool> handler)
	{
		int instanceID = transform.GetInstanceID();
		CellChangeMonitor.MovementStateChangedEntry movementStateChangedEntry = default(CellChangeMonitor.MovementStateChangedEntry);
		if (!this.movementStateChangedHandlers.TryGetValue(instanceID, out movementStateChangedEntry))
		{
			movementStateChangedEntry = default(CellChangeMonitor.MovementStateChangedEntry);
			movementStateChangedEntry.handlers = new List<Action<Transform, bool>>();
			movementStateChangedEntry.transform = transform;
		}
		movementStateChangedEntry.handlers.Add(handler);
		this.movementStateChangedHandlers[instanceID] = movementStateChangedEntry;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00019D40 File Offset: 0x00017F40
	public void UnregisterMovementStateChanged(int instance_id, Action<Transform, bool> callback)
	{
		CellChangeMonitor.MovementStateChangedEntry movementStateChangedEntry = default(CellChangeMonitor.MovementStateChangedEntry);
		if (this.movementStateChangedHandlers.TryGetValue(instance_id, out movementStateChangedEntry))
		{
			movementStateChangedEntry.handlers.Remove(callback);
			if (movementStateChangedEntry.handlers.Count == 0)
			{
				this.movementStateChangedHandlers.Remove(instance_id);
			}
		}
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00019D8C File Offset: 0x00017F8C
	public void UnregisterMovementStateChanged(Transform transform, Action<Transform, bool> callback)
	{
		this.UnregisterMovementStateChanged(transform.GetInstanceID(), callback);
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00019D9C File Offset: 0x00017F9C
	public int RegisterCellChangedHandler(Transform transform, System.Action callback, string debug_name)
	{
		int instanceID = transform.GetInstanceID();
		CellChangeMonitor.CellChangedEntry cellChangedEntry = default(CellChangeMonitor.CellChangedEntry);
		if (!this.cellChangedHandlers.TryGetValue(instanceID, out cellChangedEntry))
		{
			cellChangedEntry = default(CellChangeMonitor.CellChangedEntry);
			cellChangedEntry.transform = transform;
			cellChangedEntry.handlers = new List<CellChangeMonitor.CellChangedEntry.Handler>();
		}
		CellChangeMonitor.CellChangedEntry.Handler handler = new CellChangeMonitor.CellChangedEntry.Handler
		{
			name = debug_name,
			callback = callback
		};
		cellChangedEntry.handlers.Add(handler);
		this.cellChangedHandlers[instanceID] = cellChangedEntry;
		return instanceID;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00019E18 File Offset: 0x00018018
	public void UnregisterCellChangedHandler(int instance_id, System.Action callback)
	{
		CellChangeMonitor.CellChangedEntry cellChangedEntry = default(CellChangeMonitor.CellChangedEntry);
		if (this.cellChangedHandlers.TryGetValue(instance_id, out cellChangedEntry))
		{
			for (int i = 0; i < cellChangedEntry.handlers.Count; i++)
			{
				if (!(cellChangedEntry.handlers[i].callback != callback))
				{
					cellChangedEntry.handlers.RemoveAt(i);
					break;
				}
			}
			if (cellChangedEntry.handlers.Count == 0)
			{
				this.cellChangedHandlers.Remove(instance_id);
			}
		}
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00019E94 File Offset: 0x00018094
	public void UnregisterCellChangedHandler(Transform transform, System.Action callback)
	{
		this.UnregisterCellChangedHandler(transform.GetInstanceID(), callback);
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00019EA4 File Offset: 0x000180A4
	public void ClearLastKnownCell(Transform transform)
	{
		int instanceID = transform.GetInstanceID();
		this.transformLastKnownCell.Remove(instanceID);
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00019EC8 File Offset: 0x000180C8
	public int PosToCell(Vector3 pos)
	{
		float x = pos.x;
		int num = (int)(pos.y + 0.05f);
		int num2 = (int)x;
		return num * this.gridWidth + num2;
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00019EF5 File Offset: 0x000180F5
	public void SetGridSize(int grid_width, int grid_height)
	{
		this.gridWidth = grid_width;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x00019F00 File Offset: 0x00018100
	public void RenderEveryTick()
	{
		HashSet<int> hashSet = this.pendingDirtyTransforms;
		this.pendingDirtyTransforms = this.dirtyTransforms;
		this.dirtyTransforms = hashSet;
		this.pendingDirtyTransforms.Clear();
		this.previouslyMovingTransforms.Clear();
		hashSet = this.previouslyMovingTransforms;
		this.previouslyMovingTransforms = this.movingTransforms;
		this.movingTransforms = hashSet;
		foreach (int num in this.dirtyTransforms)
		{
			CellChangeMonitor.CellChangedEntry cellChangedEntry = default(CellChangeMonitor.CellChangedEntry);
			if (this.cellChangedHandlers.TryGetValue(num, out cellChangedEntry))
			{
				if (cellChangedEntry.transform == null)
				{
					continue;
				}
				int num2 = -1;
				this.transformLastKnownCell.TryGetValue(num, out num2);
				int num3 = this.PosToCell(cellChangedEntry.transform.GetPosition());
				if (num2 != num3)
				{
					this.cellChangedCallbacksToRun.Clear();
					this.cellChangedCallbacksToRun.AddRange(cellChangedEntry.handlers);
					foreach (CellChangeMonitor.CellChangedEntry.Handler handler in this.cellChangedCallbacksToRun)
					{
						foreach (CellChangeMonitor.CellChangedEntry.Handler handler2 in cellChangedEntry.handlers)
						{
							if (handler2.callback == handler.callback)
							{
								handler2.callback();
								break;
							}
						}
					}
					this.transformLastKnownCell[num] = num3;
				}
			}
			this.movingTransforms.Add(num);
			if (!this.previouslyMovingTransforms.Contains(num))
			{
				this.RunMovementStateChangedCallbacks(num, true);
			}
		}
		foreach (int num4 in this.previouslyMovingTransforms)
		{
			if (!this.movingTransforms.Contains(num4))
			{
				this.RunMovementStateChangedCallbacks(num4, false);
			}
		}
		this.dirtyTransforms.Clear();
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0001A170 File Offset: 0x00018370
	private void RunMovementStateChangedCallbacks(int instance_id, bool state)
	{
		CellChangeMonitor.MovementStateChangedEntry movementStateChangedEntry = default(CellChangeMonitor.MovementStateChangedEntry);
		if (this.movementStateChangedHandlers.TryGetValue(instance_id, out movementStateChangedEntry))
		{
			this.moveChangedCallbacksToRun.Clear();
			this.moveChangedCallbacksToRun.AddRange(movementStateChangedEntry.handlers);
			foreach (Action<Transform, bool> action in this.moveChangedCallbacksToRun)
			{
				if (movementStateChangedEntry.handlers.Contains(action))
				{
					action(movementStateChangedEntry.transform, state);
				}
			}
		}
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0001A20C File Offset: 0x0001840C
	private void Validate()
	{
	}

	// Token: 0x04000533 RID: 1331
	private Dictionary<int, CellChangeMonitor.CellChangedEntry> cellChangedHandlers = new Dictionary<int, CellChangeMonitor.CellChangedEntry>();

	// Token: 0x04000534 RID: 1332
	private Dictionary<int, CellChangeMonitor.MovementStateChangedEntry> movementStateChangedHandlers = new Dictionary<int, CellChangeMonitor.MovementStateChangedEntry>();

	// Token: 0x04000535 RID: 1333
	private HashSet<int> pendingDirtyTransforms = new HashSet<int>();

	// Token: 0x04000536 RID: 1334
	private HashSet<int> dirtyTransforms = new HashSet<int>();

	// Token: 0x04000537 RID: 1335
	private HashSet<int> movingTransforms = new HashSet<int>();

	// Token: 0x04000538 RID: 1336
	private HashSet<int> previouslyMovingTransforms = new HashSet<int>();

	// Token: 0x04000539 RID: 1337
	private Dictionary<int, int> transformLastKnownCell = new Dictionary<int, int>();

	// Token: 0x0400053A RID: 1338
	private List<CellChangeMonitor.CellChangedEntry.Handler> cellChangedCallbacksToRun = new List<CellChangeMonitor.CellChangedEntry.Handler>();

	// Token: 0x0400053B RID: 1339
	private List<Action<Transform, bool>> moveChangedCallbacksToRun = new List<Action<Transform, bool>>();

	// Token: 0x0400053C RID: 1340
	private int gridWidth;

	// Token: 0x020009CF RID: 2511
	private struct CellChangedEntry
	{
		// Token: 0x04002201 RID: 8705
		public Transform transform;

		// Token: 0x04002202 RID: 8706
		public List<CellChangeMonitor.CellChangedEntry.Handler> handlers;

		// Token: 0x02000B46 RID: 2886
		public struct Handler
		{
			// Token: 0x04002694 RID: 9876
			public string name;

			// Token: 0x04002695 RID: 9877
			public System.Action callback;
		}
	}

	// Token: 0x020009D0 RID: 2512
	private struct MovementStateChangedEntry
	{
		// Token: 0x04002203 RID: 8707
		public Transform transform;

		// Token: 0x04002204 RID: 8708
		public List<Action<Transform, bool>> handlers;
	}
}
