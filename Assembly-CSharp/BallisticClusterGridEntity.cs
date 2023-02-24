using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class BallisticClusterGridEntity : ClusterGridEntity
{
	// Token: 0x17000031 RID: 49
	// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0004161F File Offset: 0x0003F81F
	public override string Name
	{
		get
		{
			return Strings.Get(this.nameKey);
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00041631 File Offset: 0x0003F831
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.Payload;
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00041634 File Offset: 0x0003F834
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim(this.clusterAnimName),
					initialAnim = "idle_loop",
					symbolSwapTarget = this.clusterAnimSymbolSwapTarget,
					symbolSwapSymbol = this.clusterAnimSymbolSwapSymbol
				}
			};
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00041692 File Offset: 0x0003F892
	public override bool IsVisible
	{
		get
		{
			return !base.gameObject.HasTag(GameTags.ClusterEntityGrounded);
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000B76 RID: 2934 RVA: 0x000416A7 File Offset: 0x0003F8A7
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Visible;
		}
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x000416AA File Offset: 0x0003F8AA
	public override bool SpaceOutInSameHex()
	{
		return true;
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x000416B0 File Offset: 0x0003F8B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.m_clusterTraveler.getSpeedCB = new Func<float>(this.GetSpeed);
		this.m_clusterTraveler.getCanTravelCB = new Func<bool, bool>(this.CanTravel);
		this.m_clusterTraveler.onTravelCB = null;
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x000416FD File Offset: 0x0003F8FD
	private float GetSpeed()
	{
		return 10f;
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00041704 File Offset: 0x0003F904
	private bool CanTravel(bool tryingToLand)
	{
		return this.HasTag(GameTags.EntityInSpace);
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00041711 File Offset: 0x0003F911
	public void Configure(AxialI source, AxialI destination)
	{
		this.m_location = source;
		this.m_destionationSelector.SetDestination(destination);
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00041726 File Offset: 0x0003F926
	public override bool ShowPath()
	{
		return this.m_selectable.IsSelected;
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x00041733 File Offset: 0x0003F933
	public override bool ShowProgressBar()
	{
		return this.m_selectable.IsSelected && this.m_clusterTraveler.IsTraveling();
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x0004174F File Offset: 0x0003F94F
	public override float GetProgress()
	{
		return this.m_clusterTraveler.GetMoveProgress();
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x0004175C File Offset: 0x0003F95C
	public void SwapSymbolFromSameAnim(string targetSymbolName, string swappedSymbolName)
	{
		this.clusterAnimSymbolSwapTarget = targetSymbolName;
		this.clusterAnimSymbolSwapSymbol = swappedSymbolName;
	}

	// Token: 0x040006D3 RID: 1747
	[MyCmpReq]
	private ClusterDestinationSelector m_destionationSelector;

	// Token: 0x040006D4 RID: 1748
	[MyCmpReq]
	private ClusterTraveler m_clusterTraveler;

	// Token: 0x040006D5 RID: 1749
	[SerializeField]
	public string clusterAnimName;

	// Token: 0x040006D6 RID: 1750
	[SerializeField]
	public StringKey nameKey;

	// Token: 0x040006D7 RID: 1751
	private string clusterAnimSymbolSwapTarget;

	// Token: 0x040006D8 RID: 1752
	private string clusterAnimSymbolSwapSymbol;
}
