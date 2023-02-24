using System;
using UnityEngine;

// Token: 0x0200061D RID: 1565
public class OrnamentReceptacle : SingleEntityReceptacle
{
	// Token: 0x06002908 RID: 10504 RVA: 0x000D8FA8 File Offset: 0x000D71A8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002909 RID: 10505 RVA: 0x000D8FB0 File Offset: 0x000D71B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("snapTo_ornament", false);
	}

	// Token: 0x0600290A RID: 10506 RVA: 0x000D8FD0 File Offset: 0x000D71D0
	protected override void PositionOccupyingObject()
	{
		KBatchedAnimController component = base.occupyingObject.GetComponent<KBatchedAnimController>();
		component.transform.SetLocalPosition(new Vector3(0f, 0f, -0.1f));
		this.occupyingTracker = base.occupyingObject.AddComponent<KBatchedAnimTracker>();
		this.occupyingTracker.symbol = new HashedString("snapTo_ornament");
		this.occupyingTracker.forceAlwaysVisible = true;
		this.animLink = new KAnimLink(base.GetComponent<KBatchedAnimController>(), component);
	}

	// Token: 0x0600290B RID: 10507 RVA: 0x000D9050 File Offset: 0x000D7250
	protected override void ClearOccupant()
	{
		if (this.occupyingTracker != null)
		{
			UnityEngine.Object.Destroy(this.occupyingTracker);
			this.occupyingTracker = null;
		}
		if (this.animLink != null)
		{
			this.animLink.Unregister();
			this.animLink = null;
		}
		base.ClearOccupant();
	}

	// Token: 0x04001820 RID: 6176
	[MyCmpReq]
	private SnapOn snapOn;

	// Token: 0x04001821 RID: 6177
	private KBatchedAnimTracker occupyingTracker;

	// Token: 0x04001822 RID: 6178
	private KAnimLink animLink;
}
