using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A22 RID: 2594
public class WattsonMessage : KScreen
{
	// Token: 0x06004E83 RID: 20099 RVA: 0x001BE39B File Offset: 0x001BC59B
	public override float GetSortKey()
	{
		return 8f;
	}

	// Token: 0x06004E84 RID: 20100 RVA: 0x001BE3A4 File Offset: 0x001BC5A4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Game.Instance.Subscribe(-122303817, new Action<object>(this.OnNewBaseCreated));
		if (DlcManager.IsExpansion1Active())
		{
			this.message.SetText(UI.WELCOMEMESSAGEBODY_SPACEDOUT);
			return;
		}
		this.message.SetText(UI.WELCOMEMESSAGEBODY);
	}

	// Token: 0x06004E85 RID: 20101 RVA: 0x001BE405 File Offset: 0x001BC605
	private IEnumerator ExpandPanel()
	{
		yield return SequenceUtil.WaitForSecondsRealtime(0.2f);
		float height = 0f;
		while (height < 299f)
		{
			height = Mathf.Lerp(this.dialog.rectTransform().sizeDelta.y, 300f, Time.unscaledDeltaTime * 15f);
			this.dialog.rectTransform().sizeDelta = new Vector2(this.dialog.rectTransform().sizeDelta.x, height);
			yield return 0;
		}
		yield return null;
		yield break;
	}

	// Token: 0x06004E86 RID: 20102 RVA: 0x001BE414 File Offset: 0x001BC614
	private IEnumerator CollapsePanel()
	{
		float height = 300f;
		while (height > 1f)
		{
			height = Mathf.Lerp(this.dialog.rectTransform().sizeDelta.y, 0f, Time.unscaledDeltaTime * 15f);
			this.dialog.rectTransform().sizeDelta = new Vector2(this.dialog.rectTransform().sizeDelta.x, height);
			yield return 0;
		}
		this.Deactivate();
		yield return null;
		yield break;
	}

	// Token: 0x06004E87 RID: 20103 RVA: 0x001BE424 File Offset: 0x001BC624
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.hideScreensWhileActive.Add(NotificationScreen.Instance);
		this.hideScreensWhileActive.Add(OverlayMenu.Instance);
		if (PlanScreen.Instance != null)
		{
			this.hideScreensWhileActive.Add(PlanScreen.Instance);
		}
		if (BuildMenu.Instance != null)
		{
			this.hideScreensWhileActive.Add(BuildMenu.Instance);
		}
		this.hideScreensWhileActive.Add(ManagementMenu.Instance);
		this.hideScreensWhileActive.Add(ToolMenu.Instance);
		this.hideScreensWhileActive.Add(ToolMenu.Instance.PriorityScreen);
		this.hideScreensWhileActive.Add(PinnedResourcesPanel.Instance);
		this.hideScreensWhileActive.Add(TopLeftControlScreen.Instance);
		this.hideScreensWhileActive.Add(global::DateTime.Instance);
		this.hideScreensWhileActive.Add(BuildWatermark.Instance);
		this.hideScreensWhileActive.Add(BuildWatermark.Instance);
		this.hideScreensWhileActive.Add(ColonyDiagnosticScreen.Instance);
		if (WorldSelector.Instance != null)
		{
			this.hideScreensWhileActive.Add(WorldSelector.Instance);
		}
		foreach (KScreen kscreen in this.hideScreensWhileActive)
		{
			kscreen.Show(false);
		}
	}

	// Token: 0x06004E88 RID: 20104 RVA: 0x001BE588 File Offset: 0x001BC788
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

	// Token: 0x06004E89 RID: 20105 RVA: 0x001BE5E0 File Offset: 0x001BC7E0
	protected override void OnActivate()
	{
		global::Debug.Log("WattsonMessage OnActivate");
		base.OnActivate();
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().NewBaseSetupSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().IntroNIS);
		AudioMixer.instance.activeNIS = true;
		this.button.onClick += delegate
		{
			base.StartCoroutine(this.CollapsePanel());
		};
		this.dialog.GetComponent<KScreen>().Show(false);
		this.startFade = false;
		GameObject telepad = GameUtil.GetTelepad(ClusterManager.Instance.GetStartWorld().id);
		if (telepad != null)
		{
			KAnimControllerBase kac = telepad.GetComponent<KAnimControllerBase>();
			kac.Play(WattsonMessage.WorkLoopAnims, KAnim.PlayMode.Loop);
			for (int i = 0; i < Components.LiveMinionIdentities.Count; i++)
			{
				int idx = i + 1;
				MinionIdentity minionIdentity = Components.LiveMinionIdentities[i];
				minionIdentity.gameObject.transform.SetPosition(new Vector3(telepad.transform.GetPosition().x + (float)idx - 1.5f, telepad.transform.GetPosition().y, minionIdentity.gameObject.transform.GetPosition().z));
				GameObject gameObject = minionIdentity.gameObject;
				ChoreProvider chore_provider = gameObject.GetComponent<ChoreProvider>();
				EmoteChore chorePre = new EmoteChore(chore_provider, Db.Get().ChoreTypes.EmoteHighPriority, "anim_interacts_portal_kanim", new HashedString[] { "portalbirth_pre_" + idx.ToString() }, KAnim.PlayMode.Loop, false);
				UIScheduler.Instance.Schedule("DupeBirth", (float)idx * 0.5f, delegate(object data)
				{
					chorePre.Cancel("Done looping");
					EmoteChore emoteChore = new EmoteChore(chore_provider, Db.Get().ChoreTypes.EmoteHighPriority, "anim_interacts_portal_kanim", new HashedString[] { "portalbirth_" + idx.ToString() }, null);
					emoteChore.onComplete = (Action<Chore>)Delegate.Combine(emoteChore.onComplete, new Action<Chore>(delegate(Chore param)
					{
						this.birthsComplete++;
						if (this.birthsComplete == Components.LiveMinionIdentities.Count - 1 && base.IsActive())
						{
							this.PauseAndShowMessage();
						}
					}));
				}, null, null);
			}
			UIScheduler.Instance.Schedule("Welcome", 6.6f, delegate(object data)
			{
				kac.Play(new HashedString[] { "working_pst", "idle" }, KAnim.PlayMode.Once);
			}, null, null);
			CameraController.Instance.DisableUserCameraControl = true;
		}
		else
		{
			global::Debug.LogWarning("Failed to spawn telepad - does the starting base template lack a 'Headquarters' ?");
			this.PauseAndShowMessage();
		}
		this.scheduleHandles.Add(UIScheduler.Instance.Schedule("GoHome", 0.1f, delegate(object data)
		{
			CameraController.Instance.OrthographicSize = TuningData<WattsonMessage.Tuning>.Get().initialOrthographicSize;
			CameraController.Instance.CameraGoHome(0.5f);
			this.startFade = true;
			MusicManager.instance.PlaySong("Music_WattsonMessage", false);
		}, null, null));
	}

	// Token: 0x06004E8A RID: 20106 RVA: 0x001BE848 File Offset: 0x001BCA48
	protected void PauseAndShowMessage()
	{
		SpeedControlScreen.Instance.Pause(false, false);
		base.StartCoroutine(this.ExpandPanel());
		KFMOD.PlayUISound(this.dialogSound);
		this.dialog.GetComponent<KScreen>().Activate();
		this.dialog.GetComponent<KScreen>().SetShouldFadeIn(true);
		this.dialog.GetComponent<KScreen>().Show(true);
	}

	// Token: 0x06004E8B RID: 20107 RVA: 0x001BE8AC File Offset: 0x001BCAAC
	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().IntroNIS, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.StartPersistentSnapshots();
		MusicManager.instance.StopSong("Music_WattsonMessage", true, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		MusicManager.instance.PlayDynamicMusic();
		AudioMixer.instance.activeNIS = false;
		DemoTimer.Instance.CountdownActive = true;
		SpeedControlScreen.Instance.Unpause(false);
		CameraController.Instance.DisableUserCameraControl = false;
		foreach (SchedulerHandle schedulerHandle in this.scheduleHandles)
		{
			schedulerHandle.ClearScheduler();
		}
		UIScheduler.Instance.Schedule("fadeInUI", 0.5f, delegate(object d)
		{
			foreach (KScreen kscreen in this.hideScreensWhileActive)
			{
				if (!(kscreen == null))
				{
					kscreen.SetShouldFadeIn(true);
					kscreen.Show(true);
				}
			}
			CameraController.Instance.SetMaxOrthographicSize(20f);
			Game.Instance.StartDelayedInitialSave();
			UIScheduler.Instance.Schedule("InitialScreenshot", 1f, delegate(object data)
			{
				Game.Instance.timelapser.InitialScreenshot();
			}, null, null);
			GameScheduler.Instance.Schedule("BasicTutorial", 1.5f, delegate(object data)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Basics, true);
			}, null, null);
			GameScheduler.Instance.Schedule("WelcomeTutorial", 2f, delegate(object data)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Welcome, true);
			}, null, null);
			GameScheduler.Instance.Schedule("DiggingTutorial", 420f, delegate(object data)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Digging, true);
			}, null, null);
		}, null, null);
		Game.Instance.SetGameStarted();
		if (TopLeftControlScreen.Instance != null)
		{
			TopLeftControlScreen.Instance.RefreshName();
		}
	}

	// Token: 0x06004E8C RID: 20108 RVA: 0x001BE9B0 File Offset: 0x001BCBB0
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			CameraController.Instance.CameraGoHome(2f);
			this.Deactivate();
		}
		e.Consumed = true;
	}

	// Token: 0x06004E8D RID: 20109 RVA: 0x001BE9D7 File Offset: 0x001BCBD7
	public override void OnKeyUp(KButtonEvent e)
	{
		e.Consumed = true;
	}

	// Token: 0x06004E8E RID: 20110 RVA: 0x001BE9E0 File Offset: 0x001BCBE0
	private void OnNewBaseCreated(object data)
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x040033ED RID: 13293
	private const float STARTTIME = 0.1f;

	// Token: 0x040033EE RID: 13294
	private const float ENDTIME = 6.6f;

	// Token: 0x040033EF RID: 13295
	private const float ALPHA_SPEED = 0.01f;

	// Token: 0x040033F0 RID: 13296
	private const float expandedHeight = 300f;

	// Token: 0x040033F1 RID: 13297
	[SerializeField]
	private GameObject dialog;

	// Token: 0x040033F2 RID: 13298
	[SerializeField]
	private RectTransform content;

	// Token: 0x040033F3 RID: 13299
	[SerializeField]
	private LocText message;

	// Token: 0x040033F4 RID: 13300
	[SerializeField]
	private Image bg;

	// Token: 0x040033F5 RID: 13301
	[SerializeField]
	private KButton button;

	// Token: 0x040033F6 RID: 13302
	[SerializeField]
	private EventReference dialogSound;

	// Token: 0x040033F7 RID: 13303
	private List<KScreen> hideScreensWhileActive = new List<KScreen>();

	// Token: 0x040033F8 RID: 13304
	private bool startFade;

	// Token: 0x040033F9 RID: 13305
	private List<SchedulerHandle> scheduleHandles = new List<SchedulerHandle>();

	// Token: 0x040033FA RID: 13306
	private static readonly HashedString[] WorkLoopAnims = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x040033FB RID: 13307
	private int birthsComplete;

	// Token: 0x0200185C RID: 6236
	public class Tuning : TuningData<WattsonMessage.Tuning>
	{
		// Token: 0x0400703F RID: 28735
		public float initialOrthographicSize;
	}
}
