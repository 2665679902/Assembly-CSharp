using System;
using System.Collections.Generic;

// Token: 0x020000B6 RID: 182
public class KComponents
{
	// Token: 0x060006C8 RID: 1736 RVA: 0x0001DADF File Offset: 0x0001BCDF
	public virtual void Shutdown()
	{
		this.managers.Clear();
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0001DAEC File Offset: 0x0001BCEC
	protected T Add<T>(T manager) where T : IComponentManager
	{
		this.managers.Add(manager);
		return manager;
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0001DB00 File Offset: 0x0001BD00
	public void Spawn()
	{
		if (this.spawned)
		{
			return;
		}
		this.spawned = true;
		foreach (IComponentManager componentManager in this.managers)
		{
			componentManager.Spawn();
		}
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0001DB60 File Offset: 0x0001BD60
	public void Sim33ms(float dt)
	{
		foreach (IComponentManager componentManager in this.managers)
		{
			componentManager.FixedUpdate(dt);
		}
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
	public void RenderEveryTick(float dt)
	{
		foreach (IComponentManager componentManager in this.managers)
		{
			componentManager.RenderEveryTick(dt);
		}
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0001DC08 File Offset: 0x0001BE08
	public void Sim200ms(float dt)
	{
		foreach (IComponentManager componentManager in this.managers)
		{
			componentManager.Sim200ms(dt);
		}
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0001DC5C File Offset: 0x0001BE5C
	public void CleanUp()
	{
		foreach (IComponentManager componentManager in this.managers)
		{
			componentManager.CleanUp();
		}
		this.spawned = false;
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x0001DCB4 File Offset: 0x0001BEB4
	public void Clear()
	{
		foreach (IComponentManager componentManager in this.managers)
		{
			componentManager.Clear();
		}
		this.spawned = false;
	}

	// Token: 0x040005CC RID: 1484
	private List<IComponentManager> managers = new List<IComponentManager>();

	// Token: 0x040005CD RID: 1485
	private bool spawned;
}
