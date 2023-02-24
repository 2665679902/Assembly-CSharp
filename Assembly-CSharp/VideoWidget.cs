using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x02000C23 RID: 3107
[AddComponentMenu("KMonoBehaviour/scripts/VideoWidget")]
public class VideoWidget : KMonoBehaviour
{
	// Token: 0x0600625D RID: 25181 RVA: 0x00245022 File Offset: 0x00243222
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.button.onClick += this.Clicked;
		this.rawImage = this.thumbnailPlayer.GetComponent<RawImage>();
	}

	// Token: 0x0600625E RID: 25182 RVA: 0x00245054 File Offset: 0x00243254
	private void Clicked()
	{
		VideoScreen.Instance.PlayVideo(this.clip, false, default(EventReference), false);
		if (!string.IsNullOrEmpty(this.overlayName))
		{
			VideoScreen.Instance.SetOverlayText(this.overlayName, this.texts);
		}
	}

	// Token: 0x0600625F RID: 25183 RVA: 0x002450A0 File Offset: 0x002432A0
	public void SetClip(VideoClip clip, string overlayName = null, List<string> texts = null)
	{
		if (clip == null)
		{
			global::Debug.LogWarning("Tried to assign null video clip to VideoWidget");
			return;
		}
		this.clip = clip;
		this.overlayName = overlayName;
		this.texts = texts;
		this.renderTexture = new RenderTexture(Convert.ToInt32(clip.width), Convert.ToInt32(clip.height), 16);
		this.thumbnailPlayer.targetTexture = this.renderTexture;
		this.rawImage.texture = this.renderTexture;
		base.StartCoroutine(this.ConfigureThumbnail());
	}

	// Token: 0x06006260 RID: 25184 RVA: 0x00245128 File Offset: 0x00243328
	private IEnumerator ConfigureThumbnail()
	{
		this.thumbnailPlayer.audioOutputMode = VideoAudioOutputMode.None;
		this.thumbnailPlayer.clip = this.clip;
		this.thumbnailPlayer.time = 0.0;
		this.thumbnailPlayer.Play();
		yield return null;
		yield break;
	}

	// Token: 0x06006261 RID: 25185 RVA: 0x00245137 File Offset: 0x00243337
	private void Update()
	{
		if (this.thumbnailPlayer.isPlaying && this.thumbnailPlayer.time > 2.0)
		{
			this.thumbnailPlayer.Pause();
		}
	}

	// Token: 0x0400440C RID: 17420
	[SerializeField]
	private VideoClip clip;

	// Token: 0x0400440D RID: 17421
	[SerializeField]
	private VideoPlayer thumbnailPlayer;

	// Token: 0x0400440E RID: 17422
	[SerializeField]
	private KButton button;

	// Token: 0x0400440F RID: 17423
	[SerializeField]
	private string overlayName;

	// Token: 0x04004410 RID: 17424
	[SerializeField]
	private List<string> texts;

	// Token: 0x04004411 RID: 17425
	private RenderTexture renderTexture;

	// Token: 0x04004412 RID: 17426
	private RawImage rawImage;
}
