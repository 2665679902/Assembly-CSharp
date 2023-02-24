using System;
using KSerialization;

// Token: 0x02000B23 RID: 2851
public abstract class TargetMessage : Message
{
	// Token: 0x060057E4 RID: 22500 RVA: 0x001FD87A File Offset: 0x001FBA7A
	protected TargetMessage()
	{
	}

	// Token: 0x060057E5 RID: 22501 RVA: 0x001FD882 File Offset: 0x001FBA82
	public TargetMessage(KPrefabID prefab_id)
	{
		this.target = new MessageTarget(prefab_id);
	}

	// Token: 0x060057E6 RID: 22502 RVA: 0x001FD896 File Offset: 0x001FBA96
	public MessageTarget GetTarget()
	{
		return this.target;
	}

	// Token: 0x060057E7 RID: 22503 RVA: 0x001FD89E File Offset: 0x001FBA9E
	public override void OnCleanUp()
	{
		this.target.OnCleanUp();
	}

	// Token: 0x04003B75 RID: 15221
	[Serialize]
	private MessageTarget target;
}
