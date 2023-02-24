using System;
using UnityEngine;

// Token: 0x02000AF0 RID: 2800
[AddComponentMenu("KMonoBehaviour/scripts/MainMenuIntroShort")]
public class MainMenuIntroShort : KMonoBehaviour
{
	// Token: 0x060055EB RID: 21995 RVA: 0x001F110C File Offset: 0x001EF30C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		string @string = KPlayerPrefs.GetString("PlayShortOnLaunch", "");
		if (!string.IsNullOrEmpty(MainMenu.Instance.IntroShortName) && @string != MainMenu.Instance.IntroShortName)
		{
			VideoScreen component = KScreenManager.AddChild(FrontEndManager.Instance.gameObject, ScreenPrefabs.Instance.VideoScreen.gameObject).GetComponent<VideoScreen>();
			component.PlayVideo(Assets.GetVideo(MainMenu.Instance.IntroShortName), false, AudioMixerSnapshots.Get().MainMenuVideoPlayingSnapshot, false);
			component.OnStop = (System.Action)Delegate.Combine(component.OnStop, new System.Action(delegate
			{
				KPlayerPrefs.SetString("PlayShortOnLaunch", MainMenu.Instance.IntroShortName);
				base.gameObject.SetActive(false);
			}));
			return;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04003A75 RID: 14965
	[SerializeField]
	private bool alwaysPlay;
}
