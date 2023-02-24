using System;
using UnityEngine;

// Token: 0x0200056E RID: 1390
public class BuildingUnderConstruction : Building
{
	// Token: 0x06002199 RID: 8601 RVA: 0x000B6E80 File Offset: 0x000B5080
	protected override void OnPrefabInit()
	{
		Vector3 position = base.transform.GetPosition();
		position.z = Grid.GetLayerZ(this.Def.SceneLayer);
		base.transform.SetPosition(position);
		base.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Construction"));
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		Rotatable component2 = base.GetComponent<Rotatable>();
		if (component != null && component2 == null)
		{
			component.Offset = this.Def.GetVisualizerOffset();
		}
		KBoxCollider2D component3 = base.GetComponent<KBoxCollider2D>();
		if (component3 != null)
		{
			Vector3 visualizerOffset = this.Def.GetVisualizerOffset();
			component3.offset += new Vector2(visualizerOffset.x, visualizerOffset.y);
		}
		base.OnPrefabInit();
	}

	// Token: 0x0600219A RID: 8602 RVA: 0x000B6F4C File Offset: 0x000B514C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.Def.IsTilePiece)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			this.Def.RunOnArea(num, base.Orientation, delegate(int c)
			{
				TileVisualizer.RefreshCell(c, this.Def.TileLayer, this.Def.ReplacementLayer);
			});
		}
		base.RegisterBlockTileRenderer();
	}

	// Token: 0x0600219B RID: 8603 RVA: 0x000B6FA1 File Offset: 0x000B51A1
	protected override void OnCleanUp()
	{
		base.UnregisterBlockTileRenderer();
		base.OnCleanUp();
	}

	// Token: 0x0400134C RID: 4940
	[MyCmpAdd]
	private KSelectable selectable;

	// Token: 0x0400134D RID: 4941
	[MyCmpAdd]
	private SaveLoadRoot saveLoadRoot;

	// Token: 0x0400134E RID: 4942
	[MyCmpAdd]
	private KPrefabID kPrefabID;
}
