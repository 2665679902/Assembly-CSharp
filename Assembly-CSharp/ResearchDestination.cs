using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;

// Token: 0x02000958 RID: 2392
[SerializationConfig(MemberSerialization.OptIn)]
public class ResearchDestination : ClusterGridEntity
{
	// Token: 0x17000541 RID: 1345
	// (get) Token: 0x060046B1 RID: 18097 RVA: 0x0018DF99 File Offset: 0x0018C199
	public override string Name
	{
		get
		{
			return UI.SPACEDESTINATIONS.RESEARCHDESTINATION.NAME;
		}
	}

	// Token: 0x17000542 RID: 1346
	// (get) Token: 0x060046B2 RID: 18098 RVA: 0x0018DFA5 File Offset: 0x0018C1A5
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.POI;
		}
	}

	// Token: 0x17000543 RID: 1347
	// (get) Token: 0x060046B3 RID: 18099 RVA: 0x0018DFA8 File Offset: 0x0018C1A8
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>();
		}
	}

	// Token: 0x17000544 RID: 1348
	// (get) Token: 0x060046B4 RID: 18100 RVA: 0x0018DFAF File Offset: 0x0018C1AF
	public override bool IsVisible
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000545 RID: 1349
	// (get) Token: 0x060046B5 RID: 18101 RVA: 0x0018DFB2 File Offset: 0x0018C1B2
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Peeked;
		}
	}

	// Token: 0x060046B6 RID: 18102 RVA: 0x0018DFB5 File Offset: 0x0018C1B5
	public void Init(AxialI location)
	{
		this.m_location = location;
	}
}
