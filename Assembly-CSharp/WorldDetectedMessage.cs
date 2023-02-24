using System;
using KSerialization;
using STRINGS;

// Token: 0x02000B27 RID: 2855
public class WorldDetectedMessage : Message
{
	// Token: 0x060057F6 RID: 22518 RVA: 0x001FDABF File Offset: 0x001FBCBF
	public WorldDetectedMessage()
	{
	}

	// Token: 0x060057F7 RID: 22519 RVA: 0x001FDAC7 File Offset: 0x001FBCC7
	public WorldDetectedMessage(WorldContainer world)
	{
		this.worldID = world.id;
	}

	// Token: 0x060057F8 RID: 22520 RVA: 0x001FDADB File Offset: 0x001FBCDB
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x060057F9 RID: 22521 RVA: 0x001FDAE4 File Offset: 0x001FBCE4
	public override string GetMessageBody()
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(this.worldID);
		return string.Format(MISC.NOTIFICATIONS.WORLDDETECTED.MESSAGEBODY, world.GetProperName());
	}

	// Token: 0x060057FA RID: 22522 RVA: 0x001FDB17 File Offset: 0x001FBD17
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.WORLDDETECTED.NAME;
	}

	// Token: 0x060057FB RID: 22523 RVA: 0x001FDB24 File Offset: 0x001FBD24
	public override string GetTooltip()
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(this.worldID);
		return string.Format(MISC.NOTIFICATIONS.WORLDDETECTED.TOOLTIP, world.GetProperName());
	}

	// Token: 0x060057FC RID: 22524 RVA: 0x001FDB57 File Offset: 0x001FBD57
	public override bool IsValid()
	{
		return this.worldID != (int)ClusterManager.INVALID_WORLD_IDX;
	}

	// Token: 0x04003B82 RID: 15234
	[Serialize]
	private int worldID;
}
