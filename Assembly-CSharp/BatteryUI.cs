using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A4A RID: 2634
[AddComponentMenu("KMonoBehaviour/scripts/BatteryUI")]
public class BatteryUI : KMonoBehaviour
{
	// Token: 0x06004FE5 RID: 20453 RVA: 0x001C7944 File Offset: 0x001C5B44
	private void Initialize()
	{
		if (this.unitLabel == null)
		{
			this.unitLabel = this.currentKJLabel.gameObject.GetComponentInChildrenOnly<LocText>();
		}
		if (this.sizeMap == null || this.sizeMap.Count == 0)
		{
			this.sizeMap = new Dictionary<float, float>();
			this.sizeMap.Add(20000f, 10f);
			this.sizeMap.Add(40000f, 25f);
			this.sizeMap.Add(60000f, 40f);
		}
	}

	// Token: 0x06004FE6 RID: 20454 RVA: 0x001C79D4 File Offset: 0x001C5BD4
	public void SetContent(Battery bat)
	{
		if (bat == null || bat.GetMyWorldId() != ClusterManager.Instance.activeWorldId)
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			return;
		}
		base.gameObject.SetActive(true);
		this.Initialize();
		RectTransform component = this.batteryBG.GetComponent<RectTransform>();
		float num = 0f;
		foreach (KeyValuePair<float, float> keyValuePair in this.sizeMap)
		{
			if (bat.Capacity <= keyValuePair.Key)
			{
				num = keyValuePair.Value;
				break;
			}
		}
		this.batteryBG.sprite = ((bat.Capacity >= 40000f) ? this.bigBatteryBG : this.regularBatteryBG);
		float num2 = 25f;
		component.sizeDelta = new Vector2(num, num2);
		BuildingEnabledButton component2 = bat.GetComponent<BuildingEnabledButton>();
		Color color;
		if (component2 != null && !component2.IsEnabled)
		{
			color = Color.gray;
		}
		else
		{
			color = ((bat.PercentFull >= bat.PreviousPercentFull) ? this.energyIncreaseColor : this.energyDecreaseColor);
		}
		this.batteryMeter.color = color;
		this.batteryBG.color = color;
		float num3 = this.batteryBG.GetComponent<RectTransform>().rect.height * bat.PercentFull;
		this.batteryMeter.GetComponent<RectTransform>().sizeDelta = new Vector2(num - 5.5f, num3 - 5.5f);
		color.a = 1f;
		if (this.currentKJLabel.color != color)
		{
			this.currentKJLabel.color = color;
			this.unitLabel.color = color;
		}
		this.currentKJLabel.text = bat.JoulesAvailable.ToString("F0");
	}

	// Token: 0x0400359A RID: 13722
	[SerializeField]
	private LocText currentKJLabel;

	// Token: 0x0400359B RID: 13723
	[SerializeField]
	private Image batteryBG;

	// Token: 0x0400359C RID: 13724
	[SerializeField]
	private Image batteryMeter;

	// Token: 0x0400359D RID: 13725
	[SerializeField]
	private Sprite regularBatteryBG;

	// Token: 0x0400359E RID: 13726
	[SerializeField]
	private Sprite bigBatteryBG;

	// Token: 0x0400359F RID: 13727
	[SerializeField]
	private Color energyIncreaseColor = Color.green;

	// Token: 0x040035A0 RID: 13728
	[SerializeField]
	private Color energyDecreaseColor = Color.red;

	// Token: 0x040035A1 RID: 13729
	private LocText unitLabel;

	// Token: 0x040035A2 RID: 13730
	private const float UIUnit = 10f;

	// Token: 0x040035A3 RID: 13731
	private Dictionary<float, float> sizeMap;
}
