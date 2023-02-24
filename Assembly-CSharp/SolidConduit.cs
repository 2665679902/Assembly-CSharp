using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000641 RID: 1601
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/SolidConduit")]
public class SolidConduit : KMonoBehaviour, IFirstFrameCallback, IHaveUtilityNetworkMgr
{
	// Token: 0x06002A7B RID: 10875 RVA: 0x000E062C File Offset: 0x000DE82C
	public void SetFirstFrameCallback(System.Action ffCb)
	{
		this.firstFrameCallback = ffCb;
		base.StartCoroutine(this.RunCallback());
	}

	// Token: 0x06002A7C RID: 10876 RVA: 0x000E0642 File Offset: 0x000DE842
	private IEnumerator RunCallback()
	{
		yield return null;
		if (this.firstFrameCallback != null)
		{
			this.firstFrameCallback();
			this.firstFrameCallback = null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x06002A7D RID: 10877 RVA: 0x000E0651 File Offset: 0x000DE851
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.solidConduitSystem;
	}

	// Token: 0x06002A7E RID: 10878 RVA: 0x000E065D File Offset: 0x000DE85D
	public UtilityNetwork GetNetwork()
	{
		return this.GetNetworkManager().GetNetworkForCell(Grid.PosToCell(this));
	}

	// Token: 0x06002A7F RID: 10879 RVA: 0x000E0670 File Offset: 0x000DE870
	public static SolidConduitFlow GetFlowManager()
	{
		return Game.Instance.solidConduitFlow;
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06002A80 RID: 10880 RVA: 0x000E067C File Offset: 0x000DE87C
	public Vector3 Position
	{
		get
		{
			return base.transform.GetPosition();
		}
	}

	// Token: 0x06002A81 RID: 10881 RVA: 0x000E0689 File Offset: 0x000DE889
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Conveyor, this);
	}

	// Token: 0x06002A82 RID: 10882 RVA: 0x000E06BC File Offset: 0x000DE8BC
	protected override void OnCleanUp()
	{
		int num = Grid.PosToCell(this);
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component.Def.ReplacementLayer == ObjectLayer.NumLayers || Grid.Objects[num, (int)component.Def.ReplacementLayer] == null)
		{
			this.GetNetworkManager().RemoveFromNetworks(num, this, false);
			SolidConduit.GetFlowManager().EmptyConduit(num);
		}
		base.OnCleanUp();
	}

	// Token: 0x0400191D RID: 6429
	[MyCmpReq]
	private KAnimGraphTileVisualizer graphTileDependency;

	// Token: 0x0400191E RID: 6430
	private System.Action firstFrameCallback;
}
