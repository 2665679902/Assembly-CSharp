using System;

// Token: 0x020004B4 RID: 1204
public class Painting : Artable
{
	// Token: 0x06001B81 RID: 7041 RVA: 0x00092010 File Offset: 0x00090210
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.multitoolContext = "paint";
		this.multitoolHitEffectTag = "fx_paint_splash";
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x00092043 File Offset: 0x00090243
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Paintings.Add(this);
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x00092056 File Offset: 0x00090256
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.Paintings.Remove(this);
	}
}
