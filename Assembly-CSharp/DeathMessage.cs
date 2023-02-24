using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000B13 RID: 2835
public class DeathMessage : TargetMessage
{
	// Token: 0x0600576B RID: 22379 RVA: 0x001FCA15 File Offset: 0x001FAC15
	public DeathMessage()
	{
	}

	// Token: 0x0600576C RID: 22380 RVA: 0x001FCA28 File Offset: 0x001FAC28
	public DeathMessage(GameObject go, Death death)
		: base(go.GetComponent<KPrefabID>())
	{
		this.death.Set(death);
	}

	// Token: 0x0600576D RID: 22381 RVA: 0x001FCA4D File Offset: 0x001FAC4D
	public override string GetSound()
	{
		return "";
	}

	// Token: 0x0600576E RID: 22382 RVA: 0x001FCA54 File Offset: 0x001FAC54
	public override bool PlayNotificationSound()
	{
		return false;
	}

	// Token: 0x0600576F RID: 22383 RVA: 0x001FCA57 File Offset: 0x001FAC57
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.DUPLICANTDIED.NAME;
	}

	// Token: 0x06005770 RID: 22384 RVA: 0x001FCA63 File Offset: 0x001FAC63
	public override string GetTooltip()
	{
		return this.GetMessageBody();
	}

	// Token: 0x06005771 RID: 22385 RVA: 0x001FCA6B File Offset: 0x001FAC6B
	public override string GetMessageBody()
	{
		return this.death.Get().description.Replace("{Target}", base.GetTarget().GetName());
	}

	// Token: 0x04003B4F RID: 15183
	[Serialize]
	private ResourceRef<Death> death = new ResourceRef<Death>();
}
