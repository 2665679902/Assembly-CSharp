using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005E4 RID: 1508
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicCritterCountSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x060025E6 RID: 9702 RVA: 0x000CCF96 File Offset: 0x000CB196
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.selectable = base.GetComponent<KSelectable>();
		base.Subscribe<LogicCritterCountSensor>(-905833192, LogicCritterCountSensor.OnCopySettingsDelegate);
	}

	// Token: 0x060025E7 RID: 9703 RVA: 0x000CCFBC File Offset: 0x000CB1BC
	private void OnCopySettings(object data)
	{
		LogicCritterCountSensor component = ((GameObject)data).GetComponent<LogicCritterCountSensor>();
		if (component != null)
		{
			this.countThreshold = component.countThreshold;
			this.activateOnGreaterThan = component.activateOnGreaterThan;
			this.countCritters = component.countCritters;
			this.countEggs = component.countEggs;
		}
	}

	// Token: 0x060025E8 RID: 9704 RVA: 0x000CD00E File Offset: 0x000CB20E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
	}

	// Token: 0x060025E9 RID: 9705 RVA: 0x000CD044 File Offset: 0x000CB244
	public void Sim200ms(float dt)
	{
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		if (roomOfGameObject != null)
		{
			this.currentCount = 0;
			if (this.countCritters)
			{
				this.currentCount += roomOfGameObject.cavity.creatures.Count;
			}
			if (this.countEggs)
			{
				this.currentCount += roomOfGameObject.cavity.eggs.Count;
			}
			bool flag = (this.activateOnGreaterThan ? (this.currentCount > this.countThreshold) : (this.currentCount < this.countThreshold));
			this.SetState(flag);
			if (this.selectable.HasStatusItem(Db.Get().BuildingStatusItems.NotInAnyRoom))
			{
				this.selectable.RemoveStatusItem(this.roomStatusGUID, false);
				return;
			}
		}
		else
		{
			if (!this.selectable.HasStatusItem(Db.Get().BuildingStatusItems.NotInAnyRoom))
			{
				this.roomStatusGUID = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.NotInAnyRoom, null);
			}
			this.SetState(false);
		}
	}

	// Token: 0x060025EA RID: 9706 RVA: 0x000CD160 File Offset: 0x000CB360
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x060025EB RID: 9707 RVA: 0x000CD16F File Offset: 0x000CB36F
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x060025EC RID: 9708 RVA: 0x000CD190 File Offset: 0x000CB390
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x060025ED RID: 9709 RVA: 0x000CD218 File Offset: 0x000CB418
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x060025EE RID: 9710 RVA: 0x000CD26B File Offset: 0x000CB46B
	// (set) Token: 0x060025EF RID: 9711 RVA: 0x000CD274 File Offset: 0x000CB474
	public float Threshold
	{
		get
		{
			return (float)this.countThreshold;
		}
		set
		{
			this.countThreshold = (int)value;
		}
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000CD27E File Offset: 0x000CB47E
	// (set) Token: 0x060025F1 RID: 9713 RVA: 0x000CD286 File Offset: 0x000CB486
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateOnGreaterThan;
		}
		set
		{
			this.activateOnGreaterThan = value;
		}
	}

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x060025F2 RID: 9714 RVA: 0x000CD28F File Offset: 0x000CB48F
	public float CurrentValue
	{
		get
		{
			return (float)this.currentCount;
		}
	}

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x060025F3 RID: 9715 RVA: 0x000CD298 File Offset: 0x000CB498
	public float RangeMin
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x060025F4 RID: 9716 RVA: 0x000CD29F File Offset: 0x000CB49F
	public float RangeMax
	{
		get
		{
			return 64f;
		}
	}

	// Token: 0x060025F5 RID: 9717 RVA: 0x000CD2A6 File Offset: 0x000CB4A6
	public float GetRangeMinInputField()
	{
		return this.RangeMin;
	}

	// Token: 0x060025F6 RID: 9718 RVA: 0x000CD2AE File Offset: 0x000CB4AE
	public float GetRangeMaxInputField()
	{
		return this.RangeMax;
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x060025F7 RID: 9719 RVA: 0x000CD2B6 File Offset: 0x000CB4B6
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.CRITTER_COUNT_SIDE_SCREEN.TITLE;
		}
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000CD2BD File Offset: 0x000CB4BD
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.CRITTER_COUNT_SIDE_SCREEN.VALUE_NAME;
		}
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x060025F9 RID: 9721 RVA: 0x000CD2C4 File Offset: 0x000CB4C4
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.CRITTER_COUNT_SIDE_SCREEN.TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x060025FA RID: 9722 RVA: 0x000CD2D0 File Offset: 0x000CB4D0
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.CRITTER_COUNT_SIDE_SCREEN.TOOLTIP_BELOW;
		}
	}

	// Token: 0x060025FB RID: 9723 RVA: 0x000CD2DC File Offset: 0x000CB4DC
	public string Format(float value, bool units)
	{
		return value.ToString();
	}

	// Token: 0x060025FC RID: 9724 RVA: 0x000CD2E5 File Offset: 0x000CB4E5
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x060025FD RID: 9725 RVA: 0x000CD2ED File Offset: 0x000CB4ED
	public float ProcessedInputValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x060025FE RID: 9726 RVA: 0x000CD2F5 File Offset: 0x000CB4F5
	public LocString ThresholdValueUnits()
	{
		return "";
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x060025FF RID: 9727 RVA: 0x000CD301 File Offset: 0x000CB501
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06002600 RID: 9728 RVA: 0x000CD304 File Offset: 0x000CB504
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06002601 RID: 9729 RVA: 0x000CD307 File Offset: 0x000CB507
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x0400162D RID: 5677
	private bool wasOn;

	// Token: 0x0400162E RID: 5678
	[Serialize]
	public bool countEggs = true;

	// Token: 0x0400162F RID: 5679
	[Serialize]
	public bool countCritters = true;

	// Token: 0x04001630 RID: 5680
	[Serialize]
	public int countThreshold;

	// Token: 0x04001631 RID: 5681
	[Serialize]
	public bool activateOnGreaterThan = true;

	// Token: 0x04001632 RID: 5682
	private int currentCount;

	// Token: 0x04001633 RID: 5683
	private KSelectable selectable;

	// Token: 0x04001634 RID: 5684
	private Guid roomStatusGUID;

	// Token: 0x04001635 RID: 5685
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001636 RID: 5686
	private static readonly EventSystem.IntraObjectHandler<LogicCritterCountSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicCritterCountSensor>(delegate(LogicCritterCountSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
