using System;
using System.Diagnostics;

namespace Database
{
	// Token: 0x02000CB7 RID: 3255
	public class StatusItems : ResourceSet<StatusItem>
	{
		// Token: 0x060065FE RID: 26110 RVA: 0x00272499 File Offset: 0x00270699
		public StatusItems(string id, ResourceSet parent)
			: base(id, parent)
		{
		}

		// Token: 0x02001B32 RID: 6962
		[DebuggerDisplay("{Id}")]
		public class StatusItemInfo : Resource
		{
			// Token: 0x04007AD8 RID: 31448
			public string Type;

			// Token: 0x04007AD9 RID: 31449
			public string Tooltip;

			// Token: 0x04007ADA RID: 31450
			public bool IsIconTinted;

			// Token: 0x04007ADB RID: 31451
			public StatusItem.IconType IconType;

			// Token: 0x04007ADC RID: 31452
			public string Icon;

			// Token: 0x04007ADD RID: 31453
			public string SoundPath;

			// Token: 0x04007ADE RID: 31454
			public bool ShouldNotify;

			// Token: 0x04007ADF RID: 31455
			public float NotificationDelay;

			// Token: 0x04007AE0 RID: 31456
			public NotificationType NotificationType;

			// Token: 0x04007AE1 RID: 31457
			public bool AllowMultiples;

			// Token: 0x04007AE2 RID: 31458
			public string Effect;

			// Token: 0x04007AE3 RID: 31459
			public HashedString Overlay;

			// Token: 0x04007AE4 RID: 31460
			public HashedString SecondOverlay;
		}
	}
}
