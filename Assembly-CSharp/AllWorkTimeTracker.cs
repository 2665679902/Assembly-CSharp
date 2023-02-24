using System;

// Token: 0x020004E3 RID: 1251
public class AllWorkTimeTracker : WorldTracker
{
	// Token: 0x06001DA7 RID: 7591 RVA: 0x0009E622 File Offset: 0x0009C822
	public AllWorkTimeTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DA8 RID: 7592 RVA: 0x0009E62C File Offset: 0x0009C82C
	public override void UpdateData()
	{
		float num = 0f;
		for (int i = 0; i < Db.Get().ChoreGroups.Count; i++)
		{
			num += TrackerTool.Instance.GetWorkTimeTracker(base.WorldID, Db.Get().ChoreGroups[i]).GetCurrentValue();
		}
		base.AddPoint(num);
	}

	// Token: 0x06001DA9 RID: 7593 RVA: 0x0009E688 File Offset: 0x0009C888
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedPercent(value, GameUtil.TimeSlice.None).ToString();
	}
}
