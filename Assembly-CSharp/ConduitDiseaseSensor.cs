using System;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200059B RID: 1435
[SerializationConfig(MemberSerialization.OptIn)]
public class ConduitDiseaseSensor : ConduitThresholdSensor, IThresholdSwitch
{
	// Token: 0x0600233B RID: 9019 RVA: 0x000BEBEC File Offset: 0x000BCDEC
	protected override void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			if (this.switchedOn)
			{
				this.animController.Play(ConduitSensor.ON_ANIMS, KAnim.PlayMode.Loop);
				int num;
				int num2;
				bool flag;
				this.GetContentsDisease(out num, out num2, out flag);
				Color32 color = Color.white;
				if (num != 255)
				{
					Disease disease = Db.Get().Diseases[num];
					color = GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName);
				}
				this.animController.SetSymbolTint(ConduitDiseaseSensor.TINT_SYMBOL, color);
				return;
			}
			this.animController.Play(ConduitSensor.OFF_ANIMS, KAnim.PlayMode.Once);
		}
	}

	// Token: 0x0600233C RID: 9020 RVA: 0x000BECAC File Offset: 0x000BCEAC
	private void GetContentsDisease(out int diseaseIdx, out int diseaseCount, out bool hasMass)
	{
		int num = Grid.PosToCell(this);
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			ConduitFlow.ConduitContents contents = Conduit.GetFlowManager(this.conduitType).GetContents(num);
			diseaseIdx = (int)contents.diseaseIdx;
			diseaseCount = contents.diseaseCount;
			hasMass = contents.mass > 0f;
			return;
		}
		SolidConduitFlow flowManager = SolidConduit.GetFlowManager();
		SolidConduitFlow.ConduitContents contents2 = flowManager.GetContents(num);
		Pickupable pickupable = flowManager.GetPickupable(contents2.pickupableHandle);
		if (pickupable != null && pickupable.PrimaryElement.Mass > 0f)
		{
			diseaseIdx = (int)pickupable.PrimaryElement.DiseaseIdx;
			diseaseCount = pickupable.PrimaryElement.DiseaseCount;
			hasMass = true;
			return;
		}
		diseaseIdx = 0;
		diseaseCount = 0;
		hasMass = false;
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x0600233D RID: 9021 RVA: 0x000BED60 File Offset: 0x000BCF60
	public override float CurrentValue
	{
		get
		{
			int num;
			int num2;
			bool flag;
			this.GetContentsDisease(out num, out num2, out flag);
			if (flag)
			{
				this.lastValue = (float)num2;
			}
			return this.lastValue;
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x0600233E RID: 9022 RVA: 0x000BED8A File Offset: 0x000BCF8A
	public float RangeMin
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x0600233F RID: 9023 RVA: 0x000BED91 File Offset: 0x000BCF91
	public float RangeMax
	{
		get
		{
			return 100000f;
		}
	}

	// Token: 0x06002340 RID: 9024 RVA: 0x000BED98 File Offset: 0x000BCF98
	public float GetRangeMinInputField()
	{
		return 0f;
	}

	// Token: 0x06002341 RID: 9025 RVA: 0x000BED9F File Offset: 0x000BCF9F
	public float GetRangeMaxInputField()
	{
		return 100000f;
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06002342 RID: 9026 RVA: 0x000BEDA6 File Offset: 0x000BCFA6
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TITLE;
		}
	}

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06002343 RID: 9027 RVA: 0x000BEDAD File Offset: 0x000BCFAD
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE;
		}
	}

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06002344 RID: 9028 RVA: 0x000BEDB4 File Offset: 0x000BCFB4
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06002345 RID: 9029 RVA: 0x000BEDC0 File Offset: 0x000BCFC0
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06002346 RID: 9030 RVA: 0x000BEDCC File Offset: 0x000BCFCC
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedInt((float)((int)value), GameUtil.TimeSlice.None);
	}

	// Token: 0x06002347 RID: 9031 RVA: 0x000BEDD7 File Offset: 0x000BCFD7
	public float ProcessedSliderValue(float input)
	{
		return input;
	}

	// Token: 0x06002348 RID: 9032 RVA: 0x000BEDDA File Offset: 0x000BCFDA
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x06002349 RID: 9033 RVA: 0x000BEDDD File Offset: 0x000BCFDD
	public LocString ThresholdValueUnits()
	{
		return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_UNITS;
	}

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x0600234A RID: 9034 RVA: 0x000BEDE4 File Offset: 0x000BCFE4
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x0600234B RID: 9035 RVA: 0x000BEDE7 File Offset: 0x000BCFE7
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x0600234C RID: 9036 RVA: 0x000BEDEA File Offset: 0x000BCFEA
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x04001447 RID: 5191
	private const float rangeMin = 0f;

	// Token: 0x04001448 RID: 5192
	private const float rangeMax = 100000f;

	// Token: 0x04001449 RID: 5193
	[Serialize]
	private float lastValue;

	// Token: 0x0400144A RID: 5194
	private static readonly HashedString TINT_SYMBOL = "germs";
}
