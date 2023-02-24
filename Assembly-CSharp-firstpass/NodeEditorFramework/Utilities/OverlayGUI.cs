using System;
using UnityEngine;

namespace NodeEditorFramework.Utilities
{
	// Token: 0x02000491 RID: 1169
	public static class OverlayGUI
	{
		// Token: 0x06003235 RID: 12853 RVA: 0x00066F5D File Offset: 0x0006515D
		public static bool HasPopupControl()
		{
			return OverlayGUI.currentPopup != null;
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x00066F67 File Offset: 0x00065167
		public static void StartOverlayGUI()
		{
			if (OverlayGUI.currentPopup != null && Event.current.type != EventType.Layout && Event.current.type != EventType.Repaint)
			{
				OverlayGUI.currentPopup.Draw();
			}
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x00066F94 File Offset: 0x00065194
		public static void EndOverlayGUI()
		{
			if (OverlayGUI.currentPopup != null && (Event.current.type == EventType.Layout || Event.current.type == EventType.Repaint))
			{
				OverlayGUI.currentPopup.Draw();
			}
		}

		// Token: 0x04001175 RID: 4469
		public static PopupMenu currentPopup;
	}
}
