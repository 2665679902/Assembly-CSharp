using System;
using UnityEngine;

// Token: 0x020003E1 RID: 993
[AddComponentMenu("KMonoBehaviour/scripts/GameScheduler")]
public class GameScheduler : KMonoBehaviour, IScheduler
{
	// Token: 0x0600149C RID: 5276 RVA: 0x0006CA8C File Offset: 0x0006AC8C
	public static void DestroyInstance()
	{
		GameScheduler.Instance = null;
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x0006CA94 File Offset: 0x0006AC94
	protected override void OnPrefabInit()
	{
		GameScheduler.Instance = this;
		Singleton<StateMachineManager>.Instance.RegisterScheduler(this.scheduler);
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x0006CAAC File Offset: 0x0006ACAC
	public SchedulerHandle Schedule(string name, float time, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		return this.scheduler.Schedule(name, time, callback, callback_data, group);
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x0006CAC0 File Offset: 0x0006ACC0
	public SchedulerHandle ScheduleNextFrame(string name, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		return this.scheduler.Schedule(name, 0f, callback, callback_data, group);
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x0006CAD7 File Offset: 0x0006ACD7
	private void Update()
	{
		this.scheduler.Update();
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x0006CAE4 File Offset: 0x0006ACE4
	protected override void OnLoadLevel()
	{
		this.scheduler.FreeResources();
		this.scheduler = null;
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x0006CAF8 File Offset: 0x0006ACF8
	public SchedulerGroup CreateGroup()
	{
		return new SchedulerGroup(this.scheduler);
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x0006CB05 File Offset: 0x0006AD05
	public Scheduler GetScheduler()
	{
		return this.scheduler;
	}

	// Token: 0x04000B93 RID: 2963
	private Scheduler scheduler = new Scheduler(new GameScheduler.GameSchedulerClock());

	// Token: 0x04000B94 RID: 2964
	public static GameScheduler Instance;

	// Token: 0x02001000 RID: 4096
	public class GameSchedulerClock : SchedulerClock
	{
		// Token: 0x0600712B RID: 28971 RVA: 0x002A89F7 File Offset: 0x002A6BF7
		public override float GetTime()
		{
			return GameClock.Instance.GetTime();
		}
	}
}
