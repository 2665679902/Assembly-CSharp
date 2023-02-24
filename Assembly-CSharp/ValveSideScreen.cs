using System;
using System.Collections;
using STRINGS;
using UnityEngine;

// Token: 0x02000BF1 RID: 3057
public class ValveSideScreen : SideScreenContent
{
	// Token: 0x06006086 RID: 24710 RVA: 0x00234CD8 File Offset: 0x00232ED8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.unitsLabel.text = GameUtil.AddTimeSliceText(UI.UNITSUFFIXES.MASS.GRAM, GameUtil.TimeSlice.PerSecond);
		this.flowSlider.onReleaseHandle += this.OnReleaseHandle;
		this.flowSlider.onDrag += delegate
		{
			this.ReceiveValueFromSlider(this.flowSlider.value);
		};
		this.flowSlider.onPointerDown += delegate
		{
			this.ReceiveValueFromSlider(this.flowSlider.value);
		};
		this.flowSlider.onMove += delegate
		{
			this.ReceiveValueFromSlider(this.flowSlider.value);
			this.OnReleaseHandle();
		};
		this.numberInput.onEndEdit += delegate
		{
			this.ReceiveValueFromInput(this.numberInput.currentValue);
		};
		this.numberInput.decimalPlaces = 1;
	}

	// Token: 0x06006087 RID: 24711 RVA: 0x00234D85 File Offset: 0x00232F85
	public void OnReleaseHandle()
	{
		this.targetValve.ChangeFlow(this.targetFlow);
	}

	// Token: 0x06006088 RID: 24712 RVA: 0x00234D98 File Offset: 0x00232F98
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Valve>() != null;
	}

	// Token: 0x06006089 RID: 24713 RVA: 0x00234DA8 File Offset: 0x00232FA8
	public override void SetTarget(GameObject target)
	{
		this.targetValve = target.GetComponent<Valve>();
		if (this.targetValve == null)
		{
			global::Debug.LogError("The target object does not have a Valve component.");
			return;
		}
		this.flowSlider.minValue = 0f;
		this.flowSlider.maxValue = this.targetValve.MaxFlow;
		this.flowSlider.value = this.targetValve.DesiredFlow;
		this.minFlowLabel.text = GameUtil.GetFormattedMass(0f, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.Gram, true, "{0:0.#}");
		this.maxFlowLabel.text = GameUtil.GetFormattedMass(this.targetValve.MaxFlow, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.Gram, true, "{0:0.#}");
		this.numberInput.minValue = 0f;
		this.numberInput.maxValue = this.targetValve.MaxFlow * 1000f;
		this.numberInput.SetDisplayValue(GameUtil.GetFormattedMass(Mathf.Max(0f, this.targetValve.DesiredFlow), GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.Gram, false, "{0:0.#####}"));
		this.numberInput.Activate();
	}

	// Token: 0x0600608A RID: 24714 RVA: 0x00234EBA File Offset: 0x002330BA
	private void ReceiveValueFromSlider(float newValue)
	{
		newValue = Mathf.Round(newValue * 1000f) / 1000f;
		this.UpdateFlowValue(newValue);
	}

	// Token: 0x0600608B RID: 24715 RVA: 0x00234ED8 File Offset: 0x002330D8
	private void ReceiveValueFromInput(float input)
	{
		float num = input / 1000f;
		this.UpdateFlowValue(num);
		this.targetValve.ChangeFlow(this.targetFlow);
	}

	// Token: 0x0600608C RID: 24716 RVA: 0x00234F05 File Offset: 0x00233105
	private void UpdateFlowValue(float newValue)
	{
		this.targetFlow = newValue;
		this.flowSlider.value = newValue;
		this.numberInput.SetDisplayValue(GameUtil.GetFormattedMass(newValue, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.Gram, false, "{0:0.#####}"));
	}

	// Token: 0x0600608D RID: 24717 RVA: 0x00234F33 File Offset: 0x00233133
	private IEnumerator SettingDelay(float delay)
	{
		float startTime = Time.realtimeSinceStartup;
		float currentTime = startTime;
		while (currentTime < startTime + delay)
		{
			currentTime += Time.unscaledDeltaTime;
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		this.OnReleaseHandle();
		yield break;
	}

	// Token: 0x04004224 RID: 16932
	private Valve targetValve;

	// Token: 0x04004225 RID: 16933
	[Header("Slider")]
	[SerializeField]
	private KSlider flowSlider;

	// Token: 0x04004226 RID: 16934
	[SerializeField]
	private LocText minFlowLabel;

	// Token: 0x04004227 RID: 16935
	[SerializeField]
	private LocText maxFlowLabel;

	// Token: 0x04004228 RID: 16936
	[Header("Input Field")]
	[SerializeField]
	private KNumberInputField numberInput;

	// Token: 0x04004229 RID: 16937
	[SerializeField]
	private LocText unitsLabel;

	// Token: 0x0400422A RID: 16938
	private float targetFlow;
}
