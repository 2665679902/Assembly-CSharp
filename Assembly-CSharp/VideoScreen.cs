using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x02000C22 RID: 3106
public class VideoScreen : KModalScreen
{
	// Token: 0x0600624B RID: 25163 RVA: 0x00244A30 File Offset: 0x00242C30
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.ConsumeMouseScroll = true;
		this.closeButton.onClick += delegate
		{
			this.Stop();
		};
		this.proceedButton.onClick += delegate
		{
			this.Stop();
		};
		this.videoPlayer.isLooping = false;
		this.videoPlayer.loopPointReached += delegate(VideoPlayer data)
		{
			if (this.victoryLoopQueued)
			{
				base.StartCoroutine(this.SwitchToVictoryLoop());
				return;
			}
			if (!this.videoPlayer.isLooping)
			{
				this.Stop();
			}
		};
		VideoScreen.Instance = this;
		this.Show(false);
	}

	// Token: 0x0600624C RID: 25164 RVA: 0x00244AA8 File Offset: 0x00242CA8
	protected override void OnForcedCleanUp()
	{
		VideoScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x0600624D RID: 25165 RVA: 0x00244AB6 File Offset: 0x00242CB6
	protected override void OnShow(bool show)
	{
		base.transform.SetAsLastSibling();
		base.OnShow(show);
		this.screen = this.videoPlayer.gameObject.GetComponent<RawImage>();
	}

	// Token: 0x0600624E RID: 25166 RVA: 0x00244AE0 File Offset: 0x00242CE0
	public void DisableAllMedia()
	{
		this.overlayContainer.gameObject.SetActive(false);
		this.videoPlayer.gameObject.SetActive(false);
		this.slideshow.gameObject.SetActive(false);
	}

	// Token: 0x0600624F RID: 25167 RVA: 0x00244B18 File Offset: 0x00242D18
	public void PlaySlideShow(Sprite[] sprites)
	{
		this.Show(true);
		this.DisableAllMedia();
		this.slideshow.updateType = SlideshowUpdateType.preloadedSprites;
		this.slideshow.gameObject.SetActive(true);
		this.slideshow.SetSprites(sprites);
		this.slideshow.SetPaused(false);
	}

	// Token: 0x06006250 RID: 25168 RVA: 0x00244B68 File Offset: 0x00242D68
	public void PlaySlideShow(string[] files)
	{
		this.Show(true);
		this.DisableAllMedia();
		this.slideshow.updateType = SlideshowUpdateType.loadOnDemand;
		this.slideshow.gameObject.SetActive(true);
		this.slideshow.SetFiles(files, 0);
		this.slideshow.SetPaused(false);
	}

	// Token: 0x06006251 RID: 25169 RVA: 0x00244BB8 File Offset: 0x00242DB8
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.IsAction(global::Action.Escape))
		{
			if (this.slideshow.gameObject.activeSelf && e.TryConsume(global::Action.Escape))
			{
				this.Stop();
				return;
			}
			if (e.TryConsume(global::Action.Escape))
			{
				if (this.videoSkippable)
				{
					this.Stop();
				}
				return;
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06006252 RID: 25170 RVA: 0x00244C10 File Offset: 0x00242E10
	public void PlayVideo(VideoClip clip, bool unskippable = false, EventReference overrideAudioSnapshot = default(EventReference), bool showProceedButton = false)
	{
		global::Debug.Assert(clip != null);
		for (int i = 0; i < this.overlayContainer.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.overlayContainer.GetChild(i).gameObject);
		}
		this.Show(true);
		this.videoPlayer.isLooping = false;
		this.activeAudioSnapshot = (overrideAudioSnapshot.IsNull ? AudioMixerSnapshots.Get().TutorialVideoPlayingSnapshot : overrideAudioSnapshot);
		AudioMixer.instance.Start(this.activeAudioSnapshot);
		this.DisableAllMedia();
		this.videoPlayer.gameObject.SetActive(true);
		this.renderTexture = new RenderTexture(Convert.ToInt32(clip.width), Convert.ToInt32(clip.height), 16);
		this.screen.texture = this.renderTexture;
		this.videoPlayer.targetTexture = this.renderTexture;
		this.videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
		this.videoPlayer.clip = clip;
		this.videoPlayer.Play();
		if (this.audioHandle.isValid())
		{
			KFMOD.EndOneShot(this.audioHandle);
			this.audioHandle.clearHandle();
		}
		this.audioHandle = KFMOD.BeginOneShot(GlobalAssets.GetSound("vid_" + clip.name, false), Vector3.zero, 1f);
		KFMOD.EndOneShot(this.audioHandle);
		this.videoSkippable = !unskippable;
		this.closeButton.gameObject.SetActive(this.videoSkippable);
		this.proceedButton.gameObject.SetActive(showProceedButton && this.videoSkippable);
	}

	// Token: 0x06006253 RID: 25171 RVA: 0x00244DAC File Offset: 0x00242FAC
	public void QueueVictoryVideoLoop(bool queue, string message = "", string victoryAchievement = "", string loopVideo = "")
	{
		this.victoryLoopQueued = queue;
		this.victoryLoopMessage = message;
		this.victoryLoopClip = loopVideo;
		this.OnStop = (System.Action)Delegate.Combine(this.OnStop, new System.Action(delegate
		{
			RetireColonyUtility.SaveColonySummaryData();
			MainMenu.ActivateRetiredColoniesScreenFromData(base.transform.parent.gameObject, RetireColonyUtility.GetCurrentColonyRetiredColonyData());
		}));
	}

	// Token: 0x06006254 RID: 25172 RVA: 0x00244DE8 File Offset: 0x00242FE8
	public void SetOverlayText(string overlayTemplate, List<string> strings)
	{
		VideoOverlay videoOverlay = null;
		foreach (VideoOverlay videoOverlay2 in this.overlayPrefabs)
		{
			if (videoOverlay2.name == overlayTemplate)
			{
				videoOverlay = videoOverlay2;
				break;
			}
		}
		DebugUtil.Assert(videoOverlay != null, "Could not find a template named ", overlayTemplate);
		global::Util.KInstantiateUI<VideoOverlay>(videoOverlay.gameObject, this.overlayContainer.gameObject, true).SetText(strings);
		this.overlayContainer.gameObject.SetActive(true);
	}

	// Token: 0x06006255 RID: 25173 RVA: 0x00244E88 File Offset: 0x00243088
	private IEnumerator SwitchToVictoryLoop()
	{
		this.victoryLoopQueued = false;
		Color color = this.fadeOverlay.color;
		for (float i = 0f; i < 1f; i += Time.unscaledDeltaTime)
		{
			this.fadeOverlay.color = new Color(color.r, color.g, color.b, i);
			yield return SequenceUtil.WaitForNextFrame;
		}
		this.fadeOverlay.color = new Color(color.r, color.g, color.b, 1f);
		MusicManager.instance.PlaySong("Music_Victory_03_StoryAndSummary", false);
		MusicManager.instance.SetSongParameter("Music_Victory_03_StoryAndSummary", "songSection", 1f, true);
		this.closeButton.gameObject.SetActive(true);
		this.proceedButton.gameObject.SetActive(true);
		this.SetOverlayText("VictoryEnd", new List<string> { this.victoryLoopMessage });
		this.videoPlayer.clip = Assets.GetVideo(this.victoryLoopClip);
		this.videoPlayer.isLooping = true;
		this.videoPlayer.Play();
		this.proceedButton.gameObject.SetActive(true);
		yield return SequenceUtil.WaitForSecondsRealtime(1f);
		for (float i = 1f; i >= 0f; i -= Time.unscaledDeltaTime)
		{
			this.fadeOverlay.color = new Color(color.r, color.g, color.b, i);
			yield return SequenceUtil.WaitForNextFrame;
		}
		this.fadeOverlay.color = new Color(color.r, color.g, color.b, 0f);
		yield break;
	}

	// Token: 0x06006256 RID: 25174 RVA: 0x00244E98 File Offset: 0x00243098
	public void Stop()
	{
		this.videoPlayer.Stop();
		this.screen.texture = null;
		this.videoPlayer.targetTexture = null;
		if (!this.activeAudioSnapshot.IsNull)
		{
			AudioMixer.instance.Stop(this.activeAudioSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			this.audioHandle.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}
		if (this.OnStop != null)
		{
			this.OnStop();
		}
		this.Show(false);
	}

	// Token: 0x06006257 RID: 25175 RVA: 0x00244F10 File Offset: 0x00243110
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
		if (this.audioHandle.isValid())
		{
			int num;
			this.audioHandle.getTimelinePosition(out num);
			double num2 = this.videoPlayer.time * 1000.0;
			if ((double)num - num2 > 33.0)
			{
				VideoPlayer videoPlayer = this.videoPlayer;
				long num3 = videoPlayer.frame;
				videoPlayer.frame = num3 + 1L;
				return;
			}
			if (num2 - (double)num > 33.0)
			{
				VideoPlayer videoPlayer2 = this.videoPlayer;
				long num3 = videoPlayer2.frame;
				videoPlayer2.frame = num3 - 1L;
			}
		}
	}

	// Token: 0x040043FB RID: 17403
	public static VideoScreen Instance;

	// Token: 0x040043FC RID: 17404
	[SerializeField]
	private VideoPlayer videoPlayer;

	// Token: 0x040043FD RID: 17405
	[SerializeField]
	private Slideshow slideshow;

	// Token: 0x040043FE RID: 17406
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040043FF RID: 17407
	[SerializeField]
	private KButton proceedButton;

	// Token: 0x04004400 RID: 17408
	[SerializeField]
	private RectTransform overlayContainer;

	// Token: 0x04004401 RID: 17409
	[SerializeField]
	private List<VideoOverlay> overlayPrefabs;

	// Token: 0x04004402 RID: 17410
	private RawImage screen;

	// Token: 0x04004403 RID: 17411
	private RenderTexture renderTexture;

	// Token: 0x04004404 RID: 17412
	private EventReference activeAudioSnapshot;

	// Token: 0x04004405 RID: 17413
	[SerializeField]
	private Image fadeOverlay;

	// Token: 0x04004406 RID: 17414
	private EventInstance audioHandle;

	// Token: 0x04004407 RID: 17415
	private bool victoryLoopQueued;

	// Token: 0x04004408 RID: 17416
	private string victoryLoopMessage = "";

	// Token: 0x04004409 RID: 17417
	private string victoryLoopClip = "";

	// Token: 0x0400440A RID: 17418
	private bool videoSkippable = true;

	// Token: 0x0400440B RID: 17419
	public System.Action OnStop;
}
