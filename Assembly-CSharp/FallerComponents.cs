﻿using System;
using UnityEngine;

// Token: 0x02000771 RID: 1905
public class FallerComponents : KGameObjectComponentManager<FallerComponent>
{
	// Token: 0x0600342C RID: 13356 RVA: 0x0011894E File Offset: 0x00116B4E
	public HandleVector<int>.Handle Add(GameObject go, Vector2 initial_velocity)
	{
		return base.Add(go, new FallerComponent(go.transform, initial_velocity));
	}

	// Token: 0x0600342D RID: 13357 RVA: 0x00118964 File Offset: 0x00116B64
	public override void Remove(GameObject go)
	{
		HandleVector<int>.Handle handle = base.GetHandle(go);
		this.OnCleanUpImmediate(handle);
		KComponentManager<FallerComponent>.CleanupInfo cleanupInfo = new KComponentManager<FallerComponent>.CleanupInfo(go, handle);
		if (!KComponentCleanUp.InCleanUpPhase)
		{
			base.AddToCleanupList(cleanupInfo);
			return;
		}
		base.InternalRemoveComponent(cleanupInfo);
	}

	// Token: 0x0600342E RID: 13358 RVA: 0x001189A0 File Offset: 0x00116BA0
	protected override void OnPrefabInit(HandleVector<int>.Handle h)
	{
		FallerComponent data = base.GetData(h);
		Vector3 position = data.transform.GetPosition();
		int num = Grid.PosToCell(position);
		data.cellChangedCB = delegate
		{
			FallerComponents.OnSolidChanged(h);
		};
		float groundOffset = GravityComponent.GetGroundOffset(data.transform.GetComponent<KCollider2D>());
		int num2 = Grid.PosToCell(new Vector3(position.x, position.y - groundOffset - 0.07f, position.z));
		bool flag = Grid.IsValidCell(num2) && Grid.Solid[num2] && data.initialVelocity.sqrMagnitude == 0f;
		if ((Grid.IsValidCell(num) && Grid.Solid[num]) || flag)
		{
			data.solidChangedCB = delegate(object ev_data)
			{
				FallerComponents.OnSolidChanged(h);
			};
			int num3 = 2;
			Vector2I vector2I = Grid.CellToXY(num);
			vector2I.y--;
			if (vector2I.y < 0)
			{
				vector2I.y = 0;
				num3 = 1;
			}
			else if (vector2I.y == Grid.HeightInCells - 1)
			{
				num3 = 1;
			}
			data.partitionerEntry = GameScenePartitioner.Instance.Add("Faller", data.transform.gameObject, vector2I.x, vector2I.y, 1, num3, GameScenePartitioner.Instance.solidChangedLayer, data.solidChangedCB);
			GameComps.Fallers.SetData(h, data);
			return;
		}
		GameComps.Fallers.SetData(h, data);
		FallerComponents.AddGravity(data.transform, data.initialVelocity);
	}

	// Token: 0x0600342F RID: 13359 RVA: 0x00118B40 File Offset: 0x00116D40
	protected override void OnSpawn(HandleVector<int>.Handle h)
	{
		base.OnSpawn(h);
		FallerComponent data = base.GetData(h);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(data.transform, data.cellChangedCB, "FallerComponent.OnSpawn");
	}

	// Token: 0x06003430 RID: 13360 RVA: 0x00118B78 File Offset: 0x00116D78
	private void OnCleanUpImmediate(HandleVector<int>.Handle h)
	{
		FallerComponent data = base.GetData(h);
		GameScenePartitioner.Instance.Free(ref data.partitionerEntry);
		if (data.cellChangedCB != null)
		{
			Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(data.transformInstanceId, data.cellChangedCB);
			data.cellChangedCB = null;
		}
		if (GameComps.Gravities.Has(data.transform.gameObject))
		{
			GameComps.Gravities.Remove(data.transform.gameObject);
		}
		base.SetData(h, data);
	}

	// Token: 0x06003431 RID: 13361 RVA: 0x00118BF8 File Offset: 0x00116DF8
	private static void AddGravity(Transform transform, Vector2 initial_velocity)
	{
		if (!GameComps.Gravities.Has(transform.gameObject))
		{
			GameComps.Gravities.Add(transform.gameObject, initial_velocity, delegate
			{
				FallerComponents.OnLanded(transform);
			});
			HandleVector<int>.Handle handle = GameComps.Fallers.GetHandle(transform.gameObject);
			FallerComponent data = GameComps.Fallers.GetData(handle);
			if (data.partitionerEntry.IsValid())
			{
				GameScenePartitioner.Instance.Free(ref data.partitionerEntry);
				GameComps.Fallers.SetData(handle, data);
			}
		}
	}

	// Token: 0x06003432 RID: 13362 RVA: 0x00118C9C File Offset: 0x00116E9C
	private static void RemoveGravity(Transform transform)
	{
		if (GameComps.Gravities.Has(transform.gameObject))
		{
			GameComps.Gravities.Remove(transform.gameObject);
			HandleVector<int>.Handle h = GameComps.Fallers.GetHandle(transform.gameObject);
			FallerComponent data = GameComps.Fallers.GetData(h);
			int num = Grid.CellBelow(Grid.PosToCell(transform.GetPosition()));
			GameScenePartitioner.Instance.Free(ref data.partitionerEntry);
			if (Grid.IsValidCell(num))
			{
				data.solidChangedCB = delegate(object ev_data)
				{
					FallerComponents.OnSolidChanged(h);
				};
				data.partitionerEntry = GameScenePartitioner.Instance.Add("Faller", transform.gameObject, num, GameScenePartitioner.Instance.solidChangedLayer, data.solidChangedCB);
			}
			GameComps.Fallers.SetData(h, data);
		}
	}

	// Token: 0x06003433 RID: 13363 RVA: 0x00118D76 File Offset: 0x00116F76
	private static void OnLanded(Transform transform)
	{
		FallerComponents.RemoveGravity(transform);
	}

	// Token: 0x06003434 RID: 13364 RVA: 0x00118D80 File Offset: 0x00116F80
	private static void OnSolidChanged(HandleVector<int>.Handle handle)
	{
		FallerComponent data = GameComps.Fallers.GetData(handle);
		if (data.transform == null)
		{
			return;
		}
		Vector3 position = data.transform.GetPosition();
		position.y = position.y - data.offset - 0.1f;
		int num = Grid.PosToCell(position);
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		bool flag = !Grid.Solid[num];
		if (flag != data.isFalling)
		{
			data.isFalling = flag;
			if (flag)
			{
				FallerComponents.AddGravity(data.transform, Vector2.zero);
				return;
			}
			FallerComponents.RemoveGravity(data.transform);
		}
	}

	// Token: 0x04002049 RID: 8265
	private const float EPSILON = 0.07f;
}
