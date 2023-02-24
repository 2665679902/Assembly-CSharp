using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000B48 RID: 2888
[AddComponentMenu("KMonoBehaviour/scripts/SplashMessageScreen")]
public class OldVersionMessageScreen : KModalScreen
{
	// Token: 0x0600597B RID: 22907 RVA: 0x00205F34 File Offset: 0x00204134
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.forumButton.onClick += delegate
		{
			App.OpenWebURL("https://forums.kleientertainment.com/forums/topic/140474-previous-update-steam-branch-access/");
		};
		this.confirmButton.onClick += delegate
		{
			base.gameObject.SetActive(false);
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndWelcomeScreenSnapshot, STOP_MODE.ALLOWFADEOUT);
		};
		this.quitButton.onClick += delegate
		{
			App.Quit();
		};
	}

	// Token: 0x0600597C RID: 22908 RVA: 0x00205FB4 File Offset: 0x002041B4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.messageContainer.sizeDelta = new Vector2(Mathf.Max(384f, (float)Screen.width * 0.25f), this.messageContainer.sizeDelta.y);
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndWelcomeScreenSnapshot);
	}

	// Token: 0x04003C70 RID: 15472
	public KButton forumButton;

	// Token: 0x04003C71 RID: 15473
	public KButton confirmButton;

	// Token: 0x04003C72 RID: 15474
	public KButton quitButton;

	// Token: 0x04003C73 RID: 15475
	public LocText bodyText;

	// Token: 0x04003C74 RID: 15476
	public bool previewInEditor;

	// Token: 0x04003C75 RID: 15477
	public RectTransform messageContainer;
}
