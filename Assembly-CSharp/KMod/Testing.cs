using System;

namespace KMod
{
	// Token: 0x02000D03 RID: 3331
	public static class Testing
	{
		// Token: 0x04004BC7 RID: 19399
		public static Testing.DLLLoading dll_loading;

		// Token: 0x04004BC8 RID: 19400
		public const Testing.SaveLoad SAVE_LOAD = Testing.SaveLoad.NoTesting;

		// Token: 0x04004BC9 RID: 19401
		public const Testing.Install INSTALL = Testing.Install.NoTesting;

		// Token: 0x04004BCA RID: 19402
		public const Testing.Boot BOOT = Testing.Boot.NoTesting;

		// Token: 0x02001B3B RID: 6971
		public enum DLLLoading
		{
			// Token: 0x04007AF5 RID: 31477
			NoTesting,
			// Token: 0x04007AF6 RID: 31478
			Fail,
			// Token: 0x04007AF7 RID: 31479
			UseModLoaderDLLExclusively
		}

		// Token: 0x02001B3C RID: 6972
		public enum SaveLoad
		{
			// Token: 0x04007AF9 RID: 31481
			NoTesting,
			// Token: 0x04007AFA RID: 31482
			FailSave,
			// Token: 0x04007AFB RID: 31483
			FailLoad
		}

		// Token: 0x02001B3D RID: 6973
		public enum Install
		{
			// Token: 0x04007AFD RID: 31485
			NoTesting,
			// Token: 0x04007AFE RID: 31486
			ForceUninstall,
			// Token: 0x04007AFF RID: 31487
			ForceReinstall,
			// Token: 0x04007B00 RID: 31488
			ForceUpdate
		}

		// Token: 0x02001B3E RID: 6974
		public enum Boot
		{
			// Token: 0x04007B02 RID: 31490
			NoTesting,
			// Token: 0x04007B03 RID: 31491
			Crash
		}
	}
}
