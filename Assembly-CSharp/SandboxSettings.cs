using System;
using System.Collections.Generic;

// Token: 0x02000B75 RID: 2933
public class SandboxSettings
{
	// Token: 0x06005C03 RID: 23555 RVA: 0x00219C19 File Offset: 0x00217E19
	public void AddIntSetting(string prefsKey, Action<int> setAction, int defaultValue)
	{
		this.intSettings.Add(new SandboxSettings.Setting<int>(prefsKey, setAction, defaultValue));
	}

	// Token: 0x06005C04 RID: 23556 RVA: 0x00219C2E File Offset: 0x00217E2E
	public int GetIntSetting(string prefsKey)
	{
		return KPlayerPrefs.GetInt(prefsKey);
	}

	// Token: 0x06005C05 RID: 23557 RVA: 0x00219C38 File Offset: 0x00217E38
	public void SetIntSetting(string prefsKey, int value)
	{
		SandboxSettings.Setting<int> setting = this.intSettings.Find((SandboxSettings.Setting<int> match) => match.PrefsKey == prefsKey);
		if (setting == null)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"No intSetting named: ",
				prefsKey,
				" could be found amongst ",
				this.intSettings.Count.ToString(),
				" int settings."
			}));
		}
		setting.Value = value;
	}

	// Token: 0x06005C06 RID: 23558 RVA: 0x00219CBB File Offset: 0x00217EBB
	public void RestoreIntSetting(string prefsKey)
	{
		if (KPlayerPrefs.HasKey(prefsKey))
		{
			this.SetIntSetting(prefsKey, this.GetIntSetting(prefsKey));
			return;
		}
		this.ForceDefaultIntSetting(prefsKey);
	}

	// Token: 0x06005C07 RID: 23559 RVA: 0x00219CDC File Offset: 0x00217EDC
	public void ForceDefaultIntSetting(string prefsKey)
	{
		this.SetIntSetting(prefsKey, this.intSettings.Find((SandboxSettings.Setting<int> match) => match.PrefsKey == prefsKey).defaultValue);
	}

	// Token: 0x06005C08 RID: 23560 RVA: 0x00219D1E File Offset: 0x00217F1E
	public void AddFloatSetting(string prefsKey, Action<float> setAction, float defaultValue)
	{
		this.floatSettings.Add(new SandboxSettings.Setting<float>(prefsKey, setAction, defaultValue));
	}

	// Token: 0x06005C09 RID: 23561 RVA: 0x00219D33 File Offset: 0x00217F33
	public float GetFloatSetting(string prefsKey)
	{
		return KPlayerPrefs.GetFloat(prefsKey);
	}

	// Token: 0x06005C0A RID: 23562 RVA: 0x00219D3C File Offset: 0x00217F3C
	public void SetFloatSetting(string prefsKey, float value)
	{
		SandboxSettings.Setting<float> setting = this.floatSettings.Find((SandboxSettings.Setting<float> match) => match.PrefsKey == prefsKey);
		if (setting == null)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"No KPlayerPrefs float setting named: ",
				prefsKey,
				" could be found amongst ",
				this.floatSettings.Count.ToString(),
				" float settings."
			}));
		}
		setting.Value = value;
	}

	// Token: 0x06005C0B RID: 23563 RVA: 0x00219DBF File Offset: 0x00217FBF
	public void RestoreFloatSetting(string prefsKey)
	{
		if (KPlayerPrefs.HasKey(prefsKey))
		{
			this.SetFloatSetting(prefsKey, this.GetFloatSetting(prefsKey));
			return;
		}
		this.ForceDefaultFloatSetting(prefsKey);
	}

	// Token: 0x06005C0C RID: 23564 RVA: 0x00219DE0 File Offset: 0x00217FE0
	public void ForceDefaultFloatSetting(string prefsKey)
	{
		this.SetFloatSetting(prefsKey, this.floatSettings.Find((SandboxSettings.Setting<float> match) => match.PrefsKey == prefsKey).defaultValue);
	}

	// Token: 0x06005C0D RID: 23565 RVA: 0x00219E22 File Offset: 0x00218022
	public void AddStringSetting(string prefsKey, Action<string> setAction, string defaultValue)
	{
		this.stringSettings.Add(new SandboxSettings.Setting<string>(prefsKey, setAction, defaultValue));
	}

	// Token: 0x06005C0E RID: 23566 RVA: 0x00219E37 File Offset: 0x00218037
	public string GetStringSetting(string prefsKey)
	{
		return KPlayerPrefs.GetString(prefsKey);
	}

	// Token: 0x06005C0F RID: 23567 RVA: 0x00219E40 File Offset: 0x00218040
	public void SetStringSetting(string prefsKey, string value)
	{
		SandboxSettings.Setting<string> setting = this.stringSettings.Find((SandboxSettings.Setting<string> match) => match.PrefsKey == prefsKey);
		if (setting == null)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"No KPlayerPrefs string setting named: ",
				prefsKey,
				" could be found amongst ",
				this.stringSettings.Count.ToString(),
				" settings."
			}));
		}
		setting.Value = value;
	}

	// Token: 0x06005C10 RID: 23568 RVA: 0x00219EC3 File Offset: 0x002180C3
	public void RestoreStringSetting(string prefsKey)
	{
		if (KPlayerPrefs.HasKey(prefsKey))
		{
			this.SetStringSetting(prefsKey, this.GetStringSetting(prefsKey));
			return;
		}
		this.ForceDefaultStringSetting(prefsKey);
	}

	// Token: 0x06005C11 RID: 23569 RVA: 0x00219EE4 File Offset: 0x002180E4
	public void ForceDefaultStringSetting(string prefsKey)
	{
		this.SetStringSetting(prefsKey, this.stringSettings.Find((SandboxSettings.Setting<string> match) => match.PrefsKey == prefsKey).defaultValue);
	}

	// Token: 0x06005C12 RID: 23570 RVA: 0x00219F28 File Offset: 0x00218128
	public SandboxSettings()
	{
		this.AddStringSetting("SandboxTools.SelectedEntity", delegate(string data)
		{
			KPlayerPrefs.SetString("SandboxTools.SelectedEntity", data);
			this.OnChangeEntity();
		}, "MushBar");
		this.AddIntSetting("SandboxTools.SelectedElement", delegate(int data)
		{
			KPlayerPrefs.SetInt("SandboxTools.SelectedElement", data);
			this.OnChangeElement(this.hasRestoredElement);
			this.hasRestoredElement = true;
		}, (int)ElementLoader.GetElementIndex(SimHashes.Oxygen));
		this.AddStringSetting("SandboxTools.SelectedDisease", delegate(string data)
		{
			KPlayerPrefs.SetString("SandboxTools.SelectedDisease", data);
			this.OnChangeDisease();
		}, Db.Get().Diseases.FoodGerms.Id);
		this.AddIntSetting("SandboxTools.DiseaseCount", delegate(int val)
		{
			KPlayerPrefs.SetInt("SandboxTools.DiseaseCount", val);
			this.OnChangeDiseaseCount();
		}, 0);
		this.AddIntSetting("SandboxTools.BrushSize", delegate(int val)
		{
			KPlayerPrefs.SetInt("SandboxTools.BrushSize", val);
			this.OnChangeBrushSize();
		}, 1);
		this.AddFloatSetting("SandboxTools.NoiseScale", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandboxTools.NoiseScale", val);
			this.OnChangeNoiseScale();
		}, 1f);
		this.AddFloatSetting("SandboxTools.NoiseDensity", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandboxTools.NoiseDensity", val);
			this.OnChangeNoiseDensity();
		}, 1f);
		this.AddFloatSetting("SandboxTools.Mass", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandboxTools.Mass", val);
			this.OnChangeMass();
		}, 1f);
		this.AddFloatSetting("SandbosTools.Temperature", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandbosTools.Temperature", val);
			this.OnChangeTemperature();
		}, 300f);
		this.AddFloatSetting("SandbosTools.TemperatureAdditive", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandbosTools.TemperatureAdditive", val);
			this.OnChangeAdditiveTemperature();
		}, 5f);
		this.AddFloatSetting("SandbosTools.StressAdditive", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandbosTools.StressAdditive", val);
			this.OnChangeAdditiveStress();
		}, 50f);
		this.AddIntSetting("SandbosTools.MoraleAdjustment", delegate(int val)
		{
			KPlayerPrefs.SetInt("SandbosTools.MoraleAdjustment", val);
			this.OnChangeMoraleAdjustment();
		}, 50);
	}

	// Token: 0x06005C13 RID: 23571 RVA: 0x0021A0BC File Offset: 0x002182BC
	public void RestorePrefs()
	{
		foreach (SandboxSettings.Setting<int> setting in this.intSettings)
		{
			this.RestoreIntSetting(setting.PrefsKey);
		}
		foreach (SandboxSettings.Setting<float> setting2 in this.floatSettings)
		{
			this.RestoreFloatSetting(setting2.PrefsKey);
		}
		foreach (SandboxSettings.Setting<string> setting3 in this.stringSettings)
		{
			this.RestoreStringSetting(setting3.PrefsKey);
		}
	}

	// Token: 0x04003ED6 RID: 16086
	private List<SandboxSettings.Setting<int>> intSettings = new List<SandboxSettings.Setting<int>>();

	// Token: 0x04003ED7 RID: 16087
	private List<SandboxSettings.Setting<float>> floatSettings = new List<SandboxSettings.Setting<float>>();

	// Token: 0x04003ED8 RID: 16088
	private List<SandboxSettings.Setting<string>> stringSettings = new List<SandboxSettings.Setting<string>>();

	// Token: 0x04003ED9 RID: 16089
	public bool InstantBuild = true;

	// Token: 0x04003EDA RID: 16090
	private bool hasRestoredElement;

	// Token: 0x04003EDB RID: 16091
	public Action<bool> OnChangeElement;

	// Token: 0x04003EDC RID: 16092
	public System.Action OnChangeMass;

	// Token: 0x04003EDD RID: 16093
	public System.Action OnChangeDisease;

	// Token: 0x04003EDE RID: 16094
	public System.Action OnChangeDiseaseCount;

	// Token: 0x04003EDF RID: 16095
	public System.Action OnChangeEntity;

	// Token: 0x04003EE0 RID: 16096
	public System.Action OnChangeBrushSize;

	// Token: 0x04003EE1 RID: 16097
	public System.Action OnChangeNoiseScale;

	// Token: 0x04003EE2 RID: 16098
	public System.Action OnChangeNoiseDensity;

	// Token: 0x04003EE3 RID: 16099
	public System.Action OnChangeTemperature;

	// Token: 0x04003EE4 RID: 16100
	public System.Action OnChangeAdditiveTemperature;

	// Token: 0x04003EE5 RID: 16101
	public System.Action OnChangeAdditiveStress;

	// Token: 0x04003EE6 RID: 16102
	public System.Action OnChangeMoraleAdjustment;

	// Token: 0x04003EE7 RID: 16103
	public const string KEY_SELECTED_ENTITY = "SandboxTools.SelectedEntity";

	// Token: 0x04003EE8 RID: 16104
	public const string KEY_SELECTED_ELEMENT = "SandboxTools.SelectedElement";

	// Token: 0x04003EE9 RID: 16105
	public const string KEY_SELECTED_DISEASE = "SandboxTools.SelectedDisease";

	// Token: 0x04003EEA RID: 16106
	public const string KEY_DISEASE_COUNT = "SandboxTools.DiseaseCount";

	// Token: 0x04003EEB RID: 16107
	public const string KEY_BRUSH_SIZE = "SandboxTools.BrushSize";

	// Token: 0x04003EEC RID: 16108
	public const string KEY_NOISE_SCALE = "SandboxTools.NoiseScale";

	// Token: 0x04003EED RID: 16109
	public const string KEY_NOISE_DENSITY = "SandboxTools.NoiseDensity";

	// Token: 0x04003EEE RID: 16110
	public const string KEY_MASS = "SandboxTools.Mass";

	// Token: 0x04003EEF RID: 16111
	public const string KEY_TEMPERATURE = "SandbosTools.Temperature";

	// Token: 0x04003EF0 RID: 16112
	public const string KEY_TEMPERATURE_ADDITIVE = "SandbosTools.TemperatureAdditive";

	// Token: 0x04003EF1 RID: 16113
	public const string KEY_STRESS_ADDITIVE = "SandbosTools.StressAdditive";

	// Token: 0x04003EF2 RID: 16114
	public const string KEY_MORALE_ADJUSTMENT = "SandbosTools.MoraleAdjustment";

	// Token: 0x02001A2B RID: 6699
	public class Setting<T>
	{
		// Token: 0x0600924E RID: 37454 RVA: 0x00317057 File Offset: 0x00315257
		public Setting(string prefsKey, Action<T> setAction, T defaultValue)
		{
			this.prefsKey = prefsKey;
			this.SetAction = setAction;
			this.defaultValue = defaultValue;
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600924F RID: 37455 RVA: 0x00317074 File Offset: 0x00315274
		public string PrefsKey
		{
			get
			{
				return this.prefsKey;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (set) Token: 0x06009250 RID: 37456 RVA: 0x0031707C File Offset: 0x0031527C
		public T Value
		{
			set
			{
				this.SetAction(value);
			}
		}

		// Token: 0x040076B0 RID: 30384
		private string prefsKey;

		// Token: 0x040076B1 RID: 30385
		private Action<T> SetAction;

		// Token: 0x040076B2 RID: 30386
		public T defaultValue;
	}
}
