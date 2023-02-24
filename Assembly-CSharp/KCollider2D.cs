using System;
using UnityEngine;

// Token: 0x0200048F RID: 1167
public abstract class KCollider2D : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0008B8D0 File Offset: 0x00089AD0
	// (set) Token: 0x06001A19 RID: 6681 RVA: 0x0008B8D8 File Offset: 0x00089AD8
	public Vector2 offset
	{
		get
		{
			return this._offset;
		}
		set
		{
			this._offset = value;
			this.MarkDirty(false);
		}
	}

	// Token: 0x06001A1A RID: 6682 RVA: 0x0008B8E8 File Offset: 0x00089AE8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.autoRegisterSimRender = false;
	}

	// Token: 0x06001A1B RID: 6683 RVA: 0x0008B8F7 File Offset: 0x00089AF7
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Singleton<CellChangeMonitor>.Instance.RegisterMovementStateChanged(base.transform, new Action<Transform, bool>(KCollider2D.OnMovementStateChanged));
		this.MarkDirty(true);
	}

	// Token: 0x06001A1C RID: 6684 RVA: 0x0008B922 File Offset: 0x00089B22
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Singleton<CellChangeMonitor>.Instance.UnregisterMovementStateChanged(base.transform, new Action<Transform, bool>(KCollider2D.OnMovementStateChanged));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x06001A1D RID: 6685 RVA: 0x0008B958 File Offset: 0x00089B58
	public void MarkDirty(bool force = false)
	{
		bool flag = force || this.partitionerEntry.IsValid();
		if (!flag)
		{
			return;
		}
		Extents extents = this.GetExtents();
		if (!force && this.cachedExtents.x == extents.x && this.cachedExtents.y == extents.y && this.cachedExtents.width == extents.width && this.cachedExtents.height == extents.height)
		{
			return;
		}
		this.cachedExtents = extents;
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		if (flag)
		{
			this.partitionerEntry = GameScenePartitioner.Instance.Add(base.name, this, this.cachedExtents, GameScenePartitioner.Instance.collisionLayer, null);
		}
	}

	// Token: 0x06001A1E RID: 6686 RVA: 0x0008BA14 File Offset: 0x00089C14
	private void OnMovementStateChanged(bool is_moving)
	{
		if (is_moving)
		{
			this.MarkDirty(false);
			SimAndRenderScheduler.instance.Add(this, false);
			return;
		}
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06001A1F RID: 6687 RVA: 0x0008BA38 File Offset: 0x00089C38
	private static void OnMovementStateChanged(Transform transform, bool is_moving)
	{
		transform.GetComponent<KCollider2D>().OnMovementStateChanged(is_moving);
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x0008BA46 File Offset: 0x00089C46
	public void RenderEveryTick(float dt)
	{
		this.MarkDirty(false);
	}

	// Token: 0x06001A21 RID: 6689
	public abstract bool Intersects(Vector2 pos);

	// Token: 0x06001A22 RID: 6690
	public abstract Extents GetExtents();

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06001A23 RID: 6691
	public abstract Bounds bounds { get; }

	// Token: 0x04000E94 RID: 3732
	[SerializeField]
	public Vector2 _offset;

	// Token: 0x04000E95 RID: 3733
	private Extents cachedExtents;

	// Token: 0x04000E96 RID: 3734
	private HandleVector<int>.Handle partitionerEntry;
}
