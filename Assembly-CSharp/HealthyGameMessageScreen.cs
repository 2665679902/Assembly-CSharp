using System;
using UnityEngine;

// Token: 0x02000AB2 RID: 2738
[AddComponentMenu("KMonoBehaviour/scripts/HealthyGameMessageScreen")]
public class HealthyGameMessageScreen : KMonoBehaviour
{
	// Token: 0x060053E0 RID: 21472 RVA: 0x001E7EE7 File Offset: 0x001E60E7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.confirmButton.onClick += delegate
		{
			this.PlayIntroShort();
		};
		this.confirmButton.gameObject.SetActive(false);
	}

	// Token: 0x060053E1 RID: 21473 RVA: 0x001E7F18 File Offset: 0x001E6118
	private void PlayIntroShort()
	{
		string @string = KPlayerPrefs.GetString("PlayShortOnLaunch", "");
		if (!string.IsNullOrEmpty(MainMenu.Instance.IntroShortName) && @string != MainMenu.Instance.IntroShortName)
		{
			VideoScreen component = KScreenManager.AddChild(FrontEndManager.Instance.gameObject, ScreenPrefabs.Instance.VideoScreen.gameObject).GetComponent<VideoScreen>();
			component.PlayVideo(Assets.GetVideo(MainMenu.Instance.IntroShortName), false, AudioMixerSnapshots.Get().MainMenuVideoPlayingSnapshot, false);
			component.OnStop = (System.Action)Delegate.Combine(component.OnStop, new System.Action(delegate
			{
				KPlayerPrefs.SetString("PlayShortOnLaunch", MainMenu.Instance.IntroShortName);
				if (base.gameObject != null)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}));
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060053E2 RID: 21474 RVA: 0x001E7FC9 File Offset: 0x001E61C9
	protected override void OnSpawn()
	{
		base.OnSpawn();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060053E3 RID: 21475 RVA: 0x001E7FDC File Offset: 0x001E61DC
	private void Update()
	{
		if (!DistributionPlatform.Inst.IsDLCStatusReady())
		{
			return;
		}
		if (this.isFirstUpdate)
		{
			this.isFirstUpdate = false;
			this.spawnTime = Time.unscaledTime;
			return;
		}
		float num = Mathf.Min(Time.unscaledDeltaTime, 0.033333335f);
		float num2 = Time.unscaledTime - this.spawnTime;
		if (num2 < this.totalTime - this.fadeTime)
		{
			this.canvasGroup.alpha = this.canvasGroup.alpha + num * (1f / this.fadeTime);
			return;
		}
		if (num2 >= this.totalTime + 0.75f)
		{
			this.canvasGroup.alpha = 1f;
			this.confirmButton.gameObject.SetActive(true);
			return;
		}
		if (num2 >= this.totalTime - this.fadeTime)
		{
			this.canvasGroup.alpha = this.canvasGroup.alpha - num * (1f / this.fadeTime);
		}
	}

	// Token: 0x04003905 RID: 14597
	public KButton confirmButton;

	// Token: 0x04003906 RID: 14598
	public CanvasGroup canvasGroup;

	// Token: 0x04003907 RID: 14599
	private float spawnTime;

	// Token: 0x04003908 RID: 14600
	private float totalTime = 10f;

	// Token: 0x04003909 RID: 14601
	private float fadeTime = 1.5f;

	// Token: 0x0400390A RID: 14602
	private bool isFirstUpdate = true;
}
