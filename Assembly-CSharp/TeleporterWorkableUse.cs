using System;

// Token: 0x02000658 RID: 1624
public class TeleporterWorkableUse : Workable
{
	// Token: 0x06002B84 RID: 11140 RVA: 0x000E4D3A File Offset: 0x000E2F3A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002B85 RID: 11141 RVA: 0x000E4D42 File Offset: 0x000E2F42
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(5f);
		this.resetProgressOnStop = true;
	}

	// Token: 0x06002B86 RID: 11142 RVA: 0x000E4D5C File Offset: 0x000E2F5C
	protected override void OnStartWork(Worker worker)
	{
		Teleporter component = base.GetComponent<Teleporter>();
		Teleporter teleporter = component.FindTeleportTarget();
		component.SetTeleportTarget(teleporter);
		TeleportalPad.StatesInstance smi = teleporter.GetSMI<TeleportalPad.StatesInstance>();
		smi.sm.targetTeleporter.Trigger(smi);
	}

	// Token: 0x06002B87 RID: 11143 RVA: 0x000E4D94 File Offset: 0x000E2F94
	protected override void OnStopWork(Worker worker)
	{
		TeleportalPad.StatesInstance smi = this.GetSMI<TeleportalPad.StatesInstance>();
		smi.sm.doTeleport.Trigger(smi);
	}
}
