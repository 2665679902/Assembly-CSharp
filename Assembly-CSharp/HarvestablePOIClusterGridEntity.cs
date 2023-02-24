using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000946 RID: 2374
[SerializationConfig(MemberSerialization.OptIn)]
public class HarvestablePOIClusterGridEntity : ClusterGridEntity
{
	// Token: 0x17000523 RID: 1315
	// (get) Token: 0x06004626 RID: 17958 RVA: 0x0018B45F File Offset: 0x0018965F
	public override string Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000524 RID: 1316
	// (get) Token: 0x06004627 RID: 17959 RVA: 0x0018B467 File Offset: 0x00189667
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.POI;
		}
	}

	// Token: 0x17000525 RID: 1317
	// (get) Token: 0x06004628 RID: 17960 RVA: 0x0018B46C File Offset: 0x0018966C
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("harvestable_space_poi_kanim"),
					initialAnim = (this.m_Anim.IsNullOrWhiteSpace() ? "cloud" : this.m_Anim)
				}
			};
		}
	}

	// Token: 0x17000526 RID: 1318
	// (get) Token: 0x06004629 RID: 17961 RVA: 0x0018B4C4 File Offset: 0x001896C4
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000527 RID: 1319
	// (get) Token: 0x0600462A RID: 17962 RVA: 0x0018B4C7 File Offset: 0x001896C7
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Peeked;
		}
	}

	// Token: 0x0600462B RID: 17963 RVA: 0x0018B4CA File Offset: 0x001896CA
	public void Init(AxialI location)
	{
		base.Location = location;
	}

	// Token: 0x04002E75 RID: 11893
	public string m_name;

	// Token: 0x04002E76 RID: 11894
	public string m_Anim;
}
