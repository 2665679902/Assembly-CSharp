using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class KNumberInputField : KInputField
{
	// Token: 0x060003B5 RID: 949 RVA: 0x000131AD File Offset: 0x000113AD
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x000131B8 File Offset: 0x000113B8
	public void SetAmount(float newValue)
	{
		newValue = Mathf.Clamp(newValue, this.minValue, this.maxValue);
		if (this.decimalPlaces != -1)
		{
			float num = Mathf.Pow(10f, (float)this.decimalPlaces);
			newValue = Mathf.Round(newValue * num) / num;
		}
		this.currentValue = newValue;
		base.SetDisplayValue(this.currentValue.ToString());
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00013218 File Offset: 0x00011418
	protected override void ProcessInput(string input)
	{
		input = ((input == "") ? this.minValue.ToString() : input);
		float num = this.minValue;
		try
		{
			num = float.Parse(input);
			this.SetAmount(num);
		}
		catch
		{
		}
	}

	// Token: 0x0400043C RID: 1084
	public int decimalPlaces = -1;

	// Token: 0x0400043D RID: 1085
	public float currentValue;

	// Token: 0x0400043E RID: 1086
	public float minValue;

	// Token: 0x0400043F RID: 1087
	public float maxValue;
}
