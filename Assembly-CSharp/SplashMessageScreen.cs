using System;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C02 RID: 3074
[AddComponentMenu("KMonoBehaviour/scripts/SplashMessageScreen")]
public class SplashMessageScreen : KMonoBehaviour
{
	// Token: 0x06006144 RID: 24900 RVA: 0x0023C630 File Offset: 0x0023A830
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.forumButton.onClick += delegate
		{
			App.OpenWebURL("https://forums.kleientertainment.com/forums/forum/118-oxygen-not-included/");
		};
		this.confirmButton.onClick += delegate
		{
			base.gameObject.SetActive(false);
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndWelcomeScreenSnapshot, STOP_MODE.ALLOWFADEOUT);
		};
		this.bodyText.text = UI.DEVELOPMENTBUILDS.ALPHA.LOADING.BODY;
	}

	// Token: 0x06006145 RID: 24901 RVA: 0x0023C699 File Offset: 0x0023A899
	private void OnEnable()
	{
		this.confirmButton.GetComponent<LayoutElement>();
		this.confirmButton.GetComponentInChildren<LocText>();
	}

	// Token: 0x06006146 RID: 24902 RVA: 0x0023C6B3 File Offset: 0x0023A8B3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (!DlcManager.IsExpansion1Active())
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndWelcomeScreenSnapshot);
	}

	// Token: 0x04004303 RID: 17155
	public KButton forumButton;

	// Token: 0x04004304 RID: 17156
	public KButton confirmButton;

	// Token: 0x04004305 RID: 17157
	public LocText bodyText;

	// Token: 0x04004306 RID: 17158
	public bool previewInEditor;
}
