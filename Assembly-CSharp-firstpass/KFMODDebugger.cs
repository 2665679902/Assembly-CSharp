using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000021 RID: 33
[AddComponentMenu("KMonoBehaviour/Plugins/KFMODDebugger")]
public class KFMODDebugger : KMonoBehaviour
{
	// Token: 0x060001DB RID: 475 RVA: 0x0000A8B2 File Offset: 0x00008AB2
	public static KFMODDebugger Get()
	{
		return KFMODDebugger.instance;
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000A8BC File Offset: 0x00008ABC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		KFMODDebugger.instance = this;
		foreach (object obj in Enum.GetValues(typeof(KFMODDebugger.DebugSoundType)))
		{
			KFMODDebugger.DebugSoundType debugSoundType = (KFMODDebugger.DebugSoundType)obj;
			this.allDebugSoundTypes.Add(debugSoundType, false);
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000A930 File Offset: 0x00008B30
	protected override void OnCleanUp()
	{
		KFMODDebugger.instance = null;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000A938 File Offset: 0x00008B38
	[Conditional("ENABLE_KFMOD_LOGGER")]
	public void Log(string s)
	{
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000A93C File Offset: 0x00008B3C
	private KFMODDebugger.DebugSoundType GetDebugSoundType(string s)
	{
		if (s.Contains("Buildings"))
		{
			return KFMODDebugger.DebugSoundType.Buildings;
		}
		if (s.Contains("Notifications"))
		{
			return KFMODDebugger.DebugSoundType.Notifications;
		}
		if (s.Contains("UI"))
		{
			return KFMODDebugger.DebugSoundType.UI;
		}
		if (s.Contains("Creatures"))
		{
			return KFMODDebugger.DebugSoundType.Creatures;
		}
		if (s.Contains("Duplicant_voices"))
		{
			return KFMODDebugger.DebugSoundType.DupeVoices;
		}
		if (s.Contains("Ambience"))
		{
			return KFMODDebugger.DebugSoundType.Ambience;
		}
		if (s.Contains("Environment"))
		{
			return KFMODDebugger.DebugSoundType.Environment;
		}
		if (s.Contains("FX"))
		{
			return KFMODDebugger.DebugSoundType.FX;
		}
		if (s.Contains("Duplicant_actions/LowImportance/Movement"))
		{
			return KFMODDebugger.DebugSoundType.DupeMovement;
		}
		if (s.Contains("Duplicant_actions"))
		{
			return KFMODDebugger.DebugSoundType.DupeActions;
		}
		if (s.Contains("Plants"))
		{
			return KFMODDebugger.DebugSoundType.Plants;
		}
		if (s.Contains("Music"))
		{
			return KFMODDebugger.DebugSoundType.Music;
		}
		return KFMODDebugger.DebugSoundType.Uncategorized;
	}

	// Token: 0x040000BC RID: 188
	public static KFMODDebugger instance;

	// Token: 0x040000BD RID: 189
	public List<KFMODDebugger.AudioDebugEntry> AudioDebugLog = new List<KFMODDebugger.AudioDebugEntry>();

	// Token: 0x040000BE RID: 190
	public Dictionary<KFMODDebugger.DebugSoundType, bool> allDebugSoundTypes = new Dictionary<KFMODDebugger.DebugSoundType, bool>();

	// Token: 0x040000BF RID: 191
	public bool debugEnabled;

	// Token: 0x0200097A RID: 2426
	public struct AudioDebugEntry
	{
		// Token: 0x040020DB RID: 8411
		public string log;

		// Token: 0x040020DC RID: 8412
		public KFMODDebugger.DebugSoundType soundType;

		// Token: 0x040020DD RID: 8413
		public float callTime;
	}

	// Token: 0x0200097B RID: 2427
	public enum DebugSoundType
	{
		// Token: 0x040020DF RID: 8415
		Uncategorized = -1,
		// Token: 0x040020E0 RID: 8416
		UI,
		// Token: 0x040020E1 RID: 8417
		Notifications,
		// Token: 0x040020E2 RID: 8418
		Buildings,
		// Token: 0x040020E3 RID: 8419
		DupeVoices,
		// Token: 0x040020E4 RID: 8420
		DupeMovement,
		// Token: 0x040020E5 RID: 8421
		DupeActions,
		// Token: 0x040020E6 RID: 8422
		Creatures,
		// Token: 0x040020E7 RID: 8423
		Plants,
		// Token: 0x040020E8 RID: 8424
		Ambience,
		// Token: 0x040020E9 RID: 8425
		Environment,
		// Token: 0x040020EA RID: 8426
		FX,
		// Token: 0x040020EB RID: 8427
		Music
	}
}
