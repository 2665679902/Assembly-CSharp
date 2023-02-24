using System;
using System.IO;
using UnityEngine;

namespace Klei
{
	// Token: 0x0200051B RID: 1307
	public class GenericGameSettings
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x0007E004 File Offset: 0x0007C204
		public static GenericGameSettings instance
		{
			get
			{
				if (GenericGameSettings._instance == null)
				{
					if (FileSystem.FileExists(GenericGameSettings.Path))
					{
						GenericGameSettings._instance = YamlIO.LoadFile<GenericGameSettings>(GenericGameSettings.Path, null, null);
						global::Debug.Assert(GenericGameSettings._instance != null, "Loading " + GenericGameSettings.Path + " returned null, the file may be corrupted");
					}
					else
					{
						GenericGameSettings._instance = new GenericGameSettings();
					}
				}
				return GenericGameSettings._instance;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x0007E067 File Offset: 0x0007C267
		// (set) Token: 0x060037A5 RID: 14245 RVA: 0x0007E06F File Offset: 0x0007C26F
		public bool demoMode { get; private set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x0007E078 File Offset: 0x0007C278
		// (set) Token: 0x060037A7 RID: 14247 RVA: 0x0007E080 File Offset: 0x0007C280
		public bool sleepWhenOutOfFocus { get; private set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x0007E089 File Offset: 0x0007C289
		// (set) Token: 0x060037A9 RID: 14249 RVA: 0x0007E091 File Offset: 0x0007C291
		public int demoTime { get; private set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x0007E09A File Offset: 0x0007C29A
		// (set) Token: 0x060037AB RID: 14251 RVA: 0x0007E0A2 File Offset: 0x0007C2A2
		public bool showDemoTimer { get; private set; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x0007E0AB File Offset: 0x0007C2AB
		// (set) Token: 0x060037AD RID: 14253 RVA: 0x0007E0B3 File Offset: 0x0007C2B3
		public bool debugEnable { get; private set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x0007E0BC File Offset: 0x0007C2BC
		// (set) Token: 0x060037AF RID: 14255 RVA: 0x0007E0C4 File Offset: 0x0007C2C4
		public bool developerDebugEnable { get; private set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x0007E0CD File Offset: 0x0007C2CD
		// (set) Token: 0x060037B1 RID: 14257 RVA: 0x0007E0D5 File Offset: 0x0007C2D5
		public bool disableGameOver { get; private set; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x0007E0DE File Offset: 0x0007C2DE
		// (set) Token: 0x060037B3 RID: 14259 RVA: 0x0007E0E6 File Offset: 0x0007C2E6
		public bool disablePopFx { get; private set; }

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x0007E0EF File Offset: 0x0007C2EF
		// (set) Token: 0x060037B5 RID: 14261 RVA: 0x0007E0F7 File Offset: 0x0007C2F7
		public bool autoResumeGame { get; private set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x0007E100 File Offset: 0x0007C300
		// (set) Token: 0x060037B7 RID: 14263 RVA: 0x0007E108 File Offset: 0x0007C308
		public bool disableFogOfWar { get; private set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x0007E111 File Offset: 0x0007C311
		// (set) Token: 0x060037B9 RID: 14265 RVA: 0x0007E119 File Offset: 0x0007C319
		public bool acceleratedLifecycle { get; private set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x0007E122 File Offset: 0x0007C322
		// (set) Token: 0x060037BB RID: 14267 RVA: 0x0007E12A File Offset: 0x0007C32A
		public bool enableEditorCrashReporting { get; private set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x0007E133 File Offset: 0x0007C333
		// (set) Token: 0x060037BD RID: 14269 RVA: 0x0007E13B File Offset: 0x0007C33B
		public bool allowInsufficientMaterialBuild { get; private set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x0007E144 File Offset: 0x0007C344
		// (set) Token: 0x060037BF RID: 14271 RVA: 0x0007E14C File Offset: 0x0007C34C
		public bool keepAllAutosaves { get; private set; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x0007E155 File Offset: 0x0007C355
		// (set) Token: 0x060037C1 RID: 14273 RVA: 0x0007E15D File Offset: 0x0007C35D
		public bool takeSaveScreenshots { get; private set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x0007E166 File Offset: 0x0007C366
		// (set) Token: 0x060037C3 RID: 14275 RVA: 0x0007E16E File Offset: 0x0007C36E
		public bool disableAutosave { get; private set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060037C4 RID: 14276 RVA: 0x0007E177 File Offset: 0x0007C377
		// (set) Token: 0x060037C5 RID: 14277 RVA: 0x0007E17F File Offset: 0x0007C37F
		public bool devAutoWorldGen { get; set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x0007E188 File Offset: 0x0007C388
		// (set) Token: 0x060037C7 RID: 14279 RVA: 0x0007E190 File Offset: 0x0007C390
		public int devWorldGenSeed { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x0007E199 File Offset: 0x0007C399
		// (set) Token: 0x060037C9 RID: 14281 RVA: 0x0007E1A1 File Offset: 0x0007C3A1
		public string devWorldGenCluster { get; set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x0007E1AA File Offset: 0x0007C3AA
		// (set) Token: 0x060037CB RID: 14283 RVA: 0x0007E1B2 File Offset: 0x0007C3B2
		public string[] devWorldGenSkip { get; set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x0007E1BB File Offset: 0x0007C3BB
		// (set) Token: 0x060037CD RID: 14285 RVA: 0x0007E1C3 File Offset: 0x0007C3C3
		public string[] devStoryTraits { get; set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x0007E1CC File Offset: 0x0007C3CC
		// (set) Token: 0x060037CF RID: 14287 RVA: 0x0007E1D4 File Offset: 0x0007C3D4
		public GenericGameSettings.PerformanceCapture performanceCapture { get; set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x0007E1DD File Offset: 0x0007C3DD
		private static string Path
		{
			get
			{
				return System.IO.Path.GetDirectoryName(Application.dataPath) + "/settings.yml";
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x0007E1F4 File Offset: 0x0007C3F4
		public GenericGameSettings()
		{
			this.demoMode = false;
			this.demoTime = 300;
			this.showDemoTimer = true;
			this.sleepWhenOutOfFocus = true;
			this.debugEnable = false;
			this.developerDebugEnable = false;
			this.performanceCapture = new GenericGameSettings.PerformanceCapture();
			GenericGameSettings._instance = this;
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x0007E248 File Offset: 0x0007C448
		public void SaveSettings()
		{
			try
			{
				YamlIO.Save<GenericGameSettings>(this, GenericGameSettings.Path, null);
			}
			catch (Exception ex)
			{
				global::Debug.LogWarning("Failed to save settings.yml: " + ex.ToString());
			}
		}

		// Token: 0x0400141A RID: 5146
		private static GenericGameSettings _instance;

		// Token: 0x0400142C RID: 5164
		public bool devAutoWorldGenActive;

		// Token: 0x02000B28 RID: 2856
		public class PerformanceCapture
		{
			// Token: 0x17000EF9 RID: 3833
			// (get) Token: 0x0600586C RID: 22636 RVA: 0x000A47A4 File Offset: 0x000A29A4
			// (set) Token: 0x0600586D RID: 22637 RVA: 0x000A47AC File Offset: 0x000A29AC
			public string saveGame { get; set; }

			// Token: 0x17000EFA RID: 3834
			// (get) Token: 0x0600586E RID: 22638 RVA: 0x000A47B5 File Offset: 0x000A29B5
			// (set) Token: 0x0600586F RID: 22639 RVA: 0x000A47BD File Offset: 0x000A29BD
			public float waitTime { get; set; }

			// Token: 0x17000EFB RID: 3835
			// (get) Token: 0x06005870 RID: 22640 RVA: 0x000A47C6 File Offset: 0x000A29C6
			// (set) Token: 0x06005871 RID: 22641 RVA: 0x000A47CE File Offset: 0x000A29CE
			public bool gcStats { get; set; }
		}
	}
}
