using System;
using KSerialization;

// Token: 0x02000B25 RID: 2853
public class TutorialMessage : GenericMessage
{
	// Token: 0x060057ED RID: 22509 RVA: 0x001FD932 File Offset: 0x001FBB32
	public TutorialMessage()
	{
	}

	// Token: 0x060057EE RID: 22510 RVA: 0x001FD948 File Offset: 0x001FBB48
	public TutorialMessage(Tutorial.TutorialMessages messageId, string title, string body, string tooltip, string videoClipId = null, string videoOverlayName = null, string videoTitleText = null, string icon = "", string[] overrideDLCIDs = null)
		: base(title, body, tooltip, null)
	{
		this.messageId = messageId;
		this.videoClipId = videoClipId;
		this.videoOverlayName = videoOverlayName;
		this.videoTitleText = videoTitleText;
		this.icon = icon;
		if (overrideDLCIDs != null)
		{
			this.DLCIDs = overrideDLCIDs;
		}
	}

	// Token: 0x04003B78 RID: 15224
	[Serialize]
	public Tutorial.TutorialMessages messageId;

	// Token: 0x04003B79 RID: 15225
	public string videoClipId;

	// Token: 0x04003B7A RID: 15226
	public string videoOverlayName;

	// Token: 0x04003B7B RID: 15227
	public string videoTitleText;

	// Token: 0x04003B7C RID: 15228
	public string icon;

	// Token: 0x04003B7D RID: 15229
	public string[] DLCIDs = DlcManager.AVAILABLE_ALL_VERSIONS;
}
