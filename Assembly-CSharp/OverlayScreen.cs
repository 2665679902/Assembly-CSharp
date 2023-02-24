using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000A15 RID: 2581
[AddComponentMenu("KMonoBehaviour/scripts/OverlayScreen")]
public class OverlayScreen : KMonoBehaviour
{
	// Token: 0x170005C5 RID: 1477
	// (get) Token: 0x06004DCD RID: 19917 RVA: 0x001B77E6 File Offset: 0x001B59E6
	public HashedString mode
	{
		get
		{
			return this.currentModeInfo.mode.ViewMode();
		}
	}

	// Token: 0x06004DCE RID: 19918 RVA: 0x001B77F8 File Offset: 0x001B59F8
	protected override void OnPrefabInit()
	{
		global::Debug.Assert(OverlayScreen.Instance == null);
		OverlayScreen.Instance = this;
		this.powerLabelParent = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>();
	}

	// Token: 0x06004DCF RID: 19919 RVA: 0x001B7825 File Offset: 0x001B5A25
	protected override void OnLoadLevel()
	{
		this.harvestableNotificationPrefab = null;
		this.powerLabelParent = null;
		OverlayScreen.Instance = null;
		OverlayModes.Mode.Clear();
		this.modeInfos = null;
		this.currentModeInfo = default(OverlayScreen.ModeInfo);
		base.OnLoadLevel();
	}

