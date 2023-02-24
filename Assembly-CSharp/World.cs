using System;
using System.Collections.Generic;
using Klei;
using Rendering;
using UnityEngine;

// Token: 0x020009D3 RID: 2515
[AddComponentMenu("KMonoBehaviour/scripts/World")]
public class World : KMonoBehaviour
{
	// Token: 0x17000586 RID: 1414
	// (get) Token: 0x06004ACE RID: 19150 RVA: 0x001A265C File Offset: 0x001A085C
	// (set) Token: 0x06004ACF RID: 19151 RVA: 0x001A2663 File Offset: 0x001A0863
	public static World Instance { get; private set; }

	// Token: 0x17000587 RID: 1415
	// (get) Token: 0x06004AD0 RID: 19152 RVA: 0x001A266B File Offset: 0x001A086B
	// (set) Token: 0x06004AD1 RID: 19153 RVA: 0x001A2673 File Offset: 0x001A0873
	public SubworldZoneRenderData zoneRenderData { get; private set; }

	// Token: 0x06004AD2 RID: 19154 RVA: 0x001A267C File Offset: 0x001A087C
	protected override void OnPrefabInit()
	{
		global::Debug.Assert(World.Instance == null);
		World.Instance = this;
		this.blockTileRenderer = base.GetComponent<BlockTileRenderer>();
	}

	// Token: 0x06004AD3 RID: 19155 RVA: 0x001A26A0 File Offset: 0x001A08A0
	protected override void OnSpawn()
	{
		base.GetComponent<SimDebugView>().OnReset();
		base.GetComponent<PropertyTextures>().OnReset(null);
		this.zoneRenderData = base.GetComponent<SubworldZoneRenderData>();
		Grid.OnReveal = (Action<int>)Delegate.Combine(Grid.OnReveal, new Action<int>(this.OnReveal));
	}

	// Token: 0x06004AD4 RID: 19156 RVA: 0x001A26F0 File Offset: 0x001A08F0
	protected override void OnLoadLevel()
	{
		World.Instance = null;
		if (this.blockTileRenderer != null)
		{
			this.blockTileRenderer.FreeResources();
		}
		this.blockTileRenderer = null;
		if (this.groundRenderer != null)
		{
			this.groundRenderer.FreeResources();
		}
		this.groundRenderer = null;
		this.revealedCells.Clear();
		this.revealedCells = null;
		base.OnLoadLevel();
	}

	// Token: 0x06004AD5 RID: 19157 RVA: 0x001A275C File Offset: 0x001A095C
	public unsafe void UpdateCellInfo(List<SolidInfo> solidInfo, List<CallbackInfo> callbackInfo, int num_solid_substance_change_info, Sim.SolidSubstanceChangeInfo* solid_substance_change_info, int num_liquid_change_info, Sim.LiquidChangeInfo* liquid_change_info)
	{
		int count = solidInfo.Count;
		this.changedCells.Clear();
		for (int i = 0; i < count; i++)
		{
			int cellIdx = solidInfo[i].cellIdx;
			if (!this.changedCells.Contains(cellIdx))
			{
				this.changedCells.Add(cellIdx);
			}
			Pathfinding.Instance.AddDirtyNavGridCell(cellIdx);
			WorldDamage.Instance.OnSolidStateChanged(cellIdx);
			if (this.OnSolidChanged != null)
			{
				this.OnSolidChanged(cellIdx);
			}
		}
		if (this.changedCells.Count != 0)
		{
			SaveGame.Instance.entombedItemManager.OnSolidChanged(this.changedCells);
			GameScenePartitioner.Instance.TriggerEvent(this.changedCells, GameScenePartitioner.Instance.solidChangedLayer, null);
		}
		int count2 = callbackInfo.Count;
		for (int j = 0; j < count2; j++)
		{
			callbackInfo[j].Release();
		}
		for (int k = 0; k < num_solid_substance_change_info; k++)
		{
			int cellIdx2 = solid_substance_change_info[k].cellIdx;
			if (!Grid.IsValidCell(cellIdx2))
			{
				global::Debug.LogError(cellIdx2);
			}
			else
			{
				Grid.RenderedByWorld[cellIdx2] = Grid.Element[cellIdx2].substance.renderedByWorld && Grid.Objects[cellIdx2, 9] == null;
				this.groundRenderer.MarkDirty(cellIdx2);
			}
		}
		GameScenePartitioner instance = GameScenePartitioner.Instance;
		this.changedCells.Clear();
		for (int l = 0; l < num_liquid_change_info; l++)
		{
			int cellIdx3 = liquid_change_info[l].cellIdx;
			this.changedCells.Add(cellIdx3);
			if (this.OnLiquidChanged != null)
			{
				this.OnLiquidChanged(cellIdx3);
			}
		}
		instance.TriggerEvent(this.changedCells, GameScenePartitioner.Instance.liquidChangedLayer, null);
	}

	// Token: 0x06004AD6 RID: 19158 RVA: 0x001A2931 File Offset: 0x001A0B31
	private void OnReveal(int cell)
	{
		this.revealedCells.Add(cell);
	}

	// Token: 0x06004AD7 RID: 19159 RVA: 0x001A2940 File Offset: 0x001A0B40
	private void LateUpdate()
	{
		if (Game.IsQuitting())
		{
			return;
		}
		if (GameUtil.IsCapturingTimeLapse())
		{
			this.groundRenderer.RenderAll();
			KAnimBatchManager.Instance().UpdateActiveArea(new Vector2I(0, 0), new Vector2I(9999, 9999));
			KAnimBatchManager.Instance().UpdateDirty(Time.frameCount);
			KAnimBatchManager.Instance().Render();
		}
		else
		{
			GridArea visibleArea = GridVisibleArea.GetVisibleArea();
			this.groundRenderer.Render(visibleArea.Min, visibleArea.Max, false);
			Vector2I vector2I;
			Vector2I vector2I2;
			Singleton<KBatchedAnimUpdater>.Instance.GetVisibleArea(out vector2I, out vector2I2);
			KAnimBatchManager.Instance().UpdateActiveArea(vector2I, vector2I2);
			KAnimBatchManager.Instance().UpdateDirty(Time.frameCount);
			KAnimBatchManager.Instance().Render();
		}
		if (Camera.main != null)
		{
			Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector3(KInputManager.GetMousePos().x, KInputManager.GetMousePos().y, -Camera.main.transform.GetPosition().z));
			Shader.SetGlobalVector("_CursorPos", new Vector4(vector.x, vector.y, vector.z, 0f));
		}
		FallingWater.instance.UpdateParticles(Time.deltaTime);
		FallingWater.instance.Render();
		if (this.revealedCells.Count > 0)
		{
			GameScenePartitioner.Instance.TriggerEvent(this.revealedCells, GameScenePartitioner.Instance.fogOfWarChangedLayer, null);
			this.revealedCells.Clear();
		}
	}

	// Token: 0x04003117 RID: 12567
	public Action<int> OnSolidChanged;

	// Token: 0x04003118 RID: 12568
	public Action<int> OnLiquidChanged;

	// Token: 0x0400311A RID: 12570
	public BlockTileRenderer blockTileRenderer;

	// Token: 0x0400311B RID: 12571
	[MyCmpGet]
	[NonSerialized]
	public GroundRenderer groundRenderer;

	// Token: 0x0400311C RID: 12572
	private List<int> revealedCells = new List<int>();

	// Token: 0x0400311D RID: 12573
	public static int DebugCellID = -1;

	// Token: 0x0400311E RID: 12574
	private List<int> changedCells = new List<int>();
}
