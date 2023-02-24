using System;
using System.Collections;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000BFF RID: 3071
public class SpeedControlScreen : KScreen
{
	// Token: 0x170006B8 RID: 1720
	// (get) Token: 0x06006121 RID: 24865 RVA: 0x0023BC51 File Offset: 0x00239E51
	// (set) Token: 0x06006122 RID: 24866 RVA: 0x0023BC58 File Offset: 0x00239E58
	public static SpeedControlScreen Instance { get; private set; }

	// Token: 0x06006123 RID: 24867 RVA: 0x0023BC60 File Offset: 0x00239E60
	public static void DestroyInstance()
	{
		SpeedControlScreen.Instance = null;
	}

	// Token: 0x170006B9 RID: 1721
	// (get) Token: 0x06006124 RID: 24868 RVA: 0x0023BC68 File Offset: 0x00239E68
	public bool IsPaused
	{
		get
		{
			return this.pauseCount > 0;
		}
	}

	// Token: 0x06006125 RID: 24869 RVA: 0x0023BC74 File Offset: 0x00239E74
	protected override void OnPrefabInit()
	{
		SpeedControlScreen.Instance = this;
		this.pauseButton = this.pauseButtonWidget.GetComponent<KToggle>();
		this.slowButton = this.speedButtonWidget_slow.GetComponent<KToggle>();
		this.mediumButton = this.speedButtonWidget_medium.GetComponent<KToggle>();
		this.fastButton = this.speedButtonWidget_fast.GetComponent<KToggle>();
		KToggle[] array = new KToggle[] { this.pauseButton, this.slowButton, this.mediumButton, this.fastButton };
		for (int i = 0; i < array.Length; i++)
		{
			array[i].soundPlayer.Enabled = false;
		}
		this.slowButton.onClick += delegate
		{
			this.PlaySpeedChangeSound(1f);
			this.SetSpeed(0);
		};
		this.mediumButton.onClick += delegate
		{
			this.PlaySpeedChangeSound(2f);
			this.SetSpeed(1);
		};
		this.fastButton.onClick += delegate
		{
			this.PlaySpeedChangeSound(3f);
			this.SetSpeed(2);
		};
		this.pauseButton.onClick += delegate
		{
			this.TogglePause(true);
		};
		this.speedButtonWidget_slow.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_SLOW, global::Action.CycleSpeed), this.TooltipTextStyle);
		this.speedButtonWidget_medium.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_MEDIUM, global::Action.CycleSpeed), this.TooltipTextStyle);
		this.speedButtonWidget_fast.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_FAST, global::Action.CycleSpeed), this.TooltipTextStyle);
		this.playButtonWidget.GetComponent<KButton>().onClick += delegate
		{
			this.TogglePause(true);
		};
		KInputManager.InputChange.AddListener(new UnityAction(this.ResetToolTip));
	}

	// Token: 0x06006126 RID: 24870 RVA: 0x0023BE15 File Offset: 0x0023A015
	protected override void OnSpawn()
	{
		if (SaveGame.Instance != null)
		{
			this.speed = SaveGame.Instance.GetSpeed();
			this.SetSpeed(this.speed);
		}
		base.OnSpawn();
		this.OnChanged();
	}

	// Token: 0x06006127 RID: 24871 RVA: 0x0023BE4C File Offset: 0x0023A04C
	protected override void OnForcedCleanUp()
	{
		KInputManager.InputChange.RemoveListener(new UnityAction(this.ResetToolTip));
		base.OnForcedCleanUp();
	}

	// Token: 0x06006128 RID: 24872 RVA: 0x0023BE6A File Offset: 0x0023A06A
	public int GetSpeed()
	{
		return this.speed;
	}

	// Token: 0x06006129 RID: 24873 RVA: 0x0023BE74 File Offset: 0x0023A074
	public void SetSpeed(int Speed)
	{
		this.speed = Speed % 3;
		switch (this.speed)
		{
		case 0:
			this.slowButton.Select();
			this.slowButton.isOn = true;
			this.mediumButton.isOn = false;
			this.fastButton.isOn = false;
			break;
		case 1:
			this.mediumButton.Select();
			this.slowButton.isOn = false;
			this.mediumButton.isOn = true;
			this.fastButton.isOn = false;
			break;
		case 2:
			this.fastButton.Select();
			this.slowButton.isOn = false;
			this.mediumButton.isOn = false;
			this.fastButton.isOn = true;
			break;
		}
		this.OnSpeedChange();
	}

	// Token: 0x0600612A RID: 24874 RVA: 0x0023BF3F File Offset: 0x0023A13F
	public void ToggleRidiculousSpeed()
	{
		if (this.ultraSpeed == 3f)
		{
			this.ultraSpeed = 10f;
		}
		else
		{
			this.ultraSpeed = 3f;
		}
		this.speed = 2;
		this.OnChanged();
	}

	// Token: 0x0600612B RID: 24875 RVA: 0x0023BF73 File Offset: 0x0023A173
	public void TogglePause(bool playsound = true)
	{
		if (this.IsPaused)
		{
			this.Unpause(playsound);
			return;
		}
		this.Pause(playsound, false);
	}

	// Token: 0x0600612C RID: 24876 RVA: 0x0023BF90 File Offset: 0x0023A190
	public void ResetToolTip()
	{
		this.speedButtonWidget_slow.GetComponent<ToolTip>().ClearMultiStringTooltip();
		this.speedButtonWidget_medium.GetComponent<ToolTip>().ClearMultiStringTooltip();
		this.speedButtonWidget_fast.GetComponent<ToolTip>().ClearMultiStringTooltip();
		this.speedButtonWidget_slow.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_SLOW, global::Action.CycleSpeed), this.TooltipTextStyle);
		this.speedButtonWidget_medium.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_MEDIUM, global::Action.CycleSpeed), this.TooltipTextStyle);
		this.speedButtonWidget_fast.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_FAST, global::Action.CycleSpeed), this.TooltipTextStyle);
		if (this.pauseButton.isOn)
		{
			this.pauseButtonWidget.GetComponent<ToolTip>().ClearMultiStringTooltip();
			this.pauseButtonWidget.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.UNPAUSE, global::Action.TogglePause), this.TooltipTextStyle);
			return;
		}
		this.pauseButtonWidget.GetComponent<ToolTip>().ClearMultiStringTooltip();
		this.pauseButtonWidget.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.PAUSE, global::Action.TogglePause), this.TooltipTextStyle);
	}

	// Token: 0x0600612D RID: 24877 RVA: 0x0023C0C0 File Offset: 0x0023A2C0
	public void Pause(bool playSound = true, bool isCrashed = false)
	{
		this.pauseCount++;
		if (this.pauseCount == 1)
		{
			if (playSound)
			{
				if (isCrashed)
				{
					KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Crash_Screen", false));
				}
				else
				{
					KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Speed_Pause", false));
				}
				if (SoundListenerController.Instance != null)
				{
					SoundListenerController.Instance.SetLoopingVolume(0f);
				}
			}
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().SpeedPausedMigrated);
			MusicManager.instance.SetDynamicMusicPaused();
			this.pauseButtonWidget.GetComponent<ToolTip>().ClearMultiStringTooltip();
			this.pauseButtonWidget.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.UNPAUSE, global::Action.TogglePause), this.TooltipTextStyle);
			this.pauseButton.isOn = true;
			this.OnPause();
		}
	}

	// Token: 0x0600612E RID: 24878 RVA: 0x0023C194 File Offset: 0x0023A394
	public void Unpause(bool playSound = true)
	{
		this.pauseCount = Mathf.Max(0, this.pauseCount - 1);
		if (this.pauseCount == 0)
		{
			if (playSound)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Speed_Unpause", false));
				if (SoundListenerController.Instance != null)
				{
					SoundListenerController.Instance.SetLoopingVolume(1f);
				}
			}
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().SpeedPausedMigrated, STOP_MODE.ALLOWFADEOUT);
			MusicManager.instance.SetDynamicMusicUnpaused();
			this.pauseButtonWidget.GetComponent<ToolTip>().ClearMultiStringTooltip();
			this.pauseButtonWidget.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.PAUSE, global::Action.TogglePause), this.TooltipTextStyle);
			this.pauseButton.isOn = false;
			this.SetSpeed(this.speed);
			this.OnPlay();
		}
	}

	// Token: 0x0600612F RID: 24879 RVA: 0x0023C264 File Offset: 0x0023A464
	private void OnPause()
	{
		this.OnChanged();
	}

	// Token: 0x06006130 RID: 24880 RVA: 0x0023C26C File Offset: 0x0023A46C
	private void OnPlay()
	{
		this.OnChanged();
	}

	// Token: 0x06006131 RID: 24881 RVA: 0x0023C274 File Offset: 0x0023A474
	public void OnSpeedChange()
	{
		if (Game.IsQuitting())
		{
			return;
		}
		this.OnChanged();
	}

	// Token: 0x06006132 RID: 24882 RVA: 0x0023C284 File Offset: 0x0023A484
	private void OnChanged()
	{
		if (this.IsPaused)
		{
			Time.timeScale = 0f;
			return;
		}
		if (this.speed == 0)
		{
			Time.timeScale = this.normalSpeed;
			return;
		}
		if (this.speed == 1)
		{
			Time.timeScale = this.fastSpeed;
			return;
		}
		if (this.speed == 2)
		{
			Time.timeScale = this.ultraSpeed;
		}
	}

	// Token: 0x06006133 RID: 24883 RVA: 0x0023C2E4 File Offset: 0x0023A4E4
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.TogglePause))
		{
			this.TogglePause(true);
			return;
		}
		if (e.TryConsume(global::Action.CycleSpeed))
		{
			this.PlaySpeedChangeSound((float)((this.speed + 1) % 3 + 1));
			this.SetSpeed(this.speed + 1);
			this.OnSpeedChange();
			return;
		}
		if (e.TryConsume(global::Action.SpeedUp))
		{
			this.speed++;
			this.speed = Math.Min(this.speed, 2);
			this.SetSpeed(this.speed);
			return;
		}
		if (e.TryConsume(global::Action.SlowDown))
		{
			this.speed--;
			this.speed = Math.Max(this.speed, 0);
			this.SetSpeed(this.speed);
		}
	}

	// Token: 0x06006134 RID: 24884 RVA: 0x0023C3A4 File Offset: 0x0023A5A4
	private void PlaySpeedChangeSound(float speed)
	{
		string sound = GlobalAssets.GetSound("Speed_Change", false);
		if (sound != null)
		{
			EventInstance eventInstance = SoundEvent.BeginOneShot(sound, Vector3.zero, 1f, false);
			eventInstance.setParameterByName("Speed", speed, false);
			SoundEvent.EndOneShot(eventInstance);
		}
	}

	// Token: 0x06006135 RID: 24885 RVA: 0x0023C3EC File Offset: 0x0023A5EC
	public void DebugStepFrame()
	{
		DebugUtil.LogArgs(new object[] { string.Format("Stepping one frame {0} ({1})", GameClock.Instance.GetTime(), GameClock.Instance.GetTime() / 600f) });
		this.stepTime = Time.time;
		this.Unpause(false);
		base.StartCoroutine(this.DebugStepFrameDelay());
	}

	// Token: 0x06006136 RID: 24886 RVA: 0x0023C454 File Offset: 0x0023A654
	private IEnumerator DebugStepFrameDelay()
	{
		yield return null;
		DebugUtil.LogArgs(new object[]
		{
			"Stepped one frame",
			Time.time - this.stepTime,
			"seconds"
		});
		this.Pause(false, false);
		yield break;
	}

	// Token: 0x040042EE RID: 17134
	public GameObject playButtonWidget;

	// Token: 0x040042EF RID: 17135
	public GameObject pauseButtonWidget;

	// Token: 0x040042F0 RID: 17136
	public Image playIcon;

	// Token: 0x040042F1 RID: 17137
	public Image pauseIcon;

	// Token: 0x040042F2 RID: 17138
	[SerializeField]
	private TextStyleSetting TooltipTextStyle;

	// Token: 0x040042F3 RID: 17139
	public GameObject speedButtonWidget_slow;

	// Token: 0x040042F4 RID: 17140
	public GameObject speedButtonWidget_medium;

	// Token: 0x040042F5 RID: 17141
	public GameObject speedButtonWidget_fast;

	// Token: 0x040042F6 RID: 17142
	public GameObject mainMenuWidget;

	// Token: 0x040042F7 RID: 17143
	public float normalSpeed;

	// Token: 0x040042F8 RID: 17144
	public float fastSpeed;

	// Token: 0x040042F9 RID: 17145
	public float ultraSpeed;

	// Token: 0x040042FA RID: 17146
	private KToggle pauseButton;

	// Token: 0x040042FB RID: 17147
	private KToggle slowButton;

	// Token: 0x040042FC RID: 17148
	private KToggle mediumButton;

	// Token: 0x040042FD RID: 17149
	private KToggle fastButton;

	// Token: 0x040042FE RID: 17150
	private int speed;

	// Token: 0x040042FF RID: 17151
	private int pauseCount;

	// Token: 0x04004301 RID: 17153
	private float stepTime;
}
