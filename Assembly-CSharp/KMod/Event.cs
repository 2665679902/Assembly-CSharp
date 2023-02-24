﻿using System;
using STRINGS;

namespace KMod
{
	// Token: 0x02000D17 RID: 3351
	public struct Event
	{
		// Token: 0x060067D7 RID: 26583 RVA: 0x00281DF8 File Offset: 0x0027FFF8
		public static void GetUIStrings(EventType err_type, out string title, out string title_tooltip)
		{
			title = string.Empty;
			title_tooltip = string.Empty;
			switch (err_type)
			{
			case EventType.LoadError:
				title = UI.FRONTEND.MOD_EVENTS.REQUIRED;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.REQUIRED;
				return;
			case EventType.NotFound:
				title = UI.FRONTEND.MOD_EVENTS.NOT_FOUND;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.NOT_FOUND;
				return;
			case EventType.InstallInfoInaccessible:
				title = UI.FRONTEND.MOD_EVENTS.INSTALL_INFO_INACCESSIBLE;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.INSTALL_INFO_INACCESSIBLE;
				return;
			case EventType.OutOfOrder:
				title = UI.FRONTEND.MOD_EVENTS.OUT_OF_ORDER;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.OUT_OF_ORDER;
				return;
			case EventType.ExpectedActive:
				title = UI.FRONTEND.MOD_EVENTS.EXPECTED_ENABLED;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.EXPECTED_ENABLED;
				return;
			case EventType.ExpectedInactive:
				title = UI.FRONTEND.MOD_EVENTS.EXPECTED_DISABLED;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.EXPECTED_DISABLED;
				return;
			case EventType.ActiveDuringCrash:
				title = UI.FRONTEND.MOD_EVENTS.ACTIVE_DURING_CRASH;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.ACTIVE_DURING_CRASH;
				return;
			case EventType.InstallFailed:
				title = UI.FRONTEND.MOD_EVENTS.INSTALL_FAILED;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.INSTALL_FAILED;
				return;
			case EventType.Installed:
				title = UI.FRONTEND.MOD_EVENTS.INSTALLED;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.INSTALLED;
				return;
			case EventType.Uninstalled:
				title = UI.FRONTEND.MOD_EVENTS.UNINSTALLED;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.UNINSTALLED;
				return;
			case EventType.VersionUpdate:
				title = UI.FRONTEND.MOD_EVENTS.VERSION_UPDATE;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.VERSION_UPDATE;
				return;
			case EventType.AvailableContentChanged:
				title = UI.FRONTEND.MOD_EVENTS.AVAILABLE_CONTENT_CHANGED;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.AVAILABLE_CONTENT_CHANGED;
				return;
			case EventType.RestartRequested:
				title = UI.FRONTEND.MOD_EVENTS.REQUIRES_RESTART;
				title_tooltip = UI.FRONTEND.MODS.REQUIRES_RESTART;
				return;
			case EventType.BadWorldGen:
				title = UI.FRONTEND.MOD_EVENTS.BAD_WORLD_GEN;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.BAD_WORLD_GEN;
				return;
			case EventType.Deactivated:
				title = UI.FRONTEND.MOD_EVENTS.DEACTIVATED;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.DEACTIVATED;
				return;
			case EventType.DisabledEarlyAccess:
				title = UI.FRONTEND.MOD_EVENTS.ALL_MODS_DISABLED_EARLY_ACCESS;
				title_tooltip = UI.FRONTEND.MOD_EVENTS.TOOLTIPS.ALL_MODS_DISABLED_EARLY_ACCESS;
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x04004C2E RID: 19502
		public EventType event_type;

		// Token: 0x04004C2F RID: 19503
		public Label mod;

		// Token: 0x04004C30 RID: 19504
		public string details;
	}
}