	// Token: 0x06004DD0 RID: 19920 RVA: 0x001B785C File Offset: 0x001B5A5C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.techViewSound = KFMOD.CreateInstance(this.techViewSoundPath);
		this.techViewSoundPlaying = false;
		Shader.SetGlobalVector("_OverlayParams", Vector4.zero);
		this.RegisterModes();
		this.currentModeInfo = this.modeInfos[OverlayModes.None.ID];
	}

	// Token: 0x06004DD1 RID: 19921 RVA: 0x001B78B4 File Offset: 0x001B5AB4
	private void RegisterModes()
	{
		this.modeInfos.Clear();
		OverlayModes.None none = new OverlayModes.None();
		this.RegisterMode(none);
		this.RegisterMode(new OverlayModes.Oxygen());
		this.RegisterMode(new OverlayModes.Power(this.powerLabelParent, this.powerLabelPrefab, this.batUIPrefab, this.powerLabelOffset, this.batteryUIOffset, this.batteryUITransformerOffset, this.batteryUISmallTransformerOffset));
		this.RegisterMode(new OverlayModes.Temperature());
		this.RegisterMode(new OverlayModes.ThermalConductivity());
		this.RegisterMode(new OverlayModes.Light());
		this.RegisterMode(new OverlayModes.LiquidConduits());
		this.RegisterMode(new OverlayModes.GasConduits());
		this.RegisterMode(new OverlayModes.Decor());
		this.RegisterMode(new OverlayModes.Disease(this.powerLabelParent, this.diseaseOverlayPrefab));
		this.RegisterMode(new OverlayModes.Crop(this.powerLabelParent, this.harvestableNotificationPrefab));
		this.RegisterMode(new OverlayModes.Harvest());
		this.RegisterMode(new OverlayModes.Priorities());
		this.RegisterMode(new OverlayModes.HeatFlow());
		this.RegisterMode(new OverlayModes.Rooms());
		this.RegisterMode(new OverlayModes.Suit(this.powerLabelParent, this.suitOverlayPrefab));
		this.RegisterMode(new OverlayModes.Logic(this.logicModeUIPrefab));
		this.RegisterMode(new OverlayModes.SolidConveyor());
		this.RegisterMode(new OverlayModes.TileMode());
		this.RegisterMode(new OverlayModes.Radiation());
	}

	// Token: 0x06004DD2 RID: 19922 RVA: 0x001B7A00 File Offset: 0x001B5C00
	private void RegisterMode(OverlayModes.Mode mode)
	{
		this.modeInfos[mode.ViewMode()] = new OverlayScreen.ModeInfo
		{
			mode = mode
		};
	}

	// Token: 0x06004DD3 RID: 19923 RVA: 0x001B7A2F File Offset: 0x001B5C2F
	private void LateUpdate()
	{
		this.currentModeInfo.mode.Update();
	}

	// Token: 0x06004DD4 RID: 19924 RVA: 0x001B7A44 File Offset: 0x001B5C44
	public void ToggleOverlay(HashedString newMode, bool allowSound = true)
	{
		bool flag = allowSound && !(this.currentModeInfo.mode.ViewMode() == newMode);
		if (newMode != OverlayModes.None.ID)
		{
			ManagementMenu.Instance.CloseAll();
		}
		this.currentModeInfo.mode.Disable();
		if (newMode != this.currentModeInfo.mode.ViewMode() && newMode == OverlayModes.None.ID)
		{
			ManagementMenu.Instance.CloseAll();
		}
		SimDebugView.Instance.SetMode(newMode);
		if (!this.modeInfos.TryGetValue(newMode, out this.currentModeInfo))
		{
			this.currentModeInfo = this.modeInfos[OverlayModes.None.ID];
		}
		this.currentModeInfo.mode.Enable();
		if (flag)
		{
			this.UpdateOverlaySounds();
		}
		if (OverlayModes.None.ID == this.currentModeInfo.mode.ViewMode())
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().TechFilterOnMigrated, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			MusicManager.instance.SetDynamicMusicOverlayInactive();
			this.techViewSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			this.techViewSoundPlaying = false;
		}
		else if (!this.techViewSoundPlaying)
		{
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().TechFilterOnMigrated);
			MusicManager.instance.SetDynamicMusicOverlayActive();
			this.techViewSound.start();
			this.techViewSoundPlaying = true;
		}
		if (this.OnOverlayChanged != null)
		{
			this.OnOverlayChanged(this.currentModeInfo.mode.ViewMode());
		}
		this.ActivateLegend();
	}

	// Token: 0x06004DD5 RID: 19925 RVA: 0x001B7BCB File Offset: 0x001B5DCB
	private void ActivateLegend()
	{
		if (OverlayLegend.Instance == null)
		{
			return;
		}
		OverlayLegend.Instance.SetLegend(this.currentModeInfo.mode, false);
	}

	// Token: 0x06004DD6 RID: 19926 RVA: 0x001B7BF1 File Offset: 0x001B5DF1
	public void Refresh()
	{
		this.LateUpdate();
	}

	// Token: 0x06004DD7 RID: 19927 RVA: 0x001B7BF9 File Offset: 0x001B5DF9
	public HashedString GetMode()
	{
		if (this.currentModeInfo.mode == null)
		{
			return OverlayModes.None.ID;
		}
		return this.currentModeInfo.mode.ViewMode();
	}

	// Token: 0x06004DD8 RID: 19928 RVA: 0x001B7C20 File Offset: 0x001B5E20
	private void UpdateOverlaySounds()
	{
		string text = this.currentModeInfo.mode.GetSoundName();
		if (text != "")
		{
			text = GlobalAssets.GetSound(text, false);
			KMonoBehaviour.PlaySound(text);
		}
	}

	// Token: 0x0400334C RID: 13132
	public static HashSet<Tag> WireIDs = new HashSet<Tag>();

	// Token: 0x0400334D RID: 13133
	public static HashSet<Tag> GasVentIDs = new HashSet<Tag>();

	// Token: 0x0400334E RID: 13134
	public static HashSet<Tag> LiquidVentIDs = new HashSet<Tag>();

	// Token: 0x0400334F RID: 13135
	public static HashSet<Tag> HarvestableIDs = new HashSet<Tag>();

	// Token: 0x04003350 RID: 13136
	public static HashSet<Tag> DiseaseIDs = new HashSet<Tag>();

	// Token: 0x04003351 RID: 13137
	public static HashSet<Tag> SuitIDs = new HashSet<Tag>();

	// Token: 0x04003352 RID: 13138
	public static HashSet<Tag> SolidConveyorIDs = new HashSet<Tag>();

	// Token: 0x04003353 RID: 13139
	public static HashSet<Tag> RadiationIDs = new HashSet<Tag>();

	// Token: 0x04003354 RID: 13140
	[SerializeField]
	public EventReference techViewSoundPath;

	// Token: 0x04003355 RID: 13141
	private EventInstance techViewSound;

	// Token: 0x04003356 RID: 13142
	private bool techViewSoundPlaying;

	// Token: 0x04003357 RID: 13143
	public static OverlayScreen Instance;

	// Token: 0x04003358 RID: 13144
	[Header("Power")]
	[SerializeField]
	private Canvas powerLabelParent;

	// Token: 0x04003359 RID: 13145
	[SerializeField]
	private LocText powerLabelPrefab;

	// Token: 0x0400335A RID: 13146
	[SerializeField]
	private BatteryUI batUIPrefab;

	// Token: 0x0400335B RID: 13147
	[SerializeField]
	private Vector3 powerLabelOffset;

	// Token: 0x0400335C RID: 13148
	[SerializeField]
	private Vector3 batteryUIOffset;

	// Token: 0x0400335D RID: 13149
	[SerializeField]
	private Vector3 batteryUITransformerOffset;

	// Token: 0x0400335E RID: 13150
	[SerializeField]
	private Vector3 batteryUISmallTransformerOffset;

	// Token: 0x0400335F RID: 13151
	[SerializeField]
	private Color consumerColour;

	// Token: 0x04003360 RID: 13152
	[SerializeField]
	private Color generatorColour;

	// Token: 0x04003361 RID: 13153
	[SerializeField]
	private Color buildingDisabledColour = Color.gray;

	// Token: 0x04003362 RID: 13154
	[Header("Circuits")]
	[SerializeField]
	private Color32 circuitUnpoweredColour;

	// Token: 0x04003363 RID: 13155
	[SerializeField]
	private Color32 circuitSafeColour;

	// Token: 0x04003364 RID: 13156
	[SerializeField]
	private Color32 circuitStrainingColour;

	// Token: 0x04003365 RID: 13157
	[SerializeField]
	private Color32 circuitOverloadingColour;

	// Token: 0x04003366 RID: 13158
	[Header("Crops")]
	[SerializeField]
	private GameObject harvestableNotificationPrefab;

	// Token: 0x04003367 RID: 13159
	[Header("Disease")]
	[SerializeField]
	private GameObject diseaseOverlayPrefab;

	// Token: 0x04003368 RID: 13160
	[Header("Suit")]
	[SerializeField]
	private GameObject suitOverlayPrefab;

	// Token: 0x04003369 RID: 13161
	[Header("ToolTip")]
	[SerializeField]
	private TextStyleSetting TooltipHeader;

	// Token: 0x0400336A RID: 13162
	[SerializeField]
	private TextStyleSetting TooltipDescription;

	// Token: 0x0400336B RID: 13163
	[Header("Logic")]
	[SerializeField]
	private LogicModeUI logicModeUIPrefab;

	// Token: 0x0400336C RID: 13164
	public Action<HashedString> OnOverlayChanged;

	// Token: 0x0400336D RID: 13165
	private OverlayScreen.ModeInfo currentModeInfo;

	// Token: 0x0400336E RID: 13166
	private Dictionary<HashedString, OverlayScreen.ModeInfo> modeInfos = new Dictionary<HashedString, OverlayScreen.ModeInfo>();

	// Token: 0x02001848 RID: 6216
	private struct ModeInfo
	{
		// Token: 0x04006FF1 RID: 28657
		public OverlayModes.Mode mode;
	}
}
