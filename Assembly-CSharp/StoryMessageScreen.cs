using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A1E RID: 2590
public class StoryMessageScreen : KScreen
{
	// Token: 0x170005C7 RID: 1479
	// (set) Token: 0x06004E33 RID: 20019 RVA: 0x001BB8EB File Offset: 0x001B9AEB
	public string title
	{
		set
		{
			this.titleLabel.SetText(value);
		}
	}

	// Token: 0x170005C8 RID: 1480
	// (set) Token: 0x06004E34 RID: 20020 RVA: 0x001BB8F9 File Offset: 0x001B9AF9
	public string body
	{
		set
		{
			this.bodyLabel.SetText(value);
		}
	}

	// Token: 0x06004E35 RID: 20021 RVA: 0x001BB907 File Offset: 0x001B9B07
	public override float GetSortKey()
	{
		return 8f;
	}

	// Token: 0x06004E36 RID: 20022 RVA: 0x001BB90E File Offset: 0x001B9B0E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		StoryMessageScreen.HideInterface(true);
		CameraController.Instance.FadeOut(0.5f, 1f, null);
	}

	// Token: 0x06004E37 RID: 20023 RVA: 0x001BB931 File Offset: 0x001B9B31
	private IEnumerator ExpandPanel()
	{
		this.content.gameObject.SetActive(true);
		yield return SequenceUtil.WaitForSecondsRealtime(0.25f);
		float height = 0f;
		while (height < 299f)
		{
			height = Mathf.Lerp(this.dialog.rectTransform().sizeDelta.y, 300f, Time.unscaledDeltaTime * 15f);
			this.dialog.rectTransform().sizeDelta = new Vector2(this.dialog.rectTransform().sizeDelta.x, height);
			yield return 0;
		}
		CameraController.Instance.FadeOut(0.5f, 1f, null);
		yield return null;
		yield break;
	}

	// Token: 0x06004E38 RID: 20024 RVA: 0x001BB940 File Offset: 0x001B9B40
	private IEnumerator CollapsePanel()
	{
		float height = 300f;
		while (height > 0f)
		{
			height = Mathf.Lerp(this.dialog.rectTransform().sizeDelta.y, -1f, Time.unscaledDeltaTime * 15f);
			this.dialog.rectTransform().sizeDelta = new Vector2(this.dialog.rectTransform().sizeDelta.x, height);
			yield return 0;
		}
		this.content.gameObject.SetActive(false);
		if (this.OnClose != null)
		{
			this.OnClose();
			this.OnClose = null;
		}
		this.Deactivate();
		yield return null;
		yield break;
	}

	// Token: 0x06004E39 RID: 20025 RVA: 0x001BB950 File Offset: 0x001B9B50
	public static void HideInterface(bool hide)
	{
		SelectTool.Instance.Select(null, true);
		NotificationScreen.Instance.Show(!hide);
		OverlayMenu.Instance.Show(!hide);
		if (PlanScreen.Instance != null)
		{
			PlanScreen.Instance.Show(!hide);
		}
		if (BuildMenu.Instance != null)
		{
			BuildMenu.Instance.Show(!hide);
		}
		ManagementMenu.Instance.Show(!hide);
		ToolMenu.Instance.Show(!hide);
		ToolMenu.Instance.PriorityScreen.Show(!hide);
		ColonyDiagnosticScreen.Instance.Show(!hide);
		PinnedResourcesPanel.Instance.Show(!hide);
		TopLeftControlScreen.Instance.Show(!hide);
		if (WorldSelector.Instance != null)
		{
			WorldSelector.Instance.Show(!hide);
		}
		global::DateTime.Instance.Show(!hide);
		if (BuildWatermark.Instance != null)
		{
			BuildWatermark.Instance.Show(!hide);
		}
		PopFXManager.Instance.Show(!hide);
	}

	// Token: 0x06004E3A RID: 20026 RVA: 0x001BBA68 File Offset: 0x001B9C68
	public void Update()
	{
		if (!this.startFade)
		{
			return;
		}
		Color color = this.bg.color;
		color.a -= 0.01f;
		if (color.a <= 0f)
		{
			color.a = 0f;
		}
		this.bg.color = color;
	}

	// Token: 0x06004E3B RID: 20027 RVA: 0x001BBAC0 File Offset: 0x001B9CC0
	protected override void OnActivate()
	{
		base.OnActivate();
		SelectTool.Instance.Select(null, false);
		this.button.onClick += delegate
		{
			base.StartCoroutine(this.CollapsePanel());
		};
		this.dialog.GetComponent<KScreen>().Show(false);
		this.startFade = false;
		CameraController.Instance.DisableUserCameraControl = true;
		KFMOD.PlayUISound(this.dialogSound);
		this.dialog.GetComponent<KScreen>().Activate();
		this.dialog.GetComponent<KScreen>().SetShouldFadeIn(true);
		this.dialog.GetComponent<KScreen>().Show(true);
		MusicManager.instance.PlaySong("Music_Victory_01_Message", false);
		base.StartCoroutine(this.ExpandPanel());
	}

	// Token: 0x06004E3C RID: 20028 RVA: 0x001BBB74 File Offset: 0x001B9D74
	protected override void OnDeactivate()
	{
		base.IsActive();
		base.OnDeactivate();
		MusicManager.instance.StopSong("Music_Victory_01_Message", true, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		if (this.restoreInterfaceOnClose)
		{
			CameraController.Instance.DisableUserCameraControl = false;
			CameraController.Instance.FadeIn(0f, 1f, null);
			StoryMessageScreen.HideInterface(false);
		}
	}

	// Token: 0x06004E3D RID: 20029 RVA: 0x001BBBCD File Offset: 0x001B9DCD
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			base.StartCoroutine(this.CollapsePanel());
		}
		e.Consumed = true;
	}

	// Token: 0x06004E3E RID: 20030 RVA: 0x001BBBEC File Offset: 0x001B9DEC
	public override void OnKeyUp(KButtonEvent e)
	{
		e.Consumed = true;
	}

	// Token: 0x040033AC RID: 13228
	private const float ALPHA_SPEED = 0.01f;

	// Token: 0x040033AD RID: 13229
	[SerializeField]
	private Image bg;

	// Token: 0x040033AE RID: 13230
	[SerializeField]
	private GameObject dialog;

	// Token: 0x040033AF RID: 13231
	[SerializeField]
	private KButton button;

	// Token: 0x040033B0 RID: 13232
	[SerializeField]
	private EventReference dialogSound;

	// Token: 0x040033B1 RID: 13233
	[SerializeField]
	private LocText titleLabel;

	// Token: 0x040033B2 RID: 13234
	[SerializeField]
	private LocText bodyLabel;

	// Token: 0x040033B3 RID: 13235
	private const float expandedHeight = 300f;

	// Token: 0x040033B4 RID: 13236
	[SerializeField]
	private GameObject content;

	// Token: 0x040033B5 RID: 13237
	public bool restoreInterfaceOnClose = true;

	// Token: 0x040033B6 RID: 13238
	public System.Action OnClose;

	// Token: 0x040033B7 RID: 13239
	private bool startFade;
}
