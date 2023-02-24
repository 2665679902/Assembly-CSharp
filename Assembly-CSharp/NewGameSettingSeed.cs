using System;
using Klei.CustomSettings;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000B39 RID: 2873
public class NewGameSettingSeed : NewGameSettingWidget
{
	// Token: 0x060058F8 RID: 22776 RVA: 0x00203C5C File Offset: 0x00201E5C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.Input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		this.Input.onValueChanged.AddListener(new UnityAction<string>(this.OnValueChanged));
		this.RandomizeButton.onClick += this.GetNewRandomSeed;
	}

	// Token: 0x060058F9 RID: 22777 RVA: 0x00203CBE File Offset: 0x00201EBE
	public void Initialize(SeedSettingConfig config)
	{
		this.config = config;
		this.Label.text = config.label;
		this.ToolTip.toolTip = config.tooltip;
		this.GetNewRandomSeed();
	}

	// Token: 0x060058FA RID: 22778 RVA: 0x00203CF0 File Offset: 0x00201EF0
	public override void Refresh()
	{
		string currentQualitySettingLevelId = CustomGameSettings.Instance.GetCurrentQualitySettingLevelId(this.config);
		this.Input.text = currentQualitySettingLevelId;
	}

	// Token: 0x060058FB RID: 22779 RVA: 0x00203D1A File Offset: 0x00201F1A
	private char ValidateInput(string text, int charIndex, char addedChar)
	{
		if ('0' > addedChar || addedChar > '9')
		{
			return '\0';
		}
		return addedChar;
	}

	// Token: 0x060058FC RID: 22780 RVA: 0x00203D2C File Offset: 0x00201F2C
	private void OnEndEdit(string text)
	{
		int num;
		try
		{
			num = Convert.ToInt32(text);
		}
		catch
		{
			num = 0;
		}
		this.SetSeed(num);
	}

	// Token: 0x060058FD RID: 22781 RVA: 0x00203D60 File Offset: 0x00201F60
	public void SetSeed(int seed)
	{
		seed = Mathf.Min(seed, int.MaxValue);
		CustomGameSettings.Instance.SetQualitySetting(this.config, seed.ToString());
		this.Refresh();
	}

	// Token: 0x060058FE RID: 22782 RVA: 0x00203D8C File Offset: 0x00201F8C
	private void OnValueChanged(string text)
	{
		int num = 0;
		try
		{
			num = Convert.ToInt32(text);
		}
		catch
		{
			if (text.Length > 0)
			{
				this.Input.text = text.Substring(0, text.Length - 1);
			}
			else
			{
				this.Input.text = "";
			}
		}
		if (num > 2147483647)
		{
			this.Input.text = text.Substring(0, text.Length - 1);
		}
	}

	// Token: 0x060058FF RID: 22783 RVA: 0x00203E10 File Offset: 0x00202010
	private void GetNewRandomSeed()
	{
		int num = UnityEngine.Random.Range(0, int.MaxValue);
		this.SetSeed(num);
	}

	// Token: 0x04003C0F RID: 15375
	[SerializeField]
	private LocText Label;

	// Token: 0x04003C10 RID: 15376
	[SerializeField]
	private ToolTip ToolTip;

	// Token: 0x04003C11 RID: 15377
	[SerializeField]
	private KInputTextField Input;

	// Token: 0x04003C12 RID: 15378
	[SerializeField]
	private KButton RandomizeButton;

	// Token: 0x04003C13 RID: 15379
	private const int MAX_VALID_SEED = 2147483647;

	// Token: 0x04003C14 RID: 15380
	private SeedSettingConfig config;
}
