using System;
using UnityEngine;

// Token: 0x0200036B RID: 875
[AddComponentMenu("KMonoBehaviour/scripts/Brain")]
public class Brain : KMonoBehaviour
{
	// Token: 0x060011DD RID: 4573 RVA: 0x0005E5CF File Offset: 0x0005C7CF
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x0005E5D7 File Offset: 0x0005C7D7
	protected override void OnSpawn()
	{
		this.prefabId = base.GetComponent<KPrefabID>();
		this.choreConsumer = base.GetComponent<ChoreConsumer>();
		this.running = true;
		Components.Brains.Add(this);
	}

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x060011DF RID: 4575 RVA: 0x0005E604 File Offset: 0x0005C804
	// (remove) Token: 0x060011E0 RID: 4576 RVA: 0x0005E63C File Offset: 0x0005C83C
	public event System.Action onPreUpdate;

	// Token: 0x060011E1 RID: 4577 RVA: 0x0005E671 File Offset: 0x0005C871
	public virtual void UpdateBrain()
	{
		if (this.onPreUpdate != null)
		{
			this.onPreUpdate();
		}
		if (this.IsRunning())
		{
			this.UpdateChores();
		}
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x0005E694 File Offset: 0x0005C894
	private bool FindBetterChore(ref Chore.Precondition.Context context)
	{
		return this.choreConsumer.FindNextChore(ref context);
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x0005E6A4 File Offset: 0x0005C8A4
	private void UpdateChores()
	{
		if (this.prefabId.HasTag(GameTags.PreventChoreInterruption))
		{
			return;
		}
		Chore.Precondition.Context context = default(Chore.Precondition.Context);
		if (this.FindBetterChore(ref context))
		{
			if (this.prefabId.HasTag(GameTags.PerformingWorkRequest))
			{
				base.Trigger(1485595942, null);
				return;
			}
			this.choreConsumer.choreDriver.SetChore(context);
		}
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x0005E706 File Offset: 0x0005C906
	public bool IsRunning()
	{
		return this.running && !this.suspend;
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x0005E71B File Offset: 0x0005C91B
	public void Reset(string reason)
	{
		this.Stop("Reset");
		this.running = true;
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x0005E72F File Offset: 0x0005C92F
	public void Stop(string reason)
	{
		base.GetComponent<ChoreDriver>().StopChore();
		this.running = false;
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x0005E743 File Offset: 0x0005C943
	public void Resume(string caller)
	{
		this.suspend = false;
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x0005E74C File Offset: 0x0005C94C
	public void Suspend(string caller)
	{
		this.suspend = true;
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x0005E755 File Offset: 0x0005C955
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		this.Stop("OnCmpDisable");
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x0005E768 File Offset: 0x0005C968
	protected override void OnCleanUp()
	{
		this.Stop("OnCleanUp");
		Components.Brains.Remove(this);
	}

	// Token: 0x04000997 RID: 2455
	private bool running;

	// Token: 0x04000998 RID: 2456
	private bool suspend;

	// Token: 0x04000999 RID: 2457
	protected KPrefabID prefabId;

	// Token: 0x0400099A RID: 2458
	protected ChoreConsumer choreConsumer;
}
