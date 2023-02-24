using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000A12 RID: 2578
public class OverlayMenu : KIconToggleMenu
{
	// Token: 0x06004DBC RID: 19900 RVA: 0x001B7060 File Offset: 0x001B5260
	public static void DestroyInstance()
	{
		OverlayMenu.Instance = null;
	}

	// Token: 0x06004DBD RID: 19901 RVA: 0x001B7068 File Offset: 0x001B5268
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		OverlayMenu.Instance = this;
		this.InitializeToggles();
		base.Setup(this.overlayToggleInfos);
		Game.Instance.Subscribe(1798162660, new Action<object>(this.OnOverlayChanged));
		Game.Instance.Subscribe(-107300940, new Action<object>(this.OnResearchComplete));
		KInputManager.InputChange.AddListener(new UnityAction(this.Refresh));
		base.onSelect += this.OnToggleSelect;
	}

	// Token: 0x06004DBE RID: 19902 RVA: 0x001B70F3 File Offset: 0x001B52F3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RefreshButtons();
	}

	// Token: 0x06004DBF RID: 19903 RVA: 0x001B7101 File Offset: 0x001B5301
	public void Refresh()
	{
		this.RefreshButtons();
	}

	// Token: 0x06004DC0 RID: 19904 RVA: 0x001B710C File Offset: 0x001B530C
	protected override void RefreshButtons()
	{
		base.RefreshButtons();
		if (Research.Instance == null)
		{
			return;
		}
		foreach (KIconToggleMenu.ToggleInfo toggleInfo in this.overlayToggleInfos)
		{
			OverlayMenu.OverlayToggleInfo overlayToggleInfo = (OverlayMenu.OverlayToggleInfo)toggleInfo;
			toggleInfo.toggle.gameObject.SetActive(overlayToggleInfo.IsUnlocked());
			toggleInfo.tooltip = GameUtil.ReplaceHotkeyString(overlayToggleInfo.originalToolTipText, toggleInfo.hotKey);
		}
	}

	// Token: 0x06004DC1 RID: 19905 RVA: 0x001B71A0 File Offset: 0x001B53A0
	private void OnResearchComplete(object data)
	{
		this.RefreshButtons();
	}

	// Token: 0x06004DC2 RID: 19906 RVA: 0x001B71A8 File Offset: 0x001B53A8
	protected override void OnForcedCleanUp()
	{
		KInputManager.InputChange.RemoveListener(new UnityAction(this.Refresh));
		base.OnForcedCleanUp();
	}

	// Token: 0x06004DC3 RID: 19907 RVA: 0x001B71C6 File Offset: 0x001B53C6
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Game.Instance.Unsubscribe(1798162660, new Action<object>(this.OnOverlayChanged));
	}

	// Token: 0x06004DC4 RID: 19908 RVA: 0x001B71E9 File Offset: 0x001B53E9
	private void InitializeToggleGroups()
	{
	}

	// Token: 0x06004DC5 RID: 19909 RVA: 0x001B71EC File Offset: 0x001B53EC
	private void InitializeToggles()
	{
		this.overlayToggleInfos = new List<KIconToggleMenu.ToggleInfo>
		{
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.OXYGEN.BUTTON, "overlay_oxygen", OverlayModes.Oxygen.ID, "", global::Action.Overlay1, UI.TOOLTIPS.OXYGENOVERLAYSTRING, UI.OVERLAYS.OXYGEN.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.ELECTRICAL.BUTTON, "overlay_power", OverlayModes.Power.ID, "", global::Action.Overlay2, UI.TOOLTIPS.POWEROVERLAYSTRING, UI.OVERLAYS.ELECTRICAL.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.TEMPERATURE.BUTTON, "overlay_temperature", OverlayModes.Temperature.ID, "", global::Action.Overlay3, UI.TOOLTIPS.TEMPERATUREOVERLAYSTRING, UI.OVERLAYS.TEMPERATURE.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.TILEMODE.BUTTON, "overlay_materials", OverlayModes.TileMode.ID, "", global::Action.Overlay4, UI.TOOLTIPS.TILEMODE_OVERLAY_STRING, UI.OVERLAYS.TILEMODE.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.LIGHTING.BUTTON, "overlay_lights", OverlayModes.Light.ID, "", global::Action.Overlay5, UI.TOOLTIPS.LIGHTSOVERLAYSTRING, UI.OVERLAYS.LIGHTING.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.LIQUIDPLUMBING.BUTTON, "overlay_liquidvent", OverlayModes.LiquidConduits.ID, "", global::Action.Overlay6, UI.TOOLTIPS.LIQUIDVENTOVERLAYSTRING, UI.OVERLAYS.LIQUIDPLUMBING.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.GASPLUMBING.BUTTON, "overlay_gasvent", OverlayModes.GasConduits.ID, "", global::Action.Overlay7, UI.TOOLTIPS.GASVENTOVERLAYSTRING, UI.OVERLAYS.GASPLUMBING.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.DECOR.BUTTON, "overlay_decor", OverlayModes.Decor.ID, "", global::Action.Overlay8, UI.TOOLTIPS.DECOROVERLAYSTRING, UI.OVERLAYS.DECOR.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.DISEASE.BUTTON, "overlay_disease", OverlayModes.Disease.ID, "", global::Action.Overlay9, UI.TOOLTIPS.DISEASEOVERLAYSTRING, UI.OVERLAYS.DISEASE.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.CROPS.BUTTON, "overlay_farming", OverlayModes.Crop.ID, "", global::Action.Overlay10, UI.TOOLTIPS.CROPS_OVERLAY_STRING, UI.OVERLAYS.CROPS.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.ROOMS.BUTTON, "overlay_rooms", OverlayModes.Rooms.ID, "", global::Action.Overlay11, UI.TOOLTIPS.ROOMSOVERLAYSTRING, UI.OVERLAYS.ROOMS.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.SUIT.BUTTON, "overlay_suit", OverlayModes.Suit.ID, "SuitsOverlay", global::Action.Overlay12, UI.TOOLTIPS.SUITOVERLAYSTRING, UI.OVERLAYS.SUIT.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.LOGIC.BUTTON, "overlay_logic", OverlayModes.Logic.ID, "AutomationOverlay", global::Action.Overlay13, UI.TOOLTIPS.LOGICOVERLAYSTRING, UI.OVERLAYS.LOGIC.BUTTON),
			new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.CONVEYOR.BUTTON, "overlay_conveyor", OverlayModes.SolidConveyor.ID, "ConveyorOverlay", global::Action.Overlay14, UI.TOOLTIPS.CONVEYOR_OVERLAY_STRING, UI.OVERLAYS.CONVEYOR.BUTTON)
		};
		if (Sim.IsRadiationEnabled())
		{
			this.overlayToggleInfos.Add(new OverlayMenu.OverlayToggleInfo(UI.OVERLAYS.RADIATION.BUTTON, "overlay_radiation", OverlayModes.Radiation.ID, "", global::Action.Overlay15, UI.TOOLTIPS.RADIATIONOVERLAYSTRING, UI.OVERLAYS.RADIATION.BUTTON));
		}
	}

	// Token: 0x06004DC6 RID: 19910 RVA: 0x001B758C File Offset: 0x001B578C
	private void OnToggleSelect(KIconToggleMenu.ToggleInfo toggle_info)
	{
		if (SimDebugView.Instance.GetMode() == ((OverlayMenu.OverlayToggleInfo)toggle_info).simView)
		{
			OverlayScreen.Instance.ToggleOverlay(OverlayModes.None.ID, true);
			return;
		}
		if (((OverlayMenu.OverlayToggleInfo)toggle_info).IsUnlocked())
		{
			OverlayScreen.Instance.ToggleOverlay(((OverlayMenu.OverlayToggleInfo)toggle_info).simView, true);
		}
	}

	// Token: 0x06004DC7 RID: 19911 RVA: 0x001B75EC File Offset: 0x001B57EC
	private void OnOverlayChanged(object overlay_data)
	{
		HashedString hashedString = (HashedString)overlay_data;
		for (int i = 0; i < this.overlayToggleInfos.Count; i++)
		{
			this.overlayToggleInfos[i].toggle.isOn = ((OverlayMenu.OverlayToggleInfo)this.overlayToggleInfos[i]).simView == hashedString;
		}
	}

	// Token: 0x06004DC8 RID: 19912 RVA: 0x001B7648 File Offset: 0x001B5848
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (OverlayScreen.Instance.GetMode() != OverlayModes.None.ID && e.TryConsume(global::Action.Escape))
		{
			OverlayScreen.Instance.ToggleOverlay(OverlayModes.None.ID, true);
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06004DC9 RID: 19913 RVA: 0x001B769C File Offset: 0x001B589C
	public override void OnKeyUp(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (OverlayScreen.Instance.GetMode() != OverlayModes.None.ID && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			OverlayScreen.Instance.ToggleOverlay(OverlayModes.None.ID, true);
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x04003339 RID: 13113
	public static OverlayMenu Instance;

	// Token: 0x0400333A RID: 13114
	private List<KIconToggleMenu.ToggleInfo> overlayToggleInfos;

	// Token: 0x0400333B RID: 13115
	private UnityAction inputChangeReceiver;

	// Token: 0x0200182A RID: 6186
	private class OverlayToggleGroup : KIconToggleMenu.ToggleInfo
	{
		// Token: 0x06008D04 RID: 36100 RVA: 0x003048A5 File Offset: 0x00302AA5
		public OverlayToggleGroup(string text, string icon_name, List<OverlayMenu.OverlayToggleInfo> toggle_group, string required_tech_item = "", global::Action hot_key = global::Action.NumActions, string tooltip = "", string tooltip_header = "")
			: base(text, icon_name, null, hot_key, tooltip, tooltip_header)
		{
			this.toggleInfoGroup = toggle_group;
		}

		// Token: 0x06008D05 RID: 36101 RVA: 0x003048BD File Offset: 0x00302ABD
		public bool IsUnlocked()
		{
			return DebugHandler.InstantBuildMode || string.IsNullOrEmpty(this.requiredTechItem) || Db.Get().Techs.IsTechItemComplete(this.requiredTechItem);
		}

		// Token: 0x06008D06 RID: 36102 RVA: 0x003048EA File Offset: 0x00302AEA
		public OverlayMenu.OverlayToggleInfo GetActiveToggleInfo()
		{
			return this.toggleInfoGroup[this.activeToggleInfo];
		}

		// Token: 0x04006F58 RID: 28504
		public List<OverlayMenu.OverlayToggleInfo> toggleInfoGroup;

		// Token: 0x04006F59 RID: 28505
		public string requiredTechItem;

		// Token: 0x04006F5A RID: 28506
		[SerializeField]
		private int activeToggleInfo;
	}

	// Token: 0x0200182B RID: 6187
	private class OverlayToggleInfo : KIconToggleMenu.ToggleInfo
	{
		// Token: 0x06008D07 RID: 36103 RVA: 0x003048FD File Offset: 0x00302AFD
		public OverlayToggleInfo(string text, string icon_name, HashedString sim_view, string required_tech_item = "", global::Action hotKey = global::Action.NumActions, string tooltip = "", string tooltip_header = "")
			: base(text, icon_name, null, hotKey, tooltip, tooltip_header)
		{
			this.originalToolTipText = tooltip;
			tooltip = GameUtil.ReplaceHotkeyString(tooltip, hotKey);
			this.simView = sim_view;
			this.requiredTechItem = required_tech_item;
		}

		// Token: 0x06008D08 RID: 36104 RVA: 0x00304930 File Offset: 0x00302B30
		public bool IsUnlocked()
		{
			return DebugHandler.InstantBuildMode || string.IsNullOrEmpty(this.requiredTechItem) || Db.Get().Techs.IsTechItemComplete(this.requiredTechItem);
		}

		// Token: 0x04006F5B RID: 28507
		public HashedString simView;

		// Token: 0x04006F5C RID: 28508
		public string requiredTechItem;

		// Token: 0x04006F5D RID: 28509
		public string originalToolTipText;
	}
}
