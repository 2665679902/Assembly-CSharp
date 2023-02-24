using System;

// Token: 0x020004E1 RID: 1249
public class AllChoresCountTracker : WorldTracker
{
	// Token: 0x06001DA1 RID: 7585 RVA: 0x0009E44E File Offset: 0x0009C64E
	public AllChoresCountTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DA2 RID: 7586 RVA: 0x0009E458 File Offset: 0x0009C658
	public override void UpdateData()
	{
		float num = 0f;
		for (int i = 0; i < Db.Get().ChoreGroups.Count; i++)
		{
			Tracker choreGroupTracker = TrackerTool.Instance.GetChoreGroupTracker(base.WorldID, Db.Get().ChoreGroups[i]);
			num += ((choreGroupTracker == null) ? 0f : choreGroupTracker.GetCurrentValue());
		}
		base.AddPoint(num);
	}

	// Token: 0x06001DA3 RID: 7587 RVA: 0x0009E4C0 File Offset: 0x0009C6C0
	public override string FormatValueString(float value)
	{
		return value.ToString();
	}
}
