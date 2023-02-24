using System;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005E5 RID: 1509
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicDiseaseSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x06002604 RID: 9732 RVA: 0x000CD34D File Offset: 0x000CB54D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicDiseaseSensor>(-905833192, LogicDiseaseSensor.OnCopySettingsDelegate);
	}

	// Token: 0x06002605 RID: 9733 RVA: 0x000CD368 File Offset: 0x000CB568
	private void OnCopySettings(object data)
	{
		LogicDiseaseSensor component = ((GameObject)data).GetComponent<LogicDiseaseSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x06002606 RID: 9734 RVA: 0x000CD3A2 File Offset: 0x000CB5A2
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.animController = base.GetComponent<KBatchedAnimController>();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x000CD3E4 File Offset: 0x000CB5E4
	public void Sim200ms(float dt)
	{
		if (this.sampleIdx < 8)
		{
			int num = Grid.PosToCell(this);
			if (Grid.Mass[num] > 0f)
			{
				this.samples[this.sampleIdx] = Grid.DiseaseCount[num];
				this.sampleIdx++;
			}
			return;
		}
		this.sampleIdx = 0;
		float currentValue = this.CurrentValue;
		if (this.activateAboveThreshold)
		{
			if ((currentValue > this.threshold && !base.IsSwitchedOn) || (currentValue <= this.threshold && base.IsSwitchedOn))
			{
				this.Toggle();
			}
		}
		else if ((currentValue > this.threshold && base.IsSwitchedOn) || (currentValue <= this.threshold && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
		this.animController.SetSymbolVisiblity(LogicDiseaseSensor.TINT_SYMBOL, currentValue > 0f);
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x000CD4BF File Offset: 0x000CB6BF
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06002609 RID: 9737 RVA: 0x000CD4CE File Offset: 0x000CB6CE
	// (set) Token: 0x0600260A RID: 9738 RVA: 0x000CD4D6 File Offset: 0x000CB6D6
	public float Threshold
	{
		get
		{
			return this.threshold;
		}
		set
		{
			this.threshold = value;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x0600260B RID: 9739 RVA: 0x000CD4DF File Offset: 0x000CB6DF
	// (set) Token: 0x0600260C RID: 9740 RVA: 0x000CD4E7 File Offset: 0x000CB6E7
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateAboveThreshold;
		}
		set
		{
			this.activateAboveThreshold = value;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x0600260D RID: 9741 RVA: 0x000CD4F0 File Offset: 0x000CB6F0
	public float CurrentValue
	{
		get
		{
			float num = 0f;
			for (int i = 0; i < 8; i++)
			{
				num += (float)this.samples[i];
			}
			return num / 8f;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x0600260E RID: 9742 RVA: 0x000CD522 File Offset: 0x000CB722
	public float RangeMin
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x0600260F RID: 9743 RVA: 0x000CD529 File Offset: 0x000CB729
	public float RangeMax
	{
		get
		{
			return 100000f;
		}
	}

	// Token: 0x06002610 RID: 9744 RVA: 0x000CD530 File Offset: 0x000CB730
	public float GetRangeMinInputField()
	{
		return 0f;
	}

	// Token: 0x06002611 RID: 9745 RVA: 0x000CD537 File Offset: 0x000CB737
	public float GetRangeMaxInputField()
	{
		return 100000f;
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06002612 RID: 9746 RVA: 0x000CD53E File Offset: 0x000CB73E
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE;
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06002613 RID: 9747 RVA: 0x000CD545 File Offset: 0x000CB745
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06002614 RID: 9748 RVA: 0x000CD551 File Offset: 0x000CB751
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06002615 RID: 9749 RVA: 0x000CD55D File Offset: 0x000CB75D
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedInt((float)((int)value), GameUtil.TimeSlice.None);
	}

	// Token: 0x06002616 RID: 9750 RVA: 0x000CD568 File Offset: 0x000CB768
	public float ProcessedSliderValue(float input)
	{
		return input;
	}

	// Token: 0x06002617 RID: 9751 RVA: 0x000CD56B File Offset: 0x000CB76B
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x06002618 RID: 9752 RVA: 0x000CD56E File Offset: 0x000CB76E
	public LocString ThresholdValueUnits()
	{
		return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_UNITS;
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x06002619 RID: 9753 RVA: 0x000CD575 File Offset: 0x000CB775
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x0600261A RID: 9754 RVA: 0x000CD578 File Offset: 0x000CB778
	public int IncrementScale
	{
		get
		{
			return 100;
		}
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x0600261B RID: 9755 RVA: 0x000CD57C File Offset: 0x000CB77C
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x0600261C RID: 9756 RVA: 0x000CD589 File Offset: 0x000CB789
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x0600261D RID: 9757 RVA: 0x000CD5A8 File Offset: 0x000CB7A8
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			if (this.switchedOn)
			{
				this.animController.Play(LogicDiseaseSensor.ON_ANIMS, KAnim.PlayMode.Loop);
				int num = Grid.PosToCell(this);
				byte b = Grid.DiseaseIdx[num];
				Color32 color = Color.white;
				if (b != 255)
				{
					Disease disease = Db.Get().Diseases[(int)b];
					color = GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName);
				}
				this.animController.SetSymbolTint(LogicDiseaseSensor.TINT_SYMBOL, color);
				return;
			}
			this.animController.Play(LogicDiseaseSensor.OFF_ANIMS, KAnim.PlayMode.Once);
		}
	}

	// Token: 0x0600261E RID: 9758 RVA: 0x000CD66C File Offset: 0x000CB86C
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x0600261F RID: 9759 RVA: 0x000CD6BF File Offset: 0x000CB8BF
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TITLE;
		}
	}

	// Token: 0x04001637 RID: 5687
	[SerializeField]
	[Serialize]
	private float threshold;

	// Token: 0x04001638 RID: 5688
	[SerializeField]
	[Serialize]
	private bool activateAboveThreshold = true;

	// Token: 0x04001639 RID: 5689
	private KBatchedAnimController animController;

	// Token: 0x0400163A RID: 5690
	private bool wasOn;

	// Token: 0x0400163B RID: 5691
	private const float rangeMin = 0f;

	// Token: 0x0400163C RID: 5692
	private const float rangeMax = 100000f;

	// Token: 0x0400163D RID: 5693
	private const int WINDOW_SIZE = 8;

	// Token: 0x0400163E RID: 5694
	private int[] samples = new int[8];

	// Token: 0x0400163F RID: 5695
	private int sampleIdx;

	// Token: 0x04001640 RID: 5696
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001641 RID: 5697
	private static readonly EventSystem.IntraObjectHandler<LogicDiseaseSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicDiseaseSensor>(delegate(LogicDiseaseSensor component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001642 RID: 5698
	private static readonly HashedString[] ON_ANIMS = new HashedString[] { "on_pre", "on_loop" };

	// Token: 0x04001643 RID: 5699
	private static readonly HashedString[] OFF_ANIMS = new HashedString[] { "on_pst", "off" };

	// Token: 0x04001644 RID: 5700
	private static readonly HashedString TINT_SYMBOL = "germs";
}
