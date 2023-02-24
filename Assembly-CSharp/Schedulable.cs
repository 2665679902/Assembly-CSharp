using System;
using UnityEngine;

// Token: 0x02000908 RID: 2312
[AddComponentMenu("KMonoBehaviour/scripts/Schedulable")]
public class Schedulable : KMonoBehaviour
{
	// Token: 0x0600435B RID: 17243 RVA: 0x0017CE44 File Offset: 0x0017B044
	public Schedule GetSchedule()
	{
		return ScheduleManager.Instance.GetSchedule(this);
	}

	// Token: 0x0600435C RID: 17244 RVA: 0x0017CE54 File Offset: 0x0017B054
	public bool IsAllowed(ScheduleBlockType schedule_block_type)
	{
		WorldContainer myWorld = base.gameObject.GetMyWorld();
		if (myWorld == null)
		{
			DebugUtil.LogWarningArgs(new object[] { string.Format("Trying to schedule {0} but {1} is not on a valid world. Grid cell: {2}", schedule_block_type.Id, base.gameObject.name, Grid.PosToCell(base.gameObject.GetComponent<KPrefabID>())) });
			return false;
		}
		return myWorld.AlertManager.IsRedAlert() || ScheduleManager.Instance.IsAllowed(this, schedule_block_type);
	}

	// Token: 0x0600435D RID: 17245 RVA: 0x0017CED1 File Offset: 0x0017B0D1
	public void OnScheduleChanged(Schedule schedule)
	{
		base.Trigger(467134493, schedule);
	}

	// Token: 0x0600435E RID: 17246 RVA: 0x0017CEDF File Offset: 0x0017B0DF
	public void OnScheduleBlocksTick(Schedule schedule)
	{
		base.Trigger(1714332666, schedule);
	}

	// Token: 0x0600435F RID: 17247 RVA: 0x0017CEED File Offset: 0x0017B0ED
	public void OnScheduleBlocksChanged(Schedule schedule)
	{
		base.Trigger(-894023145, schedule);
	}
}
