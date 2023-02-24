using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.Video;

// Token: 0x02000B26 RID: 2854
public class TutorialMessageDialog : MessageDialog
{
	// Token: 0x17000654 RID: 1620
	// (get) Token: 0x060057EF RID: 22511 RVA: 0x001FD99E File Offset: 0x001FBB9E
	public override bool CanDontShowAgain
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060057F0 RID: 22512 RVA: 0x001FD9A1 File Offset: 0x001FBBA1
	public override bool CanDisplay(Message message)
	{
		return typeof(TutorialMessage).IsAssignableFrom(message.GetType());
	}

	// Token: 0x060057F1 RID: 22513 RVA: 0x001FD9B8 File Offset: 0x001FBBB8
	public override void SetMessage(Message base_message)
	{
		this.message = base_message as TutorialMessage;
		this.description.text = this.message.GetMessageBody();
		if (!string.IsNullOrEmpty(this.message.videoClipId))
		{
			VideoClip video = Assets.GetVideo(this.message.videoClipId);
			this.SetVideo(video, this.message.videoOverlayName, this.message.videoTitleText);
		}
	}

	// Token: 0x060057F2 RID: 22514 RVA: 0x001FDA28 File Offset: 0x001FBC28
	public void SetVideo(VideoClip clip, string overlayName, string titleText)
	{
		if (this.videoWidget == null)
		{
			this.videoWidget = Util.KInstantiateUI(this.videoWidgetPrefab, base.transform.gameObject, true).GetComponent<VideoWidget>();
			this.videoWidget.transform.SetAsFirstSibling();
		}
		this.videoWidget.SetClip(clip, overlayName, new List<string>
		{
			titleText,
			VIDEOS.TUTORIAL_HEADER
		});
	}

	// Token: 0x060057F3 RID: 22515 RVA: 0x001FDA9E File Offset: 0x001FBC9E
	public override void OnClickAction()
	{
	}

	// Token: 0x060057F4 RID: 22516 RVA: 0x001FDAA0 File Offset: 0x001FBCA0
	public override void OnDontShowAgain()
	{
		Tutorial.Instance.HideTutorialMessage(this.message.messageId);
	}

	// Token: 0x04003B7E RID: 15230
	[SerializeField]
	private LocText description;

	// Token: 0x04003B7F RID: 15231
	private TutorialMessage message;

	// Token: 0x04003B80 RID: 15232
	[SerializeField]
	private GameObject videoWidgetPrefab;

	// Token: 0x04003B81 RID: 15233
	private VideoWidget videoWidget;
}
