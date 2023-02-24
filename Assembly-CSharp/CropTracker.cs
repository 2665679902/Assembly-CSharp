using System;

// Token: 0x020004E5 RID: 1253
public class CropTracker : WorldTracker
{
	// Token: 0x06001DAD RID: 7597 RVA: 0x0009E793 File Offset: 0x0009C993
	public CropTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DAE RID: 7598 RVA: 0x0009E79C File Offset: 0x0009C99C
	public override void UpdateData()
	{
		float num = 0f;
		foreach (PlantablePlot plantablePlot in Components.PlantablePlots.GetItems(base.WorldID))
		{
			if (!(plantablePlot.plant == null) && plantablePlot.HasDepositTag(GameTags.CropSeed) && !plantablePlot.plant.HasTag(GameTags.Wilting))
			{
				num += 1f;
			}
		}
		base.AddPoint(num);
	}

	// Token: 0x06001DAF RID: 7599 RVA: 0x0009E834 File Offset: 0x0009CA34
	public override string FormatValueString(float value)
	{
		return value.ToString() + "%";
	}
}
