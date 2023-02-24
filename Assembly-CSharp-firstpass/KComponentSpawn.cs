using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class KComponentSpawn : MonoBehaviour, ISim200ms, ISim33ms
{
	// Token: 0x060006B6 RID: 1718 RVA: 0x0001DA65 File Offset: 0x0001BC65
	private void FixedUpdate()
	{
		KComponentCleanUp.SetInCleanUpPhase(false);
		this.comps.Spawn();
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0001DA78 File Offset: 0x0001BC78
	private void Update()
	{
		KComponentCleanUp.SetInCleanUpPhase(false);
		this.comps.Spawn();
		this.comps.RenderEveryTick(Time.deltaTime);
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0001DA9B File Offset: 0x0001BC9B
	public void Sim33ms(float dt)
	{
		this.comps.Sim33ms(dt);
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0001DAA9 File Offset: 0x0001BCA9
	public void Sim200ms(float dt)
	{
		this.comps.Sim200ms(dt);
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x0001DAB7 File Offset: 0x0001BCB7
	private void OnDestroy()
	{
		this.comps.Shutdown();
	}

	// Token: 0x040005CA RID: 1482
	public static KComponentSpawn instance;

	// Token: 0x040005CB RID: 1483
	public KComponents comps;
}
