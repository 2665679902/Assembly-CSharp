using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000991 RID: 2449
public class StatusItem : Resource
{
	// Token: 0x06004878 RID: 18552 RVA: 0x00196624 File Offset: 0x00194824
	private StatusItem(string id, string composed_prefix)
		: base(id, Strings.Get(composed_prefix + ".NAME"))
	{
		this.composedPrefix = composed_prefix;
		this.tooltipText = Strings.Get(composed_prefix + ".TOOLTIP");
	}

	// Token: 0x06004879 RID: 18553 RVA: 0x00196678 File Offset: 0x00194878
	public StatusItem(string id, string prefix, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, bool showWorldIcon = true, int status_overlays = 129022, Func<string, object, string> resolve_string_callback = null)
		: this(id, "STRINGS." + prefix + ".STATUSITEMS." + id.ToUpper())
	{
		switch (icon_type)
		{
		case StatusItem.IconType.Info:
			icon = "dash";
			break;
		case StatusItem.IconType.Exclamation:
			icon = "status_item_exclamation";
			break;
		}
		this.iconName = icon;
		this.notificationType = notification_type;
		this.sprite = Assets.GetTintedSprite(icon);
		this.iconType = icon_type;
		this.allowMultiples = allow_multiples;
		this.render_overlay = render_overlay;
		this.showShowWorldIcon = showWorldIcon;
		this.status_overlays = status_overlays;
		this.resolveStringCallback = resolve_string_callback;
		if (this.sprite == null)
		{
			global::Debug.LogWarning("Status item '" + id + "' references a missing icon: " + icon);
		}
	}

	// Token: 0x0600487A RID: 18554 RVA: 0x00196730 File Offset: 0x00194930
	public StatusItem(string id, string name, string tooltip, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, int status_overlays = 129022, bool showWorldIcon = true, Func<string, object, string> resolve_string_callback = null)
		: base(id, name)
	{
		switch (icon_type)
		{
		case StatusItem.IconType.Info:
			icon = "dash";
			break;
		case StatusItem.IconType.Exclamation:
			icon = "status_item_exclamation";
			break;
		}
		this.iconName = icon;
		this.notificationType = notification_type;
		this.sprite = Assets.GetTintedSprite(icon);
		this.tooltipText = tooltip;
		this.iconType = icon_type;
		this.allowMultiples = allow_multiples;
		this.render_overlay = render_overlay;
		this.status_overlays = status_overlays;
		this.showShowWorldIcon = showWorldIcon;
		this.resolveStringCallback = resolve_string_callback;
		if (this.sprite == null)
		{
			global::Debug.LogWarning("Status item '" + id + "' references a missing icon: " + icon);
		}
	}

	// Token: 0x0600487B RID: 18555 RVA: 0x001967E4 File Offset: 0x001949E4
	public void AddNotification(string sound_path = null, string notification_text = null, string notification_tooltip = null)
	{
		this.shouldNotify = true;
		if (sound_path == null)
		{
			if (this.notificationType == NotificationType.Bad)
			{
				this.soundPath = "Warning";
			}
			else
			{
				this.soundPath = "Notification";
			}
		}
		else
		{
			this.soundPath = sound_path;
		}
		if (notification_text != null)
		{
			this.notificationText = notification_text;
		}
		else
		{
			DebugUtil.Assert(this.composedPrefix != null, "When adding a notification, either set the status prefix or specify strings!");
			this.notificationText = Strings.Get(this.composedPrefix + ".NOTIFICATION_NAME");
		}
		if (notification_tooltip != null)
		{
			this.notificationTooltipText = notification_tooltip;
			return;
		}
		DebugUtil.Assert(this.composedPrefix != null, "When adding a notification, either set the status prefix or specify strings!");
		this.notificationTooltipText = Strings.Get(this.composedPrefix + ".NOTIFICATION_TOOLTIP");
	}

	// Token: 0x0600487C RID: 18556 RVA: 0x001968A2 File Offset: 0x00194AA2
	public virtual string GetName(object data)
	{
		return this.ResolveString(this.Name, data);
	}

	// Token: 0x0600487D RID: 18557 RVA: 0x001968B1 File Offset: 0x00194AB1
	public virtual string GetTooltip(object data)
	{
		return this.ResolveTooltip(this.tooltipText, data);
	}

	// Token: 0x0600487E RID: 18558 RVA: 0x001968C0 File Offset: 0x00194AC0
	private string ResolveString(string str, object data)
	{
		if (this.resolveStringCallback != null && (data != null || this.resolveStringCallback_shouldStillCallIfDataIsNull))
		{
			return this.resolveStringCallback(str, data);
		}
		return str;
	}

