using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000B14 RID: 2836
public class DiscoveredSpaceMessage : Message
{
	// Token: 0x06005772 RID: 22386 RVA: 0x001FCA92 File Offset: 0x001FAC92
	public DiscoveredSpaceMessage()
	{
	}

	// Token: 0x06005773 RID: 22387 RVA: 0x001FCA9A File Offset: 0x001FAC9A
	public DiscoveredSpaceMessage(Vector3 pos)
	{
		this.cameraFocusPos = pos;
		this.cameraFocusPos.z = -40f;
	}

	// Token: 0x06005774 RID: 22388 RVA: 0x001FCAB9 File Offset: 0x001FACB9
	public override string GetSound()
	{
		return "Discover_Space";
	}

	// Token: 0x06005775 RID: 22389 RVA: 0x001FCAC0 File Offset: 0x001FACC0
	public override string GetMessageBody()
	{
		return MISC.NOTIFICATIONS.DISCOVERED_SPACE.TOOLTIP;
	}

	// Token: 0x06005776 RID: 22390 RVA: 0x001FCACC File Offset: 0x001FACCC
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.DISCOVERED_SPACE.NAME;
	}

	// Token: 0x06005777 RID: 22391 RVA: 0x001FCAD8 File Offset: 0x001FACD8
	public override string GetTooltip()
	{
		return null;
	}

	// Token: 0x06005778 RID: 22392 RVA: 0x001FCADB File Offset: 0x001FACDB
	public override bool IsValid()
	{
		return true;
	}

	// Token: 0x06005779 RID: 22393 RVA: 0x001FCADE File Offset: 0x001FACDE
	public override void OnClick()
	{
		this.OnDiscoveredSpaceClicked();
	}

	// Token: 0x0600577A RID: 22394 RVA: 0x001FCAE6 File Offset: 0x001FACE6
	private void OnDiscoveredSpaceClicked()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound(this.GetSound(), false));
		MusicManager.instance.PlaySong("Stinger_Surface", false);
		CameraController.Instance.SetTargetPos(this.cameraFocusPos, 8f, true);
	}

	// Token: 0x04003B50 RID: 15184
	[Serialize]
	private Vector3 cameraFocusPos;

	// Token: 0x04003B51 RID: 15185
	private const string MUSIC_STINGER = "Stinger_Surface";
}
