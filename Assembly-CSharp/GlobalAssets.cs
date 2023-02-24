using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x02000796 RID: 1942
public class GlobalAssets : KMonoBehaviour
{
	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x06003682 RID: 13954 RVA: 0x00130263 File Offset: 0x0012E463
	// (set) Token: 0x06003683 RID: 13955 RVA: 0x0013026A File Offset: 0x0012E46A
	public static GlobalAssets Instance { get; private set; }

	// Token: 0x06003684 RID: 13956 RVA: 0x00130274 File Offset: 0x0012E474
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		GlobalAssets.Instance = this;
		if (GlobalAssets.SoundTable.Count == 0)
		{
			Bank[] array = null;
			try
			{
				if (RuntimeManager.StudioSystem.getBankList(out array) != RESULT.OK)
				{
					array = null;
				}
			}
			catch
			{
				array = null;
			}
			if (array != null)
			{
				foreach (Bank bank in array)
				{
					EventDescription[] array3;
					RESULT eventList = bank.getEventList(out array3);
					if (eventList != RESULT.OK)
					{
						string text;
						bank.getPath(out text);
						global::Debug.LogError(string.Format("ERROR [{0}] loading FMOD events for bank [{1}]", eventList, text));
					}
					else
					{
						foreach (EventDescription eventDescription in array3)
						{
							string text;
							eventDescription.getPath(out text);
							if (text == null)
							{
								bank.getPath(out text);
								GUID guid;
								eventDescription.getID(out guid);
								global::Debug.LogError(string.Format("Got a FMOD event with a null path! {0} {1} in bank {2}", eventDescription.ToString(), guid, text));
							}
							else
							{
								string text2 = Assets.GetSimpleSoundEventName(text);
								text2 = text2.ToLowerInvariant();
								if (text2.Length > 0 && !GlobalAssets.SoundTable.ContainsKey(text2))
								{
									GlobalAssets.SoundTable[text2] = text;
									if (text.ToLower().Contains("lowpriority") || text2.Contains("lowpriority"))
									{
										GlobalAssets.LowPrioritySounds.Add(text);
									}
									else if (text.ToLower().Contains("highpriority") || text2.Contains("highpriority"))
									{
										GlobalAssets.HighPrioritySounds.Add(text);
									}
								}
							}
						}
					}
				}
			}
		}
		SetDefaults.Initialize();
		GraphicsOptionsScreen.SetColorModeFromPrefs();
		this.AddColorModeStyles();
		LocString.CreateLocStringKeys(typeof(UI), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(INPUT), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(GAMEPLAY_EVENTS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(ROOMS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(BUILDING.STATUSITEMS), "STRINGS.BUILDING.");
		LocString.CreateLocStringKeys(typeof(BUILDING.DETAILS), "STRINGS.BUILDING.");
		LocString.CreateLocStringKeys(typeof(SETITEMS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(COLONY_ACHIEVEMENTS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(CREATURES), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(RESEARCH), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(DUPLICANTS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(ITEMS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(ROBOTS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(ELEMENTS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(MISC), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(VIDEOS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(NAMEGEN), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(WORLDS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(CLUSTER_NAMES), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(SUBWORLDS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(WORLD_TRAITS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(INPUT_BINDINGS), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(LORE), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(CODEX), "STRINGS.");
		LocString.CreateLocStringKeys(typeof(SUBWORLDS), "STRINGS.");
	}

	// Token: 0x06003685 RID: 13957 RVA: 0x00130630 File Offset: 0x0012E830
	private void AddColorModeStyles()
	{
		TMP_Style tmp_Style = new TMP_Style("logic_on", string.Format("<color=#{0}>", ColorUtility.ToHtmlStringRGB(this.colorSet.logicOn)), "</color>");
		TMP_StyleSheet.instance.AddStyle(tmp_Style);
		TMP_Style tmp_Style2 = new TMP_Style("logic_off", string.Format("<color=#{0}>", ColorUtility.ToHtmlStringRGB(this.colorSet.logicOff)), "</color>");
		TMP_StyleSheet.instance.AddStyle(tmp_Style2);
		TMP_StyleSheet.RefreshStyles();
	}

	// Token: 0x06003686 RID: 13958 RVA: 0x001306B6 File Offset: 0x0012E8B6
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GlobalAssets.Instance = null;
	}

	// Token: 0x06003687 RID: 13959 RVA: 0x001306C4 File Offset: 0x0012E8C4
	public static string GetSound(string name, bool force_no_warning = false)
	{
		if (name == null)
		{
			return null;
		}
		name = name.ToLowerInvariant();
		string text = null;
		GlobalAssets.SoundTable.TryGetValue(name, out text);
		return text;
	}

	// Token: 0x06003688 RID: 13960 RVA: 0x001306EF File Offset: 0x0012E8EF
	public static bool IsLowPriority(string path)
	{
		return GlobalAssets.LowPrioritySounds.Contains(path);
	}

	// Token: 0x06003689 RID: 13961 RVA: 0x001306FC File Offset: 0x0012E8FC
	public static bool IsHighPriority(string path)
	{
		return GlobalAssets.HighPrioritySounds.Contains(path);
	}

	// Token: 0x04002455 RID: 9301
	private static Dictionary<string, string> SoundTable = new Dictionary<string, string>();

	// Token: 0x04002456 RID: 9302
	private static HashSet<string> LowPrioritySounds = new HashSet<string>();

	// Token: 0x04002457 RID: 9303
	private static HashSet<string> HighPrioritySounds = new HashSet<string>();

	// Token: 0x04002459 RID: 9305
	public ColorSet colorSet;

	// Token: 0x0400245A RID: 9306
	public ColorSet[] colorSetOptions;
}
