using System;
using UnityEngine;

// Token: 0x0200054C RID: 1356
public class ArtifactModule : SingleEntityReceptacle, IRenderEveryTick
{
	// Token: 0x06002056 RID: 8278 RVA: 0x000B0C3C File Offset: 0x000AEE3C
	protected override void OnSpawn()
	{
		this.craft = this.module.CraftInterface.GetComponent<Clustercraft>();
		if (this.craft.Status == Clustercraft.CraftStatus.InFlight && base.occupyingObject != null)
		{
			base.occupyingObject.SetActive(false);
		}
		base.OnSpawn();
		base.Subscribe(705820818, new Action<object>(this.OnEnterSpace));
		base.Subscribe(-1165815793, new Action<object>(this.OnExitSpace));
	}

	// Token: 0x06002057 RID: 8279 RVA: 0x000B0CBD File Offset: 0x000AEEBD
	public void RenderEveryTick(float dt)
	{
		this.ArtifactTrackModulePosition();
	}

	// Token: 0x06002058 RID: 8280 RVA: 0x000B0CC8 File Offset: 0x000AEEC8
	private void ArtifactTrackModulePosition()
	{
		this.occupyingObjectRelativePosition = this.animController.Offset + Vector3.up * 0.5f + new Vector3(0f, 0f, -1f);
		if (base.occupyingObject != null)
		{
			this.PositionOccupyingObject();
		}
	}

	// Token: 0x06002059 RID: 8281 RVA: 0x000B0D27 File Offset: 0x000AEF27
	private void OnEnterSpace(object data)
	{
		if (base.occupyingObject != null)
		{
			base.occupyingObject.SetActive(false);
		}
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x000B0D43 File Offset: 0x000AEF43
	private void OnExitSpace(object data)
	{
		if (base.occupyingObject != null)
		{
			base.occupyingObject.SetActive(true);
		}
	}

	// Token: 0x0400129A RID: 4762
	[MyCmpReq]
	private KBatchedAnimController animController;

	// Token: 0x0400129B RID: 4763
	[MyCmpReq]
	private RocketModuleCluster module;

	// Token: 0x0400129C RID: 4764
	private Clustercraft craft;
}
