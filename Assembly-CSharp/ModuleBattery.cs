using System;

// Token: 0x02000613 RID: 1555
public class ModuleBattery : Battery
{
	// Token: 0x0600289A RID: 10394 RVA: 0x000D76D0 File Offset: 0x000D58D0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.connectedTags = new Tag[0];
		base.IsVirtual = true;
	}

	// Token: 0x0600289B RID: 10395 RVA: 0x000D76EC File Offset: 0x000D58EC
	protected override void OnSpawn()
	{
		CraftModuleInterface craftInterface = base.GetComponent<RocketModuleCluster>().CraftInterface;
		base.VirtualCircuitKey = craftInterface;
		base.OnSpawn();
		this.meter.gameObject.GetComponent<KBatchedAnimTracker>().matchParentOffset = true;
	}
}
