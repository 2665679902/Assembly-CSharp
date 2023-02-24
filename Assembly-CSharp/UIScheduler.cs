using System;
using UnityEngine;

// Token: 0x02000C1C RID: 3100
[AddComponentMenu("KMonoBehaviour/scripts/UIScheduler")]
public class UIScheduler : KMonoBehaviour, IScheduler
{
	// Token: 0x0600622C RID: 25132 RVA: 0x00244334 File Offset: 0x00242534
	public static void DestroyInstance()
	{
		UIScheduler.Instance = null;
	}

	// Token: 0x0600622D RID: 25133 RVA: 0x0024433C File Offset: 0x0024253C
	protected override void OnPrefabInit()
	{
		UIScheduler.Instance = this;
	}

	// Token: 0x0600622E RID: 25134 RVA: 0x00244344 File Offset: 0x00242544
	public SchedulerHandle Schedule(string name, float time, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		return this.scheduler.Schedule(name, time, callback, callback_data, group);
	}

	// Token: 0x0600622F RID: 25135 RVA: 0x00244358 File Offset: 0x00242558
	public SchedulerHandle ScheduleNextFrame(string name, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		return this.scheduler.Schedule(name, 0f, callback, callback_data, group);
	}

	// Token: 0x06006230 RID: 25136 RVA: 0x0024436F File Offset: 0x0024256F
	private void Update()
	{
		this.scheduler.Update();
	}

	// Token: 0x06006231 RID: 25137 RVA: 0x0024437C File Offset: 0x0024257C
	protected override void OnLoadLevel()
	{
		this.scheduler.FreeResources();
		this.scheduler = null;
	}

	// Token: 0x06006232 RID: 25138 RVA: 0x00244390 File Offset: 0x00242590
	public SchedulerGroup CreateGroup()
	{
		return new SchedulerGroup(this.scheduler);
	}

	// Token: 0x06006233 RID: 25139 RVA: 0x0024439D File Offset: 0x0024259D
	public Scheduler GetScheduler()
	{
		return this.scheduler;
	}

	// Token: 0x040043E5 RID: 17381
	private Scheduler scheduler = new Scheduler(new UIScheduler.UISchedulerClock());

	// Token: 0x040043E6 RID: 17382
	public static UIScheduler Instance;

	// Token: 0x02001AAF RID: 6831
	public class UISchedulerClock : SchedulerClock
	{
		// Token: 0x060093D5 RID: 37845 RVA: 0x0031A772 File Offset: 0x00318972
		public override float GetTime()
		{
			return Time.unscaledTime;
		}
	}
}