	// Token: 0x0600487F RID: 18559 RVA: 0x001968E4 File Offset: 0x00194AE4
	private string ResolveTooltip(string str, object data)
	{
		if (data != null)
		{
			if (this.resolveTooltipCallback != null)
			{
				return this.resolveTooltipCallback(str, data);
			}
			if (this.resolveStringCallback != null)
			{
				return this.resolveStringCallback(str, data);
			}
		}
		else
		{
			if (this.resolveStringCallback_shouldStillCallIfDataIsNull && this.resolveStringCallback != null)
			{
				return this.resolveStringCallback(str, data);
			}
			if (this.resolveTooltipCallback_shouldStillCallIfDataIsNull && this.resolveTooltipCallback != null)
			{
				return this.resolveTooltipCallback(str, data);
			}
		}
		return str;
	}

	// Token: 0x06004880 RID: 18560 RVA: 0x0019695D File Offset: 0x00194B5D
	public bool ShouldShowIcon()
	{
		return this.iconType == StatusItem.IconType.Custom && this.showShowWorldIcon;
	}

	// Token: 0x06004881 RID: 18561 RVA: 0x00196970 File Offset: 0x00194B70
	public virtual void ShowToolTip(ToolTip tooltip_widget, object data, TextStyleSetting property_style)
	{
		tooltip_widget.ClearMultiStringTooltip();
		string tooltip = this.GetTooltip(data);
		tooltip_widget.AddMultiStringTooltip(tooltip, property_style);
	}

	// Token: 0x06004882 RID: 18562 RVA: 0x00196993 File Offset: 0x00194B93
	public void SetIcon(Image image, object data)
	{
		if (this.sprite == null)
		{
			return;
		}
		image.color = this.sprite.color;
		image.sprite = this.sprite.sprite;
	}

	// Token: 0x06004883 RID: 18563 RVA: 0x001969C0 File Offset: 0x00194BC0
	public bool UseConditionalCallback(HashedString overlay, Transform transform)
	{
		return overlay != OverlayModes.None.ID && this.conditionalOverlayCallback != null && this.conditionalOverlayCallback(overlay, transform);
	}

	// Token: 0x06004884 RID: 18564 RVA: 0x001969E6 File Offset: 0x00194BE6
	public StatusItem SetResolveStringCallback(Func<string, object, string> cb)
	{
		this.resolveStringCallback = cb;
		return this;
	}

	// Token: 0x06004885 RID: 18565 RVA: 0x001969F0 File Offset: 0x00194BF0
	public void OnClick(object data)
	{
		if (this.statusItemClickCallback != null)
		{
			this.statusItemClickCallback(data);
		}
	}

	// Token: 0x06004886 RID: 18566 RVA: 0x00196A08 File Offset: 0x00194C08
	public static StatusItem.StatusItemOverlays GetStatusItemOverlayBySimViewMode(HashedString mode)
	{
		StatusItem.StatusItemOverlays statusItemOverlays;
		if (!StatusItem.overlayBitfieldMap.TryGetValue(mode, out statusItemOverlays))
		{
			string text = "ViewMode ";
			HashedString hashedString = mode;
			global::Debug.LogWarning(text + hashedString.ToString() + " has no StatusItemOverlay value");
			statusItemOverlays = StatusItem.StatusItemOverlays.None;
		}
		return statusItemOverlays;
	}

	// Token: 0x04002FA1 RID: 12193
	public string tooltipText;

	// Token: 0x04002FA2 RID: 12194
	public string notificationText;

	// Token: 0x04002FA3 RID: 12195
	public string notificationTooltipText;

	// Token: 0x04002FA4 RID: 12196
	public string soundPath;

	// Token: 0x04002FA5 RID: 12197
	public string iconName;

	// Token: 0x04002FA6 RID: 12198
	public TintedSprite sprite;

	// Token: 0x04002FA7 RID: 12199
	public bool shouldNotify;

	// Token: 0x04002FA8 RID: 12200
	public StatusItem.IconType iconType;

	// Token: 0x04002FA9 RID: 12201
	public NotificationType notificationType;

	// Token: 0x04002FAA RID: 12202
	public Notification.ClickCallback notificationClickCallback;

	// Token: 0x04002FAB RID: 12203
	public Func<string, object, string> resolveStringCallback;

	// Token: 0x04002FAC RID: 12204
	public Func<string, object, string> resolveTooltipCallback;

	// Token: 0x04002FAD RID: 12205
	public bool resolveStringCallback_shouldStillCallIfDataIsNull;

