using System;

// Token: 0x020005C5 RID: 1477
public class GeoTunerSwitchGeyserWorkable : Workable
{
	// Token: 0x060024C7 RID: 9415 RVA: 0x000C6FD8 File Offset: 0x000C51D8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_remote_kanim") };
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
	}

	// Token: 0x060024C8 RID: 9416 RVA: 0x000C700C File Offset: 0x000C520C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(3f);
	}

	// Token: 0x04001532 RID: 5426
	private const string animName = "anim_use_remote_kanim";
}
