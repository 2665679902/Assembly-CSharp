using System;
using UnityEngine;

// Token: 0x02000720 RID: 1824
public struct DiseaseContainer
{
	// Token: 0x060031E2 RID: 12770 RVA: 0x0010AD7C File Offset: 0x00108F7C
	public DiseaseContainer(GameObject go, ushort elemIdx)
	{
		this.elemIdx = elemIdx;
		this.isContainer = go.GetComponent<IUserControlledCapacity>() != null && go.GetComponent<Storage>() != null;
		Conduit component = go.GetComponent<Conduit>();
		if (component != null)
		{
			this.conduitType = component.type;
		}
		else
		{
			this.conduitType = ConduitType.None;
		}
		this.controller = go.GetComponent<KBatchedAnimController>();
		this.overpopulationCount = 1;
		this.instanceGrowthRate = 1f;
		this.accumulatedError = 0f;
		this.visualDiseaseProvider = null;
		this.autoDisinfectable = go.GetComponent<AutoDisinfectable>();
		if (this.autoDisinfectable != null)
		{
			AutoDisinfectableManager.Instance.AddAutoDisinfectable(this.autoDisinfectable);
		}
	}

	// Token: 0x060031E3 RID: 12771 RVA: 0x0010AE2C File Offset: 0x0010902C
	public void Clear()
	{
		this.controller = null;
	}

	// Token: 0x04001E49 RID: 7753
	public AutoDisinfectable autoDisinfectable;

	// Token: 0x04001E4A RID: 7754
	public ushort elemIdx;

	// Token: 0x04001E4B RID: 7755
	public bool isContainer;

	// Token: 0x04001E4C RID: 7756
	public ConduitType conduitType;

	// Token: 0x04001E4D RID: 7757
	public KBatchedAnimController controller;

	// Token: 0x04001E4E RID: 7758
	public GameObject visualDiseaseProvider;

	// Token: 0x04001E4F RID: 7759
	public int overpopulationCount;

	// Token: 0x04001E50 RID: 7760
	public float instanceGrowthRate;

	// Token: 0x04001E51 RID: 7761
	public float accumulatedError;
}
