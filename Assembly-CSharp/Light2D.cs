using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020008C0 RID: 2240
[AddComponentMenu("KMonoBehaviour/scripts/Light2D")]
public class Light2D : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06004064 RID: 16484 RVA: 0x001680EC File Offset: 0x001662EC
	private T MaybeDirty<T>(T old_value, T new_value, ref bool dirty)
	{
		if (!EqualityComparer<T>.Default.Equals(old_value, new_value))
		{
			dirty = true;
			return new_value;
		}
		return old_value;
	}

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x06004065 RID: 16485 RVA: 0x00168102 File Offset: 0x00166302
	// (set) Token: 0x06004066 RID: 16486 RVA: 0x0016810F File Offset: 0x0016630F
	public global::LightShape shape
	{
		get
		{
			return this.pending_emitter_state.shape;
		}
		set
		{
			this.pending_emitter_state.shape = this.MaybeDirty<global::LightShape>(this.pending_emitter_state.shape, value, ref this.dirty_shape);
		}
	}

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x06004067 RID: 16487 RVA: 0x00168134 File Offset: 0x00166334
	// (set) Token: 0x06004068 RID: 16488 RVA: 0x0016813C File Offset: 0x0016633C
	public LightGridManager.LightGridEmitter emitter { get; private set; }

	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x06004069 RID: 16489 RVA: 0x00168145 File Offset: 0x00166345
	// (set) Token: 0x0600406A RID: 16490 RVA: 0x00168152 File Offset: 0x00166352
	public Color Color
	{
		get
		{
			return this.pending_emitter_state.colour;
		}
		set
		{
			this.pending_emitter_state.colour = value;
		}
	}

	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x0600406B RID: 16491 RVA: 0x00168160 File Offset: 0x00166360
	// (set) Token: 0x0600406C RID: 16492 RVA: 0x0016816D File Offset: 0x0016636D
	public int Lux
	{
		get
		{
			return this.pending_emitter_state.intensity;
		}
		set
		{
			this.pending_emitter_state.intensity = value;
		}
	}

	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x0600406D RID: 16493 RVA: 0x0016817B File Offset: 0x0016637B
	// (set) Token: 0x0600406E RID: 16494 RVA: 0x00168188 File Offset: 0x00166388
	public float Range
	{
		get
		{
			return this.pending_emitter_state.radius;
		}
		set
		{
			this.pending_emitter_state.radius = this.MaybeDirty<float>(this.pending_emitter_state.radius, value, ref this.dirty_shape);
		}
	}

	// Token: 0x17000483 RID: 1155
	// (get) Token: 0x0600406F RID: 16495 RVA: 0x001681AD File Offset: 0x001663AD
	// (set) Token: 0x06004070 RID: 16496 RVA: 0x001681BA File Offset: 0x001663BA
	private int origin
	{
		get
		{
			return this.pending_emitter_state.origin;
		}
		set
		{
			this.pending_emitter_state.origin = this.MaybeDirty<int>(this.pending_emitter_state.origin, value, ref this.dirty_position);
		}
	}

	// Token: 0x17000484 RID: 1156
	// (get) Token: 0x06004071 RID: 16497 RVA: 0x001681DF File Offset: 0x001663DF
	// (set) Token: 0x06004072 RID: 16498 RVA: 0x001681E7 File Offset: 0x001663E7
	public float IntensityAnimation { get; set; }

	// Token: 0x17000485 RID: 1157
	// (get) Token: 0x06004073 RID: 16499 RVA: 0x001681F0 File Offset: 0x001663F0
	// (set) Token: 0x06004074 RID: 16500 RVA: 0x001681F8 File Offset: 0x001663F8
	public Vector2 Offset
	{
		get
		{
			return this._offset;
		}
		set
		{
			if (this._offset != value)
			{
				this._offset = value;
				this.origin = Grid.PosToCell(base.transform.GetPosition() + this._offset);
			}
		}
	}

	// Token: 0x17000486 RID: 1158
	// (get) Token: 0x06004075 RID: 16501 RVA: 0x00168235 File Offset: 0x00166435
	private bool isRegistered
	{
		get
		{
			return this.solidPartitionerEntry != HandleVector<int>.InvalidHandle;
		}
	}

	// Token: 0x06004076 RID: 16502 RVA: 0x00168248 File Offset: 0x00166448
	public Light2D()
	{
		this.emitter = new LightGridManager.LightGridEmitter();
		this.Range = 5f;
		this.Lux = 1000;
	}

	// Token: 0x06004077 RID: 16503 RVA: 0x0016829D File Offset: 0x0016649D
	protected override void OnPrefabInit()
	{
		base.Subscribe<Light2D>(-592767678, Light2D.OnOperationalChangedDelegate);
		this.IntensityAnimation = 1f;
	}

	// Token: 0x06004078 RID: 16504 RVA: 0x001682BC File Offset: 0x001664BC
	protected override void OnCmpEnable()
	{
		this.materialPropertyBlock = new MaterialPropertyBlock();
		base.OnCmpEnable();
		Components.Light2Ds.Add(this);
		if (base.isSpawned)
		{
			this.AddToScenePartitioner();
			this.emitter.Refresh(this.pending_emitter_state, true);
		}
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnMoved), "Light2D.OnMoved");
	}

	// Token: 0x06004079 RID: 16505 RVA: 0x00168328 File Offset: 0x00166528
	protected override void OnCmpDisable()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnMoved));
		Components.Light2Ds.Remove(this);
		base.OnCmpDisable();
		this.FullRemove();
	}

	// Token: 0x0600407A RID: 16506 RVA: 0x00168360 File Offset: 0x00166560
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.origin = Grid.PosToCell(base.transform.GetPosition() + this.Offset);
		if (base.isActiveAndEnabled)
		{
			this.AddToScenePartitioner();
			this.emitter.Refresh(this.pending_emitter_state, true);
		}
	}

	// Token: 0x0600407B RID: 16507 RVA: 0x001683BA File Offset: 0x001665BA
	protected override void OnCleanUp()
	{
		this.FullRemove();
	}

	// Token: 0x0600407C RID: 16508 RVA: 0x001683C2 File Offset: 0x001665C2
	private void OnMoved()
	{
		if (base.isSpawned)
		{
			this.FullRefresh();
		}
	}

	// Token: 0x0600407D RID: 16509 RVA: 0x001683D2 File Offset: 0x001665D2
	private HandleVector<int>.Handle AddToLayer(Extents ext, ScenePartitionerLayer layer)
	{
		return GameScenePartitioner.Instance.Add("Light2D", base.gameObject, ext, layer, new Action<object>(this.OnWorldChanged));
	}

	// Token: 0x0600407E RID: 16510 RVA: 0x001683F8 File Offset: 0x001665F8
	private Extents ComputeExtents()
	{
		Vector2I vector2I = Grid.CellToXY(this.origin);
		int num = (int)this.Range;
		Vector2I vector2I2 = new Vector2I(vector2I.x - num, vector2I.y - num);
		int num2 = 2 * num;
		int num3 = ((this.shape == global::LightShape.Circle) ? (2 * num) : num);
		return new Extents(vector2I2.x, vector2I2.y, num2, num3);
	}

	// Token: 0x0600407F RID: 16511 RVA: 0x00168458 File Offset: 0x00166658
	private void AddToScenePartitioner()
	{
		Extents extents = this.ComputeExtents();
		this.solidPartitionerEntry = this.AddToLayer(extents, GameScenePartitioner.Instance.solidChangedLayer);
		this.liquidPartitionerEntry = this.AddToLayer(extents, GameScenePartitioner.Instance.liquidChangedLayer);
	}

	// Token: 0x06004080 RID: 16512 RVA: 0x0016849A File Offset: 0x0016669A
	private void RemoveFromScenePartitioner()
	{
		if (this.isRegistered)
		{
			GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
			GameScenePartitioner.Instance.Free(ref this.liquidPartitionerEntry);
		}
	}

	// Token: 0x06004081 RID: 16513 RVA: 0x001684C4 File Offset: 0x001666C4
	private void MoveInScenePartitioner()
	{
		GameScenePartitioner.Instance.UpdatePosition(this.solidPartitionerEntry, this.ComputeExtents());
		GameScenePartitioner.Instance.UpdatePosition(this.liquidPartitionerEntry, this.ComputeExtents());
	}

	// Token: 0x06004082 RID: 16514 RVA: 0x001684F2 File Offset: 0x001666F2
	[ContextMenu("Refresh")]
	public void FullRefresh()
	{
		if (!base.isSpawned || !base.isActiveAndEnabled)
		{
			return;
		}
		DebugUtil.DevAssert(this.isRegistered, "shouldn't be refreshing if we aren't spawned and enabled", null);
		this.RefreshShapeAndPosition();
		this.emitter.Refresh(this.pending_emitter_state, true);
	}

	// Token: 0x06004083 RID: 16515 RVA: 0x00168530 File Offset: 0x00166730
	public void FullRemove()
	{
		this.RemoveFromScenePartitioner();
		this.emitter.RemoveFromGrid();
	}

	// Token: 0x06004084 RID: 16516 RVA: 0x00168544 File Offset: 0x00166744
	public Light2D.RefreshResult RefreshShapeAndPosition()
	{
		if (!base.isSpawned)
		{
			return Light2D.RefreshResult.None;
		}
		if (!base.isActiveAndEnabled)
		{
			this.FullRemove();
			return Light2D.RefreshResult.Removed;
		}
		int num = Grid.PosToCell(base.transform.GetPosition() + this.Offset);
		if (!Grid.IsValidCell(num))
		{
			this.FullRemove();
			return Light2D.RefreshResult.Removed;
		}
		this.origin = num;
		if (this.dirty_shape)
		{
			this.RemoveFromScenePartitioner();
			this.AddToScenePartitioner();
		}
		else if (this.dirty_position)
		{
			this.MoveInScenePartitioner();
		}
		this.dirty_shape = false;
		this.dirty_position = false;
		return Light2D.RefreshResult.Updated;
	}

	// Token: 0x06004085 RID: 16517 RVA: 0x001685D6 File Offset: 0x001667D6
	private void OnWorldChanged(object data)
	{
		this.FullRefresh();
	}

	// Token: 0x06004086 RID: 16518 RVA: 0x001685E0 File Offset: 0x001667E0
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.EMITS_LIGHT, this.Range), UI.GAMEOBJECTEFFECTS.TOOLTIPS.EMITS_LIGHT, Descriptor.DescriptorType.Effect, false),
			new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.EMITS_LIGHT_LUX, this.Lux), UI.GAMEOBJECTEFFECTS.TOOLTIPS.EMITS_LIGHT_LUX, Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x04002A32 RID: 10802
	private bool dirty_shape;

	// Token: 0x04002A33 RID: 10803
	private bool dirty_position;

	// Token: 0x04002A34 RID: 10804
	[SerializeField]
	private LightGridManager.LightGridEmitter.State pending_emitter_state = LightGridManager.LightGridEmitter.State.DEFAULT;

	// Token: 0x04002A37 RID: 10807
	public float Angle;

	// Token: 0x04002A38 RID: 10808
	public Vector2 Direction;

	// Token: 0x04002A39 RID: 10809
	[SerializeField]
	private Vector2 _offset;

	// Token: 0x04002A3A RID: 10810
	public bool drawOverlay;

	// Token: 0x04002A3B RID: 10811
	public Color overlayColour;

	// Token: 0x04002A3C RID: 10812
	public MaterialPropertyBlock materialPropertyBlock;

	// Token: 0x04002A3D RID: 10813
	private HandleVector<int>.Handle solidPartitionerEntry = HandleVector<int>.InvalidHandle;

	// Token: 0x04002A3E RID: 10814
	private HandleVector<int>.Handle liquidPartitionerEntry = HandleVector<int>.InvalidHandle;

	// Token: 0x04002A3F RID: 10815
	private static readonly EventSystem.IntraObjectHandler<Light2D> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<Light2D>(delegate(Light2D light, object data)
	{
		light.enabled = (bool)data;
	});

	// Token: 0x0200168E RID: 5774
	public enum RefreshResult
	{
		// Token: 0x04006A2A RID: 27178
		None,
		// Token: 0x04006A2B RID: 27179
		Removed,
		// Token: 0x04006A2C RID: 27180
		Updated
	}
}
