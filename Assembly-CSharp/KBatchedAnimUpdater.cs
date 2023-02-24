using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041D RID: 1053
public class KBatchedAnimUpdater : Singleton<KBatchedAnimUpdater>
{
	// Token: 0x06001697 RID: 5783 RVA: 0x00074AD0 File Offset: 0x00072CD0
	public void InitializeGrid()
	{
		this.Clear();
		Vector2I visibleSize = this.GetVisibleSize();
		int num = (visibleSize.x + 32 - 1) / 32;
		int num2 = (visibleSize.y + 32 - 1) / 32;
		this.controllerGrid = new Dictionary<int, KBatchedAnimController>[num, num2];
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				this.controllerGrid[j, i] = new Dictionary<int, KBatchedAnimController>();
			}
		}
		this.visibleChunks.Clear();
		this.previouslyVisibleChunks.Clear();
		this.previouslyVisibleChunkGrid = new bool[num, num2];
		this.visibleChunkGrid = new bool[num, num2];
		this.controllerChunkInfos.Clear();
		this.movingControllerInfos.Clear();
	}

	// Token: 0x06001698 RID: 5784 RVA: 0x00074B84 File Offset: 0x00072D84
	public Vector2I GetVisibleSize()
	{
		if (CameraController.Instance != null)
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			CameraController.Instance.GetWorldCamera(out vector2I, out vector2I2);
			return new Vector2I((int)((float)(vector2I2.x + vector2I.x) * KBatchedAnimUpdater.VISIBLE_RANGE_SCALE.x), (int)((float)(vector2I2.y + vector2I.y) * KBatchedAnimUpdater.VISIBLE_RANGE_SCALE.y));
		}
		return new Vector2I((int)((float)Grid.WidthInCells * KBatchedAnimUpdater.VISIBLE_RANGE_SCALE.x), (int)((float)Grid.HeightInCells * KBatchedAnimUpdater.VISIBLE_RANGE_SCALE.y));
	}

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06001699 RID: 5785 RVA: 0x00074C10 File Offset: 0x00072E10
	// (remove) Token: 0x0600169A RID: 5786 RVA: 0x00074C48 File Offset: 0x00072E48
	public event System.Action OnClear;

	// Token: 0x0600169B RID: 5787 RVA: 0x00074C80 File Offset: 0x00072E80
	public void Clear()
	{
		foreach (KBatchedAnimController kbatchedAnimController in this.updateList)
		{
			if (kbatchedAnimController != null)
			{
				UnityEngine.Object.DestroyImmediate(kbatchedAnimController);
			}
		}
		this.updateList.Clear();
		foreach (KBatchedAnimController kbatchedAnimController2 in this.alwaysUpdateList)
		{
			if (kbatchedAnimController2 != null)
			{
				UnityEngine.Object.DestroyImmediate(kbatchedAnimController2);
			}
		}
		this.alwaysUpdateList.Clear();
		this.queuedRegistrations.Clear();
		this.visibleChunks.Clear();
		this.previouslyVisibleChunks.Clear();
		this.controllerGrid = null;
		this.previouslyVisibleChunkGrid = null;
		this.visibleChunkGrid = null;
		System.Action onClear = this.OnClear;
		if (onClear == null)
		{
			return;
		}
		onClear();
	}

	// Token: 0x0600169C RID: 5788 RVA: 0x00074D84 File Offset: 0x00072F84
	public void UpdateRegister(KBatchedAnimController controller)
	{
		switch (controller.updateRegistrationState)
		{
		case KBatchedAnimUpdater.RegistrationState.Registered:
			break;
		case KBatchedAnimUpdater.RegistrationState.PendingRemoval:
			controller.updateRegistrationState = KBatchedAnimUpdater.RegistrationState.Registered;
			return;
		case KBatchedAnimUpdater.RegistrationState.Unregistered:
			((controller.visibilityType == KAnimControllerBase.VisibilityType.Always) ? this.alwaysUpdateList : this.updateList).AddLast(controller);
			controller.updateRegistrationState = KBatchedAnimUpdater.RegistrationState.Registered;
			break;
		default:
			return;
		}
	}

	// Token: 0x0600169D RID: 5789 RVA: 0x00074DD8 File Offset: 0x00072FD8
	public void UpdateUnregister(KBatchedAnimController controller)
	{
		switch (controller.updateRegistrationState)
		{
		case KBatchedAnimUpdater.RegistrationState.Registered:
			controller.updateRegistrationState = KBatchedAnimUpdater.RegistrationState.PendingRemoval;
			break;
		case KBatchedAnimUpdater.RegistrationState.PendingRemoval:
		case KBatchedAnimUpdater.RegistrationState.Unregistered:
			break;
		default:
			return;
		}
	}

	// Token: 0x0600169E RID: 5790 RVA: 0x00074E08 File Offset: 0x00073008
	public void VisibilityRegister(KBatchedAnimController controller)
	{
		this.queuedRegistrations.Add(new KBatchedAnimUpdater.RegistrationInfo
		{
			transformId = controller.transform.GetInstanceID(),
			controllerInstanceId = controller.GetInstanceID(),
			controller = controller,
			register = true
		});
	}

	// Token: 0x0600169F RID: 5791 RVA: 0x00074E58 File Offset: 0x00073058
	public void VisibilityUnregister(KBatchedAnimController controller)
	{
		if (App.IsExiting)
		{
			return;
		}
		this.queuedRegistrations.Add(new KBatchedAnimUpdater.RegistrationInfo
		{
			transformId = controller.transform.GetInstanceID(),
			controllerInstanceId = controller.GetInstanceID(),
			controller = controller,
			register = false
		});
	}

	// Token: 0x060016A0 RID: 5792 RVA: 0x00074EB0 File Offset: 0x000730B0
	private Dictionary<int, KBatchedAnimController> GetControllerMap(Vector2I chunk_xy)
	{
		Dictionary<int, KBatchedAnimController> dictionary = null;
		if (this.controllerGrid != null && 0 <= chunk_xy.x && chunk_xy.x < this.controllerGrid.GetLength(0) && 0 <= chunk_xy.y && chunk_xy.y < this.controllerGrid.GetLength(1))
		{
			dictionary = this.controllerGrid[chunk_xy.x, chunk_xy.y];
		}
		return dictionary;
	}

	// Token: 0x060016A1 RID: 5793 RVA: 0x00074F1C File Offset: 0x0007311C
	public void LateUpdate()
	{
		this.ProcessMovingAnims();
		this.UpdateVisibility();
		this.ProcessRegistrations();
		this.CleanUp();
		float num = Time.unscaledDeltaTime;
		int count = this.alwaysUpdateList.Count;
		KBatchedAnimUpdater.UpdateRegisteredAnims(this.alwaysUpdateList, num);
		if (this.DoGridProcessing())
		{
			num = Time.deltaTime;
			if (num > 0f)
			{
				int count2 = this.updateList.Count;
				KBatchedAnimUpdater.UpdateRegisteredAnims(this.updateList, num);
			}
		}
	}

	// Token: 0x060016A2 RID: 5794 RVA: 0x00074F90 File Offset: 0x00073190
	private static void UpdateRegisteredAnims(LinkedList<KBatchedAnimController> list, float dt)
	{
		LinkedListNode<KBatchedAnimController> next;
		for (LinkedListNode<KBatchedAnimController> linkedListNode = list.First; linkedListNode != null; linkedListNode = next)
		{
			next = linkedListNode.Next;
			KBatchedAnimController value = linkedListNode.Value;
			if (value == null)
			{
				list.Remove(linkedListNode);
			}
			else if (value.updateRegistrationState != KBatchedAnimUpdater.RegistrationState.Registered)
			{
				value.updateRegistrationState = KBatchedAnimUpdater.RegistrationState.Unregistered;
				list.Remove(linkedListNode);
			}
			else if (value.forceUseGameTime)
			{
				value.UpdateAnim(Time.deltaTime);
			}
			else
			{
				value.UpdateAnim(dt);
			}
		}
	}

	// Token: 0x060016A3 RID: 5795 RVA: 0x00074FFD File Offset: 0x000731FD
	public bool IsChunkVisible(Vector2I chunk_xy)
	{
		return this.visibleChunkGrid[chunk_xy.x, chunk_xy.y];
	}

	// Token: 0x060016A4 RID: 5796 RVA: 0x00075016 File Offset: 0x00073216
	public void GetVisibleArea(out Vector2I vis_chunk_min, out Vector2I vis_chunk_max)
	{
		vis_chunk_min = this.vis_chunk_min;
		vis_chunk_max = this.vis_chunk_max;
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x00075030 File Offset: 0x00073230
	public static Vector2I PosToChunkXY(Vector3 pos)
	{
		return KAnimBatchManager.CellXYToChunkXY(Grid.PosToXY(pos));
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x00075040 File Offset: 0x00073240
	private void UpdateVisibility()
	{
		if (!this.DoGridProcessing())
		{
			return;
		}
		Vector2I vector2I;
		Vector2I vector2I2;
		KBatchedAnimUpdater.GetVisibleCellRange(out vector2I, out vector2I2);
		this.vis_chunk_min = new Vector2I(vector2I.x / 32, vector2I.y / 32);
		this.vis_chunk_max = new Vector2I(vector2I2.x / 32, vector2I2.y / 32);
		this.vis_chunk_max.x = Math.Min(this.vis_chunk_max.x, this.controllerGrid.GetLength(0) - 1);
		this.vis_chunk_max.y = Math.Min(this.vis_chunk_max.y, this.controllerGrid.GetLength(1) - 1);
		bool[,] array = this.previouslyVisibleChunkGrid;
		this.previouslyVisibleChunkGrid = this.visibleChunkGrid;
		this.visibleChunkGrid = array;
		Array.Clear(this.visibleChunkGrid, 0, this.visibleChunkGrid.Length);
		List<Vector2I> list = this.previouslyVisibleChunks;
		this.previouslyVisibleChunks = this.visibleChunks;
		this.visibleChunks = list;
		this.visibleChunks.Clear();
		for (int i = this.vis_chunk_min.y; i <= this.vis_chunk_max.y; i++)
		{
			for (int j = this.vis_chunk_min.x; j <= this.vis_chunk_max.x; j++)
			{
				this.visibleChunkGrid[j, i] = true;
				this.visibleChunks.Add(new Vector2I(j, i));
				if (!this.previouslyVisibleChunkGrid[j, i])
				{
					foreach (KeyValuePair<int, KBatchedAnimController> keyValuePair in this.controllerGrid[j, i])
					{
						KBatchedAnimController value = keyValuePair.Value;
						if (!(value == null))
						{
							value.SetVisiblity(true);
						}
					}
				}
			}
		}
		for (int k = 0; k < this.previouslyVisibleChunks.Count; k++)
		{
			Vector2I vector2I3 = this.previouslyVisibleChunks[k];
			if (!this.visibleChunkGrid[vector2I3.x, vector2I3.y])
			{
				foreach (KeyValuePair<int, KBatchedAnimController> keyValuePair2 in this.controllerGrid[vector2I3.x, vector2I3.y])
				{
					KBatchedAnimController value2 = keyValuePair2.Value;
					if (!(value2 == null))
					{
						value2.SetVisiblity(false);
					}
				}
			}
		}
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x000752E4 File Offset: 0x000734E4
	private void ProcessMovingAnims()
	{
		foreach (KBatchedAnimUpdater.MovingControllerInfo movingControllerInfo in this.movingControllerInfos.Values)
		{
			if (!(movingControllerInfo.controller == null))
			{
				Vector2I vector2I = KBatchedAnimUpdater.PosToChunkXY(movingControllerInfo.controller.PositionIncludingOffset);
				if (movingControllerInfo.chunkXY != vector2I)
				{
					KBatchedAnimUpdater.ControllerChunkInfo controllerChunkInfo = default(KBatchedAnimUpdater.ControllerChunkInfo);
					DebugUtil.Assert(this.controllerChunkInfos.TryGetValue(movingControllerInfo.controllerInstanceId, out controllerChunkInfo));
					DebugUtil.Assert(movingControllerInfo.controller == controllerChunkInfo.controller);
					DebugUtil.Assert(controllerChunkInfo.chunkXY == movingControllerInfo.chunkXY);
					Dictionary<int, KBatchedAnimController> dictionary = this.GetControllerMap(controllerChunkInfo.chunkXY);
					if (dictionary != null)
					{
						DebugUtil.Assert(dictionary.ContainsKey(movingControllerInfo.controllerInstanceId));
						dictionary.Remove(movingControllerInfo.controllerInstanceId);
					}
					dictionary = this.GetControllerMap(vector2I);
					if (dictionary != null)
					{
						DebugUtil.Assert(!dictionary.ContainsKey(movingControllerInfo.controllerInstanceId));
						dictionary[movingControllerInfo.controllerInstanceId] = controllerChunkInfo.controller;
					}
					movingControllerInfo.chunkXY = vector2I;
					controllerChunkInfo.chunkXY = vector2I;
					this.controllerChunkInfos[movingControllerInfo.controllerInstanceId] = controllerChunkInfo;
					if (dictionary != null)
					{
						controllerChunkInfo.controller.SetVisiblity(this.visibleChunkGrid[vector2I.x, vector2I.y]);
					}
					else
					{
						controllerChunkInfo.controller.SetVisiblity(false);
					}
				}
			}
		}
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x00075484 File Offset: 0x00073684
	private void ProcessRegistrations()
	{
		for (int i = 0; i < this.queuedRegistrations.Count; i++)
		{
			KBatchedAnimUpdater.RegistrationInfo registrationInfo = this.queuedRegistrations[i];
			if (registrationInfo.register)
			{
				if (!(registrationInfo.controller == null))
				{
					int instanceID = registrationInfo.controller.GetInstanceID();
					DebugUtil.Assert(!this.controllerChunkInfos.ContainsKey(instanceID));
					KBatchedAnimUpdater.ControllerChunkInfo controllerChunkInfo = new KBatchedAnimUpdater.ControllerChunkInfo
					{
						controller = registrationInfo.controller,
						chunkXY = KBatchedAnimUpdater.PosToChunkXY(registrationInfo.controller.PositionIncludingOffset)
					};
					this.controllerChunkInfos[instanceID] = controllerChunkInfo;
					bool flag = false;
					if (Singleton<CellChangeMonitor>.Instance != null)
					{
						flag = Singleton<CellChangeMonitor>.Instance.IsMoving(registrationInfo.controller.transform);
						Singleton<CellChangeMonitor>.Instance.RegisterMovementStateChanged(registrationInfo.controller.transform, new Action<Transform, bool>(this.OnMovementStateChanged));
					}
					Dictionary<int, KBatchedAnimController> controllerMap = this.GetControllerMap(controllerChunkInfo.chunkXY);
					if (controllerMap != null)
					{
						DebugUtil.Assert(!controllerMap.ContainsKey(instanceID));
						controllerMap.Add(instanceID, registrationInfo.controller);
					}
					if (flag)
					{
						DebugUtil.DevAssertArgs(!this.movingControllerInfos.ContainsKey(instanceID), new object[]
						{
							"Readding controller which is already moving",
							registrationInfo.controller.name,
							controllerChunkInfo.chunkXY,
							this.movingControllerInfos.ContainsKey(instanceID) ? this.movingControllerInfos[instanceID].chunkXY.ToString() : null
						});
						this.movingControllerInfos[instanceID] = new KBatchedAnimUpdater.MovingControllerInfo
						{
							controllerInstanceId = instanceID,
							controller = registrationInfo.controller,
							chunkXY = controllerChunkInfo.chunkXY
						};
					}
					if (controllerMap != null && this.visibleChunkGrid[controllerChunkInfo.chunkXY.x, controllerChunkInfo.chunkXY.y])
					{
						registrationInfo.controller.SetVisiblity(true);
					}
				}
			}
			else
			{
				KBatchedAnimUpdater.ControllerChunkInfo controllerChunkInfo2 = default(KBatchedAnimUpdater.ControllerChunkInfo);
				if (this.controllerChunkInfos.TryGetValue(registrationInfo.controllerInstanceId, out controllerChunkInfo2))
				{
					if (registrationInfo.controller != null)
					{
						Dictionary<int, KBatchedAnimController> controllerMap2 = this.GetControllerMap(controllerChunkInfo2.chunkXY);
						if (controllerMap2 != null)
						{
							DebugUtil.Assert(controllerMap2.ContainsKey(registrationInfo.controllerInstanceId));
							controllerMap2.Remove(registrationInfo.controllerInstanceId);
						}
						registrationInfo.controller.SetVisiblity(false);
					}
					this.movingControllerInfos.Remove(registrationInfo.controllerInstanceId);
					Singleton<CellChangeMonitor>.Instance.UnregisterMovementStateChanged(registrationInfo.transformId, new Action<Transform, bool>(this.OnMovementStateChanged));
					this.controllerChunkInfos.Remove(registrationInfo.controllerInstanceId);
				}
			}
		}
		this.queuedRegistrations.Clear();
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x00075740 File Offset: 0x00073940
	public void OnMovementStateChanged(Transform transform, bool is_moving)
	{
		if (transform == null)
		{
			return;
		}
		KBatchedAnimController component = transform.GetComponent<KBatchedAnimController>();
		int instanceID = component.GetInstanceID();
		KBatchedAnimUpdater.ControllerChunkInfo controllerChunkInfo = default(KBatchedAnimUpdater.ControllerChunkInfo);
		DebugUtil.Assert(this.controllerChunkInfos.TryGetValue(instanceID, out controllerChunkInfo));
		if (is_moving)
		{
			DebugUtil.DevAssertArgs(!this.movingControllerInfos.ContainsKey(instanceID), new object[]
			{
				"Readding controller which is already moving",
				component.name,
				controllerChunkInfo.chunkXY,
				this.movingControllerInfos.ContainsKey(instanceID) ? this.movingControllerInfos[instanceID].chunkXY.ToString() : null
			});
			this.movingControllerInfos[instanceID] = new KBatchedAnimUpdater.MovingControllerInfo
			{
				controllerInstanceId = instanceID,
				controller = component,
				chunkXY = controllerChunkInfo.chunkXY
			};
			return;
		}
		this.movingControllerInfos.Remove(instanceID);
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x00075828 File Offset: 0x00073A28
	private void CleanUp()
	{
		if (!this.DoGridProcessing())
		{
			return;
		}
		int length = this.controllerGrid.GetLength(0);
		for (int i = 0; i < 16; i++)
		{
			int num = (this.cleanUpChunkIndex + i) % this.controllerGrid.Length;
			int num2 = num % length;
			int num3 = num / length;
			Dictionary<int, KBatchedAnimController> dictionary = this.controllerGrid[num2, num3];
			ListPool<int, KBatchedAnimUpdater>.PooledList pooledList = ListPool<int, KBatchedAnimUpdater>.Allocate();
			foreach (KeyValuePair<int, KBatchedAnimController> keyValuePair in dictionary)
			{
				if (keyValuePair.Value == null)
				{
					pooledList.Add(keyValuePair.Key);
				}
			}
			foreach (int num4 in pooledList)
			{
				dictionary.Remove(num4);
			}
			pooledList.Recycle();
		}
		this.cleanUpChunkIndex = (this.cleanUpChunkIndex + 16) % this.controllerGrid.Length;
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x00075950 File Offset: 0x00073B50
	public static void GetVisibleCellRange(out Vector2I min, out Vector2I max)
	{
		Grid.GetVisibleExtents(out min.x, out min.y, out max.x, out max.y);
		min.x -= 4;
		min.y -= 4;
		if (CameraController.Instance != null && DlcManager.IsExpansion1Active())
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			CameraController.Instance.GetWorldCamera(out vector2I, out vector2I2);
			min.x = Math.Min(vector2I.x + vector2I2.x - 1, Math.Max(vector2I.x, min.x));
			min.y = Math.Min(vector2I.y + vector2I2.y - 1, Math.Max(vector2I.y, min.y));
			max.x += 4;
			max.y += 4;
			max.x = Math.Min(vector2I.x + vector2I2.x - 1, Math.Max(vector2I.x, max.x));
			max.y = Math.Min(vector2I.y + vector2I2.y - 1, Math.Max(vector2I.y, max.y));
			return;
		}
		min.x = Math.Min((int)((float)Grid.WidthInCells * KBatchedAnimUpdater.VISIBLE_RANGE_SCALE.x) - 1, Math.Max(0, min.x));
		min.y = Math.Min((int)((float)Grid.HeightInCells * KBatchedAnimUpdater.VISIBLE_RANGE_SCALE.y) - 1, Math.Max(0, min.y));
		max.x += 4;
		max.y += 4;
		max.x = Math.Min((int)((float)Grid.WidthInCells * KBatchedAnimUpdater.VISIBLE_RANGE_SCALE.x) - 1, Math.Max(0, max.x));
		max.y = Math.Min((int)((float)Grid.HeightInCells * KBatchedAnimUpdater.VISIBLE_RANGE_SCALE.y) - 1, Math.Max(0, max.y));
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x00075B3D File Offset: 0x00073D3D
	private bool DoGridProcessing()
	{
		return this.controllerGrid != null && Camera.main != null;
	}

	// Token: 0x04000C8C RID: 3212
	private const int VISIBLE_BORDER = 4;

	// Token: 0x04000C8D RID: 3213
	public static readonly Vector2I INVALID_CHUNK_ID = Vector2I.minusone;

	// Token: 0x04000C8E RID: 3214
	private Dictionary<int, KBatchedAnimController>[,] controllerGrid;

	// Token: 0x04000C8F RID: 3215
	private LinkedList<KBatchedAnimController> updateList = new LinkedList<KBatchedAnimController>();

	// Token: 0x04000C90 RID: 3216
	private LinkedList<KBatchedAnimController> alwaysUpdateList = new LinkedList<KBatchedAnimController>();

	// Token: 0x04000C91 RID: 3217
	private bool[,] visibleChunkGrid;

	// Token: 0x04000C92 RID: 3218
	private bool[,] previouslyVisibleChunkGrid;

	// Token: 0x04000C93 RID: 3219
	private List<Vector2I> visibleChunks = new List<Vector2I>();

	// Token: 0x04000C94 RID: 3220
	private List<Vector2I> previouslyVisibleChunks = new List<Vector2I>();

	// Token: 0x04000C95 RID: 3221
	private Vector2I vis_chunk_min = Vector2I.zero;

	// Token: 0x04000C96 RID: 3222
	private Vector2I vis_chunk_max = Vector2I.zero;

	// Token: 0x04000C97 RID: 3223
	private List<KBatchedAnimUpdater.RegistrationInfo> queuedRegistrations = new List<KBatchedAnimUpdater.RegistrationInfo>();

	// Token: 0x04000C98 RID: 3224
	private Dictionary<int, KBatchedAnimUpdater.ControllerChunkInfo> controllerChunkInfos = new Dictionary<int, KBatchedAnimUpdater.ControllerChunkInfo>();

	// Token: 0x04000C99 RID: 3225
	private Dictionary<int, KBatchedAnimUpdater.MovingControllerInfo> movingControllerInfos = new Dictionary<int, KBatchedAnimUpdater.MovingControllerInfo>();

	// Token: 0x04000C9A RID: 3226
	private const int CHUNKS_TO_CLEAN_PER_TICK = 16;

	// Token: 0x04000C9B RID: 3227
	private int cleanUpChunkIndex;

	// Token: 0x04000C9C RID: 3228
	private static readonly Vector2 VISIBLE_RANGE_SCALE = new Vector2(1.5f, 1.5f);

	// Token: 0x02001044 RID: 4164
	public enum RegistrationState
	{
		// Token: 0x040056D5 RID: 22229
		Registered,
		// Token: 0x040056D6 RID: 22230
		PendingRemoval,
		// Token: 0x040056D7 RID: 22231
		Unregistered
	}

	// Token: 0x02001045 RID: 4165
	private struct RegistrationInfo
	{
		// Token: 0x040056D8 RID: 22232
		public bool register;

		// Token: 0x040056D9 RID: 22233
		public int transformId;

		// Token: 0x040056DA RID: 22234
		public int controllerInstanceId;

		// Token: 0x040056DB RID: 22235
		public KBatchedAnimController controller;
	}

	// Token: 0x02001046 RID: 4166
	private struct ControllerChunkInfo
	{
		// Token: 0x040056DC RID: 22236
		public KBatchedAnimController controller;

		// Token: 0x040056DD RID: 22237
		public Vector2I chunkXY;
	}

	// Token: 0x02001047 RID: 4167
	private class MovingControllerInfo
	{
		// Token: 0x040056DE RID: 22238
		public int controllerInstanceId;

		// Token: 0x040056DF RID: 22239
		public KBatchedAnimController controller;

		// Token: 0x040056E0 RID: 22240
		public Vector2I chunkXY;
	}
}