	// Token: 0x04002FAE RID: 12206
	public bool resolveTooltipCallback_shouldStillCallIfDataIsNull;

	// Token: 0x04002FAF RID: 12207
	public bool allowMultiples;

	// Token: 0x04002FB0 RID: 12208
	public Func<HashedString, object, bool> conditionalOverlayCallback;

	// Token: 0x04002FB1 RID: 12209
	public HashedString render_overlay;

	// Token: 0x04002FB2 RID: 12210
	public int status_overlays;

	// Token: 0x04002FB3 RID: 12211
	public Action<object> statusItemClickCallback;

	// Token: 0x04002FB4 RID: 12212
	private string composedPrefix;

	// Token: 0x04002FB5 RID: 12213
	private bool showShowWorldIcon = true;

	// Token: 0x04002FB6 RID: 12214
	public const int ALL_OVERLAYS = 129022;

	// Token: 0x04002FB7 RID: 12215
	private static Dictionary<HashedString, StatusItem.StatusItemOverlays> overlayBitfieldMap = new Dictionary<HashedString, StatusItem.StatusItemOverlays>
	{
		{
			OverlayModes.None.ID,
			StatusItem.StatusItemOverlays.None
		},
		{
			OverlayModes.Power.ID,
			StatusItem.StatusItemOverlays.PowerMap
		},
		{
			OverlayModes.Temperature.ID,
			StatusItem.StatusItemOverlays.Temperature
		},
		{
			OverlayModes.ThermalConductivity.ID,
			StatusItem.StatusItemOverlays.ThermalComfort
		},
		{
			OverlayModes.Light.ID,
			StatusItem.StatusItemOverlays.Light
		},
		{
			OverlayModes.LiquidConduits.ID,
			StatusItem.StatusItemOverlays.LiquidPlumbing
		},
		{
			OverlayModes.GasConduits.ID,
			StatusItem.StatusItemOverlays.GasPlumbing
		},
		{
			OverlayModes.SolidConveyor.ID,
			StatusItem.StatusItemOverlays.Conveyor
		},
		{
			OverlayModes.Decor.ID,
			StatusItem.StatusItemOverlays.Decor
		},
		{
			OverlayModes.Disease.ID,
			StatusItem.StatusItemOverlays.Pathogens
		},
		{
			OverlayModes.Crop.ID,
			StatusItem.StatusItemOverlays.Farming
		},
		{
			OverlayModes.Rooms.ID,
			StatusItem.StatusItemOverlays.Rooms
		},
		{
			OverlayModes.Suit.ID,
			StatusItem.StatusItemOverlays.Suits
		},
		{
			OverlayModes.Logic.ID,
			StatusItem.StatusItemOverlays.Logic
		},
		{
			OverlayModes.Oxygen.ID,
			StatusItem.StatusItemOverlays.None
		},
		{
			OverlayModes.TileMode.ID,
			StatusItem.StatusItemOverlays.None
		},
		{
			OverlayModes.Radiation.ID,
			StatusItem.StatusItemOverlays.Radiation
		}
	};

	// Token: 0x0200178B RID: 6027
	public enum IconType
	{
		// Token: 0x04006D50 RID: 27984
		Info,
		// Token: 0x04006D51 RID: 27985
		Exclamation,
		// Token: 0x04006D52 RID: 27986
		Custom
	}

	// Token: 0x0200178C RID: 6028
	[Flags]
	public enum StatusItemOverlays
	{
		// Token: 0x04006D54 RID: 27988
		None = 2,
		// Token: 0x04006D55 RID: 27989
		PowerMap = 4,
		// Token: 0x04006D56 RID: 27990
		Temperature = 8,
		// Token: 0x04006D57 RID: 27991
		ThermalComfort = 16,
		// Token: 0x04006D58 RID: 27992
		Light = 32,
		// Token: 0x04006D59 RID: 27993
		LiquidPlumbing = 64,
		// Token: 0x04006D5A RID: 27994
		GasPlumbing = 128,
		// Token: 0x04006D5B RID: 27995
		Decor = 256,
		// Token: 0x04006D5C RID: 27996
		Pathogens = 512,
		// Token: 0x04006D5D RID: 27997
		Farming = 1024,
		// Token: 0x04006D5E RID: 27998
		Rooms = 4096,
		// Token: 0x04006D5F RID: 27999
		Suits = 8192,
		// Token: 0x04006D60 RID: 28000
		Logic = 16384,
		// Token: 0x04006D61 RID: 28001
		Conveyor = 32768,
		// Token: 0x04006D62 RID: 28002
		Radiation = 65536
	}
}
