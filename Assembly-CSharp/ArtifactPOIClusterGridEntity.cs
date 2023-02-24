using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000934 RID: 2356
[SerializationConfig(MemberSerialization.OptIn)]
public class ArtifactPOIClusterGridEntity : ClusterGridEntity
{
	// Token: 0x170004EA RID: 1258
	// (get) Token: 0x060044FC RID: 17660 RVA: 0x00185399 File Offset: 0x00183599
	public override string Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x170004EB RID: 1259
	// (get) Token: 0x060044FD RID: 17661 RVA: 0x001853A1 File Offset: 0x001835A1
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.POI;
		}
	}

	// Token: 0x170004EC RID: 1260
	// (get) Token: 0x060044FE RID: 17662 RVA: 0x001853A4 File Offset: 0x001835A4
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("gravitas_space_poi_kanim"),
					initialAnim = (this.m_Anim.IsNullOrWhiteSpace() ? "station_1" : this.m_Anim)
				}
			};
		}
	}

	// Token: 0x170004ED RID: 1261
	// (get) Token: 0x060044FF RID: 17663 RVA: 0x001853FC File Offset: 0x001835FC
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170004EE RID: 1262
	// (get) Token: 0x06004500 RID: 17664 RVA: 0x001853FF File Offset: 0x001835FF
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Peeked;
		}
	}

	// Token: 0x06004501 RID: 17665 RVA: 0x00185402 File Offset: 0x00183602
	public void Init(AxialI location)
	{
		base.Location = location;
	}

	// Token: 0x04002E09 RID: 11785
	public string m_name;

	// Token: 0x04002E0A RID: 11786
	public string m_Anim;
}
