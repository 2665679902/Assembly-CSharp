using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Database;
using Klei.CustomSettings;
using KSerialization;
using ProcGen;
using UnityEngine;

// Token: 0x020006F7 RID: 1783
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/CustomGameSettings")]
public class CustomGameSettings : KMonoBehaviour
{
	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06003096 RID: 12438 RVA: 0x001009C1 File Offset: 0x000FEBC1
	public static CustomGameSettings Instance
	{
		get
		{
			return CustomGameSettings.instance;
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x06003097 RID: 12439 RVA: 0x001009C8 File Offset: 0x000FEBC8
	public IReadOnlyDictionary<string, string> CurrentStoryLevelsBySetting
	{
		get
		{
			return this.currentStoryLevelsBySetting;
		}
	}

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x06003098 RID: 12440 RVA: 0x001009D0 File Offset: 0x000FEBD0
	// (remove) Token: 0x06003099 RID: 12441 RVA: 0x00100A08 File Offset: 0x000FEC08
	public event Action<SettingConfig, SettingLevel> OnQualitySettingChanged;

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x0600309A RID: 12442 RVA: 0x00100A40 File Offset: 0x000FEC40
	// (remove) Token: 0x0600309B RID: 12443 RVA: 0x00100A78 File Offset: 0x000FEC78
	public event Action<SettingConfig, SettingLevel> OnStorySettingChanged;

	// Token: 0x0600309C RID: 12444 RVA: 0x00100AB0 File Offset: 0x000FECB0
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 6))
		{
			this.customGameMode = (this.is_custom_game ? CustomGameSettings.CustomGameMode.Custom : CustomGameSettings.CustomGameMode.Survival);
		}
		if (this.CurrentQualityLevelsBySetting.ContainsKey("CarePackages "))
		{
			if (!this.CurrentQualityLevelsBySetting.ContainsKey(CustomGameSettingConfigs.CarePackages.id))
			{
				this.CurrentQualityLevelsBySetting.Add(CustomGameSettingConfigs.CarePackages.id, this.CurrentQualityLevelsBySetting["CarePackages "]);
			}
			this.CurrentQualityLevelsBySetting.Remove("CarePackages ");
		}
		this.CurrentQualityLevelsBySetting.Remove("Expansion1Active");
		if (!DlcManager.IsExpansion1Active())
		{
			foreach (KeyValuePair<string, SettingConfig> keyValuePair in this.QualitySettings)
			{
				SettingConfig value = keyValuePair.Value;
				if (!DlcManager.IsVanillaId(value.required_content))
				{
					global::Debug.Assert(value.required_content == "EXPANSION1_ID", "A new expansion setting has been added, but its deserialization has not been implemented.");
					if (this.CurrentQualityLevelsBySetting.ContainsKey(value.id))
					{
						global::Debug.Assert(this.CurrentQualityLevelsBySetting[value.id] == value.missing_content_default, string.Format("This save has Expansion1 content disabled, but its expansion1-dependent setting {0} is set to {1}", value.id, this.CurrentQualityLevelsBySetting[value.id]));
					}
					else
					{
						this.SetQualitySetting(value, value.missing_content_default);
					}
				}
			}
		}
		string clusterDefaultName;
		this.CurrentQualityLevelsBySetting.TryGetValue(CustomGameSettingConfigs.ClusterLayout.id, out clusterDefaultName);
		if (clusterDefaultName.IsNullOrWhiteSpace())
		{
			DebugUtil.DevAssert(!DlcManager.IsExpansion1Active(), "Deserializing CustomGameSettings.ClusterLayout: ClusterLayout is blank, using default cluster instead", null);
			clusterDefaultName = WorldGenSettings.ClusterDefaultName;
			this.SetQualitySetting(CustomGameSettingConfigs.ClusterLayout, clusterDefaultName);
		}
		if (!SettingsCache.clusterLayouts.clusterCache.ContainsKey(clusterDefaultName))
		{
			global::Debug.Log("Deserializing CustomGameSettings.ClusterLayout: '" + clusterDefaultName + "' doesn't exist in the clusterCache, trying to rewrite path to scoped path.");
			string text = SettingsCache.GetScope("EXPANSION1_ID") + clusterDefaultName;
			if (SettingsCache.clusterLayouts.clusterCache.ContainsKey(text))
			{
				global::Debug.Log(string.Concat(new string[] { "Deserializing CustomGameSettings.ClusterLayout: Success in rewriting ClusterLayout '", clusterDefaultName, "' to '", text, "'" }));
				this.SetQualitySetting(CustomGameSettingConfigs.ClusterLayout, text);
			}
			else
			{
				global::Debug.LogWarning("Deserializing CustomGameSettings.ClusterLayout: Failed to find cluster '" + clusterDefaultName + "' including the scoped path, setting to default cluster name.");
				global::Debug.Log("ClusterCache: " + string.Join(",", SettingsCache.clusterLayouts.clusterCache.Keys));
				this.SetQualitySetting(CustomGameSettingConfigs.ClusterLayout, WorldGenSettings.ClusterDefaultName);
			}
		}
		this.CheckCustomGameMode();
	}

	// Token: 0x0600309D RID: 12445 RVA: 0x00100D70 File Offset: 0x000FEF70
	protected override void OnPrefabInit()
	{
		bool flag = DlcManager.IsExpansion1Active();
		CustomGameSettings.instance = this;
		this.AddQualitySettingConfig(CustomGameSettingConfigs.ClusterLayout);
		this.AddQualitySettingConfig(CustomGameSettingConfigs.WorldgenSeed);
		this.AddQualitySettingConfig(CustomGameSettingConfigs.ImmuneSystem);
		this.AddQualitySettingConfig(CustomGameSettingConfigs.CalorieBurn);
		this.AddQualitySettingConfig(CustomGameSettingConfigs.Morale);
		this.AddQualitySettingConfig(CustomGameSettingConfigs.Durability);
		if (flag)
		{
			this.AddQualitySettingConfig(CustomGameSettingConfigs.Radiation);
		}
		this.AddQualitySettingConfig(CustomGameSettingConfigs.Stress);
		this.AddQualitySettingConfig(CustomGameSettingConfigs.StressBreaks);
		this.AddQualitySettingConfig(CustomGameSettingConfigs.CarePackages);
		this.AddQualitySettingConfig(CustomGameSettingConfigs.SandboxMode);
		this.AddQualitySettingConfig(CustomGameSettingConfigs.FastWorkersMode);
		if (SaveLoader.GetCloudSavesAvailable())
		{
			this.AddQualitySettingConfig(CustomGameSettingConfigs.SaveToCloud);
		}
		if (flag)
		{
			this.AddQualitySettingConfig(CustomGameSettingConfigs.Teleporters);
		}
		foreach (Story story in Db.Get().Stories.resources)
		{
			long num = (long)((story.kleiUseOnlyCoordinateOffset == -1) ? (-1) : global::Util.IntPow(3, story.kleiUseOnlyCoordinateOffset));
			int num2 = ((story.kleiUseOnlyCoordinateOffset == -1) ? (-1) : 3);
			SettingConfig settingConfig = new ListSettingConfig(story.Id, "", "", new List<SettingLevel>
			{
				new SettingLevel("Disabled", "", "", 0L, null),
				new SettingLevel("Guaranteed", "", "", 1L, null)
			}, "Disabled", "Disabled", num, (long)num2, false, false, "", "", false);
			this.AddStorySettingConfig(settingConfig);
		}
		this.VerifySettingCoordinates();
	}

	// Token: 0x0600309E RID: 12446 RVA: 0x00100F24 File Offset: 0x000FF124
	public void DisableAllStories()
	{
		foreach (KeyValuePair<string, SettingConfig> keyValuePair in this.StorySettings)
		{
			this.SetStorySetting(keyValuePair.Value, false);
		}
	}

	// Token: 0x0600309F RID: 12447 RVA: 0x00100F80 File Offset: 0x000FF180
	public void SetSurvivalDefaults()
	{
		this.customGameMode = CustomGameSettings.CustomGameMode.Survival;
		foreach (KeyValuePair<string, SettingConfig> keyValuePair in this.QualitySettings)
		{
			this.SetQualitySetting(keyValuePair.Value, keyValuePair.Value.GetDefaultLevelId());
		}
	}

	// Token: 0x060030A0 RID: 12448 RVA: 0x00100FEC File Offset: 0x000FF1EC
	public void SetNosweatDefaults()
	{
		this.customGameMode = CustomGameSettings.CustomGameMode.Nosweat;
		foreach (KeyValuePair<string, SettingConfig> keyValuePair in this.QualitySettings)
		{
			this.SetQualitySetting(keyValuePair.Value, keyValuePair.Value.GetNoSweatDefaultLevelId());
		}
	}

	// Token: 0x060030A1 RID: 12449 RVA: 0x00101058 File Offset: 0x000FF258
	public SettingLevel CycleSettingLevel(ListSettingConfig config, int direction)
	{
		this.SetQualitySetting(config, config.CycleSettingLevelID(this.CurrentQualityLevelsBySetting[config.id], direction));
		return config.GetLevel(this.CurrentQualityLevelsBySetting[config.id]);
	}

	// Token: 0x060030A2 RID: 12450 RVA: 0x00101090 File Offset: 0x000FF290
	public SettingLevel ToggleSettingLevel(ToggleSettingConfig config)
	{
		this.SetQualitySetting(config, config.ToggleSettingLevelID(this.CurrentQualityLevelsBySetting[config.id]));
		return config.GetLevel(this.CurrentQualityLevelsBySetting[config.id]);
	}

	// Token: 0x060030A3 RID: 12451 RVA: 0x001010C7 File Offset: 0x000FF2C7
	public void SetQualitySetting(SettingConfig config, string value)
	{
		this.CurrentQualityLevelsBySetting[config.id] = value;
		this.CheckCustomGameMode();
		if (this.OnQualitySettingChanged != null)
		{
			this.OnQualitySettingChanged(config, this.GetCurrentQualitySetting(config));
		}
	}

	// Token: 0x060030A4 RID: 12452 RVA: 0x001010FC File Offset: 0x000FF2FC
	private void CheckCustomGameMode()
	{
		bool flag = true;
		bool flag2 = true;
		foreach (KeyValuePair<string, string> keyValuePair in this.CurrentQualityLevelsBySetting)
		{
			if (!this.QualitySettings.ContainsKey(keyValuePair.Key))
			{
				DebugUtil.LogWarningArgs(new object[] { "Quality settings missing " + keyValuePair.Key });
			}
			else if (this.QualitySettings[keyValuePair.Key].triggers_custom_game)
			{
				if (keyValuePair.Value != this.QualitySettings[keyValuePair.Key].GetDefaultLevelId())
				{
					flag = false;
				}
				if (keyValuePair.Value != this.QualitySettings[keyValuePair.Key].GetNoSweatDefaultLevelId())
				{
					flag2 = false;
				}
				if (!flag && !flag2)
				{
					break;
				}
			}
		}
		CustomGameSettings.CustomGameMode customGameMode;
		if (flag)
		{
			customGameMode = CustomGameSettings.CustomGameMode.Survival;
		}
		else if (flag2)
		{
			customGameMode = CustomGameSettings.CustomGameMode.Nosweat;
		}
		else
		{
			customGameMode = CustomGameSettings.CustomGameMode.Custom;
		}
		if (customGameMode != this.customGameMode)
		{
			DebugUtil.LogArgs(new object[] { "Game mode changed from", this.customGameMode, "to", customGameMode });
			this.customGameMode = customGameMode;
		}
	}

	// Token: 0x060030A5 RID: 12453 RVA: 0x00101250 File Offset: 0x000FF450
	public SettingLevel GetCurrentQualitySetting(SettingConfig setting)
	{
		return this.GetCurrentQualitySetting(setting.id);
	}

	// Token: 0x060030A6 RID: 12454 RVA: 0x00101260 File Offset: 0x000FF460
	public SettingLevel GetCurrentQualitySetting(string setting_id)
	{
		SettingConfig settingConfig = this.QualitySettings[setting_id];
		if (this.customGameMode == CustomGameSettings.CustomGameMode.Survival && settingConfig.triggers_custom_game)
		{
			return settingConfig.GetLevel(settingConfig.GetDefaultLevelId());
		}
		if (this.customGameMode == CustomGameSettings.CustomGameMode.Nosweat && settingConfig.triggers_custom_game)
		{
			return settingConfig.GetLevel(settingConfig.GetNoSweatDefaultLevelId());
		}
		if (!this.CurrentQualityLevelsBySetting.ContainsKey(setting_id))
		{
			this.CurrentQualityLevelsBySetting[setting_id] = this.QualitySettings[setting_id].GetDefaultLevelId();
		}
		string text = (DlcManager.IsContentActive(settingConfig.required_content) ? this.CurrentQualityLevelsBySetting[setting_id] : settingConfig.GetDefaultLevelId());
		return this.QualitySettings[setting_id].GetLevel(text);
	}

	// Token: 0x060030A7 RID: 12455 RVA: 0x00101314 File Offset: 0x000FF514
	public string GetCurrentQualitySettingLevelId(SettingConfig config)
	{
		return this.CurrentQualityLevelsBySetting[config.id];
	}

	// Token: 0x060030A8 RID: 12456 RVA: 0x00101328 File Offset: 0x000FF528
	public string GetSettingLevelLabel(string setting_id, string level_id)
	{
		SettingConfig settingConfig = this.QualitySettings[setting_id];
		if (settingConfig != null)
		{
			SettingLevel level = settingConfig.GetLevel(level_id);
			if (level != null)
			{
				return level.label;
			}
		}
		global::Debug.LogWarning("No label string for setting: " + setting_id + " level: " + level_id);
		return "";
	}

	// Token: 0x060030A9 RID: 12457 RVA: 0x00101374 File Offset: 0x000FF574
	public string GetQualitySettingLevelTooltip(string setting_id, string level_id)
	{
		SettingConfig settingConfig = this.QualitySettings[setting_id];
		if (settingConfig != null)
		{
			SettingLevel level = settingConfig.GetLevel(level_id);
			if (level != null)
			{
				return level.tooltip;
			}
		}
		global::Debug.LogWarning("No tooltip string for setting: " + setting_id + " level: " + level_id);
		return "";
	}

	// Token: 0x060030AA RID: 12458 RVA: 0x001013C0 File Offset: 0x000FF5C0
	public void AddQualitySettingConfig(SettingConfig config)
	{
		this.QualitySettings.Add(config.id, config);
		if (!this.CurrentQualityLevelsBySetting.ContainsKey(config.id) || string.IsNullOrEmpty(this.CurrentQualityLevelsBySetting[config.id]))
		{
			this.CurrentQualityLevelsBySetting[config.id] = config.GetDefaultLevelId();
		}
	}

	// Token: 0x060030AB RID: 12459 RVA: 0x00101424 File Offset: 0x000FF624
	public void LoadClusters()
	{
		Dictionary<string, ClusterLayout> clusterCache = SettingsCache.clusterLayouts.clusterCache;
		List<SettingLevel> list = new List<SettingLevel>(clusterCache.Count);
		foreach (KeyValuePair<string, ClusterLayout> keyValuePair in clusterCache)
		{
			StringEntry stringEntry;
			string text = (Strings.TryGet(new StringKey(keyValuePair.Value.name), out stringEntry) ? stringEntry.ToString() : keyValuePair.Value.name);
			string text2 = (Strings.TryGet(new StringKey(keyValuePair.Value.description), out stringEntry) ? stringEntry.ToString() : keyValuePair.Value.description);
			list.Add(new SettingLevel(keyValuePair.Key, text, text2, 0L, null));
		}
		CustomGameSettingConfigs.ClusterLayout.StompLevels(list, WorldGenSettings.ClusterDefaultName, WorldGenSettings.ClusterDefaultName);
	}

	// Token: 0x060030AC RID: 12460 RVA: 0x00101514 File Offset: 0x000FF714
	public void Print()
	{
		string text = "Custom Settings: ";
		foreach (KeyValuePair<string, string> keyValuePair in this.CurrentQualityLevelsBySetting)
		{
			text = string.Concat(new string[] { text, keyValuePair.Key, "=", keyValuePair.Value, "," });
		}
		global::Debug.Log(text);
		text = "Story Settings: ";
		foreach (KeyValuePair<string, string> keyValuePair2 in this.currentStoryLevelsBySetting)
		{
			text = string.Concat(new string[] { text, keyValuePair2.Key, "=", keyValuePair2.Value, "," });
		}
		global::Debug.Log(text);
	}

	// Token: 0x060030AD RID: 12461 RVA: 0x0010161C File Offset: 0x000FF81C
	private bool AllValuesMatch(Dictionary<string, string> data, CustomGameSettings.CustomGameMode mode)
	{
		bool flag = true;
		foreach (KeyValuePair<string, SettingConfig> keyValuePair in this.QualitySettings)
		{
			if (!(keyValuePair.Key == CustomGameSettingConfigs.WorldgenSeed.id))
			{
				string text = null;
				if (mode != CustomGameSettings.CustomGameMode.Survival)
				{
					if (mode == CustomGameSettings.CustomGameMode.Nosweat)
					{
						text = keyValuePair.Value.GetNoSweatDefaultLevelId();
					}
				}
				else
				{
					text = keyValuePair.Value.GetDefaultLevelId();
				}
				if (data.ContainsKey(keyValuePair.Key) && data[keyValuePair.Key] != text)
				{
					flag = false;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x060030AE RID: 12462 RVA: 0x001016D0 File Offset: 0x000FF8D0
	public List<CustomGameSettings.MetricSettingsData> GetSettingsForMetrics()
	{
		List<CustomGameSettings.MetricSettingsData> list = new List<CustomGameSettings.MetricSettingsData>();
		list.Add(new CustomGameSettings.MetricSettingsData
		{
			Name = "CustomGameMode",
			Value = this.customGameMode.ToString()
		});
		foreach (KeyValuePair<string, string> keyValuePair in this.CurrentQualityLevelsBySetting)
		{
			list.Add(new CustomGameSettings.MetricSettingsData
			{
				Name = keyValuePair.Key,
				Value = keyValuePair.Value
			});
		}
		CustomGameSettings.MetricSettingsData metricSettingsData = new CustomGameSettings.MetricSettingsData
		{
			Name = "CustomGameModeActual",
			Value = CustomGameSettings.CustomGameMode.Custom.ToString()
		};
		foreach (object obj in Enum.GetValues(typeof(CustomGameSettings.CustomGameMode)))
		{
			CustomGameSettings.CustomGameMode customGameMode = (CustomGameSettings.CustomGameMode)obj;
			if (customGameMode != CustomGameSettings.CustomGameMode.Custom && this.AllValuesMatch(this.CurrentQualityLevelsBySetting, customGameMode))
			{
				metricSettingsData.Value = customGameMode.ToString();
				break;
			}
		}
		list.Add(metricSettingsData);
		return list;
	}

	// Token: 0x060030AF RID: 12463 RVA: 0x0010183C File Offset: 0x000FFA3C
	public bool VerifySettingCoordinates()
	{
		bool flag = this.VerifySettingsDictionary(this.QualitySettings);
		bool flag2 = this.VerifySettingsDictionary(this.StorySettings);
		return flag || flag2;
	}

	// Token: 0x060030B0 RID: 12464 RVA: 0x00101864 File Offset: 0x000FFA64
	private bool VerifySettingsDictionary(Dictionary<string, SettingConfig> configs)
	{
		Dictionary<long, string> dictionary = new Dictionary<long, string>();
		bool flag = false;
		foreach (KeyValuePair<string, SettingConfig> keyValuePair in configs)
		{
			if (keyValuePair.Value.coordinate_dimension < 0L || keyValuePair.Value.coordinate_dimension_width < 0L)
			{
				if (keyValuePair.Value.coordinate_dimension >= 0L || keyValuePair.Value.coordinate_dimension_width >= 0L)
				{
					flag = true;
					global::Debug.Assert(false, keyValuePair.Value.id + ": Both coordinate dimension props must be unset (-1) if either is unset.");
				}
			}
			else
			{
				List<SettingLevel> levels = keyValuePair.Value.GetLevels();
				if (keyValuePair.Value.coordinate_dimension_width < (long)levels.Count)
				{
					flag = true;
					global::Debug.Assert(false, string.Concat(new string[]
					{
						keyValuePair.Value.id,
						": Range between coordinate min and max insufficient for all levels (",
						keyValuePair.Value.coordinate_dimension_width.ToString(),
						"<",
						levels.Count.ToString(),
						")"
					}));
				}
				foreach (SettingLevel settingLevel in levels)
				{
					long num = keyValuePair.Value.coordinate_dimension * settingLevel.coordinate_offset;
					string text = keyValuePair.Value.id + " > " + settingLevel.id;
					if (settingLevel.coordinate_offset < 0L)
					{
						flag = true;
						global::Debug.Assert(false, text + ": Level coordinate offset must be >= 0");
					}
					else if (settingLevel.coordinate_offset == 0L)
					{
						if (settingLevel.id != keyValuePair.Value.GetDefaultLevelId())
						{
							flag = true;
							global::Debug.Assert(false, text + ": Only the default level should have a coordinate offset of 0");
						}
					}
					else if (settingLevel.coordinate_offset > keyValuePair.Value.coordinate_dimension_width)
					{
						flag = true;
						global::Debug.Assert(false, text + ": level coordinate must be <= dimension width");
					}
					else
					{
						string text2;
						bool flag2 = !dictionary.TryGetValue(num, out text2);
						dictionary[num] = text;
						if (settingLevel.id == keyValuePair.Value.GetDefaultLevelId())
						{
							flag = true;
							global::Debug.Assert(false, text + ": Default level must be coordinate 0");
						}
						if (!flag2)
						{
							flag = true;
							global::Debug.Assert(false, text + ": Combined coordinate conflicts with another coordinate (" + text2 + "). Ensure this SettingConfig's min and max don't overlap with another SettingConfig's");
						}
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x060030B1 RID: 12465 RVA: 0x00101B2C File Offset: 0x000FFD2C
	public static string[] ParseSettingCoordinate(string coord)
	{
		string[] array = CustomGameSettings.ParseCoordinate(coord, "(.*)-(\\d*)-(.*)-(.*)");
		if (array.Length == 1)
		{
			array = CustomGameSettings.ParseCoordinate(coord, "(.*)-(\\d*)-(.*)");
		}
		return array;
	}

	// Token: 0x060030B2 RID: 12466 RVA: 0x00101B58 File Offset: 0x000FFD58
	private static string[] ParseCoordinate(string coord, string pattern)
	{
		Match match = new Regex(pattern).Match(coord);
		string[] array = new string[match.Groups.Count];
		for (int i = 0; i < match.Groups.Count; i++)
		{
			array[i] = match.Groups[i].Value;
		}
		return array;
	}

	// Token: 0x060030B3 RID: 12467 RVA: 0x00101BB0 File Offset: 0x000FFDB0
	public string GetSettingsCoordinate()
	{
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ClusterLayout);
		if (currentQualitySetting == null)
		{
			DebugUtil.DevLogError("GetSettingsCoordinate: clusterLayoutSetting is null, returning '0' coordinate");
			CustomGameSettings.Instance.Print();
			global::Debug.Log("ClusterCache: " + string.Join(",", SettingsCache.clusterLayouts.clusterCache.Keys));
			return "0-0-0-0";
		}
		ClusterLayout clusterData = SettingsCache.clusterLayouts.GetClusterData(currentQualitySetting.id);
		SettingLevel currentQualitySetting2 = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.WorldgenSeed);
		string otherSettingsCode = this.GetOtherSettingsCode();
		string storyTraitSettingsCode = this.GetStoryTraitSettingsCode();
		return string.Format("{0}-{1}-{2}-{3}", new object[]
		{
			clusterData.GetCoordinatePrefix(),
			currentQualitySetting2.id,
			otherSettingsCode,
			storyTraitSettingsCode
		});
	}

	// Token: 0x060030B4 RID: 12468 RVA: 0x00101C70 File Offset: 0x000FFE70
	public void ParseAndApplySettingsCode(string code)
	{
		long num = this.Base36toBase10(code);
		Dictionary<SettingConfig, string> dictionary = new Dictionary<SettingConfig, string>();
		foreach (KeyValuePair<string, string> keyValuePair in this.CurrentQualityLevelsBySetting)
		{
			SettingConfig settingConfig = this.QualitySettings[keyValuePair.Key];
			if (settingConfig.coordinate_dimension >= 0L && settingConfig.coordinate_dimension_width >= 0L)
			{
				long num2 = 0L;
				long num3 = settingConfig.coordinate_dimension * settingConfig.coordinate_dimension_width;
				long num4 = num;
				if (num4 >= num3)
				{
					long num5 = num4 / num3 * num3;
					num4 -= num5;
				}
				if (num4 >= settingConfig.coordinate_dimension)
				{
					num2 = num4 / settingConfig.coordinate_dimension;
				}
				foreach (SettingLevel settingLevel in settingConfig.GetLevels())
				{
					if (settingLevel.coordinate_offset == num2)
					{
						dictionary[settingConfig] = settingLevel.id;
						break;
					}
				}
			}
		}
		foreach (KeyValuePair<SettingConfig, string> keyValuePair2 in dictionary)
		{
			this.SetQualitySetting(keyValuePair2.Key, keyValuePair2.Value);
		}
	}

	// Token: 0x060030B5 RID: 12469 RVA: 0x00101DEC File Offset: 0x000FFFEC
	private string GetOtherSettingsCode()
	{
		long num = 0L;
		foreach (KeyValuePair<string, string> keyValuePair in this.CurrentQualityLevelsBySetting)
		{
			SettingConfig settingConfig;
			this.QualitySettings.TryGetValue(keyValuePair.Key, out settingConfig);
			if (settingConfig != null && settingConfig.coordinate_dimension >= 0L && settingConfig.coordinate_dimension_width >= 0L)
			{
				SettingLevel level = settingConfig.GetLevel(keyValuePair.Value);
				long num2 = settingConfig.coordinate_dimension * level.coordinate_offset;
				num += num2;
			}
		}
		return this.Base10toBase36(num);
	}

	// Token: 0x060030B6 RID: 12470 RVA: 0x00101E94 File Offset: 0x00100094
	private long Base36toBase10(string input)
	{
		if (input == "0")
		{
			return 0L;
		}
		long num = 0L;
		for (int i = input.Length - 1; i >= 0; i--)
		{
			num *= 36L;
			long num2 = (long)this.hexChars.IndexOf(input[i]);
			num += num2;
		}
		DebugUtil.LogArgs(new object[]
		{
			"tried converting",
			input,
			", got",
			num,
			"and returns to",
			this.Base10toBase36(num)
		});
		return num;
	}

	// Token: 0x060030B7 RID: 12471 RVA: 0x00101F20 File Offset: 0x00100120
	private string Base10toBase36(long input)
	{
		if (input == 0L)
		{
			return "0";
		}
		long num = input;
		string text = "";
		while (num > 0L)
		{
			text += this.hexChars[(int)(num % 36L)].ToString();
			num /= 36L;
		}
		return text;
	}

	// Token: 0x060030B8 RID: 12472 RVA: 0x00101F6C File Offset: 0x0010016C
	public void AddStorySettingConfig(SettingConfig config)
	{
		this.StorySettings.Add(config.id, config);
		if (!this.currentStoryLevelsBySetting.ContainsKey(config.id) || string.IsNullOrEmpty(this.currentStoryLevelsBySetting[config.id]))
		{
			this.currentStoryLevelsBySetting[config.id] = config.GetDefaultLevelId();
		}
	}

	// Token: 0x060030B9 RID: 12473 RVA: 0x00101FCD File Offset: 0x001001CD
	public void SetStorySetting(SettingConfig config, string value)
	{
		this.SetStorySetting(config, value == "Guaranteed");
	}

	// Token: 0x060030BA RID: 12474 RVA: 0x00101FE1 File Offset: 0x001001E1
	public void SetStorySetting(SettingConfig config, bool value)
	{
		this.currentStoryLevelsBySetting[config.id] = (value ? "Guaranteed" : "Disabled");
		if (this.OnStorySettingChanged != null)
		{
			this.OnStorySettingChanged(config, this.GetCurrentStoryTraitSetting(config));
		}
	}

	// Token: 0x060030BB RID: 12475 RVA: 0x00102020 File Offset: 0x00100220
	public void ParseAndApplyStoryTraitSettingsCode(string code)
	{
		long num = this.Base36toBase10(code);
		Dictionary<SettingConfig, string> dictionary = new Dictionary<SettingConfig, string>();
		foreach (KeyValuePair<string, string> keyValuePair in this.currentStoryLevelsBySetting)
		{
			SettingConfig settingConfig = this.StorySettings[keyValuePair.Key];
			if (settingConfig.coordinate_dimension >= 0L && settingConfig.coordinate_dimension_width >= 0L)
			{
				long num2 = 0L;
				long num3 = settingConfig.coordinate_dimension * settingConfig.coordinate_dimension_width;
				long num4 = num;
				if (num4 >= num3)
				{
					long num5 = num4 / num3 * num3;
					num4 -= num5;
				}
				if (num4 >= settingConfig.coordinate_dimension)
				{
					num2 = num4 / settingConfig.coordinate_dimension;
				}
				foreach (SettingLevel settingLevel in settingConfig.GetLevels())
				{
					if (settingLevel.coordinate_offset == num2)
					{
						dictionary[settingConfig] = settingLevel.id;
						break;
					}
				}
			}
		}
		foreach (KeyValuePair<SettingConfig, string> keyValuePair2 in dictionary)
		{
			this.SetStorySetting(keyValuePair2.Key, keyValuePair2.Value);
		}
	}

	// Token: 0x060030BC RID: 12476 RVA: 0x0010219C File Offset: 0x0010039C
	private string GetStoryTraitSettingsCode()
	{
		long num = 0L;
		foreach (KeyValuePair<string, string> keyValuePair in this.currentStoryLevelsBySetting)
		{
			SettingConfig settingConfig;
			this.StorySettings.TryGetValue(keyValuePair.Key, out settingConfig);
			if (settingConfig != null && settingConfig.coordinate_dimension >= 0L && settingConfig.coordinate_dimension_width >= 0L)
			{
				SettingLevel level = settingConfig.GetLevel(keyValuePair.Value);
				long num2 = settingConfig.coordinate_dimension * level.coordinate_offset;
				num += num2;
			}
		}
		return this.Base10toBase36(num);
	}

	// Token: 0x060030BD RID: 12477 RVA: 0x00102244 File Offset: 0x00100444
	public SettingLevel GetCurrentStoryTraitSetting(SettingConfig setting)
	{
		return this.GetCurrentStoryTraitSetting(setting.id);
	}

	// Token: 0x060030BE RID: 12478 RVA: 0x00102254 File Offset: 0x00100454
	public SettingLevel GetCurrentStoryTraitSetting(string settingId)
	{
		SettingConfig settingConfig = this.StorySettings[settingId];
		if (this.customGameMode == CustomGameSettings.CustomGameMode.Survival && settingConfig.triggers_custom_game)
		{
			return settingConfig.GetLevel(settingConfig.GetDefaultLevelId());
		}
		if (this.customGameMode == CustomGameSettings.CustomGameMode.Nosweat && settingConfig.triggers_custom_game)
		{
			return settingConfig.GetLevel(settingConfig.GetNoSweatDefaultLevelId());
		}
		if (!this.currentStoryLevelsBySetting.ContainsKey(settingId))
		{
			this.currentStoryLevelsBySetting[settingId] = this.StorySettings[settingId].GetDefaultLevelId();
		}
		string text = (DlcManager.IsContentActive(settingConfig.required_content) ? this.currentStoryLevelsBySetting[settingId] : settingConfig.GetDefaultLevelId());
		return this.StorySettings[settingId].GetLevel(text);
	}

	// Token: 0x060030BF RID: 12479 RVA: 0x00102308 File Offset: 0x00100508
	public List<string> GetCurrentStories()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, string> keyValuePair in this.currentStoryLevelsBySetting)
		{
			if (this.IsStoryActive(keyValuePair.Key, keyValuePair.Value))
			{
				list.Add(keyValuePair.Key);
			}
		}
		return list;
	}

	// Token: 0x060030C0 RID: 12480 RVA: 0x00102380 File Offset: 0x00100580
	public bool IsStoryActive(string id, string level)
	{
		SettingConfig settingConfig;
		return this.StorySettings.TryGetValue(id, out settingConfig) && settingConfig != null && level == "Guaranteed";
	}

	// Token: 0x04001D57 RID: 7511
	private static CustomGameSettings instance;

	// Token: 0x04001D58 RID: 7512
	private const int NUM_STORY_LEVELS = 3;

	// Token: 0x04001D59 RID: 7513
	public const string STORY_DISABLED_LEVEL = "Disabled";

	// Token: 0x04001D5A RID: 7514
	public const string STORY_GUARANTEED_LEVEL = "Guaranteed";

	// Token: 0x04001D5B RID: 7515
	[Serialize]
	public bool is_custom_game;

	// Token: 0x04001D5C RID: 7516
	[Serialize]
	public CustomGameSettings.CustomGameMode customGameMode;

	// Token: 0x04001D5D RID: 7517
	[Serialize]
	private Dictionary<string, string> CurrentQualityLevelsBySetting = new Dictionary<string, string>();

	// Token: 0x04001D5E RID: 7518
	private Dictionary<string, string> currentStoryLevelsBySetting = new Dictionary<string, string>();

	// Token: 0x04001D5F RID: 7519
	public Dictionary<string, SettingConfig> QualitySettings = new Dictionary<string, SettingConfig>();

	// Token: 0x04001D60 RID: 7520
	public Dictionary<string, SettingConfig> StorySettings = new Dictionary<string, SettingConfig>();

	// Token: 0x04001D63 RID: 7523
	private const string storyCoordinatePattern = "(.*)-(\\d*)-(.*)-(.*)";

	// Token: 0x04001D64 RID: 7524
	private const string noStoryCoordinatePattern = "(.*)-(\\d*)-(.*)";

	// Token: 0x04001D65 RID: 7525
	private string hexChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	// Token: 0x02001418 RID: 5144
	public enum CustomGameMode
	{
		// Token: 0x04006283 RID: 25219
		Survival,
		// Token: 0x04006284 RID: 25220
		Nosweat,
		// Token: 0x04006285 RID: 25221
		Custom = 255
	}

	// Token: 0x02001419 RID: 5145
	public struct MetricSettingsData
	{
		// Token: 0x04006286 RID: 25222
		public string Name;

		// Token: 0x04006287 RID: 25223
		public string Value;
	}
}
